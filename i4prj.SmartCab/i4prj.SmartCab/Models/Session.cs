using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Prism;
using Xamarin.Forms;

namespace i4prj.SmartCab.Models
{
    public static class Session
    {
        private static string _tokenKeyName = "token";
        private static string _customerKeyName = "customer";

        public static string Token
        {
            get
            {
                if (PrismApplicationBase.Current.Properties.ContainsKey(_tokenKeyName))
                {
                    string data = PrismApplicationBase.Current.Properties[_tokenKeyName] as string;
                    return data;
                }
                return null;
            }

            set
            {
                if (PrismApplicationBase.Current.Properties.ContainsKey(_tokenKeyName)) {
                    PrismApplicationBase.Current.Properties[_tokenKeyName] = value;
                    Debug.WriteLine($"Key {_tokenKeyName} altered with data {value}.");
                }
                else
                {
                    PrismApplicationBase.Current.Properties.Add(_tokenKeyName, value);
                    Debug.WriteLine($"New key {_tokenKeyName} added with data {value}.");
                }
            }
        }

        public static Customer Customer
        {
            get
            {
                if (PrismApplicationBase.Current.Properties.ContainsKey(_customerKeyName))
                {
                    Customer data = JsonConvert.DeserializeObject<Customer>(PrismApplicationBase.Current.Properties[_customerKeyName].ToString());
                    return data;
                }
                return null;
            }

            set
            {
                if (PrismApplicationBase.Current.Properties.ContainsKey(_customerKeyName))
                {
                    PrismApplicationBase.Current.Properties[_customerKeyName] = JsonConvert.SerializeObject(value);
                    Debug.WriteLine($"Key {_customerKeyName} altered with data {JsonConvert.SerializeObject(value)}.");
                }
                else
                {
                    PrismApplicationBase.Current.Properties.Add(_customerKeyName, JsonConvert.SerializeObject(value));
                    Debug.WriteLine($"New key {_customerKeyName} added with data {JsonConvert.SerializeObject(value)}.");
                }
            }
        }

        public static void Clear()
        {
            PrismApplicationBase.Current.Properties.Remove(_tokenKeyName);
            PrismApplicationBase.Current.Properties.Remove(_customerKeyName);
        }

        public static void Save()
        {
            PrismApplicationBase.Current.SavePropertiesAsync();
        }
    }
}



