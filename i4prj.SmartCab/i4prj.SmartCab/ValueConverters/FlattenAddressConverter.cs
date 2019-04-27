using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    /// <summary>
    /// Flatten address converter.
    /// </summary>
    public class FlattenAddressConverter : IValueConverter
    {
        /// <summary>
        /// Convert the specified (IAddress) value to a string with an optional
        /// prefix as provided by parameter.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
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
