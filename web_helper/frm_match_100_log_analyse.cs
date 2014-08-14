﻿using System;
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
        }

        //state: 0-未处理   1-已处理，值更新了时间   2-已处理，一个匹配   3-已处理，2个匹配
        public void update_standard_data()
        {
            string sql = "select * from europe_100_log where state='0' ";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                string id = row["id"].ToString();
                string start_time = row["start_time"].ToString();
                string host = row["host"].ToString();
                string client = row["client"].ToString();
                string win = row["profit_win"].ToString();
                string draw = row["profit_draw"].ToString();
                string lose = row["profit_lose"].ToString();
                string time_zone = row["time_zone"].ToString();
                string time_add = row["time_add"].ToString();

                string convert_host = "";
                string convert_client = "";
                string convert_time = "";
                string state = "1";

                convert_host = Match100Helper.convert_team_name(host);
                convert_client = Match100Helper.convert_team_name(client);
                DateTime time = Match100Helper.convert_start_time(start_time);
                time = time.AddMinutes(Convert.ToInt16(time_add));
                time = Tool.get_time_by_kind(time, Convert.ToInt16(time_zone));
                convert_time = time.ToString("yyyy-MM-dd HH:mm:ss");

                if (!string.IsNullOrEmpty(convert_host) || !string.IsNullOrEmpty(convert_client))
                {
                    state = "2";
                }
                if (!string.IsNullOrEmpty(convert_host) && !string.IsNullOrEmpty(convert_client))
                {
                    state = "3";
                }
                sql = " update europe_100_log set f_host='{0}',f_client='{1}',f_start_time='{2}' state='{3}' where id='{4}'";
                sql = string.Format(sql, convert_time, convert_host, convert_client, state, id);

                sb.AppendLine(convert_time.PR(30) + convert_host.PR(50) + convert_client.PR(50));
                this.txt_result.Text = sb.ToString();
                Application.DoEvents();

            }

            sql = "select * from europe_100_log where state='1' ";
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

                            sql = " update europe_100_log set f_host='{0}',state='3' where id='{1}'";
                            sql = string.Format(sql, row_temp["f_host"].ToString(), id);
                            SQLServerHelper.exe_sql(sql);
                            Match100Helper.insert_team(row_temp["f_host"].ToString(), host);


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
                            sql = " update europe_100_log set f_client='{0}',state='3' where id='{1}'";
                            sql = string.Format(sql, row_temp["f_client"].ToString(), id);
                            SQLServerHelper.exe_sql(sql);
                            Match100Helper.insert_team(row_temp["f_client"].ToString(), client);

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
            dt_result.Columns.Add("target_id");



            string sql = " select * from europe_100_log where state ='1' or state='2' ";
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
                string win = row["profit_win"].ToString();
                string draw = row["profit_draw"].ToString();
                string lose = row["profit_lose"].ToString();
                string time_zone = row["time_zone"].ToString();
                string time_add = row["time_add"].ToString();
                string state = row["state"].ToString();

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
                row_new["target_id"] = "";
                dt_result.Rows.Add(row_new);

                sb.AppendLine("---------------------------------------------------------------------------------------------------------");
                sb.AppendLine(id.PR(5) + start_time.PR(20) + host.PR(30) + client.PR(30) + f_start_time.PR(20) + f_host.PR(30) + f_client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                sql = " select * from europe_100_log where (state='2' or state='3') and f_start_time='{1}' and id<>'{2}'";
                sql = string.Format(sql, f_start_time, id);
                DataTable dt_temp = SQLServerHelper.get_table(sql);
                foreach (DataRow row_temp in dt.Rows)
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
                    row_new_temp["win"] = row_temp["profit_win"].ToString();
                    row_new_temp["draw"] = row_temp["profit_draw"].ToString();
                    row_new_temp["lose"] = row_temp["profit_lose"].ToString();
                    row_new_temp["target_id"] = "";
                    dt_result.Rows.Add(row_new_temp);

                    sb.AppendLine("".PR(5) + row_temp["start_time"].PR(20) + row_temp["host"].PR(30) + row_temp["client"].PR(30) + row_temp["f_start_time"].PR(20) + row_temp["f_host"].PR(30) + row_temp["f_client"].PR(30) + row_temp["profit_win"].PR(10) + row_temp["profit_draw"].PR(10) + row_temp["profit_lose"].PR(10));
                }
                sb.AppendLine("---------------------------------------------------------------------------------------------------------");
            }
            this.dgv_result.DataSource = dt_result;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string sql = "";
            for (int i = 0; i < dgv_result.Rows.Count - 1; i++)
            {
                if (dgv_result.Rows[i].Cells["type"].Value == null) continue;
                if (dgv_result.Rows[i].Cells["type"].Value.ToString() != "M") continue;
                if (dgv_result.Rows[i].Cells["target_id"].Value == null) continue;

                string id = dgv_result.Rows[i].Cells["id"].Value.ToString();
                string target_id = dgv_result.Rows[i].Cells["target_id"].Value.ToString();

                for (int j = 0; j < dgv_result.Rows.Count - 1; j++)
                {
                    if (dgv_result.Rows[i].Cells["id"].ToString() == target_id)
                    {
                        string host = dgv_result.Rows[j].Cells["host"].ToString();
                        string client = dgv_result.Rows[j].Cells["client"].ToString();
                        string f_host = dgv_result.Rows[j].Cells["f_host"].ToString();
                        string f_client = dgv_result.Rows[j].Cells["f_client"].ToString();

                        sql = " update europe_100_log set f_host='{0}',f_client='{1}',state='3'  where id='{2}' ";
                        sql = string.Format(sql, f_host, f_client, id);
                        SQLServerHelper.exe_sql(sql);
                        sql = " update europe_100_log set f_host='{0}',f_client='{1},state='3'  where id='{2}' ";
                        sql = string.Format(sql, f_host, f_client, target_id);
                        SQLServerHelper.exe_sql(sql);

                        Match100Helper.insert_team(f_host, dgv_result.Rows[i].Cells["host"].ToString());
                        Match100Helper.insert_team(f_client, dgv_result.Rows[i].Cells["client"].ToString());


                    }
                }
            }
        }

        private void btn_into_offical_Click(object sender, EventArgs e)
        {
            string sql = "select * from europe_100_log where state='3'";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                string company = row["company"].ToString();
                string time_span = row["time_span"].ToString();
                string start_time = row["f_start_time"].ToString();
                string type = row["type"].ToString();
                string host = row["f_host"].ToString();
                string client = row["f_client"].ToString();
                string profit_win = row["profit_win"].ToString();
                string profit_draw = row["profit_draw"].ToString();
                string profit_lose = row["profit_lose"].ToString();

                sql = "select * from europe_100 where company='{0}'  and time_span='{1}' and start_time='{2}' and host='{3}' and client='{4}'";
                sql = string.Format(sql, company, time_span, start_time, host, client);
                DataTable dt_temp = SQLServerHelper.get_table(sql);
                if (dt_temp.Rows.Count == 0)
                {
                    sql = " insert into europe_100  (timespan,company,type,start_time,host,client,profit_win,profit_draw,profit_lose) values" +
                          " ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
                    sql = string.Format(sql, time_span, company, type, start_time, host, client, profit_win, profit_draw, profit_lose);
                    SQLServerHelper.exe_sql(sql);
                }
            }
        }

    }
}
