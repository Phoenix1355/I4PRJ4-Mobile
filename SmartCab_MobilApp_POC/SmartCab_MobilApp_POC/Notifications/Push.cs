using System;
using Newtonsoft.Json;

namespace SmartCab_MobilApp_POC.Notifications
{
    [JsonObject]
    public class Push
    {
        [JsonProperty("notification_target")]
        public Target Target { get; set; }

        [JsonProperty("notification_content")]
        public Content Content { get; set; }
    }
}
