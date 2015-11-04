using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Com.Baidu.Mapapi.Map;
using Com.Baidu.Mapapi.Model;
using Hubs1.Core.ViewModels;

namespace Hubs1.Droid.Views
{
    [Activity(Label = "酒店信息")]
    public class HotelView : MvxActivity, IMenuItemClickListener
    {
        private readonly BitmapDescriptor _hotelBitmap = BitmapDescriptorFactory.FromResource(Resource.Drawable.dot);
        private readonly BitmapDescriptor _localtionBitmap = BitmapDescriptorFactory.FromResource(Resource.Drawable.location);

        public new HotelViewModel ViewModel
        {
            get { return (HotelViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.HotelView);
            //InitOverlay();
            Button btn6 = FindViewById<Button>(Resource.Id.btnIOS6);
            Button btn7 = FindViewById<Button>(Resource.Id.btnIOS7);

            btn6.Click += delegate
            {
                SetTheme(Resource.Style.ActionSheetStyleIOS6);
                ShowActionSheet();
            };
            btn7.Click += delegate
            {
                SetTheme(Resource.Style.ActionSheetStyleIOS7);
                ShowActionSheet();
            };
        }

      
        private void InitOverlay()
        {
            var map = FindViewById<MapView>(Resource.Id.bmapView);
            var mBaiduMap = map.Map;
            //位置
            LatLng hotelLatLng = new LatLng(ViewModel.HotelData.Latitude, ViewModel.HotelData.Longitude);
            OverlayOptions hotelOverlayOptions = new MarkerOptions()
                .InvokeIcon(_hotelBitmap)
                .InvokePosition(hotelLatLng)
                .InvokeZIndex(9);
            mBaiduMap.AddOverlay(hotelOverlayOptions).JavaCast<Marker>();

            #region 当前位置




            //LatLng locationLatLng = new LatLng(CurrentData.Latitude, CurrentData.Longitude);
            //OverlayOptions locationOverlayOptions = new MarkerOptions()
            //    .InvokeIcon(_localtionBitmap)
            //    .InvokePosition(locationLatLng)
            //    .InvokeZIndex(9);
            //Marker locationMarker = _mBaiduMap.AddOverlay(locationOverlayOptions).JavaCast<Marker>(); ;

            //设置居中
            var mMapStatusUpdate = MapStatusUpdateFactory.NewLatLng(hotelLatLng);
            //改变地图状态
            mBaiduMap.SetMapStatus(mMapStatusUpdate);

            #endregion 当前位置


        }

        public void ShowActionSheet()
        {
            var menuView = new ActionSheet(this);
            menuView.SetCancelButtonTitle("取消");// before add items
            menuView.AddItems(new []{ "支付宝", "微信", "财付通", "银联" });
            menuView.SetItemClickListener(this);
            menuView.SetCancelableOnTouchMenuOutside(true);
            menuView.ShowMenu();
        }

     
        public void OnItemClick(int itemPosition)
        {
            Toast.MakeText(this, (itemPosition + 1) + " click", 0).Show();
        }
    }
}