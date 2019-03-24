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
using i4prj.SmartCab.Services;
using System.Diagnostics;

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
            LocalSessionService.Instance.Clear();

            await NavigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(Login));
        }

        // Bare lavet som test til Møller
        private DelegateCommand _getRidesCommand;
        public DelegateCommand GetRidesCommand => _getRidesCommand ?? (_getRidesCommand = new DelegateCommand(GetRidesCommandExecute));

        private async void GetRidesCommandExecute()
        {
            AzureApiService api = new AzureApiService();

            var response = await api.GetRides();

            string responseBodyAsText = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("GET RESPONSE");
            Debug.WriteLine(responseBodyAsText);
        }
    }
}
