using AzureSQLConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureSQLConnection.Controllers
{
    public class CurOrderController : ApiController
    {
        CapstoneDBEntities db = new CapstoneDBEntities();
        [ActionName("CreateOrder")]
        // POST: api/User/XAMARING 
        [HttpPost]
        public HttpResponseMessage CreateOrder(curPlacedOrder orderToCreate)
        {
            db.curPlacedOrders.Add(orderToCreate);
            try
            {
               db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "Order Created");
        }

        [ActionName("DeleteOrder")]
        // POST: api/User/XAMARING 
        [HttpPost]
        public HttpResponseMessage DeleteOrder(int orderID)
        {
            curPlacedOrder curOrder = db.curPlacedOrders.Where(x => x.orderID == orderID).FirstOrDefault();
            db.curPlacedOrders.Remove(curOrder);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Order Erased");
        }

        [ActionName("GrabOrder")]
        // POST: api/User/XAMARING 
        [HttpGet]
        public HttpResponseMessage GrabOrder(int orderId)
        {
            curPlacedOrder order = db.curPlacedOrders.Where(x => x.orderID == orderId).FirstOrDefault();
            if (order != null)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, order);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "No order for the orderID:[" + orderId + "]");
            }
        }


    }
}
