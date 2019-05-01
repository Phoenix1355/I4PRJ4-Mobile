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
using System.Collections.Generic;
using System.Linq;

namespace i4prj.SmartCab.ViewModels
{
    public class RidesViewModel : ViewModelBase
    {
        private IBackendApiService _backendApiService;
        private ISessionService _sessionService;
        private IRidesService _ridesService;

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
        public RidesViewModel(INavigationService navigationService, IPageDialogService dialogService, IBackendApiService backendApiService, ISessionService sessionService, IRidesService ridesService) : base(navigationService, dialogService)
        {
            Title = "Turoversigt";

            _backendApiService = backendApiService;
            _sessionService = sessionService;
            _ridesService = ridesService;

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
            else if (customerRidesResponse.WasSuccessfull())
            {
                // Convert the API response to Ride class
                var allRides = _ridesService.CreateRidesFromDTO(customerRidesResponse.Body.rides);

                // Filter rides
                var openRides = _ridesService.GetOpenRides(allRides);
                var archivedRides = _ridesService.GetArchivedRides(allRides);

                // Update rides
                SetRides(openRides, archivedRides);
            } 
            else
            {
                await DialogService.DisplayAlertAsync("Fejl", "Vi beklager, men noget gik galt og vi kan derfor ikke vise din tuveroversigt.", "OK");
            }

            IsRefreshing = false;
        }

        /// <summary>
        /// Sets the Rides property in two groups based on parameters.
        /// </summary>
        /// <param name="openRides">Open rides.</param>
        /// <param name="archivedRides">Archived rides.</param>
        private void SetRides(IEnumerable<IRide> openRides, IEnumerable<IRide> archivedRides)
        {
            // Clear Rides property (as this can be done numerous times)
            if (Rides != null && Rides.Count > 0) Rides.Clear();

            // Set Index property on rides to show alternating background color
            var i = 0;
            foreach (var item in openRides)
            {
                item.Index = i;
                i++;
            }

            i = 0;
            foreach (var item in archivedRides)
            {
                item.Index = i;
                i++;
            }

            // Create group for open rides and add them ro Rides property
            if (openRides.ToList().Count > 0)
            {
                var openRidesGroup = new RidesGroup("Åbne ture");
                openRides.ToList().ForEach(x => openRidesGroup.Add(x));
                Rides.Add(openRidesGroup);
            }

            // Create group for archived rides and add them ro Rides property
            if (archivedRides.ToList().Count > 0)
            {
                var archivedRidesGroup = new RidesGroup("Arkiverede ture");
                archivedRides.ToList().ForEach(x => archivedRidesGroup.Add(x));
                Rides.Add(archivedRidesGroup);
            }
        }

        #region PageLifeCycleAware
        /// <summary>
        /// On appearing handler.
        /// (Re)loads the rides.
        /// </summary>
        public override void OnAppearing()
        {
            base.OnAppearing();

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
        public DelegateCommand UpdateListCommand => _updateListCommand ?? (_updateListCommand = new DelegateCommand(UpdateListCommandExecute));

        private void UpdateListCommandExecute()
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