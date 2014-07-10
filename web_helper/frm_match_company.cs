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
        DataTable dt = new DataTable();
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
            dt = SQLServerHelper.get_table(sql);
            this.dgv_company.DataSource = dt;
        }

        public void bind_data()
        {
            DataTable dt_condition = new DataTable(); 

            DataColumn col = new DataColumn();
            col.DataType = Type.GetType("System.Boolean");
            col.ColumnName = "selected";
            col.DefaultValue = false;
            dt_condition.Columns.Add(col);
            dt_condition.Columns.Add("start_time");
            dt_condition.Columns.Add("host");
            dt_condition.Columns.Add("client");
            dt_condition.Columns.Add("three");
            dt_condition.Columns.Add("one");
            dt_condition.Columns.Add("zero");

        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        private void btn_compute_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //sb.Append(MatchCompany.get_min_from_single(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(),50));
            }

            this.txt_result.Text = sb.ToString(); 
        }

       
    }
}
