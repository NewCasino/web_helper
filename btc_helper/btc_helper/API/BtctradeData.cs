
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MongoDB.Bson;

public class BtctradeData
{
     
    public static void insert_depth(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray asks = doc["asks"].AsBsonArray;
        BsonArray bids = doc["bids"].AsBsonArray; 

        BtcHelper.delete_depth("btctrade","btc_cny");
        BtcHelper.insert_depth_log("btctrade", "btc_cny", "sell", "btc", asks.ToString());
        BtcHelper.insert_depth_log("btctrade", "btc_cny", "buy", "btc", bids.ToString());
        for (int i = 0; i < asks.Count; i++)
        {
            BtcHelper.insert_depth("btctrade", "btc_cny", "sell", "btc", asks[i][0].ToString(), asks[i][1].ToString());
        }
        for (int i = 0; i < bids.Count; i++)
        {
            BtcHelper.insert_depth("btctrade", "btc_cny", "buy", "btc", bids[i][0].ToString(), bids[i][1].ToString());
        } 
    }
    public static void insert_ticker(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);
        BtcHelper.insert_ticker("btctrade", "btc_cny", doc["last"].ToString(), doc["sell"].ToString(), doc["buy"].ToString(), doc["high"].ToString(), doc["low"].ToString(), doc["vol"].ToString());
    }
 
}