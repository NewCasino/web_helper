using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

class PinSQL
{
 
    public static void insert_sport(string sport_id,string sport_name)
    {
        string sql = "delete from pin_sports where sport_id={0}";
        sql = string.Format(sql, sport_id);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into pin_sports (sport_id,sport_name) values ( {0} ,'{1}')";
        sql = string.Format(sql, sport_id, sport_name);
        SQLServerHelper.exe_sql(sql);
    } 
    public static void insert_league(string sport_id, string league_id, string league_name)
    {
        string sql = "delete from pin_sports where league_id={0}";
        sql = string.Format(sql, league_id);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into pin_leagues (sport_id,league_id,league_name) values ( {0} , {1} ,'{2}')";
        sql = string.Format(sql, sport_id, league_id, league_name);
        SQLServerHelper.exe_sql(sql);
    }
    public static void insert_event(string sport_id, string league_id, string event_id, string start_time, string host, string away)
    {
        string sql = "delete from pin_events where event_id={0}";
        sql = string.Format(sql, event_id);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into pin_events(sport_id,league_id,event_id,start_time,host,away) values ({0},{1},{2},'{3}','{4}','{5}')";
        sql = string.Format(sql, sport_id, league_id, event_id, start_time, host, away);
        SQLServerHelper.exe_sql(sql);
    }
}
