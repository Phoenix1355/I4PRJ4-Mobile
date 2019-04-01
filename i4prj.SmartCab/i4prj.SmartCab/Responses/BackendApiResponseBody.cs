using System;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;

namespace i4prj.SmartCab.Responses
{
    /// <summary>
    /// Base class from which all http response body from IBackendApiService is derived.
    /// </summary>
    public abstract class BackendApiResponseBody
    {
        public string title { get; set; }

        public int status { get; set; }

        public Dictionary<string, IList<string>> errors { get; set; }

    }
}
