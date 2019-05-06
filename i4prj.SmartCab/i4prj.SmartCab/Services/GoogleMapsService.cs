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
        public List<string> ConvertRequestToAddresses(ICreateRideRequest request)
        {
            List<string> addresses = new List<string>();

            addresses.Add($"{request.OriginStreetName} {request.OriginStreetNumber} {request.OriginCityName} {request.OriginPostalCode}");
            addresses.Add($"{request.DestinationStreetName} {request.DestinationStreetNumber} {request.DestinationCityName} {request.DestinationPostalCode}");

            return addresses;
        }

        public async Task<List<Location>> GetPosition(List<string> addresses)
        {
            List<Location> positions = new List<Location>();

            try
            {
                foreach (var address in addresses)
                {
                    var locations = await Geocoding.GetLocationsAsync(address);

                    var location = locations?.FirstOrDefault();

                    positions.Add(location);
                }
                
                return positions;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting positions");
            }

            return new List<Location>();
        }
    }
}
