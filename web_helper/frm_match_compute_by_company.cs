using System;
using System.Collections;
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
    public partial class frm_match_compute_by_company : Form
    {
        DataTable dt_all = new DataTable();
        public frm_match_compute_by_company()
        {
            InitializeComponent();
        }

        private void frm_match_compute_by_company_Load(object sender, EventArgs e)
        {  
        } 
        private void btn_load_Click(object sender, EventArgs e)
        {
            bind_data();
        } 
        private void btn_single_match_Click(object sender, EventArgs e)
        {
            ArrayList list_companys = new ArrayList();
            foreach (DataGridViewRow row in dgv_company.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string company = row.Cells["company"].Value.ToString();
                    list_companys.Add(company);
                }
            }

            StringBuilder sb = new StringBuilder();
            List<BsonDocument> list = new List<BsonDocument>();
            foreach (DataGridViewRow row in dgv_match.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string start_time = row.Cells["start_time"].Value.ToString();
                    string host = row.Cells["host"].Value.ToString();
                    string client = row.Cells["client"].Value.ToString();

                    BsonDocument doc = MatchCompany.get_max_from_single_match(start_time, host, client, 50, list_companys);
                    list.Add(doc);


                    sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    sb.Append(MatchCompany.get_info_from_doc(doc));
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();
                }
            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

            if (cb_persent_asc.Checked) get_single_by_persent_asc(list);
            if (cb_persent_desc.Checked) get_single_by_persent_desc(list);
            if (cb_company_asc.Checked) get_single_by_company_asc(list);
            if (cb_company_desc.Checked) get_single_by_company_desc(list);


        }
        private void btn_two_match_Click(object sender, EventArgs e)
        {

            ArrayList list_companys = new ArrayList();
            foreach (DataGridViewRow row in dgv_company.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string company = row.Cells["company"].Value.ToString();
                    list_companys.Add(company);
                }
            }


            StringBuilder sb = new StringBuilder();
            List<BsonDocument> list = new List<BsonDocument>();
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
                                                                              50,list_companys);
                    list.Add(doc);
                    sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    sb.Append(MatchCompany.get_info_from_doc(doc));
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();
                }
            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

            if (cb_two_persent_asc.Checked) get_two_by_persent_asc(list);
            if (cb_two_persent_desc.Checked) get_two_by_persent_desc(list);
            if (cb_two_company_asc.Checked) get_two_by_company_asc(list);
            if (cb_two_company_desc.Checked) get_two_by_company_desc(list);
        }
        private void btn_three_match_Click(object sender, EventArgs e)
        {

            ArrayList list_companys = new ArrayList();
            foreach (DataGridViewRow row in dgv_company.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string company = row.Cells["company"].Value.ToString();
                    list_companys.Add(company);
                }
            }


            StringBuilder sb = new StringBuilder();
            List<BsonDocument> list = new List<BsonDocument>();
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
                    for (int k = j + 1; k < dt.Rows.Count; k++)
                    {
                        BsonDocument doc = MatchCompany.get_max_from_three_match(dt.Rows[i]["start_time"].ToString(),
                                                                                  dt.Rows[i]["host"].ToString(),
                                                                                  dt.Rows[i]["client"].ToString(),
                                                                                  dt.Rows[j]["start_time"].ToString(),
                                                                                  dt.Rows[j]["host"].ToString(),
                                                                                  dt.Rows[j]["client"].ToString(),
                                                                                  dt.Rows[k]["start_time"].ToString(),
                                                                                  dt.Rows[k]["host"].ToString(),
                                                                                  dt.Rows[k]["client"].ToString(),
                                                                                  50,list_companys);
                        list.Add(doc);
                        sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                        sb.Append(MatchCompany.get_info_from_doc(doc));
                        this.txt_result.Text = sb.ToString();
                        Application.DoEvents();
                    }
                }
            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

            if (cb_three_persent_asc.Checked) get_three_by_persent_asc(list);
            if (cb_three_persent_desc.Checked) get_three_by_persent_desc(list);
            if (cb_three_company_asc.Checked) get_three_by_company_asc(list);
            if (cb_three_company_desc.Checked) get_three_by_company_desc(list);
        }

        private void btn_single_range_Click(object sender, EventArgs e)
        {

            ArrayList list_companys = new ArrayList();
            foreach (DataGridViewRow row in dgv_company.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string company = row.Cells["company"].Value.ToString();
                    list_companys.Add(company);
                }
            }

            StringBuilder sb = new StringBuilder();
            List<BsonDocument> list = new List<BsonDocument>();
            foreach (DataGridViewRow row in dgv_match.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string start_time = row.Cells["start_time"].Value.ToString();
                    string host = row.Cells["host"].Value.ToString();
                    string client = row.Cells["client"].Value.ToString();

                    BsonDocument doc = new BsonDocument();
                    for (int i = 1; i < 101; i++)
                    {
                        doc = MatchCompany.get_max_from_single_match(start_time, host, client, i, list_companys);
                        list.Add(doc);

                        sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                        sb.Append(MatchCompany.get_info_from_doc(doc));
                        this.txt_result.Text = sb.ToString();
                        Application.DoEvents();
                    }

                }
            }

            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        private void btn_two_range_Click(object sender, EventArgs e)
        {

            ArrayList list_companys = new ArrayList();
            foreach (DataGridViewRow row in dgv_company.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string company = row.Cells["company"].Value.ToString();
                    list_companys.Add(company);
                }
            }


            StringBuilder sb = new StringBuilder();
            List<BsonDocument> list = new List<BsonDocument>();
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
                    for (int k = 1; k < 101; k++)
                    {
                        BsonDocument doc = MatchCompany.get_max_from_two_match(dt.Rows[i]["start_time"].ToString(),
                                                                                  dt.Rows[i]["host"].ToString(),
                                                                                  dt.Rows[i]["client"].ToString(),
                                                                                  dt.Rows[j]["start_time"].ToString(),
                                                                                  dt.Rows[j]["host"].ToString(),
                                                                                  dt.Rows[j]["client"].ToString(),
                                                                                  k,list_companys);
                        list.Add(doc);
                        sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                        sb.Append(MatchCompany.get_info_from_doc(doc));
                        this.txt_result.Text = sb.ToString();
                        Application.DoEvents();
                    }
                }
            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        private void btn_three_range_Click(object sender, EventArgs e)
        {

            ArrayList list_companys = new ArrayList();
            foreach (DataGridViewRow row in dgv_company.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string company = row.Cells["company"].Value.ToString();
                    list_companys.Add(company);
                }
            } 
            StringBuilder sb = new StringBuilder();
            List<BsonDocument> list = new List<BsonDocument>();
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
                    for (int k = j + 1; k < dt.Rows.Count; k++)
                    {
                        for (int l = 1; l < 101; l++)
                        {
                            BsonDocument doc = MatchCompany.get_max_from_three_match(dt.Rows[i]["start_time"].ToString(),
                                                                                      dt.Rows[i]["host"].ToString(),
                                                                                      dt.Rows[i]["client"].ToString(),
                                                                                      dt.Rows[j]["start_time"].ToString(),
                                                                                      dt.Rows[j]["host"].ToString(),
                                                                                      dt.Rows[j]["client"].ToString(),
                                                                                      dt.Rows[k]["start_time"].ToString(),
                                                                                      dt.Rows[k]["host"].ToString(),
                                                                                      dt.Rows[k]["client"].ToString(),
                                                                                      l,list_companys);
                            list.Add(doc);
                            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                            sb.Append(MatchCompany.get_info_from_doc(doc));
                            this.txt_result.Text = sb.ToString();
                            Application.DoEvents();
                        }
                    }
                }
            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }
        private void btn_match_all_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dgv_match.Rows.Count - 1; i++)
            {
                dgv_match.Rows[i].Cells["selected"].Value = true;
            }
        }
        private void btn_match_reverse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_match.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(dgv_match.Rows[i].Cells["selected"].Value) == true)
                {
                    dgv_match.Rows[i].Cells["selected"].Value = false;
                }
                else
                {
                    dgv_match.Rows[i].Cells["selected"].Value = true;
                }
            }
        }
        private void btn_company_all_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_company.Rows.Count - 1; i++)
            {
                dgv_company.Rows[i].Cells["selected"].Value = true;
            }
        }
        private void btn_company_reverse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_company.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(dgv_company.Rows[i].Cells["selected"].Value) == true)
                {
                    dgv_company.Rows[i].Cells["selected"].Value = false;
                }
                else
                {
                    dgv_company.Rows[i].Cells["selected"].Value = true;
                }
            }
        }
        private void dgv_match_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_match.Columns[0].Width = 50;
            dgv_match.Columns[1].Width = 140;
            dgv_match.Columns[2].Width = 80;
            dgv_match.Columns[3].Width = 80;
            dgv_match.Columns[4].Width = 80;
        }
        private void dgv_company_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_company.Columns[0].Width = 50;
            dgv_company.Columns[1].Width = 250;
        }

        public void bind_data()
        {

            string sql = "";
            sql = " select host,client,company,profit_win,profit_draw,profit_lose," +
                  " persent_win,persent_draw,persent_lose,persent_return" +
                  " from europe_500  where start_time>'{0}' order by start_time,host,client,id ";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            dt_all = SQLServerHelper.get_table(sql);
            this.dgv_all.DataSource = dt_all;


            DataTable dt_match = new DataTable();
            DataColumn col = new DataColumn();
            col.DataType = Type.GetType("System.Boolean");
            col.ColumnName = "selected";
            col.DefaultValue = false;
            dt_match.Columns.Add(col);
            dt_match.Columns.Add("start_time");
            dt_match.Columns.Add("type");
            dt_match.Columns.Add("host");
            dt_match.Columns.Add("client");
            sql = " select distinct start_time,type,host,client" +
                 " from europe_500 where start_time>'{0}'  order by start_time,host,client";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt_temp_match = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_temp_match.Rows)
            {
                DataRow row_new = dt_match.NewRow();
                row_new["start_time"] = row["start_time"].ToString();
                row_new["type"] = row["type"].ToString();
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
                 " from europe_500 where start_time>'{0}'";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt_temp_company = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_temp_company.Rows)
            {
                DataRow row_new = dt_company.NewRow();
                row_new["company"] = row["company"].ToString();
                dt_company.Rows.Add(row_new);
            }
            this.dgv_company.DataSource = dt_company; 

        }
        public void get_single_by_persent_asc(List<BsonDocument> list)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("BY PERSENT ASC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("persent");
            persent.DataType = Type.GetType("System.Double");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                row_new["persent"] = Convert.ToDouble(list[i]["min_value"].ToString()) / Convert.ToDouble(list[i]["bid_count"].ToString()) * 100;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "persent asc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_single_by_persent_desc(List<BsonDocument> list)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("BY PERSENT DESC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("persent");
            persent.DataType = Type.GetType("System.Double");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                row_new["persent"] = Convert.ToDouble(list[i]["min_value"].ToString()) / Convert.ToDouble(list[i]["bid_count"].ToString()) * 100;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "persent desc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_single_by_company_asc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY COMPANY ASC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn count = new DataColumn("count");
            count.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(count);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                BsonArray array = list[i]["companys"].AsBsonArray;
                ArrayList al = new ArrayList();
                foreach (BsonValue value in array)
                {
                    bool is_has = false;
                    foreach (string name in al)
                    {
                        if (name == value.ToString()) is_has = true;
                    }
                    if (is_has == false) al.Add(value.ToString());
                }
                row_new["count"] = al.Count;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "count asc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_single_by_company_desc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY COMPANY DESC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn count = new DataColumn("count");
            count.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(count);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                BsonArray array = list[i]["companys"].AsBsonArray;
                ArrayList al = new ArrayList();
                foreach (BsonValue value in array)
                {
                    bool is_has = false;
                    foreach (string name in al)
                    {
                        if (name == value.ToString()) is_has = true;
                    }
                    if (is_has == false) al.Add(value.ToString());
                }
                row_new["count"] = al.Count;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "count desc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_two_by_persent_asc(List<BsonDocument> list)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("BY PERSENT ASC:" + Environment.NewLine);
            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("persent");
            persent.DataType = Type.GetType("System.Double");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                row_new["persent"] = Convert.ToDouble(list[i]["min_value"].ToString()) / Convert.ToDouble(list[i]["bid_count"].ToString()) * 100;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "persent asc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_two_by_persent_desc(List<BsonDocument> list)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("BY PERSENT DESC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("persent");
            persent.DataType = Type.GetType("System.Double");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                row_new["persent"] = Convert.ToDouble(list[i]["min_value"].ToString()) / Convert.ToDouble(list[i]["bid_count"].ToString()) * 100;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "persent desc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_two_by_company_asc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY COMPANY ASC:" + Environment.NewLine);
            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn count = new DataColumn("count");
            count.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(count);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                BsonArray array = list[i]["companys"].AsBsonArray;
                ArrayList al = new ArrayList();
                foreach (BsonValue value in array)
                {
                    bool is_has = false;
                    foreach (string name in al)
                    {
                        if (name == value.ToString()) is_has = true;
                    }
                    if (is_has == false) al.Add(value.ToString());
                }
                row_new["count"] = al.Count;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "count asc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_two_by_company_desc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY COMPANY DESC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn count = new DataColumn("count");
            count.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(count);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                BsonArray array = list[i]["companys"].AsBsonArray;
                ArrayList al = new ArrayList();
                foreach (BsonValue value in array)
                {
                    bool is_has = false;
                    foreach (string name in al)
                    {
                        if (name == value.ToString()) is_has = true;
                    }
                    if (is_has == false) al.Add(value.ToString());
                }
                row_new["count"] = al.Count;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "count desc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_three_by_persent_asc(List<BsonDocument> list)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("BY PERSENT ASC:" + Environment.NewLine);
            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("persent");
            persent.DataType = Type.GetType("System.Double");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                row_new["persent"] = Convert.ToDouble(list[i]["min_value"].ToString()) / Convert.ToDouble(list[i]["bid_count"].ToString()) * 100;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "persent asc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_three_by_persent_desc(List<BsonDocument> list)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("BY PERSENT DESC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("persent");
            persent.DataType = Type.GetType("System.Double");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                row_new["persent"] = Convert.ToDouble(list[i]["min_value"].ToString()) / Convert.ToDouble(list[i]["bid_count"].ToString()) * 100;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "persent desc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_three_by_company_asc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY COMPANY ASC:" + Environment.NewLine);
            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn count = new DataColumn("count");
            count.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(count);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                BsonArray array = list[i]["companys"].AsBsonArray;
                ArrayList al = new ArrayList();
                foreach (BsonValue value in array)
                {
                    bool is_has = false;
                    foreach (string name in al)
                    {
                        if (name == value.ToString()) is_has = true;
                    }
                    if (is_has == false) al.Add(value.ToString());
                }
                row_new["count"] = al.Count;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "count asc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_three_by_company_desc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY COMPANY DESC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn count = new DataColumn("count");
            count.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(count);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                BsonArray array = list[i]["companys"].AsBsonArray;
                ArrayList al = new ArrayList();
                foreach (BsonValue value in array)
                {
                    bool is_has = false;
                    foreach (string name in al)
                    {
                        if (name == value.ToString()) is_has = true;
                    }
                    if (is_has == false) al.Add(value.ToString());
                }
                row_new["count"] = al.Count;
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "count desc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(MatchCompany.get_info_from_doc(doc));

            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        } 
    }
}
