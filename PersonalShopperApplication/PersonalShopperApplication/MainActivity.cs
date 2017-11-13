﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Java.Interop;
using CapstoneModelLib.GeneratedCode;
using System;
using System.Collections.Generic;
//using Toast;

namespace PersonalShopperApplication
{
    [Activity(Label = "PersonalShopperApplication", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //ssl secure https pulls
        private User tempUser;
        private BaseDB db;
        private string creState;
        private string storeState;
        private List<OrderItem> curOrder;
        private bool removeItemFromOrder = false;
        private int editItemPos;
        private double orderTotal;
        public Address delv = new Address("350 s 1200 e", "Salt Lake City", "UT", 84102, 7);
        public Address storeAddress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }



        #region SignIn
        UserActionsDB UADB = new UserActionsDB("mongodb://192.168.1.200/27017");
        [Export("SignIn")]
        public void SignIn(View view)
        {
            EditText userName = (EditText)FindViewById(Resource.Id.Username);
            string un = userName.Text;
            EditText password = (EditText)FindViewById(Resource.Id.Password);
            string pass = password.Text;
            //User curUser = UADB.SignIn(un, pass).Result;
            //if (curUser != null)
            //{
                SetContentView(Resource.Layout.Home);
                TextView tv = FindViewById(Resource.Id.homeWelcome) as TextView;
                tv.SetText("Welcome, " + un + "!", TextView.BufferType.Normal);
            //}
            //else
            //{
            //Toast.Toast toast = new Toast.Toast();
            //toast.Message = "Incorrect Username or Password!";
            //toast.DurationToast = Toast.Toast.ToastDuration.Medium;
            //toast.Show();
            // }
        }

        [Export("CreateAccount")]
        public void CreateAccount(View view)
        {
            SetContentView(Resource.Layout.CreateAccount1);
        }
        #endregion

        #region home
        [Export("PlaceOrder")]
        public void PlaceOrder(View view)
        {
            SetContentView(Resource.Layout.PlaceOrder);
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
            SetContentView(Resource.Layout.Main);
        }
        #endregion

        #region createAccount
        [Export("CreateAccountNext")]
        public void CreateAccountNext(View view)
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
            /*
             * also check if userName is in use  
             * then set variables to a global tempUser which will be cleared once the account is created
             * also store the hashPassword
             */
            if (pass1.Equals(pass2))
            {
                byte[] passhash = UADB.SaltAndHashPassword(pass1);
                tempUser = new User(2, uname, passhash, fname, lname, null);
                SetContentView(Resource.Layout.CreateAccount2);
                Spinner states = (Spinner)FindViewById(Resource.Id.creStates);
               // Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

                states.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(creState_ItemSelected);
                var adapter = ArrayAdapter.CreateFromResource(
                        this, Resource.Array.states_array, Android.Resource.Layout.SimpleSpinnerItem);

                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                states.Adapter = adapter;


            }
            else
            {

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
                int apart;
                Int32.TryParse(apartment, out apart);
                int? apartme = apart;
                int zipcode;
                if (Int32.TryParse(zipcodest, out zipcode)) {
                    Address address = new Address(streetAddress, city, creState, zipcode, apartme);
                    /*
                     * Change the customer class to take a user as a constructor
                     */
                    Customer newCustomer = new Customer(tempUser.userID, tempUser.Username, tempUser.passHash, tempUser.fName, tempUser.lName, address);
                    SetContentView(Resource.Layout.Home);
                    TextView tv = FindViewById(Resource.Id.homeWelcome) as TextView;
                    tv.SetText("Welcome, " + newCustomer.Username.ToString() + "!", TextView.BufferType.Normal);
                }
            }
            
        }

        private void creState_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("The state is {0}", spinner.GetItemAtPosition(e.Position));
            creState = spinner.GetItemAtPosition(e.Position).ToString();
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        #endregion

        #region PlaceOrderLogic

        [Export("AddItemPage")]
        public void AddItemPage(View view)
        {
            if (curOrder == null)
            {
                curOrder = new List<OrderItem>();
                orderTotal = 0;
            }
            SetContentView(Resource.Layout.AddOrderItem);
        }

        
        [Export("AddItemToOrder")]
        public void AddItemToOrder(View view)
        {
            EditText addItemName = (EditText)FindViewById(Resource.Id.AddItemName);
            string itemName = addItemName.Text;
            EditText itemMaxPrice = (EditText)FindViewById(Resource.Id.AddItemMaxPrice);
            string itemPriceString = itemMaxPrice.Text;
            EditText itemAmount = (EditText)FindViewById(Resource.Id.AddItemAmmount);
            string itemAmountString = itemAmount.Text;
            double price;
            int amount;
            if(!String.IsNullOrEmpty(itemName) && Double.TryParse(itemPriceString,out price) && Int32.TryParse(itemAmountString, out amount))
            {
                SetContentView(Resource.Layout.PlaceOrder);
                OrderItem oi = new OrderItem(itemName, price, 0.0, amount);
                curOrder.Add(oi);
                ListView orderItems = FindViewById(Resource.Id.orderedItems) as ListView;
                List<String> order = new List<string>();
                orderTotal = 0;
                for (int j = 0; j < curOrder.Count; j++)
                {
                    order.Add(curOrder[j].name);
                    orderTotal += curOrder[j].maxPrice;
                }
                if (orderItems != null)
                {
                    
                    TextView estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                    estimatedCost.SetText("Estimated Cost:: " + orderTotal.ToString(), TextView.BufferType.Normal);
                    orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
                    orderItems.ItemClick += async(sender, e) => {
                        ListView lv = sender as ListView;
                        if (e.Position == null)
                        {
                            return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                        }
                        if (!removeItemFromOrder)
                        {
                           Toast.MakeText(this, "Item Selected: " + e.Position, ToastLength.Short).Show();
                            SetContentView(Resource.Layout.EditOrderItem);
                            OrderItem item = curOrder[e.Position];
                            EditText iName = (EditText)FindViewById(Resource.Id.EditItemName);
                            EditText iPrice = (EditText)FindViewById(Resource.Id.EditItemMaxPrice);
                            EditText iAmount = (EditText)FindViewById(Resource.Id.EditItemAmmount);
                            iName.Text = item.name;
                            iPrice.Text = item.maxPrice.ToString();
                            iAmount.Text = item.amount.ToString();
                        }
                        else
                        {
                            curOrder.RemoveAt(e.Position);
                            order = new List<string>();
                            for (int j = 0; j < curOrder.Count; j++)
                            {
                                order.Add(curOrder[j].name);
                                orderTotal += curOrder[j].maxPrice;
                            }
                            orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
                            estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                            estimatedCost.SetText("Estimated Cost:: " + orderTotal, TextView.BufferType.Normal);
                        }
                    };
                    
                }
                else
                {
                    Toast.MakeText(this, "Error ListView is null", ToastLength.Long).Show();
                }
            }


        }


        [Export("EditItemToOrder")]
        public void EditItemToOrder(View view)
        {
            EditText editItemName = (EditText)FindViewById(Resource.Id.EditItemName);
            string itemName = editItemName.Text;
            EditText itemMaxPrice = (EditText)FindViewById(Resource.Id.EditItemMaxPrice);
            string itemPriceString = itemMaxPrice.Text;
            EditText itemAmount = (EditText)FindViewById(Resource.Id.EditItemAmmount);
            string itemAmountString = itemAmount.Text;
            double price;
            int amount;
            if (!String.IsNullOrEmpty(itemName) && Double.TryParse(itemPriceString, out price) && Int32.TryParse(itemAmountString, out amount))
            {
                SetContentView(Resource.Layout.PlaceOrder);
                OrderItem oi = new OrderItem(itemName, price, 0.0, amount);
                curOrder.RemoveAt(editItemPos);
                curOrder.Add(oi);
                ListView orderItems = FindViewById(Resource.Id.orderedItems) as ListView;
                List<String> order = new List<string>();
                orderTotal = 0;
                for (int j = 0; j < curOrder.Count; j++)
                {
                    order.Add(curOrder[j].name);
                    orderTotal += curOrder[j].maxPrice;
                }
                if (orderItems != null)
                {
                    TextView estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                    estimatedCost.SetText("Estimated Cost:: " + orderTotal, TextView.BufferType.Normal);
                    orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
                    orderItems.ItemClick += async (sender, e) => {
                        ListView lv = sender as ListView;
                        if (e.Position == null)
                        {
                            return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                        }
                        if (!removeItemFromOrder)
                        {
                            Toast.MakeText(this, "Item Selected: " + e.Position, ToastLength.Short).Show();
                            SetContentView(Resource.Layout.EditOrderItem);
                            OrderItem item = curOrder[e.Position];
                            EditText iName = (EditText)FindViewById(Resource.Id.EditItemName);
                            EditText iPrice = (EditText)FindViewById(Resource.Id.EditItemMaxPrice);
                            EditText iAmount = (EditText)FindViewById(Resource.Id.EditItemAmmount);
                            iName.Text = item.name;
                            iPrice.Text = item.maxPrice.ToString();
                            iAmount.Text = item.amount.ToString();
                        }
                        else
                        {
                            curOrder.RemoveAt(e.Position);
                            order = new List<string>();
                            orderTotal = 0;
                            for (int j = 0; j < curOrder.Count; j++)
                            {
                                order.Add(curOrder[j].name);
                                orderTotal += curOrder[j].maxPrice;
                            }
                            orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
                            estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                            estimatedCost.SetText("Estimated Cost:: " + orderTotal, TextView.BufferType.Normal);
                        }
                    };
                    
                }
                else
                {
                    Toast.MakeText(this, "Error ListView is null", ToastLength.Long).Show();
                }
            }

        }

        [Export("RemoveItem")]
        public void RemoveItem(View view)
        {
            removeItemFromOrder = !removeItemFromOrder;
            Toast.MakeText(this, "RemoveItemFromText is " + removeItemFromOrder, ToastLength.Short).Show();
        }

        [Export("SetStoreAddress")]
        public void SetStoreAddress(View view)
        {
            SetContentView(Resource.Layout.SetStoreAddress);
            Spinner states = (Spinner)FindViewById(Resource.Id.storeStates);
            states.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(storeState_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.states_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            states.Adapter = adapter;
        }

        [Export("FinishStoreAddress")]
        public void FinishStoreAddress(View view)
        {
            EditText strAdd = (EditText)FindViewById(Resource.Id.storeStreetAdd);
            string streetAddress = strAdd.Text;
            EditText cty = (EditText)FindViewById(Resource.Id.storeCity);
            string city = cty.Text;
            EditText zip = (EditText)FindViewById(Resource.Id.storeZip);
            string zipcodest = zip.Text;

            //toast to make sure state is correct
            EditText apt = (EditText)FindViewById(Resource.Id.storeApt);
            string apartment = apt.Text;
            /*
             * Add the new information to the database and add the user as a shopper 
             * Also make sure that none of the data is empty
             */
            if (!String.IsNullOrEmpty(streetAddress) && !String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(zipcodest) && !String.IsNullOrEmpty(storeState))
            {
                int apart;
                Int32.TryParse(apartment, out apart);
                int? apartme = apart;
                int zipcode;
                if (Int32.TryParse(zipcodest, out zipcode))
                {
                    Address address = new Address(streetAddress, city, storeState, zipcode, apartme);
                    /*
                     * Change the customer class to take a user as a constructor
                     */
                    //Customer newCustomer = new Customer(tempUser.userID, tempUser.Username, tempUser.passHash, tempUser.fName, tempUser.lName, address);
                    storeAddress = address;
                    SetContentView(Resource.Layout.PlaceOrder);
                    ListView orderItems = FindViewById(Resource.Id.orderedItems) as ListView;
                    List<String> order = new List<string>();
                    orderTotal = 0;
                    for (int j = 0; j < curOrder.Count; j++)
                    {
                        order.Add(curOrder[j].name);
                        orderTotal += curOrder[j].maxPrice;
                    }
                    orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
                    TextView estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                    estimatedCost.SetText("Estimated Cost:: " + orderTotal, TextView.BufferType.Normal);
                }
            }
        }

        private void storeState_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("The state is {0}", spinner.GetItemAtPosition(e.Position));
            storeState = spinner.GetItemAtPosition(e.Position).ToString();
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        #endregion
    }
}

