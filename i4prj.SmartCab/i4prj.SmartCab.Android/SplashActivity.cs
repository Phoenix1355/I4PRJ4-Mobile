using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Android.Views;
using Prism;
using Prism.Ioc;

namespace i4prj.SmartCab.Droid
{
    /// <summary>
    /// Splash activity shows splash screen and proceeds to MainActivity.
    /// </summary>
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();

            // Copy intent from splash screen to MainActivity
            // so received Push Notifications are passed on to MainActivity
            // From: https://docs.microsoft.com/en-us/appcenter/sdk/push/xamarin-forms
            var intent = new Intent(Application.Context, typeof(MainActivity));
            if (Intent.Extras != null) intent.PutExtras(Intent.Extras);

            StartActivity(intent);
        }
    }
}

