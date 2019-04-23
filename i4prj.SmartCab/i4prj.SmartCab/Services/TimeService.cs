using System;
using System.Collections.Generic;
using System.Text;
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.Services
{
    public class TimeService : ITimeService
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        public TimeSpan GetCurrentTime()
        {
            return DateTime.Now.TimeOfDay;
        }
        
    }
}
