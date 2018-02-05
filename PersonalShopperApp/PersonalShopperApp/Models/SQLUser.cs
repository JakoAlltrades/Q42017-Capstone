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
    public class SQLUser
    {
        public int userID { get; set; } = 3;
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public byte[] passHash { get; set; }
        public byte[] stAddress { get; set; }

        public SQLUser(string firstName, string lastName, string userName, byte[] passHash, byte[] address)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.userName = userName;
            this.passHash = passHash;
            this.stAddress = address;
        }
    }
}