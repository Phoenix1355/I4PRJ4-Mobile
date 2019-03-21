using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace i4prj.SmartCab.ViewModels
{
    public class CreateCustomerViewModel : ViewModelBase
    {
        private IPageDialogService _dialogService;

        public CreateCustomerViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _dialogService = dialogService;

            Title = "Opret konto";

            Request = new CreateCustomerRequest();

            IsBusy = false;
        }

        #region Properties

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public bool IsReady
        {
            get { return !_isBusy; }
            set { SetProperty(ref _isBusy, !value); }
        }

        private CreateCustomerRequest _request;
        public CreateCustomerRequest Request
        {
            get { return _request; }
            set { SetProperty(ref _request, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _submitRequest;
        public DelegateCommand SubmitRequestCommand => _submitRequest ?? (_submitRequest = new DelegateCommand(OnSubmitRequestCommandExecutedAsync));

        private async void OnSubmitRequestCommandExecutedAsync()
        {
            BackendApiService service = new BackendApiService();

            IsBusy = true;
            BackendApiResponse response = await service.SubmitCreateCustomerRequest(Request);
            IsBusy = false;

            if (response == null)
            {
                await _dialogService.DisplayAlertAsync("Fejl", "Ingen forbindelse til internettet", "OK");
            }
            else
            {
                if (response.WasSuccessfull())
                {
                    await _dialogService.DisplayAlertAsync("Hey", "All good", "OK");
                    Debug.WriteLine(response.HttpResponseMessage.ToString());
                }
                else if (response.WasUnsuccessfull())
                {
                    await _dialogService.DisplayAlertAsync("Hey", "Fejl i request", "OK");
                    string responseBodyAsText = await response.HttpResponseMessage.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseBodyAsText);
                }
                else if (response.WasUnauthorized())
                {
                    await _dialogService.DisplayAlertAsync("Hey", "Du er ikke logget ind", "OK");
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Hey", "Et response du ikke har taget højde for: " + response.HttpResponseMessage.StatusCode.ToString(), "OK");
                }
            }
            Debug.WriteLine("End of OnSubmitRequestCommandExecutedAsync");
        }

        #endregion
    }
}
