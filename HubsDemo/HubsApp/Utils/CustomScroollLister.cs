
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
            //    case ScrollState.Idle:// �ǵ���Ļֹͣ����ʱ
            //        scrollFlag = false;
            //        var lastPostion = view.LastVisiblePosition;
            //        var viewCount = view.Count;
            //        if (lastPostion==viewCount-1)
            //        {

            //        }

            //        break;
            //    case ScrollState.TouchScroll:// ����ʱ
            //        scrollFlag = true;
            //        break;
            //    case ScrollState.Fling:// �ǵ��û�����֮ǰ������Ļ��̧����ָ����Ļ�������Ի���ʱ
            //        scrollFlag = false;
            //        break;
            //}
            //listview����ʱ��ִ�����������������ü������ݵķ�����
            _adapter.NotifyDataSetChanged();//����adapter����  

            _listView.SetSelection(_lastItem - 1); ;//����listview�ĵ�ǰλ�ã����������ÿ�μ�����󶼻᷵�ص�list�ĵ�һ� 
        }

    }
}