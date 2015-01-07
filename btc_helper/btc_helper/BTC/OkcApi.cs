using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

 
    public class OkcApi
    {

        //PUBLIC
        public static string ticker(string pair)
        {
            string url = "https://www.okcoin.com/api/v1/ticker.do?symbol={0}";
            url = string.Format(url, pair);
            return OkcHelper.query(url); 
        }
        public static string depth(string pair)
        {
            string url = "https://www.okcoin.com/api/v1/depth.do?symbol={0}&size=200";
            url = string.Format(url, pair);
            return OkcHelper.query(url);
        }
        //TRADE
        public static string personal_info()
        { 
            Dictionary<string, string> list = new Dictionary<string, string>(); 
            return OkcHelper.query("https://www.okcoin.com/api/v1/userinfo.do",list);
        }
    }
 
