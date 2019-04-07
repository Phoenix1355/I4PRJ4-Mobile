﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    public class PriceResponse : BackendApiResponse
    {

        public string Body { get; set; }

        public PriceResponse(HttpResponseMessage response)
            :base(response)
        {

        }

        protected override async void MakeBody()
        {
            string responseBodyAsText = await HttpResponseMessage.Content.ReadAsStringAsync();
            try
            {
                Body = JsonConvert.DeserializeObject<string>(responseBodyAsText);
                Debug.WriteLine("Http-result parset uden fejl.");
                Debug.Write(responseBodyAsText);
            }
            catch (Newtonsoft.Json.JsonSerializationException e)
            {
                Debug.WriteLine("Http-result kunne parses som json. Fejl: " + e.Message);
            }
        }


    }
}