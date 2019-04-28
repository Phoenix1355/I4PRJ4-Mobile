using System;
using System.Globalization;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    /// <summary>
    /// Index to alternate background color converter.
    /// </summary>
    public class IndexToAlternateBackgroundColorConverter : IValueConverter
    {
        /// <summary>
        /// Convert the specified index value to an alternating background color
        /// of transparent or light gray.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Transparent;

            if (value is int index)
            {
                if (index % 2 == 0) color = Color.FromHex("#eeeeee");
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}