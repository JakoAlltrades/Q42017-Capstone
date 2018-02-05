using AzureSQLConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureSQLConnection.Controllers
{
    public class CompletedOrderController : ApiController
    {
        CapstoneDBEntities db = new CapstoneDBEntities();
        [ActionName("FinishOrder")]
        // POST: api/User/XAMARING 
        [HttpPost]
        public HttpResponseMessage FinishOrder(completedOrder order)
        {
            db.completedOrders.Add(order);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Order Finished");
        }

        [ActionName("GetPastOrders")]
        // POST: api/User/XAMARING 
        [HttpGet]
        public HttpResponseMessage GrabPastOrders(int userId)
        {
            List<completedOrder> pastOrders = db.completedOrders.Where(x => x.customerID == userId).ToList();
            if (pastOrders.Count != 0)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, pastOrders);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "No previous orders for the UserID:[" + userId + "]");
            }
        }

        [ActionName("GetPastDeliveries")]
        // POST: api/User/XAMARING 
        [HttpGet]
        public HttpResponseMessage GrabPastDeliveries(int shopperId)
        {
            List<completedOrder> pastDeliveries = db.completedOrders.Where(x => x.shopperID == shopperId).ToList();
            if (pastDeliveries.Count != 0)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, pastDeliveries);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "No previous deliveries for the ShopperID:[" + shopperId + "]");
            }
        }
    }
}
