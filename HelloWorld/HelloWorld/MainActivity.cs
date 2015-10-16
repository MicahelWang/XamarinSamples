using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;

namespace HelloWorld
{
    [Activity(Label = "HelloWorld", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            EditText txtPhoneNumber = FindViewById<EditText>(Resource.Id.txtPhoneNumber);
            Button btnTranslate = FindViewById<Button>(Resource.Id.btnTranslate);
            Button btnCall = FindViewById<Button>(Resource.Id.btnCall);
            Button btnGetLocation = FindViewById<Button>(Resource.Id.btnLocation);

            btnCall.Enabled = false;

            string translatedNumber = string.Empty;
            btnTranslate.Click += (object o, EventArgs e)=> {
                translatedNumber = PhoneTranslator.ToNumber(txtPhoneNumber.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    btnCall.Text = "Call";
                    btnCall.Enabled = false;
                }
                else
                {
                    btnCall.Text = string.Format("Call {0}", translatedNumber);
                    btnCall.Enabled = true;
                }
            };

            btnCall.Click += (object o, EventArgs e) => {
                //初始化对话框
                var callDialog = new AlertDialog.Builder(this);

                //对话框内容
                callDialog.SetMessage(string.Format("Call {0}?", translatedNumber));

                callDialog.SetNeutralButton("Call", delegate {
                    var callIntent = new Intent(Intent.ActionCall);

                    callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));

                    StartActivity(callIntent);
                });
                callDialog.SetNegativeButton("Cancel", delegate { });

                callDialog.Show();
            };

            btnGetLocation.Click += (o, e) => {  
                // Get Location
                LocationManager lm = (LocationManager)GetSystemService(LocationService);
                var tent = new Intent(this, typeof(LocationBroadCast));
                var ptent = PendingIntent.GetBroadcast(this, 0, tent, PendingIntentFlags.UpdateCurrent);
                lm.RequestLocationUpdates(GetBestLocationProvider(lm), 5000, 100, ptent);
            };
        }

        private string GetBestLocationProvider(LocationManager lm)
        {
            Criteria cri = new Criteria();
            cri.Accuracy = Accuracy.Coarse;
            cri.PowerRequirement = Power.Low;
            cri.AltitudeRequired = false;
            cri.BearingAccuracy = Accuracy.Low;
            cri.CostAllowed = false;
            cri.HorizontalAccuracy = Accuracy.Low;
            cri.SpeedAccuracy = Accuracy.Low;
            cri.SpeedRequired = false;
            cri.VerticalAccuracy = Accuracy.Low;
            string pidStr = lm.GetBestProvider(cri, true);
            return pidStr;
        }

        
    }
}

