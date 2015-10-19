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
using Utils;

using Com.Baidu.Mapapi;
using Com.Baidu.Mapapi.Map;

namespace HubsApp
{
    [Activity(Label = "æ∆µÍœÍ«È")]
    public class DetailsActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Details);
            // Create your application here
            try
            {
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

        protected MapView mapView = null;
        protected override void OnDestroy()
        {
            base.OnDestroy();
            mapView.OnDestroy();
        }

        protected override void OnResume()
        {
            base.OnResume();
            mapView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            mapView.OnPause();
        }
    }
}