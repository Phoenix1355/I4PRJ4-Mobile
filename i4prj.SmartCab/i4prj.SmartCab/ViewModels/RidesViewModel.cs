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
using i4prj.SmartCab.Responses;

namespace i4prj.SmartCab.ViewModels
{
    public class RidesViewModel : ViewModelBase, IPageLifecycleAware
    {
        private IBackendApiService _backendApiService;
        private ISessionService _sessionService;

        public ObservableCollection<RidesGroup> Rides { get; set; }

        public RidesViewModel(INavigationService navigationService, IPageDialogService dialogService, IBackendApiService backendApiService, ISessionService sessionService) : base(navigationService, dialogService)
        {
            Title = "Turoversigt";

            _backendApiService = backendApiService;
            _sessionService = sessionService;

            Rides = new ObservableCollection<RidesGroup>();
        }

        private async Task LoadRides()
        {
            // Show loader
            IsRefreshing = true;

            // Get rides
            CustomerRidesResponse customerRidesResponse = await _backendApiService.GetCustomerRides();

            // Log out user if request was unauthorized
            if (customerRidesResponse.WasUnauthorized())
            {
                _sessionService.Clear();

                await DialogService.DisplayAlertAsync("Udløbet log ind", "Dit log ind er udløbet. Du vil blive vist til log ind siden.", "OK");

                await NavigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(LoginPage));
            }
            // Build Rides property
            else
            {
                // Create the two groups of rides
                var openRidesGroup = new RidesGroup("Åbne ture");
                var archivedRidesGroup = new RidesGroup("Arkiverede ture");

                // Iterate groups from Backend and add to groups
                foreach (var item in customerRidesResponse.Body.rides)
                {
                    var ride = new Ride(item);
                    if (ride.IsOpen())
                    {
                        openRidesGroup.Add(ride);
                    }
                    else
                    {
                        archivedRidesGroup.Add(ride);
                    }
                }

                // Clear Rides property (as this method can be called numerous times)
                if (Rides != null && Rides.Count > 0) Rides.Clear();

                // Add non-empty groups to Rides property
                if (openRidesGroup.Count > 0) Rides.Add(openRidesGroup);
                if (archivedRidesGroup.Count > 0) Rides.Add(archivedRidesGroup);
            }

            IsRefreshing = false;
        }

        #region PageLifeCycleAware
        public void OnAppearing()
        {
            LoadRides();

            Debug.Write("RidesViewModel::OnAppearing");
        }

        public void OnDisappearing()
        {
            Debug.Write("RidesViewModel::OnDisappearing");
        }
        #endregion

        #region Commands
        private DelegateCommand _updateListCommand;
        public DelegateCommand UpdateListCommand => _updateListCommand ?? (_updateListCommand = new DelegateCommand(UpdateListCommandAsync));

        private void UpdateListCommandAsync()
        {
            LoadRides();
        }
        #endregion

        #region Properties
        private bool _isRefreshing = false;
        public bool IsRefreshing 
        {
            get { return _isRefreshing;  }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
        #endregion
    }
}

/*
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
 */
