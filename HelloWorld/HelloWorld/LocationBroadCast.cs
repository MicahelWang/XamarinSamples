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
using Android.Locations;

namespace HelloWorld
{
    [BroadcastReceiver()]
    public class LocationBroadCast : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Location lc = (Location)intent.Extras.Get(LocationManager.KeyLocationChanged);
            Toast.MakeText(context, string.Format( "Lon = {0}, Lat = {1}", lc.Longitude, lc.Latitude), ToastLength.Short).Show();
        }
    }
}