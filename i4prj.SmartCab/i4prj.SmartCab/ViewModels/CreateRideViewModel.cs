using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace i4prj.SmartCab.ViewModels
{
    public class CreateRideViewModel : ViewModelBase
    {
        public CreateRideViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Opret tur";
            Request = new CreateRideRequest();
            ApiService=new AzureApiService();
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

        private double _price;

        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        #endregion


        #region Commands

        private DelegateCommand _returnFromUserCommand;

        public DelegateCommand ReturnFromUserCommand => _returnFromUserCommand ??
                                                        (_returnFromUserCommand =
                                                            new DelegateCommand(ReturnFromUserCommandExecuteAsync));
        private async void ReturnFromUserCommandExecuteAsync()
        {

            Debug.WriteLine("Det lykkedes");

            Request.OriginAddress.CreateAddress(Request.Origin);
            Request.DestinationAddress.CreateAddress(Request.Destination);

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
                //For testing
                Price = 15;
            }
        }

        private DelegateCommand _createRideCommand;
        public DelegateCommand CreateRideCommand => _createRideCommand ?? (_createRideCommand = new DelegateCommand(CreateRideCommandExecuteAsync));

        private async void CreateRideCommandExecuteAsync()
        {
            Request.OriginAddress.CreateAddress(Request.Origin);
            Request.DestinationAddress.CreateAddress(Request.Destination);

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
                //måske skal der ske noget mere her?
                await DialogService.DisplayAlertAsync("Succes", "Turen er blevet oprettet!", "OK");
                await NavigationService.GoBackAsync();
            }
        }

        #endregion

    }
}
