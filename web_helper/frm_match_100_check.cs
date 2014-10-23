﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;

namespace web_helper
{
    public partial class frm_match_100_check : Form
    {
        StringBuilder sb = new StringBuilder();
        public frm_match_100_check()
        {
            InitializeComponent();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }
        private void btn_team_discrimination_Click(object sender, EventArgs e)
        {
            team_discrimination();
        }
        private void btn_check_matchs_Click(object sender, EventArgs e)
        {
            check_match_odd_count();
        }
        private void btn_add_all_Click(object sender, EventArgs e)
        {
            add_all_to_europe_100();
        }
        private void btn_check_qty_Click(object sender, EventArgs e)
        {
            load_qty();
        }



        public void team_discrimination()
        {
            string sql = "select isnull(min(id),0) from europe_100_log";
            DataTable dt_temp = SQLServerHelper.get_table(sql);
            string start_id = dt_temp.Rows[0][0].ToString();

            sql = "select distinct website  from europe_100_log where id> {0} ";
            sql = string.Format(sql, start_id);

            DataTable dt_website = SQLServerHelper.get_table(sql);
            sb.AppendLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (DataRow row in dt_website.Rows)
            {
                string website = row["website"].ToString();
                sql = "select  *  from europe_100_log where id>'{0}' and website='{1}' and f_state='{2}'";
                string sql_temp = "";

                sql_temp = string.Format(sql, start_id, website, "1");
                int team_zero = SQLServerHelper.get_table(sql_temp).Rows.Count;

                sql_temp = string.Format(sql, start_id, website, "2");
                int team_single = SQLServerHelper.get_table(sql_temp).Rows.Count;


                sql_temp = string.Format(sql, start_id, website, "3");
                int team_all_3 = SQLServerHelper.get_table(sql_temp).Rows.Count;

                sql_temp = string.Format(sql, start_id, website, "4");
                int team_all_4 = SQLServerHelper.get_table(sql_temp).Rows.Count;

                sql_temp = string.Format(sql, start_id, website, "w");
                int team_w = SQLServerHelper.get_table(sql_temp).Rows.Count;

                int team_all = team_all_3 + team_all_4;

                int total = team_zero + team_single + team_all + team_w;
                sb.AppendLine(website.PR(20) + "Team All:   " + team_all.PR(5) + (Math.Round(Convert.ToDouble(team_all) / total * 100, 2).ToString() + "%").PR(12) +
                                               "Team Single:   " + team_single.PR(5) + (Math.Round(Convert.ToDouble(team_single) / total * 100, 2).ToString() + "%").PR(12) +
                                               "Team Zero:   " + team_zero.PR(5) + (Math.Round(Convert.ToDouble(team_zero) / total * 100, 2).ToString() + "%").PR(12) +
                                               "Team Wrong:   " + team_w.PR(5) + (Math.Round(Convert.ToDouble(team_w) / total * 100, 2).ToString() + "%").PR(12));
                this.txt_result.Text = sb.ToString();
                Application.DoEvents();
            }
            sb.AppendLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void check_match_odd_count()
        {

            string sql = " select start_time,host,client  " +
                         " from (select distinct website,start_time,host,client from europe_100 where  start_time>'{0}') a" +
                         " group by start_time,host,client" +
                         " having count(*)>1";
            sql = string.Format(sql, DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt_match = SQLServerHelper.get_table(sql);
            foreach (DataRow row_match in dt_match.Rows)
            {
                string start_time = row_match["start_time"].ToString();
                string host = row_match["host"].ToString();
                string client = row_match["client"].ToString();

                sql = " select distinct website from europe_100 where start_time='{0}' and host='{1}' and client='{2}'";
                sql = string.Format(sql, start_time, host, client);
                DataTable dt_website = SQLServerHelper.get_table(sql);
                for (int i = 0; i < dt_website.Rows.Count; i++)
                {
                    string website = dt_website.Rows[i]["website"].ToString();

                    sql = " select * from   europe_100 " +
                          " where id=(select max(id) from europe_100 where website='{0}' and start_time='{1}' and host='{2}' and client='{3}')";
                    sql = string.Format(sql, website, start_time, host, client);
                    DataTable dt_odd = SQLServerHelper.get_table(sql);

                    string win = dt_odd.Rows[0]["odd_win"].ToString();
                    string draw = dt_odd.Rows[0]["odd_draw"].ToString();
                    string lose = dt_odd.Rows[0]["odd_lose"].ToString();

                    if (i == 0)
                    {
                        sb.AppendLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
                        sb.AppendLine(start_time.PR(20) + host.PR(30) + client.PR(30) + website.PR(20) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        this.txt_result.Text = sb.ToString();
                        Application.DoEvents();
                    }
                    else
                    {
                        sb.AppendLine("".PR(20) + "".PR(30) + "".PR(30) + website.PR(20) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        this.txt_result.Text = sb.ToString();
                        Application.DoEvents();
                    }
                }
            }
            sb.AppendLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void add_all_to_europe_100()
        {
            string sql = "select * from europe_100 where  persent_win is null";
            DataTable dt_table = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_table.Rows)
            {
                string id = row["id"].ToString();
                string start_time = row["start_time"].ToString();
                string host = row["host"].ToString();
                string client = row["client"].ToString();
                string odd_win = row["odd_win"].ToString();
                string odd_draw = row["odd_draw"].ToString();
                string odd_lose = row["odd_lose"].ToString();

                if (!string.IsNullOrEmpty(odd_win) && !string.IsNullOrEmpty(odd_draw) && !string.IsNullOrEmpty(odd_lose))
                {
                    BsonDocument doc_odd = Match100Helper.get_odd_doc_from_europe(odd_win, odd_draw, odd_lose);
                    string persent_win = doc_odd["persent_win"].ToString();
                    string persent_draw = doc_odd["persent_draw"].ToString();
                    string persent_lose = doc_odd["persent_lose"].ToString();
                    string persent_return = doc_odd["persent_return"].ToString();

                    sql = "update europe_100 set persent_win='{0}',persent_draw='{1}',persent_lose='{2}',persent_return='{3}' where id='{4}'";
                    sql = string.Format(sql, persent_win, persent_draw, persent_lose, persent_return, id);
                    SQLServerHelper.exe_sql(sql);
                    sb.AppendLine(start_time.PR(20) + host.PR(30) + client.PR(30) + odd_win.PR(10) + odd_lose.PR(10) + odd_lose.PR(10) + persent_win.PR(10) + persent_draw.PR(10) + persent_lose.PR(10) + persent_return.PR(10));
                    this.txt_result.Text = sb.PRINT();
                    Application.DoEvents();

                }
            }
        }
        public void load_qty()
        {
            string sql = "select distinct substring(timespan,0,11) date  from europe_100_log order by date desc";
            DataTable dt_date = SQLServerHelper.get_table(sql);
            if (dt_date.Rows.Count > 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    string date = dt_date.Rows[i][0].ToString();
                    sql = " select website, count(*) qty" +
                        " from europe_100_log" +
                        " where substring(timespan,0,11)='{0}'" +
                        " group by website" +
                        " order by qty";
                    sql = string.Format(sql, date);

                    DataTable dt = SQLServerHelper.get_table(sql);
                    sb.AppendLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
                    sb.AppendLine(date);
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine(row[0].ToString().PR(20) + row[1].ToString().PR(10));
                    }
                }
                sb.AppendLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
            }

            this.txt_result.Text = sb.ToString();
        }

        private void btn_analyse_Click(object sender, EventArgs e)
        {
            analyse();
            MessageBox.Show("Analyse OK!");
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



            // string sql = " select * from europe_100_log where f_state in('1','2') and f_start_time>'{0}' ";
            string sql = " select * from  europe_100_log " +
                " where id in" +
                " (" +
                " select max(id)" +
                " from europe_100_log " +
                " where f_state in('1','2') and f_start_time>'{0}' " +
                " group by website,league,start_time,host,client,f_league,f_start_time,f_host,f_client" +
                " )";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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

                sql = " select * from europe_100_log " +
                      " where  id in" +
                        " (" +
                        " select max(id)" +
                        " from europe_100_log " +
                        " where f_state in('1','2','4') and f_start_time>'{0}' " +
                        " group by website,league,start_time,host,client,f_league,f_start_time,f_host,f_client" +
                        " )" +
                      "  and id<>'{1}' and f_start_time='{2}'";
                sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), id, f_start_time);
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
                    this.txt_result.Text = sb.PRINT();
                    Application.DoEvents();
                }
                sb.AppendLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

            }
            this.txt_result.Text = sb.PRINT();
            Application.DoEvents();
            this.dgv_result.DataSource = dt_result;
        }



        //the function  used for future
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
                string win = row["odd_win"].ToString();
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

        public void analyse_by_date_and_odd()
        {
            string sql_select = "" +
                " select id,timespan,website,start_time,host,client,odd_win,odd_draw,odd_lose,time_zone,time_add,f_league,f_start_time,f_host,f_client,'' group_id," +
                        " ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00),2)  persent_return," +
                        " ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00)/CONVERT(float,odd_win),2) persent_win," +
                        " ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00)/CONVERT(float,odd_draw),2) persent_draw," +
                        " ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00)/CONVERT(float,odd_lose),2) persent_lose " +
                " from europe_100_log" +
                " where ISNUMERIC(odd_win)=1" +
                " and   ISNUMERIC(odd_draw)=1" +
                " and   ISNUMERIC(odd_lose)=1 " +
                " and   id in (select max(id) from europe_100_log group by website,start_time,host,client) ";

            string sql = "";

            sql = sql_select + " and time_span>'{0}'";
            sql = string.Format(sql, DateTime.Now.ToString("yyyyMMddHHmmss") + "000");
            DataTable dt = SQLServerHelper.get_table(sql);

          
            foreach (DataRow row in dt.Rows)
            {
                int group_id = 0;
                DataTable dt_group = dt.Clone();

                //all this website's & this host's match
                sql = sql_select + " 　and website='{0}' and ( host='{1}' or client ='{1}')";
                sql = string.Format(sql, row["website"].ToString(), row["host"].ToString());
                DataTable dt_host = SQLServerHelper.get_table(sql); 

                foreach (DataRow row_host in dt_host.Rows)
                {
                 
                    //all this start_time's match
                    sql = sql_select + " 　and  website<>'{0}' and  f_start_time='{1}'";
                    sql = string.Format(sql, row_host["website"].ToString(), row_host["f_start_time"].ToString());
                    DataTable dt_start_time = SQLServerHelper.get_table(sql);

                    foreach (DataRow row_start_time in dt_start_time.Rows)
                    {
                        bool is_add = false;
                        if (row_host["f_start_time"].ToString() == row_start_time["f_start_time"].ToString())
                        {
                            if (Convert.ToDecimal(row_host["persent_win"].ToString()) > Convert.ToDecimal(row_start_time["persent_win"].ToString()) - 5 &&
                                Convert.ToDecimal(row_host["persent_win"].ToString()) < Convert.ToDecimal(row_start_time["persent_win"].ToString()) + 5 &&
                                Convert.ToDecimal(row_host["persent_draw"].ToString()) > Convert.ToDecimal(row_start_time["persent_draw"].ToString()) - 5 &&
                                Convert.ToDecimal(row_host["persent_draw"].ToString()) < Convert.ToDecimal(row_start_time["persent_draw"].ToString()) + 5 &&
                                Convert.ToDecimal(row_host["persent_lose"].ToString()) > Convert.ToDecimal(row_start_time["persent_lose"].ToString()) - 5 &&
                                Convert.ToDecimal(row_host["persent_lose"].ToString()) < Convert.ToDecimal(row_start_time["persent_lose"].ToString()) + 5)
                            {
                                if (is_add == false)
                                {
                                    is_add = true;
                                    group_id = group_id + 1;
                                    row_host["group_id"] = group_id.ToString();
                                    dt_group.ImportRow(row_host);
                                }
                                
                                row_start_time["group_id"] = group_id.ToString();
                                dt_group.ImportRow(row_start_time); 
                            }
                        }
                    } 
                }

                //analyse dt_group
                DataTable dt_website = new DataTable();
                dt_website.Columns.Add("website");
                dt_website.Columns.Add("qty");
                foreach (DataRow row_group in dt_group.Rows)
                {
                    bool is_has = false;
                    foreach (DataRow row_website in dt_website.Rows)
                    {
                        if (row_website["website"].ToString() == row_group["website"].ToString())
                        { 
                            is_has = true;
                            row_website["qty"] = (Convert.ToInt16(row_website["qty"].ToString() + 1)).ToString();
                        } 
                    }
                    if (is_has == false)
                    { 
                        DataRow row_new = dt_website.NewRow();
                        row_new["website"] = row_group["website"].ToString();
                        row_new["qty"] = "0";
                        dt_website.Rows.Add(row_new);
                    } 
                }




            }

        }
    }
}
