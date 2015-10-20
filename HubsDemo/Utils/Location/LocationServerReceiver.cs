using Android.Content;
using Android.Util;

namespace Utils.Location
{
    public class LocationServerReceiver : BroadcastReceiver
    {
        private const string Tag = "LocaltionReceiver";
        //Service Action
        public const string LocaltionReceiver = "com.Hubs1.app.Location.LocaltionReceiver";

        public override void OnReceive(Context context, Intent intent)
        {
            Log.Info(Tag, "LocaltionReceiver booting-------------------------");
            Intent _intent = new Intent(context, typeof(LocationService));
            if (Intent.ActionBootCompleted.Equals(intent.Action))
            {
                Log.Debug(Tag, "Boot Completed");

                _intent.PutExtra("startingModel", 1);
            }
            else
            {
                _intent.PutExtra("startingModel", 2);
            }

            context.StartService(_intent);
        }
    }
}