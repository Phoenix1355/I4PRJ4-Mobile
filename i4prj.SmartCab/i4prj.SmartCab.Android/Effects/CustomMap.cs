using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using i4prj.SmartCab.CustomControls;
using i4prj.SmartCab.Droid.Effects;
using Java.Util;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;


//https://docs.microsoft.com/da-dk/xamarin/xamarin-forms/app-fundamentals/custom-renderer/map/polyline-map-overlay

//[assembly: ExportRenderer(typeof(BindableMap), typeof(CustomMapRenderer))]
namespace i4prj.SmartCab.Droid.Effects
{

    public class CustomMapRenderer : MapRenderer
    {
        List<Position> _routeCoordinates;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var formsMap = (BindableMap)e.NewElement;
                //_routeCoordinates = formsMap.RouteCoordinates;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);

            var polylineOptions = new PolylineOptions();
            polylineOptions.InvokeColor(0x66FF0000);

            foreach (var position in _routeCoordinates)
            {
                polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
            }

            NativeMap.AddPolyline(polylineOptions);
        }
    }
}