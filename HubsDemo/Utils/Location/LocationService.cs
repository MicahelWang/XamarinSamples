
using Android.App;
using Android.Content;
using Android.OS;
using Com.Baidu.Location;
using Java.Util;
using Android.Util;
using Utils.Common;

namespace Utils.Location
{
    public class LocationService : Service
    {
        #region 声明与定义

        private const string Tag = "LocationService";
        //Service Action
        public const string ActionMoblieLocatorService = "com.Hubs1.app.Location.LocationService";
        /// <summary>
        /// 间隔时间
        /// </summary>
        private const int DelayTime = 5 * 60 * 1000;

        /// <summary>
        /// 服务启动方式 开机启动/手动启动
        /// </summary>
        private int _startingMode = -1;

        /// <summary>
        /// 当前网络是否可用的标志
        /// </summary>
        private bool _isOpenNetwork = false;

        /// <summary>
        /// 检测网络的次数(5分钟一次，检测三次)
        /// </summary>
        private int _checkNetworkNumber = 0;

        private const string _tempcoor = "gcj02";

        #endregion 声明与定义
        /// <summary>
        /// 定时器
        /// </summary>
        private Timer _timer;
        private Context _context;
        private LocationClient _locationClient;
        private MyLocationListener _myLocationListener;

        private PowerManager.WakeLock _wakeLock;


        public override void OnCreate()
        {
            base.OnCreate();
            Log.Info(Tag, "-------------------------LocationServer OnCreate-------------------");
            _context = this;
            InitLocation();
        }

        public void StartBaiduService()
        {
            bool isRun = Helper.IsServiceRun(ApplicationContext, "com.baidu.location.f");
            Log.Info(Tag, "--startBaiduService IsRun =" + isRun);
            _locationClient.Start();
        }


        public override void OnRebind(Intent intent)
        {
            base.OnRebind(intent);
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        private void InitLocation()
        {
            LocationClientOption option = new LocationClientOption();
            option.SetLocationMode(LocationClientOption.LocationMode.HightAccuracy);//可选，默认高精度，设置定位模式，高精度，低功耗，仅设备
            option.CoorType = _tempcoor;//可选，默认gcj02，设置返回的定位结果坐标系，
            const int interval = 1000;
            option.ScanSpan = interval;//可选，默认0，即仅定位一次，设置发起定位请求的间隔需要大于等于1000ms才是有效的
            option.SetIsNeedAddress(false);//可选，设置是否需要地址信息，默认不需要
            option.OpenGps = true;//可选，默认false,设置是否使用gps
            option.LocationNotify = true;//可选，默认false，设置是否当gps有效时按照1S1次频率输出GPS结果
            option.SetIgnoreKillProcess(true);//可选，默认true，定位SDK内部是一个SERVICE，并放到了独立进程，设置是否在stop的时候杀死这个进程，默认不杀死

            _locationClient.LocOption = option;
        }

        public override void OnDestroy()
        {
            Log.Info(Tag, "-----------------------------------LocationService OnDestroy-----------------");
            if (_locationClient != null && _locationClient.IsStarted)
            {
                _locationClient.Stop();
                if (_myLocationListener != null)
                {
                    _locationClient.UnRegisterLocationListener(_myLocationListener);
                }
            }

            ReleaseWakeLock();
            Intent intent = new Intent(ApplicationContext,typeof(LocationServerReceiver));
            intent.PutExtra("startingMode", 2);
            base.OnDestroy();
        }

        private void AcquireWakeLock()
        {
            if (null != _wakeLock) return;
            PowerManager powerManager = ApplicationContext.GetSystemService(Context.PowerService) as PowerManager;
            if (powerManager != null)
                _wakeLock = powerManager.NewWakeLock(WakeLockFlags.Partial | WakeLockFlags.OnAfterRelease,
                    ActionMoblieLocatorService);
            //if (null != _wakeLock)
            //{
            //    _wakeLock.Acquire();
            //}
            _wakeLock?.Acquire();
        }

        private void ReleaseWakeLock()
        {
            if (null == _wakeLock) return;
            _wakeLock.Release();
            _wakeLock = null;
        }
        
    }
}