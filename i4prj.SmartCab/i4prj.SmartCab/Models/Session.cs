using System;
using Xamarin.Forms;

namespace i4prj.SmartCab.Models
{
    public static class Session
    {
        private static string _keyName = "token";
        public static string Token
        {
            get
            {
                if (Application.Current.Properties.ContainsKey(_keyName))
                {
                    string token = Application.Current.Properties[_keyName] as string;
                    return token;
                }
                return null;
            }

            set {
                Prism.PrismApplicationBase.Current.Properties.Add(_keyName, value);
                Prism.PrismApplicationBase.Current.SavePropertiesAsync();
            }
        }

        public static void Clear()
        {
            Prism.PrismApplicationBase.Current.Properties.Remove(_keyName);
        }
    }
}



