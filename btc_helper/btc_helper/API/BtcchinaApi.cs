using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using MongoDB.Bson;

public class BtcchinaApi
    {

        //PUBLIC
     
        public static string depth(string pair)
        {
            string url = "https://data.btcchina.com/data/orderbook?market=btccny";
            switch (pair)
            {
                case "btc_cny":
                    url = "https://data.btcchina.com/data/orderbook?market=btccny";
                    break;
                case "ltc_cny":
                    url = "https://data.btcchina.com/data/orderbook?market=ltccny";
                    break;
                default:
                    break;
            } 
            return BtcchinaHelper.query(url);
        }
        public static string ticker(string pair)
        {
            string url = "https://data.btcchina.com/data/ticker?market=btccny"; 
            switch (pair)
            {
                case "btc_cny":
                    url = "https://data.btcchina.com/data/ticker?market=btccny";
                    break;
                case "ltc_cny":
                    url = "https://data.btcchina.com/data/ticker?market=ltccny";
                    break;
                default:
                    break;
            }  
            return BtcchinaHelper.query(url);
        }

        public static string trade(string pair,string date)
        {
            string url = "https://data.btcchina.com/data/historydata?since={0}&limit=5000&sincetype=time"; 
           
            url = string.Format(url, date);
            return BtcchinaHelper.query(url);
        }
        public static string trade_by_id(string pair, string id)
        {
            string url = "https://data.btcchina.com/data/historydata?since={0}&limit=5000";

            url = string.Format(url, id);
            return BtcchinaHelper.query(url);
        }

        //TRADE
        public static string personal_info()
        {
            BsonArray param = new BsonArray();
            
            return BtcchinaHelper.query("https://api.btcchina.com/api_trade_v1.php","getAccountInfo",param); 
        } 
    }
 
