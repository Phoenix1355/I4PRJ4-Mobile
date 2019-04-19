using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    public class CreateRideViewModel : ViewModelBase
    {
        private IBackendApiService _backendApiService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRideViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="pageDialogService">Page dialog service.</param>
        public CreateRideViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IBackendApiService backEndApiService)
            : base(navigationService, pageDialogService)
        {
            Title = "Opret tur";
            Request = new CreateRideRequest();
            _backendApiService = backEndApiService;
            Price = "Beregn min pris";

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

        #endregion

        #region Commands

        private DelegateCommand _calculatePriceCommand;
        /// <summary>
        /// Submits the CalculatePriceRequest
        /// </summary>
        public DelegateCommand CalculatePriceCommand => _calculatePriceCommand ??
                                                        (_calculatePriceCommand =
                                                            new DelegateCommand(CalculatePriceCommandExecuteAsync));
        private async void CalculatePriceCommandExecuteAsync()
        {
            CalculatePriceRequest request = new CalculatePriceRequest(Request);

            IsBusy = true;
            PriceResponse response = await _backendApiService.SubmitCalculatePriceRequest(request);
            IsBusy = false;


            if (response == null)
            {
                await DialogService.DisplayAlertAsync("Forbindelse",
                    "Pris kunne ikke beregnes - ingen internetforbindelse", "OK");
            }
            else if (response.WasUnsuccessfull())
            {
                await DialogService.DisplayAlertAsync("Ukendt fejl", "Prisen for turen kunne ikke beregnes", "OK");
            }
            else if(response.WasSuccessfull())
            {
                Price = response.Body.Price;
            }
        }
        

        private DelegateCommand _createRideCommand;
        /// <summary>
        /// Submits the CreateRideRequest
        /// </summary>
        public DelegateCommand CreateRideCommand => _createRideCommand ?? (_createRideCommand = new DelegateCommand(CreateRideCommandExecuteAsync));

        private async void CreateRideCommandExecuteAsync()
        {

            IsBusy = true;
            CreateRideResponse response = await _backendApiService.SubmitCreateRideRequest(Request);
            IsBusy = false;

            //Debug.WriteLine(response.HttpResponseMessage.StatusCode);

            if (response == null)
            {
                await DialogService.DisplayAlertAsync("Forbindelse", "Du har ikke forbindelse til internettet", "OK");
            }
            else if (response.WasUnsuccessfull())
            {
                await DialogService.DisplayAlertAsync("Fejl", response.Body.errors.First().Value[0], "OK");
            }
            else if (response.WasSuccessfull())
            {
                await DialogService.DisplayAlertAsync("Success", "Turen er blevet oprettet \nAt betale: "+response.Body.price+" kr.", "OK");
                await NavigationService.GoBackAsync();
            }
        }

        #endregion

    }
}
