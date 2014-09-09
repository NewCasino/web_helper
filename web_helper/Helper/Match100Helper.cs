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
        str=str.Replace(",", "#");
        double output = 0;
        if (double.TryParse(str, out output) == false) return false;
        return true;
    } 
    
    public static BsonDocument get_doc_result()
    {
        BsonDocument doc_result = new BsonDocument();
        doc_result.Add("data", "");
        doc_result.Add("used", new BsonArray());
        doc_result.Add("loop", new BsonArray()); 
        return doc_result;
    }
    public static void insert_data(string website, string league, string start_time, string host, string client, string odd_win, string odd_draw, string odd_lose, string time_zone, string time_add)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        league = league.Replace("'", " ");
        host = host.Replace("'", " ");
        client = client.Replace("'", " ");
        sql = " insert into europe_100_log " +
              " ( timespan,website,league,start_time,host,client,odd_win,odd_draw,odd_lose,time_zone,time_add,f_state) values" +
              " ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','0')";
        sql = string.Format(sql, timespan, website, league, start_time, host, client, odd_win, odd_draw, odd_lose, time_zone, time_add);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_future_match(string league, string time, string host, string client)
    {
        string sql = " delete  from future_match where league='{0}' and start_time='{1}' and host='{2}' and client='{3}'";
        sql = string.Format(sql, league, time, host, client);
        SQLServerHelper.exe_sql(sql);
        sql = "insert into future_match (league,start_time,host,client) values ('{0}','{1}','{2}','{3}')";
        sql = string.Format(sql, league, time, host, client);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_name(string name, string name_other)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(name_other)) return;

        string name_like = name;
        //string name_like = name.Replace("FC", "");
        //name_like = name_like.Replace("(", "");
        //name_like = name_like.Replace(")", "");
      
        string sql = "select * from names where replace(lower(name_all),' ','') like '%'+replace(lower('{0}'),' ','')+'%'";
        //string sql = "select * from names where replace(name_all,' ','') like '%'+replace('{0}',' ','')+'%'";
        sql = string.Format(sql, name_like);
        DataTable dt = SQLServerHelper.get_table(sql);
        if (dt.Rows.Count > 0)
        {
            string id = dt.Rows[0]["id"].ToString();
            string name_all=dt.Rows[0]["name_all"].ToString();
            string name_update = name_all;
            if(!name_all.Contains(name_other)) name_update=name_update+"●" + name_other; 
            if ( !name_all.Contains(name)) name_update = name_update + "●" + name;
            if (name_all != name_update)
            {
                sql = " update names set name_all='{0}' where id={1}";
                sql = string.Format(sql, Tool.drop_repeat(name_update), id);
                SQLServerHelper.exe_sql(sql);
            }
        }
        else
        {
            sql = "insert into names (name,name_all) values ('{0}','{1}')";
            sql = string.Format(sql, name,Tool.drop_repeat( name + "●" + name_other));
            SQLServerHelper.exe_sql(sql);
        } 
    }
    public static void insert_teams_log(string website, string season_name, string lg_name1, string lg_name2, string lg_name3, string name1, string name2, string name3)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        sql = " select * from teams_log where website='{0}'and season_name='{1}' and lg_name1='{2}'and lg_name2='{3}'and lg_name3='{4}'and name1='{5}'and name2='{6}' and name3='{7}'";
        sql = string.Format(sql,website,season_name,lg_name1,lg_name2,lg_name3,name1,name2,name3);
        if (SQLServerHelper.get_table(sql).Rows.Count == 0)
        {
            sql = "   insert into teams_log (timespan,website,season_name,lg_name1,lg_name2,lg_name3,lg_name_all,name1,name2,name3,name_all) values" +
                  "   ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
            sql = string.Format(sql, timespan, website, season_name,
                                 lg_name1, lg_name2, lg_name3, lg_name1 + "●" + lg_name2 + "●" + lg_name3,
                                 name1, name2, name3, name1 + "●" + name2 + "●" + name3);
            SQLServerHelper.exe_sql(sql);

        }
    }
    public static string convert_team_name(string name)
    {
        string name_like = name;
       //string name_like = name.Replace("FC", "");
       //name_like = name_like.Replace("(", "");
       //name_like = name_like.Replace(")", "");

        string result = "";
        string sql = " select * from names  " +
                     " where replace(lower(name_all),' ','') like '%'+replace(lower('{0}'),' ','')+'%'";
        //string sql = " select * from names  " +
           //          " where replace(name_all,' ','') like '%'+replace('{0}',' ','')+'%'";
        sql = string.Format(sql, name_like);
        DataTable dt = SQLServerHelper.get_table(sql);
        if (dt.Rows.Count > 0)
        {
            result = dt.Rows[0]["name"].ToString();
        }
        return result;
    }

    public static DateTime convert_start_time(string start_time,string time_zone,string timespan)
    {
        start_time = start_time.Trim().ToLower();
        start_time = start_time.Replace('/', '-');

        //no date,get date by timezone & timespan
        if (!start_time.Contains("-"))
        {
            DateTime dt_span = Convert.ToDateTime(timespan);
            DateTime dt_convert = Tool.get_time_by_kind(dt_span, Convert.ToInt16(time_zone));
            start_time = dt_convert.ToString("MM-dd") + "●" + start_time; 
        }


        string result = "";
        string str_date = "";
        string str_time = "";
        string str_month = "";
        string str_day = "";
        string str_hour = "";
        string str_min = "";
        int len = start_time.Length;

        int pos1 = start_time.IndexOf('-');
        if (string.IsNullOrEmpty(str_month) && pos1 > -1 && pos1 - 2 >= 0)
        {
            str_month = is_double_str(start_time.Substring(pos1 - 2, 2)) ? start_time.Substring(pos1 - 2, 2) : "";
        }
        if (string.IsNullOrEmpty(str_month) && pos1 > -1 && pos1 - 1 >= 0)
        {
            str_month = is_double_str(start_time.Substring(pos1 - 1, 1)) ? start_time.Substring(pos1 - 1, 1) : "";
        }
        if (string.IsNullOrEmpty(str_day) && pos1 > -1 && pos1 + 2 < len )
        {
            str_day = is_double_str(start_time.Substring(pos1 + 1, 2)) ? start_time.Substring(pos1 + 1, 2) : "";
        }
        if (string.IsNullOrEmpty(str_day) && pos1 > -1 && pos1 + 1 < len )
        {
            str_day = is_double_str(start_time.Substring(pos1 + 1, 1)) ? start_time.Substring(pos1 + 1, 1) : "";
        }

        int pos2 = start_time.IndexOf(':');
        if (string.IsNullOrEmpty(str_hour) && pos2 > -1 && pos2 - 2 >= 0)
        {
            str_hour = is_double_str(start_time.Substring(pos2 - 2, 2)) ? start_time.Substring(pos2 - 2, 2) : "";
        }
        if (string.IsNullOrEmpty(str_hour) && pos2 > -1 && pos2 - 1 >= 0)
        {
            str_hour = is_double_str(start_time.Substring(pos2 - 1, 1)) ? start_time.Substring(pos2 - 1, 1) : "";
        }
        if (string.IsNullOrEmpty(str_min) && pos2 > -1 && pos2 + 2 < len )
        {
            str_min = is_double_str(start_time.Substring(pos2 + 1, 2)) ? start_time.Substring(pos2 + 1, 2) : "";
        }
        if (string.IsNullOrEmpty(str_min) && pos2 > -1 && pos2 + 1 < len )
        {
            str_min = is_double_str(start_time.Substring(pos2 + 1, 1)) ? start_time.Substring(pos2 + 1, 1) : "";
        }

        str_date = Convert.ToInt16(str_month).ToString("00") + "-" + Convert.ToInt16(str_day).ToString("00");
        str_time = Convert.ToInt16(str_hour).ToString("00") + ":" + Convert.ToInt16(str_min).ToString("00");
        
        //convert pm time
        if (start_time.Contains("pm")) str_time = Tool.get_24h_from_12h(str_time);

        DateTime dt_return =Tool.get_time(str_date, str_time); 

        //convert timezone
        dt_return = Tool.get_time_by_kind(dt_return, Convert.ToInt16(time_zone)); 

        return dt_return;
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
    public static string get_date_from_week_no(string week_no)
    {
        if (week_no.Length >= 3) week_no = week_no.Substring(0, 3).ToLower();
        DateTime now = DateTime.Now;
        for (int i = 0; i < 10; i++)
        {
            DateTime dt = now.AddDays(i);
            if (dt.DayOfWeek.ToString().Substring(0, 3).ToLower() == week_no)
            {
                return dt.ToString("MM-dd");
            }
        }
        return now.ToString("MM-dd");
    }
    public static DateTime get_datetime_from_week_no(string week_no)
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

    public static BsonDocument get_odd_doc_from_europe(string win, string draw, string lose)
    {
        BsonDocument doc = new BsonDocument();

        double d_win = Convert.ToDouble(win);
        double d_draw = Convert.ToDouble(draw);
        double d_lose = Convert.ToDouble(lose);

        double d_return_persent = (1 / (1 / d_win + 1 / d_draw + 1 / d_lose)) * 100;
        double d_win_persent = d_return_persent / d_win;
        double d_draw_persent = d_return_persent / d_draw;
        double d_lose_persent = d_return_persent / d_lose;


        doc.Add("win", d_win.ToString("###.000"));
        doc.Add("darw", d_draw.ToString("###.000"));
        doc.Add("lose", d_lose.ToString("###.000"));
        doc.Add("return_persent", Math.Round(d_return_persent, 3).ToString());
        doc.Add("win_persent", Math.Round(d_win_persent, 3).ToString());
        doc.Add("draw_persent", Math.Round(d_draw_persent, 3).ToString());
        doc.Add("lose_persent", Math.Round(d_lose_persent, 3).ToString());

        return doc;
    }
    public static BsonDocument get_odd_doc_from_english(string win, string draw, string lose)
    {
        BsonDocument doc = new BsonDocument();

        double d_win = Convert.ToDouble(convert_english_odd(win));
        double d_draw = Convert.ToDouble(convert_english_odd(draw));
        double d_lose = Convert.ToDouble(convert_english_odd(lose));

        double d_return_persent = (1 / (1 / d_win + 1 / d_draw + 1 / d_lose)) * 100;
        double d_win_persent = d_return_persent / d_win;
        double d_draw_persent = d_return_persent / d_draw;
        double d_lose_persent = d_return_persent / d_lose;


        doc.Add("win", d_win.ToString("###.000"));
        doc.Add("darw", d_draw.ToString("###.000"));
        doc.Add("lose", d_lose.ToString("###.000"));
        doc.Add("return_persent", Math.Round(d_return_persent, 3).ToString());
        doc.Add("win_persent", Math.Round(d_win_persent, 3).ToString());
        doc.Add("draw_persent", Math.Round(d_draw_persent, 3).ToString());
        doc.Add("lose_persent", Math.Round(d_lose_persent, 3).ToString());

        return doc;
    }
    public static BsonDocument get_odd_doc_from_ameriaca(string win, string draw, string lose)
    {
        BsonDocument doc = new BsonDocument();

        double d_win = Convert.ToDouble(convert_ameriaca_odd(win));
        double d_draw = Convert.ToDouble(convert_ameriaca_odd(draw));
        double d_lose = Convert.ToDouble(convert_ameriaca_odd(lose));

        double d_return_persent = (1 / (1 / d_win + 1 / d_draw + 1 / d_lose)) * 100;
        double d_win_persent = d_return_persent / d_win;
        double d_draw_persent = d_return_persent / d_draw;
        double d_lose_persent = d_return_persent / d_lose;


        doc.Add("win", d_win.ToString("###.000"));
        doc.Add("darw", d_draw.ToString("###.000"));
        doc.Add("lose", d_lose.ToString("###.000"));
        doc.Add("return_persent", Math.Round(d_return_persent, 3).ToString());
        doc.Add("win_persent", Math.Round(d_win_persent, 3).ToString());
        doc.Add("draw_persent", Math.Round(d_draw_persent, 3).ToString());
        doc.Add("lose_persent", Math.Round(d_lose_persent, 3).ToString());

        return doc;
    }
    public static string convert_english_odd(string str)
    {
        string result = "";
        string[] list = str.Split('/');
        if (list.Length == 2)
        {
            double d1 = Convert.ToDouble(list[0].ToString().Trim());
            double d2 = Convert.ToDouble(list[1].ToString().Trim());
            result = Math.Round(d1 / d2 + 1, 3).ToString("###.000");

        }
        return result;
    }
    public static string convert_ameriaca_odd(string str)
    {
        string result = "";
        double d1 = Convert.ToDouble(str.Replace("-", "").Replace("+", "").Trim());
        if (str.Contains("-"))
        {
            result = Math.Round(100 / d1 + 1, 3).ToString("###.000");
        }
        else
        {
            result = Math.Round(d1 / 100 + 1, 3).ToString("###.000");
        }
        return result;
    }
}

