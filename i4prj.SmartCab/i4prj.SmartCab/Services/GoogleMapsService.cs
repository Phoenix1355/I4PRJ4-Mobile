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
        /// <summary>
        /// Gets the positon(longitude,latidude) of the address specified by the string
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>

        private IXamarinEssentials _geocoding;

        public GoogleMapsService(IXamarinEssentials geocoding)
        {
            _geocoding = geocoding;
        }

        public async Task<Location> GetPosition(string address)
        {
            try
            {

                var locations = await _geocoding.GetGeocodingResult(address);

                var location = locations?.FirstOrDefault();
             
                
                return location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        

        /// <summary>
        /// Gets the position in the middle of two locations
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public Position GetMiddlePosition(Location from, Location to)
        {
            return new Position((from.Latitude + to.Latitude) / 2, (from.Longitude + to.Longitude) / 2);
        }

        /// <summary>
        /// Gets the appropriate radius for the map, according to two locations and a specified margin.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="margin">The margin.</param>
        /// <returns></returns>
        public double GetMapRadius(Location from, Location to, double margin)
        {
            Position middlePosition = GetMiddlePosition(from, to);

            return _geocoding.CalculateDistanceResult(new Location(middlePosition.Latitude, middlePosition.Longitude), to,
                margin);
        }
    }
}
