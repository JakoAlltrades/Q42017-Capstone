using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Math;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.PayPal.Android;
using Org.Json;

namespace PersonalShopperApp.Models
{

    /*
     * Class copied from Paypal test from the Xamarin.PayPal forms Nuget package
     * 
     * https://github.com/AlejandroRuiz/PayPal.Forms
     */
    public class PayPalManager
    {
        Context Context;

        /**
     * - Set to PayPalConfiguration.ENVIRONMENT_PRODUCTION to move real money.
     * 
     * - Set to PayPalConfiguration.ENVIRONMENT_SANDBOX to use your test credentials
     * from https://developer.paypal.com
     * 
     * - Set to PayPalConfiguration.ENVIRONMENT_NO_NETWORK to kick the tires
     * without communicating to PayPal's servers.
     */
        private static String CONFIG_ENVIRONMENT = PayPalConfiguration.EnvironmentNoNetwork;

        // note that these credentials will differ between live & sandbox environments.
        private static String CONFIG_CLIENT_ID = "access_token$sandbox$dkcsytxfdf7qrf35$58ae56ec8dc8a2598d81594a49664244";

        public static int REQUEST_CODE_PAYMENT = 1;
        public static int REQUEST_CODE_FUTURE_PAYMENT = 2;
        public static int REQUEST_CODE_PROFILE_SHARING = 3;

        private PayPalConfiguration config;

        public PayPalManager(Context context)
        {
            Context = context;
            config = new PayPalConfiguration()
                .Environment(CONFIG_ENVIRONMENT)
                .ClientId(CONFIG_CLIENT_ID)
                // The following are only used in PayPalFuturePaymentActivity.
                .MerchantName("Shop-Lyft")
                .MerchantPrivacyPolicyUri(Android.Net.Uri.Parse("https://www.example.com/privacy"))
                .MerchantUserAgreementUri(Android.Net.Uri.Parse("https://www.example.com/legal"));

            Intent intent = new Intent(Context, typeof(PayPalService));
            intent.PutExtra(PayPalService.ExtraPaypalConfiguration, config);
            Context.StartService(intent);
        }

        public PayPalPayment getThingToBuy(string paymentIntent, double price, string itemName)
        {
            return new PayPalPayment(new BigDecimal(price), "USD", itemName,
                paymentIntent);
        }

        private void sendAuthorizationToServer(PayPalAuthorization authorization)
        {

            /**
         * TODO: Send the authorization response to your server, where it can
         * exchange the authorization code for OAuth access and refresh tokens.
         * 
         * Your server must then store these tokens, so that your server code
         * can execute payments for this user in the future.
         * 
         * A more complete example that includes the required app-server to
         * PayPal-server integration is available from
         * https://github.com/paypal/rest-api-sdk-python/tree/master/samples/mobile_backend
         */

        }

        public void BuySomething(PayPalPayment item)
        {
            /* 
         * PAYMENT_INTENT_SALE will cause the payment to complete immediately.
         * Change PAYMENT_INTENT_SALE to 
         *   - PAYMENT_INTENT_AUTHORIZE to only authorize payment and capture funds later.
         *   - PAYMENT_INTENT_ORDER to create a payment for authorization and capture
         *     later via calls from your server.
         * 
         * Also, to include additional payment details and an item list, see getStuffToBuy() below.
         */
            //PayPalPayment thingToBuy = getThingToBuy(PayPalPayment.PaymentIntentSale);

            /*
         * See getStuffToBuy(..) for examples of some available payment options.
         */

            Intent intent = new Intent(Context, typeof(PaymentActivity));

            // send the same configuration for restart resiliency
            intent.PutExtra(PayPalService.ExtraPaypalConfiguration, config);

            intent.PutExtra(PaymentActivity.ExtraPayment, item);

            (Context as Activity).StartActivityForResult(intent, REQUEST_CODE_PAYMENT);
        }

        public void Destroy()
        {
            Context.StopService(new Intent(Context, typeof(PayPalService)));
        }

        public void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            if (requestCode == PayPalManager.REQUEST_CODE_PAYMENT)
            {
                if (resultCode == Result.Ok)
                {
                    PaymentConfirmation confirm =
                        (PaymentConfirmation)data.GetParcelableExtra(PaymentActivity.ExtraResultConfirmation);
                    if (confirm != null)
                    {
                        try
                        {
                            System.Diagnostics.Debug.WriteLine(confirm.ToJSONObject().ToString(4));
                            System.Diagnostics.Debug.WriteLine(confirm.Payment.ToJSONObject().ToString(4));
                            /**
                         	*  TODO: send 'confirm' (and possibly confirm.getPayment() to your server for verification
                         	* or consent completion.
                         	* See https://developer.paypal.com/webapps/developer/docs/integration/mobile/verify-mobile-payment/
                         	* for more details.
                         	*
                         	* For sample mobile backend interactions, see
                         	* https://github.com/paypal/rest-api-sdk-python/tree/master/samples/mobile_backend
                         	*/
                            Toast.MakeText(
                                Context.ApplicationContext,
                                "PaymentConfirmation info received from PayPal", ToastLength.Short)
                                .Show();

                        }
                        catch (JSONException e)
                        {
                            System.Diagnostics.Debug.WriteLine("an extremely unlikely failure occurred: " + e.Message);
                        }
                    }
                }
                else if (resultCode == Result.Canceled)
                {
                    System.Diagnostics.Debug.WriteLine("The user canceled.");
                }
                else if ((int)resultCode == PaymentActivity.ResultExtrasInvalid)
                {
                    System.Diagnostics.Debug.WriteLine(
                        "An invalid Payment or PayPalConfiguration was submitted. Please see the docs.");
                }
            }
            else if (requestCode == REQUEST_CODE_FUTURE_PAYMENT)
            {
                if (resultCode == Result.Ok)
                {
                    PayPalAuthorization auth = (Xamarin.PayPal.Android.PayPalAuthorization)data.GetParcelableExtra(PayPalFuturePaymentActivity.ExtraResultAuthorization);
                    if (auth != null)
                    {
                        try
                        {
                            System.Diagnostics.Debug.WriteLine(auth.ToJSONObject().ToString(4));

                            String authorization_code = auth.AuthorizationCode;
                            System.Diagnostics.Debug.WriteLine(authorization_code);

                            sendAuthorizationToServer(auth);
                            Toast.MakeText(
                                Context.ApplicationContext,
                                "Future Payment code received from PayPal", ToastLength.Long)
                                .Show();

                        }
                        catch (JSONException e)
                        {
                            System.Diagnostics.Debug.WriteLine("an extremely unlikely failure occurred: " + e.Message);
                        }
                    }
                }
                else if (resultCode == Result.Ok)
                {
                    System.Diagnostics.Debug.WriteLine("The user canceled.");
                }
                else if ((int)resultCode == PayPalFuturePaymentActivity.ResultExtrasInvalid)
                {
                    System.Diagnostics.Debug.WriteLine(
                        "Probably the attempt to previously start the PayPalService had an invalid PayPalConfiguration. Please see the docs.");
                }
            }
            else if (requestCode == REQUEST_CODE_PROFILE_SHARING)
            {
                if (resultCode == Result.Ok)
                {
                    PayPalAuthorization auth = (Xamarin.PayPal.Android.PayPalAuthorization)data.GetParcelableExtra(PayPalProfileSharingActivity.ExtraResultAuthorization);
                    if (auth != null)
                    {
                        try
                        {
                            System.Diagnostics.Debug.WriteLine(auth.ToJSONObject().ToString(4));

                            String authorization_code = auth.AuthorizationCode;
                            System.Diagnostics.Debug.WriteLine(authorization_code);

                            sendAuthorizationToServer(auth);
                            Toast.MakeText(
                                Context.ApplicationContext,
                                "Profile Sharing code received from PayPal", ToastLength.Short)
                                .Show();

                        }
                        catch (JSONException e)
                        {
                            System.Diagnostics.Debug.WriteLine("an extremely unlikely failure occurred: " + e.Message);
                        }
                    }
                }
                else if (resultCode == Result.Canceled)
                {
                    System.Diagnostics.Debug.WriteLine("The user canceled.");
                }
                else if ((int)resultCode == PayPalFuturePaymentActivity.ResultExtrasInvalid)
                {
                    System.Diagnostics.Debug.WriteLine(
                        "Probably the attempt to previously start the PayPalService had an invalid PayPalConfiguration. Please see the docs.");
                }
            }
        }
    }
}