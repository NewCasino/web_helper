
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

public class BtcchinaData
{
     
    public static void insert_depth(string result,string pair)
    { 
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray asks = doc["asks"].AsBsonArray;
        BsonArray bids = doc["bids"].AsBsonArray;
        string time = doc["date"].ToString() + "000";
        BtcHelper.delete_depth("btcchina", pair);
        BtcHelper.insert_depth_log("btcchina", pair, "sell", "btc", asks.ToString(),time);
        BtcHelper.insert_depth_log("btcchina", pair, "buy", "btc", bids.ToString(),time);
        for (int i = 0; i < asks.Count; i++)
        {
            BtcHelper.insert_depth("btcchina", pair, "sell", "btc", asks[i][0].ToString(), asks[i][1].ToString(),time);
        }
        for (int i = 0; i < bids.Count; i++)
        {
            BtcHelper.insert_depth("btcchina", pair, "buy", "btc", bids[i][0].ToString(), bids[i][1].ToString(),time);
        } 
    }
    public static void insert_ticker(string result,string pair)
    {
        
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);
        BtcHelper.insert_ticker("btcchina", pair, doc["ticker"]["last"].ToString(), doc["ticker"]["sell"].ToString(), doc["ticker"]["buy"].ToString(), doc["ticker"]["high"].ToString(), doc["ticker"]["low"].ToString(), doc["ticker"]["vol"].ToString(),doc["ticker"]["date"].ToString()+"000");
    }

    public static void insert_trade(string result, string pair)
    {
        StringBuilder sb = new StringBuilder();
        BsonArray list = MongoHelper.get_array_from_str(result);
        BsonArray sell=new BsonArray();
        BsonArray buy=new BsonArray();
        for (int i = 0; i < list.Count; i++)
        {
            BsonDocument doc_item = new BsonDocument();
            doc_item["id"] = list[i]["tid"].ToString();
            doc_item["time"] = list[i]["date"].ToString();
            doc_item["price"] = list[i]["price"].ToString();
            doc_item["qty"] = list[i]["amount"].ToString(); 

            if (list[i]["type"].ToString() == "sell")
            {
                sell.Add(doc_item);
            }
            if (list[i]["type"].ToString() == "buy")
            {
                buy.Add(doc_item);
            }
        }
        BtcHelper.insert_trade_log("btcchina", pair, "sell", "btc", sell.ToString());
        BtcHelper.insert_trade_log("btcchina", pair, "buy", "btc", buy.ToString());

    }
}