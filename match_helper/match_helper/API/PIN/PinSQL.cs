using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

class PinSQL
{

    public static void insert_sport(string sport_id, string sport_name)
    {
        string sql = "delete from s_pin_sports where sport_id={0}";
        sql = string.Format(sql, sport_id);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into s_pin_sports (sport_id,sport_name) values ( {0} ,'{1}')";
        sql = string.Format(sql, sport_id, sport_name);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_league(string sport_id, string league_id, string league_name)
    {
        string sql = "delete from s_pin_leagues where league_id={0}";
        sql = string.Format(sql, league_id);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into s_pin_leagues (sport_id,league_id,league_name) values ( {0} , {1} ,'{2}')";
        sql = string.Format(sql, sport_id, league_id, league_name);
        SQLServerHelper.exe_sql(sql);
    }
    public static string get_league_name(string sport_id, string league_id)
    {
        string sql = "select * from s_pin_leagues where sport_id='{0}' and league_id='{1}'";
        sql = string.Format(sql, sport_id, league_id);
        DataTable dt = SQLServerHelper.get_table(sql);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0][0].ToString();
        }
        else
        {
            return "";
        }
    }
    public static void insert_event(string sport_id, string league_id, string event_id, string start_time, string home, string away)
    {
        string sql = "delete from s_pin_events where event_id={0}";
        sql = string.Format(sql, event_id);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into s_pin_events(sport_id,league_id,event_id,start_time,home,away) values ({0},{1},{2},'{3}','{4}','{5}')";
        sql = string.Format(sql, sport_id, league_id, event_id, start_time, home, away);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_odd_three(string event_id, string period_num, string home, string draw, string away)
    {
        UInt64 timespan = UnixTime.unix_now;
        string sql = "delete from s_pin_odds where event_id={0} and period_type='{1}' and type_id='0'";
        sql = string.Format(sql, event_id, period_num);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into s_pin_odds (timespan,event_id,period_type,type_id,type_name,o1,o2,o3) values ({0},{1},'{2}','0','three','{3}','{4}','{5}')";
        sql = string.Format(sql,timespan.ToString(),event_id, period_num, home, away, draw);
        SQLServerHelper.exe_sql(sql); 
    }

    public static void insert_odd_spread(string event_id, string period_num, string home, string away, string odd_home, string odd_away)
    {
        UInt64 timespan = UnixTime.unix_now;
        string sql = "delete from s_pin_odds where event_id={0} and period_type='{1}' and type_id='1' and r1='{2}' and r2='{3}'";
        sql = string.Format(sql, event_id, period_num, home, away);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into s_pin_odds ( timespan,event_id,period_type,type_id,type_name,r1,r2,o1,o2) values ({0},{1},'{2}','1''spread','{3}','{4}','{5}','{6}')";
        sql = string.Format(sql,timespan.ToString(),event_id, period_num, home, away, odd_home, odd_away);
        SQLServerHelper.exe_sql(sql);
    }

    public static void insert_odd_total(string event_id, string period_num, string total, string odd_over, string odd_under)
    {
        UInt64 timespan = UnixTime.unix_now;
        string sql = "delete from s_pin_odds where event_id={0} and period_type='{1}' and type_id='2' and r1='{2}'";
        sql = string.Format(sql, event_id, period_num, total);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into s_pin_odds ( timespan,event_id,period_type,type_id,type_name,r1,o1,o2) values ({0},{1},'{2}','2','total','{3}','{4}','{5}')";
        sql = string.Format(sql,timespan.ToString(),event_id, period_num, total, odd_over, odd_under);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_odd_team_total_home(string event_id, string period_num, string total, string odd_over, string odd_under)
    {
        UInt64 timespan = UnixTime.unix_now;
        string sql = "delete from s_pin_odds where event_id={0} and period_type='{1}' and type_id='3' and r1 ='{2}'";
        sql = string.Format(sql, event_id, period_num, total);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into s_pin_odds ( timespan,event_id,period_type,type_id,type_name,r1,o1,o2) values ({0},{1},'{2}','3','team_total','{3}','{4}','{5}')";
        sql = string.Format(sql,timespan.ToString(),event_id, period_num, total, odd_over, odd_under);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_odd_team_total_away(string event_id, string period_num, string total, string odd_over, string odd_under)
    {
        UInt64 timespan = UnixTime.unix_now;
        string sql = "delete from s_pin_odds where event_id={0} and period_type='{1}' and type_id='3' and r2 ='{2}'";
        sql = string.Format(sql, event_id, period_num, total);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into s_pin_odds ( timespan,event_id,period_type,type_id,type_name,r2,o1,o2) values ({0},{1},'{2}','3','team_total','{3}','{4}','{5}')";
        sql = string.Format(sql,timespan.ToString(),event_id, period_num, total, odd_over, odd_under);
        SQLServerHelper.exe_sql(sql);
    }
}
