using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TencentTest
{
    public class Constants
    {
        //appid
        //请同时修改  androidmanifest.xml里面，.PayActivityd里的属性<data android:scheme="wxb4ba3c02aa476ea1"/>为新设置的appid
        public static  string APP_ID = "wx4a6d023e24cc7109";




        //商户号
        public static string MCH_ID = "1249287101";


        //  API密钥，在商户平台设置
        public static string API_KEY = "0bc13ddb055824a2c74d2bb6c3a92a7a";
    }
}