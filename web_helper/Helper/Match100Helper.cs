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


    public static BsonDocument get_doc_result()
    {
        BsonDocument doc_result = new BsonDocument();
        doc_result.Add("data", "");
        doc_result.Add("loop", new BsonArray());
        return doc_result;
    }
    public static void insert_data(string company,string type,string start_time,string host,string client,string odd_win,string odd_draw,string odd_lose,string time_zone,string time_add)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        sql = " insert into europe_100_log " +
              " ( timespan,company,type,start_time,host,client,profit_win,profit_draw,profit_lose,time_zone,time_add,f_state) values" +
              " ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','0')";
        sql = string.Format(sql,timespan,company,type,start_time,host,client,odd_win,odd_draw,odd_lose,time_zone,time_add);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_future_match(string type, string time, string host, string client)
    {
        string sql = " delete  from future_match where type='{0}' and start_time='{1}' and host='{2}' and client='{3}'";
        sql = string.Format(sql, type, time, host, client);
        SQLServerHelper.exe_sql(sql);
        sql = "insert into future_match (type,start_time,host,client) values ('{0}','{1}','{2}','{3}')";
        sql = string.Format(sql, type, time, host, client);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_team(string name, string name_other)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(name_other)) return;
        
        string sql = "select * from teams where replace(lower(all_name),' ','') like '%'+replace(lower('{0}'),' ','')+'%'";
        sql = string.Format(sql, name);
        DataTable dt = SQLServerHelper.get_table(sql);
        if (dt.Rows.Count > 0)
        {
            sql = " update teams set all_name=all_name+'{0}' where replace(lower(all_name),' ','') like '%'+replace(lower('{1}'),' ','')+'%'";
            sql = string.Format(sql, name_other,name );
            SQLServerHelper.exe_sql(sql);
        } 
        else
        {
            sql = "insert into teams (name1,all_name) values ('{0}','{1}')";
            sql = string.Format(sql, name, name + "" + name_other);
            SQLServerHelper.exe_sql(sql);
        }

    }
    public static string convert_team_name(string name)
    {
        string result = "";
        string sql = " select name1,name2,all_name from teams  " +
                     " where replace(lower(all_name),' ','') like '%'+replace(lower('{0}'),' ','')+'%'";
        sql = string.Format(sql, name);
        DataTable dt = SQLServerHelper.get_table(sql);
        if (dt.Rows.Count > 0)
        {
            result = dt.Rows[0]["name1"].ToString();
        }
        return result;
    }
    public static DateTime convert_start_time(string start_time)
    {
        start_time = start_time.Trim().ToLower();
        start_time = start_time.Replace('/', '-');
        string result = "";
        string str_date = "";
        string str_time = "";
        string str_month = "";
        string str_day = "";
        string str_hour = "";
        string str_min = "";
        int len = start_time.Length; 

        int pos1 = start_time.IndexOf('-');
        if (string.IsNullOrEmpty(str_month) && pos1 > -1 && pos1 - 2 > 0)
        {
            str_month = is_double_str(start_time.Substring(pos1 - 2, 2)) ? start_time.Substring(pos1 - 2, 2) : "";
        }
        if (string.IsNullOrEmpty(str_month) && pos1 > -1 && pos1 - 1 > 0)
        {
            str_month = is_double_str(start_time.Substring(pos1 - 1, 1)) ? start_time.Substring(pos1 - 1, 1) : "";
        }
        if (string.IsNullOrEmpty(str_day) && pos1 > -1 && pos1 + 2 < len - 1)
        {
            str_day = is_double_str(start_time.Substring(pos1 + 1, 2)) ? start_time.Substring(pos1 + 1, 2) : "";
        }
        if (string.IsNullOrEmpty(str_day) && pos1 > -1 && pos1 + 1 < len - 1)
        {
            str_day = is_double_str(start_time.Substring(pos1 + 1, 1)) ? start_time.Substring(pos1 + 1, 1) : "";
        }

        int pos2 = start_time.IndexOf(':');
        if (string.IsNullOrEmpty(str_hour) && pos2 > -1 && pos2 - 2 > 0)
        {
            str_hour = is_double_str(start_time.Substring(pos2 - 2, 2)) ? start_time.Substring(pos2 - 2, 2) : "";
        }
        if (string.IsNullOrEmpty(str_hour) && pos2 > -1 && pos2 - 1 > 0)
        {
            str_hour = is_double_str(start_time.Substring(pos2 - 1, 1)) ? start_time.Substring(pos2 - 1, 1) : "";
        }
        if (string.IsNullOrEmpty(str_min) && pos2 > -1 && pos2 + 2 < len - 1)
        {
            str_min = is_double_str(start_time.Substring(pos2 + 1, 2)) ? start_time.Substring(pos2 + 1, 2) : "";
        }
        if (string.IsNullOrEmpty(str_min) && pos2 > -1 && pos2 + 1 < len - 1)
        {
            str_min = is_double_str(start_time.Substring(pos2 + 1, 1)) ? start_time.Substring(pos2 + 1, 1) : "";
        }

        str_date = Convert.ToInt16(str_month).ToString("00") + "-" + Convert.ToInt16(str_day).ToString("00");
        str_time = Convert.ToInt16(str_hour).ToString("00") + ":" + Convert.ToInt16(str_min).ToString("00");
        if (start_time.Contains("pm")) str_time = Tool.get_24h_from_12h(str_time);
        return Tool.get_time(str_date, str_time);
    } 
    public static DateTime round_time(DateTime dt)
    {
        int min = dt.Minute;
        if (min > 0 && min < 10) min = 10;
        if (min > 10 && min < 20) min = 20;
        if (min > 20 && min < 30) min = 30;
        if (min > 30 && min < 40) min = 40;
        if (min > 40 && min < 50) min = 50;
        if (min > 50 && min < 60)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour + 1, 0, 0);
        } 
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, min, 0);
    }
    public static string  get_date_from_week_no(string week_no)
    {
        if (week_no.Length >= 3) week_no = week_no.Substring(0, 3).ToLower();
        DateTime now = DateTime.Now;
        for (int i = 0;  i < 10; i++)
        {
            DateTime dt = now.AddDays(i);
            if (dt.DayOfWeek.ToString().Substring(0, 3).ToLower() == week_no)
            {
                return dt.ToString("MM-dd");
            }
        }
        return now.ToString("MM-dd");
    }
    public static DateTime  get_datetime_from_week_no(string week_no)
    {
        if (week_no.Length >= 3) week_no = week_no.Substring(0, 3).ToLower();
        DateTime now = DateTime.Now;
        for (int i = 0; i < 10; i++)
        {
            DateTime dt = now.AddDays(i);
            if (dt.DayOfWeek.ToString().Substring(0, 3).ToLower() == week_no)
            {
                return dt;
            }
        }
        return now;
    } 
  
}

