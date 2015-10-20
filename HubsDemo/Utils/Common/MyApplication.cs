using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;

namespace Utils.Common
{
    public class Helper
    {

        public static bool IsServiceRun(Context context, string className)
        {
            var isRun = false;
            ActivityManager activityManager = (ActivityManager) context.GetSystemService(Context.ActivityService);
            var serviceList = activityManager.GetRunningServices(40);

            if (serviceList.Any(m => m.Service.ClassName == className))
            {
                isRun = true;
            }
            return isRun;
        }
     
         
    }
}