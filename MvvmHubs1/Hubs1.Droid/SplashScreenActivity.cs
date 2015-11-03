using Android.App;
using Cirrious.MvvmCross.Droid.Views;

namespace Hubs1.Droid
{
    [Activity(Label = "汇通预定", MainLauncher = true, NoHistory = true, Icon = "@drawable/icon")]
    public class SplashScreenActivity
       : MvxSplashScreenActivity
    {
        public SplashScreenActivity()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}

