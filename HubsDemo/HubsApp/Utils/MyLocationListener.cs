using System.Text;
using Android.App;
using Android.Util;
using Com.Baidu.Location;

namespace HubsApp.Utils
{
    /// <summary>
    /// ʵ��ʵʱλ�ûص�����
    /// </summary>
    public class MyLocationListener : Java.Lang.Object, IBDLocationListener
    {
        private const string Tag = "LocationListener";

        //private double longitude;
        //private double latitude;
        public void OnReceiveLocation(BDLocation location)
        {
            if (location == null)
            {
                return;
            }
            Log.Info(Tag, "BDLocationListener OnReceiveLocation");

            int locType = location.LocType;
            //longitude = location.Longitude;
            //latitude = location.Latitude;
            CurrentData.Latitude = location.Latitude;
            CurrentData.Longitude = location.Longitude;
            //Resource.String.
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
            switch (location.LocType)
            {
                case BDLocation.TypeGpsLocation:
                    // GPS��λ���
                    sb.Append("\nspeed : ");
                    sb.Append(location.Speed); // ��λ������ÿСʱ
                    sb.Append("\nsatellite : ");
                    sb.Append(location.SatelliteNumber);
                    sb.Append("\nheight : ");
                    sb.Append(location.Altitude); // ��λ����
                    sb.Append("\ndirection : ");
                    sb.Append(location.Direction);
                    sb.Append("\naddr : ");
                    sb.Append(location.AddrStr);
                    sb.Append("\ndescribe : ");
                    sb.Append("gps��λ�ɹ�");
                    break;
                case BDLocation.TypeNetWorkLocation:
                    // ���綨λ���
                    sb.Append("\naddr : ");
                    sb.Append(location.AddrStr);
                    //��Ӫ����Ϣ
                    sb.Append("\noperationers : ");
                    sb.Append(location.Operators);
                    sb.Append("\ndescribe : ");
                    sb.Append("���綨λ�ɹ�");
                    break;
                case BDLocation.TypeOffLineLocation:
                    // ���߶�λ���
                    sb.Append("\ndescribe : ");
                    sb.Append("���߶�λ�ɹ������߶�λ���Ҳ����Ч��");
                    break;
                case BDLocation.TypeServerError:
                    sb.Append("\ndescribe : ");
                    sb.Append("��������綨λʧ�ܣ����Է���IMEI�źʹ��嶨λʱ�䵽loc-bugs@baidu.com��������׷��ԭ��");
                    break;
                case BDLocation.TypeNetWorkException:
                    sb.Append("\ndescribe : ");
                    sb.Append("���粻ͬ���¶�λʧ�ܣ����������Ƿ�ͨ��");
                    break;
                case BDLocation.TypeCriteriaException:
                    sb.Append("\ndescribe : ");
                    sb.Append("�޷���ȡ��Ч��λ���ݵ��¶�λʧ�ܣ�һ���������ֻ���ԭ�򣬴��ڷ���ģʽ��һ���������ֽ�����������������ֻ�");
                    break;
            }

            // LogMsg(sb.ToString());
            Log.Info(Tag, sb.ToString());
            //mLocationClient.setEnableGpsRealTimeTransfer(true);
        }
    }
}