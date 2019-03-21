using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using i4prj.SmartCab.Models;
using Xamarin.Forms;
using i4prj.SmartCab.Views;

namespace i4prj.SmartCab.ViewModels
{
    public class RidesViewModel : ViewModelBase
    {
        public RidesViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Turoversigt";
        }

        private DelegateCommand _logOutCommand;
        public DelegateCommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new DelegateCommand(LogOutCommandExecuted));

        private async void LogOutCommandExecuted()
        {
            await DialogService.DisplayAlertAsync("Log ud", "Du bliver nu logget ud", "OK");
            Session.Clear();

            await NavigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(Login));
        }
    }
}
