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
using System.IO;



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
        str = str.Replace(",", "#");
        double output = 0;
        if (double.TryParse(str, out output) == false) return false;
        return true;
    }

    public static BsonDocument get_doc_result()
    {
        BsonDocument doc_result = new BsonDocument();
        doc_result.Add("data", "");
        doc_result.Add("url", "");
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
            string name_all = dt.Rows[0]["name_all"].ToString();
            string name_update = name_all;
            if (!name_all.Contains(name_other)) name_update = name_update + M.D + name_other;
            if (!name_all.Contains(name)) name_update = name_update + M.D + name;
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
            sql = string.Format(sql, name, Tool.drop_repeat(name + M.D + name_other));
            SQLServerHelper.exe_sql(sql);
        }
    }
    public static string insert_name_all(string name1, string name2)
    {

        string result = "";

        if (string.IsNullOrEmpty(name1) || string.IsNullOrEmpty(name2)) return "";
        string sql = "select * from names where replace(lower(name_all),' ','') like '%'+replace(lower('{0}'),' ','')+'%'";
        sql = string.Format(sql, name1);
        DataTable dt1 = SQLServerHelper.get_table(sql);
        sql = string.Format(sql, name2);
        DataTable dt2 = SQLServerHelper.get_table(sql);

        if (dt1.Rows.Count == 0 && dt2.Rows.Count == 0)
        {
            sql = "insert into names (name,name_all) values ('{0}','{1}')";
            sql = string.Format(sql, name1, Tool.drop_repeat(name1 + M.D + name2));
            SQLServerHelper.exe_sql(sql);
            result = name1;
        }
        string id = "";
        string name_all = "";
        string name_update = "";
        if (dt1.Rows.Count > 0 && dt2.Rows.Count == 0)
        {
            result = dt1.Rows[0]["name"].ToString();
            id = dt1.Rows[0]["id"].ToString();
            name_all = dt1.Rows[0]["name_all"].ToString();
            name_update = name_all;
            if (!name_all.Contains(name1)) name_update = name_update + M.D + name1;
            if (!name_all.Contains(name2)) name_update = name_update + M.D + name2;
            if (name_all != name_update)
            {
                sql = " update names set name_all='{0}' where id={1}";
                sql = string.Format(sql, Tool.drop_repeat(name_update), id);
                SQLServerHelper.exe_sql(sql);
            }
        }
        if (dt1.Rows.Count == 0 && dt2.Rows.Count > 0)
        {
            result = dt2.Rows[0]["name"].ToString();
            id = dt2.Rows[0]["id"].ToString();
            name_all = dt2.Rows[0]["name_all"].ToString();
            name_update = name_all;
            if (!name_all.Contains(name1)) name_update = name_update + M.D + name1;
            if (!name_all.Contains(name2)) name_update = name_update + M.D + name2;
            if (name_all != name_update)
            {
                sql = " update names set name_all='{0}' where id={1}";
                sql = string.Format(sql, Tool.drop_repeat(name_update), id);
                SQLServerHelper.exe_sql(sql);
            }
        }
        if (dt1.Rows.Count > 0 && dt2.Rows.Count > 0)
        {
            
            if (dt1.Rows[0]["name_all"].ToString().E_SPLIT(M.D).Length >= dt2.Rows[0]["name_all"].ToString().E_SPLIT(M.D).Length)
            {
                id = dt1.Rows[0]["id"].ToString();
                name_all = dt1.Rows[0]["name_all"].ToString();
                result = dt1.Rows[0]["name"].ToString();
            }
            else
            {
                id = dt2.Rows[0]["id"].ToString();
                name_all = dt2.Rows[0]["name_all"].ToString();
                result = dt2.Rows[0]["name"].ToString();
            }
            name_update = name_all;

            if (!name_all.Contains(name1)) name_update = name_update + M.D + name1;
            if (!name_all.Contains(name2)) name_update = name_update + M.D + name2;
            if (name_all != name_update)
            {
                sql = " update names set name_all='{0}' where id={1}";
                sql = string.Format(sql, Tool.drop_repeat(name_update), id);
                SQLServerHelper.exe_sql(sql);
            } 
        }

        return result;  
    }

    public static void insert_teams_log(string website, string season_name, string lg_name1, string lg_name2, string lg_name3, string name1, string name2, string name3)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        sql = " select * from teams_log where website='{0}'and season_name='{1}' and lg_name1='{2}'and lg_name2='{3}'and lg_name3='{4}'and name1='{5}'and name2='{6}' and name3='{7}'";
        sql = string.Format(sql, website, season_name, lg_name1, lg_name2, lg_name3, name1, name2, name3);
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

    public static DateTime convert_start_time(string start_time, string time_zone, string timespan)
    {

        //wether has date info
        bool is_has_date = true;

        start_time = start_time.Trim().ToLower();
        start_time = start_time.Replace('/', '-');

        //no date,get date by timezone & timespan
        if (!start_time.Contains("-"))
        {
            DateTime dt_span = Convert.ToDateTime(timespan);
            //convert to orgrin time zone
            dt_span = dt_span.AddHours(Convert.ToInt16(time_zone) - 8);
            start_time = dt_span.ToString("MM-dd") + "●" + start_time;
            is_has_date = false;
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
        if (string.IsNullOrEmpty(str_day) && pos1 > -1 && pos1 + 2 < len)
        {
            str_day = is_double_str(start_time.Substring(pos1 + 1, 2)) ? start_time.Substring(pos1 + 1, 2) : "";
        }
        if (string.IsNullOrEmpty(str_day) && pos1 > -1 && pos1 + 1 < len)
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
        if (string.IsNullOrEmpty(str_min) && pos2 > -1 && pos2 + 2 < len)
        {
            str_min = is_double_str(start_time.Substring(pos2 + 1, 2)) ? start_time.Substring(pos2 + 1, 2) : "";
        }
        if (string.IsNullOrEmpty(str_min) && pos2 > -1 && pos2 + 1 < len)
        {
            str_min = is_double_str(start_time.Substring(pos2 + 1, 1)) ? start_time.Substring(pos2 + 1, 1) : "";
        }

        str_date = Convert.ToInt16(str_month).ToString("00") + "-" + Convert.ToInt16(str_day).ToString("00");
        str_time = Convert.ToInt16(str_hour).ToString("00") + ":" + Convert.ToInt16(str_min).ToString("00");

        //convert pm time
        if (start_time.Contains("pm")) str_time = Tool.get_24h_from_12h(str_time);

        DateTime dt_return = Tool.get_time(str_date, str_time);

        //convert timezone
        dt_return = Tool.get_time_by_kind(dt_return, Convert.ToInt16(time_zone));

        //when has no date info and the date less than now
        if (is_has_date == false)
        {
            if ((dt_return - DateTime.Now).Seconds < 0)
            {
                dt_return = dt_return.AddDays(1);
            }
        }

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

    public static void create_log(string method_name, WebBrowser browser)
    {
        string html_path = @"D:\log\htmls\" + DateTime.Now.ToString("yyyyMMdd");

        if (!Directory.Exists(html_path)) Directory.CreateDirectory(html_path);
        html_path = html_path + @"\";

        Random random = new Random();
        string html = BrowserHelper.get_html(ref browser);
        string file_name = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + random.Next(100).ToString("000") + "_" + method_name + ".html";
        FileStream stream = (FileStream)File.Open(html_path + file_name, FileMode.Create);
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(html);
        writer.Close();
        stream.Close();


        string log_name = @"D:\log\log\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        if (!File.Exists(log_name))
        {
            FileStream stream_temp = (FileStream)File.Create(log_name);
            stream_temp.Close();
        }


        FileStream stream2 = (FileStream)File.Open(log_name, FileMode.Append);
        StreamWriter writer2 = new StreamWriter(stream2);
        string line = file_name.PR(50) + browser.Document.Url;
        writer2.WriteLine(line);
        writer2.Close();
        stream2.Close();
    }
    public static void create_log_result(string result)
    {
        string result_path = @"D:\log\results\" + DateTime.Now.ToString("yyyyMMdd");
        if (!Directory.Exists(result_path)) Directory.CreateDirectory(result_path);
        result_path = result_path + @"\";

        Random random = new Random(); 
        string file_name = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + random.Next(100).ToString("000")+ ".txt";
        FileStream stream = (FileStream)File.Open(result_path + file_name, FileMode.Create);
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(result);
        writer.Close();
        stream.Close();

    }
}

