using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Pair
{
    btc_usd,
    btc_cny,
    ltc_usd,
    ltc_cny,
    Unknown
}
class BtcHelper
{ 
    public static void delete_depth(string website, string currency)
    {
        string sql = "delete  from depth where website='{0}' and currency='{1}'";
        sql = string.Format(sql, website, currency);
        SQLServerHelper.exe_sql(sql);

    }
    public static void insert_depth(string website, string currency, string type, string qty_type, string price, string qty)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff");


        sql = "insert into depth (timespan,website,currency,type,qty_type,price,qty) values('{0}','{1}','{2}','{3}','{4}',{5},{6})";
        sql = string.Format(sql, timespan, website, currency, type, qty_type, price, qty);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_depth_log(string website, string currency, string type, string qty_type, string text)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff");


        sql = "insert into depth_log (timespan,website,currency,type,qty_type,text) values('{0}','{1}','{2}','{3}','{4}','{5}')";
        sql = string.Format(sql, timespan, website, currency, type, qty_type, text);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_ticker(string website, string type, string last, string sell, string buy, string hight, string low, string vol)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff");

        sql = "delete ticker where website='{0}' and currency='{1}'";
        sql = string.Format(sql, website, type);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into ticker (timespan,website,currency,last,sell,buy,high,low,vol) values('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8})";
        sql = string.Format(sql, timespan, website, type, last, sell, buy, hight, low, vol);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into ticker_log (timespan,website,currency,last,sell,buy,high,low,vol) values('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8})";
        sql = string.Format(sql, timespan, website, type, last, sell, buy, hight, low, vol);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_trade_log(string website, string currency, string type, string qty_type, string text)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff");
        sql = "insert into trade_log (timespan,website,currency,type,qty_type,text) values('{0}','{1}','{2}','{3}','{4}','{5}')";
        sql = string.Format(sql, timespan, website, currency, type, qty_type, text);
        SQLServerHelper.exe_sql(sql);
    }
    public static string get_type_from_pair(string pair)
    {
        string result = "";
        pair = pair.ToLower();
        if (pair.Contains("btc") && pair.Contains("usd")) result = "btc_usd";
        if (pair.Contains("btc") && pair.Contains("cny")) result = "btc_cny";
        if (pair.Contains("ltc") && pair.Contains("usd")) result = "ltc_usd";
        if (pair.Contains("ltc") && pair.Contains("cny")) result = "ltc_cny";
        return result; 
    }
}

