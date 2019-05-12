using System;
using System.Collections.Generic;
using System.Text;

namespace i4prj.SmartCab.Interfaces
{
    public interface ITimeService
    {
        DateTime GetCurrentDate();
        TimeSpan GetCurrentTime();

        TimeSpan AddTimeSpans(TimeSpan t1, TimeSpan t2, ref bool overflow);


    }
}
