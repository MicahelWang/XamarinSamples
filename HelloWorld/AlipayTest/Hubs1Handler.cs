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
using System.Linq.Expressions;

namespace AlipayTest
{
    public class Hubs1Handler : Handler
    {
        public delegate void MyHandler(Message msg);
        public MyHandler myHandler = null;
        public override void HandleMessage(Message msg)
        {
            if (myHandler != null)
                myHandler.Invoke(msg);
        }


    }
}