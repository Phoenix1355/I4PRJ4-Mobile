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
using i4prj.SmartCab.CustomControls;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Essentials.Map;

namespace i4prj.SmartCab.ViewModels
{
    public class MapsViewModel : ViewModelBase
    {

        private readonly IBackendApiService _backendApiService;
        private readonly double _radiusMargin = 1.5;
        public IMapsService _mapsService;

        /// <summary>
        /// Initializes an instance of the <see cref="MapsViewModel"/>
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        /// <param name="sessionService"></param>
        /// <param name="apiService"></param>

        public MapsViewModel(INavigationService navigationService, IPageDialogService dialogService, ISessionService sessionService, IBackendApiService apiService) : base(navigationService, dialogService, sessionService)
        {
            _backendApiService = apiService;
            _mapsService = new GoogleMapsService(new GeocodingService());
            _locationPins = new ObservableCollection<Pin>();
            _positionOfMap = new Position();
        }

        private async void SetUpMap()
        {
            string fromAddress = Request.CreateStringAddress("origin");
            string toAddress = Request.CreateStringAddress("destination");

            Xamarin.Essentials.Location fromLocation = await _mapsService.GetPosition(fromAddress);
            Xamarin.Essentials.Location toLocation = await _mapsService.GetPosition(toAddress);

            if (fromLocation != null && toLocation != null)
            {
                _locationPins.Add(new Pin(){Address=fromAddress,Type=PinType.Generic,Label="Startdestination",Position =new Position(fromLocation.Latitude,fromLocation.Longitude)});
                _locationPins.Add(new Pin() { Address = toAddress, Type = PinType.Generic, Label = "Slutdestination", Position = new Position(toLocation.Latitude, toLocation.Longitude) });

                MapRadius = _mapsService.GetMapRadius(fromLocation,toLocation,_radiusMargin);
                PositionOfMap = _mapsService.GetMiddlePosition(fromLocation, toLocation);
            }
        }

        private ObservableCollection<Pin> _locationPins;
        public ObservableCollection<Pin> LocationPins
        {
            get { return _locationPins; }
            set { SetProperty(ref _locationPins, value); }
        }

        private Position _positionOfMap;

        public Position PositionOfMap
        {
            get { return _positionOfMap; }
            set { SetProperty(ref _positionOfMap, value); }
        }

        private double _mapRadius;

        public double MapRadius
        {
            get { return _mapRadius; }
            set { SetProperty(ref _mapRadius, value); }
        }

        private ICreateRideRequest _request;
        public ICreateRideRequest Request
        {
            get { return _request; }
            set
            {
                SetProperty(ref _request, value);
            }
        }

        private string _price;

        public string Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private DelegateCommand _confirmCommand;
        /// <summary>
        /// Submits the CreateRideRequest
        /// </summary>
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
                await DialogService.DisplayAlertAsync("Succes", "Turen er blevet oprettet \nAt betale: " + response.Body.price + " kr.", "OK");
                await NavigationService.NavigateAsync("/" + nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
            }

        }

        private DelegateCommand _cancelCommand;
        /// <summary>
        /// The view navigates back to the CreateRideViewModel
        /// </summary>
        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(CancelCommandExecute));

        private async void CancelCommandExecute()
        {
            await NavigationService.GoBackAsync();
        }

        /// <summary>
        /// Initializes Price and Request to the value of parameters. If the properties are not null, the SetUpMap is called.
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if(parameters.ContainsKey("Price"))
                Price = "At betale: " + parameters.GetValue<string>("Price")+ " kr.";

            if(parameters.ContainsKey("Ride"))
                Request = parameters.GetValue<CreateRideRequest>("Ride");

            if (Request != null && !Price.Length.Equals(0))
            {
                SetUpMap();
            }

        }
        
    }
}
