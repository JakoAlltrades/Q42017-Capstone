﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PersonalShopperApp.Models
{
    public class CustomerActionsDB : BaseDB
    {
        public CustomerActionsDB(): base()
        {
        }

        public virtual async System.Threading.Tasks.Task<List<Order>> GetPrevoisOrdersAsync(Customer customer)
        {
            List<Order> ordersPlacedByCustomerID = new List<Order>();
            //var database = null;//client.GetDatabase("personalshopperdb");
            //var collection = database.GetCollection<BsonDocument>("orders");
            //using (IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(new BsonDocument()))
            //{
            //    while (await cursor.MoveNextAsync())
            //    {
            //        IEnumerable<BsonDocument> batch = cursor.Current;
            //        foreach (BsonDocument document in batch)
            //        {
            //            Order temp = BsonSerializer.Deserialize<Order>(document);
            //            if (temp.customerID == customer.userID)
            //            {
            //                ordersPlacedByCustomerID.Add(temp);
            //            }
            //        }
            //    }
            //}
            return ordersPlacedByCustomerID;
        }

        public virtual bool PlaceOrder(Order order)
        {
            bool orderPlaced = false;
            //var database = client.GetDatabase("personalshopperdb");
            //var collection = database.GetCollection<BsonDocument>("curPlacedOrders");
            //BsonDocument bsonOrder = new BsonDocument();
            //BsonDocumentWriter writer = new BsonDocumentWriter(bsonOrder);
            //BsonSerializer.Serialize<Order>(writer, order);
            //if (!bsonOrder.IsBsonNull)
            //{
            //    collection.InsertOneAsync(bsonOrder);
            //    //check if new order was inserted via linq
            //}
            return orderPlaced;
        }

        public virtual async System.Threading.Tasks.Task<bool> BecomeShopperAsync(Customer customer)
        {
            bool hasBecomeAShopper = false;
            //var database = client.GetDatabase("personalshopperdb");
            //var collection = database.GetCollection<Shopper>("shoppers");
            //Shopper shopper = customer as Shopper;
            //BsonDocument bsonShopper = new BsonDocument();
            //BsonDocumentWriter writer = new BsonDocumentWriter(bsonShopper);
            //BsonSerializer.Serialize<Shopper>(writer, shopper);
            //if (!bsonShopper.IsBsonNull)
            //{
            //    await collection.InsertOneAsync(shopper);
            //    Shopper pulledShopper = collection.AsQueryable<Shopper>().Where(x => x.shopperID == shopper.shopperID).First();
            //    if (pulledShopper.shopperID == shopper.shopperID)
            //    {
            //        hasBecomeAShopper = true;
            //    }
            //}
            return hasBecomeAShopper;
        }

    }

}
