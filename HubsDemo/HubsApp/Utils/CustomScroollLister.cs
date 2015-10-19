
using Android.Widget;

namespace HubsApp
{
    public class CustomScroollLister : Java.Lang.Object, AbsListView.IOnScrollListener
    {
        //private bool _scrollFlag;

        private readonly CustomAdapter _adapter;
        private readonly ListView _listView;

        private int _lastItem;
        public CustomScroollLister(CustomAdapter adapter, ListView listView)
        {
            _adapter = adapter;
            _listView = listView;
            //this._scrollFlag = scrollFlag;
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {

            _lastItem = firstVisibleItem + visibleItemCount;
        }

        void AbsListView.IOnScrollListener.OnScrollStateChanged(AbsListView view, ScrollState scrollState)
        {
            //switch (scrollState)
            //{
            //    case ScrollState.Idle:// 是当屏幕停止滚动时
            //        scrollFlag = false;
            //        var lastPostion = view.LastVisiblePosition;
            //        var viewCount = view.Count;
            //        if (lastPostion==viewCount-1)
            //        {

            //        }

            //        break;
            //    case ScrollState.TouchScroll:// 滚动时
            //        scrollFlag = true;
            //        break;
            //    case ScrollState.Fling:// 是当用户由于之前划动屏幕并抬起手指，屏幕产生惯性滑动时
            //        scrollFlag = false;
            //        break;
            //}
            //listview滚动时会执行这个方法，这儿调用加载数据的方法。
            _adapter.NotifyDataSetChanged();//提醒adapter更新  

            _listView.SetSelection(_lastItem - 1); ;//设置listview的当前位置，如果不设置每次加载完后都会返回到list的第一项。 
        }

    }
}