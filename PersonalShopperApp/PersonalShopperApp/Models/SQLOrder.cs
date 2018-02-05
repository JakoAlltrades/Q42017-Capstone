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
    public class SQLOrder
    {
        public int orderID { get; set; }
        public int customerID { get; set; }
        public int? shopperID { get; set; } = null;
        public byte[] storeAddress { get; set; }
        public byte[] deliveryAddress { get; set; }
        public byte[] Lists { get; set; }
        public decimal estimatedCost { get; set; }
        public decimal actualCost { get; set; }
        public Nullable<int> orderRating { get; set; } = null;

        public SQLOrder(int customerID, int? shopperID, byte[] storeAddress, byte[] deliveryAddress, byte[] Lists, decimal estimatedCost, decimal actualCost)
        {
            this.customerID = customerID;
            this.shopperID = shopperID;
            this.storeAddress = storeAddress;
            this.deliveryAddress = deliveryAddress;
            this.Lists = Lists;
            this.actualCost = actualCost;
            this.estimatedCost = estimatedCost;
        }

        public void SetRating(int rating)
        {
            orderRating = rating;
        }
    }
}