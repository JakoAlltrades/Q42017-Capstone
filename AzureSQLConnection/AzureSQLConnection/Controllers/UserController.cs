using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AzureSQLConnection.Models;

namespace AzureSQLConnection.Controllers
{
    public class UserController : ApiController
    {

        CapstoneDBEntities db = new CapstoneDBEntities();
        PasswordHasher ph = new PasswordHasher(); 
        
        [ActionName("XAMARIN_REG")]
        // POST: api/User/XAMARING 
        [HttpPost]
        public HttpResponseMessage Xamarin_reg(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Created");
        }

        [ActionName("CheckUserName")]
        // POST: api/User/XAMARING 
        [HttpGet]
        public HttpResponseMessage CheckUserName(string username)
        {
            List<User> userNameCheck = db.Users.Where(x => x.userName == username).ToList();
            if (userNameCheck.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Accepted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Username is already in use");
            }
        }

        [ActionName("XAMARIN_Login")]
        // GET: api/User/XAMARIN_Login 
        [HttpGet]
        public HttpResponseMessage Xamarin_login(string username, string pass)
        {
            byte[] passhash;
            //get passhash methods from personal shopper app
            passhash = ph.Hash(pass);
            User user = db.
                
                Users.Where(x => x.userName == username && x.passHash == passhash).FirstOrDefault();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter valid UserName and Password");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, user);
            }
        }

    }
}
