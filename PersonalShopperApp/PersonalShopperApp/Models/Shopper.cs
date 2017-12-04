﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Android.Widget;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalShopperApp.Models
{
    public class Shopper : Customer
    {
        public Shopper(int ID, string userName, byte[] passHash, string fName, string lName, Address address) : base(ID, userName, passHash, fName, lName, address)
        {
            shopperID = ID;
        }

        public int shopperID
        {
            get;
            private set;
        }

        public virtual double rating
        {
            get;
            set;
        }

        public virtual double radius
        {
            get;
            set;
        }

        public virtual bool isAvailable
        {
            get;
            set;
        }

        public virtual bool inStandBy
        {
            get;
            set;
        }

        private Order curTakenOrder;

        private bool RecieveOrder(Order order)
        {
            bool orderTaken = false;
            if (order != null)
            {
                curTakenOrder = order;
                orderTaken = true;
            }
            return orderTaken;
        }

    }

}