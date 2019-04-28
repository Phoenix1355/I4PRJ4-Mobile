using System;
using System.Globalization;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    /// <summary>
    /// Ride time remaining to string converter.
    /// </summary>
    public class RideTimeRemainingToStringConverter : IValueConverter
    {
        /// <summary>
        /// Convert the specified (TimeSpan) value to a textual countdown 
        /// representation
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "Udløber om ";
            if (value is TimeSpan timeSpan)
            {
                if (timeSpan.TotalMinutes > 60)
                {
                    result += string.Format("{0:0} t {1:0} m {2:0} s", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                }
                else if (timeSpan.TotalSeconds > 60)
                {
                    result += string.Format("{0:0} m {1:0} s", timeSpan.Minutes, timeSpan.Seconds);
                }
                else if (timeSpan.TotalSeconds > 0)
                {
                    result += string.Format("{0:0} s", timeSpan.Seconds);
                }
                else
                {
                    result = "Udløbet";
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