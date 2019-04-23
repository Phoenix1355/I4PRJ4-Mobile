using System;
using System.Collections.Generic;
using System.Text;

namespace i4prj.SmartCab.Interfaces
{
    public interface ITimeService
    {
        DateTime GetCurrentDate();
        TimeSpan GetCurrentTime();
    }
}
