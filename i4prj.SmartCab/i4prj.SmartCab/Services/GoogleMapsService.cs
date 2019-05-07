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
    }
}
