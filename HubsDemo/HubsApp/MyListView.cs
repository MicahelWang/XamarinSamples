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
using Utils;

namespace HubsApp
{
//    [Activity(Label = "MyListView")]
//    public class MyListView : Activity
//    {
//        private ListView lv;
//        protected override void OnCreate(Bundle bundle)
//        {
//            base.OnCreate(bundle);

//            lv = (ListView)FindViewById(Resource.Id.HotelListView);/*����һ����̬����*/
//            var data = GetAllHotel();
//            IList<IDictionary<string, object>> myList = new List<IDictionary<string, object>>();
//            for (int i = 0; i < 10; i++)
//            {
//                var map = new JavaDictionary<string, object>();
//                var entity = data[i];
//                map.Add("itemTitle", entity.Name);
//                map.Add("itemText", entity.Name + "_Text");
//                map.Add("itemEntity", entity);
//                myList.Add(map);
//            }

//            SimpleAdapter mSimpleAdapter = new SimpleAdapter(this, myList,//��Ҫ�󶨵�����                
//    R.layout.item,//ÿһ�еĲ���//��̬�����е�����Դ�ļ���Ӧ�����岼�ֵ�View��new String[] {"ItemImage"
//, "ItemTitle", "ItemText"},   
//new int[] { Resource.Id.itemTitle, Resource.Id.itemText}
//}  
//            );

//lv.setAdapter(mSimpleAdapter);//ΪListView�������� lv.setOnItemClickListener(new 
//OnItemClickListener()
//    {
        
//        public void onItemClick(AdapterView <?> arg0, View arg1, int arg2,
//                long arg3) {
//            setTitle("�����˵�" + arg2 + "��");//���ñ�������ʾ�������                
//        }
//    });
        
    


//private List<HotelEntity> GetAllHotel()
//{
//    List<HotelEntity> lstHotel = new List<HotelEntity>();
//    lstHotel.Add(new HotelEntity() { Latitude = 31.21938946823591, Longitude = 121.44572496414184, Name = "���Ŵ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.230000, Longitude = 121.476000, Name = "PMS ������ԾƵ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.220894186037426, Longitude = 121.46296620368957, Name = "�½�����Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.181047789207966, Longitude = 121.43518924713135, Name = "��ͤ����" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.203854609746724, Longitude = 121.41012668609619, Name = "�������ű���" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.197500, Longitude = 121.387500, Name = "��Է����-���ԾƵ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.234086966300285, Longitude = 121.4723539352417, Name = "�������Ŵ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.227000, Longitude = 121.480000, Name = "������������ᾭ��Ƶ�1" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.233389758364698, Longitude = 121.47172093391418, Name = "�������ʷ���" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.204267559849956, Longitude = 121.41145706176758, Name = "���ӱ���" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.198412676650566, Longitude = 121.44904017448425, Name = "�������ɳǴ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.230967838191443, Longitude = 121.3983678817749, Name = "��ɳ����Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.23520615775418, Longitude = 121.48757815361023, Name = "�³Ƿ���" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.245058139900607, Longitude = 121.48505687713623, Name = "���Ǵ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.218150810609426, Longitude = 121.44397616386413, Name = "�Ϻ�����" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.218352667033244, Longitude = 121.44526362419128, Name = "��������" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.213599751388894, Longitude = 121.43236756324768, Name = "���������Ϻ��ﻪ����Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.23753623513008, Longitude = 121.48224592208862, Name = "�Ͼ�����" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.205405457472807, Longitude = 121.47171020507812, Name = "��ϴ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.220536358974804, Longitude = 121.46071314811706, Name = "��������" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.235527234822246, Longitude = 121.47753596305847, Name = "���Ƿ���-test�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.1897489872964, Longitude = 121.43797874450683, Name = "�Ϻ���������" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.18101107359755, Longitude = 121.43502831459045, Name = "�ϻ�ͤ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.189124877352842, Longitude = 121.3604736328125, Name = "���ط���" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.200000, Longitude = 121.441667, Name = "�Ϻ�����������Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.25388185476643, Longitude = 121.49248123168945, Name = "�Ϻ��̵ؾ�������-test" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.004000, Longitude = 121.419000, Name = "���������Ϻ����з���" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.202845, Longitude = 121.447302, Name = "��ͤ������԰�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.045500, Longitude = 121.745842, Name = "[�ظ�]������Ǵ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.275605, Longitude = 121.545694, Name = "�Ϻ���԰����" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.227793560939002, Longitude = 121.52342319488525, Name = "�Ϻ��Ͻ�ɽ��Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.204221676594173, Longitude = 121.47072315216064, Name = "�Ϻ�˹������������Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.20766285901391, Longitude = 121.48805022239685, Name = "��������" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.224821019673175, Longitude = 121.52956008911133, Name = "�Ϻ����žӷ���Ԣ" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.236297815333568, Longitude = 121.42712116241455, Name = "�þӿ�ݾƵ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 30.858772568027262, Longitude = 121.56147837638855, Name = "�Ϻ��𺣰��ȼٴ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.228729341617146, Longitude = 121.45437240600586, Name = "�Ϻ���ɯ����ȫ�׷��Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.158779, Longitude = 121.421543, Name = "�Ϻ��ӱ����ʾƵ깫Ԣ" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.22832567265717, Longitude = 121.48159146308899, Name = "�Ϻ��»��־Ƶ깫Ԣ" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.176500, Longitude = 121.385500, Name = "�Ϻ��������Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.235032, Longitude = 121.439835, Name = "���T����Ʒ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.205556, Longitude = 121.595833, Name = "��������" });
//    lstHotel.Add(new HotelEntity() { Latitude = 30.916500, Longitude = 121.462000, Name = "�Ϻ��·�չ�Ż�ɽׯ" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.186111, Longitude = 121.345833, Name = "�Ϻ������Ÿ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.23234393681464, Longitude = 121.48475646972656, Name = "�Ϻ��и��������Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.226000, Longitude = 121.480000, Name = "�Ϻ��������ʾƵ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.214581550841018, Longitude = 121.41388177871704, Name = "�Ϻ����ش�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.254000, Longitude = 121.554000, Name = "�Ϻ���Ե���ʾƵ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.252643649300527, Longitude = 121.60013437271118, Name = "��������Ϻ��ֶ�ʥɯ��Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.235365, Longitude = 121.446618, Name = "ͬ�����" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.191000, Longitude = 121.444500, Name = "��ͥ����Ƶ��ϵ�·��" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.039500, Longitude = 121.744500, Name = "�Ϻ���ɽ�ȼٴ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.229000, Longitude = 121.454500, Name = "�Ϻ��������ʾƵ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.261000, Longitude = 121.489000, Name = "�Ϻ�������ʹ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.245754, Longitude = 121.453547, Name = "�Ϻ���ˮ���Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.224500, Longitude = 121.435000, Name = "�Ϻ��¶��Ĵ�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.216667, Longitude = 121.529167, Name = "�Ϻ����������Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.27580915812983, Longitude = 121.59767746925354, Name = "����������ʾƵ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.23018804007575, Longitude = 121.45438313484192, Name = "�Ϻ�������Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.248965624714163, Longitude = 121.48927330970764, Name = "���˴�Ƶ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.156914, Longitude = 121.443489, Name = "�Ϻ����վƵ꣨��վ�꣩" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.18231446904834, Longitude = 121.38231754302978, Name = "�Ϻ����ϾƵ�" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.276635, Longitude = 121.521250, Name = "�Ϻ��·��ӭ����" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.209103530205826, Longitude = 121.47337317466736, Name = "���оƵ깫Ԣ" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.261000, Longitude = 121.564000, Name = "�Ϻ�˫ӵ����" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.189000, Longitude = 121.448500, Name = "�Ϻ��������" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.205556, Longitude = 121.445833, Name = "�Ϻ���ɽ����" });
//    lstHotel.Add(new HotelEntity() { Latitude = 31.223500, Longitude = 121.480500, Name = "�Ϻ�ϣ��������Ԣ" });

//    return lstHotel;
//}
}