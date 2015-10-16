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

namespace GetCurrentHotel
{
    [Activity(Label = "¾ÆµêÃ÷Ï¸")]
    public class HotelDetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.HotelDetail);
            // Create your application here

            var hotelName = Intent.Extras.GetString("SelectedHotelName");
            var hotelLon = Intent.Extras.GetDouble("SelectedHotelLon");
            var hotelLat = Intent.Extras.GetDouble("SelectedHotelLat");

            TextView txtHotelName = FindViewById<TextView>(Resource.Id.txtHotelName);
            TextView txtHotelCoordinate = FindViewById<TextView>(Resource.Id.txtHotelCoordinate);
            txtHotelName.Text = hotelName;
            txtHotelCoordinate.Text = string.Format("({0}, {1})", hotelLon.ToString("0.0000"), hotelLat.ToString("0.0000"));
        }
    }
}