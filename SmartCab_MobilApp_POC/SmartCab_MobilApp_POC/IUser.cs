using System;
using System.Collections;
using System.Collections.Generic;

namespace SmartCab_MobilApp_POC
{
    public interface IUser
    {
        List<IDevice> AndroidDevices { get; set; }
        List<IDevice> IOSDevices { get; set; }
    }
}
