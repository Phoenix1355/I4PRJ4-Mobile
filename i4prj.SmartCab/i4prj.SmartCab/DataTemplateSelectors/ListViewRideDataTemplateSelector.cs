using System;
using i4prj.SmartCab.Models;
using Xamarin.Forms;

namespace i4prj.SmartCab.DataTemplateSelectors
{
    /// <summary>
    /// Data template selector class for determining the item data template
    /// when displaying rides in a list view
    /// </summary>
    public class ListViewRideDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Bindable property to define the data template in XAML
        /// </summary>
        /// <value>The open template.</value>
        public DataTemplate OpenTemplate { get; set; }

        /// <summary>
        /// Bindable property to define the data template in XAML
        /// </summary>
        /// <value>The archived template.</value>
        public DataTemplate ArchivedTemplate { get; set; }

        /// <summary>
        /// Select the data template based on the status of the ride.
        /// A ride is open in the following states:
        /// (LookingForMatch || Debited || WaitingForAccept || Accepted) state and not passed it's deadline
        /// A ride is archived in the following states:
        /// (Expired) state or passed it's deadline
        /// </summary>
        /// <returns>A data template.</returns>
        /// <param name="item">Item (Ride).</param>
        /// <param name="container">Container.</param>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var ride = (Ride)item;
            return (ride.Status == Ride.RideStatus.Expired || ride.ConfirmationDeadline < DateTime.Now) ? ArchivedTemplate : OpenTemplate;
        }
    }
}
