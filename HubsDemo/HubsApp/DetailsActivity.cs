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
using Utils;


namespace HubsApp
{
    [Activity(Label = "酒店详情")]
    public class DetailsActivity : Activity
    {

        protected MapView _mapView = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Details);
            // Create your application here
            try
            {
                _mapView = FindViewById<MapView>(Resource.Id.bmapView);
                //var hotelEntity =(HotelEntity) Intent.GetSerializableExtra("HOTELENTITY");
                var name = Intent.GetStringExtra("Name");
                var latitude = Intent.GetDoubleExtra("Latitude", 0.00);
                var longitude = Intent.GetDoubleExtra("Longitude", 0.00);
                var hotelEntity = new HotelEntity() { Name = name, Latitude = latitude, Longitude = longitude };

                var txtHotelName = FindViewById<TextView>(Resource.Id.txtHotelName);
                var txtHotelCoordinate = FindViewById<TextView>(Resource.Id.txtHotelCoordinate);
                txtHotelName.Text = hotelEntity.Name;
                txtHotelCoordinate.Text = string.Format("({0}, {1})", hotelEntity.Longitude.ToString("0.0000"), hotelEntity.Latitude.ToString("0.0000"));
            }
            catch (Exception ex)
            {

            }
        }

       
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _mapView.OnDestroy();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _mapView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _mapView.OnPause();
        }
    }
}