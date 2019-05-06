using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace i4prj.SmartCab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsPage : ContentPage
    {
        public MapsPage()
        {
            InitializeComponent();

            MapsViewModel viewModel = (MapsViewModel)BindingContext;

            IEnumerable b = viewModel.Locations;
            
            //Mangler at udtrække locations her og sætte mapregion til det rigtige.

            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(10, 10), Distance.FromKilometers(100)));
        }
    }
}