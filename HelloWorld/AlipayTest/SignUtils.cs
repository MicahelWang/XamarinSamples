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
using Java.Security.Spec;
using Java.Security;

namespace AlipayTest
{
    public class SignUtils
    {
        private static string ALGORITHM = "RSA";

	    private static string SIGN_ALGORITHMS = "SHA1WithRSA";

	    private static string DEFAULT_CHARSET = "UTF-8";

	    public static string sign(string content, string privateKey)
        {
            try
            {
                PKCS8EncodedKeySpec priPKCS8 = new PKCS8EncodedKeySpec(Convert.FromBase64String(privateKey));
                        //Base64.decode(privateKey));
                KeyFactory keyf = KeyFactory.GetInstance(ALGORITHM, "BC");
                IPrivateKey priKey = keyf.GeneratePrivate(priPKCS8);

                Signature signature = Signature.GetInstance(SIGN_ALGORITHMS);

                signature.InitSign(priKey);
                signature.Update(Encoding.Default.GetBytes(content));

                byte[] signed = signature.Sign();
                
                return Convert.ToBase64String(signed);
            }
            catch (Exception e)
            {
                string mesg = e.StackTrace;
            }

            return null;
        }
    }
}