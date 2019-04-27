using System;
using System.Collections.Generic;
using System.Text;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace i4prj.SmartCab.ViewModels
{
    public class EditAccountViewModel : ViewModelBase
    {
        private IBackendApiService _apiService;
        private ISessionService _sessionService;
        public EditAccountViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
            IBackendApiService apiService, ISessionService sessionService) : base(navigationService, pageDialogService)
        {
            _apiService = apiService;
            _sessionService = sessionService;
            Request = new EditAccountRequest();

            Request.Email = _sessionService.Customer.Email;
            Request.Name = _sessionService.Customer.Name;
            Request.PhoneNumber = _sessionService.Customer.PhoneNumber;
        }

        #region Properties

        private IEditAccountRequest _request;

        public IEditAccountRequest Request
        {
            get { return _request; }
            set { SetProperty(ref _request, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _editAccountCommand;

        public DelegateCommand EditAccountCommand => _editAccountCommand ??
                                                     (_editAccountCommand =
                                                         new DelegateCommand(EditAccountCommandExecuteAsync));

        private async void EditAccountCommandExecuteAsync()
        {
            IsBusy = true;
            EditAccountResponse response = await _apiService.SubmitEditAccountRequest(Request);
            IsBusy = false;

            if (response == null)
            {
                await DialogService.DisplayAlertAsync("Forbindelse", "Du har ikke forbindelse til internettet", "OK");
            }
            else if (response.WasUnsuccessfull())
            {
                await DialogService.DisplayAlertAsync("Ukendt fejl", "Ændringerne kunne ikke registreres", "OK");
            }
            else if (response.WasSuccessfull())
            {
                _sessionService.Update(_sessionService.Token,new Customer(response.Body.customer));
                
                await DialogService.DisplayAlertAsync("Success", "Ændringer godkendt", "OK");
                await NavigationService.NavigateAsync("/"+ nameof(CustomerMasterDetailPage) +"/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
            }

        }

        #endregion
    }
}
