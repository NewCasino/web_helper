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
    public partial class frm_match_100_analyse : Form
    {
        DataTable dt_all = new DataTable();
        public frm_match_100_analyse()
        {
            InitializeComponent();
        }

        private void frm_match_compute_by_website_Load(object sender, EventArgs e)
        {  
        } 
        private void btn_load_Click(object sender, EventArgs e)
        {
            bind_data();
        } 
        private void btn_single_match_Click(object sender, EventArgs e)
        {
            ArrayList list_websites = new ArrayList();
            foreach (DataGridViewRow row in dgv_website.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string website = row.Cells["website"].Value.ToString();
                    list_websites.Add(website);
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

                    BsonDocument doc = Match100Analyse.get_max_from_single_match(start_time, host, client, 50, list_websites);
                    list.Add(doc);


                    sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    sb.Append(Match100Analyse.get_info_from_doc(doc));
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();
                }
            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

            if (cb_persent_asc.Checked) get_single_by_persent_asc(list);
            if (cb_persent_desc.Checked) get_single_by_persent_desc(list);
            if (cb_website_asc.Checked) get_single_by_website_asc(list);
            if (cb_website_desc.Checked) get_single_by_website_desc(list);
            if (cb_start_time_asc.Checked) get_single_by_start_time_asc(list);
            if (cb_start_time_desc.Checked) get_single_by_start_time_desc(list);


        }
        private void btn_two_match_Click(object sender, EventArgs e)
        {

            ArrayList list_websites = new ArrayList();
            foreach (DataGridViewRow row in dgv_website.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string website = row.Cells["website"].Value.ToString();
                    list_websites.Add(website);
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
                    BsonDocument doc = Match100Analyse.get_max_from_two_match(dt.Rows[i]["start_time"].ToString(),
                                                                              dt.Rows[i]["host"].ToString(),
                                                                              dt.Rows[i]["client"].ToString(),
                                                                              dt.Rows[j]["start_time"].ToString(),
                                                                              dt.Rows[j]["host"].ToString(),
                                                                              dt.Rows[j]["client"].ToString(),
                                                                              50,list_websites);
                    list.Add(doc);
                    sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    sb.Append(Match100Analyse.get_info_from_doc(doc));
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();
                }
            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

            if (cb_two_persent_asc.Checked) get_two_by_persent_asc(list);
            if (cb_two_persent_desc.Checked) get_two_by_persent_desc(list);
            if (cb_two_website_asc.Checked) get_two_by_website_asc(list);
            if (cb_two_website_desc.Checked) get_two_by_website_desc(list);
            if (cb_two_start_time_asc.Checked) get_two_by_start_time_asc(list);
            if (cb_two_start_time_desc.Checked) get_two_by_start_time_desc(list);
        }
        private void btn_three_match_Click(object sender, EventArgs e)
        {

            ArrayList list_websites = new ArrayList();
            foreach (DataGridViewRow row in dgv_website.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string website = row.Cells["website"].Value.ToString();
                    list_websites.Add(website);
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
                        BsonDocument doc = Match100Analyse.get_max_from_three_match(dt.Rows[i]["start_time"].ToString(),
                                                                                  dt.Rows[i]["host"].ToString(),
                                                                                  dt.Rows[i]["client"].ToString(),
                                                                                  dt.Rows[j]["start_time"].ToString(),
                                                                                  dt.Rows[j]["host"].ToString(),
                                                                                  dt.Rows[j]["client"].ToString(),
                                                                                  dt.Rows[k]["start_time"].ToString(),
                                                                                  dt.Rows[k]["host"].ToString(),
                                                                                  dt.Rows[k]["client"].ToString(),
                                                                                  50,list_websites);
                        list.Add(doc);
                        sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                        sb.Append(Match100Analyse.get_info_from_doc(doc));
                        this.txt_result.Text = sb.ToString();
                        Application.DoEvents();
                    }
                }
            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

            if (cb_three_persent_asc.Checked) get_three_by_persent_asc(list);
            if (cb_three_persent_desc.Checked) get_three_by_persent_desc(list);
            if (cb_three_website_asc.Checked) get_three_by_website_asc(list);
            if (cb_three_website_desc.Checked) get_three_by_website_desc(list);
        }

        private void btn_single_range_Click(object sender, EventArgs e)
        {

            ArrayList list_websites = new ArrayList();
            foreach (DataGridViewRow row in dgv_website.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string website = row.Cells["website"].Value.ToString();
                    list_websites.Add(website);
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
                        doc = Match100Analyse.get_max_from_single_match(start_time, host, client, i, list_websites);
                        list.Add(doc);

                        sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                        sb.Append(Match100Analyse.get_info_from_doc(doc));
                        this.txt_result.Text = sb.ToString();
                        Application.DoEvents();
                    }

                }
            }

            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        private void btn_two_range_Click(object sender, EventArgs e)
        {

            ArrayList list_websites = new ArrayList();
            foreach (DataGridViewRow row in dgv_website.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string website = row.Cells["website"].Value.ToString();
                    list_websites.Add(website);
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
                        BsonDocument doc = Match100Analyse.get_max_from_two_match(dt.Rows[i]["start_time"].ToString(),
                                                                                  dt.Rows[i]["host"].ToString(),
                                                                                  dt.Rows[i]["client"].ToString(),
                                                                                  dt.Rows[j]["start_time"].ToString(),
                                                                                  dt.Rows[j]["host"].ToString(),
                                                                                  dt.Rows[j]["client"].ToString(),
                                                                                  k,list_websites);
                        list.Add(doc);
                        sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                        sb.Append(Match100Analyse.get_info_from_doc(doc));
                        this.txt_result.Text = sb.ToString();
                        Application.DoEvents();
                    }
                }
            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        private void btn_three_range_Click(object sender, EventArgs e)
        {

            ArrayList list_websites = new ArrayList();
            foreach (DataGridViewRow row in dgv_website.Rows)
            {
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true)
                {
                    string website = row.Cells["website"].Value.ToString();
                    list_websites.Add(website);
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
                            BsonDocument doc = Match100Analyse.get_max_from_three_match(dt.Rows[i]["start_time"].ToString(),
                                                                                      dt.Rows[i]["host"].ToString(),
                                                                                      dt.Rows[i]["client"].ToString(),
                                                                                      dt.Rows[j]["start_time"].ToString(),
                                                                                      dt.Rows[j]["host"].ToString(),
                                                                                      dt.Rows[j]["client"].ToString(),
                                                                                      dt.Rows[k]["start_time"].ToString(),
                                                                                      dt.Rows[k]["host"].ToString(),
                                                                                      dt.Rows[k]["client"].ToString(),
                                                                                      l,list_websites);
                            list.Add(doc);
                            sb.Append("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                            sb.Append(Match100Analyse.get_info_from_doc(doc));
                            this.txt_result.Text = sb.ToString();
                            Application.DoEvents();
                        }
                    }
                }
            }
            sb.Append("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
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
        private void btn_website_all_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_website.Rows.Count - 1; i++)
            {
                dgv_website.Rows[i].Cells["selected"].Value = true;
            }
        }
        private void btn_website_reverse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_website.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(dgv_website.Rows[i].Cells["selected"].Value) == true)
                {
                    dgv_website.Rows[i].Cells["selected"].Value = false;
                }
                else
                {
                    dgv_website.Rows[i].Cells["selected"].Value = true;
                }
            }
        }
        private void dgv_match_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_match.Columns[0].Width = 50;
            dgv_match.Columns[1].Width = 120; 
            dgv_match.Columns[2].Width = 130;
            dgv_match.Columns[3].Width = 130;
        }
        private void dgv_website_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_website.Columns[0].Width = 50;
            dgv_website.Columns[1].Width = 200;
        } 

        public void bind_data()
        {

            string sql = "";
            sql = " select host,client,website,odd_win,odd_draw,odd_lose " +
                 " from europe_100" +
                 " where id in (select max(id) from europe_100 where start_time>'{0}' group by website,start_time,host,client)" +
                 " order by start_time,host,client,id";
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
            dt_match.Columns.Add("host");
            dt_match.Columns.Add("client");
            sql = "  select distinct start_time,host,client" +
                 "  from europe_100" +
                 "  where id in (select max(id) from europe_100 where start_time>'{0}' group by website,start_time,host,client)" +
                 "  order by start_time,host,client";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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



            DataTable dt_website = new DataTable();
            DataColumn col1 = new DataColumn();
            col1.DataType = Type.GetType("System.Boolean");
            col1.ColumnName = "selected";
            col1.DefaultValue = false;
            dt_website.Columns.Add(col1);
            dt_website.Columns.Add("website");
            sql = " select distinct  website" +
                  " from europe_100" +
                  " where id in (select max(id) from europe_100 where start_time>'{0}' group by website,start_time,host,client) ";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt_temp_website = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_temp_website.Rows)
            {
                DataRow row_new = dt_website.NewRow();
                row_new["website"] = row["website"].ToString();
                dt_website.Rows.Add(row_new);
            }
            this.dgv_website.DataSource = dt_website; 

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
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
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
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_single_by_website_asc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY website ASC:" + Environment.NewLine);

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
                BsonArray array = list[i]["websites"].AsBsonArray;
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
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_single_by_website_desc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY website DESC:" + Environment.NewLine);

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
                BsonArray array = list[i]["websites"].AsBsonArray;
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
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_single_by_start_time_asc(List<BsonDocument> list)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("BY START TIME ASC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("start_time");
            persent.DataType = Type.GetType("System.String");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                row_new["start_time"] = list[i]["start_time"].ToString();
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "start_time asc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_single_by_start_time_desc(List<BsonDocument> list)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("BY START TIME ASC:" + Environment.NewLine);

            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("start_time");
            persent.DataType = Type.GetType("System.String");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                row_new["start_time"] = list[i]["start_time"].ToString();
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "start_time desc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
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
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc)); 
            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
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
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_two_by_website_asc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY website ASC:" + Environment.NewLine);
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
                BsonArray array = list[i]["websites"].AsBsonArray;
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
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_two_by_website_desc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY website DESC:" + Environment.NewLine);

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
                BsonArray array = list[i]["websites"].AsBsonArray;
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
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_two_by_start_time_asc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY START TIME ASC:" + Environment.NewLine);
            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("start_time");
            persent.DataType = Type.GetType("System.String");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                DateTime time1 = Convert.ToDateTime(list[i]["start_time1"].ToString());
                DateTime time2 = Convert.ToDateTime(list[i]["start_time2"].ToString());
                TimeSpan  span= time1 - time2;
                row_new["start_time"] = span.Seconds > 0 ? list[i]["start_time2"].ToString() : list[i]["start_time1"].ToString(); 
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "start_time asc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));
            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_two_by_start_time_desc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY START TIME ASC:" + Environment.NewLine);
            DataTable dt = new DataTable();
            DataColumn order = new DataColumn("order");
            order.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(order);
            DataColumn persent = new DataColumn("start_time");
            persent.DataType = Type.GetType("System.String");
            dt.Columns.Add(persent);

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["order"] = i;
                DateTime time1 = Convert.ToDateTime(list[i]["start_time1"].ToString());
                DateTime time2 = Convert.ToDateTime(list[i]["start_time2"].ToString());
                TimeSpan span = time1 - time2;
                row_new["start_time"] = span.Seconds > 0 ? list[i]["start_time2"].ToString() : list[i]["start_time1"].ToString();
                dt.Rows.Add(row_new);
            }

            dt.DefaultView.Sort = "start_time desc";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
                sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));
            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
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
                sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
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
                sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_three_by_website_asc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY website ASC:" + Environment.NewLine);
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
                BsonArray array = list[i]["websites"].AsBsonArray;
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
                sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void get_three_by_website_desc(List<BsonDocument> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BY website DESC:" + Environment.NewLine);

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
                BsonArray array = list[i]["websites"].AsBsonArray;
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
                sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                sb.Append(Match100Analyse.get_info_from_doc(doc));

            }
            sb.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        } 
    }
}
