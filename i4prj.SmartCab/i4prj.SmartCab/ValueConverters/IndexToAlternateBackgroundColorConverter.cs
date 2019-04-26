using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    public class RideStatusToAcceptedBoolConverter : IValueConverter
    {
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