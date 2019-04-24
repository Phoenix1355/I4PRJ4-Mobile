using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    public class DateTimeConverter : IValueConverter
    {
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
