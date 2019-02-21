using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartCab_MobilApp_POC.Notifications
{
    [JsonObject]
    public class Content
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("custom_data")]
        public IDictionary<string, string> Payload { get; set; }
    }
}
