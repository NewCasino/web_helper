using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.OleDb;
using System.Threading;
using mshtml;
using System.Reflection;
using System.Data;



class Match100Helper
{
    public static bool is_odd_str(string str)
    {
        if (str.Contains(".") == false) return false;

        double output = 0;
        if (double.TryParse(str, out output) == false) return false;

        return true;
    }
    public static bool is_double_str(string str)
    {
        double output = 0;
        if (double.TryParse(str, out output) == false) return false;
        return true;
    }
    public static string get_24h(string str)
    {
        try
        {
            int h = Convert.ToInt32(str.Substring(0, 2));
            h = h + 12;
            return h.ToString("00" + str.Substring(2, 3));
        }
        catch (Exception error)
        {
            return "error";
        }

    }
    public static string get_12m(string str)
    {
        //一月：January 
        //二月：February
        //三月：March
        //四月：April
        //五月：May 
        //六月：June 
        //七月：July
        //八月：August 
        //九月：September 
        //十月：October 
        //十一月：November
        //十二月：December
        str = str.ToLower();
        if (str.Contains("jan")) return "01";
        if (str.Contains("feb")) return "02";
        if (str.Contains("mar")) return "03";
        if (str.Contains("apr")) return "04";
        if (str.Contains("may")) return "05";
        if (str.Contains("jun")) return "06";
        if (str.Contains("jul")) return "07";
        if (str.Contains("aug")) return "08";
        if (str.Contains("sep")) return "09";
        if (str.Contains("oct")) return "10";
        if (str.Contains("nov")) return "11";
        if (str.Contains("dec")) return "12";
        return "00"; 
    }

    public static BsonDocument get_doc_result()
    {
        BsonDocument doc_result = new BsonDocument();
        doc_result.Add("data", "");
        doc_result.Add("loop", new BsonArray());
        return doc_result;
    } 
    public void insert_data(string company, string type, string start_time, string host, string client, string odd_win, string odd_draw, string odd_lose)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        sql = " insert into europe_100_log " +
           " (start_time,host,client,company,timespan," +
           "  profit_win,profit_draw,profit_lose,persent_win,persent_draw,persent_lose,persent_return,kelly_win,kelly_draw,kelly_lose," +
           "  start_profit_win,start_profit_draw,start_profit_lose,start_persent_win,start_persent_draw,start_persent_lose," +
           "  start_persent_return,start_kelly_win,start_kelly_draw,start_kelly_lose,type)" +
           "  values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}'," +
           "          '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}')";
        sql = string.Format(sql, start_time, host, client, company, timespan,
                         odd_win, odd_draw, odd_lose, "", "", "", "", "", "", "",
                         "", "", "", "", "", "",
                         "", "", "", "", type);
        SQLServerHelper.exe_sql(sql);
    }
   
}

