using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using i4prj.SmartCab.Models;
using Xamarin.Forms;
using i4prj.SmartCab.Views;
using i4prj.SmartCab.Services;
using System.Diagnostics;
using i4prj.SmartCab.Interfaces;
using System.Collections.ObjectModel;
using Prism.AppModel;

namespace i4prj.SmartCab.ViewModels
{
    public class RidesViewModel : ViewModelBase, IPageLifecycleAware
    {
        private IBackendApiService _backendApiService;

        public ObservableCollection<RideGroup> Rides { get; set; }

        public RidesViewModel(INavigationService navigationService, IPageDialogService dialogService, IBackendApiService backendApiService) : base(navigationService, dialogService)
        {
            Title = "Turoversigt";

            _backendApiService = backendApiService;

            Rides = new ObservableCollection<RideGroup>
            {
                new RideGroup("Åbne ture")
                {
                    new Ride {
                        Origin = new Address
                        {
                            CityName = "Aarhus C",
                            StreetName = "Læssøesgade",
                            StreetNumber = 45,
                            PostalCode = 8000
                        },
                        Destination = new Address
                        {
                            CityName = "Aarhus C",
                            StreetName = "Banegårdsgade",
                            StreetNumber = 1,
                            PostalCode = 8000
                        },
                        DepartureTime = DateTime.Now.Add(TimeSpan.FromMinutes(30)),
                        ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromMinutes(2)),
                        AmountOfPassengers = 2,
                        Shared = false,
                        Price = 249.95,
                        Status = Ride.RideStatus.WaitingForAccept,
                        Index = 0
                    },
                    new Ride {
                        Origin = new Address
                        {
                            CityName = "Aarhus C",
                            StreetName = "Læssøesgade",
                            StreetNumber = 45,
                            PostalCode = 8000
                        },
                        Destination = new Address
                        {
                            CityName = "Aarhus C",
                            StreetName = "Banegårdsgade",
                            StreetNumber = 1,
                            PostalCode = 8000
                        },
                        DepartureTime = DateTime.Now.Add(TimeSpan.FromMinutes(30)),
                        ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromMinutes(5)),
                        AmountOfPassengers = 2,
                        Shared = false,
                        Price = 300.00,
                        Status = Ride.RideStatus.Accepted,
                        Index = 1
                    }
                },
                new RideGroup("Arkiverede ture")
                {
                    new Ride {
                        Origin = new Address
                        {
                            CityName = "Aarhus C",
                            StreetName = "Læssøesgade",
                            StreetNumber = 45,
                            PostalCode = 8000
                        },
                        Destination = new Address
                        {
                            CityName = "Aarhus C",
                            StreetName = "Lundbyesgade",
                            StreetNumber = 8,
                            PostalCode = 8000
                        },
                        DepartureTime = DateTime.Now.Subtract(TimeSpan.FromDays(2)),
                        ConfirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromDays(2)).Subtract(TimeSpan.FromMinutes(30)),
                        AmountOfPassengers = 1,
                        Shared = false,
                        Price = 100.00,
                        Status = Ride.RideStatus.WaitingForAccept
                    }
                }
            };
        }

        public void OnAppearing()
        {
            // TODO: Hent rides
            Debug.Write("RidesViewModel::OnAppearing");
        }

        public void OnDisappearing()
        {
            Debug.Write("RidesViewModel::OnDisappearing");
        }

        private DelegateCommand _updateListCommand;

        public DelegateCommand UpdateListCommand => _updateListCommand ?? (_updateListCommand = new DelegateCommand(UpdateListCommandAsync));

        private async void UpdateListCommandAsync()
        {
            await DialogService.DisplayAlertAsync("Opdater liste", "Wuhuu", "OK");

            IsRefreshing = false;
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing 
        {
            get { return _isRefreshing;  }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }

        /*private DelegateCommand _getRidesCommand;
        public DelegateCommand GetRidesCommand => _getRidesCommand ?? (_getRidesCommand = new DelegateCommand(GetRidesCommandExecute));

        private async void GetRidesCommandExecute()
        {
            AzureApiService api = new AzureApiService();

            var response = await api.GetRides();

            string responseBodyAsText = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("GET RESPONSE");
            Debug.WriteLine(responseBodyAsText);
        }*/
    }


}
