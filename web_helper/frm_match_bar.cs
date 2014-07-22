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

        private void btn_compute_by_company_Click(object sender, EventArgs e)
        {
            frm_match_compute_by_company frm = new frm_match_compute_by_company();
            frm.Show();
        }

        private void btn_down_excel_Click(object sender, EventArgs e)
        {
            frm_match_down_excel frm = new frm_match_down_excel();
            frm.Show();
        }

        private void frm_match_company_info_Click(object sender, EventArgs e)
        {
            frm_match_company_info frm = new frm_match_company_info();
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
            frm_match_100 frm = new frm_match_100();
            frm.Show();
        }

        private void btn_compute_method_Click(object sender, EventArgs e)
        {
            frm_match_compute_method_analyse frm = new frm_match_compute_method_analyse();
            frm.Show();
        }
    }
}
