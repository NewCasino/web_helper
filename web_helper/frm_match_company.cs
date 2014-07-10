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
    public partial class frm_match_company : Form
    {
        public frm_match_company()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            string sql = "";
            sql = " select company,profit_win,profit_draw,profit_lose,"+
                  " persent_win,persent_draw,persent_lose,persent_return"+
                  " from europe_new where start_time='{0}' and host='{1}' and client='{2}'";
            sql = string.Format(sql, "2014-09-02 00:00:00", "A1", "A2");
            DataTable dt = SQLServerHelper.get_table(sql);
            this.dgv_company.DataSource = dt;
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        } 
      
    }
}
