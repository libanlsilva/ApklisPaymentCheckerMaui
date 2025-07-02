using ApklisPaymentCheckerMaui.Interfaces;
using AndroidNet = Android.Net;

namespace ApklisPaymentCheckerMaui.Platforms.Android
{
    public class ApklisPaymentChecker: IApklisPaymentChecker
    {
        private readonly string APKLIS_PROVIDER = "content://cu.uci.android.apklis.PaymentProvider/app/";
        private readonly string APKLIS_PAID = "paid";
        private readonly string APKLIS_USER_NAME = "user_name";

        public ApklisPaymentChecker()
        {

        }
        
        public (bool IsPaid, string UserName) GetPaymentInfo(string packageName)
        {
            bool paid = false;
            string? username = null;
            
            try
            {
                var providerUri = AndroidNet.Uri.Parse($"{APKLIS_PROVIDER}{packageName}");
                if (providerUri == null)
                {
                    return (false, string.Empty);
                }
                
                // Usar Platform.CurrentActivity para obtener el contexto correcto en MAUI
                var activity = Platform.CurrentActivity;
                if (activity?.ApplicationContext == null)
                {
                    return (false, string.Empty);
                }

                var context = activity.ApplicationContext;
                var contentResolver = context.ContentResolver;
                if (contentResolver == null)
                {
                    return (false, string.Empty);
                }
                
                using (var apklisdb = contentResolver.Query(providerUri, null, null, null, null))
                {
                    if (apklisdb != null && apklisdb.MoveToFirst())
                    {
                        var paidColumnIndex = apklisdb.GetColumnIndex(APKLIS_PAID);
                        var userNameColumnIndex = apklisdb.GetColumnIndex(APKLIS_USER_NAME);
                        
                        if (paidColumnIndex >= 0)
                        {
                            paid = apklisdb.GetInt(paidColumnIndex) > 0;
                        }
                        
                        if (userNameColumnIndex >= 0)
                        {
                            username = apklisdb.GetString(userNameColumnIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log del error si es necesario
                System.Diagnostics.Debug.WriteLine($"Error checking Apklis payment: {ex.Message}");
                return (false, string.Empty);
            }

            return (paid, username ?? string.Empty);
        }
    }
}