using System;
using System.Collections.ObjectModel;

namespace i4prj.SmartCab.Models
{
    /// <summary>
    /// A titled group of rides
    /// </summary>
    public class RideGroup : ObservableCollection<Ride>
    {
        public string Title { get; set; }

        public RideGroup(string title)
        {
            Title = title;
        }

    }
}
