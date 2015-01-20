using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

class MbookSQL
{
    public static void insert_events(string event_id, string sport, string country, string competition, string start_time, string home, string away)
    {
        string sql = "";
        sql = "delete from mbook_events where event_id='{0}'";
        sql = string.Format(sql, event_id);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into mbook_events (event_id,sport,country,competition,start_time,home,away)" +
            " values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
        sql = string.Format(sql, event_id, sport, country, competition, start_time, home, away);
        SQLServerHelper.exe_sql(sql);

    }
    public static void insert_market(string event_id, string market_name, string runner_name,
                              string depth_no, string type,string odd,string amount)
    {
        string sql = "";
        sql = "delete from mbook_market where event_id='{0}' and market='{1}' and runner='{2}' and depth_no>='{3}' and type='{4}'";
        sql = string.Format(sql, event_id, market_name, runner_name, depth_no,type);
        SQLServerHelper.exe_sql(sql);

        sql = " insert into mbook_market (timespan,event_id,market,runner,depth_no,type,odd,amount) values" +
            " ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
        sql = string.Format(sql,UnixTime.unix_now.ToString(),event_id, market_name, runner_name, depth_no,type,odd,amount);
        SQLServerHelper.exe_sql(sql);
    }
}
