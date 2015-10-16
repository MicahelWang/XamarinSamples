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

namespace Phoneword
{
    [Activity(Label = "@string/CallHistory")]
    public class CallHistoryActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //从意图中获取传递过来的参数
            var phoneNumbers = Intent.Extras.GetStringArrayList("phone_numbers") ?? new string[0];

            // 将字符串数组显示到列表控件中（因为继承的是ListActivity所以整个视图就是一个列表）
            this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);

            //关于ArrayAdapter的第二个参数，其实就是指定列表中每个项的视图，后面我们会通过自定义的方式控制列表的项
        }
    }
}