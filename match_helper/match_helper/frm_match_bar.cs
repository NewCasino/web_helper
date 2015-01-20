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

        private void button6_Click(object sender, EventArgs e)
        {
            frm_match_tool frm = new frm_match_tool();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frm_match_100_load_data_pinnaclesports frm = new frm_match_100_load_data_pinnaclesports();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frm_match_100_analyse_with_present frm = new frm_match_100_analyse_with_present();
            frm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frm_match_tool_for_two_site frm = new frm_match_tool_for_two_site();
            frm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            frm_match_100_analyse2 frm = new frm_match_100_analyse2();
            frm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frm_match_tool_range frm = new frm_match_tool_range();
            frm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frm_match_100_load_html frm = new frm_match_100_load_html();
            frm.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frm_match_100_pinnaclesports frm = new frm_match_100_pinnaclesports();
            frm.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frm_single_matchbook frm = new frm_single_matchbook();
            frm.Show();
        } 
    }
}
