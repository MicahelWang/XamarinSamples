using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Phoneword
{
    [Activity(Label = "Phoneword", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
      static readonly  List<string> PhoneNumbers=new List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            var phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var translateButton = FindViewById<Button>(Resource.Id.TransllateButton);
            var callButton = FindViewById<Button>(Resource.Id.CallButton);
            var callHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);
            callButton.Enabled = false;


            var translatedNumber = string.Empty;
            translateButton.Click += (sender, args) =>
            {
                translatedNumber = PhoneTranslator.ToNumber(phoneNumberText.Text);
                if (String.IsNullOrWhiteSpace(translatedNumber))
                {
                    callButton.Text = "Call";
                    callButton.Enabled = false;
                }
                else
                {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;
                }
            };

            callButton.Click += delegate
            {
                var callDialog = new AlertDialog.Builder(this);

                callDialog.SetMessage("Call " + translatedNumber);

                callDialog.SetNeutralButton("Call", delegate
                {
                    // 将电话加入到历史记录列表中
                    PhoneNumbers.Add(translatedNumber);
                    callHistoryButton.Enabled = true;

                    var callIntent = new Intent(Intent.ActionCall);

                    callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));

                    StartActivity(callIntent);
                });

                callDialog.SetNegativeButton("Cancel", delegate { });

                callDialog.Show();
            };

            callHistoryButton.Click += delegate
            {

                var intent = new Intent(this, typeof (CallHistoryActivity));

                intent.PutStringArrayListExtra("phone_numbers", PhoneNumbers);
                StartActivity(intent);
            };
        }
    }
}

