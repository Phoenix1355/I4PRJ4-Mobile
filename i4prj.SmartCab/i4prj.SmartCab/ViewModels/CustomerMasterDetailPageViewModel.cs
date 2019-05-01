using System;
using System.Diagnostics;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.Views;
using Prism.Behaviors;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Prism.Services;
using Xamarin.Forms;

namespace i4prj.SmartCab.ViewModels
{
    public class CustomerMasterDetailPageViewModel : RestrictedAccessViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:i4prj.SmartCab.ViewModels.CustomerMasterDetailPageViewModel"/> class. Dependencies auto injected.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="dialogService">Dialog service.</param>
        public CustomerMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISessionService sessionService)
            : base(navigationService, dialogService, sessionService)
        {
            Title = "Customer Master Detail Page"; 
            Customer = sessionService.Customer;
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
            SessionService.Clear();

            await NavigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(LoginPage));
        }


        #endregion
    }
}
