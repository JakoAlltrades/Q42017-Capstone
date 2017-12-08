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
            if (Intent.HasExtra("customerDB"))
            {
                var databaseSerialized = Intent.GetStringExtra("customerDB");
                CADB = JsonConvert.DeserializeObject<CustomerActionsDB>(databaseSerialized);
            }
            if (Intent.HasExtra("curCustomer"))
            {
                var customerSerialized = Intent.GetStringExtra("curCustomer");
                curCustomer = JsonConvert.DeserializeObject<Customer>(customerSerialized);
            }

            ListView prevOrders = FindViewById<ListView>(Resource.Id.prevOrdersList);

            List<Order> pastOrders = await CADB.GetPrevoisOrdersAsync(curCustomer);
            List<string> pastOrderDates = pastOrders.Select(x => x._id).ToList<string>();
            prevOrders.SetAdapter(new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, pastOrderDates));
        }
    }
}