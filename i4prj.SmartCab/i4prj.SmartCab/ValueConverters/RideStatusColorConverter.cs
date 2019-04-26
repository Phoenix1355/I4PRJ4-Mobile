using System;
using System.Globalization;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Xamarin.Forms;

namespace i4prj.SmartCab.ValueConverters
{
    public class RideStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color result = Color.Gray;

            if (value is Ride.RideStatus status)
            {
                switch (status)
                {
                    case Ride.RideStatus.WaitingForAccept:
                    case Ride.RideStatus.LookingForMatch:
                    case Ride.RideStatus.Debited:
                        result = Color.FromRgba(25, 90, 136, 40);
                        break;
                    
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