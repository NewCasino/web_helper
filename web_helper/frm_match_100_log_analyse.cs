using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace web_helper
{
    public partial class frm_match_100_log_analyse : Form
    {
        StringBuilder sb = new StringBuilder();
        bool is_picking = false;
        public frm_match_100_log_analyse()
        {
            InitializeComponent();
        } 

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        } 
      
        
        private void btn_pick_data_Click(object sender, EventArgs e)
        {
            pick_data(); 
            MessageBox.Show("Pick Complete!!");
        }
        private void time_Tick(object sender, EventArgs e)
        {
            if (is_picking == false) { pick_data(); }
            this.lb_time.Text = DateTime.Now.ToString("hh:mm:ss");
        }
        private void btn_start_Click(object sender, EventArgs e)
        {
            time.Start();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            time.Stop();
        }
      


        public void pick_data()
        { 
            is_picking = true;
            //f_state: 0-未处理   1-已处理，值更新了时间   2-已处理，一个匹配   3-已处理，2个匹配
            string id_min = "9999999999999999";
            string sql = "select min(id) from europe_100_log where f_state='0' ";
            DataTable dt_min = SQLServerHelper.get_table(sql);
            if (!string.IsNullOrEmpty(dt_min.Rows[0][0].ToString())) id_min = dt_min.Rows[0][0].ToString();  

            sql = "select * from europe_100_log where f_state='0' ";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                string id = row["id"].ToString();
                string website = row["website"].ToString();
                string start_time = row["start_time"].ToString();
                string host = row["host"].ToString();
                string client = row["client"].ToString();
                string win = row["odd_win"].ToString();
                string draw = row["odd_draw"].ToString();
                string lose = row["odd_lose"].ToString();
                string time_zone = row["time_zone"].ToString();
                string time_add = row["time_add"].ToString();
                string timespan = row["timespan"].ToString();

                string convert_host = "";
                string convert_client = "";
                string convert_time = "";
                string f_state = "1";

                try
                {
                    convert_host = Match100Helper.convert_team_name(host);
                    convert_client = Match100Helper.convert_team_name(client);


                    DateTime time = Match100Helper.convert_start_time(start_time, time_zone, timespan);
                    if (website == "pinnaclesports" && time.Minute % 5 != 0) time = time.AddMinutes(Convert.ToInt16(time_add));
                    convert_time = time.ToString("yyyy-MM-dd HH:mm:ss");


                    if (!string.IsNullOrEmpty(convert_host) || !string.IsNullOrEmpty(convert_client))
                    {
                        f_state = "2";
                    }
                    if (!string.IsNullOrEmpty(convert_host) && !string.IsNullOrEmpty(convert_client))
                    {
                        f_state = "3";
                    }
                    sql = " update europe_100_log set f_host='{0}',f_client='{1}',f_start_time='{2}' ,f_state='{3}' where id='{4}'";
                    sql = string.Format(sql, convert_host, convert_client, convert_time, f_state, id);
                    SQLServerHelper.exe_sql(sql);

                    sb.AppendLine(website.PR(10) + convert_time.PR(30) + convert_host.PR(50) + convert_client.PR(50));
                    this.txt_result.Text = sb.PRINT();
                    Application.DoEvents();
                }
                catch (Exception error)
                {
                    sql = " update europe_100_log set  f_state='w' where id='{0}'";
                    sql = string.Format(sql, id);
                    SQLServerHelper.exe_sql(sql);
                    sb.AppendLine(id + " Wrong!");
                    this.txt_result.Text = sb.PRINT();
                    Application.DoEvents();

                }

            }



            //循环根据一个补全另一个
            sql = "select * from europe_100_log where f_state='1'  and f_start_time>'{0}'";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                string id = row["id"].ToString();
                string start_time = row["start_time"].ToString();
                string host = row["host"].ToString();
                string client = row["client"].ToString();
                string f_start_time = row["f_start_time"].ToString();
                string f_host = row["f_host"].ToString();
                string f_client = row["f_client"].ToString();

                if (string.IsNullOrEmpty(f_host) && !string.IsNullOrEmpty(f_start_time) && !string.IsNullOrEmpty(f_client))
                {
                    sql = " select * from europe_100_log where f_start_time='{0}' and f_client='{1}' and id<>'{2}'";
                    sql = string.Format(sql, f_start_time, f_client, id);
                    DataTable dt_temp = SQLServerHelper.get_table(sql);
                    foreach (DataRow row_temp in dt_temp.Rows)
                    {
                        if (!string.IsNullOrEmpty(row_temp["f_host"].ToString()))
                        {
                            sql = " update europe_100_log set f_host='{0}',f_state='3' where id='{1}'";
                            sql = string.Format(sql, row_temp["f_host"].ToString(), id);
                            SQLServerHelper.exe_sql(sql);
                            Match100Helper.insert_name(row_temp["f_host"].ToString(), host);

                            sb.AppendLine(start_time.PR(30) + f_client.PR(50) + row_temp["f_host"].ToString());
                            this.txt_result.Text = sb.PRINT();
                            Application.DoEvents();
                        }
                    }
                }

                if (string.IsNullOrEmpty(f_client) && !string.IsNullOrEmpty(f_start_time) && !string.IsNullOrEmpty(f_host))
                {
                    sql = " select * from europe_100_log where f_start_time='{0}' and f_host='{1}' and id<>'{2}'";
                    sql = string.Format(sql, f_start_time, f_host, id);
                    DataTable dt_temp = SQLServerHelper.get_table(sql);
                    foreach (DataRow row_temp in dt_temp.Rows)
                    {
                        if (!string.IsNullOrEmpty(row_temp["f_client"].ToString()))
                        {
                            sql = " update europe_100_log set f_client='{0}',f_state='3' where id='{1}'";
                            sql = string.Format(sql, row_temp["f_client"].ToString(), id);
                            SQLServerHelper.exe_sql(sql);
                            Match100Helper.insert_name(row_temp["f_client"].ToString(), client);

                            sb.AppendLine(start_time.PR(30) + f_host.PR(50) + row_temp["f_client"].ToString());
                            this.txt_result.Text = sb.PRINT();
                            Application.DoEvents();
                        }
                    }
                }
            }  

            //Insert Office
            sql = "select * from europe_100_log where f_state='3'";
            DataTable dt_log = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_log.Rows)
            {
                string id = row["id"].ToString();
                string website = row["website"].ToString();
                string time_span = row["timespan"].ToString();
                string start_time = row["f_start_time"].ToString();
                string league = row["league"].ToString();
                string host = row["f_host"].ToString();
                string client = row["f_client"].ToString();
                string odd_win = row["odd_win"].ToString().Trim();
                string odd_draw = row["odd_draw"].ToString().Trim();
                string odd_lose = row["odd_lose"].ToString().Trim();

                //get max id
                sql = " select isnull(max(id),-1)  from europe_100 where website='{0}' and  start_time='{1}' and host='{2}' and client='{3}'";
                sql = string.Format(sql, website, start_time, host, client);
                string max_id = SQLServerHelper.get_table(sql).Rows[0][0].ToString();

                sql = "select * from europe_100 where website='{0}' and  start_time='{1}' and host='{2}' and client='{3}' and odd_win='{4}' and odd_draw='{5}' and odd_lose='{6}' and id={7}";
                sql = string.Format(sql, website, start_time, host, client, odd_win, odd_draw, odd_lose,max_id);
                DataTable dt_temp = SQLServerHelper.get_table(sql);

                if (dt_temp.Rows.Count == 0)
                {
                    sql = " insert into europe_100  (timespan,website,league,start_time,host,client,odd_win,odd_draw,odd_lose) values" +
                          " ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
                    sql = string.Format(sql, time_span, website, league, start_time, host, client, odd_win, odd_draw, odd_lose);
                    SQLServerHelper.exe_sql(sql);
                }
                else
                {
                    sql = "update europe_100 set timespan='{0}' where id={1}";
                    sql = string.Format(sql, time_span,max_id);
                    SQLServerHelper.exe_sql(sql);
                }
                sql = "update europe_100_log set f_state='4' where id={0}";
                sql = string.Format(sql, id);
                SQLServerHelper.exe_sql(sql);
            } 
            is_picking = false;
        } 
  
   
    }
    
}
