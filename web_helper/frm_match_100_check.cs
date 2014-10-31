using System;
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

        private void btn_analyse_one_by_other_Click(object sender, EventArgs e)
        {
            analyse_one_by_other();
        }
        private void btn_analyse_by_date_odd_Click(object sender, EventArgs e)
        {
            analyse_by_time_and_odd();
        }
        private void btn_analyse_by_similar_name_Click(object sender, EventArgs e)
        {
            analyse_by_similar_name();
        }
        private void btn_analyse_by_hand_Click(object sender, EventArgs e)
        {
            analyse_by_hand();
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
        private void btn_insert_office_Click(object sender, EventArgs e)
        {
            insert_office();
            MessageBox.Show("Inert Office OK!");
        }
        private void btn_repair_Click(object sender, EventArgs e)
        { 
            string sql = " select * " +
                         " from europe_100 " +
                         " where id in (select max(id) from europe_100 where start_time>'{0}'   and website<>'marathonbet' group by website,start_time,host,client)";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt1 = SQLServerHelper.get_table(sql);

             sql = " select * " +
                        " from europe_100 " +
                        " where id in (select max(id) from europe_100 where start_time>'{0}'   and website='marathonbet' group by website,start_time,host,client)";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt2 = SQLServerHelper.get_table(sql);

            foreach (DataRow row1 in dt1.Rows)
            {
                foreach (DataRow row2 in dt2.Rows)
                {
                    if (row1["host"].ToString() == row2["host"].ToString() && row1["client"].ToString() == row2["client"].ToString() && row1["start_time"].ToString() != row2["start_time"].ToString())
                    {
                        DateTime time1 = Convert.ToDateTime(row1["start_time"].ToString());
                        DateTime time2 = Convert.ToDateTime(row2["start_time"].ToString());
                        TimeSpan span = time1 - time2;
                        if (span.TotalHours >= -12 && span.TotalHours <= 12)
                        {
                            sql = "update europe_100  set start_time='{0}' where start_time='{1}' and host='{2}' and client='{3}'";
                            sql = string.Format(sql, row1["start_time"].ToString(), row2["start_time"].ToString(), row2["host"].ToString(), row2["client"].ToString());
                            //SQLServerHelper.exe_sql(sql); 
                            sb.AppendLine(row2["start_time"].PR(20) + row2["host"].PR(30) + row2["client"].PR(30) + row1["start_time"].PR(20));
                            this.txt_result.Text = sb.ToString();
                            Application.DoEvents();
                        }

                    }

                }


            }


            


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

        //anlayse one by other
        public void analyse_one_by_other()
        {
            string sql = "";
            DataTable dt = new DataTable();

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


        }

        //analyse by team  split name word
        public void analyse_by_similar_name()
        {
            string sql = "select id  from europe_100_log where (f_state='1' or f_state='2') and   timespan>'{0}' ";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd ") + "00:00:00");
            DataTable dt_id = SQLServerHelper.get_table(sql);
            foreach (DataRow row_id in dt_id.Rows)
            {
                string max_id = row_id[0].ToString();
                //max id row
                sql = "select * from  europe_100_log where id='{0}' and (f_state='1' or f_state='2')";
                sql = string.Format(sql, max_id);
                DataTable dt = SQLServerHelper.get_table(sql);
                if (dt.Rows.Count == 0) continue;
                DataRow row = dt.Rows[0];


                bool has_update = false;
                sql = "select * from europe_100_log  where  f_start_time='{0}' and website<>'{1}'";
                sql = string.Format(sql, row["f_start_time"].ToString(), row["website"].ToString());
                DataTable dt_temp = SQLServerHelper.get_table(sql);
                foreach (DataRow row_temp in dt_temp.Rows)
                {
                    if (row["f_start_time"].ToString() == row_temp["f_start_time"].ToString())
                    {
                        if (is_similar_name(row["host"].ToString(), row_temp["host"].ToString()) && is_similar_name(row["client"].ToString(), row_temp["client"].ToString()))
                        {
                            sb.AppendLine(row["website"].PR(15) + row["league"].PR(50) + row["f_start_time"].PR(20) + row["host"].PR(30) + row["client"].PR(30) + row["f_host"].PR(30) + row["f_client"].PR(30));
                            sb.AppendLine(row_temp["website"].PR(15) + row_temp["league"].PR(50) + row_temp["f_start_time"].PR(20) + row_temp["host"].PR(30) + row_temp["client"].PR(30) + row_temp["f_host"].PR(30) + row_temp["f_client"].PR(30));
                            sb.AppendLine("-------------------------------------------------------------------------------------------------------------------");
                            this.txt_result.Text = sb.ToString();
                            Application.DoEvents();

                            string host = "";
                            string client = "";
                            string sql_update = "update europe_100_log set host='{0}', client='{1}',f_state='3' where id='{2}'";
                            if (has_update == false)
                            {
                                host = Match100Helper.insert_name_all(row["host"].ToString(), row_temp["host"].ToString());
                                client = Match100Helper.insert_name_all(row["client"].ToString(), row_temp["client"].ToString());
                                sql = string.Format(sql_update, host, client, row["id"].ToString());
                                SQLServerHelper.exe_sql(sql);
                                sql = string.Format(sql_update, host, client, row_temp["id"].ToString());
                                SQLServerHelper.exe_sql(sql);
                                row["f_host"] = host;
                                row["f_client"] = client;
                                has_update = true;
                            }
                            else
                            {
                                host = row["f_host"].ToString();
                                client = row["f_client"].ToString();
                                Match100Helper.insert_name(host, row_temp["host"].ToString());
                                Match100Helper.insert_name(client, row_temp["client"].ToString());
                                sql = string.Format(sql_update, host, client, row_temp["id"].ToString());
                                SQLServerHelper.exe_sql(sql);
                            }
                        }
                    }
                }
            }

        }
        public bool is_similar_name(string name1, string name2)
        {
            if (string.IsNullOrEmpty(name1) || string.IsNullOrEmpty(name2)) return false;
            name1 = replace_special_match_word(name1);
            name2 = replace_special_match_word(name2);

            string[] names1 = name1.E_SPLIT(" ");
            string[] names2 = name2.E_SPLIT(" ");

            foreach (string item1 in names1)
            {
                foreach (string item2 in names2)
                {
                    if (item1 == item2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public string replace_special_match_word(string name)
        {
            name = name.Replace("/r/n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("\v", " ").Replace("\f", " ").Replace("&nbsp;", " ").Replace("<br>", " ").Replace("<BR>", " ");
            name = name.ToLower();
            name = name.Replace("fc", " ").Replace("u-16", " ").Replace("u16", " ").Replace("u 16", " ")
                                         .Replace("u-17", " ").Replace("u17", " ").Replace("u 17", " ")
                                         .Replace("u-18", " ").Replace("u18", " ").Replace("u 18", " ")
                                         .Replace("u-19", " ").Replace("u19", " ").Replace("u 19", " ")
                                         .Replace("u-20", " ").Replace("u20", " ").Replace("u 20", " ");
            return name;
        }
        public int similar_word_count(string name1, string name2)
        {
            int count = 0;

            if (string.IsNullOrEmpty(name1) || string.IsNullOrEmpty(name2)) return count;
            name1 = name1.Replace("/r/n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("\v", " ").Replace("\f", " ").Replace("&nbsp;", " ").Replace("<br>", " ").Replace("<BR>", " ").Replace("(", " ").Replace(")", " ");
            name2 = name1.Replace("/r/n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("\v", " ").Replace("\f", " ").Replace("&nbsp;", " ").Replace("<br>", " ").Replace("<BR>", " ").Replace("(", " ").Replace(")", " ");
            name1 = name1.ToLower();
            name2 = name2.ToLower();
            name1 = name1.Replace("fc", " ");
            name2 = name2.Replace("fc", " ");

            string[] names1 = name1.E_SPLIT(" ");
            string[] names2 = name2.E_SPLIT(" ");

            foreach (string item1 in names1)
            {
                foreach (string item2 in names2)
                {
                    if (item1 == item2)
                    {
                        count = count + 1;
                    }
                }
            }
            return count;
        }


        //analyse by start_time and win persent
        public void analyse_by_time_and_odd()
        {
            //one by one 
            string sql_select = "" +
                " select id,timespan,website,start_time,host,client,odd_win,odd_draw,odd_lose,time_zone,time_add,f_state,f_league,f_start_time,f_host,f_client,'' group_id," +
                        " ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00),2)  persent_return," +
                        " ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00)/CONVERT(float,odd_win),2) persent_win," +
                        " ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00)/CONVERT(float,odd_draw),2) persent_draw," +
                        " ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00)/CONVERT(float,odd_lose),2) persent_lose " +
                " from europe_100_log" +

                " where ISNUMERIC(odd_win)=1" +
                " and   ISNUMERIC(odd_draw)=1" +
                " and   ISNUMERIC(odd_lose)=1 " +
                " and   id in (select max(id) from europe_100_log group by website,start_time,host,client) ";

            string sql = "select id from europe_100_log where id in (select max(id) from europe_100_log where (f_state='1' or f_state='2') and   timespan>'{0}' group by website,start_time,host,client) ";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd ") + "00:00:00");
            DataTable dt_id = SQLServerHelper.get_table(sql);
            foreach (DataRow row_id in dt_id.Rows)
            {
                Application.DoEvents();
                DataRow row;
                sql = sql_select + "and id='{0}' and (f_state='1' or f_state='2')";
                sql = string.Format(sql, row_id[0].ToString());
                DataTable dt = SQLServerHelper.get_table(sql);
                if (dt.Rows.Count == 0) continue;
                row = dt.Rows[0];


                int group_id = 0;
                DataTable dt_group = dt.Clone();

                //all this website's & this host's match
                sql = sql_select + "and website='{0}' and ( host='{1}' or client ='{1}')";
                sql = string.Format(sql, row["website"].ToString(), row["host"].ToString());
                DataTable dt_host = SQLServerHelper.get_table(sql);

                foreach (DataRow row_host in dt_host.Rows)
                {

                    //all this start_time's match
                    sql = sql_select + " and id in (select max(id) from europe_100_log where website<>'{0}' and  f_start_time='{1}' group by website,start_time,host,client) ";
                    sql = string.Format(sql, row_host["website"].ToString(), row_host["f_start_time"].ToString());
                    DataTable dt_start_time = SQLServerHelper.get_table(sql);

                    bool is_add = false;
                    foreach (DataRow row_start_time in dt_start_time.Rows)
                    {
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
                dt_website.Columns.Add("group");
                foreach (DataRow row_group in dt_group.Rows)
                {
                    bool is_has = false;
                    foreach (DataRow row_website in dt_website.Rows)
                    {
                        if (row_website["website"].ToString() == row_group["website"].ToString())
                        {
                            is_has = true;
                            if (!row_website["group"].ToString().Contains("[" + row_group["group_id"].ToString() + "]"))
                            {
                                row_website["qty"] = (Convert.ToInt32(row_website["qty"].ToString()) + 1).ToString();
                                row_website["group"] = row_website["group"].ToString() + "[" + row_group["group_id"].ToString() + "]";
                            }


                        }
                    }
                    if (is_has == false)
                    {
                        DataRow row_new = dt_website.NewRow();
                        row_new["website"] = row_group["website"].ToString();
                        row_new["qty"] = "1";
                        row_new["group"] = "[" + row_group["group_id"].ToString() + "]";

                        dt_website.Rows.Add(row_new);
                    }
                }
                //when count>2,find the same match to update team names
                foreach (DataRow row_website in dt_website.Rows)
                {
                    if (Convert.ToDecimal(row_website["qty"].ToString()) > 1 && row_website["website"].ToString() != row["website"].ToString())
                    {
                        foreach (DataRow row_group in dt_group.Rows)
                        {
                            if (row_group["website"].ToString() == row_website["website"].ToString())
                            {
                                string id_1 = "";
                                string id_2 = "";
                                string host_1 = "";
                                string host_2 = "";
                                string client_1 = "";
                                string client_2 = "";
                                id_1 = row_group["id"].ToString();
                                host_1 = row_group["host"].ToString();
                                client_1 = row_group["client"].ToString();


                                bool is_fit = false;
                                foreach (DataRow row_group_1 in dt_group.Rows)
                                {
                                    if (
                                       row_group_1["website"].ToString() == row_group["website"].ToString() &&
                                       row_group_1["group_id"].ToString() != row_group["group_id"].ToString() &&
                                      (row_group_1["host"].ToString() == row_group["host"].ToString() || row_group_1["host"].ToString() == row_group["client"].ToString() ||
                                       row_group_1["client"].ToString() == row_group["host"].ToString() || row_group_1["client"].ToString() == row_group["host"].ToString())
                                       )
                                    {
                                        is_fit = true;
                                    }
                                }
                                if (is_fit == true)
                                {
                                    foreach (DataRow row_group_1 in dt_group.Rows)
                                    {
                                        if (row_group_1["website"].ToString() == row["website"].ToString() && row_group_1["group_id"].ToString() == row_group["group_id"].ToString())
                                        {
                                            id_2 = row_group_1["id"].ToString();
                                            host_2 = row_group_1["host"].ToString();
                                            client_2 = row_group_1["client"].ToString();

                                            //update id_1 and id_2
                                            update_two(id_1, host_1, client_1, id_2, host_2, client_2);

                                            sb.AppendLine(row_group_1["id"].PR(5) + row_group_1["start_time"].ToString().PR(20) + row_group_1["host"].PR(30) + row_group_1["client"].PR(30));
                                            sb.AppendLine(row_group["id"].PR(5) + row_group["start_time"].ToString().PR(20) + row_group["host"].PR(30) + row_group["client"].PR(30));
                                            sb.AppendLine("--------------------------------------------------------------------------------------------------------------------------------");
                                            this.txt_result.Text = sb.ToString();
                                            Application.DoEvents();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void update_two(string id_1, string host_1, string client_1, string id_2, string host_2, string client_2)
        {
            string sql = "select * from europe_100_log where id  in('{0}','{1}')";
            sql = string.Format(sql, id_1, id_2);
            DataTable dt = SQLServerHelper.get_table(sql);

            string host = "";
            string client = "";

            if (!string.IsNullOrEmpty(dt.Rows[0]["f_host"].ToString()) && string.IsNullOrEmpty(dt.Rows[1]["f_host"].ToString()))
            {
                //Match100Helper.insert_name(dt.Rows[0]["f_host"].ToString(), dt.Rows[1]["f_host"].ToString());
                host = dt.Rows[0]["f_host"].ToString();
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["f_host"].ToString()) && !string.IsNullOrEmpty(dt.Rows[1]["f_host"].ToString()))
            {
                // Match100Helper.insert_name(dt.Rows[1]["f_host"].ToString(), dt.Rows[0]["f_host"].ToString());
                host = dt.Rows[1]["f_host"].ToString();
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["f_host"].ToString()) && string.IsNullOrEmpty(dt.Rows[1]["f_host"].ToString()))
            {
                //Match100Helper.insert_name_all(dt.Rows[0]["f_host"].ToString(), dt.Rows[1]["f_host"].ToString());
                host = dt.Rows[0]["f_host"].ToString();
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["f_client"].ToString()) && string.IsNullOrEmpty(dt.Rows[1]["f_client"].ToString()))
            {
                // Match100Helper.insert_name(dt.Rows[0]["f_client"].ToString(), dt.Rows[1]["f_client"].ToString());
                client = dt.Rows[0]["f_client"].ToString();
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["f_client"].ToString()) && !string.IsNullOrEmpty(dt.Rows[1]["f_client"].ToString()))
            {
                //Match100Helper.insert_name(dt.Rows[1]["f_client"].ToString(), dt.Rows[0]["f_client"].ToString());
                client = dt.Rows[1]["f_client"].ToString();
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["f_client"].ToString()) && string.IsNullOrEmpty(dt.Rows[1]["f_client"].ToString()))
            {
                //Match100Helper.insert_name_all(dt.Rows[0]["f_client"].ToString(), dt.Rows[1]["f_client"].ToString());
                client = dt.Rows[0]["f_client"].ToString();
            }
            sql = "update europe_100_log set f_host='{0}' , f_client='{1}',f_state='3'  where id in ('{2}','{3}')";
            sql = string.Format(sql, host, client, id_1, id_2);
            //SQLServerHelper.exe_sql(sql); 
        }


        //analyse by hand 
        public void analyse_by_hand()
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

        //analyse by odd
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

        //insert office
        public void insert_office()
        {
            string sql = "";
            DataTable dt = new DataTable();
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
                sql = string.Format(sql, website, start_time, host, client, odd_win, odd_draw, odd_lose, max_id);
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
                    sql = string.Format(sql, time_span, max_id);
                    SQLServerHelper.exe_sql(sql);
                }
                sql = "update europe_100_log set f_state='4' where id={0}";
                sql = string.Format(sql, id);
                SQLServerHelper.exe_sql(sql);
            }

        }




    }
}
