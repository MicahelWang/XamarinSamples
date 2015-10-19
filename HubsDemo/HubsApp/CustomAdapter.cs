using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Utils;
using Object = Java.Lang.Object;

namespace HubsApp
{
    public class CustomAdapter : BaseAdapter
    {

        private List<HotelEntity> _data; //����  
        private int _resource; //item�Ĳ���  
        private Context _context;
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