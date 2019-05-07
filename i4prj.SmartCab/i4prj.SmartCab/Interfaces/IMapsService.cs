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
        Task<Location> GetPosition(string addresses);
    }
}
