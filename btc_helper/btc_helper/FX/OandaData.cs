
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

public class OandaData
{
     
    public static string  insert_list(string result)
    { 
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray list = doc["instruments"].AsBsonArray;
       

        
        for (int i = 0; i < list.Count; i++)
        {
            OandaSQL.insert_pairs(list[i]["instrument"].ToString(), list[i]["displayName"].ToString(), list[i]["pip"].ToString(), list[i]["maxTradeUnits"].ToString());
        }
        return sb.ToString();
    }
    public static string show_price(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray list = doc["prices"].AsBsonArray;



        for (int i = 0; i < list.Count; i++)
        {
            sb.Append(list[i]["instrument"].PR(20) + list[i]["time"].PR(50) + list[i]["bid"].PR(20) + list[i]["ask"].PR(20) + M.N);
        }
        return sb.ToString();
    }
    public static string show_candles(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray list = doc["candles"].AsBsonArray;

        for (int i = 0; i < list.Count; i++)
        {
            sb.Append(list[i]["time"].PR(30) + list[i]["openMid"].PR(20) + list[i]["highMid"].PR(20) + list[i]["lowMid"].PR(20)+ list[i]["closeMid"].PR(20)  + list[i]["volume"].PR(20)  +M.N);
        }
        return sb.ToString();
    }
    public static string show_candles_times(string result)
    {
        StringBuilder sb = new StringBuilder();
        BsonDocument doc = MongoHelper.get_doc_from_str(result);

        BsonArray list = doc["candles"].AsBsonArray;



        for (int i = 0; i < list.Count; i++)
        {
            sb.Append(list[i]["time"].PR(30) + list[i]["openBid"].PR(20) + list[i]["highBid"].PR(20) + list[i]["lowBid"].PR(20) + list[i]["lowAsk"].PR(20) + list[i]["closeBid"].PR(20) + list[i]["closeAsk"].PR(20) + list[i]["volume"].PR(20) + M.N);
        }
        return sb.ToString();
    }
}