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
            userActionsDB.client.GetDatabase("personalshopperdb").DropCollection("users");
            User test1 = new User(userActionsDB.GetCurUserIDAsync().Result, "jpriem", userActionsDB.Hash("priem"), "John", "Priem", new Address("356 s 600 e", "Salt Lake City", "UT", 84102, 7));
            userActionsDB.CreateUser(test1);
            User test2 = new User(userActionsDB.GetCurUserIDAsync().Result, "tpriem", userActionsDB.Hash("priem"), "Thomas", "Priem", new Address("1696 Autumn Pl", "Brentwood", "TN", 37027));
            userActionsDB.CreateUser(test2);
            User user1 = userActionsDB.SignIn(test1.Username, "priem").Result;
            User user2 = userActionsDB.SignIn(test2.Username, "priem").Result;
        }
    }
}
