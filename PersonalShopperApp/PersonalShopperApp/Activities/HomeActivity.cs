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
using Java.Interop;
using PersonalShopperApp.Models;
using Java.IO;

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        User curUser;
        Customer curCustomer;
        Shopper curShopper;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.Home);
            // Create your application here
            IParcelable iUser = Intent.GetParcelableExtra("curUser");
            iUser.
            TextView tv = FindViewById(Resource.Id.homeWelcome) as TextView;
            tv.SetText("Welcome, " + curUser.Username + "!", TextView.BufferType.Normal);
            //instatiate an userdb, customerdb, and shopper db
        }

        #region home
        [Export("PlaceOrder")]
        public void PlaceOrder(View view)
        {
            Intent placeOrder = new Intent(this, typeof(PlaceOrderActivity));
            this.StartActivity(placeOrder);
        }

        [Export("PrevOrders")]
        public void PrevOrders(View view)
        {
            SetContentView(Resource.Layout.PreviousOrders);
        }

        [Export("BecShopper")]
        public void BecShopper(View view)
        {
            SetContentView(Resource.Layout.BecomeAShopper);
        }

        [Export("prevDeliv")]
        public void prevDeliv(View view)
        {
            SetContentView(Resource.Layout.PreviousDeliveries);
        }

        [Export("SignOut")]
        public void SignOut(View view)
        {
            Intent signIn = new Intent(Intent.ActionView);
            this.StartActivity(signIn);
        }
        #endregion
    }
}