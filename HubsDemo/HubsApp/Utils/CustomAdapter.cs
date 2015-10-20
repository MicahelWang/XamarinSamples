using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Utils;
using Object = Java.Lang.Object;


namespace HubsApp.Utils
{
    public class CustomAdapter : BaseAdapter
    {

        private readonly List<HotelEntity> _data; //����  
        private readonly int _resource; //item�Ĳ���  
        private readonly Context _context;
        private LayoutInflater _inflator;
        private TextView _titleTextView;
        private TextView _textTextView;

        public CustomAdapter(List<HotelEntity> data, int resource, Context context)
        {
            this._data = data;
            this._resource = resource;
            this._context = context;
        }

        public override int Count
        {
            get { return _data.Count; }
        }

        public override Object GetItem(int position)
        {
            return _data[position];
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                _inflator = (LayoutInflater) _context.GetSystemService(Context.LayoutInflaterService);
                convertView = _inflator.Inflate(_resource, null);
                _titleTextView = (TextView) convertView.FindViewById(Resource.Id.itemTitle);
                //Ϊ�˼��ٿ�������ֻ�ڵ�һҳʱ����findViewById  
                _textTextView = (TextView) convertView.FindViewById(Resource.Id.itemText);
                convertView.Tag = _context;
            }
            var enttiy = _data[position];

            _titleTextView.Text = enttiy.Name;
            const string description = "����{0}��";
            var distance = (new Random()).Next(500);
            _textTextView.Text = string.Format(description, distance);
            return convertView;
        }

        public override long GetItemId(int position)
        {
            return position;
        }


    }
}