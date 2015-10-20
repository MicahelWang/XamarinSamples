using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Baidu.Location;

namespace Utils.Location
{
    /// <summary>
    /// 实现实时位置回调监听
    /// </summary>
    public class MyLocationListener : Java.Lang.Object, IBDLocationListener
    {

        private double longitude;
        private double latitude;
        public void OnReceiveLocation(BDLocation location)
        {
            if (location==null)
            {
                return;
            }
            Log.Info("LocationService", "BDLocationListener OnReceiveLocation");

            int locType = location.LocType;
            longitude = location.Longitude;
            latitude = location.Latitude;
            //Receive Location
            StringBuilder sb = new StringBuilder();
            sb.Append("time : ");
            sb.Append(location.Time);
            sb.Append("\nerror code : ");
            sb.Append(location.LocType);
            sb.Append("\nlatitude : ");
            sb.Append(location.Latitude);
            sb.Append("\nlontitude : ");
            sb.Append(location.Longitude);
            sb.Append("\nradius : ");
            sb.Append(location.Radius);
            if (location.LocType == BDLocation.TypeGpsLocation)
            {
                // GPS定位结果
                sb.Append("\nspeed : ");
                sb.Append(location.Speed); // 单位：公里每小时
                sb.Append("\nsatellite : ");
                sb.Append(location.SatelliteNumber);
                sb.Append("\nheight : ");
                sb.Append(location.Altitude); // 单位：米
                sb.Append("\ndirection : ");
                sb.Append(location.Direction);
                sb.Append("\naddr : ");
                sb.Append(location.AddrStr);
                sb.Append("\ndescribe : ");
                sb.Append("gps定位成功");

            }
            else if (location.LocType == BDLocation.TypeNetWorkLocation)
            {
                // 网络定位结果
                sb.Append("\naddr : ");
                sb.Append(location.AddrStr);
                //运营商信息
                sb.Append("\noperationers : ");
                sb.Append(location.Operators);
                sb.Append("\ndescribe : ");
                sb.Append("网络定位成功");
            }
            else if (location.LocType == BDLocation.TypeOffLineLocation)
            {
                // 离线定位结果
                sb.Append("\ndescribe : ");
                sb.Append("离线定位成功，离线定位结果也是有效的");
            }
            else if (location.LocType == BDLocation.TypeServerError)
            {
                sb.Append("\ndescribe : ");
                sb.Append("服务端网络定位失败，可以反馈IMEI号和大体定位时间到loc-bugs@baidu.com，会有人追查原因");
            }
            else if (location.LocType == BDLocation.TypeNetWorkException)
            {
                sb.Append("\ndescribe : ");
                sb.Append("网络不同导致定位失败，请检查网络是否通畅");
            }
            else if (location.LocType == BDLocation.TypeCriteriaException)
            {
                sb.Append("\ndescribe : ");
                sb.Append("无法获取有效定位依据导致定位失败，一般是由于手机的原因，处于飞行模式下一般会造成这种结果，可以试着重启手机");
            }

            // LogMsg(sb.ToString());
            Log.Info("BaiduLocationApiDem", sb.ToString());
            // mLocationClient.setEnableGpsRealTimeTransfer(true);
        }
    }
}