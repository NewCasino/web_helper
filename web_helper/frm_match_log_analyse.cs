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
    public partial class frm_match_log_analyse : Form
    {
        public frm_match_log_analyse()
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
            StringBuilder sb = new StringBuilder();
            string sql = "select * from europe_100_log";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {



                string start_time = row["start_time"].ToString();
                string host = row["host"].ToString();
                string client = row["client"].ToString();
                string win = row["profit_win"].ToString();
                string draw = row["profit_draw"].ToString();
                string lose = row["profit_lose"].ToString();

                string convert_host = "";
                string convert_client = "";
                string convert_time = "";
                try
                {
                    convert_host = Match100Helper.convert_team_name(host);
                    convert_client = Match100Helper.convert_team_name(client);
                    convert_time = Match100Helper.convert_start_time(start_time);

                }
                catch (Exception error) { }

                sb.AppendLine(start_time.PR(20) + convert_time.PR(20) + host.PR(30) + convert_host.PR(30) + client.PR(30) + convert_client.PR(30));
                this.txt_result.Text = sb.ToString();
                Application.DoEvents();

            }
        }
    }
}
