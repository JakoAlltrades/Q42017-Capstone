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
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "PrevDeliveriesActivity")]
    public class PrevDeliveriesActivity : Activity
    {
        List<Order> curOrders = new List<Order>();
        Shopper curShopper;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            ActionBar.Hide();
            SetContentView(Resource.Layout.PreviousDeliveries);
            if (Intent.HasExtra("curShopper"))
            {
                var customerSerialized = Intent.GetStringExtra("curShopper");
                curShopper = JsonConvert.DeserializeObject<Shopper>(customerSerialized);
            }
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format("https://azuresqlconnection20180123112406.azurewebsites.net/api/CompletedOrder/GetPastDeliveries?shopperId=" + curShopper.shopperID));
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = await client.GetAsync(uri);
            var message = response.Content.ReadAsStringAsync().Result.Replace("\\", "");
            var jsonList = JsonConvert.DeserializeObject<List<SQLOrder>>(message);
            for (int j = 0; j < jsonList.Count; j++)
            {
                OrderLists lists = null;
                Address delAddress = null, storeAddress = null;
                delAddress = SQLSerializer.DeserializeAddress(jsonList.ElementAt(j).deliveryAddress);
                storeAddress = SQLSerializer.DeserializeAddress(jsonList.ElementAt(j).storeAddress);
                lists = SQLSerializer.DeserializeLists(jsonList.ElementAt(j).Lists);
                curOrders.Add(new Order(jsonList.ElementAt(j).orderID, jsonList.ElementAt(j).customerID, jsonList.ElementAt(j).shopperID, delAddress, storeAddress, lists));
            }
            ListView prevOrders = FindViewById<ListView>(Resource.Id.prevDeliveries);

            //List<Order> pastOrders = SADB.GetCompletedOrders(curShopper);
            List<string> pastOrderDates = new List<string>();
            for (int j = 0; j < curOrders.Count; j++)
            {
                pastOrderDates.Add(curOrders.ElementAt(j)._id);

            }
            prevOrders.SetAdapter(new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, pastOrderDates));

        }
    }
}