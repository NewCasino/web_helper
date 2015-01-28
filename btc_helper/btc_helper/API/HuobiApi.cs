using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


public class HuobiApi
    {

        //PUBLIC
     
        public static string depth(string pair)
        {
            

            string url = "http://api.huobi.com/staticmarket/depth_btc_json.js";
            switch (pair)
            {
                case "btc_cny":
                    url = "http://api.huobi.com/staticmarket/depth_btc_json.js";
                    break;
                case "ltc_cny":
                    url = "http://api.huobi.com/staticmarket/depth_ltc_json.js";
                    break;
                default:
                    break; 
            }
            return HuobiHelper.query(url);
        }
        public static string ticker(string pair)
        {
            string url = "http://api.huobi.com/staticmarket/ticker_btc_json.js"; 
            switch (pair)
            {
                case "btc_cny":
                    url = "http://api.huobi.com/staticmarket/ticker_btc_json.js";
                    break;
                case "ltc_cny":
                    url = "http://api.huobi.com/staticmarket/ticker_ltc_json.js";
                    break;
                default:
                    break;
            }
            return HuobiHelper.query(url);
        }
        //TRADE
       
    }
 
