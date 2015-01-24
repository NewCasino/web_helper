
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

public class CnokcData
{
    public static string show_ticker(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        sb.Append("DATE".PR(10)+doc["date"].ToString() + M.N);
        sb.Append("BUY".PR(10)+doc["ticker"]["buy"].ToString() + M.N);
        sb.Append("HIGH".PR(10)+ doc["ticker"]["high"].ToString() + M.N);
        sb.Append("LAST".PR(10)+ doc["ticker"]["last"].ToString() + M.N);
        sb.Append("LOW".PR(10)+ doc["ticker"]["low"].ToString() + M.N);
        sb.Append("SELL".PR(10)+doc["ticker"]["sell"].ToString() + M.N);
        sb.Append("VOL".PR(10)+ doc["ticker"]["vol"].ToString() + M.N);
        return sb.ToString();
    } 
    public static string show_depth(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray asks = doc["asks"].AsBsonArray;
        BsonArray bids = doc["bids"].AsBsonArray;
        int min = int.MaxValue;
        if (asks.Count < min) min = asks.Count;
        if (bids.Count < min) min = bids.Count;
       
        sb.Append("ASKS".PR(10) + "".PR(10) + "BIDS".PR(10) + M.N);
        for (int i = 0; i < min; i++)
        {
            sb.Append(asks[i][0].PR(10) + asks[i][1].PR(10) + bids[i][0].PR(10) + bids[i][1].PR(10) + M.N);
        } 
        return sb.ToString();
    }

    public static void insert_depth(string result,string pair)
    { 
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray asks = doc["asks"].AsBsonArray;
        BsonArray bids = doc["bids"].AsBsonArray;

        BtcHelper.delete_depth("okcoin_cn", pair);
        BtcHelper.insert_depth_log("okcoin_cn", pair, "sell", "btc", asks.ToString());
        BtcHelper.insert_depth_log("okcoin_cn", pair, "buy", "btc", bids.ToString());
        for (int i = 0; i < asks.Count; i++)
        {
            BtcHelper.insert_depth("okcoin_cn", pair, "sell", "btc", asks[i][0].ToString(), asks[i][1].ToString());
        }
        for (int i = 0; i < bids.Count; i++)
        {
            BtcHelper.insert_depth("okcoin_cn", pair, "buy", "btc", bids[i][0].ToString(), bids[i][1].ToString());
        } 
    }
    public static void insert_ticker(string result,string pair)
    { 
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);
        BtcHelper.insert_ticker("okcoin_cn", pair, doc["ticker"]["last"].ToString(),
                                 doc["ticker"]["sell"].ToString(), doc["ticker"]["buy"].ToString(), doc["ticker"]["high"].ToString(), doc["ticker"]["low"].ToString(), doc["ticker"]["vol"].ToString());
        
    }
}