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
    [Activity(Label = "PrevOrderActivity")]
    public class PrevOrderActivity : Activity
    {
        private Customer curCustomer;
        private CustomerActionsDB CADB;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.PreviousOrders);
            ActionBar.Hide();
            if (Intent.HasExtra("curCustomer"))
            {
                var customerSerialized = Intent.GetStringExtra("curCustomer");
                curCustomer = JsonConvert.DeserializeObject<Customer>(customerSerialized);
            }

            ListView prevOrders = FindViewById<ListView>(Resource.Id.prevOrdersList);

            List<Order> pastOrders = new List<Order>();
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format("https://azuresqlconnection20180123112406.azurewebsites.net/api/CompletedOrder/GetPastOrders?userId=" + curCustomer.userID));
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
                pastOrders.Add(new Order(jsonList.ElementAt(j).orderID, jsonList.ElementAt(j).customerID, jsonList.ElementAt(j).shopperID, delAddress, storeAddress, lists));
            }

            List<string> pastOrderDates = new List<string>();
            for (int j = 0; j < pastOrders.Count; j++)
            {
                pastOrderDates.Add(pastOrders.ElementAt(j)._id);

            }
            prevOrders.SetAdapter(new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, pastOrderDates));
        }
    }
}