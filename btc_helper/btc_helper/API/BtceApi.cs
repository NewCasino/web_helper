

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

public class BtceApi
{
    //PUBLIC
    public static string info()
    {
        string url = "https://btc-e.com/api/3/info";
        return BtceHelper.query(url);
    }
    public static string tiker(string pair)
    {
        string url = "https://btc-e.com/api/3/ticker/{0}";
        url = string.Format(url, pair);
        return BtceHelper.query(url);
    }
    public static string depth(string pair)
    {
        string url = "https://btc-e.com/api/3/depth/{0}?limit=2000";
        url = string.Format(url, pair);
        return BtceHelper.query(url);
    }
    public static string trades(string pair)
    { 
        string url = "https://btc-e.com/api/3/trades/{0}";
        url = string.Format(url, pair);
        return BtceHelper.query(url);
    }


    //TRADE
    public static string personal_info()
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("method", "getInfo");
        return BtceHelper.query(list);
    }
    public static string trade(string pair, string type, string rate, string amount)
    {

        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("method", "Trade");
        list.Add("pair", pair);
        list.Add("type", type);
        list.Add("rate", rate);
        list.Add("amount", amount);
        return BtceHelper.query(list);
    }

}