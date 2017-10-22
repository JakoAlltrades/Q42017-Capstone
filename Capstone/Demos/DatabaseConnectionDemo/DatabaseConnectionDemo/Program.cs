using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseConnectionDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().Wait();

            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            var connectionString = "mongodb://10.10.19.42:27017";//possible IP for VMWare
            //var client = new MongoClient(new MongoUrl("mongodb://localhost:27017"));
            var client = new MongoClient(connectionString);
        }
    }
}