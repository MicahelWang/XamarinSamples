using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Com.Baidu.Mapapi;
using Hubs1.Core.ViewModels;

namespace Hubs1.Droid.Views
{
    [Activity(Label = "附近的酒店")]
    public class HotelListView : MvxActivity
    {
        public new HotelListViewModel ViewModel
        {
            get { return (HotelListViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.HotelListView);
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SDKInitializer.Initialize(ApplicationContext);
        }
    }
}