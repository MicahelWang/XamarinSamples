using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HubsApp
{
    [Activity(Label = "PayLayout")]
    public class PayLayout : Activity
    {
        private Button _btnAlipay, _btnTecentPay, _btnCancel;

        private LinearLayout _layout;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.PayLayout);
            _btnAlipay = FindViewById<Button>(Resource.Id.btn_Alipay);
            _btnAlipay = FindViewById<Button>(Resource.Id.btn_TecentPay);
            _btnAlipay = FindViewById<Button>(Resource.Id.btn_Cancel);


            _layout = FindViewById<LinearLayout>(Resource.Id.pop_layout);
            // Create your application here
        }
    }
}