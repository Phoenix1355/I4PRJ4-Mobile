using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Services
{
    /// <summary>
    /// JWT Service.
    /// </summary>
    /// <remarks>
    /// Header formatting: { "alg":"HS256","typ":"JWT"}
    /// Payload formatting: { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress":"demo@demo.dk","UserId":"f855f339-9cb8-408f-a9bb-28a6cdac129d","http://schemas.microsoft.com/ws/2008/06/identity/claims/role":"Customer","http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration":"05/01/2019 11:54:54","exp":1556711694}
    /// </remarks>
    public class JWTService
    {
        /// <summary>
        /// Get the header value defined by key in token.
        /// </summary>
        /// <returns>The header value.</returns>
        /// <param name="token">Token.</param>
        /// <param name="key">Key.</param>
        public static string GetHeaderValue(string token, string key)
        {
            string result = null;

            if (!string.IsNullOrEmpty(token))
            {
                var parts = token.Split('.');

                if (parts.Length == 3)
                {
                    var headerJson = System.Convert.FromBase64String(parts[0]);

                    string headerJsonConverted = Encoding.UTF8.GetString(headerJson, 0, headerJson.Length);

                    var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(headerJsonConverted);

                    result = FindValueByKey(deserialized, key);
                }
            }

            return result;
        }

        /// <summary>
        /// Get the payload value defined by key in token.
        /// </summary>
        /// <returns>The payload value.</returns>
        /// <param name="token">Token.</param>
        /// <param name="key">Key.</param>
        public static string GetPayloadValue(string token, string key)
        {
            string result = null;

            if (!string.IsNullOrEmpty(token))
            {
                var parts = token.Split('.');

                if (parts.Length == 3)
                {
                    var payloadJson = System.Convert.FromBase64String(parts[1]);

                    string payloadJsonConverted = Encoding.UTF8.GetString(payloadJson, 0, payloadJson.Length);

                    var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(payloadJsonConverted);

                    result = FindValueByKey(deserialized, key);
                }
            }

            return result;
        }

        private static string FindValueByKey(Dictionary<string, string> set, string key)
        {
            string result = null;

            foreach (var kvp in set)
            {
                if (kvp.Key.Equals(key))
                {
                    result = kvp.Value;
                    break;
                }
            }

            return result;
        }
    }
}
