using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Telephony;
using Android.Util;

namespace Pagina1.Droid
{
    [BroadcastReceiver(Enabled = true, Label = "SMS Receiver")]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class SmsReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == "android.provider.Telephony.SMS_RECEIVED")
            {
                Bundle bundle = intent.Extras;
                if (bundle != null)
                {
                    Java.Lang.Object[] pdus = (Java.Lang.Object[])bundle.Get("pdus");
                    SmsMessage[] messages = new SmsMessage[pdus.Length];
                    for (int i = 0; i < pdus.Length; i++)
                    {
                        messages[i] = SmsMessage.CreateFromPdu((byte[])pdus[i], bundle.GetString("format"));
                        string messageBody = messages[i].MessageBody;
                        string sender = messages[i].OriginatingAddress;

                        // Process the SMS message here
                        Log.Debug("SmsReceiver", $"Received SMS from {sender}: {messageBody}");

                        // Extract and process location data from the messageBody
                        // Assuming messageBody contains coordinates in a specific format
                    }
                }
            }
        }
    }
}