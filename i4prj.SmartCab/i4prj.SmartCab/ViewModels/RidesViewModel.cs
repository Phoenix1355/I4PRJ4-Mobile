using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using i4prj.SmartCab.Models;
using Xamarin.Forms;
using i4prj.SmartCab.Views;
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

        /// <summary>
        /// Gets or sets the rides collection to display in the view.
        /// </summary>
        /// <value>The rides for display in the view.</value>
        public ObservableCollection<RidesGroup> Rides { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.ViewModels.RidesViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="dialogService">Dialog service.</param>
        /// <param name="backendApiService">Backend API service.</param>
        /// <param name="sessionService">Session service.</param>
        public RidesViewModel(INavigationService navigationService, IPageDialogService dialogService, IBackendApiService backendApiService, ISessionService sessionService) : base(navigationService, dialogService)
        {
            Title = "Turoversigt";

            _backendApiService = backendApiService;
            _sessionService = sessionService;

            Rides = new ObservableCollection<RidesGroup>();
        }

        /// <summary>
        /// Loads the rides.
        /// </summary>
        /// <returns>The rides.</returns>
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
        /// <summary>
        /// On appearing handler.
        /// (Re)loads the rides.
        /// </summary>
        public void OnAppearing()
        {
            _ = LoadRides();

            Debug.Write("RidesViewModel::OnAppearing");
        }
        #endregion

        #region Commands
        private DelegateCommand _updateListCommand;
        /// <summary>
        /// Update list of rides command.
        /// </summary>
        /// <value>The update list command.</value>
        public DelegateCommand UpdateListCommand => _updateListCommand ?? (_updateListCommand = new DelegateCommand(UpdateListCommandAsync));

        private void UpdateListCommandAsync()
        {
            _ = LoadRides();
        }
        #endregion

        #region Properties
        private bool _isRefreshing = false;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:i4prj.SmartCab.ViewModels.RidesViewModel"/> is refreshing the list of rides.
        /// </summary>
        /// <value><c>true</c> if it is refreshing; otherwise, <c>false</c>.</value>
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