//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AzureSQLConnection.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class curPlacedOrder
    {
        public int orderID { get; set; }
        public int customerID { get; set; }
        public Nullable<int> shopperID { get; set; }
        public byte[] storeAddress { get; set; }
        public byte[] deliveryAddress { get; set; }
        public byte[] Lists { get; set; }
        public decimal estimatedCost { get; set; }
        public decimal actualCost { get; set; }
        public Nullable<int> orderRating { get; set; }
    
        public virtual User User { get; set; }
        public virtual Shopper Shopper { get; set; }
    }
}