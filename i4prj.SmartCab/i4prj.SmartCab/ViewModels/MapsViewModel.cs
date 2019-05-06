using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace i4prj.SmartCab.ViewModels
{
    public class MapsViewModel : ViewModelBase
    {

        private readonly IBackendApiService _backendApiService;
        private readonly IMapsService _mapsService;

        public MapsViewModel(INavigationService navigationService, IPageDialogService dialogService, ISessionService sessionService, IBackendApiService apiService) : base(navigationService, dialogService, sessionService)
        {
            _backendApiService = apiService;
            _mapsService = new GoogleMapsService();
            _locations = new ObservableCollection<Location>();
        }

        private async void GetPositionsFromRequest()
        {
            
            List<string> addresses = _mapsService.ConvertRequestToAddresses(Request);
            List<Xamarin.Essentials.Location> locations = await _mapsService.GetPosition(addresses);

            _locations.Add(new Location(addresses[0],"Startdestination",new Position(locations[0].Latitude, locations[0].Longitude)));
            _locations.Add(new Location(addresses[1],"Slutdestination", new Position(locations[1].Latitude, locations[1].Longitude)));

            MapView = MapSpan.FromCenterAndRadius(new Position(10, 10),Distance.FromKilometers(10));
        }

        private ObservableCollection<Location> _locations;

        public IEnumerable Locations => _locations;

        private ICreateRideRequest _request;
        public ICreateRideRequest Request
        {
            get { return _request; }
            set
            {
                SetProperty(ref _request, value);
            }
        }

        private MapSpan _mapView;
        public MapSpan MapView
        {
            get { return _mapView; }
            set
            {
                SetProperty(ref _mapView, value);
            }
        }

        private string _price;

        public string Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }


        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ?? (_confirmCommand = new DelegateCommand(ConfirmCommandExecute));

        private async void ConfirmCommandExecute()
        {
            IsBusy = true;
            CreateRideResponse response = await _backendApiService.SubmitCreateRideRequest(Request);
            IsBusy = false;    

            if (response == null)
            {
                await DialogService.DisplayAlertAsync("Forbindelse", "Du har ikke forbindelse til internettet", "OK");
                await NavigationService.GoBackAsync();
            }
            else if (response.WasUnsuccessfull())
            {
                await DialogService.DisplayAlertAsync("Fejl", response.Body.errors.First().Value[0], "OK");
                await NavigationService.GoBackAsync();
            }
            else if (response.WasSuccessfull())
            {
                await DialogService.DisplayAlertAsync("Success", "Turen er blevet oprettet \nAt betale: " + response.Body.price + " kr.", "OK");
                await NavigationService.NavigateAsync("/" + nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
            }

        }

        private DelegateCommand _cancelCommand;

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(CancelCommandExecute));

        private async void CancelCommandExecute()
        {
            await NavigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Price = parameters.GetValue<string>("Price");

            Request = parameters.GetValue<CreateRideRequest>("Ride");

            GetPositionsFromRequest();
        }


        public class Location : INotifyPropertyChanged
        {
            Position _position;

            public string Address { get; }
            public string TypeOfAddress { get; }

            public Position Position
            {
                get => _position;
                set
                {
                    if (!_position.Equals(value))
                    {
                        _position = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
                    }
                }
            }

            public Location(string address,string typeOfAddress, Position position)
            {
                Address = address;
                Position = position;
                TypeOfAddress = typeOfAddress;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            
        }
        
    }
}
