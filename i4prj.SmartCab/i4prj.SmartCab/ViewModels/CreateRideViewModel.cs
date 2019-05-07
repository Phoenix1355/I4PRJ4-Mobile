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

            //TEST
            /*
            Request.OriginCityName = "Aarhus V";
            Request.OriginPostalCode = "8210";
            Request.OriginStreetName = "Bispehavevej";
            Request.OriginStreetNumber = "3";
            Request.DestinationCityName = "Aarhus C";
            Request.DestinationPostalCode = "8000";
            Request.DestinationStreetName = "Banegårdspladsen";
            Request.DestinationStreetNumber = "1";
            */
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

        private DelegateCommand _checkTimeCommand;
        /// <summary>
        /// Submits the CalculatePriceRequest
        /// </summary>
        public DelegateCommand CheckTimeCommand => _checkTimeCommand ??
                                                        (_checkTimeCommand =
                                                            new DelegateCommand(CheckTimeCommandExecuteAsync));

        private async void CheckTimeCommandExecuteAsync()
        {
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
        /// Submits the CreateRideRequest
        /// </summary>
        public DelegateCommand CreateRideCommand => _createRideCommand ?? (_createRideCommand = new DelegateCommand(CreateRideCommandExecuteAsync));

        private async void CreateRideCommandExecuteAsync()
        {
            await NavigationService.NavigateAsync(nameof(MapsPage), RideInfo);
           
        }

        #endregion

    }
}
