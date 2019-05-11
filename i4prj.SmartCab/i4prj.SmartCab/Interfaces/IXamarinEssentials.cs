using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace i4prj.SmartCab.Interfaces
{
    public interface IXamarinEssentials
    {
        Task<IEnumerable<Location>> GetGeocodingResult(string address);

        double CalculateDistanceResult(Location from, Location to, double margin);
    }
}
