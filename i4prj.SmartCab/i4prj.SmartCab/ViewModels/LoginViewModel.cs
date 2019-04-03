using System;
using System.Diagnostics;
using i4prj.SmartCab.Interfaces;
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
        private IBackendApiService _backendApiService;
        private ISessionService _sessionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.ViewModels.LoginViewModel"/> class. Dependencies auto injected.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="dialogService">Dialog service.</param>
        /// <param name="backendApiService">Backend Api service.</param>
        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogService, IBackendApiService backendApiService, ISessionService sessionService)
            : base(navigationService, dialogService)
        {
            Title = "Log ind";

            Request = new LoginRequest();

            _backendApiService = backendApiService;
            _sessionService = sessionService;
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
        /// <summary>
        /// Submits the LoginRequest.
        /// </summary>
        public DelegateCommand SubmitRequestCommand => _submitRequest ?? (_submitRequest = new DelegateCommand(SubmitRequestCommandExecuteAsync));

        private async void SubmitRequestCommandExecuteAsync()
        {
            IsBusy = true;
            LoginResponse response = await _backendApiService.SubmitLoginRequestRequest(Request);
            IsBusy = false;

            if (response == null)
            {
                await DialogService.DisplayAlertAsync("Fejl", "Ingen forbindelse til internettet", "OK");
            }
            else if (response.WasUnsuccessfull())
            {
                await DialogService.DisplayAlertAsync("Ukendt fejl", "Du kunne ikke logges ind", "OK");
            }
            else
            {
                _sessionService.Update(response.Body.token, new Customer(response.Body.customer));

                await NavigationService.NavigateAsync("/" + nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
            }
        }

        #endregion
    }
}
