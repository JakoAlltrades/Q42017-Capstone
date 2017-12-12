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
using PersonalShopperApp.Models;
using Java.Interop;
using Newtonsoft.Json;

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "RecieveOrdersActivity")]
    public class RecieveOrdersActivity : Activity
    {
        List<Order> curPlacedOrders = new List<Order>();
        Order example = new Order();
        Shopper curShopper;
        //ShopperActionsDB SADB;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            // Create your application here
            SetContentView(Resource.Layout.ReceiveOrder);
            example.customerID = 1;
            example.deliveryAddress = new Address("350 s 600 e", "Salt Lake City", "UT", 84102, 406);
            example.orderID = 1;
            example.shopperID = 2;
            example.storeAddress = new Address("455 s 500 e", "Salt Lake City", "UT", 84102);
            example.placedOrder.Add(new OrderItem("eggs", 1.99, 0));
            example.placedOrder.Add(new OrderItem("1 lb. ground beef", 5.99, 0));
            example.placedOrder.Add(new OrderItem("sausage", 4.99, 0));
            curPlacedOrders.Add(example);

            ListView curOrders = FindViewById(Resource.Id.waitingOrders) as ListView;
            List<String> orders = new List<string>();
            for (int j = 0; j < curPlacedOrders.Count; j++)
            {
                orders.Add(curPlacedOrders.ElementAt(j)._id);

            }
            curOrders.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, orders));
            curOrders.ItemClick += (sender, e) =>
            {
                ListView lv = sender as ListView;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (e.Position == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                SetContentView(Resource.Layout.TakeOrderFromWaitingList);
                TextView estCost = FindViewById<TextView>(Resource.Id.estCost);
                TextView storeView = FindViewById<TextView>(Resource.Id.storeAddressView);
                TextView delvView = FindViewById<TextView>(Resource.Id.delvAddressView);
                Order curO = curPlacedOrders.ElementAt(e.Position);
                estCost.Text = "Estimated Cost: " + curO.EstimateCost();
                storeView.Text = "Store Address: " + curO.storeAddress.ToString();
                delvView.Text = "Delivery Address: " + curO.deliveryAddress.ToString();
            };
        }

        [Export("AcceptOrder")]
        public void AcceptOrder(View view)
        {
            //var geoUri = Android.Net.Uri.Parse("geo:42.37,-71.12");
            //var mapIntent = new Intent(Intent.ActionView, geoUri);
            Intent directions = new Intent(this, typeof(MapActivity));
            string orderJson = JsonConvert.SerializeObject(example);
            directions.PutExtra("curOrder", orderJson);
            StartActivity(directions);
        }

        [Export("DeclineOrder")]
        public void DeclineOrder(View view)
        {
            SetContentView(Resource.Layout.ReceiveOrder);
            example.customerID = 1;
            example.deliveryAddress = new Address("350 s 600 e", "Salt Lake City", "UT", 84102, 406);
            example.orderID = 1;
            example.shopperID = 2;
            example.storeAddress = new Address("455 s 500 e", "Salt Lake City", "UT", 84102);
            example.placedOrder.Add(new OrderItem("eggs", 1.99, 0));
            example.placedOrder.Add(new OrderItem("1 lb. ground beef", 5.99, 0));
            example.placedOrder.Add(new OrderItem("sausage", 4.99, 0));
            curPlacedOrders.Add(example);

            ListView curOrders = FindViewById(Resource.Id.waitingOrders) as ListView;
            List<String> orders = new List<string>();
            for (int j = 0; j < curPlacedOrders.Count; j++)
            {
                orders.Add(curPlacedOrders.ElementAt(j)._id);

            }
            curOrders.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, orders));
            curOrders.ItemClick += (sender, e) =>
            {
                ListView lv = sender as ListView;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (e.Position == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                SetContentView(Resource.Layout.TakeOrderFromWaitingList);
                TextView estCost = FindViewById<TextView>(Resource.Id.estCost);
                TextView storeView = FindViewById<TextView>(Resource.Id.storeAddressView);
                TextView delvView = FindViewById<TextView>(Resource.Id.delvAddressView);
                Order curO = curPlacedOrders.ElementAt(e.Position);
                estCost.Text = "Estimated Cost: " + curO.EstimateCost();
                storeView.Text = "Store Address: " + curO.storeAddress.ToString();
                delvView.Text = "Delivery Address: " + curO.deliveryAddress.ToString();
            };
        }

        
    }


}