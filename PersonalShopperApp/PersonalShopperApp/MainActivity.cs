using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Java.Interop;
using System;
using System.Collections.Generic;
using Android.Content;
using PersonalShopperApp.Activities;
using System.Net;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;
using MongoDB.Bson.Serialization;
using PersonalShopperApp.Models;
using Newtonsoft.Json;

namespace PersonalShopperApp
{
    [Activity(Label = "PersonalShopperApp", MainLauncher = true)]
    public class MainActivity : Activity
    {

        private User tempUser;
        private string creState;
        public Address delv = new Address("350 s 1200 e", "Salt Lake City", "UT", 84102, 7);
        public Address storeAddress;
        User curUser;


        UserActionsDB UADB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            UADB = new UserActionsDB("mongodb://192.168.1.200:27017");
            bool isConnected = UADB.Connect();
            if(!isConnected)
            {
                throw new Exception("Failed To connect to DB");
            }
            //Customer newCustomer = new Customer(tempUser.userID, tempUser.Username, tempUser.passHash, tempUser.fName, tempUser.lName, delv);
            /*string url = "http:localhost/PersonalShopperApplicationConnector/API/values";
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";*/
            /*var geoUri = Android.Net.Uri.Parse("geo:42.37,-71.12");
            var mapIntent = new Intent(Intent.ActionView, geoUri);
            Intent i = new Intent(this, typeof(MapActivity));
            this.StartActivity(i);*/
        }



        #region SignIn
        [Export("SignIn")]
        public void SignIn(View view)
        {
           
            EditText userName = (EditText)FindViewById(Resource.Id.Username);
            string un = userName.Text;
            EditText password = (EditText)FindViewById(Resource.Id.Password);
            string pass = password.Text;
            curUser = UADB.SignIn(un, pass).Result;
            if (curUser != null)
            {
                Intent home = new Intent(this, typeof(HomeActivity));
                string userJson = JsonConvert.SerializeObject(curUser);
                home.PutExtra("curUser", userJson);
                this.StartActivity(home);
            }
            else
            {
                Toast.MakeText(this, "Incorrect Username or Password!", ToastLength.Short).Show();
            }
        }

        [Export("CreateAccount")]
        public void CreateAccount(View view)
        {
            SetContentView(Resource.Layout.CreateAccount1);
        }
        #endregion

        #region createAccount
        [Export("CreateAccountNext")]
        public async void CreateAccountNext(View view)
        {
            EditText firstName = (EditText)FindViewById(Resource.Id.creFirstName);
            string fname = firstName.Text;
            EditText lastName = (EditText)FindViewById(Resource.Id.creLastName);
            string lname = lastName.Text;
            EditText userName = (EditText)FindViewById(Resource.Id.creUsername);
            string uname = userName.Text;
            EditText pass = (EditText)FindViewById(Resource.Id.crePassword);
            string pass1 = pass.Text;
            EditText conPass = (EditText)FindViewById(Resource.Id.creConPass);
            string pass2 = conPass.Text;
            if (await UADB.CheckIfUsernameUsedAsync(uname))
            {
                if (pass1.Equals(pass2))
                {
                    byte[] passhash = UADB.Hash(pass1);
                    tempUser = new User(await UADB.GetCurUserIDAsync(), uname, passhash, fname, lname, null);
                    SetContentView(Resource.Layout.CreateAccount2);
                    Spinner states = (Spinner)FindViewById(Resource.Id.creStates);
                    //Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

                    states.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(CreState_ItemSelected);
                    var adapter = ArrayAdapter.CreateFromResource(
                        this, Resource.Array.states_array, Android.Resource.Layout.SimpleSpinnerItem);

                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    states.Adapter = adapter;
                }
                else
                {
                    Toast.MakeText(this, "The passwords do not match please reenter them correctly \np1:[" + pass1 + "]\np2:[" + pass2 + "]", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "The user name is already taken please enter a new one", ToastLength.Short).Show();
            }
        }

        [Export("FinishCreation")]
        public void FinishCreation(View view)
        {
            EditText strAdd = (EditText)FindViewById(Resource.Id.creStreetAdd);
            string streetAddress = strAdd.Text;
            EditText cty = (EditText)FindViewById(Resource.Id.creCity);
            string city = cty.Text;
            EditText zip = (EditText)FindViewById(Resource.Id.creZip);
            string zipcodest = zip.Text;
            
            //toast to make sure state is correct
            EditText apt = (EditText)FindViewById(Resource.Id.creApt);
            string apartment = apt.Text;
            /*
             * Add the new information to the database and add the user as a shopper 
             * Also make sure that none of the data is empty
             */
            if (!String.IsNullOrEmpty(streetAddress) && !String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(zipcodest) && !String.IsNullOrEmpty(creState))
            {
                int? apartme = null;
                int apart;
                if (!String.IsNullOrWhiteSpace(apartment))
                { 
                    Int32.TryParse(apartment, out apart);
                    apartme = apart;
                }
                int zipcode;
                if (Int32.TryParse(zipcodest, out zipcode)) {
                    Address address = new Address(streetAddress, city, creState, zipcode, apartme);
                    /*
                     * Change the customer class to take a user as a constructor
                     */
                    Customer newCustomer = new Customer(tempUser.userID, tempUser.Username, tempUser.passHash, tempUser.fName, tempUser.lName, address);
                    Intent home = new Intent(this, typeof(HomeActivity));
                    string customerJson = JsonConvert.SerializeObject(newCustomer);
                    home.PutExtra("curCustomer", customerJson);
                    TextView tv = FindViewById(Resource.Id.homeWelcome) as TextView;
                    tv.SetText("Welcome, " + newCustomer.Username.ToString() + "!", TextView.BufferType.Normal);
                    this.StartActivity(home);
                }
            }
            
        }

        private void CreState_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            //string toast = string.Format("The state is {0}", spinner.GetItemAtPosition(e.Position));
            creState = spinner.GetItemAtPosition(e.Position).ToString();
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        #endregion
    }
}

