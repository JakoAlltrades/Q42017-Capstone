using AzureSQLConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureSQLConnection.Controllers
{
    public class ShopperController : ApiController
    {
        CapstoneDBEntities db = new CapstoneDBEntities();
        [ActionName("MakeShopper")]
        // POST: api/User/XAMARING 
        [HttpPost]
        public HttpResponseMessage MakeShopper(Shopper user)
        {
           
                db.Shoppers.Add(user);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Shopper Created");
            
        }

        [ActionName("CheckIfShopper")]
        // POST: api/User/XAMARING 
        [HttpGet]
        public HttpResponseMessage CheckIfShopper(int userId)
        {
            Shopper shopper = db.Shoppers.Where(x => x.userID == userId).FirstOrDefault();
            if (shopper != null)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, shopper);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "No shopper for the UserID:[" + userId + "]");
            }
        }

        [ActionName("GatherCurrentOrders")]
        // POST: api/User/XAMARING 
        [HttpGet]
        public HttpResponseMessage GetCurrentOrders()
        {
            List<curPlacedOrder> curPlacedOrders = db.curPlacedOrders.ToList();
            return Request.CreateResponse(HttpStatusCode.Accepted, curPlacedOrders);
        }
    }
}
