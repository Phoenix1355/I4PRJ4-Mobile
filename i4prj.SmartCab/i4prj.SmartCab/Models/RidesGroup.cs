using System;
using System.Collections.ObjectModel;
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.Models
{
    /// <summary>
    /// A titled group of rides
    /// </summary>
    public class RidesGroup : ObservableCollection<IRide>
    {
        public string Title { get; set; }

        public RidesGroup(string title)
        {
            Title = title;
        }

    }
}
