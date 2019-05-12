using System;
using System.Collections.Generic;
using System.Text;
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.Services
{
    public class TimeService : ITimeService
    {

        private readonly int _maxHours = 23;
        private readonly int _maxMinutes = 59;
        /// <summary>
        /// Gets the current date.
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetCurrentTime()
        {
            return DateTime.Now.TimeOfDay;
        }

        /// <summary>
        /// Adds two TimeSpans. If it overflows, at the overflowed TimeSpan is returned
        /// </summary>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <param name="overflow">if set to <c>true</c> [overflow].</param>
        /// <returns></returns>
        public TimeSpan AddTimeSpans(TimeSpan t1, TimeSpan t2, ref bool overflow)
        {
            overflow = false;

            if (_maxHours < (t1.Hours + t2.Hours + 1))
            {
                if (t1.Hours + t2.Hours == _maxHours)
                {
                    if (t1.Minutes + t2.Minutes > _maxMinutes)
                    {
                        overflow = true;
                        return new TimeSpan(0,(t1.Minutes+t2.Minutes-(_maxMinutes+1)),0);
                    }
                    else
                    {
                        return t1.Add(t2);
                    }
                }
                else
                {
                    overflow = true;

                    int hours = ((t1.Hours + t2.Hours) - (_maxHours + 1));
                    int minutes;

                    if (t1.Minutes + t2.Minutes > _maxMinutes)
                    {
                        hours++;
                        minutes = (t1.Minutes + t2.Minutes) - (_maxMinutes + 1);
                    }
                    else
                    {
                        minutes = t1.Minutes + t2.Minutes;
                    }
                   
                    return new TimeSpan(hours,minutes,0);
                }
            }

            return t1.Add(t2);
        }
        
    }
}
