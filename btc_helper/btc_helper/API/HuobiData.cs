
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

public class HuobiData
{
     
    public static void insert_depth(string result,string pair)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray asks = doc["asks"].AsBsonArray;
        BsonArray bids = doc["bids"].AsBsonArray;

        BtcHelper.delete_depth("huobi", pair);
        BtcHelper.insert_depth_log("huobi", pair, "sell", "btc", asks.ToString());
        BtcHelper.insert_depth_log("huobi", pair, "buy", "btc", bids.ToString());
        for (int i = 0; i < asks.Count; i++)
        {
            BtcHelper.insert_depth("huobi", pair, "sell", "btc", asks[i][0].ToString(), asks[i][1].ToString());
        }
        for (int i = 0; i < bids.Count; i++)
        {
            BtcHelper.insert_depth("huobi", pair, "buy", "btc", bids[i][0].ToString(), bids[i][1].ToString());
        } 
    }
    public static void insert_ticker(string result,string  pair)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);
        BtcHelper.insert_ticker("huobi", pair, doc["ticker"]["last"].ToString(), doc["ticker"]["sell"].ToString(), doc["ticker"]["buy"].ToString(), doc["ticker"]["high"].ToString(), doc["ticker"]["low"].ToString(), doc["ticker"]["vol"].ToString());
    }
 
}