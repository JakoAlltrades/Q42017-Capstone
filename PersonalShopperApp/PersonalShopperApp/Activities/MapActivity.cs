using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.OS;
using Android.Gms.Maps.Model;
using Android;
using Java.Interop;
using PersonalShopperApp.Models;
using Newtonsoft.Json;

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "MapActivity")]
    public class MapActivity : Activity, IOnMapReadyCallback
    {
        private static readonly LatLng InMaui = new LatLng(20.72110, -156.44776);
        private GoogleMap _map;
        private MapFragment _mapFragment;
        CameraPosition cameraPosition;
        Order curOrder;
        [Export("OnMapReady")]
        public void OnMapReady(GoogleMap map)
        {
            _map = map;
            if (_map != null)
            {
                _map.MapType = GoogleMap.MapTypeNormal;
                _map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BaseMaps);
            ActionBar.Hide();
            if (Intent.HasExtra("curOrder"))
            {
                var customerSerialized = Intent.GetStringExtra("curOrder");
                curOrder = JsonConvert.DeserializeObject<Order>(customerSerialized);
            }
            // Create your application here
            _mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;
            if (_mapFragment == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                _mapFragment = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, _mapFragment, "map");
                fragTx.Commit();
            }
            MapsInitializer.Initialize(this);
            _mapFragment.GetMapAsync(this);
            LatLng location = new LatLng(50.897778, 3.013333);
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(18);
            builder.Bearing(155);
            builder.Tilt(65);
            cameraPosition = builder.Build();


            if (_map == null)
            {
                //_map.MyLocationEnabled = true;


                if (_map != null)
                {
                    _map.MapType = GoogleMap.MapTypeNormal;
                    _map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
                }
            }



        }

        
        [Export("ArrivedAtStore")]
        public void ArrivedAtStore(View view)
        {
            Intent atStore = new Intent(this, typeof(ShopperFillOrderActivity));
            string orderJson = JsonConvert.SerializeObject(curOrder);
            atStore.PutExtra("curOrder", orderJson);
            StartActivity(atStore);
        }

        [Export("ArrivedAtDelivery")]
        public void ArrivedAtDelivery(View view)
        {
            Intent atDelv = new Intent(this, typeof(DeliverOrderActivity));
            string orderJson = JsonConvert.SerializeObject(curOrder);
            atDelv.PutExtra("curOrder", orderJson);
            StartActivity(atDelv);
        }
    }
}