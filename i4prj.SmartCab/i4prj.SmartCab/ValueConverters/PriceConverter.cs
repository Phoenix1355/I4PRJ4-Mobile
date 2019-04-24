using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Format("{0:C2} DKK", value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
