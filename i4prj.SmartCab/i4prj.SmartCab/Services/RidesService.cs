using System;
using System.Collections.Generic;
using System.Linq;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;

namespace i4prj.SmartCab.Services
{
    public class RidesService : IRidesService
    {
        public IEnumerable<IRide> CreateRidesFromApiResponse(IEnumerable<IApiResponseRide> items)
        {
            List<IRide> result = new List<IRide>();

            foreach (var item in items)
            {
                result.Add(new Ride(item));
            }

            return result;
        }

        public IEnumerable<IRide> GetArchivedRides(IEnumerable<IRide> rides)
        {
            return rides.Where(r => r.IsArchived());
        }

        public IEnumerable<IRide> GetOpenRides(IEnumerable<IRide> rides)
        {
            return rides.Where(r => r.IsOpen());
        }
    }
}
