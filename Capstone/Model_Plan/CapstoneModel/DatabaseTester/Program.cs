using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneModelLib.GeneratedCode;

namespace DatabaseTester
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        public async static Task MainAsync()
        {
            UserActionsDB userActionsDB = new UserActionsDB("mongodb://192.168.1.200/27017");
            //userActionsDB.client.ListDatabases();

            //userActionsDB.client.GetDatabase("personalshopperdb").CreateCollection("shoppers");
            //userActionsDB.client.GetDatabase("personalshopperdb").CreateCollection("curPlacedOrders");
            //userActionsDB.client.GetDatabase("personalshopperdb").CreateCollection("completedOrders");

            //userActionsDB.client.GetDatabase("personalshopperdb").DropCollection("users");//Drops the collection/table
            User test1 = new User(userActionsDB.GetCurUserIDAsync().Result, "jpriem", userActionsDB.Hash("priem"), "John", "Priem", new Address("356 s 600 e", "Salt Lake City", "UT", 84102, 406));
            bool bool1 = userActionsDB.CreateUser(test1);
            User test2 = new User(userActionsDB.GetCurUserIDAsync().Result, "tpriem", userActionsDB.Hash("priem"), "Thomas", "Priem", new Address("1696 Autumn Pl", "Brentwood", "TN", 37027));
            bool bool2 = userActionsDB.CreateUser(test2);//False since both accounts are already created
            User user1 = userActionsDB.SignIn(test1.Username, "priem").Result;
            User user2 = userActionsDB.SignIn(test2.Username, "priem").Result;

            Order orderExample = new Order();
            orderExample.placedOrder.Add(new OrderItem("salt", 1.99, 0, 1));
            orderExample.placedOrder.Add(new OrderItem("ground pepper", 1.99, 0, 1));
            orderExample.placedOrder.Add(new OrderItem("half dozen eggs", 2.99, 0, 1));//Actual price is set as zero until the shopper sets the price
            CustomerActionDB CADBU1 = new CustomerActionDB("mongodb://192.168.1.200/27017", user1);
            
            CustomerActionDB CADBU2 = new CustomerActionDB("mongodb://192.168.1.200/27017", user2);
        }
    }
}
