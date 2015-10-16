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

            //����ͼ�л�ȡ���ݹ����Ĳ���
            var phoneNumbers = Intent.Extras.GetStringArrayList("phone_numbers") ?? new string[0];

            // ���ַ���������ʾ���б�ؼ��У���Ϊ�̳е���ListActivity����������ͼ����һ���б�
            this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);

            //����ArrayAdapter�ĵڶ�����������ʵ����ָ���б���ÿ�������ͼ���������ǻ�ͨ���Զ���ķ�ʽ�����б����
        }
    }
}