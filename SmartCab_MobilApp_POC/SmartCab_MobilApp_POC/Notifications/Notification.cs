using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Newtonsoft.Json;

namespace SmartCab_MobilApp_POC.Notifications
{
    public class Notification
    {
        private readonly Push _push;

        public Notification(string name, string title, string body, IDictionary<string, string> payload = null)
        {
            _push = new Push
            {
                Content = new Content
                {
                    Name = name,
                    Title = title,
                    Body = body,
                    Payload = payload
                },
                Target = new Target
                {
                    Type = Constants.DeviceTarget
                }
            };
        }

        public async Task NotifyMySelf()
        {
            // Get own device ID/install ID
            System.Guid? installId = await AppCenter.GetInstallIdAsync();

            Debug.WriteLine("InstallID: " + installId);

            _push.Target.Devices = new List<string>
            {
                installId.ToString()
            };

            await PostAsync();
        }

        public async Task Notify(IUser user)
        {
            if (user.IOSDevices.Count > 0)
            {
                Debug.WriteLine($"Pushed to {user.IOSDevices.Count} iOS devices");
                _push.Target.Devices = user.IOSDevices;
            }

            if (user.AndroidDevices.Count > 0)
            {
                Debug.WriteLine($"Pushed to {user.AndroidDevices.Count} iOS devices");
                _push.Target.Devices = user.AndroidDevices;
            }

            await PostAsync();
        }

        public async Task Notify(IEnumerable<IUser> users)
        {
            foreach (var user in users)
            {
                await Notify(user);
            }
        }

        private async Task PostAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(Constants.ApiKeyName, Constants.ApiKey);

                var response = await client.PostAsync(
                    $"{Constants.Url}{Constants.Organization}/{Constants.Android}/{Constants.Apis.Notification}",
                    new StringContent(JsonConvert.SerializeObject(_push), Encoding.UTF8, "application/json")
                );

                Debug.WriteLine("Push request content: " + JsonConvert.SerializeObject(_push));
            }
        }
    }
}
