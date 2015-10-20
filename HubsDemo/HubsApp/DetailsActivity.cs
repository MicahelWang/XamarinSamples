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
using Utils;


namespace HubsApp
{
    [Activity(Label = "酒店详情")]
    public class DetailsActivity : Activity
    {
        private const string Tag = "DetailsActivity";

        private MapView _mMapView = null;
        private BaiduMap _mBaiduMap;
        private Marker _targetMarker;
        private BitmapDescriptor icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.icon_marka);
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
                var hotelEntity = new HotelEntity() { Name = name, Latitude = latitude, Longitude = longitude };

                txtHotelName.Text = hotelEntity.Name;
                txtHotelCoordinate.Text = string.Format("({0}, {1})", hotelEntity.Longitude.ToString("0.0000"), hotelEntity.Latitude.ToString("0.0000"));
                var mBaidumap = _mMapView.Map;
                LatLng point = new LatLng(hotelEntity.Latitude, hotelEntity.Longitude);
                #endregion 数据初始化


                _mBaiduMap = _mMapView.Map;
                var position = new LatLng(hotelEntity.Latitude, hotelEntity.Longitude);
                

                #region 设置居中
                var msu = new MapStatus.Builder().Target(position).Zoom(14.00f).Build();

                MapStatusUpdate mMapStatusUpdate = MapStatusUpdateFactory.NewMapStatus(msu);
                //改变地图状态
                mBaidumap.SetMapStatus(mMapStatusUpdate);
                #endregion

                #region 标记酒店位置

                InitOverlay(position);

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


        private void InitOverlay(LatLng target)
        {
          
            
            OverlayOptions overlayOptions = new MarkerOptions().InvokeIcon(icon).InvokePosition(target).InvokeZIndex(9);
            _targetMarker = (Marker) (_mBaiduMap.AddOverlay(overlayOptions));
        }

        protected override void OnDestroy()
        {
            _mMapView.OnDestroy();
            base.OnDestroy();
            icon.Recycle();
            
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