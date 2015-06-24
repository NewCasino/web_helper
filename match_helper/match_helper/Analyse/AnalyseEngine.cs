using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;

class AnalyseEngine
{
    public static void select_event()
    {
        string sql = "";
        //select pin website
        sql = "select * from a_all where a_website_id=1 and a_event_id is null";
        DataTable dt_pin = SQLServerHelper.get_table(sql);
        foreach (DataRow row in dt_pin.Rows)
        {
            #region old method
            /*
            sql = "select * from a_event a,a_all b where a.id=b.a_event_id  and b.event_id={0} and b.a_website_id=1";
            sql = string.Format(sql, row["event_id"].ToString());
            DataTable dt_temp = SQLServerHelper.get_table(sql);
            if (dt_temp.Rows.Count == 0)
            {
                sql = "insert into a_event (timespan,start_time,team1,team2,a_all_id) values ( {0} ,'{1}','{2}','{3}','{4}')";
                sql = string.Format(sql, UnixTime.unix_now.ToString(), row["start_time"].ToString(), row["team1"].ToString(), row["team2"].ToString(), row["id"].ToString());
                SQLServerHelper.exe_sql(sql);

                sql = "select * from a_event where a_all_id={0}";
                sql = string.Format(sql, row["id"].ToString());
                string max_id = SQLServerHelper.get_table(sql).Rows[0]["id"].ToString();

                sql = "update a_all set a_event_id={0} where id={1}";
                sql = string.Format(sql, max_id, row["id"].ToString());
                SQLServerHelper.exe_sql(sql);
            }
            else
            {
                sql = " update a_all set a_event_id={0} where id={1}";
                sql = string.Format(sql, dt_temp.Rows[0]["a_event_id"].ToString(), row["id"].ToString());
                SQLServerHelper.exe_sql(sql);
            }
            */
            #endregion
            sql = "select * from a_event";
            DataTable dt_time = SQLServerHelper.get_table(sql);
            bool is_find = false;
            foreach (DataRow row_time in dt_time.Rows)
            {
                if (AnalyseHelper.is_same_event(row["start_time"].ToString(), row["team1"].ToString(), row["team2"].ToString(), row["start_time"].ToString(), row_time["team1"].ToString(), row_time["team2"].ToString()) == true)
                {
                    is_find = true;
                    sql = "update a_all  set a_event_id={0} where id={1} ";
                    sql = string.Format(sql, row_time["id"].ToString(), row["id"].ToString());
                    SQLServerHelper.exe_sql(sql);
                }
            }

            if (is_find == false)
            {
                sql = "insert into a_event (timespan,start_time,team1,team2,a_all_id) values ({0},'{1}','{2}','{3}','{4}')";
                sql = string.Format(sql, UnixTime.unix_now.ToString(), row["start_time"].ToString(), row["team1"].ToString(), row["team2"].ToString(), row["id"].ToString());
                SQLServerHelper.exe_sql(sql);

                sql = "select * from a_event where a_all_id={0}";
                sql = string.Format(sql, row["id"].ToString());
                string max_id = SQLServerHelper.get_table(sql).Rows[0]["id"].ToString();

                sql = "update a_all set a_event_id={0} where id={1}";
                sql = string.Format(sql, max_id, row["id"].ToString());
                SQLServerHelper.exe_sql(sql);
            }

        }

        //select other websites
        sql = "select * from a_all where a_website_id<>1 and a_event_id is null";
        DataTable dt_other = SQLServerHelper.get_table(sql);
        foreach (DataRow row in dt_other.Rows)
        {
            sql = "select * from a_event";
            DataTable dt_time = SQLServerHelper.get_table(sql);
            bool is_find = false;
            foreach (DataRow row_time in dt_time.Rows)
            {
                if (AnalyseHelper.is_same_event(row["start_time"].ToString(), row["team1"].ToString(), row["team2"].ToString(), row["start_time"].ToString(), row_time["team1"].ToString(), row_time["team2"].ToString()) == true)
                {
                    is_find = true;
                    sql = "update a_all  set a_event_id={0} where id={1} ";
                    sql = string.Format(sql, row_time["id"].ToString(), row["id"].ToString());
                    SQLServerHelper.exe_sql(sql);
                }
            }

            if (is_find == false)
            {
                sql = "insert into a_event (timespan,start_time,team1,team2,a_all_id) values ({0},'{1}','{2}','{3}','{4}')";
                sql = string.Format(sql, UnixTime.unix_now.ToString(),row["start_time"].ToString(), row["team1"].ToString(), row["team2"].ToString(), row["id"].ToString());
                SQLServerHelper.exe_sql(sql);

                sql = "select * from a_event where a_all_id={0}";
                sql = string.Format(sql, row["id"].ToString());
                string max_id = SQLServerHelper.get_table(sql).Rows[0]["id"].ToString();

                sql = "update a_all set a_event_id={0} where id={1}";
                sql = string.Format(sql, max_id, row["id"].ToString());
                SQLServerHelper.exe_sql(sql);
            }
        }
    }
    public static void select_odd()
    {
        string sql = " select * from a_all where  a_event_id is not null and a_odd_id is null";
        DataTable dt_all = SQLServerHelper.get_table(sql);
        foreach (DataRow row in dt_all.Rows)
        {

            //get type id
            string type_id = "";
            type_id = string.IsNullOrEmpty(row["a_type_id"].ToString()) ? AnalyseHelper.get_bet_type_id(row["type_name"].ToString()) : row["a_type_id"].ToString();


            //insert a_odd
            sql = " select * from a_odd where event_id= {0} and type_id={1}  " +
                  " and m1='{2}' and m2='{3}' and m3='{4}'  and m4='{5}' and m5='{6}' and m6='{7}'" +
                  " and r1='{8}' and r2='{9}' and r3='{10}' and r4='{11}' and r5='{12}' and r6='{13}'";
            sql = string.Format(sql, row["a_event_id"].ToString(), type_id,
                                row["m1"].ToString(), row["m2"].ToString(), row["m3"].ToString(), row["m4"].ToString(), row["m5"].ToString(), row["m6"].ToString(),
                                row["r1"].ToString(), row["r2"].ToString(), row["r3"].ToString(), row["r4"].ToString(), row["r5"].ToString(), row["r6"].ToString());
            DataTable dt_temp = SQLServerHelper.get_table(sql);

            if (dt_temp.Rows.Count > 0)
            {
                sql = "delete   from a_odd where id={0}";
                sql = string.Format(sql, dt_temp.Rows[0]["id"].ToString());
                SQLServerHelper.exe_sql(sql);
            }
            sql = " insert into a_odd (timespan,website_id,event_id,type_id,a_all_id,m1,m2,m3,m4,m5,m6,r1,r2,r3,r4,r5,r6,o1,o2,o3,o4,o5,o6)" +
                " values({0},{1},{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";
            sql = string.Format(sql, UnixTime.unix_now.ToString(), row["a_website_id"].ToString(), row["a_event_id"].ToString(), row["a_type_id"].ToString(), row["id"].ToString(),
                 row["m1"].ToString(), row["m2"].ToString(), row["m3"].ToString(), row["m4"].ToString(), row["m5"].ToString(), row["m6"].ToString(),
                 row["r1"].ToString(), row["r2"].ToString(), row["r3"].ToString(), row["r4"].ToString(), row["r5"].ToString(), row["r6"].ToString(),
                 row["o1"].ToString(), row["o2"].ToString(), row["o3"].ToString(), row["o4"].ToString(), row["o5"].ToString(), row["o6"].ToString());
            SQLServerHelper.exe_sql(sql);


            //insert a_odd_log 
            sql = " insert into a_odd_log (timespan,website_id,event_id,type_id,a_all_id,m1,m2,m3,m4,m5,m6,r1,r2,r3,r4,r5,r6,o1,o2,o3,o4,o5,o6)" +
                  " values({0},{1},{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";
            sql = string.Format(sql, UnixTime.unix_now.ToString(), row["a_website_id"].ToString(), row["a_event_id"].ToString(), row["a_type_id"].ToString(), row["id"].ToString(),
                 row["m1"].ToString(), row["m2"].ToString(), row["m3"].ToString(), row["m4"].ToString(), row["m5"].ToString(), row["m6"].ToString(),
                 row["r1"].ToString(), row["r2"].ToString(), row["r3"].ToString(), row["r4"].ToString(), row["r5"].ToString(), row["r6"].ToString(),
                 row["o1"].ToString(), row["o2"].ToString(), row["o3"].ToString(), row["o4"].ToString(), row["o5"].ToString(), row["o6"].ToString());
            SQLServerHelper.exe_sql(sql);


            //update a_all's a_type_id,a_odd_id 
            sql = "select max(id) from a_odd_log ";
            DataTable dt_max = SQLServerHelper.get_table(sql);
            string odd_id = dt_max.Rows[0][0].ToString();

            sql = "update a_all set a_type_id={0},a_odd_id={1} where id={2}";
            sql = string.Format(sql, type_id, odd_id, row["id"].ToString());
            SQLServerHelper.exe_sql(sql);
        }

    }
    public static void select_pin()
    {
        string sql = " select b.id b_id, a.sport_id,a.league_id,a.event_id,a.start_time,a.home,a.away," +
                     " b.period_type,b.type_id,b.type_name,b.m1,b.m2,b.m3,b.m4,b.m5,b.m6,b.r1,b.r2,b.r3,b.r4,b.r5,b.r6,b.o1,b.o2,b.o3,b.o4,b.o5,b.o6" +
                     " from s_pin_events a,s_pin_odds b" +
                     " where a.event_id=b.event_id and note1='0'";
        DataTable dt = SQLServerHelper.get_table(sql);
        foreach (DataRow row in dt.Rows)
        {
            DateTime time = Tool.get_time(row["start_time"].ToString());
            if (time.Minute % 10 == 4 || time.Minute % 10 == 9) time = time.AddMinutes(1); 

            sql = " insert into a_all (timespan,event_id,start_time,team1,team2,type_name,odd_id,m1,m2,m3,m4,m5,m6,r1,r2,r3,r4,r5,r6,o1,o2,o3,o4,o5,o6,a_website_id,a_type_id)" +
                  " values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}',{19},{20},'{21}','{22}','{23}','{24}',{25},{26})";
            sql = string.Format(sql, UnixTime.unix_now.ToString(), row["event_id"].ToString(), time.ToString("yyyy-MM-dd HH:mm:ss"), row["home"].ToString(), row["away"].ToString(), row["type_name"].ToString(), "",
                          row["m1"].ToString(), row["m2"].ToString(), row["m3"].ToString(), row["m4"].ToString(), row["m5"].ToString(), row["m6"].ToString(),
                          row["r1"].ToString(), row["r2"].ToString(), row["r3"].ToString(), row["r4"].ToString(), row["r5"].ToString(), row["r6"].ToString(),
                          row["o1"].ToString(), row["o2"].ToString(), row["o3"].ToString(), row["o4"].ToString(), row["o5"].ToString(), row["o6"].ToString(), "1", row["type_id"].ToString());
            SQLServerHelper.exe_sql(sql);

            sql = " update  s_pin_odds set note1='1' where id={0}";
            sql = string.Format(sql, row["b_id"].ToString());
            SQLServerHelper.exe_sql(sql);
        }
      
    }
    public static void select_mb()
    {
        string sql = " select b.id b_id,a.event_id,a.start_time,a.home,a.away,b.type_id,b.type_name,b.m1,b.m2,b.m3,b.m4,b.m5,b.m6," +
                     " b.r1,b.r2,b.r3,b.r4,b.r5,b.r6,b.o1,b.o2,b.o3,b.o4,b.o5,b.o6" +
                     " from s_mb_events a,s_mb_odds b" +
                     " where a.event_id=b.event_id and note1='0'";
        DataTable dt = SQLServerHelper.get_table(sql);
        foreach (DataRow row in dt.Rows)
        {
             
            sql = " insert into a_all (timespan,event_id,start_time,team1,team2,type_name,odd_id,m1,m2,m3,m4,m5,m6,r1,r2,r3,r4,r5,r6,o1,o2,o3,o4,o5,o6,a_website_id,a_type_id)" +
                  " values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}',{19},{20},'{21}','{22}','{23}','{24}',{25},{26})";
            sql = string.Format(sql, UnixTime.unix_now.ToString(), row["event_id"].ToString(), row["start_time"].ToString(), row["home"].ToString(), row["away"].ToString(), row["type_name"].ToString(), "",
                          row["m1"].ToString(), row["m2"].ToString(), row["m3"].ToString(), row["m4"].ToString(), row["m5"].ToString(), row["m6"].ToString(),
                          row["r1"].ToString(), row["r2"].ToString(), row["r3"].ToString(), row["r4"].ToString(), row["r5"].ToString(), row["r6"].ToString(),
                          row["o1"].ToString(), row["o2"].ToString(), row["o3"].ToString(), row["o4"].ToString(), row["o5"].ToString(), row["o6"].ToString(), "2", row["type_id"].ToString());
            SQLServerHelper.exe_sql(sql);

            sql = " update s_mb_odds set note1='1' where id={0} ";
            sql = string.Format(sql, row["b_id"].ToString());
            SQLServerHelper.exe_sql(sql);
        }
    }  
}
