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
using Java.Interop;
using Xamarin.PayPal.Android;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "DeliverOrderActivity")]
    public class DeliverOrderActivity : Activity
    {
        Order curOrder;
        Shopper curShopper;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DeliverOrder);
            base.ActionBar.Hide();
            if (Intent.HasExtra("curOrder"))
            {
                var customerSerialized = Intent.GetStringExtra("curOrder");
                curOrder = JsonConvert.DeserializeObject<Order>(customerSerialized);
            }
            if (Intent.HasExtra("curShopper"))
            {
                var customerSerialized = Intent.GetStringExtra("curShopper");
                curShopper = JsonConvert.DeserializeObject<Shopper>(customerSerialized);
            }
            TextView actCost = FindViewById<TextView>(Resource.Id.delvActCost);
            actCost.Text = "Actual Cost:: " + curOrder.CalculateActualCost();
            Button confirmDelv = FindViewById<Button>(Resource.Id.confirmDelivery);
            confirmDelv.Click += ConfirmDelivery;
        }

        [Export("FoundItems")]
        public void PreviewFound(View view)
        {
            SetContentView(Resource.Layout.ViewFound);
            ListView lv = FindViewById<ListView>(Resource.Id.foundItemsList);
            List<String> foundItems = new List<string>();
            for (int j = 0; j < curOrder.Lists.foundItems.Count; j++)
            {
                foundItems.Add(curOrder.Lists.foundItems.ElementAt(j).name);
            }
            lv.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, foundItems));
        }

        [Export("BackToOrder")]
        public void BackToOrder(View view)
        {
            SetContentView(Resource.Layout.DeliverOrder);
            TextView actCost = FindViewById<TextView>(Resource.Id.delvActCost);
            actCost.Text = "Actual Cost:: " + curOrder.CalculateActualCost();
            Button confirmDelv = FindViewById<Button>(Resource.Id.confirmDelivery);
            confirmDelv.Click += ConfirmDelivery;
        }

        [Export("MissingItems")]
        public void PreviewMissing(View view)
        {
            SetContentView(Resource.Layout.ViewMissing);
            ListView lv = FindViewById<ListView>(Resource.Id.missingItemsList);
            List<String> missingItems = new List<string>();
            for (int j = 0; j < curOrder.Lists.missingItems.Count; j++)
            {
                missingItems.Add(curOrder.Lists.missingItems.ElementAt(j).name);
            }
            lv.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, missingItems));
        }

        public async void ConfirmDelivery(object sender, EventArgs e)
        {
            Models.PayPalManager ppm = new Models.PayPalManager(this);
            ppm.BuySomething(ppm.getThingToBuy(PayPalPayment.PaymentIntentSale, curOrder.CalculateActualCost(), curOrder._id));
            HttpClient client1 = new HttpClient();
            string url1 = "https://azuresqlconnection20180123112406.azurewebsites.net/api/CurOrder/DeleteOrder";
            var uri1 = new Uri(url1);
            client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response1;
            byte[] lists, storeAddress, delvAddress;
            lists = SQLSerializer.SerializeLists(curOrder.Lists);
            storeAddress = SQLSerializer.SerializeAddress(curOrder.storeAddress);
            delvAddress = SQLSerializer.SerializeAddress(curOrder.deliveryAddress);
            SQLOrder sQLOrder = new SQLOrder(curOrder.customerID, curOrder.shopperID, storeAddress, delvAddress, lists, (decimal)curOrder.EstimateCost(), (decimal)curOrder.CalculateActualCost());
            sQLOrder.orderID = curOrder.orderID;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(sQLOrder);
            var content1 = new StringContent(json, Encoding.UTF8, "application/json");
            response1 = await client1.PostAsync(uri1, content1);
            if (response1.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                HttpClient client2 = new HttpClient();
                HttpResponseMessage response2;
                string url2 = "https://azuresqlconnection20180123112406.azurewebsites.net/api/CompletedOrder/FinishOrder";
                var uri2 = new Uri(url2);
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var content2 = new StringContent(json, Encoding.UTF8, "application/json");
                response2 = await client2.PostAsync(uri2, content2);
                if (response2.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Intent home = new Intent(this, typeof(HomeActivity));
                    string userJson = Newtonsoft.Json.JsonConvert.SerializeObject(curShopper);
                    home.PutExtra("curShopper", userJson);
                    this.StartActivity(home);
                }
                else
                {
                    Toast.MakeText(this, "Error: failed to add order to completed orders", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Error: Failed to remove order with id [" + curOrder.orderID + "] from table", ToastLength.Short).Show();
            }
        }
    }
}