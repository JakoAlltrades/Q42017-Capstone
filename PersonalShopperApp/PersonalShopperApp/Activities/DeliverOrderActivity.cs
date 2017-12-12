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

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "DeliverOrderActivity")]
    public class DeliverOrderActivity : Activity
    {
        Order curOrder;
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
            
            TextView actCost = FindViewById<TextView>(Resource.Id.delvActCost);
            actCost.Text = "Actual Cost:: " + curOrder.CalculateActualCost();
        }

        [Export("ConfirmDelivery")]
        public void ConfirmDelivery(View view)
        {
            Models.PayPalManager ppm = new Models.PayPalManager(this);
            ppm.BuySomething(ppm.getThingToBuy(PayPalPayment.PaymentIntentSale, curOrder.CalculateActualCost(), curOrder._id));
            Finish();
        }
    }
}