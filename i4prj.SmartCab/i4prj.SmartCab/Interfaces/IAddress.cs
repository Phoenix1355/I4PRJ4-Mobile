using System;
using System.Collections.Generic;
using System.Text;

namespace i4prj.SmartCab.Interfaces
{
    /// <summary>
    /// Interface for IAddress
    /// </summary>
    public interface IAddress
    {
        string CityName { get;set; }
        int PostalCode { get; set; }
        string StreetName { get; set; }
        int StreetNumber { get; set; }
    }
}
