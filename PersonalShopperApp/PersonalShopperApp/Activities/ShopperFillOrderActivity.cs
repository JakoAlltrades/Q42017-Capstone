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

namespace PersonalShopperApp.Activities
{
    [Activity(Label = "ShopperFillOrderActivity")]
    public class ShopperFillOrderActivity : Activity
    {
        Order curOrder;
        OrderItem curItem;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ShopperViewOrder);
            ActionBar.Hide();
            if (Intent.HasExtra("curOrder"))
            {
                var customerSerialized = Intent.GetStringExtra("curOrder");
                curOrder = JsonConvert.DeserializeObject<Order>(customerSerialized);
            }

            ListView curOrders = FindViewById(Resource.Id.shopperViewOrder) as ListView;
            List<String> orderedItems = new List<string>();
            for (int j = 0; j < curOrder.placedOrder.Count; j++)
            {
                orderedItems.Add(curOrder.placedOrder.ElementAt(j).name);
            }
            curOrders.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, orderedItems));
            curOrders.ItemClick += (sender, e) =>
            {
                ListView lv = sender as ListView;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (e.Position == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                SetContentView(Resource.Layout.AssignPriceToItem);
                TextView itemNameView = FindViewById<TextView>(Resource.Id.ItemNameView);
                curItem = curOrder.placedOrder.ElementAt(e.Position);
                itemNameView.Text = "Assign Price to " + curItem.name;
            };
        }

        [Export("ItemFound")]
        public void ItemFound(View view)
        {
            EditText actPrice = FindViewById<EditText>(Resource.Id.actualPrice);
            double price;
            OrderItem temp = curItem;
            if (Double.TryParse(actPrice.Text, out price))
            {
                curItem.actualPrice = price;
            }
            curOrder.placedOrder.Remove(temp);
            curOrder.placedOrder.Add(curItem);
            curOrder.FoundItem(curItem);
            SetContentView(Resource.Layout.ShopperViewOrder);
            ListView curOrders = FindViewById(Resource.Id.shopperViewOrder) as ListView;
            List<String> orderedItems = new List<string>();
            for (int j = 0; j < curOrder.placedOrder.Count; j++)
            {
                orderedItems.Add(curOrder.placedOrder.ElementAt(j).name);
            }
            curOrders.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, orderedItems));
            curOrders.ItemClick += (sender, e) =>
            {
                ListView lv = sender as ListView;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (e.Position == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                SetContentView(Resource.Layout.AssignPriceToItem);
                TextView itemNameView = FindViewById<TextView>(Resource.Id.ItemNameView);
                curItem = curOrder.placedOrder.ElementAt(e.Position);
                itemNameView.Text = "Assign Price to " + curItem.name;
            };
        }

        [Export("ItemMissing")]
        public void ItemMissing(View view)
        {
            curOrder.MoveItemToMissing(curItem);
            SetContentView(Resource.Layout.ShopperViewOrder);
            ListView curOrders = FindViewById(Resource.Id.shopperViewOrder) as ListView;
            List<String> orderedItems = new List<string>();
            for (int j = 0; j < curOrder.placedOrder.Count; j++)
            {
                orderedItems.Add(curOrder.placedOrder.ElementAt(j).name);
            }
            curOrders.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, orderedItems));
            curOrders.ItemClick += (sender, e) =>
            {
                ListView lv = sender as ListView;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (e.Position == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                SetContentView(Resource.Layout.AssignPriceToItem);
                TextView itemNameView = FindViewById<TextView>(Resource.Id.ItemNameView);
                curItem = curOrder.placedOrder.ElementAt(e.Position);
                itemNameView.Text = "Assign Price to " + curItem.name;
            };
        }

        [Export("FoundItems")]
        public void FoundItems(View view)
        {
            SetContentView(Resource.Layout.ViewFound);
            ListView lv = FindViewById<ListView>(Resource.Id.foundItemsList);
            List<String> foundItems = new List<string>();
            for (int j = 0; j < curOrder.placedOrder.Count; j++)
            {
                foundItems.Add(curOrder.foundItems.ElementAt(j).name);
            }
            lv.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, foundItems));
        }

        [Export("MissingItems")]
        public void MissingItems(View view)
        {
            SetContentView(Resource.Layout.ViewMissing);
            ListView lv = FindViewById<ListView>(Resource.Id.missingItemsList);
            List<String> missingItems = new List<string>();
            for (int j = 0; j < curOrder.placedOrder.Count; j++)
            {
                missingItems.Add(curOrder.missingItems.ElementAt(j).name);
            }
            lv.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, missingItems));
            lv.ItemClick += (sender, e) =>
            {
                ListView l = sender as ListView;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (e.Position == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                SetContentView(Resource.Layout.MissingToFoundItem);
                TextView itemNameView = FindViewById<TextView>(Resource.Id.MissingItemNameView);
                curItem = curOrder.placedOrder.ElementAt(e.Position);
                itemNameView.Text = "Missing to Found: " + curItem.name;
            };
        }

        [Export("BackToOrder")]
        public void BackToOrder(View view)
        {
            SetContentView(Resource.Layout.ShopperViewOrder);
            ListView curOrders = FindViewById(Resource.Id.shopperViewOrder) as ListView;
            List<String> orderedItems = new List<string>();
            for (int j = 0; j < curOrder.placedOrder.Count; j++)
            {
                orderedItems.Add(curOrder.placedOrder.ElementAt(j).name);
            }
            curOrders.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleExpandableListItem1, orderedItems));
            curOrders.ItemClick += (sender, e) =>
            {
                ListView lv = sender as ListView;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (e.Position == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                SetContentView(Resource.Layout.AssignPriceToItem);
                TextView itemNameView = FindViewById<TextView>(Resource.Id.ItemNameView);
                curItem = curOrder.placedOrder.ElementAt(e.Position);
                itemNameView.Text = "Assign Price to " + curItem.name;
            };
        }
        [Export("FinishOrder")]
        public void FinishOrder(View view)
        {
            Intent directions = new Intent(this, typeof(MapActivity));
            string orderJson = JsonConvert.SerializeObject(curOrder);
            directions.PutExtra("curOrder", orderJson);
            StartActivity(directions);
        }
    }
}