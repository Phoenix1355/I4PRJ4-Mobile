using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    /// <summary>
    /// Converts ride status to accepted bool converter.
    /// </summary>
    public class RideStatusToAcceptedBoolConverter : IValueConverter
    {
        /// <summary>
        /// Convert the specified (RideStatus) value to boolean of true value
        /// of RideStatus is accepted. Optional negation by parameter with value
        /// of "inverse".
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = false;
            if (value is Ride.RideStatus status)
            {
                if (status == Ride.RideStatus.Accepted) result = true;
            }

            if (parameter is string p)
            {
                if (p.Equals("inverse")) result = !result;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}