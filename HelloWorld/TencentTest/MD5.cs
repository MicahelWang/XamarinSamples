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
using Java.Security;

namespace TencentTest
{
    class MD5
    {
        public static string getMessageDigest(byte[] buffer)
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            try
            {
                MessageDigest mdTemp = MessageDigest.GetInstance("MD5");
                mdTemp.Update(buffer);
                byte[] md = mdTemp.Digest();
                int j = md.Length;
                char[] str = new char[j * 2];
                int k = 0;
                for (int i = 0; i < j; i++)
                {
                    byte byte0 = md[i];
                    str[k++] = hexDigits[rightMove(byte0, 4) & 0xf];
                    str[k++] = hexDigits[byte0 & 0xf];
                }
                return str.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        static int rightMove(int value, int pos)
        {

            if (pos != 0)  //�ƶ� 0 λʱֱ�ӷ���ԭֵ
            {
                int mask = 0x7fffffff;     // int.MaxValue = 0x7FFFFFFF �������ֵ
                value >>= 1;     //��һ�������ƣ��ѷ���Ҳ���ϣ��޷����������λ����ʾ�����������������з��ŵģ��з���������1λ������ʱ��λ��0������ʱ��λ��1
                value &= mask;     //���������ֵ�����߼������㣬�����Ľ��Ϊ���Ա�ʾ����ֵ�����λ
                value >>= pos - 1;     //�߼�������ֵ�޷��ţ����޷��ŵ�ֱֵ�����������㣬����ʣ�µ�λ
            }
            return value;
        }


    }
}