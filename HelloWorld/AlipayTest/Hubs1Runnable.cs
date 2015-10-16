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
using Java.Lang;

namespace AlipayTest
{
    public class Hubs1Runnable : IRunnable
    {
        public delegate void MyRunnable();
        public MyRunnable myRunnable = null;

        public IntPtr Handle
        {
            get
            {
                return null;
            }
        }
        public Hubs1Runnable()
        {

        }
        public void Run()
        {
            if (myRunnable != null)
                myRunnable.Invoke();
        }

        public void Dispose()
        {
            
        }
    }
}