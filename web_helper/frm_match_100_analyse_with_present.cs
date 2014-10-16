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
    public partial class frm_match_100_analyse_with_present : Form
    {
        DataTable dt_all = new DataTable();
        public frm_match_100_analyse_with_present()
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
        private void btn_compute_Click(object sender, EventArgs e)
        { 

            StringBuilder sb = new StringBuilder();
            List<BsonDocument> list = new List<BsonDocument>();
            DataTable dt = new DataTable();

            string id = this.txt_id.Text; ;
            double add = Convert.ToDouble(this.txt_add.Text);
            string a_start_time = "";
            string a_host = "";
            string a_client = "";

            dt.Columns.Add("start_time");
            dt.Columns.Add("host");
            dt.Columns.Add("client");
            foreach (DataGridViewRow row in dgv_match.Rows)
            {
                try
                {
                    if (row.Cells["id"].Value.ToString() == id)
                    {
                        a_start_time = row.Cells["start_time"].Value.ToString();
                        a_host = row.Cells["host"].Value.ToString();
                        a_client = row.Cells["client"].Value.ToString();
                    }
                }
                catch (Exception error) { }
                if (Convert.ToBoolean(row.Cells["selected"].Value) == true && row.Cells["id"].Value.ToString() != id)
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
                 
                    BsonDocument doc = Match100Analyse.get_max_from_two_match_with_present(a_start_time,
                                                                              a_host,
                                                                              a_client,
                                                                              dt.Rows[i]["start_time"].ToString(),
                                                                              dt.Rows[i]["host"].ToString(),
                                                                              dt.Rows[i]["client"].ToString(),
                                                                              50,add);
                    list.Add(doc);
                    sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    sb.Append(Match100Analyse.get_info_from_doc(doc));
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents(); 
            }
            sb.Append("----------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

            if (cb_two_persent_asc.Checked) get_two_by_persent_asc(list);
            if (cb_two_persent_desc.Checked) get_two_by_persent_desc(list);
            if (cb_two_start_time_asc.Checked) get_two_by_start_time_asc(list);
            if (cb_two_start_time_desc.Checked) get_two_by_start_time_desc(list);
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
        private void dgv_match_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_match.Columns["selected"].Width = 50;
            dgv_match.Columns["id"].Width = 100;
            dgv_match.Columns["start_time"].Width = 120;
            dgv_match.Columns["host"].Width = 130;
            dgv_match.Columns["client"].Width = 130;
            dgv_match.Columns["odd_win"].Width = 80;
            dgv_match.Columns["odd_draw"].Width = 80;
            dgv_match.Columns["odd_lose"].Width = 80;
        } 


        public void bind_data()
        {

            string sql = "";
            sql = " select  start_time,host,client,website,odd_win,odd_draw,odd_lose " +
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
            dt_match.Columns.Add("id");
            dt_match.Columns.Add("start_time"); 
            dt_match.Columns.Add("host");
            dt_match.Columns.Add("client");
            dt_match.Columns.Add("odd_win");
            dt_match.Columns.Add("odd_draw");
            dt_match.Columns.Add("odd_lose");
            sql = "  select distinct id,start_time,host,client,odd_win,odd_draw,odd_lose" +
                 "  from europe_100" +
                 "  where id in (select max(id) from europe_100 where start_time>'{0}' group by website,start_time,host,client)" +
                 "  and   website='500'"+
                 "  order by start_time,host,client";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt_temp_match = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_temp_match.Rows)
            {
                DataRow row_new = dt_match.NewRow();
                row_new["id"] = row["id"].ToString();
                row_new["start_time"] = row["start_time"].ToString(); 
                row_new["host"] = row["host"].ToString();
                row_new["client"] = row["client"].ToString();
                row_new["odd_win"] = row["odd_win"].ToString();
                row_new["odd_draw"] = row["odd_draw"].ToString(); 
                row_new["odd_lose"] = row["odd_lose"].ToString();

                dt_match.Rows.Add(row_new);
            }
            this.dgv_match.DataSource = dt_match;  
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

        
    }
}
  