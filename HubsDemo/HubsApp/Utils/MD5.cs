using System;
using Java.Security;

namespace HubsApp.Utils
{

    public class Md5
    {

        private Md5() { }

        public  static String GetMessageDigest(byte[] buffer)
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
                    str[k++] = hexDigits[MoveByte(byte0,4) & 0xf];
                    str[k++] = hexDigits[byte0 & 0xf];
                }
                return new String(str);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// ���������λ��������0���ƣ�����Ǹ�������Ҫ���������ת����������λ��
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static int MoveByte(int value, int pos)
        {
            if (value < 0)
            {
                string s = Convert.ToString(value, 2);    // ת��Ϊ������
                for (int i = 0; i < pos; i++)
                {
                    s = "0" + s.Substring(0, 31);
                }
                return Convert.ToInt32(s, 2);            // ������������ת��Ϊ����
            }
            else
            {
                return value >> pos;
            }
        }
    }

}