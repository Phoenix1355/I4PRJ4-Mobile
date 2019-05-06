using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                try
                {
                    var parts = token.Split('.');

                    if (parts.Length == 3)
                    {
                        var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(FromBase64String(parts[0]));

                        result = FindValueByKey(deserialized, key);
                    }
                }
                catch (FormatException)
                {
                    // Silent
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
                try
                {
                    var parts = token.Split('.');

                    if (parts.Length == 3)
                    {
                        var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(FromBase64String(parts[1]));

                        result = FindValueByKey(deserialized, key);
                    }
                } 
                catch (FormatException)
                {
                    // Silent
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

        private static string FromBase64String(string value)
        {
            Debug.WriteLine(AddPadding(value));
            var temp = System.Convert.FromBase64String(AddPadding(value));
            return Encoding.UTF8.GetString(temp, 0, temp.Length);
        }

        private static string AddPadding(string value)
        {
            // Padd ending with = until length is multiple of 4
            for (var i = 0; i < (value.Length % 4); i++)
            {
                value += "=";
            }

            return value;
        }
    }
}
