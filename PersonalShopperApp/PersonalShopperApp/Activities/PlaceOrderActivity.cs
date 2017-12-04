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
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;
using PayPal.Forms;
using Xamarin.Forms.Maps;
using Xamarin.PayPal.Android;

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "PlaceOrderActivity")]
    public class PlaceOrderActivity : Activity
    {


        private Order curOrder = new Order();
        public Address delv = new Address("350 s 1200 e", "Salt Lake City", "UT", 84102, 7);
        public Address storeAddress;
        private bool removeItemFromOrder = false;
        private int editItemPos;
        private string storeState;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PlaceOrder);
            // Create your application here
            curOrder = new Order();
            delv.ToString();
            curOrder.deliveryAddress = delv;
            
        }

       

        #region PlaceOrderLogic
        [Export("AddItemPage")]
        public void AddItemPage(View view)
        {
            SetContentView(Resource.Layout.AddOrderItem);
        }


        [Export("AddItemToOrder")]
        public void AddItemToOrder(View view)
        {
            EditText addItemName = (EditText)FindViewById(Resource.Id.AddItemName);
            string itemName = addItemName.Text;
            EditText itemMaxPrice = (EditText)FindViewById(Resource.Id.AddItemMaxPrice);
            string itemPriceString = itemMaxPrice.Text;
            double price;
            if (!String.IsNullOrEmpty(itemName) && Double.TryParse(itemPriceString, out price))
            {
                SetContentView(Resource.Layout.PlaceOrder);
                OrderItem oi = new OrderItem(itemName, price, 0.0);
                curOrder.placedOrder.Add(oi);
                ListView orderItems = FindViewById(Resource.Id.orderedItems) as ListView;
                List<String> order = new List<string>();
                for (int j = 0; j < curOrder.placedOrder.Count; j++)
                {
                    order.Add(curOrder.placedOrder[j].name);
                    
                }
                if (orderItems != null)
                {

                    TextView estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                    estimatedCost.SetText("Estimated Cost:: " + curOrder.EstimateCost().ToString(), TextView.BufferType.Normal);
#pragma warning disable CS0618 // Type or member is obsolete
                    orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
#pragma warning restore CS0618 // Type or member is obsolete
                    orderItems.ItemClick += (sender, e) =>
                    {
                        ListView lv = sender as ListView;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                        if (e.Position == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                        {
                            return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                        }
                        if (!removeItemFromOrder)
                        {
                            Toast.MakeText(this, "Item Selected: " + e.Position, ToastLength.Short).Show();
                            SetContentView(Resource.Layout.EditOrderItem);
                            OrderItem item = curOrder.placedOrder[e.Position];
                            EditText iName = (EditText)FindViewById(Resource.Id.EditItemName);
                            EditText iPrice = (EditText)FindViewById(Resource.Id.EditItemMaxPrice);
                            iName.Text = item.name;
                            iPrice.Text = item.maxPrice.ToString();
                        }
                        else
                        {
                            editItemPos = e.Position;
                            curOrder.placedOrder.RemoveAt(editItemPos);
                            order = new List<string>();
                            curOrder.EstimateCost();
                            foreach(OrderItem item in curOrder.placedOrder)
                            {
                                order.Add(item.name);
                            }
#pragma warning disable CS0618 // Type or member is obsolete
                            orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
                            estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                            estimatedCost.SetText("Estimated Cost:: " + curOrder.EstimateCost(), TextView.BufferType.Normal);
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
            double price;
            if (!String.IsNullOrEmpty(itemName) && Double.TryParse(itemPriceString, out price))
            {
                SetContentView(Resource.Layout.PlaceOrder);
                OrderItem oi = new OrderItem(itemName, price, 0.0);
                curOrder.placedOrder.RemoveAt(editItemPos);
                curOrder.placedOrder.Add(oi);
                ListView orderItems = FindViewById(Resource.Id.orderedItems) as ListView;
                List<String> order = new List<string>();
                for (int j = 0; j < curOrder.placedOrder.Count; j++)
                {
                    order.Add(curOrder.placedOrder[j].name);
                }
                if (orderItems != null)
                {
                    TextView estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                    estimatedCost.SetText("Estimated Cost:: " + curOrder.EstimateCost(), TextView.BufferType.Normal);
                    orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
                    orderItems.ItemClick += (sender, e) =>
                    {
                        ListView lv = sender as ListView;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                        if (e.Position == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                        {
                            return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                        }
                        if (!removeItemFromOrder)
                        {
                            Toast.MakeText(this, "Item Selected: " + e.Position, ToastLength.Short).Show();
                            SetContentView(Resource.Layout.EditOrderItem);
                            OrderItem item = curOrder.placedOrder[e.Position];
                            EditText iName = (EditText)FindViewById(Resource.Id.EditItemName);
                            EditText iPrice = (EditText)FindViewById(Resource.Id.EditItemMaxPrice);
                            iName.Text = item.name;
                            iPrice.Text = item.maxPrice.ToString();
                        }
                        else
                        {
                            curOrder.placedOrder.RemoveAt(e.Position);
                            order = new List<string>();
                            for (int j = 0; j < curOrder.placedOrder.Count; j++)
                            {
                                order.Add(curOrder.placedOrder[j].name);
                            }
                            orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
                            estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                            estimatedCost.SetText("Estimated Cost:: " + curOrder.EstimateCost(), TextView.BufferType.Normal);
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
            states.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(StoreState_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.states_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            states.Adapter = adapter;
        }

        [Export("ConfirmOrder")]
        public async void ConfrimOrderAsync(View view)
        {
            //Position delvPos = await curOrder.GetDeliveryPositionAsync();
            //Position storePos = await curOrder.GetStorePositionAsync();
            //PaymentResult result = await CrossPayPalManager.Current.Buy(new PayPalItem("Test Product", new Decimal(.01), "USD"), new Decimal(0));
            Models.PayPalManager ppm = new Models.PayPalManager(this);
            await ppm.BuySomething(ppm.getThingToBuy(PayPalPayment.PaymentIntentSale, curOrder.EstimateCost(), curOrder._id);
            
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
                    for (int j = 0; j < curOrder.placedOrder.Count; j++)
                    {
                        order.Add(curOrder.placedOrder[j].name);
                    }
                    orderItems.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, order));
                    TextView estimatedCost = FindViewById(Resource.Id.EstimatedCost) as TextView;
                    estimatedCost.SetText("Estimated Cost:: " + curOrder.EstimateCost(), TextView.BufferType.Normal);
                }
            }
        }

        private void StoreState_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            //string toast = string.Format("The state is {0}", spinner.GetItemAtPosition(e.Position));
            storeState = spinner.GetItemAtPosition(e.Position).ToString();
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        #endregion
    }
}