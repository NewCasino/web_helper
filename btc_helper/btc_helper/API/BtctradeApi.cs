using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


public class BtctradeApi
    {

        //PUBLIC
     
        public static string depth()
        {
            string url = "http://www.btctrade.com/api/depth/"; 
            return BtctradeHelper.query(url);
        }
        public static string ticker()
        {
            string url = "http://www.btctrade.com/api/ticker/";
            return BtctradeHelper.query(url);
        }
        //TRADE
       
    }
 
