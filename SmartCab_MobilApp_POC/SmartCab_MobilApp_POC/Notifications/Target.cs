using System;
using System.Collections;
using Newtonsoft.Json;

namespace SmartCab_MobilApp_POC.Notifications
{
    [JsonObject]
    public class Target
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("devices")]
        public IEnumerable Devices { get; set; }
    }
}
