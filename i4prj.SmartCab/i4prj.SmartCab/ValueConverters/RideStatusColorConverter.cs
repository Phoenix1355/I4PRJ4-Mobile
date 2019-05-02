using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    /// <summary>
    /// Ride status color converter.
    /// </summary>
    public class RideStatusColorConverter : IValueConverter
    {
        /// <summary>
        /// Convert the specified (RideStatus) value to a specific color.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color result = Color.Gray;

            if (value is Ride.RideStatus status)
            {
                switch (status)
                {
                    case Ride.RideStatus.WaitingForAccept:
                    case Ride.RideStatus.LookingForMatch:
                        result = Color.FromRgba(25, 90, 136, 40);
                        break;

                    case Ride.RideStatus.Debited:
                    case Ride.RideStatus.Accepted:
                        result = Color.FromRgba(43, 136, 25, 40);
                        break;

                    case Ride.RideStatus.Expired:
                        result = Color.Red;
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