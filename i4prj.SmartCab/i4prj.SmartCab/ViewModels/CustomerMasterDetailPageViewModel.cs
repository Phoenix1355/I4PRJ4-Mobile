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
    public class CustomerMasterDetailPageViewModel : ViewModelBase
    {
        private ISessionService _sessionService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:i4prj.SmartCab.ViewModels.CustomerMasterDetailPageViewModel"/> class. Dependencies auto injected.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="dialogService">Dialog service.</param>
        public CustomerMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISessionService sessionService)
            : base(navigationService, dialogService)
        {
            Title = "Customer Master Detail Page"; 
            Customer = sessionService.Customer;

            _sessionService = sessionService;
        }

        public ICustomer Customer { get; private set; }

        #region Commands

        private DelegateCommand _logoutCommand;
        /// <summary>
        /// Logs out the current Customer.
        /// </summary>
        public DelegateCommand LogOutCommand => _logoutCommand ?? (_logoutCommand = new DelegateCommand(LogOutCommandExecute));

        private async void LogOutCommandExecute()
        {
            _sessionService.Clear();

            await NavigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(LoginPage));
        }

        #endregion
    }
}
