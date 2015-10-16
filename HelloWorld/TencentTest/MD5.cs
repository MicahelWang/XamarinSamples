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

            if (pos != 0)  //移动 0 位时直接返回原值
            {
                int mask = 0x7fffffff;     // int.MaxValue = 0x7FFFFFFF 整数最大值
                value >>= 1;     //第一次做右移，把符号也算上，无符号整数最高位不表示正负但操作数还是有符号的，有符号数右移1位，正数时高位补0，负数时高位补1
                value &= mask;     //和整数最大值进行逻辑与运算，运算后的结果为忽略表示正负值的最高位
                value >>= pos - 1;     //逻辑运算后的值无符号，对无符号的值直接做右移运算，计算剩下的位
            }
            return value;
        }


    }
}