using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Activity_Liftcycle
{
    [Activity(Label = "Activity_Liftcycle", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int _count = 1;

        private TextView tv;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //如果存在则恢复之前的状态
            if (bundle != null)
            {
                _count = bundle.GetInt("_count");
            }
            tv = FindViewById<TextView>(Resource.Id.textView1);
            tv.Text = _count.ToString();

            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Click += (sender, args) =>
            {
                tv.Text = string.Format("{0}", _count++);
            };
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("_count", _count);
        }
    }
}

