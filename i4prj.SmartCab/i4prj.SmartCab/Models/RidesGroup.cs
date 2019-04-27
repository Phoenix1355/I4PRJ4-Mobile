using System;
using System.Collections.ObjectModel;

namespace i4prj.SmartCab.Models
{
    /// <summary>
    /// A titled group of rides
    /// </summary>
    public class RidesGroup : ObservableCollection<Ride>
    {
        public string Title { get; set; }

        public RidesGroup(string title)
        {
            Title = title;
        }

    }
}
