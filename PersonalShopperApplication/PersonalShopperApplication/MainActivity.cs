using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Java.Interop;
using CapstoneModelLib.GeneratedCode;
using Toast;

namespace PersonalShopperApplication
{
    [Activity(Label = "PersonalShopperApplication", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        #region SignIn
        UserActionsDB UADB = new UserActionsDB();
        [Export("SignIn")]
        private void SignIn(View view)
        {
            EditText userName = (EditText)FindViewById(Resource.Id.Username);
            string un = userName.Text;
            EditText password = (EditText)FindViewById(Resource.Id.Password);
            string pass = password.Text;
            User curUser = UADB.SignIn(un, pass);
            if (curUser != null)
            {
                SetContentView(Resource.Layout.Home);
            }
            else
            {
                Toast.Toast toast = new Toast.Toast();
                toast.Message = "Incorrect Username or Password!";
                toast.DurationToast = Toast.Toast.ToastDuration.Medium;
                //toast.Show();
            }
        }

        [Export("CreateAccount")]
        private void CreateAccount(View view)
        {
            SetContentView(Resource.Layout.CreateAccount1);
        }
        #endregion

        #region home
        [Export("PlaceOrder")]
        private void PlaceOrder(View view)
        {
            //SetContentView(Resource.Layout.Home);
        }

        [Export("PrevOrders")]
        private void PrevOrders(View view)
        {
            //SetContentView(Resource.Layout.Home);
        }

        [Export("BecShopper")]
        private void BecShopper(View view)
        {
            //SetContentView(Resource.Layout.Home);
        }

        [Export("prevDeliv")]
        private void prevDeliv(View view)
        {
            //SetContentView(Resource.Layout.Home);
        }

        [Export("SignOut")]
        private void SignOut(View view)
        {
            //SetContentView(Resource.Layout.Home);
        }
        #endregion

        #region createAccount
        [Export("CreateAccountNext")]
        private void CreateAccountNext(View view)
        {
            SetContentView(Resource.Layout.CreateAccount2);
        }

        [Export("FinishCreation")]
        private void FinishCreation(View view)
        {
            SetContentView(Resource.Layout.Home);
        }

        #endregion
    }
}

