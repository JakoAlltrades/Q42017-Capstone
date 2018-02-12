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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PersonalShopperApp.Models
{
    public class SQLSerializer
    {
        public static byte[] SerializeAddress(Address address)
        {
            byte[] addressBytes = null;
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                var ms = new MemoryStream();
                bf.Serialize(ms, address);
                addressBytes = ms.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return addressBytes;

        }

        public static Address DeserializeAddress(byte[] addressBytes)
        {
            Address address = null;
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                var ms = new MemoryStream(addressBytes);
                address = (Address)bf.Deserialize(ms);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return address;

        }

        public static byte[] SerializeLists(OrderLists lists)
        {
            byte[] listsBytes = null;
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                var ms = new MemoryStream();
                bf.Serialize(ms, lists);
                listsBytes = ms.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return listsBytes;

        }

        public static OrderLists DeserializeLists(byte[] listsBytes)
        {
            OrderLists Lists = null;
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                var ms = new MemoryStream(listsBytes);
                Lists = (OrderLists)bf.Deserialize(ms);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Lists;

        }
    }
}