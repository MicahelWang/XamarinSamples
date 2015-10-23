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
using Java.Lang;

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
            _btnTecentPay = FindViewById<Button>(Resource.Id.btn_TecentPay);
            _btnCancel = FindViewById<Button>(Resource.Id.btn_Cancel);


            _layout = FindViewById<LinearLayout>(Resource.Id.pop_layout);
            // Create your application here

            _layout.Click += delegate
            {
                Toast.MakeText(ApplicationContext,
                                "提示：点击窗口外部关闭窗口！",
                                ToastLength.Short);
            };
            _btnAlipay.Click += (sender, args) =>
            {
                Finish();
            };
            _btnTecentPay.Click += (sender, args) =>
            {
                Finish();
            };
            _btnCancel.Click += (sender, args) =>
            {
                Finish();
            };


        }

        
        public override bool OnTouchEvent(MotionEvent e)
        {
            Finish();
            return true;
        }
    }
}