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

namespace AlipayTest
{
    public class PayResult
    {
        private string resultStatus;
        private string result;
        private string memo;

        public PayResult(string rawResult)
        {

            if (string.IsNullOrWhiteSpace(rawResult))
                return;

            string[] resultParams = rawResult.Split(';');
            foreach (string resultParam in resultParams)
            {
                if (resultParam.StartsWith("resultStatus"))
                {
                    resultStatus = GetValue(resultParam, "resultStatus");
                }
                if (resultParam.StartsWith("result"))
                {
                    result = GetValue(resultParam, "result");
                }
                if (resultParam.StartsWith("memo"))
                {
                    memo = GetValue(resultParam, "memo");
                }
            }
        }

        public override string ToString()
        {
            return "resultStatus={" + resultStatus + "};memo={" + memo
                    + "};result={" + result + "}";
        }

        private string GetValue(string content, string key)
        {
            string prefix = key + "={";
            return content.Substring(content.IndexOf(prefix) + prefix.Length,
                    content.LastIndexOf("}") - prefix.Length);
        }





        /**
 * @return the resultStatus
 */
        public string getResultStatus()
        {
            return resultStatus;
        }





        /**
 * @return the memo
 */
        public string getMemo()
        {
            return memo;
        }





        /**
 * @return the result
 */
        public string getResult()
        {
            return result;
        }
    }
}