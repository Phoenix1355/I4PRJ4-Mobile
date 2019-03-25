using System;
using System.Diagnostics;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace i4prj.SmartCab.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Log ind";

            Request = new LoginRequest();
        }

        #region Properties

        private LoginRequest _request;
        public LoginRequest Request
        {
            get { return _request; }
            set { SetProperty(ref _request, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _submitRequest;
        public DelegateCommand SubmitRequestCommand => _submitRequest ?? (_submitRequest = new DelegateCommand(SubmitRequestCommandExecuteAsync));

        private async void SubmitRequestCommandExecuteAsync()
        {
            AzureApiService api = new AzureApiService();

            IsBusy = true;
            LoginResponse response = await api.SubmitLoginRequestRequest(Request);
            IsBusy = false;

            if (response == null)
            {
                await DialogService.DisplayAlertAsync("Fejl", "Ingen forbindelse til internettet", "OK");
            }
            else
            {
                if (response.WasSuccessfull())
                {
                    LocalSessionService.Instance.Update(response.Body.token, new Customer(response.Body.customer));

                    await NavigationService.NavigateAsync("/" + nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
                }
                else if (response.WasUnsuccessfull())
                {
                    string errorMessage = "Fejl i request.\n";

                    string error = response.GetFirstError();
                    if (error != null) errorMessage += error;

                    await DialogService.DisplayAlertAsync("Fejl", errorMessage, "OK");

                }
            }
        }

        #endregion
    }
}
