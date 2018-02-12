﻿using System;
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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);

            base.ActionBar.Hide();
            //CADB = new CustomerActionDB("192.168.1.200");
            //SADB = new ShopperActionsDB("192.168.1.200");
            // Create your application here
            if (Intent.HasExtra("curUser"))
            {
                var userSerialized = Intent.GetStringExtra("curUser");
                curUser = JsonConvert.DeserializeObject<User>(userSerialized);
                curCustomer = new Customer(curUser.userID, curUser.Username, curUser.passHash, curUser.fName, curUser.lName, curUser.Address);
                //curShopper = new Shopper(curUser.Username, curUser.passHash, curUser.fName, curUser.lName, curUser.Address);
            }
            if(Intent.HasExtra("curCustomer"))
            {
                var customerSerialized = Intent.GetStringExtra("curCustomer");
                curCustomer = JsonConvert.DeserializeObject<Customer>(customerSerialized);
                curUser = new User(curCustomer.userID, curCustomer.Username, curCustomer.passHash, curCustomer.fName, curCustomer.lName, curCustomer.Address);
               // curShopper = new Shopper(curUser.Username, curUser.passHash, curUser.fName, curUser.lName, curUser.Address);
            }
            if(Intent.HasExtra("curShopper"))
            {
                var shopperSerialized = Intent.GetStringExtra("curShopper");
                curShopper = JsonConvert.DeserializeObject<Shopper>(shopperSerialized);
                curUser = new User(curShopper.userID, curShopper.Username, curShopper.passHash, curShopper.fName, curShopper.lName, curShopper.Address);
                curCustomer = new Customer(curUser.userID, curUser.Username, curUser.passHash, curUser.fName, curUser.lName, curUser.Address);
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
            Button prevDelv = FindViewById<Button>(Resource.Id.prevDelv);
            prevDelv.Click += PrevDeliv;
            Button prevOrder = FindViewById<Button>(Resource.Id.prevOrder);
            prevOrder.Click += PrevOrders;
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

        
        public void PrevOrders(object sender, EventArgs e)
        {
            Intent prevOrders = new Intent(this, typeof(PrevOrderActivity));
            string customerJson = JsonConvert.SerializeObject(curCustomer);
            prevOrders.PutExtra("curCustomer", customerJson);
            this.StartActivity(prevOrders);
        }

        [Export("BecShopper")]
        public void BecShopper(View view)
        {
            SetContentView(Resource.Layout.BecomeAShopper);
            Button shopperButton = FindViewById<Button>(Resource.Id.becomeShopper);
            shopperButton.Click += MakeShopper;
            Button recieveOrder = FindViewById<Button>(Resource.Id.RecieveOrders);
            recieveOrder.Click += RecieveOrder;
        }

        public async void MakeShopper(object sender, EventArgs e)
        {
            BinaryFormatter binarryFormatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] stAddress = null;
            try
            {
                binarryFormatter.Serialize(ms, curUser.Address);
                stAddress = ms.ToArray();
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
            }
            SQLUser sQLUser = new SQLUser(curUser.fName, curUser.lName, curUser.Username, curUser.passHash, stAddress);
            SQLShopper sShopper = new SQLShopper(curUser.userID);
            sShopper.User = sQLUser;
            HttpClient client = new HttpClient();
            string url = "https://azuresqlconnection20180123112406.azurewebsites.net/api/Shopper/MakeShopper";
            var uri = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(sShopper);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, content);
            if(response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Toast.MakeText(this, "You are now a shopper", ToastLength.Long).Show();
            }
            else
            {
                var errorMessage1 = response.Content.ReadAsStringAsync().Result;
                Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();
            }
        }



        
        public async void PrevDeliv(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var uri = new Uri("https://azuresqlconnection20180123112406.azurewebsites.net/api/Shopper/CheckIfShopper?userId=" + curUser.userID);
            HttpResponseMessage response;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = await httpClient.GetAsync(uri);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] {
                    '"'
                });
                var jsonObject = JsonConvert.DeserializeObject<SQLShopper>(errorMessage1);
                curShopper = new Shopper(jsonObject.userID, jsonObject.shopperID, jsonObject.User.userName, jsonObject.User.passHash, jsonObject.User.firstName, jsonObject.User.lastName, SQLSerializer.DeserializeAddress(jsonObject.User.stAddress));
                if (curShopper != null)
                {
                    Intent prevDelvs = new Intent(this, typeof(PrevDeliveriesActivity));
                    string shopperJson = JsonConvert.SerializeObject(curShopper);
                    prevDelvs.PutExtra("curShopper", shopperJson);
                    this.StartActivity(prevDelvs);
                }
            }
            else
            {
                Toast.MakeText(this, "Error: UserId [" + curUser.userID + "] is not a shopper, add him to get previous deliveries", ToastLength.Short).Show();
            }
        }

        [Export("SignOut")]
        public void SignOut(View view)
        {
            Intent signIn = new Intent(this, typeof(MainActivity));
            this.StartActivity(signIn);
            Finish();
        }
        
        public async void RecieveOrder(object sender, EventArgs e)
        {
            Intent recieveOrders = new Intent(this, typeof(RecieveOrdersActivity));
            HttpClient httpClient = new HttpClient();
            var uri = new Uri("https://azuresqlconnection20180123112406.azurewebsites.net/api/Shopper/CheckIfShopper?userId=" + curUser.userID);
            HttpResponseMessage response;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = await httpClient.GetAsync(uri);
            if(response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] {
                    '"'
                });
                var jsonObject = JsonConvert.DeserializeObject<SQLShopper>(errorMessage1);
                curShopper = new Shopper(jsonObject.userID, jsonObject.shopperID, jsonObject.User.userName, jsonObject.User.passHash, jsonObject.User.firstName, jsonObject.User.lastName, SQLSerializer.DeserializeAddress(jsonObject.User.stAddress));
                string shopperJson = JsonConvert.SerializeObject(curShopper);
                recieveOrders.PutExtra("curShopper", shopperJson);
                //string shopperDB = JsonConvert.SerializeObject(SADB);
                //recieveOrders.PutExtra("shopperDB", shopperDB);
                string customerJson = Newtonsoft.Json.JsonConvert.SerializeObject(curCustomer);
                recieveOrders.PutExtra("curCustomer", customerJson);
                this.StartActivity(recieveOrders);
            }
            else
            {
                Toast.MakeText(this, "Error: UserId [" + curUser.userID + "] is not a shopper, add him to get orders", ToastLength.Short).Show();
            }
            
        }
        
        #endregion
    }
}