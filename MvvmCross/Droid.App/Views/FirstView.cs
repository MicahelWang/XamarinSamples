
using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace Droid.App.Views
{
    [Activity(Label = "View For FirstViewModel")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FirstView);
        }
    }
}