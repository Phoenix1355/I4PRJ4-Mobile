using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SmartCab_MobilApp_POC.Notifications;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

namespace SmartCab_MobilApp_POC
{
    public partial class MainPage : ContentPage
    {
        private int _notificationsCount = 0;

        private string webApiUrl = "https://smartcabpocwebapi.azurewebsites.net/api/rides/";

        public MainPage()
        {
            InitializeComponent();

            if (!AppCenter.Configured)
            {
                Microsoft.AppCenter.Push.Push.PushNotificationReceived += (sender, e) =>
                {
                    lblTitle.Text = e.Title;
                    lblBody.Text = e.Message;
                };
            }
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Clicked!";

            string myJson = "{\"departureTime\": \"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\"}";
            Debug.WriteLine(myJson);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    webApiUrl,
                    new StringContent(myJson, Encoding.UTF8, "application/json")
                );
            }
        }

        private async void NotificationButton_OnClicked(object sender, EventArgs e)
        {
            _notificationsCount++;

            ((Button)sender).Text = $"Clicked ({_notificationsCount})!";

            Notification notification = new Notification($"Notification #{_notificationsCount}", "Din taxa er på vej!", "Dan-Taxa har accepteret din tur og ankommer på afhentningsstedet kl. 18:00.");
            await notification.NotifyMySelf();

        }
    }
}
