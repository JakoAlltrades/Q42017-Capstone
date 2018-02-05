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

namespace PersonalShopperApp.Models
{
    [Serializable]
    public class OrderLists
    {
        public List<OrderItem> foundItems
        {
            get;
            private set;
        }

        public List<OrderItem> missingItems
        {
            get;
            private set;
        }

        public List<OrderItem> placedOrder
        {
            get;
            private set;
        }

        public OrderLists()
        {
            foundItems = new List<OrderItem>();
            missingItems = new List<OrderItem>();
            placedOrder = new List<OrderItem>();
        }
    }
}