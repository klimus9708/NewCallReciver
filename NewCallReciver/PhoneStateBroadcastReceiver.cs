using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace NewCallReciver
{
    [BroadcastReceiver]
    [IntentFilter(new[] { "android.intent.action.PHONE_STATE" })]
    public partial class PhoneStateBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Extras != null)
            {
                string state = intent.GetStringExtra(TelephonyManager.ExtraState);
                string telephone = intent.GetStringExtra(TelephonyManager.ExtraIncomingNumber);
                if (state == TelephonyManager.ExtraStateRinging)
                {
                    
                    if (!string.IsNullOrEmpty(telephone))
                    {
                        Toast.MakeText(context,"Incoming call from " + telephone + ".",ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(context, "Incoming call from unknow number.", ToastLength.Short).Show();

                    }
                    Intent buttonDown = new Intent(Intent.ActionMediaButton);
                    buttonDown.PutExtra(Intent.ExtraKeyEvent, new KeyEvent(KeyEventActions.Up, Keycode.Headsethook));
                    context.SendOrderedBroadcast(buttonDown, "android.permission.CALL_PRIVILEGED");
                    
                }
                else if (state == TelephonyManager.ExtraStateOffhook)
                {
                    Toast.MakeText(context, "Incoming call answered.", ToastLength.Short).Show();
                }
                else if (state == TelephonyManager.ExtraStateIdle)
                {
                    Toast.MakeText(context, "Incoming call ended " + telephone, ToastLength.Short).Show();
                }
            }
        }
        //private void showNotification(Context context)
        //{
        //    PendingIntent contentIntent = PendingIntent.GetActivity(context, 0,
        //       // new Intent(context, ,0));
        //}
    }
}