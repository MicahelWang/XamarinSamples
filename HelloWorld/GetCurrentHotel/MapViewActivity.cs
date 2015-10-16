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
using Com.Baidu.Mapapi.Map;


namespace GetCurrentHotel
{
    [Activity(Label = "MapViewActivity", Theme = "@android:style/Theme.Holo.Light")]
    public class MapViewActivity : Activity
    {
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            
            // Create your application here
            SetContentView(Resource.Layout.BaiduMapView);
            mMapView = FindViewById<MapView>(Resource.Id.bmapView);


        }
    }
}