using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Android.Locations;

namespace PersonalShopperApp.Models
{
    public partial class MapPage : ContentPage
    {
        LocationManager locationmanager;
        public MapPage(Position destination, Order orderTaken, LocationManager lm)
        {
            locationmanager = lm;
            Location l = locationmanager.GetLastKnownLocation(LocationManager.GpsProvider);
            Position curPosition = new Position(l.Latitude, l.Longitude);
            TK.CustomMap.TKCustomMap kCustomMap = new TK.CustomMap.TKCustomMap(new MapSpan(curPosition, 20, 20));

            kCustomMap.MoveToRegion(MapSpan.FromCenterAndRadius(curPosition, Distance.FromMiles(1.0)));
        }
    }
}