using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    /// <summary>
    /// DateTime converter.
    /// </summary>
    public class DateTimeConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified (DateTime) value to a string with
        /// formatting as provided by parameter.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";

            if (value is DateTime datetime)
            {
                var format = parameter as string;
                if (!string.IsNullOrEmpty(format))
                {
                    result = datetime.ToString(format);
                }
                else
                {
                    result = datetime.ToString();
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
