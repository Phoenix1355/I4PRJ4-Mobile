using Prism;
using Prism.Ioc;
using i4prj.SmartCab.ViewModels;
using i4prj.SmartCab.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Services;

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

            Debug.WriteLine($"App::OnInitialized Token: {LocalSessionService.Instance.Token}");

            if (LocalSessionService.Instance.Token != null)
            {
                await NavigationService.NavigateAsync(nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
            }
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

            containerRegistry.RegisterForNavigation<CustomerMasterDetailPage, CustomerMasterDetailPageViewModel>();
        }

        protected override void OnStart()
        {
            Debug.WriteLine("OnStart");
        }
        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep");
        }
        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
        }
    }
}
