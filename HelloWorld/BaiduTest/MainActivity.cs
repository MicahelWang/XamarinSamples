using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Baidu.Mapapi.Map;
using Com.Baidu.Mapapi;

namespace BaiduTest
{
    [Activity(Label = "BaiduTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SDKInitializer.Initialize(ApplicationContext);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mMapView = FindViewById<MapView>(Resource.Id.bmapView);

        }

        protected MapView mMapView = null;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mMapView.OnDestroy();
        }

        protected override void OnResume()
        {
            base.OnResume();
            mMapView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            mMapView.OnPause();
        }
    }
}

