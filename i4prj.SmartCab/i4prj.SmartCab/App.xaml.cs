using Prism;
using Prism.Ioc;
using i4prj.SmartCab.ViewModels;
using i4prj.SmartCab.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.Interfaces;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Prism.Navigation;
using i4prj.SmartCab.Responses;
using System.Net.Http;
using System.Net;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace i4prj.SmartCab
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var sessionService = new LocalSessionService();
            //sessionService.Clear();
            
            Debug.WriteLine($"App::OnInitialized Token: {sessionService.Token}");

            // Check expiration if token is available
            if (sessionService.Token != null)
            {
                var unixExpiration = JWTService.GetPayloadValue(sessionService.Token, "exp");

                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(int.Parse(unixExpiration));
                DateTime loginExpirationDate = dateTimeOffset.LocalDateTime;

                // Login has expired
                if (loginExpirationDate < DateTime.Now)
                {
                    sessionService.Clear();

                    await NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(LoginPage));
                }
                // Login still valid
                else
                {
                    await NavigationService.NavigateAsync(nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
                }
            }
            // No token available
            else 
            {
                await NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(LoginPage));
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CreateCustomerPage, CreateCustomerViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<RidesPage, RidesViewModel>();
            containerRegistry.RegisterForNavigation<CreateRidePage,CreateRideViewModel>();
            containerRegistry.RegisterForNavigation<EditAccountPage,EditAccountViewModel>();
            containerRegistry.RegisterForNavigation<MapsPage,MapsViewModel>();

            containerRegistry.RegisterForNavigation<CustomerMasterDetailPage, CustomerMasterDetailPageViewModel>();

            // Dependency injection setup
            containerRegistry.Register<ISessionService, LocalSessionService>();
            containerRegistry.Register<IBackendApiService, AzureApiService>();
            containerRegistry.Register<IRidesService, RidesService>();
        }

        protected override void OnStart()
        {
            SetupPush();


            Debug.WriteLine("OnStart");

            AppCenter.Start("9707acc9-f699-4165-9346-9378d040c10f", typeof(Push));
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
        }

        /// <summary>
        /// Sets up the eventhandler which is invoked when a notification is
        /// received
        /// </summary>
        private void SetupPush()
        {
            if (!AppCenter.Configured)
            {
                Push.PushNotificationReceived += (sender, e) =>
                {
                    DebugWriteReceivedPushNotification(e);
                    HandleReceivedPushNotification(e);

                };
            }
        }

        private void DebugWriteReceivedPushNotification(PushNotificationReceivedEventArgs e)
        {
            // Add the notification message and title to the message
            var summary = $"Push notification received:" +
                            $"\n\tNotification title: {e.Title}" +
                            $"\n\tMessage: {e.Message}";

            // If there is custom data associated with the notification,
            // print the entries
            if (e.CustomData != null)
            {
                summary += "\n\tCustom data:\n";
                foreach (var key in e.CustomData.Keys)
                {
                    summary += $"\t\t{key} : {e.CustomData[key]}\n";
                }
            }

            // Send the notification summary to debug output
            Debug.WriteLine(summary);
        }

        private void HandleReceivedPushNotification(PushNotificationReceivedEventArgs e)
        {
            // Pass on custom data from push notification
            // to the view model of next view
            var navigationParams = new NavigationParameters();
            if (e.CustomData != null)
            {
                foreach (var item in e.CustomData)
                {
                    navigationParams.Add(item.Key, item.Value);
                }
            }
            Console.WriteLine("Navigating to: " + "/" + nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
            NavigationService.NavigateAsync("/" + nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage), navigationParams);
        }
    }
}
