using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;
using HtmlAgilityPack;
using System.Collections;


class CurrencyHelper
{
    public static void insert_currency(string website,string c_from,string c_to,string c_rate)
    {
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff");
        string sql = "";

        sql = "delete from currency where website='{0}' and c_from='{1}'and c_to='{2}'";
        sql = string.Format(sql, website, c_from, c_to);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into currency (timespan,website,c_from,c_to,c_rate) values('{0}','{1}','{2}','{3}','{4}')";
        sql = string.Format(sql, timespan, website, c_from, c_to, c_rate);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into currency_log (timespan,website,c_from,c_to,c_rate) values('{0}','{1}','{2}','{3}','{4}')";
        sql = string.Format(sql, timespan, website, c_from, c_to, c_rate);
        SQLServerHelper.exe_sql(sql); 
    }
    public static string  get_simple_name(string cn_name)
    {
        switch (cn_name)
        {

            case "美元":
                return "usd";
                break;
            case "欧元":
                return "eur";
                break;
            case "港币":
                return "hkd";
                break;
            case "新西兰元":
                return "nzd";
                break;
            case "澳大利亚元":
                return "aud";
                break;
            case "加拿大元":
                return "cad";
                break;
            case "英镑":
                return "gbp";
                break;
            case "日元":
                return "jpy";
                break;
            case "新加坡元":
                return "sgd";
                break;
            case "瑞士法郎":
                return "chf";
                break;
            default:
                return "no";
                break;

        }
    }
    public static double get_rate(string from, string to)
    {
        string sql = "";
        sql = "select c_rate from currency where website='cmbchina' and c_from='{0}' and c_to='{1}'";
        sql = string.Format(sql,from, to);
        double rate = Convert.ToDouble(SQLServerHelper.get_table(sql).Rows[0][0].ToString());
        return rate;
    }
}

