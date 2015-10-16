using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Com.Alipay.Android;
using Java.IO;
using Java.Lang;
using Com.Alipay.Sdk.App;


namespace AlipayTest
{
    [Activity(Label = "Hubs1支付", MainLauncher = true, Icon = "@drawable/msp_icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        // 商户PID
        public const string PARTNER = "2088601988973166";
        // 商户收款账号
        public const string SELLER = "daniel.sheng@hubs1.net";
        // 商户私钥，pkcs8格式
        public const string RSA_PRIVATE = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAJ2HWLvt8qEJ8xLoW9T4efxAC/DxFv+9upMkXHfp9frbIPxJY0ezbm92/mOu+OQWT2ELXhYcpSIoyASvTrCD3PZ3eKuBwNfTNAHTE+JkIsODdPyHXL7uCLvSfSEbIXtl+FCuw9XbvUNEzcGmNZrwzpOyWXGcVqGy7Cr7mudim7oFAgMBAAECgYADb+6ZuyluJh9trDBEx18yB9u62CfeUK9/gm2aGDrLzHg9yQQnjly8heYrGqhHocz9mxfkd5+KzUUABSs8YsQxe8HXNCPdfJABSRXUXt/2AKaXcRbck6YgRAEwxSaWLXvzVESuVs4z03YJN/K5cdxEUMZmCVQ7EG5HUJbP4gxEQQJBANsdn3PRsjVNOccNSquCrf3FpxJ+BE+5frfIX0lyNgpkUMmPekKEOsN0aSvcnG3XBv9c/xgQqJQ9WmoR6AtF8ukCQQC4C79v2Kg5vaJB4/XCl1WAkOI5YXldBSOo8jOdhpMKR5dO0zg+io3B0j38U7tz+xk5H7+LYud1FrFDdZQh9cS9AkEAgnLzYDeimhsc38WpA8zsGx5WJitCE9jCeVXgbNCDHdK1ShqSVhF1DrI6fvN7aeVPdC6AbGpWgtK4BlgcxsFhKQJBAJwUCmM1n2RoN2QdiFtfr3j6ZX839I44P4eU7sTWTXhYQi7s3TPcmF8Yhsynzb6L4VYaYHw6ggNAHVASNG+gVxkCQC5xeZpuaYONs78vbiqim8QLl/3dv8JmVrS3uU+FmXG5WX5FgNwu28oXYhC8H6nfVPzrNCy2CxAuohzL5Ww/0K8=";
        // 支付宝公钥
        public const string RSA_PUBLIC = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";

        private const int SDK_PAY_FLAG = 1;

        private const int SDK_CHECK_FLAG = 2;

        private Handler mHandler;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button chkButton = FindViewById<Button>(Resource.Id.check);
            chkButton.Click += check;

            Button payButton = FindViewById<Button>(Resource.Id.pay);
            payButton.Click += pay;

            mHandler =  new Handler((msg) => {
                switch (msg.What)
                {
                    case SDK_PAY_FLAG:
                        {
                            PayResult payResult = new PayResult((string)msg.Obj);

                            // 支付宝返回此次支付结果及加签，建议对支付宝签名信息拿签约时支付宝提供的公钥做验签
                            string resultInfo = payResult.getResult();

                            string resultStatus = payResult.getResultStatus();

                            // 判断resultStatus 为“9000”则代表支付成功，具体状态码代表含义可参考接口文档
                            if (string.Equals(resultStatus, "9000"))
                            {
                                Toast.MakeText(this, "支付成功",
                                        ToastLength.Short).Show();
                            }
                            else
                            {
                                // 判断resultStatus 为非“9000”则代表可能支付失败
                                // “8000”代表支付结果因为支付渠道原因或者系统原因还在等待支付结果确认，最终交易是否成功以服务端异步通知为准（小概率状态）
                                if (string.Equals(resultStatus, "8000"))
                                {
                                    Toast.MakeText(this, "支付结果确认中",
                                            ToastLength.Short).Show();

                                }
                                else
                                {
                                    // 其他值就可以判断为支付失败，包括用户主动取消支付，或者系统返回的错误
                                    Toast.MakeText(this, "支付失败",
                                            ToastLength.Short).Show();

                                }
                            }
                            break;
                        }
                    case SDK_CHECK_FLAG:
                        {
                            Toast.MakeText(this, "检查结果为：" + msg.Obj,
                                    ToastLength.Short).Show();
                            break;
                        }
                    default:
                        break;
                }
            });

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }

        public void handleMessage(Message msg)
        {
            
        }

        /**
 * get the out_trade_no for an order. 生成商户订单号，该值在商户端应保持唯一（可自定义格式规范）
 * 
 */
        public string GetOutTradeNo()
        {
            string key = DateTime.Now.ToString("MMddHHmmss");

            Random r = new Random();
            key = key + r.Next();
            key = key.Substring(0, 15);
            return key;
        }

        /**
	 * create the order info. 创建订单信息
	 * 
	 */
        public string GetOrderInfo(string subject, string body, string price)
        {

            // 签约合作者身份ID
            string orderInfo = "partner=" + "\"" + PARTNER + "\"";

            // 签约卖家支付宝账号
            orderInfo += "&seller_id=" + "\"" + SELLER + "\"";

            // 商户网站唯一订单号
            orderInfo += "&out_trade_no=" + "\"" + GetOutTradeNo() + "\"";

            // 商品名称
            orderInfo += "&subject=" + "\"" + subject + "\"";

            // 商品详情
            orderInfo += "&body=" + "\"" + body + "\"";

            // 商品金额
            orderInfo += "&total_fee=" + "\"" + price + "\"";

            // 服务器异步通知页面路径
            orderInfo += "&notify_url=" + "\"" + "http://notify.msp.hk/notify.htm"
                    + "\"";

            // 服务接口名称， 固定值
            orderInfo += "&service=\"mobile.securitypay.pay\"";

            // 支付类型， 固定值
            orderInfo += "&payment_type=\"1\"";

            // 参数编码， 固定值
            orderInfo += "&_input_charset=\"utf-8\"";

            // 设置未付款交易的超时时间
            // 默认30分钟，一旦超时，该笔交易就会自动被关闭。
            // 取值范围：1m～15d。
            // m-分钟，h-小时，d-天，1c-当天（无论交易何时创建，都在0点关闭）。
            // 该参数数值不接受小数点，如1.5h，可转换为90m。
            orderInfo += "&it_b_pay=\"30m\"";

            // extern_token为经过快登授权获取到的alipay_open_id,带上此参数用户将使用授权的账户进行支付
            // orderInfo += "&extern_token=" + "\"" + extern_token + "\"";

            // 支付宝处理完请求后，当前页面跳转到商户指定页面的路径，可空
            orderInfo += "&return_url=\"m.alipay.com\"";

            // 调用银行卡支付，需配置此参数，参与签名， 固定值 （需要签约《无线银行卡快捷支付》才能使用）
            // orderInfo += "&paymethod=\"expressGateway\"";

            return orderInfo;
        }

        /**
	 * sign the order info. 对订单信息进行签名
	 * 
	 * @param content
	 *            待签名订单信息
	 */
        public string Sign(string content)
        {
            return SignUtils.sign(content, RSA_PRIVATE);
        }
        /**
	 * get the sign type we use. 获取签名方式
	 * 
	 */
        public string GetSignType()
        {
            return "sign_type=\"RSA\"";
        }
        public void pay(object o, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PARTNER) || string.IsNullOrWhiteSpace(RSA_PRIVATE)
                    || string.IsNullOrWhiteSpace(SELLER))
            {
                new AlertDialog.Builder(this)
                        .SetTitle("警告")
                        .SetMessage("需要配置PARTNER | RSA_PRIVATE| SELLER")
                        .SetPositiveButton("确定", (dialoginterface, i) => { Finish(); }

                                ).Show();
                return;
            }
            // 订单
            string orderInfo = GetOrderInfo("测试的商品", "该测试商品的详细描述", "0.01");

            // 对订单做RSA 签名
            string sign = Sign(orderInfo);
            try
            {
                // 仅需对sign 做URL编码
                sign = Java.Net.URLEncoder.Encode(sign, "UTF-8");
            }
            catch (UnsupportedEncodingException ex)
            {
                ex.PrintStackTrace();
            }
            finally
            {
                string payInfo = orderInfo + "&sign=\"" + sign + "\"&"
                                + GetSignType();
                // 完整的符合支付宝参数规范的订单信息
                Runnable payRunnable = new Runnable(()=> {
                    PayTask alipay = new PayTask(this);
                    // 调用支付接口，获取支付结果
                    string result = alipay.Pay(payInfo);

                    Message msg = new Message();
                    msg.What = SDK_PAY_FLAG;
                    msg.Obj = result;
                    mHandler.SendMessage(msg);
                });
                
                // 必须异步调用
                Thread payThread = new Thread(payRunnable);
                payThread.Start();
            }


        }

        /**
	 * check whether the device has authentication alipay account.
	 * 查询终端设备是否存在支付宝认证账户
	 * 
	 */
        public void check(object o, EventArgs e)
        {
            Runnable checkRunnable = new Runnable(()=> {
                PayTask payTask = new PayTask(this);
                // 调用查询接口，获取查询结果
                bool isExist = payTask.CheckAccountIfExist();

                Message msg = new Message();
                msg.What = SDK_CHECK_FLAG;
                msg.Obj = isExist;
                mHandler.SendMessage(msg);
            });

            Thread checkThread = new Thread(checkRunnable);
            checkThread.Start();

        }

        /**
 * get the sdk version. 获取SDK版本号
 * 
 */
        public void getSDKVersion()
        {
            PayTask payTask = new PayTask(this);
            string version = payTask.Version;
            Toast.MakeText(this, version, ToastLength.Short).Show();
        }

    }
}

