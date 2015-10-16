using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text;
using Com.Tencent.MM.Sdk.Openapi;
using Com.Tencent.MM.Sdk.Modelpay;
using System.Collections.Generic;
using Java.Lang;
using Android.Util;
using System.Xml;

namespace TencentTest
{
    [Activity(Label = "TencentTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        TextView show;
        System.Text.StringBuilder sb;
        IWXAPI msgApi;
        PayReq req;
        Dictionary<string, string> resultunifiedorder;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            show = (TextView)FindViewById(Resource.Id.editText_prepay_id);
            req = new PayReq();
            sb = new System.Text.StringBuilder();
            msgApi = WXAPIFactory.CreateWXAPI(this, null);
            msgApi.RegisterApp(Constants.APP_ID);
            //生成prepay_id
            Button payBtn = FindViewById<Button>(Resource.Id.unifiedorder_btn);

            Dictionary<string, string> xml = new Dictionary<string, string>();

            payBtn.Click += (o, e) => {
                ProgressDialog dialog = ProgressDialog.Show(this, "abc".ToString(), "def");

                IRunnable payRunnable = new Runnable(() => {
                    string url = string.Format("https://api.mch.weixin.qq.com/pay/unifiedorder");
                    string entity = genProductArgs();

                    byte[] buf = Util.httpPost(url, entity);

                    string content = Encoding.Default.GetString(buf);

                    xml = decodeXml(content);
                });

                AsyncTask.Execute(payRunnable);

                if (dialog != null)
                {
                    dialog.Dismiss();
                }
                sb.Append("prepay_id\n" + resultunifiedorder["prepay_id"] + "\n\n");
                show.Text = sb.ToString();
            };

    		Button appayBtn = FindViewById<Button>(Resource.Id.appay_btn);
            appayBtn.Click += (o, e) => {
                sendPayReq();
            };
            //生成签名参数
            Button appay_pre_btn = FindViewById<Button>(Resource.Id.appay_pre_btn);
            appay_pre_btn.Click += (o, e) => {
                genPayReq();
            };
        }

        private void genPayReq()
        {

            req.AppId = Constants.APP_ID;
            req.PartnerId = Constants.MCH_ID;
            req.PrepayId = resultunifiedorder.ContainsKey("prepay_id") ? resultunifiedorder["prepay_id"]: "";
            req.PackageValue = "Sign=WXPay";
            req.NonceStr = genNonceStr();
            req.TimeStamp = genTimeStamp().ToString();


            List<System.Collections.Generic.KeyValuePair<string, string>> signParams = new List<System.Collections.Generic.KeyValuePair<string, string>>();
            signParams.Add(new KeyValuePair<string, string>("appid", req.AppId));
            signParams.Add(new KeyValuePair<string, string>("noncestr", req.NonceStr));
            signParams.Add(new KeyValuePair<string, string>("package", req.PackageValue));
            signParams.Add(new KeyValuePair<string, string>("partnerid", req.PartnerId));
            signParams.Add(new KeyValuePair<string, string>("prepayid", req.PrepayId));
            signParams.Add(new KeyValuePair<string, string>("timestamp", req.TimeStamp));

            req.Sign = genAppSign(signParams);

            sb.Append("sign\n" + req.Sign + "\n\n");

            show.Text = sb.ToString();

        }

        private void sendPayReq()
        {
            msgApi.RegisterApp(Constants.APP_ID);
            msgApi.SendReq(req);
        }

        private string genNonceStr()
        {
            Random random = new Random();
            return MD5.getMessageDigest(Encoding.Default.GetBytes(random.Next(10000).ToString()));
        }

        private long genTimeStamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
        }

        private string genAppSign(List<KeyValuePair<string, string>> args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < args.Count; i++) {
                sb.Append(args[i].Key);
                sb.Append('=');
                sb.Append(args[i]);
                sb.Append('&');
            }
            sb.Append("key=");
            sb.Append(Constants.API_KEY);

            this.sb.Append("sign str\n" + sb.ToString() + "\n\n");
            string appSign = MD5.getMessageDigest(Encoding.Default.GetBytes(sb.ToString())).ToUpper();

            return appSign;
        }

        public Dictionary<string, string> decodeXml(string content)
        {

            try
            {
                Dictionary<string, string> xml = new Dictionary<string, string>();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                XmlNodeList lstNodes = doc.ChildNodes;
                foreach (XmlNode node in lstNodes)
                {
                    if (xml.ContainsKey(node.Name))
                        xml[node.Name] = node.InnerText;
                    else
                        xml.Add(node.Name, node.InnerText);
                }
                
			    return xml;
		    } catch (System.Exception e) {

		    }
		    return null;

	    }
        private string genProductArgs()
        {
            System.Text.StringBuilder xml = new System.Text.StringBuilder();

            try
            {
                string nonceStr = genNonceStr();


                xml.Append("</xml>");
                List<KeyValuePair<string, string>> packageParams = new List<KeyValuePair<string, string>>();
                packageParams.Add(new KeyValuePair<string, string>("appid", Constants.APP_ID));
                packageParams.Add(new KeyValuePair<string, string>("body", "weixin"));
                packageParams.Add(new KeyValuePair<string, string>("mch_id", Constants.MCH_ID));
                packageParams.Add(new KeyValuePair<string, string>("nonce_str", nonceStr));
                packageParams.Add(new KeyValuePair<string, string>("notify_url", "http://121.40.35.3/test"));
                packageParams.Add(new KeyValuePair<string, string>("out_trade_no", genOutTradNo()));
                packageParams.Add(new KeyValuePair<string, string>("spbill_create_ip", "127.0.0.1"));
                packageParams.Add(new KeyValuePair<string, string>("total_fee", "1"));
                packageParams.Add(new KeyValuePair<string, string>("trade_type", "APP"));


                string sign = genPackageSign(packageParams);
                packageParams.Add(new KeyValuePair<string, string>("sign", sign));


                string xmlstring = toXml(packageParams);

                return xmlstring;

            }
            catch (System.Exception e)
            {
                return null;
            }


        }

        /**
	 生成签名
	 */

        private string genPackageSign(List<KeyValuePair<string, string>> paramters)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < paramters.Count; i++) {
                sb.Append(paramters[i].Key);
                sb.Append('=');
                sb.Append(paramters[i].Value);
                sb.Append('&');
            }
            sb.Append("key=");
            sb.Append(Constants.API_KEY);


            string packageSign = MD5.getMessageDigest(Encoding.Default.GetBytes(sb.ToString())).ToUpper();
            return packageSign;
        }

        private string toXml(List<KeyValuePair<string, string>> parameters)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<xml>");
            for (int i = 0; i < parameters.Count; i++) {
                sb.Append("<" + parameters[i].Key + ">");


                sb.Append(parameters[i].Value);
                sb.Append("</" + parameters[i].Key + ">");
            }
            sb.Append("</xml>");

            return sb.ToString();
        }

        private string genOutTradNo()
        {
            Random random = new Random();
            return MD5.getMessageDigest(Encoding.Default.GetBytes(random.Next(10000).ToString()));
        }
    }
}

