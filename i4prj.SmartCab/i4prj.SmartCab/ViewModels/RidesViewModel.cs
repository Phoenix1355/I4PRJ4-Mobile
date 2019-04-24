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

            /*
            public IAddress Origin { get; set; }
            public IAddress Destination { get; set; }
            public DateTime DepartureTime { get; set; }
            public DateTime ConfirmationDeadline { get; set; }
            public int AmountOfPassengers { get; set; }
            public bool Shared { get; set; }
            public double Price { get; set; }
            public RideStatus Status { get; set; }
            */

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
                        ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromMinutes(5)),
                        AmountOfPassengers = 2,
                        Shared = false,
                        Price = 249.95,
                        Status = Ride.RideStatus.WaitingForAccept
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
                        Price = 100,
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
