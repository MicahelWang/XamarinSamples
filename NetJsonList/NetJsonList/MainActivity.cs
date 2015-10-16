using System;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace NetJsonList
{
    [Activity(Label = "NetJsonList", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView _tv;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            _tv = FindViewById<TextView>(Resource.Id.textView1);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            LoadXamarin();
        }

        public void LoadXamarin()
        {
            const string url = "http://www.xamarin-cn.com/test.json";

            var httpRequest = (HttpWebRequest) HttpWebRequest.Create(new Uri(url));

            var httpResponse = (HttpWebResponse) httpRequest.GetResponse();
            var text = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();
            _tv.Text = text;

        }
    }
}

