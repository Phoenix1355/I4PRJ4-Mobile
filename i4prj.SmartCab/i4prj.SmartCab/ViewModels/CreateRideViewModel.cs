using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRideViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="pageDialogService">Page dialog service.</param>
        public CreateRideViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Opret tur";
            Request = new CreateRideRequest();
            ApiService=new AzureApiService();
            Price = "Beregn min pris";
        }

        #region Properties

        private CreateRideRequest _request;

        public CreateRideRequest Request
        {
            get { return _request; }
            set { SetProperty(ref _request, value); }
        }

        private IBackendApiService _apiService;

        public IBackendApiService ApiService
        {
            get { return _apiService; }
            set { SetProperty(ref _apiService, value); }
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
            CalculatePriceRequest request = new CalculatePriceRequest(Request.OriginAddress,Request.DestinationAddress);

            IsBusy = true;
            PriceResponse response = await ApiService.SubmitCalculatePriceRequest(request);
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
                Price = "Din pris: " + response.Body + " kr." + "\n (Tryk for at beregne igen)";
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
            CreateRideResponse response = await ApiService.SubmitCreateRideRequest(Request);
            IsBusy = false;

            Debug.WriteLine(response.HttpResponseMessage.StatusCode);

            if (response == null)
            {
                await DialogService.DisplayAlertAsync("Forbindelse", "Du har ikke forbindelse til internettet", "OK");
            }
            else if (response.WasUnsuccessfull())
            {
                await DialogService.DisplayAlertAsync("Ukendt fejl", "Turen kunne ikke oprettes", "OK");
            }
            else if (response.WasSuccessfull())
            {
                await DialogService.DisplayAlertAsync("Succes", "Turen er blevet oprettet \nAt betale: "+response.Body.price+" kr.", "OK");
                await NavigationService.GoBackAsync();
            }
        }

        #endregion

    }
}
