using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace i4prj.SmartCab.Interfaces
{
    public interface IMapsService
    {
        Task<List<Location>> GetPosition(List<string> addresses);

        List<string> ConvertRequestToAddresses(ICreateRideRequest request);
    }
}
