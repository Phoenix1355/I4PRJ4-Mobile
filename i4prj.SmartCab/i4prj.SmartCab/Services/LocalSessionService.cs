using System;
using System.Diagnostics;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Newtonsoft.Json;
using Prism;
using Xamarin.Forms;

namespace i4prj.SmartCab.Services
{
    public class LocalSessionService : ISessionService
    {
        #region Singleton
        private static readonly LocalSessionService instance = new LocalSessionService();

        static LocalSessionService()
        {
        }

        private LocalSessionService()
        {
        }

        public static LocalSessionService Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion Singleton

        private string _tokenKeyName = "token";
        private string _customerKeyName = "customer";

        #region Properties

        public string Token
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

            private set
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

        public ICustomer Customer
        {
            get
            {
                if (PrismApplicationBase.Current.Properties.ContainsKey(_customerKeyName))
                {
                    ICustomer data = JsonConvert.DeserializeObject<Customer>(PrismApplicationBase.Current.Properties[_customerKeyName].ToString());
                    return data;
                }
                return null;
            }

            private set
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

        #endregion 

        public void Update(string token, ICustomer customer)
        {
            Token = token;
            Customer = customer;

            Save();
        }

        public void Clear()
        {
            PrismApplicationBase.Current.Properties.Remove(_tokenKeyName);
            PrismApplicationBase.Current.Properties.Remove(_customerKeyName);

            Save();
        }

        private void Save()
        {
            PrismApplicationBase.Current.SavePropertiesAsync();
        }
    }
}