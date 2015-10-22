using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Com.Baidu.Mapapi.Map;
using Com.Baidu.Mapapi.Model;
using HubsApp.Utils;
using Utils;


namespace HubsApp
{
    [Activity(Label = "酒店详情")]
    public class DetailsActivity : Activity
    {
        private const string Tag = "DetailsActivity";

        private MapView _mMapView = null;
        private BaiduMap _mBaiduMap;
        private Marker _hotelMarker,_locationMarker;
        private readonly BitmapDescriptor _hotelBitmap = BitmapDescriptorFactory.FromResource(Resource.Drawable.dot);
        private readonly BitmapDescriptor _localtionBitmap = BitmapDescriptorFactory.FromResource(Resource.Drawable.map_location);

        private HotelEntity _hotelEntity = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Details);
            // Create your application here
            try
            {
                _mMapView = FindViewById<MapView>(Resource.Id.bmapView);
                var txtHotelName = FindViewById<TextView>(Resource.Id.txtHotelName);
                var txtHotelCoordinate = FindViewById<TextView>(Resource.Id.txtHotelCoordinate);


                #region 数据初始化

                //var hotelEntity =(HotelEntity) Intent.GetSerializableExtra("HOTELENTITY");
                var name = Intent.GetStringExtra("Name");
                var latitude = Intent.GetDoubleExtra("Latitude", 0.00);
                var longitude = Intent.GetDoubleExtra("Longitude", 0.00);
                _hotelEntity = new HotelEntity() { Name = name, Latitude = latitude, Longitude = longitude };

                txtHotelName.Text = _hotelEntity.Name;
                txtHotelCoordinate.Text = string.Format("({0}, {1})", _hotelEntity.Longitude.ToString("0.0000"), _hotelEntity.Latitude.ToString("0.0000"));

                #endregion 数据初始化

                var mBaidumap = _mMapView.Map;
                _mBaiduMap = _mMapView.Map;
                var locationPoint = new LatLng(CurrentData.Latitude, CurrentData.Longitude);


                #region 设置居中
                var msu = new MapStatus.Builder().Target(locationPoint).Zoom(14.00f).Build();

                MapStatusUpdate mMapStatusUpdate = MapStatusUpdateFactory.NewMapStatus(msu);
                //改变地图状态
                mBaidumap.SetMapStatus(mMapStatusUpdate);
                #endregion

                #region 标记酒店位置

                InitOverlay();

                #endregion 标记酒店位置

                #region 添加事件
                //_mBaiduMap.SetOnMarkerClickListener(new Com.Baidu.Mapapi.Map.BaiduMap.);
                #endregion


            }
            catch (Exception ex)
            {
                Log.Error(Tag, ex.StackTrace);
            }
        }


        private void InitOverlay()
        {

            LatLng hotelLatLng = new LatLng(_hotelEntity.Latitude, _hotelEntity.Longitude);
            OverlayOptions hotelOverlayOptions = new MarkerOptions()
                .InvokeIcon(_hotelBitmap)
                .InvokePosition(hotelLatLng)
                .InvokeZIndex(9);

            Overlay hotelOverlay =_mBaiduMap.AddOverlay(hotelOverlayOptions);
            //_hotelMarker = (Marker)(_mBaiduMap.AddOverlay(hotelOverlayOptions));

            LatLng locationLatLng = new LatLng(CurrentData.Latitude, CurrentData.Longitude);
            OverlayOptions locationOverlayOptions = new MarkerOptions()
                .InvokeIcon(_localtionBitmap)
                .InvokePosition(locationLatLng)
                .InvokeZIndex(9);

            Overlay locationOverlay=_mBaiduMap.AddOverlay(locationOverlayOptions);
            //_locationMarker = (Marker)(_mBaiduMap.AddOverlay(locationOverlayOptions));

          

        }

        protected override void OnDestroy()
        {
            _mMapView.OnDestroy();
            base.OnDestroy();
            _hotelBitmap.Recycle();
            _localtionBitmap.Recycle();

        }

        protected override void OnResume()
        {
            base.OnResume();
            _mMapView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _mMapView.OnPause();
        }


    }
}