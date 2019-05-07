using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

namespace i4prj.SmartCab.CustomControls
{
    //Heavy inspiration from https://xamarinhelp.com/xamarin-forms-maps/
    public class BindableMap : Map
    {
        public static readonly BindableProperty MapPinsProperty = BindableProperty.Create(
                 nameof(Pins),
                 typeof(ObservableCollection<Pin>),
                 typeof(BindableMap),
                 new ObservableCollection<Pin>(),
                 propertyChanged: (map, oldValue, newValue) =>
                 {
                     var bindable = (BindableMap)map;
                     bindable.Pins.Clear();

                     var collection = (ObservableCollection<Pin>)newValue;
                     foreach (var item in collection)
                         bindable.Pins.Add(item);

                     collection.CollectionChanged += (sender, e) =>
                     {
                         Device.BeginInvokeOnMainThread(() =>
                         {
                             switch (e.Action)
                             {
                                 case NotifyCollectionChangedAction.Add:
                                     if (e.OldItems != null)
                                         foreach (var item in e.OldItems)
                                             bindable.Pins.Remove((Pin)item);
                                     if (e.NewItems != null)
                                         foreach (var item in e.NewItems)
                                             bindable.Pins.Add((Pin)item);
                                     break;
                             }
                         });
                     };
                 });

        public List<Pin> MapPins { get; set; }

        public static readonly BindableProperty MapPositionProperty = BindableProperty.Create(
                 nameof(MapPosition),
                 typeof(Position),
                 typeof(BindableMap),
                 new Position(0,0),
                 propertyChanged: (map,oldValue, newValue) =>
                 {
                     ((BindableMap)map).MoveToRegion(MapSpan.FromCenterAndRadius(
                         (Position)newValue,
                         Distance.FromKilometers((double)((BindableMap)map).GetValue(RadiusProperty))));
                 }, defaultBindingMode:(BindingMode.TwoWay));


        public Position MapPosition { get; set; }

        public static readonly BindableProperty RadiusProperty = BindableProperty.Create(
            nameof(Radius),
            typeof(double),
            typeof(BindableMap),
            propertyChanged: (map, oldValue, newValue) => { ((BindableMap) map).Radius = (double) newValue; });

        public double Radius { get; set; }
    }
}
