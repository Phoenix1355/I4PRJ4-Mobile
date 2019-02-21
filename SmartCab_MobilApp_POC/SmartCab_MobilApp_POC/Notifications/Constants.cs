using System;
namespace SmartCab_MobilApp_POC.Notifications
{
    public class Constants
    {
        //https://api.appcenter.ms/v0.1/apps/frank.andersen-gmail.com/SmartCabPoc/push/notifications

        public const string Url = "https://api.appcenter.ms/v0.1/apps/";
        public const string ApiKeyName = "X-API-Token";
        public const string ApiKey = "bc967a133c849e47a340c4cbb4c294cbc4d751c2";
        public const string Organization = "frank.andersen-gmail.com";
        public const string Android = "SmartCabPoc";
        public const string IOS = "{Your iOS App Name}";
        public const string DeviceTarget = "devices_target";
        public class Apis { public const string Notification = "push/notifications"; }
    }
}
