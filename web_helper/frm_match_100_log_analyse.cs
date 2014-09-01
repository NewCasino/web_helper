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
        public frm_match_100_log_analyse()
        {
            InitializeComponent();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }


        private void btn_analyse_Click(object sender, EventArgs e)
        { 
            update_standard_data();
            analyse();
            MessageBox.Show("Analyse OK!");
        }

      
        public void update_standard_data()
        {
            //f_state: 0-未处理   1-已处理，值更新了时间   2-已处理，一个匹配   3-已处理，2个匹配
            string sql = "select * from europe_100_log where f_state='0' ";
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

                string convert_host = "";
                string convert_client = "";
                string convert_time = "";
                string f_state = "1";

                convert_host = Match100Helper.convert_team_name(host);
                convert_client = Match100Helper.convert_team_name(client);
                DateTime time = Match100Helper.convert_start_time(start_time);

                if (website == "pinnaclesports" && time.Minute % 5 != 0)   time = time.AddMinutes(Convert.ToInt16(time_add));
               
                time = Tool.get_time_by_kind(time, Convert.ToInt16(time_zone));
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
                sql = string.Format(sql,  convert_host, convert_client,convert_time, f_state, id);
                SQLServerHelper.exe_sql(sql);

                sb.AppendLine(convert_time.PR(30) + convert_host.PR(50) + convert_client.PR(50));
                this.txt_result.Text = sb.ToString();
                Application.DoEvents();

            }

            sql = "select * from europe_100_log where f_state='1' ";
            dt = SQLServerHelper.get_table(sql);
            //循环根据一个补全另一个
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
                            this.txt_result.Text = sb.ToString();
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
                            this.txt_result.Text = sb.ToString();
                            Application.DoEvents();
                        }
                    }
                }
            } 
        }
        public void analyse()
        {
            DataTable dt_result = new DataTable();
            dt_result.Columns.Add("type");
            dt_result.Columns.Add("id");
            dt_result.Columns.Add("start_time");
            dt_result.Columns.Add("f_start_time");
            dt_result.Columns.Add("host");
            dt_result.Columns.Add("client");
            dt_result.Columns.Add("f_host");
            dt_result.Columns.Add("f_client");
            dt_result.Columns.Add("win");
            dt_result.Columns.Add("draw");
            dt_result.Columns.Add("lose");
            dt_result.Columns.Add("f_target_id");



            string sql = " select * from europe_100_log where f_state ='1' or f_state='2' ";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                string id = row["id"].ToString();
                string start_time = row["start_time"].ToString();
                string f_start_time = row["f_start_time"].ToString();
                string host = row["host"].ToString();
                string client = row["client"].ToString();
                string f_host = row["f_host"].ToString();
                string f_client = row["f_client"].ToString();
                string win = row["odd_win"].ToString();
                string draw = row["odd_draw"].ToString();
                string lose = row["odd_lose"].ToString();
                string time_zone = row["time_zone"].ToString();
                string time_add = row["time_add"].ToString();
                string f_state = row["f_state"].ToString();

                DataRow row_new = dt_result.NewRow();
                row_new["type"] = "M";
                row_new["id"] = id;
                row_new["start_time"] = start_time;
                row_new["host"] = host;
                row_new["client"] = client;
                row_new["f_start_time"] = f_start_time;
                row_new["f_host"] = f_host;
                row_new["f_client"] = f_client;
                row_new["win"] = win;
                row_new["draw"] = draw;
                row_new["lose"] = lose;
                row_new["f_target_id"] = "";
                dt_result.Rows.Add(row_new);

             
                sb.AppendLine(id.PR(5) + start_time.PR(20) + host.PR(30) + client.PR(30) + f_start_time.PR(20) + f_host.PR(30) + f_client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
          
                sql = " select * from europe_100_log where (f_state='2' or f_state='3') and f_start_time='{0}' and id<>'{1}' and f_start_time='{2}'";
                sql = string.Format(sql, f_start_time, id,f_start_time);
                DataTable dt_temp = SQLServerHelper.get_table(sql);
                foreach (DataRow row_temp in dt_temp.Rows)
                {
                    DataRow row_new_temp = dt_result.NewRow();
                    row_new_temp["type"] = "";
                    row_new_temp["id"] = row_temp["id"].ToString();
                    row_new_temp["start_time"] = row_temp["start_time"].ToString();
                    row_new_temp["host"] = row_temp["host"].ToString();
                    row_new_temp["client"] = row_temp["client"].ToString();
                    row_new_temp["f_start_time"] = row_temp["f_start_time"].ToString();
                    row_new_temp["f_host"] = row_temp["f_host"].ToString();
                    row_new_temp["f_client"] = row_temp["f_client"].ToString();
                    row_new_temp["win"] = row_temp["odd_win"].ToString();
                    row_new_temp["draw"] = row_temp["odd_draw"].ToString();
                    row_new_temp["lose"] = row_temp["odd_lose"].ToString();
                    row_new_temp["f_target_id"] = "";
                    dt_result.Rows.Add(row_new_temp);

                    sb.AppendLine("".PR(5) + row_temp["start_time"].PR(20) + row_temp["host"].PR(30) + row_temp["client"].PR(30) + row_temp["f_start_time"].PR(20) + row_temp["f_host"].PR(30) + row_temp["f_client"].PR(30) + row_temp["odd_win"].PR(10) + row_temp["odd_draw"].PR(10) + row_temp["odd_lose"].PR(10));
                  
                }
                sb.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                
            }
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
            this.dgv_result.DataSource = dt_result;
        }
        public void analyse_by_odd()
        {
            DataTable dt_result = new DataTable();
            dt_result.Columns.Add("id");
            dt_result.Columns.Add("leage"); 
            dt_result.Columns.Add("start_time"); 
            dt_result.Columns.Add("host");
            dt_result.Columns.Add("client");
            dt_result.Columns.Add("f_league");
            dt_result.Columns.Add("f_start_time");
            dt_result.Columns.Add("f_host");
            dt_result.Columns.Add("f_client");
            dt_result.Columns.Add("odd_type");
            dt_result.Columns.Add("win");
            dt_result.Columns.Add("draw");
            dt_result.Columns.Add("lose");
            dt_result.Columns.Add("f_target_id");



            string sql = " select * from europe_100_log where f_state<>'0' order by f_league,start_time ";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {

                string id = row["id"].ToString();
                string leage = row["league"].ToString();
                string start_time = row["start_time"].ToString();
                string host = row["host"].ToString();
                string client = row["clinet"].ToString();
                string f_league = row["f_league"].ToString();
                string f_start_time = row["f_start_time"].ToString();
                string f_host = row["f_host"].ToString();
                string f_client = row["f_client"].ToString();
                string  win = row["odd_win"].ToString();
                string draw = row["odd_draw"].ToString();
                string lose = row["odd_lose"].ToString();
                string odd_type = get_odd_order(win, draw, lose);


                DataRow row_new = dt_result.NewRow();
                row_new["id"] = id;
                row_new["leage"] = leage;
                row_new["start_time"] = start_time;
                row_new["host"] = host;
                row_new["client"] = client;
                row_new["f_league"] = f_league;
                row_new["f_start_time"] = f_start_time;
                row_new["f_host"] = f_host;
                row_new["f_client"] = f_client;
                row_new["odd_type"] = odd_type;
                row_new["win"] = win;
                row_new["draw"] = draw;
                row_new["lose"] = lose; 
                dgv_result.Rows.Add(row_new);
                //sb.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

            }
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
            this.dgv_result.DataSource = dt_result;
        }
        public void update_by_odd()
        { 
            string sql = "";
            for (int i = 0; i < dgv_result.Rows.Count - 1; i++)
            {
                if (dgv_result.Rows[i].Cells["type"].Value == null) continue;
                //if (dgv_result.Rows[i].Cells["type"].Value.ToString() != "M") continue;
                if (dgv_result.Rows[i].Cells["f_target_id"].Value == null) continue;

                string id = dgv_result.Rows[i].Cells["id"].Value.ToString();
                string f_target_id = dgv_result.Rows[i].Cells["f_target_id"].Value.ToString();

                for (int j = 0; j < dgv_result.Rows.Count - 1; j++)
                {
                    if (dgv_result.Rows[i].Cells["id"].ToString() == f_target_id)
                    {
                        string host = dgv_result.Rows[j].Cells["host"].ToString();
                        string client = dgv_result.Rows[j].Cells["client"].ToString();
                        string f_host = dgv_result.Rows[j].Cells["f_host"].ToString();
                        string f_client = dgv_result.Rows[j].Cells["f_client"].ToString();

                        sql = " update europe_100_log set f_host='{0}',f_client='{1}',f_state='3'  where id='{2}' ";
                        sql = string.Format(sql, f_host, f_client, id);
                        SQLServerHelper.exe_sql(sql);
                        sql = " update europe_100_log set f_host='{0}',f_client='{1},f_state='3'  where id='{2}' ";
                        sql = string.Format(sql, f_host, f_client, f_target_id);
                        SQLServerHelper.exe_sql(sql);

                        Match100Helper.insert_name(f_host, host);
                        Match100Helper.insert_name(f_client, client);

                        Match100Helper.insert_name(f_host, dgv_result.Rows[i].Cells["host"].ToString());
                        Match100Helper.insert_name(f_client, dgv_result.Rows[i].Cells["client"].ToString());


                    }
                }
            }
        }
        public string get_odd_order(string str_win, string str_draw, string str_lose)
        {
            double win = Convert.ToDouble(str_win);
            double draw = Convert.ToDouble(str_draw);
            double lose = Convert.ToDouble(str_lose);

            if (win >= draw && draw >= lose) return "WDL";
            if (win >= lose && lose >= draw) return "WLD";
            if (draw >= win && win >= lose) return "DWL";
            if (draw >= lose && lose >= win) return "DLW";
            if (lose >= win && win >= draw) return "LWD";
            if (lose >= draw && draw > win) return "LDW";
            return "WRONG";
        }
        private void btn_update_Click(object sender, EventArgs e)
        {
            string sql = "";
            for (int i = 0; i < dgv_result.Rows.Count - 1; i++)
            {
                if (dgv_result.Rows[i].Cells["type"].Value == null) continue;
                if (dgv_result.Rows[i].Cells["type"].Value.ToString() != "M") continue;
                if (dgv_result.Rows[i].Cells["f_target_id"].Value == null) continue;

                string id = dgv_result.Rows[i].Cells["id"].Value.ToString();
                string f_target_id = dgv_result.Rows[i].Cells["f_target_id"].Value.ToString();

                for (int j = 0; j < dgv_result.Rows.Count - 1; j++)
                {
                    if (dgv_result.Rows[i].Cells["id"].ToString() == f_target_id)
                    {
                        string host = dgv_result.Rows[j].Cells["host"].ToString();
                        string client = dgv_result.Rows[j].Cells["client"].ToString();
                        string f_host = dgv_result.Rows[j].Cells["f_host"].ToString();
                        string f_client = dgv_result.Rows[j].Cells["f_client"].ToString();

                        sql = " update europe_100_log set f_host='{0}',f_client='{1}',f_state='3'  where id='{2}' ";
                        sql = string.Format(sql, f_host, f_client, id);
                        SQLServerHelper.exe_sql(sql);
                        sql = " update europe_100_log set f_host='{0}',f_client='{1},f_state='3'  where id='{2}' ";
                        sql = string.Format(sql, f_host, f_client, f_target_id);
                        SQLServerHelper.exe_sql(sql);

                        Match100Helper.insert_name(f_host, dgv_result.Rows[i].Cells["host"].ToString());
                        Match100Helper.insert_name(f_client, dgv_result.Rows[i].Cells["client"].ToString());


                    }
                }
            }
        } 
        private void btn_into_offical_Click(object sender, EventArgs e)
        {
            string sql = "select * from europe_100_log where f_state='3'";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                string website = row["website"].ToString();
                string time_span = row["timespan"].ToString();
                string start_time = row["f_start_time"].ToString();
                string league = row["league"].ToString();
                string host = row["f_host"].ToString();
                string client = row["f_client"].ToString();
                string odd_win = row["odd_win"].ToString().Trim();
                string odd_draw = row["odd_draw"].ToString().Trim();
                string odd_lose = row["odd_lose"].ToString().Trim();

                sql = "select * from europe_100 where website='{0}' and  start_time='{1}' and host='{2}' and client='{3}' and odd_win='{4}' and odd_draw='{5}' and odd_lose='{6}'";
                sql = string.Format(sql, website,start_time, host, client,odd_win,odd_lose,odd_lose);
                DataTable dt_temp = SQLServerHelper.get_table(sql);
                if (dt_temp.Rows.Count == 0)
                {
                    sql = " insert into europe_100  (timespan,website,type,start_time,host,client,odd_win,odd_draw,odd_lose) values" +
                          " ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
                    sql = string.Format(sql, time_span, website, league, start_time, host, client, odd_win, odd_draw, odd_lose);
                    SQLServerHelper.exe_sql(sql);
                }
            }
        } 
    }
    
}
