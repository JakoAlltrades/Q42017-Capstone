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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "RecieveOrdersActivity")]
    public class RecieveOrdersActivity : Activity
    {
        List<Order> curPlacedOrders = new List<Order>();
        Shopper curShopper;
        Order curOrder;
        //ShopperActionsDB SADB;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            // Create your application here
            SetContentView(Resource.Layout.ReceiveOrder);
            curOrder = null;
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format("https://azuresqlconnection20180123112406.azurewebsites.net/api/Shopper/GatherCurrentOrders"));
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = await client.GetAsync(uri);
            var message = response.Content.ReadAsStringAsync().Result.Replace("\\", "");
            var jsonList = JsonConvert.DeserializeObject<List<SQLOrder>>(message);
            for(int j = 0; j < jsonList.Count; j++)
            {
                BinaryFormatter bf1 = new BinaryFormatter(), bf2 = new BinaryFormatter(), bf3 = new BinaryFormatter();
                MemoryStream ms1, ms2, ms3;
                OrderLists lists = null;
                Address delAddress = null, storeAddress = null;
                try
                {
                    ms1 = new MemoryStream(jsonList.ElementAt(j).deliveryAddress);
                    delAddress = (Address)bf1.Deserialize(ms1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                try
                {

                    ms2 = new MemoryStream(jsonList.ElementAt(j).storeAddress);
                    storeAddress = (Address)bf2.Deserialize(ms2);

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    ms3 = new MemoryStream(jsonList.ElementAt(j).Lists);
                    OrderLists listObject = (OrderLists)bf3.Deserialize(ms3);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                curPlacedOrders.Add(new Order(jsonList.ElementAt(j).orderID, jsonList.ElementAt(j).customerID, jsonList.ElementAt(j).shopperID, delAddress, storeAddress, lists));
            }
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
                curOrder = curO;
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
            string orderJson = JsonConvert.SerializeObject(curOrder);
            directions.PutExtra("curOrder", orderJson);
            StartActivity(directions);
        }

        [Export("DeclineOrder")]
        public void DeclineOrder(View view)
        {
            SetContentView(Resource.Layout.ReceiveOrder);

            curOrder = null;

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
                curOrder = curO;
                estCost.Text = "Estimated Cost: " + curO.EstimateCost();
                storeView.Text = "Store Address: " + curO.storeAddress.ToString();
                delvView.Text = "Delivery Address: " + curO.deliveryAddress.ToString();
            };
        }

        
    }


}