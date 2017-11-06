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
using Android.Gms.Maps;

namespace PersonalShopperApplication.Resources.layout
{
    [Activity(Label = "MapsActivity")]
    public class MapsActivity : Activity, IOnMapReadyCallback
    {
        public void OnMapReady(GoogleMap googleMap)
        {
            throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
    }
}