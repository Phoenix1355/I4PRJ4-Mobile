using System;
using System.Collections.Generic;
using i4prj.SmartCab.Models;

namespace i4prj.SmartCab.Interfaces
{
    public interface IRidesService
    {
        /// <summary>
        /// Creates the rides from API response.
        /// </summary>
        /// <returns>The rides from API response.</returns>
        /// <param name="rides">Rides.</param>
        IEnumerable<IRide> CreateRidesFromDTO(IEnumerable<IRideDTO> rides);

        /// <summary>
        /// Gets the open rides.
        /// </summary>
        /// <returns>The open rides.</returns>
        /// <param name="rides">Rides.</param>
        IEnumerable<IRide> GetOpenRides(IEnumerable<IRide> rides);

        /// <summary>
        /// Gets the archived rides.
        /// </summary>
        /// <returns>The archived rides.</returns>
        /// <param name="rides">Rides.</param>
        IEnumerable<IRide> GetArchivedRides(IEnumerable<IRide> rides);
    }
}
