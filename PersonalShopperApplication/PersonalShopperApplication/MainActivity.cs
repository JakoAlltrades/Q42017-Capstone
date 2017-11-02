using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Java.Interop;
using CapstoneModelLib.GeneratedCode;
using System;
//using Toast;

namespace PersonalShopperApplication
{
    [Activity(Label = "PersonalShopperApplication", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //ssl secure https pulls
        private User tempUser;
        private BaseDB db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        #region SignIn
        UserActionsDB UADB = new UserActionsDB("mongodb://192.168.1.200/27017");
        [Export("SignIn")]
        public void SignIn(View view)
        {
            EditText userName = (EditText)FindViewById(Resource.Id.Username);
            string un = userName.Text;
            EditText password = (EditText)FindViewById(Resource.Id.Password);
            string pass = password.Text;
            User curUser = UADB.SignIn(un, pass).Result;
            if (curUser != null)
            {
                SetContentView(Resource.Layout.Home);
            }
            else
            {
                //Toast.Toast toast = new Toast.Toast();
                //toast.Message = "Incorrect Username or Password!";
                //toast.DurationToast = Toast.Toast.ToastDuration.Medium;
                //toast.Show();
            }
        }

        [Export("CreateAccount")]
        public void CreateAccount(View view)
        {
            SetContentView(Resource.Layout.CreateAccount1);
        }
        #endregion

        #region home
        [Export("PlaceOrder")]
        public void PlaceOrder(View view)
        {
            //SetContentView(Resource.Layout.Home);
        }

        [Export("PrevOrders")]
        public void PrevOrders(View view)
        {
            //SetContentView(Resource.Layout.Home);
        }

        [Export("BecShopper")]
        public void BecShopper(View view)
        {
            SetContentView(Resource.Layout.BecomeAShopper);
        }

        [Export("prevDeliv")]
        public void prevDeliv(View view)
        {
            //SetContentView(Resource.Layout.Home);
        }

        [Export("SignOut")]
        public void SignOut(View view)
        {
            SetContentView(Resource.Layout.Main);
        }
        #endregion

        #region createAccount
        [Export("CreateAccountNext")]
        public void CreateAccountNext(View view)
        {
            EditText firstName = (EditText)FindViewById(Resource.Id.creFirstName);
            string fname = firstName.Text;
            EditText lastName = (EditText)FindViewById(Resource.Id.creLastName);
            string lname = lastName.Text;
            EditText userName = (EditText)FindViewById(Resource.Id.creUsername);
            string uname = userName.Text;
            EditText pass = (EditText)FindViewById(Resource.Id.crePassword);
            string pass1 = pass.Text;
            EditText conPass = (EditText)FindViewById(Resource.Id.creConPass);
            string pass2 = conPass.Text;
            /*
             * also check if userName is in use  
             * then set variables to a global tempUser which will be cleared once the account is created
             * also store the hashPassword
             */
            if (pass1.Equals(pass2))
            {
                byte[] passhash = UADB.SaltAndHashPassword(pass1);
                tempUser = new User(2, uname, passhash, fname, lname, null);
                SetContentView(Resource.Layout.CreateAccount2);
            }
            else
            {

            }
            
        }

        [Export("FinishCreation")]
        public void FinishCreation(View view)
        {
            EditText strAdd = (EditText)FindViewById(Resource.Id.creStreetAdd);
            string streetAddress = strAdd.Text;
            EditText cty = (EditText)FindViewById(Resource.Id.creCity);
            string city = cty.Text;
            EditText zip = (EditText)FindViewById(Resource.Id.creZip);
            string zipcodest = zip.Text;
            Spinner st = (Spinner)FindViewById(Resource.Id.creStates);
            string state = st.Selected.ToString();
            //toast to make sure state is correct
            EditText apt = (EditText)FindViewById(Resource.Id.creApt);
            string apartment = apt.Text;
            /*
             * Add the new information to the database and add the user as a shopper 
             * Also make sure that none of the data is empty
             */
            if (!String.IsNullOrEmpty(streetAddress) && !String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(zipcodest) && !String.IsNullOrEmpty(state))
            {
                int apart;
                Int32.TryParse(apartment, out apart);
                int? apartme = apart;
                int zipcode;
                if (Int32.TryParse(zipcodest, out zipcode)) {
                    Address address = new Address(streetAddress, city, state, zipcode, apartme);
                    /*
                     * Change the customer class to take a user as a constructor
                     */
                    Customer newCustomer = new Customer(tempUser.userID, tempUser.Username, tempUser.passHash, tempUser.fName, tempUser.lName, address);
                    SetContentView(Resource.Layout.Home);
                }
            }
            
        }

        #endregion
    }
}

