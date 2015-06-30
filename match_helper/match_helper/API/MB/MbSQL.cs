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

        public static void insert_odds(string event_id,string type_id,string type_name,
                                       string m1,string m2,string m3,string m4,string m5,string m6,
                                       string r1,string r2,string r3,string r4,string r5,string r6,
                                       string o1,string o2,string o3,string o4,string o5,string o6)
        {
            string sql = "";
            sql = " insert into s_mb_odds (timespan,event_id,type_id,type_name,m1,m2,m3,m4,m5,m6,r1,r2,r3,r4,r5,r6,o1,o2,o3,o4,o5,o6,note1) " +
                  " values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','0')";

            sql = string.Format(sql,UnixTime.unix_now.ToString(), event_id, type_id, type_name,
                              m1, m2, m3, m4, m5, m6, r1, r2, r3, r4, r5, r6, o1, o2, o3, o4, o5, o6); 
            SQLServerHelper.exe_sql(sql);
        }
    }
 
