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
using MongoDB.Bson.Serialization;
using PersonalShopperApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data;
using Newtonsoft.Json;

namespace PersonalShopperApp
{
    [Activity(Label = "PersonalShopperApp", MainLauncher = true)]
    public class MainActivity : Activity
    {

        private string creState;
        public Address delv = new Address("350 s 1200 e", "Salt Lake City", "UT", 84102, 7);
        public Address storeAddress;
        static byte[] pass = { 23, 123, 123, 123, 44, 12 };
        private User tempUser;


        UserActionsDB UADB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            base.ActionBar.Hide();
            //tempUser = new User(2, "Tpriem", new byte[24], "Tom", "Priem", delv);
            UADB = new UserActionsDB();
            bool isConnected = UADB.Connect();
            Button signIn = FindViewById<Button>(Resource.Id.signIn);
            signIn.Click += SignIn;
            //if(!isconnected)
            //{
            //    throw new exception("failed to connect to db");
            //}
            //Customer newCustomer = new Customer(tempUser.userID, tempUser.Username, tempUser.passHash, tempUser.fName, tempUser.lName, delv);
            /*string url = "http:localhost/PersonalShopperApplicationConnector/API/values";
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";*/
            
            //Intent i = new Intent(this, typeof(MapActivity));
            //this.StartActivity(i);
        }



        #region SignIn
        public async void SignIn(object sender, EventArgs e)
        {
           
            EditText userName = (EditText)FindViewById(Resource.Id.Username);
            string un = userName.Text;
            EditText password = (EditText)FindViewById(Resource.Id.Password);
            string passString = password.Text;
            byte[] passHash = UADB.Hash(passString);
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format("https://azuresqlconnection20180123112406.azurewebsites.net/api/User/Xamarin_Login?userName=" + un + "&pass=" + passString));
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = await client.GetAsync(uri);
            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] {
                    '"'
                });
                var jsonObject = JsonConvert.DeserializeObject<SQLUser>(errorMessage1);
                Address address = null;
                var addressBytes = jsonObject.stAddress;
                address = SQLSerializer.DeserializeAddress(addressBytes);
                tempUser = new User(jsonObject.userID, jsonObject.userName, jsonObject.passHash, jsonObject.firstName, jsonObject.lastName, address);
                Intent home = new Intent(this, typeof(HomeActivity));
                string userJson = Newtonsoft.Json.JsonConvert.SerializeObject(tempUser);
                home.PutExtra("curUser", userJson);
                this.StartActivity(home);
            }
            else
            {
                var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1]
            {
                '"'
            });
                Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();
            }
            //curUser = UADB.SignIn(un, pass).Result;
            //if (curUser != null)
            //{
            //Intent home = new Intent(this, typeof(HomeActivity));
            //string userJson = JsonConvert.SerializeObject(tempUser);
            //home.PutExtra("curUser", userJson);
            //this.StartActivity(home);
            //}
            //else
            //{
            //    Toast.MakeText(this, "Incorrect Username or Password!", ToastLength.Short).Show();
            //}
        }

        [Export("CreateAccount")]
        public void CreateAccount(View view)
        {
            SetContentView(Resource.Layout.CreateAccount1);
            Button createNext = FindViewById<Button>(Resource.Id.CreateNext);
            createNext.Click += CreateAccountNext;
        }
        #endregion

        #region createAccount
        public async void CreateAccountNext(object sender, EventArgs e)
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
        //    if (await UADB.CheckIfUsernameUsedAsync(uname))
        //    {
        if (pass1.Equals(pass2))
        {
                HttpClient client = new HttpClient();
                var uri = new Uri(string.Format("https://azuresqlconnection20180123112406.azurewebsites.net/api/User/CheckUserName?username=" + uname));
                HttpResponseMessage response;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(uri);
                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] {
                    '"'
                });
                    byte[] passhash = UADB.Hash(pass1);
                    tempUser = new User(0, uname, passhash, fname, lname, null);//new User(await UADB.GetCurUserIDAsync(), uname, passhash, fname, lname, null);
                    SetContentView(Resource.Layout.CreateAccount2);
                    Spinner states = (Spinner)FindViewById(Resource.Id.creStates);

                    states.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(CreState_ItemSelected);
                    var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.states_array, Android.Resource.Layout.SimpleSpinnerItem);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    states.Adapter = adapter;
                    Button createButton = FindViewById<Button>(Resource.Id.finishCreation);
                    createButton.Click += FinishCreation;
                }
                else
                {
                    var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1]
                    {
                        '"'
                    });
                    Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();
                }
                
        }
        else
        {
            Toast.MakeText(this, "The passwords do not match please reenter them correctly \np1:[" + pass1 + "]\np2:[" + pass2 + "]", ToastLength.Short).Show();
        }
        //    }
        //    else
        //    {
        //        Toast.MakeText(this, "The user name is already taken please enter a new one", ToastLength.Short).Show();
        //    }
        }
        

        public async void FinishCreation(object sender, EventArgs e)
        {
            EditText strAdd = FindViewById<EditText>(Resource.Id.creStreetAdd);
            string streetAddress = strAdd.Text;
            EditText cty = FindViewById<EditText>(Resource.Id.creCity);
            string city = cty.Text;
            EditText zip = FindViewById<EditText>(Resource.Id.creZip);
            string zipcodest = zip.Text;
            
            //toast to make sure state is correct
            EditText apt = FindViewById<EditText>(Resource.Id.creApt);
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
                    BinaryFormatter bf = new BinaryFormatter();
                    byte[] stAddress = null;
                    MemoryStream ms = new MemoryStream();
                    stAddress = SQLSerializer.SerializeAddress(address);
                    Customer newCustomer = new Customer(0, tempUser.Username, tempUser.passHash, tempUser.fName, tempUser.lName, address);
                    SQLUser sUser = new SQLUser(newCustomer.fName, newCustomer.lName, newCustomer.Username, newCustomer.passHash, stAddress);
                    HttpClient client = new HttpClient();
                    //string url = "http://localhost:1166/api/User/XAMARIN_REG";
                    string url = "https://azuresqlconnection20180123112406.azurewebsites.net/api/User/XAMARIN_REG";
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response;
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(sUser);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(uri, content);
                    //UADB.CreateUser(newCustomer as User);
                    if (response.StatusCode == HttpStatusCode.Accepted)
                    {
                        var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1]
                        {
                            '"'
                        });
                        Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();
                        Intent home = new Intent(this, typeof(HomeActivity));
                        string customerJson = Newtonsoft.Json.JsonConvert.SerializeObject(newCustomer);
                        home.PutExtra("curCustomer", customerJson);
                        this.StartActivity(home);
                    }
                    else
                    {
                        var errorMessage1 = response.Content.ReadAsStringAsync().Result;
                        Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();
                    }
                }
            }
            else
            {
                Toast.MakeText(this, "Error: please enter data for all data fields that are required (apt not required)", ToastLength.Short).Show();
            }
        }

        private void CreState_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("The state is {0}", spinner.GetItemAtPosition(e.Position));
            creState = spinner.GetItemAtPosition(e.Position).ToString();
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        #endregion
    }
}

