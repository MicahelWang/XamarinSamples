using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Alipay.Sdk.App;
using Com.Tencent.MM.Sdk.Modelpay;
using Com.Tencent.MM.Sdk.Openapi;
using HubsApp.Utils;
using Java.IO;
using Java.Lang;

namespace HubsApp
{
    [Activity(Label = "PayLayout", Theme = "@style/MyDialogStyleBottom")]
    public class PayLayout : Activity
    {

        private const string Tag = "PayLayout";
        private Button _btnAlipay, _btnTecentPay, _btnCancel;

        private LinearLayout _layout;
        private Handler _handler;

        private PayReq _payRequest;
        private IWXAPI _msgApi = null;
        private Dictionary<string, string> _resultunifiedorder;
        private System.Text.StringBuilder _sb;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.PayLayout);

            _msgApi = WXAPIFactory.CreateWXAPI(this, null);
            _sb = new System.Text.StringBuilder();

            #region 获取控件

            _btnAlipay = FindViewById<Button>(Resource.Id.btn_Alipay);
            _btnTecentPay = FindViewById<Button>(Resource.Id.btn_TecentPay);
            _btnCancel = FindViewById<Button>(Resource.Id.btn_Cancel);
            _layout = FindViewById<LinearLayout>(Resource.Id.pop_layout);

            #endregion 获取控件

            // Create your application here

            #region 事件绑定

            _layout.Click += (sender, args) =>
            {
                Toast.MakeText(ApplicationContext,
                    "提示：点击窗口外部关闭窗口！",
                    ToastLength.Long);
            };
            _btnAlipay.Click += AlipayPay;

            _btnTecentPay.Click += TecentPayPay;

            _btnCancel.Click += (sender, args) =>
            {
                Finish();
            };

            #endregion 事件绑定

            #region Handler  回调处理

            _handler = new Handler((msg) =>
            {
                var msgWhat = (MsgWhat)msg.What;

                switch (msgWhat)
                {
                    #region 支付宝支付结果回调

                    case MsgWhat.AlipayPayFlag:
                        {
                            PayResult payResult = new PayResult((string)msg.Obj);

                            // 支付宝返回此次支付结果及加签，建议对支付宝签名信息拿签约时支付宝提供的公钥做验签
                            string resultInfo = payResult.Result;

                            string resultStatus = payResult.ResultStatus;

                            // 判断resultStatus 为“9000”则代表支付成功，具体状态码代表含义可参考接口文档
                            switch (resultStatus)
                            {
                                case "9000":
                                    {
                                        Toast.MakeText(this, "支付成功",
                                   ToastLength.Short).Show();
                                        break;
                                    }
                                // 判断resultStatus 为非“9000”则代表可能支付失败
                                // “8000”代表支付结果因为支付渠道原因或者系统原因还在等待支付结果确认，最终交易是否成功以服务端异步通知为准（小概率状态）
                                case "8000":
                                    {
                                        Toast.MakeText(this, "支付结果确认中",
                                        ToastLength.Short).Show(); break;
                                    }
                                case "4000":
                                    {
                                        Toast.MakeText(this, "未安装支付宝",
                                   ToastLength.Short).Show();
                                        break;
                                    }
                                default:
                                    {
                                        // 其他值就可以判断为支付失败，包括用户主动取消支付，或者系统返回的错误
                                        Toast.MakeText(this, "支付失败",
                                            ToastLength.Short).Show();
                                        break;
                                    }
                            }
                            break;
                        }

                    #endregion 支付宝支付结果回调
                    #region 微信获取认证结果回调

                    case MsgWhat.TencentValidateFlag:
                        {

                            Dictionary<string, string> xml = null;
                            try
                            {
                                xml = DecodeXml(msg.Obj.ToString());
                            }
                            catch (System.Exception)
                            {

                                Toast.MakeText(this, "服务器请求异常",
                                        ToastLength.Long).Show();
                                break;
                            }

                            if (xml.ContainsKey("return_code"))
                            {
                                var returnCode = xml["return_code"];

                                if (returnCode == "FAIL")
                                {
                                    var errMsg = xml["return_msg"];
                                    Toast.MakeText(this, "请求微信异常：" + errMsg,
                                        ToastLength.Long).Show();
                                }
                                else
                                {
                                    _sb.Append("prepay_id\n" + xml["prepay_id"] + "\n\n");
                                    SendPayReq();
                                }
                            }
                            //Toast.MakeText(this, "验证结果：" + msg.Obj,
                            //ToastLength.Short).Show();
                            break;
                        }

                    #endregion 微信获取认证结果回调
                    #region 微信支付结果回调

                    case MsgWhat.TencentPayFlag:
                        {
                            Toast.MakeText(this, "检查结果为：" + msg.Obj,
                                ToastLength.Short).Show();
                            break;
                        }
                        #endregion 微信支付结果回调
                }
            });

            #endregion Handler  回调处理

        }


        public override bool OnTouchEvent(MotionEvent e)
        {
            Finish();
            return true;
        }

        private void TecentPayPay(object sender, EventArgs args)
        {
            _msgApi.RegisterApp(Constants.AppId);


            var checkRunnable = new Runnable(() =>
            {


                string url = string.Format("https://api.mch.weixin.qq.com/pay/unifiedorder");
                string entity = GenProductArgs();
                var buf = Util.httpPost(url, entity);

                string content = System.Text.Encoding.Default.GetString(buf);
                _resultunifiedorder = DecodeXml(content);
                var msg = new Message
                {
                    What = (int)MsgWhat.TencentValidateFlag,
                    Obj = content
                };
                _handler.SendMessage(msg);
            });

            Java.Lang.Thread checkThread = new Java.Lang.Thread(checkRunnable);
            checkThread.Start();

        }

        private void AlipayPay(object sender, EventArgs args)
        {


            if (!AliPayHelper.CheckConfig())
            {
                Toast.MakeText(ApplicationContext,
                    "系统异常.",
                    ToastLength.Long);
                Log.Error(Tag, "Aplipay Config Exception ");
                return;
            }
            string payInfo = AliPayHelper.GetPayInfo();
            // 完整的符合支付宝参数规范的订单信息
            Runnable payRunnable = new Runnable(() =>
            {
                PayTask alipay = new PayTask(this);
                // 调用支付接口，获取支付结果
                string result = alipay.Pay(payInfo);

                Message msg = new Message
                {
                    What = (int)MsgWhat.AlipayPayFlag,
                    Obj = result
                };
                _handler.SendMessage(msg);
            });

            // 必须异步调用
            Thread payThread = new Thread(payRunnable);
            payThread.Start();
        }
        #region 微信支付相关


        public Dictionary<string, string> DecodeXml(string content)
        {

            try
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(content); //加载xml
                Dictionary<string, string> xml = new Dictionary<string, string>();
                var nodes = xmlDoc.DocumentElement.SelectNodes("/xml/*");
                foreach (XmlNode node in nodes)
                {
                    if (xml.ContainsKey(node.Name))
                    {
                        xml[node.Name] = node.InnerText;
                    }
                    else
                    {
                        xml.Add(node.Name, node.InnerText);
                    }

                }



                return xml;
            }
            catch (System.Exception e)
            {

            }
            return null;

        }


        private string GenProductArgs()
        {
            try
            {
                string nonceStr = genNonceStr();

                List<KeyValuePair<string, string>> packageParams = new List<KeyValuePair<string, string>>();
                packageParams.Add(new KeyValuePair<string, string>("appid", Constants.AppId));
                packageParams.Add(new KeyValuePair<string, string>("body", "weixin"));
                packageParams.Add(new KeyValuePair<string, string>("mch_id", Constants.MchId));
                packageParams.Add(new KeyValuePair<string, string>("nonce_str", nonceStr));
                packageParams.Add(new KeyValuePair<string, string>("notify_url", "http://121.40.35.3/test"));
                packageParams.Add(new KeyValuePair<string, string>("out_trade_no", genOutTradNo()));
                packageParams.Add(new KeyValuePair<string, string>("spbill_create_ip", "127.0.0.1"));
                packageParams.Add(new KeyValuePair<string, string>("total_fee", "1"));
                packageParams.Add(new KeyValuePair<string, string>("trade_type", "APP"));


                string sign = GenPackageSign(packageParams);
                packageParams.Add(new KeyValuePair<string, string>("sign", sign));

                string xmlstring = toXml(packageParams);

                return xmlstring;

            }
            catch (System.Exception e)
            {

                return null;
            }


        }

        private string genNonceStr()
        {
            Random random = new Random();
            return Md5.GetMessageDigest(random.Next(10000).ToString().ToBytes());
        }

        private long genTimeStamp()
        {
            return DateTime.Now.Ticks / 1000;
        }



        private string genOutTradNo()
        {
            Random random = new Random();
            return Md5.GetMessageDigest(random.Next(10000).ToString().ToBytes());
        }

        private void GenPayReq()
        {

            _payRequest.AppId = Constants.AppId;
            _payRequest.PartnerId = Constants.MchId;
            _payRequest.PrepayId = _resultunifiedorder["prepay_id"];
            _payRequest.PackageValue = "Sign=WXPay";
            _payRequest.NonceStr = genNonceStr();
            _payRequest.TimeStamp = genTimeStamp().ToString();


            List<KeyValuePair<string, string>> signParams = new List<KeyValuePair<string, string>>();
            signParams.Add(new KeyValuePair<string, string>("appid", _payRequest.AppId));
            signParams.Add(new KeyValuePair<string, string>("noncestr", _payRequest.NonceStr));
            signParams.Add(new KeyValuePair<string, string>("package", _payRequest.PackageValue));
            signParams.Add(new KeyValuePair<string, string>("partnerid", _payRequest.PartnerId));
            signParams.Add(new KeyValuePair<string, string>("prepayid", _payRequest.PrepayId));
            signParams.Add(new KeyValuePair<string, string>("timestamp", _payRequest.TimeStamp));

            _payRequest.Sign = genAppSign(signParams);

            _sb.Append("sign\n" + _payRequest.Sign + "\n\n");

            //show.setText(sb.ToString());



        }

        private void SendPayReq()
        {
            _msgApi.RegisterApp(Constants.AppId);
            _msgApi.SendReq(_payRequest);
        }

        private string GenPackageSign(List<KeyValuePair<string, string>> param)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < param.Count; i++)
            {
                sb.Append(param[i].Key);
                sb.Append('=');
                sb.Append(param[i].Value);
                sb.Append('&');
            }
            sb.Append("key=");
            sb.Append(Constants.ApiKey);


            string packageSign = Md5.GetMessageDigest(sb.ToString().ToBytes()).ToUpper();
            return packageSign;
        }

        private string genAppSign(List<KeyValuePair<string, string>> param)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < param.Count; i++)
            {
                sb.Append(param[i].Key);
                sb.Append('=');
                sb.Append(param[i].Value);
                sb.Append('&');
            }
            sb.Append("key=");
            sb.Append(Constants.ApiKey);

            sb.Append("sign str\n" + sb.ToString() + "\n\n");
            string appSign = Md5.GetMessageDigest(sb.ToString().ToBytes()).ToUpper();
            return appSign;
        }

        private string toXml(List<KeyValuePair<string, string>> param)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<xml>");
            for (int i = 0; i < param.Count; i++)
            {
                sb.Append("<" + param[i].Key + ">");

                sb.Append(param[i].Value);
                sb.Append("</" + param[i].Key + ">");
            }
            sb.Append("</xml>");


            return sb.ToString();
        }

        #endregion 微信支付相关
    }

    internal enum MsgWhat
    {
        AlipayPayFlag,
        TencentValidateFlag,
        TencentPayFlag,
    }
}