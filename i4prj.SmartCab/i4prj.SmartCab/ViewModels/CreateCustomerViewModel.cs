using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.Views;
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
        private IBackendApiService _backendApiService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.ViewModels.CreateCustomerViewModel"/> class. Dependencies auto injected.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="dialogService">Dialog service.</param>
        /// /// <param name="backendApiService">Backend Api Service.</param>
        public CreateCustomerViewModel(INavigationService navigationService, IPageDialogService dialogService, IBackendApiService backendApiService, ISessionService sessionService)
            : base(navigationService, dialogService, sessionService)
        {
            Title = "Opret bruger";
            Request = new CreateCustomerRequest();

            _backendApiService = backendApiService;
        }

        #region Properties

        private CreateCustomerRequest _request;
        public CreateCustomerRequest Request
        {
            get { return _request; }
            set { SetProperty(ref _request, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _submitRequest;

        /// <summary>
        /// Submits the CreateCustomerRequest. (Lazy-creation)
        /// </summary>
        public DelegateCommand SubmitRequestCommand => _submitRequest ?? (_submitRequest = new DelegateCommand(SubmitRequestCommandExecuteAsync));

        private async void SubmitRequestCommandExecuteAsync()
        {
            IsBusy = true;
            CreateCustomerResponse response = await _backendApiService.SubmitCreateCustomerRequest(Request);
            IsBusy = false;

            if (response == null)
            {
                await DialogService.DisplayAlertAsync("Forbindelse", "Du har ikke forbindelse til internettet", "OK");
            }
            else if (response.WasUnsuccessfull())
            {
                await DialogService.DisplayAlertAsync("Ukendt fejl", "Din bruger kunne ikke oprettes", "OK");
            }
            else
            {
                if (response.Body != null) SessionService.Update(response.Body.token, new Customer(response.Body.customer));

                await NavigationService.NavigateAsync("/" + nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
            }
        }

        #endregion
    }
}
