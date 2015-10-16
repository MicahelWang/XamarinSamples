using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Hubs1
{
    [Activity(Label = "@string/MainTitle", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int _count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", _count++); };


        }



        private void BindDataView()
        {

            ////SimpleExpandableListAdapter(Context context, IList<IDictionary<string, object>> groupData, 
            ////int expandedGroupLayout, 
            ////int collapsedGroupLayout, 
            ////string[] groupFrom, int[] groupTo, IList<IList<IDictionary<string, object>>> childData, 
            ////int childLayout, int lastChildLayout, string[] childFrom, int[] childTo);
            //var groupData = new List<Dictionary<string, object>>();
            //var expandedGroupLayout = 1;
            //var collapsedGroupLayout = 1;
            //string[] groupFrom=new string[] {};
            //int[] groupTo=new int[] {};

            //IExpandableListAdapter adapter = new SimpleExpandableListAdapter(this, data,);
        }
    }

}

