using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace i4prj.SmartCab.ViewModels
{
    public class CreateRideViewModel : RestrictedAccessViewModelBase
    {
        private IBackendApiService _backendApiService;
        public ITimeService _timeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRideViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="pageDialogService">Page dialog service.</param>
        public CreateRideViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISessionService sessionService, IBackendApiService backendApiService)
            : base(navigationService, pageDialogService, sessionService)
        {
            Title = "Opret tur";
            Request = new CreateRideRequest(new TimeService());
            _backendApiService = backendApiService;
            Price = "Opret tur";
            RideInfo=new NavigationParameters();
            _timeService = new TimeService();
        }

        #region Properties

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

        public INavigationParameters RideInfo { get; set; }

        private bool _priceCalculationSucceeded;
        
        public bool PriceCalculationSucceeded
        {
            get { return _priceCalculationSucceeded; }
            set { SetProperty(ref _priceCalculationSucceeded, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _checkDepartureTimeCommand;
        /// <summary>
        /// Checks the departuretime in the request, and shows a dialog, if it is not valid
        /// </summary>
        public DelegateCommand CheckDepartureTimeCommand => _checkDepartureTimeCommand ??
                                                        (_checkDepartureTimeCommand =
                                                            new DelegateCommand(CheckDepartureTimeCommandExecuteAsync));

        private async void CheckDepartureTimeCommandExecuteAsync()
        {
            if(Request.DepartureDate.Date==_timeService.GetCurrentDate().Date)
            {
                if (Request.DepartureTime < _timeService.GetCurrentTime())
                {
                    Request.DepartureTime = _timeService.GetCurrentTime();
                    await DialogService.DisplayAlertAsync("Fejl", "Du kan ikke vælge tidspunkt tidligere end det nuværende klokkeslet", "Ok");
                }
            }
            if (Request.DepartureDate.Date == Request.ConfirmationDeadlineDate.Date)
            {
                if (Request.ConfirmationDeadlineTime > Request.DepartureTime)
                {
                    Request.DepartureTime = Request.ConfirmationDeadlineTime;
                    await DialogService.DisplayAlertAsync("Fejl", "Svartiden kan ikke være senere end afgangstiden", "Ok");
                }
            }
        }


        private DelegateCommand _checkConfirmationDeadlineTimeCommand;
        /// <summary>
        /// Checks the confirmationdeadline time in the request, and shows a dialog, if it is not valid
        /// </summary>
        public DelegateCommand CheckConfirmationDeadlineTimeCommand => _checkConfirmationDeadlineTimeCommand ??
                                                            (_checkConfirmationDeadlineTimeCommand =
                                                                new DelegateCommand(CheckConfirmationDeadlineTimeCommandExecuteAsync));

        private async void CheckConfirmationDeadlineTimeCommandExecuteAsync()
        {
            if (Request.ConfirmationDeadlineDate.Date == _timeService.GetCurrentDate().Date)
            {
                if (Request.ConfirmationDeadlineTime < _timeService.GetCurrentTime())
                {
                    Request.ConfirmationDeadlineTime = _timeService.GetCurrentTime();
                    await DialogService.DisplayAlertAsync("Fejl", "Du kan ikke vælge tidspunkt tidligere end det nuværende klokkeslet", "Ok");
                }
            }
            if (Request.DepartureDate.Date == Request.ConfirmationDeadlineDate.Date)
            {
                if (Request.ConfirmationDeadlineTime > Request.DepartureTime)
                {
                    Request.ConfirmationDeadlineTime = Request.DepartureTime;
                    await DialogService.DisplayAlertAsync("Fejl", "Svartiden kan ikke være senere end afgangstiden", "Ok");
                }
            }
        }


        private DelegateCommand _calculatePriceCommand;
        /// <summary>
        /// Submits the CalculatePriceRequest
        /// </summary>
        public DelegateCommand CalculatePriceCommand => _calculatePriceCommand ??
                                                        (_calculatePriceCommand =
                                                            new DelegateCommand(CalculatePriceCommandExecuteAsync));
        private async void CalculatePriceCommandExecuteAsync()
        {
            if (!Request.IsValid)
            {
                return;
            }

            CalculatePriceRequest request = new CalculatePriceRequest(Request);

            IsBusy = true;
            PriceResponse response = await _backendApiService.SubmitCalculatePriceRequest(request);
            IsBusy = false;


            if (response == null)
            {
                Price = "Opret tur: Pris kunne ikke beregnes";
                RideInfo = new NavigationParameters();
                PriceCalculationSucceeded = false;
                await DialogService.DisplayAlertAsync("Forbindelse",
                    "Pris kunne ikke beregnes - ingen internetforbindelse", "OK");
            }
            else if (response.WasUnsuccessfull())
            {
                Price = "Opret tur: Pris kunne ikke beregnes";
                RideInfo = new NavigationParameters();
                PriceCalculationSucceeded = false;
                await DialogService.DisplayAlertAsync("Ukendt fejl", "Prisen for turen kunne ikke beregnes", "OK");
            }
            else if(response.WasSuccessfull())
            {
                Price = "Opret tur: " + response.Body.price + " kr. ";
                
                RideInfo = new NavigationParameters();

                PriceCalculationSucceeded = true;
                RideInfo.Add("Price",response.Body.price);
                RideInfo.Add("Ride",Request);
            }
        }
        

        private DelegateCommand _createRideCommand;
        /// <summary>
        /// Navigates to the MapsPage
        /// </summary>
        public DelegateCommand CreateRideCommand => _createRideCommand ?? (_createRideCommand = new DelegateCommand(CreateRideCommandExecuteAsync));

        private async void CreateRideCommandExecuteAsync()
        {
            await NavigationService.NavigateAsync(nameof(RideConfirmationPage), RideInfo);
           
        }

        #endregion

    }
}
