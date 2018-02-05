using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalShopperApp.Models
{
    [Serializable]
    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int? ApartmentNumber { get; set; }
        public int Zipcode { get; set; }

        public string State { get; set; }

        public Address(string streetAddress, string city, string state, int zipcode, int? aptNum = null)
        {
            StreetAddress = streetAddress;
            City = city;
            State = state;
            Zipcode = zipcode;
            ApartmentNumber = aptNum;
        }

        public string GeocoderString()
        {
            return StreetAddress + ", " + City + ", " + State;
        }

        public override string ToString()
        {
            string baseStr;
            if(ApartmentNumber == null)
            {
                baseStr = StreetAddress + ", " + City + ", " + State + ", " + Zipcode + ", US"; 
            }
            else
            {
                baseStr = StreetAddress + " apt. " + ApartmentNumber + ", " + City + ", " + State + ", " + Zipcode + ", US";
            }
            return baseStr;
        }
    }
}
