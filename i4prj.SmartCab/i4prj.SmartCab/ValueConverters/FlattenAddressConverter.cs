using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    public class FlattenAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string flatAddress = "";

            if (parameter is String prefix)
            {
                flatAddress = prefix + " ";
            }

            if (value is IAddress address)
            {
                flatAddress += $"{address.StreetName} {address.StreetNumber}, {address.PostalCode} {address.CityName}";
            }

            return flatAddress;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
