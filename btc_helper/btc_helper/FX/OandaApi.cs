using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data;
using MongoDB.Bson;

public class OandaApi
{

    //PUBLIC

    public static string list()
    { 
        return OandaHelper.query("https://api-fxtrade.oanda.com/v1/instruments?accountId=904796"); 
    }
    public static string price()
    {
        string pairs = OandaSQL.get_all_pairs();
        string url = "https://api-fxtrade.oanda.com/v1/prices?instruments={0}";
        url = string.Format(url, pairs);
        return OandaHelper.query(url);
    } 
    public static string candles()
    {
        return OandaHelper.query("https://api-fxtrade.oanda.com/v1/candles?instrument=EUR_USD&count=100&candleFormat=midpoint&granularity=S5&dailyAlignment=0&alignmentTimezone=America%2FNew_York");
    }
    
    public static string candles_time()
    {
        return OandaHelper.query("https://api-fxtrade.oanda.com/v1/candles?instrument=EUR_USD&start=2014-06-19T15%3A47%3A40Z&end=2014-06-19T15%3A47%3A50Z"); 
    }

}

