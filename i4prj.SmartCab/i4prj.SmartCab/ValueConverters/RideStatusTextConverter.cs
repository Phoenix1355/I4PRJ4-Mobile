using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    /// <summary>
    /// Ride status text converter.
    /// </summary>
    public class RideStatusTextConverter : IValueConverter
    {
        /// <summary>
        /// Convert the specified (RideStatus) value to a textual representation
        /// of the status.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "";
            if (value is Ride.RideStatus status)
            {
                switch (status)
                {
                    case Ride.RideStatus.WaitingForAccept:
                    case Ride.RideStatus.LookingForMatch:
                    case Ride.RideStatus.Debited:
                        result = "Afventer bekræftelse";
                        break;
                    
                    case Ride.RideStatus.Accepted:
                        result = "Bekræftet";
                        break;

                    case Ride.RideStatus.Expired:
                        result = "Udløbet";
                        break;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}