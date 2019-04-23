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
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.ViewModels
{
    public class RidesViewModel : ViewModelBase
    {
        private IBackendApiService _backendApiService;

        public RidesViewModel(INavigationService navigationService, IPageDialogService dialogService, IBackendApiService backendApiService) : base(navigationService, dialogService)
        {
            Title = "Turoversigt";

            _backendApiService = backendApiService;
        }

        /*private DelegateCommand _getRidesCommand;
        public DelegateCommand GetRidesCommand => _getRidesCommand ?? (_getRidesCommand = new DelegateCommand(GetRidesCommandExecute));

        private async void GetRidesCommandExecute()
        {
            AzureApiService api = new AzureApiService();

            var response = await api.GetRides();

            string responseBodyAsText = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("GET RESPONSE");
            Debug.WriteLine(responseBodyAsText);
        }*/
    }
}
