using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 
    class MbSQL
    {
        public static void insert_events(string event_id,string league,string start_time,string home,string away)
        {

            league = SQLServerHelper.format_sql_str(league);
            home = SQLServerHelper.format_sql_str(home);
            away = SQLServerHelper.format_sql_str(away);
            string sql = "";
            sql = "delete from s_mb_events where event_id='{0}'";
            sql = string.Format(sql, event_id);
          
            SQLServerHelper.exe_sql(sql);

            sql = " insert into s_mb_events(event_id,league,start_time,home,away) values('{0}','{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, event_id, league, start_time, home, away);
 
            SQLServerHelper.exe_sql(sql);
        }

        public static void insert_odds(string event_id,string period_type,string bet_type,string home,string draw,string away)
        {
            string sql = "";
            sql = "delete from s_mb_odds where event_id='{0}' and period_type='{1}' and bet_type='{2}'";
            sql = string.Format(sql,event_id, period_type, bet_type);
           
            SQLServerHelper.exe_sql(sql);

            sql = "insert into s_mb_odds (event_id,period_type,bet_type,o1,o2,o3) values('{0}','{1}','{2}','{3}','{4}','{5}')";
            sql=string.Format(sql,event_id, period_type, bet_type, home, draw, away);
            
            SQLServerHelper.exe_sql(sql);
        }
    }
 