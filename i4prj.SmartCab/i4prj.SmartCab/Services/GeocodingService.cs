using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using Xamarin.Essentials;

namespace i4prj.SmartCab.Services
{

    /// <summary>
    /// A class used to stub out the geocoding service from xamarin essentials.
    /// </summary>
    /// <seealso cref="i4prj.SmartCab.Interfaces.IXamarinEssentials" />
    public class GeocodingService : IXamarinEssentials
    {
        public async Task<IEnumerable<Location>> GetGeocodingResult(string address)
        {
            return await Geocoding.GetLocationsAsync(address);
        }

        public double CalculateDistanceResult(Location from, Location to, double margin)
        {
            return Xamarin.Essentials.Location.CalculateDistance(from, to,
                       DistanceUnits.Kilometers) + margin;
        }
    }
}
