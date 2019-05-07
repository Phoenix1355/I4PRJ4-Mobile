using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace i4prj.SmartCab.Services
{
    public class GoogleMapsService : IMapsService
    {

        public async Task<Location> GetPosition(string address)
        {
            try
            {
             
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
             
                
                return location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public Position GetMiddlePosition(Location from, Location to)
        {
            return new Position((from.Latitude + to.Latitude) / 2, (from.Longitude + to.Longitude) / 2);
        }

        public double GetMapRadius(Location from, Location to, double margin)
        {
            Position middlePosition = GetMiddlePosition(from, to);

            return Xamarin.Essentials.Location.CalculateDistance(new Location(middlePosition.Latitude, middlePosition.Longitude), from, DistanceUnits.Kilometers) + margin;
        }
    }
}
