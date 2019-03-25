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
    public class CustomerMasterDetailPageViewModel : ViewModelBase
    {
        public CustomerMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Customer Master Detail Page";
        }

        #region Commands

        private DelegateCommand _logoutCommand;
        public DelegateCommand LogOutCommand => _logoutCommand ?? (_logoutCommand = new DelegateCommand(LogOutCommandExecute));

        private async void LogOutCommandExecute()
        {
            LocalSessionService.Instance.Clear();

            await NavigationService.NavigateAsync("/" + nameof(Login));
        }

        #endregion
    }
}
