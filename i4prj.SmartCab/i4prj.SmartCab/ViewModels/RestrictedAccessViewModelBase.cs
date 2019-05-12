using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections;
using Prism.AppModel;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Services;
using System.Diagnostics;
using i4prj.SmartCab.Views;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace i4prj.SmartCab.ViewModels
{
    public abstract class RestrictedAccessViewModelBase : ViewModelBase
    {
        protected RestrictedAccessViewModelBase(INavigationService navigationService, IPageDialogService dialogService, ISessionService sessionService)
            : base(navigationService, dialogService, sessionService)
        {
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            _ = AutoLogout();

            Debug.WriteLine("RestrictedAccessViewModelBase::OnAppearing");
           
        }

        private async Task AutoLogout()
        {
            if (SessionService.Token != null)
            {
                var unixExpiration = JWTService.GetPayloadValue(SessionService.Token, "exp");

                Debug.WriteLine("Unix expiration: " + unixExpiration);

                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(int.Parse(unixExpiration));

                DateTime loginExpirationDate = dateTimeOffset.LocalDateTime;

                /*
                if (loginExpirationDate < DateTime.Now)
                {
                    SessionService.Clear();

                    await DialogService.DisplayAlertAsync("Udløbet log ind", "Dit log ind er udløbet. Du vil blive vist til log ind siden.", "OK");

                    Debug.WriteLine("Login token expired at " + loginExpirationDate.ToString());
                    Debug.WriteLine("Automatically redirected to login page.");

                    await NavigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(LoginPage));
                }
                */
            }
        }
    }
}
