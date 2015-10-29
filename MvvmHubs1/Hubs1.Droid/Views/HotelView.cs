using System.Collections.Generic;
using Android.App;
using Cirrious.MvvmCross.Droid.Views;
using Hubs1.Core.ViewModels;

namespace Hubs1.Droid.Views
{
    [Activity(Label = "酒店信息")]
    public class HotelView : MvxActivity
    {
        public new HotelViewModel ViewModel
        {
            get { return (HotelViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.HotelView);
            var list = new List<string>();
        }
    }
}