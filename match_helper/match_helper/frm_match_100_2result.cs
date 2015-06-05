using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;

namespace match_helper
{
    public partial class frm_match_100_2result : Form
    {
        public frm_match_100_2result()
        {
            InitializeComponent();
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            list.Add("PinnacleSports");
            BsonDocument doc = Analyse2Result.get_best(1, 50, list);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(Analyse2Result.get_info(doc));
            sb.AppendLine("-------------------------------------------------------------------------------------------------------------");
            this.txt_result.Text = sb.ToString();
        }

        public void compute()
        {
            string sql = "";
            //Fist Select Pin Event
            sql = "select * from a_event_s where a_website_id=1 and a_event_id is null";
            DataTable dt_pin = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_pin.Rows)
            {
                sql = "select * from a_event where a_event_s_id={0}";
                sql = string.Format(sql, row["id"].ToString());
                if (SQLServerHelper.get_table(sql).Rows.Count == 0)
                {
                    sql = "insert into a_event (start_time,team1,team2,a_event_s_id) values ({0},{1},{2},{3})";
                    sql = string.Format(row["start_time"].ToString(), row["team1"].ToString(), row["team2"].ToString(), row["id"].ToString());
                    SQLServerHelper.exe_sql(sql);

                    sql = "select * from a_event_s_id where id={0}";
                    sql = string.Format(sql, row["id"].ToString());
                    string max_id = SQLServerHelper.get_table(sql).Rows[0]["id"].ToString();

                    sql = "update a_event set a_event_id={0} where id={1}";
                    sql = string.Format(sql, max_id,row["id"].ToString());
                    SQLServerHelper.exe_sql(sql);
                } 
            }

            //Then Select Other Website Event
            sql = "select * from a_event_s where a_website_id<>1 and a_event_id is null";
            DataTable dt_other = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_other.Rows)
            {
                sql = "select * from a_event where start_time='{0}'";
                sql = string.Format(sql, row["start_time"].ToString());
                DataTable dt_time = SQLServerHelper.get_table(sql);
                bool is_find=false;
                foreach(DataRow row_time in dt_time.Rows)
                {
                    if (AnalyseHelper.is_alike_name(row["team1"].ToString(), row_time["team1"].ToString()) &&
                        AnalyseHelper.is_alike_name(row["team2"].ToString(), row_time["team2"].ToString()))
                    {
                        is_find = true;
                        sql = "update a_event_s  set a_event_id={0} where id={1} ";
                        sql = string.Format(sql, row_time["id"].ToString(), row["id"].ToString());
                        SQLServerHelper.exe_sql(sql);
                    }
                }

                if (is_find == false)
                {
                    sql = "insert into a_event (start_time,team1,team2,a_event_s_id) values ({0},{1},{2},{3})";
                    sql = string.Format(row["start_time"].ToString(), row["team1"].ToString(), row["team2"].ToString(), row["id"].ToString());
                    SQLServerHelper.exe_sql(sql);

                    sql = "select * from a_event_s_id where id={0}";
                    sql = string.Format(sql, row["id"].ToString());
                    string max_id = SQLServerHelper.get_table(sql).Rows[0]["id"].ToString();

                    sql = "update a_event set a_event_id={0} where id={1}";
                    sql = string.Format(sql, max_id, row["id"].ToString());
                    SQLServerHelper.exe_sql(sql); 
                }

            }
           
        }

        public void select_pin()
        {
            string sql = " select  a.sport_id,a.league_id,a.event_id,a.start_time,a.home,a.away," +
                         " b.period_type,b.bet_type,b.r1,b.r2,b.r3,b.r4,b.r5,b.r6,b.o1,b.o2,b.o3,b.o4,b.o5,b.o6" +
                         " from s_pin_events a,s_pin_odds b" +
                         " where a.event_id=b.event_id";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                sql = " select * from a_event_s where event_id='{0}'and website_id=1";
                sql = string.Format(sql, row["event_id"].ToString());
                if (SQLServerHelper.get_table(sql).Rows.Count == 0)
                {
                    sql = " insert into a_event_s (timespan,website_id,event_id,start_time,team1,team2) values({0},{1},'{2}','{3}','{4}','{5}')";
                    sql = string.Format(sql, UnixTime.unix_now.ToString(), "1", row["event_id"].ToString(), row["start_time"].ToString(), row["team1"].ToString(), row["team2"].ToString());
                    SQLServerHelper.exe_sql(sql); 
                }
                sql = " insert into a_odd_s (timespan,event_id,type_name,odd_id,r1,r2,r3,r4,r5,r6,o1,o2,o3,o4,o5,o6)" +
                      " values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')";
                sql = string.Format(sql, UnixTime.unix_now.ToString(), row["event_id"].ToString(), row["bet_type"].ToString(), "",
                                  row["r1"].ToString(), row["r2"].ToString(), row["r3"].ToString(), row["r4"].ToString(), row["r5"].ToString(), row["r6"].ToString(),
                                  row["o1"].ToString(), row["o2"].ToString(), row["o3"].ToString(), row["o4"].ToString(), row["o5"].ToString(), row["o6"].ToString());
                SQLServerHelper.exe_sql(sql); 
            } 
        }

    }
}
