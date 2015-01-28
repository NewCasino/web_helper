
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

public class BtceData
{
    public static string show_info(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonDocument pairs = doc["pairs"].AsBsonDocument;
        foreach (BsonElement element in pairs)
        {
            sb.Append(element.Name.PR(10) +
                      element.Value.AsBsonDocument["decimal_places"].PR(10) +
                      element.Value.AsBsonDocument["min_price"].PR(10) +
                      element.Value.AsBsonDocument["max_price"].PR(10) +
                      element.Value.AsBsonDocument["min_amount"].PR(10) +
                      element.Value.AsBsonDocument["hidden"].PR(10) +
                      element.Value.AsBsonDocument["fee"].PR(10) + M.N);
        }

        return sb.ToString();
    }
    public static string show_tiker(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);
        sb.Append("HIGH".PR(10) + doc[0]["high"].PR(10) + M.N);
        sb.Append("LOW".PR(10) + doc[0]["low"].PR(10) + M.N);
        sb.Append("AVG".PR(10) + doc[0]["avg"].PR(10) + M.N);
        sb.Append("VOL".PR(10) + doc[0]["vol"].PR(10) + M.N);
        sb.Append("VOL_CUR".PR(10) + doc[0]["vol_cur"].PR(10) + M.N);
        sb.Append("LAST".PR(10) + doc[0]["last"].PR(10) + M.N);
        sb.Append("BUY".PR(10) + doc[0]["buy"].PR(10) + M.N);
        sb.Append("SELL".PR(10) + doc[0]["sell"].PR(10) + M.N);
        sb.Append("UPDATED".PR(10) + doc[0]["updated"].PR(10) + M.N);
        return sb.ToString();
    }
    public static string show_depth(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray asks = doc["btc_usd"]["asks"].AsBsonArray;
        BsonArray bids = doc["btc_usd"]["bids"].AsBsonArray;
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
    public static string show_trades(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);
        BsonArray trades = doc[0].AsBsonArray;
        for (int i = 0; i < trades.Count; i++)
        {
            sb.Append(trades[i]["type"].PR(10) +
                      trades[i]["price"].PR(10) +
                      trades[i]["amount"].PR(10) +
                      trades[i]["tid"].PR(10) +
                      trades[i]["timestamp"].PR(10) + M.N);
        }
        return sb.ToString();
    }
    public static string show_personal_info(string result)
    {

        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonDocument funds = doc["return"]["funds"].AsBsonDocument;
        sb.Append("FUNDS" + M.N);
        foreach (BsonElement element in funds)
        {
            sb.Append(element.Name.PR(10) + element.Value.PR(10) + M.N);
        }
        sb.Append("TRANSACTION COUNT:".PR(20) + doc["return"]["transaction_count"].PR(10) + M.N);
        return sb.ToString();
    }


    public static void  insert_depth(string result,string pair)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);



        BsonArray asks = doc[pair]["asks"].AsBsonArray;
        BsonArray bids = doc[pair]["bids"].AsBsonArray;

        BtcHelper.delete_depth("btce", pair);
        BtcHelper.insert_depth_log("btce", pair, "sell", "btc", asks.ToString());
        BtcHelper.insert_depth_log("btce", pair, "buy", "btc", bids.ToString());
        for (int i = 0; i < asks.Count; i++)
        {
            BtcHelper.insert_depth("btce", pair, "sell", "btc", asks[i][0].ToString(), asks[i][1].ToString());
            
        }
        for (int i = 0; i < bids.Count; i++)
        {
            BtcHelper.insert_depth("btce", pair, "buy", "btc", bids[i][0].ToString(), bids[i][1].ToString());
        }
      
    }

    public static void insert_ticker(string result,string pair)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BtcHelper.insert_ticker("btce", pair, doc[pair]["last"].ToString(), doc[pair]["buy"].ToString(),
                                doc[pair]["sell"].ToString(), doc[pair]["high"].ToString(), doc[pair]["low"].ToString(), doc[pair]["vol"].ToString());

   
    }
}