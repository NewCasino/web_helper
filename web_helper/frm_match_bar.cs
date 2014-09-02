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
    public partial class frm_match_bar : Form
    {
        public frm_match_bar()
        {
            InitializeComponent();
        }

        private void btn_compute_by_website_Click(object sender, EventArgs e)
        {
            frm_match_500_analyse frm = new frm_match_500_analyse();
            frm.Show();
        }

        private void btn_down_excel_Click(object sender, EventArgs e)
        {
            frm_match_500_down_excel frm = new frm_match_500_down_excel();
            frm.Show();
        }

        private void frm_match_website_info_Click(object sender, EventArgs e)
        {
            frm_match_website_info frm = new frm_match_website_info();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_match_compute_by_mix frm = new frm_match_compute_by_mix();
            frm.Show();
        }

        private void btn_html_analyse_Click(object sender, EventArgs e)
        {
            frm_html_analyse frm = new frm_html_analyse();
            frm.Show();
        }

        private void btn_match_100_Click(object sender, EventArgs e)
        {
            frm_match_100_load_data frm = new frm_match_100_load_data();
            frm.Show();
        }

        private void btn_compute_method_Click(object sender, EventArgs e)
        {
            frm_match_compute_method_analyse frm = new frm_match_compute_method_analyse();
            frm.Show();
        }

        private void btn_browser_Click(object sender, EventArgs e)
        {
            frm_web_browser frm = new frm_web_browser();
            frm.Show();
        }

        private void btn_500_read_info_Click(object sender, EventArgs e)
        {
            frm_match_100_team_analyse frm = new frm_match_100_team_analyse();
            frm.Show();
        }

        private void btn_log_analyse_Click(object sender, EventArgs e)
        {
            frm_match_100_log_analyse frm = new frm_match_100_log_analyse();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_match_100_analyse frm = new frm_match_100_analyse();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_match_100_check frm = new frm_match_100_check();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frm_match_100_method_test frm = new frm_match_100_method_test();
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frm_match_nowgoal_data frm = new frm_match_nowgoal_data();
            frm.Show();
        }
    }
}
