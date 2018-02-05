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
    public class SQLShopper
    {
        public int shopperID { get; set; }
        public int userID { get; set; }
        public virtual SQLUser User { get; set; }

        public SQLShopper(int UserID)
        {
            userID = UserID;
        }
    }
}