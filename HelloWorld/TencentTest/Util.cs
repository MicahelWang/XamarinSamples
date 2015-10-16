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
using System.Net;
using Java.Security;
using Org.Apache.Http.Conn.Ssl;
using Javax.Net.Ssl;
using Org.Apache.Http.Impl.Client;
using Java.Security.Cert;
using Java.Net;
using Org.Apache.Http.Params;
using Org.Apache.Http.Protocol;
using Org.Apache.Http;
using Org.Apache.Http.Conn.Schemes;
using Org.Apache.Http.Conn;
using Org.Apache.Http.Impl.Conn.Tsccm;
using Org.Apache.Http.Client;
using Org.Apache.Http.Client.Methods;
using Org.Apache.Http.Entity;
using Org.Apache.Http.Util;

namespace TencentTest
{
    public class Util
    {
        public static byte[] httpPost(string url, string entity)
        {
            if (url == null || url.Length == 0)
            {
                return null;
            }

            IHttpClient httpClient = getNewHttpClient();

            HttpPost httpPost = new HttpPost(url);

            try
            {
                httpPost.Entity = new StringEntity(entity);
                httpPost.SetHeader("Accept", "application/json");
                httpPost.SetHeader("Content-type", "application/json");

                IHttpResponse resp = httpClient.Execute(httpPost);
                if (resp.StatusLine.StatusCode != Org.Apache.Http.HttpStatus.ScOk)
                {
                    return null;
                }

                return EntityUtils.ToByteArray(resp.Entity);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static DefaultHttpClient getNewHttpClient()
        {
            try
            {
                KeyStore trustStore = KeyStore.GetInstance(KeyStore.DefaultType);
                trustStore.Load(null, null);

                Org.Apache.Http.Conn.Ssl.SSLSocketFactory sf = new SSLSocketFactoryEx(trustStore);
                sf.HostnameVerifier = Org.Apache.Http.Conn.Ssl.SSLSocketFactory.AllowAllHostnameVerifier;

                IHttpParams parameters = new BasicHttpParams();
                HttpProtocolParams.SetVersion(parameters, Org.Apache.Http.HttpVersion.Http11);
                HttpProtocolParams.SetContentCharset(parameters, HTTP.Utf8);

                SchemeRegistry registry = new SchemeRegistry();
                registry.Register(new Scheme("http", PlainSocketFactory.SocketFactory, 80));
                registry.Register(new Scheme("https", sf, 443));

                IClientConnectionManager ccm = new ThreadSafeClientConnManager(parameters, registry);

                return new DefaultHttpClient(ccm, parameters);
            }
            catch (Exception e)
            {
                return new DefaultHttpClient();
            }
        }



        public class X509TrustManager : Java.Lang.Object, IX509TrustManager
        {
            public void CheckClientTrusted(X509Certificate[] chain, string authType)
            {

            }

            public void CheckServerTrusted(X509Certificate[] chain, string authType)
            {

            }

            public X509Certificate[] GetAcceptedIssuers()
            {
                return null;
            }
        }
        private class SSLSocketFactoryEx : Org.Apache.Http.Conn.Ssl.SSLSocketFactory
        {

            SSLContext sslContext = SSLContext.GetInstance("TLS");

            public SSLSocketFactoryEx(KeyStore truststore) : base(truststore)
            {
                ITrustManager tm = new X509TrustManager();
                sslContext.Init(null, new ITrustManager[] { tm }, null);
            }


            public override Socket CreateSocket(Socket socket, string host, int port, bool autoClose)
            {
                return sslContext.SocketFactory.CreateSocket(socket, host, port, autoClose);
            }

            public override Socket CreateSocket()
            {
                return sslContext.SocketFactory.CreateSocket();
            }


        }
    }
}