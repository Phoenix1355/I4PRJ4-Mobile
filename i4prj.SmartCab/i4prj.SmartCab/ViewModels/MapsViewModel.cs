using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using i4prj.SmartCab.Interfaces;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms.Maps;

namespace i4prj.SmartCab.ViewModels
{
    public class MapsViewModel : ViewModelBase
    { 
        public MapsViewModel(INavigationService navigationService, IPageDialogService dialogService, ISessionService sessionService) : base(navigationService, dialogService, sessionService)
        {
            Locations = new List<Location>();
            Location b = new Location();
            b.Address = "Aarhus V 8210 Bispehavevej 3";
            b.Description="Mit hjem";
            b.Position = new Position(100,100);
            Locations.Add(b);
        }

        private List<Location> Locations { get; set; }

        public class Location
        {
            public string Address { get; set; }
            public string Description { get; set; }
            public Position Position { get; set; }
        }

          
    }
}
