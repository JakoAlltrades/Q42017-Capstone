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
using Newtonsoft.Json;

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        User curUser;
        Customer curCustomer;
        Shopper curShopper;
        //CustomerActionDB CADB;
        //ShopperActionsDB SADB;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.Home);
            //CADB = new CustomerActionDB("192.168.1.200");
            //SADB = new ShopperActionsDB("192.168.1.200");
            // Create your application here
            if (Intent.HasExtra("curUser"))
            {
                var userSerialized = Intent.GetStringExtra("curUser");
                curUser = JsonConvert.DeserializeObject<User>(userSerialized);
                curCustomer = curUser as Customer;
            }
            if(Intent.HasExtra("curCustomer"))
            {
                var customerSerialized = Intent.GetStringExtra("curCustomer");
                curCustomer = JsonConvert.DeserializeObject<Customer>(customerSerialized);
            }
            if(Intent.HasExtra("curShopper"))
            {
                var shopperSerialized = Intent.GetStringExtra("curShopper");
                curShopper = JsonConvert.DeserializeObject<Shopper>(shopperSerialized);
            }
            //if (curUser != null || curCustomer != null)
            //{
            //    if(curUser == null)
            //    {
            //        curUser = curCustomer as User;
            //    }
            //    if (curShopper == null)
            //    {
            //        if (curShopper == null && curUser != null)
            //        {
            //            await SADB.CheckIfUserIsShopper(curUser);
            //        }
            //    }
            //}
            TextView tv = FindViewById(Resource.Id.homeWelcome) as TextView;
            tv.SetText("Welcome, " + curUser.Username + "!", TextView.BufferType.Normal);
        }

        #region home
        [Export("PlaceOrder")]
        public void PlaceOrder(View view)
        {
            Intent placeOrder = new Intent(this, typeof(PlaceOrderActivity));
            string customerJson = JsonConvert.SerializeObject(curCustomer);
            placeOrder.PutExtra("curCustomer", customerJson);
            //string customerDB = JsonConvert.SerializeObject(SADB);
            //placeOrder.PutExtra("customerDB", customerDB);
            this.StartActivity(placeOrder);
        }

        [Export("PrevOrders")]
        public void PrevOrders(View view)
        {
            Intent prevOrders = new Intent(this, typeof(PrevOrderActivity));
            string customerJson = JsonConvert.SerializeObject(curCustomer);
            prevOrders.PutExtra("curCustomer", customerJson);
            //string customerDB = JsonConvert.SerializeObject(CADB);
            //prevOrders.PutExtra("customerDB", customerDB);
            this.StartActivity(prevOrders);
        }

        [Export("BecShopper")]
        public void BecShopper(View view)
        {
            SetContentView(Resource.Layout.BecomeAShopper);
        }

        [Export("PrevDeliv")]
        public void PrevDeliv(View view)
        {
            if (curShopper != null)
            {
                Intent prevDelvs = new Intent(this, typeof(PrevDeliveriesActivity));
                string shopperJson = JsonConvert.SerializeObject(curShopper);
                prevDelvs.PutExtra("curShopper", shopperJson);
                //string shopperDB = JsonConvert.SerializeObject(SADB);
                //prevDelvs.PutExtra("shopperDB", shopperDB);
                this.StartActivity(prevDelvs);
            }
        }

        [Export("SignOut")]
        public void SignOut(View view)
        {
            Intent signIn = new Intent(this, typeof(MainActivity));
            this.StartActivity(signIn);
            Finish();
        }
        #endregion
    }
}