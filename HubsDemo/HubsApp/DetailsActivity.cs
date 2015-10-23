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
        private readonly BitmapDescriptor _hotelBitmap = BitmapDescriptorFactory.FromResource(Resource.Drawable.dot);
        private readonly BitmapDescriptor _localtionBitmap = BitmapDescriptorFactory.FromResource(Resource.Drawable.map_location);

        private Button _btnReserve;

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
                _btnReserve = FindViewById<Button>(Resource.Id.btnReserve);


                #region 数据初始化

                //var hotelEntity =(HotelEntity) Intent.GetSerializableExtra("HOTELENTITY");
                var name = Intent.GetStringExtra("Name");
                var latitude = Intent.GetDoubleExtra("Latitude", 0.00);
                var longitude = Intent.GetDoubleExtra("Longitude", 0.00);
                _hotelEntity = new HotelEntity() { Name = name, Latitude = latitude, Longitude = longitude };

                txtHotelName.Text = _hotelEntity.Name;
                txtHotelCoordinate.Text = string.Format("({0}, {1})", _hotelEntity.Longitude.ToString("0.0000"), _hotelEntity.Latitude.ToString("0.0000"));

                #endregion 数据初始化

                _mBaiduMap = _mMapView.Map;
                #region 标记酒店位置

                InitOverlay();

                #endregion 标记酒店位置

                #region 添加事件
                _mBaiduMap.SetOnMarkerClickListener(new OnMarkerClickListener(this));
                _mBaiduMap.SetOnMapClickListener(new OnMapClickListener(this));
                _btnReserve.Click += (sender, args) =>
                {
                    StartActivity(new Intent(this,typeof(PayLayout)));
                };

                #endregion


            }
            catch (Exception ex)
            {
                Log.Error(Tag, ex.StackTrace);
            }
        }


        private void InitOverlay()
        {
            //位置
            LatLng hotelLatLng = new LatLng(_hotelEntity.Latitude, _hotelEntity.Longitude);
            OverlayOptions hotelOverlayOptions = new MarkerOptions()
                .InvokeIcon(_hotelBitmap)
                .InvokePosition(hotelLatLng)
                .InvokeZIndex(9);
            Marker hotelMarker = _mBaiduMap.AddOverlay(hotelOverlayOptions).JavaCast<Marker>();
            Bundle bundle = new Bundle();
            bundle.PutSerializable("info", _hotelEntity);
            hotelMarker.ExtraInfo = bundle;

            #region 当前位置




            LatLng locationLatLng = new LatLng(CurrentData.Latitude, CurrentData.Longitude);
            OverlayOptions locationOverlayOptions = new MarkerOptions()
                .InvokeIcon(_localtionBitmap)
                .InvokePosition(locationLatLng)
                .InvokeZIndex(9);
            Marker locationMarker = _mBaiduMap.AddOverlay(locationOverlayOptions).JavaCast<Marker>(); ;

            //设置居中
            MapStatusUpdate mMapStatusUpdate = MapStatusUpdateFactory.NewLatLng(locationLatLng);
            //改变地图状态
            _mBaiduMap.SetMapStatus(mMapStatusUpdate);

            #endregion 当前位置


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

        #region 内部类
        public class OnMapClickListener : Java.Lang.Object, BaiduMap.IOnMapClickListener
        {
            private readonly DetailsActivity _detailsActivity;

            public OnMapClickListener(DetailsActivity detailsActivity)
            {
                _detailsActivity = detailsActivity;
            }

            public void OnMapClick(LatLng p0)
            {
                _detailsActivity._mBaiduMap.HideInfoWindow();
            }

            public bool OnMapPoiClick(MapPoi p0)
            {
                return false;
            }
        }

        class OnMarkerClickListener : Java.Lang.Object, BaiduMap.IOnMarkerClickListener
        {
            private readonly DetailsActivity _detailsActivity;

            public OnMarkerClickListener(DetailsActivity detailsActivity)
            {
                _detailsActivity = detailsActivity;
            }

            public bool OnMarkerClick(Marker marker)
            {
                try
                {


                    HotelEntity entity = marker.ExtraInfo.GetSerializable("info") as HotelEntity;

                    TextView location = new TextView(_detailsActivity.ApplicationContext);
                    location.SetBackgroundResource(Resource.Drawable.infowindow_bg);
                    location.SetPadding(30, 20, 30, 50);
                    if (entity != null) location.Text = entity.Name;

                    var latlng = marker.Position;
                    var point = _detailsActivity._mBaiduMap.Projection.ToScreenLocation(latlng);
                    Log.Info(Tag, "--!" + point.X + " , " + point.Y);
                    point.Y -= 47;
                    var pointInfo = _detailsActivity._mBaiduMap.Projection.FromScreenLocation(point);
                    var window = new InfoWindow(location, pointInfo,0);
                    _detailsActivity._mBaiduMap.ShowInfoWindow(window);
                }
                catch (Exception)
                {

                    return false;
                }
                return true;
            }
        }
        #endregion


    }
}