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
        private void btn_check_persent_Click(object sender, EventArgs e)
        {
            check_team_recongition_persent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            check_match_odd_count();
        }
 
        public void check_team_recongition_persent()
        { 
            string sql = "select isnull(min(id),0) from europe_100_log";
            DataTable dt_temp = SQLServerHelper.get_table(sql);
            string start_id = dt_temp.Rows[0][0].ToString();

            sql = "select distinct website  from europe_100_log where id> {0} ";
            sql = string.Format(sql, start_id);

            DataTable dt_website = SQLServerHelper.get_table(sql); 
            foreach (DataRow row in dt_website.Rows)
            {
                string website=row["website"].ToString();
                sql = "select  *  from europe_100_log where id>'{0}' and website='{1}' and f_state='{2}'";
                string sql_temp = "";

                sql_temp = string.Format(sql, start_id, website, "1");
                int team_zero = SQLServerHelper.get_table(sql_temp).Rows.Count;

                sql_temp = string.Format(sql, start_id,website, "2");
                int team_single = SQLServerHelper.get_table(sql_temp).Rows.Count;


                sql_temp = string.Format(sql, start_id, website, "3");
                int team_all_3 = SQLServerHelper.get_table(sql_temp).Rows.Count;

                sql_temp = string.Format(sql, start_id, website, "4");
                int team_all_4 = SQLServerHelper.get_table(sql_temp).Rows.Count;

                sql_temp = string.Format(sql, start_id, website, "w");
                int team_w = SQLServerHelper.get_table(sql_temp).Rows.Count;

                int team_all = team_all_3 + team_all_4;

                int total = team_zero + team_single + team_all+team_w;
                sb.AppendLine(website.PR(20) + "Team All:   " + team_all.PR(5) + (Math.Round(Convert.ToDouble(team_all) / total * 100, 2).ToString() + "%").PR(12) +
                                               "Team Single:   " + team_single.PR(5) + (Math.Round(Convert.ToDouble(team_single) / total * 100, 2).ToString() + "%").PR(12) +
                                               "Team Zero:   " + team_zero.PR(5) + (Math.Round(Convert.ToDouble(team_zero) / total * 100, 2).ToString() + "%").PR(12)+
                                               "Team Wrong:   " + team_w.PR(5) + (Math.Round(Convert.ToDouble(team_w) / total * 100, 2).ToString() + "%").PR(12));
                this.txt_result.Text = sb.ToString();
                Application.DoEvents();
            }
            sb.AppendLine("-----------------------------------------------------------------------------------------------------------------------------------");
        }
        public void check_match_odd_count()
        {
        
            string sql = " select start_time,host,client  "+
                         " from (select distinct website,start_time,host,client from europe_100 where  start_time>'{0}') a"+
                         " group by start_time,host,client"+
                         " having count(*)>1";
            sql=string.Format(sql,DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt_match = SQLServerHelper.get_table(sql); 
            foreach (DataRow row_match in dt_match.Rows)
            {
                string start_time = row_match["start_time"].ToString();
                string host = row_match["host"].ToString();
                string client = row_match["client"].ToString();

                sql = " select distinct website from europe_100 where start_time='{0}' and host='{1}' and client='{2}'";
                sql=string.Format(sql,start_time,host,client);
                DataTable dt_website=SQLServerHelper.get_table(sql);
                for(int i=0;i<dt_website.Rows.Count;i++)
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
            sb.AppendLine("-----------------------------------------------------------------------------------------------------------------------------------");
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        } 

    }
}
