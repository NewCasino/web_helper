using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace web_helper
{
    public partial class frm_match_company : Form
    {
        DataTable dt_all = new DataTable();
        public frm_match_company()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            bind_data();
        }
        public void bind_data()
        {

            string sql = "";
            sql = " select company,profit_win,profit_draw,profit_lose," +
                  " persent_win,persent_draw,persent_lose,persent_return" +
                  " from europe_new order by start_time,host,client,id";
            dt_all = SQLServerHelper.get_table(sql);
            this.dgv_all.DataSource = dt_all;


            DataTable dt_match = new DataTable();
            DataColumn col = new DataColumn();
            col.DataType = Type.GetType("System.Boolean");
            col.ColumnName = "selected";
            col.DefaultValue = false;
            dt_match.Columns.Add(col);
            dt_match.Columns.Add("start_time");
            dt_match.Columns.Add("host");
            dt_match.Columns.Add("client");
            sql = " select distinct start_time,host,client" +
                 " from europe_new order by start_time,host,client";
            DataTable dt_temp_match = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_temp_match.Rows)
            {
                DataRow row_new = dt_match.NewRow();
                row_new["start_time"] = row["start_time"].ToString();
                row_new["host"] = row["host"].ToString();
                row_new["client"] = row["client"].ToString();
                dt_match.Rows.Add(row_new);
            }
            this.dgv_match.DataSource = dt_match;



            DataTable dt_company = new DataTable();
            DataColumn col1 = new DataColumn();
            col1.DataType = Type.GetType("System.Boolean");
            col1.ColumnName = "selected";
            col1.DefaultValue = false;
            dt_company.Columns.Add(col1);
            dt_company.Columns.Add("company");
            sql = " select distinct  company" +
                 " from europe_new";
            DataTable dt_temp_company = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_temp_company.Rows)
            {
                DataRow row_new = dt_company.NewRow();
                row_new["company"] = row["company"].ToString();
                dt_company.Rows.Add(row_new);
            }
            this.dgv_company.DataSource = dt_company;






        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }


        private void btn_single_match_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in dgv_match.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string start_time = row.Cells["start_time"].Value.ToString();
                    string host = row.Cells["host"].Value.ToString();
                    string client = row.Cells["client"].Value.ToString();

                    BsonDocument doc = MatchCompany.get_max_from_single_match(start_time, host, client, 50);


                    sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    sb.Append(MatchCompany.get_info_from_doc(doc));
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();
                }
            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }

        private void btn_two_match_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            dt.Columns.Add("start_time");
            dt.Columns.Add("host");
            dt.Columns.Add("client");
            foreach (DataGridViewRow row in dgv_match.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    DataRow row_new = dt.NewRow();
                    row_new["start_time"] = row.Cells["start_time"].Value.ToString();
                    row_new["host"] = row.Cells["host"].Value.ToString();
                    row_new["client"] = row.Cells["client"].Value.ToString();
                    dt.Rows.Add(row_new);
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = i + 1; j < dt.Rows.Count; j++)
                {
                    BsonDocument doc = MatchCompany.get_max_from_two_match(dt.Rows[i]["start_time"].ToString(),
                                                                              dt.Rows[i]["host"].ToString(),
                                                                              dt.Rows[i]["client"].ToString(),
                                                                              dt.Rows[j]["start_time"].ToString(),
                                                                              dt.Rows[j]["host"].ToString(),
                                                                              dt.Rows[j]["client"].ToString(),
                                                                              50);


                    sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    sb.Append(MatchCompany.get_info_from_doc(doc));
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();
                }
            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }


    }
}
