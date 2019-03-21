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
        public CreateCustomerViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Opret bruger";

            Request = new CreateCustomerRequest();
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
        public DelegateCommand SubmitRequestCommand => _submitRequest ?? (_submitRequest = new DelegateCommand(OnSubmitRequestCommandExecutedAsync));

        private async void OnSubmitRequestCommandExecutedAsync()
        {
            BackendApiService service = new BackendApiService();

            IsBusy = true;
            CreateCustomerResponse response = await service.SubmitCreateCustomerRequest(Request);
            IsBusy = false;

            if (response == null)
            {
                await DialogService.DisplayAlertAsync("Fejl", "Ingen forbindelse til internettet", "OK");
            }
            else
            {
                if (response.WasSuccessfull())
                {
                    Session.Token = response.Body.token;

                    await NavigationService.NavigateAsync("/" + nameof(Rides));
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
