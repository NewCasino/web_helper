using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;

namespace web_helper
{
    public partial class frm_match_company_info : Form
    {
        public frm_match_company_info()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        { 
            bind_data();
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            string sql = "";
            for (int i = 0; i < dgv_company.Rows.Count; i++)
            {
                string id = dgv_company.Rows[i].Cells[0].Value == null ? "" : dgv_company.Rows[i].Cells[0].Value.ToString();
                string info = dgv_company.Rows[i].Cells[1].Value == null ? "" : dgv_company.Rows[i].Cells[1].Value.ToString();


                if (string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(info))
                {
                    sql = " insert into doc_info (type,info) values ('company','{0}')";
                    sql = string.Format(sql, dgv_company.Rows[i].Cells[1].Value.ToString());
                    SQLServerHelper.exe_sql(sql);
                    continue;
                }
                if (!string.IsNullOrEmpty(id))
                {
                    sql = " update doc_info set info='{0}' where id={1}";
                    sql = string.Format(sql, info, id);
                    SQLServerHelper.exe_sql(sql);
                    continue;
                }
            }
        } 
        public void bind_data()
        {
            string info = "";
            if (string.IsNullOrEmpty(this.txt_condition.Text))
            {
                info = "%";
            }
            else
            {
                info = "%" + txt_condition.Text + "%";
            }

            string sql = "select id,info from doc_info where type='company'  and info like '{0}'";
            sql = string.Format(sql, info);

            DataTable dt = new DataTable();
            dt = SQLServerHelper.get_table(sql);
            this.dgv_company.DataSource = dt;
        }

        private void dgv_company_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dgv_company.Columns[1].Width = 200;
            this.dgv_company.Columns[0].ReadOnly = true;
        }
        private void dgv_company_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.lb_company_id.Text = dgv_company.Rows[e.RowIndex].Cells[0].Value == null ? "" : dgv_company.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.txt_result.Text = dgv_company.Rows[e.RowIndex].Cells[1].Value == null ? "" : dgv_company.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.lb_row_id.Text = e.RowIndex.ToString();
        } 
 
        private void btn_json_beautify_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = JsonBeautify.beautify(this.txt_result.Text);
        } 
        private void btn_check_json_Click(object sender, EventArgs e)
        {
            string msg = MongoHelper.check_is_update_string(this.txt_result.Text);
            if (msg == "right")
            {
                MessageBox.Show(" Bingo! Right JSON String!");
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private void btn_name_add_Click(object sender, EventArgs e)
        { 

            if (string.IsNullOrEmpty(this.txt_name.Text)) return;
            if (string.IsNullOrEmpty(lb_row_id.Text)) { MessageBox.Show("Select no row!!"); return; }

            string id = dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[0].Value == null ? "" : dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[0].Value.ToString();
            string info = dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value == null ? "" : dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value.ToString();

          

            BsonDocument doc;
            if (!string.IsNullOrEmpty(info))
            {
                doc = MongoHelper.get_doc_from_str(info);
            }
            else
            {
                doc = new BsonDocument();
                doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
                doc.Add("names", new BsonArray());
                doc.Add("urls", new BsonArray());
            }

            bool is_has = false;
            foreach (string value in doc["names"].AsBsonArray)
            {
                if (value == txt_name.Text) is_has = true;
            }
            if (is_has == false) doc["names"].AsBsonArray.Add(txt_name.Text);
            this.txt_result.Text = doc.ToString();
            dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value = doc.ToString();

        } 
        private void btn_url_add_Click(object sender, EventArgs e)
        { 
            if (string.IsNullOrEmpty(this.txt_name.Text)) return;
            if (string.IsNullOrEmpty(lb_row_id.Text)) { MessageBox.Show("Select no row!!"); return; }

            string id = dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[0].Value == null ? "" : dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[0].Value.ToString();
            string info = dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value == null ? "" : dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value.ToString();

            BsonDocument doc;
            if (!string.IsNullOrEmpty(info))
            {
                doc = MongoHelper.get_doc_from_str(dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value.ToString());
            }
            else
            {
                doc = new BsonDocument();
                doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
                doc.Add("names", new BsonArray());
                doc.Add("urls", new BsonArray());
            }
            BsonDocument doc_url = new BsonDocument();
            doc_url.Add("url", txt_url.Text);
            doc_url.Add("remark",txt_url_remark.Text);
            doc_url.Add("use", "1");
            doc_url.Add("create_time", DateTime.Now.ToString());
            doc["urls"].AsBsonArray.Add(doc_url.AsBsonValue);
            this.txt_result.Text = doc.ToString();
            dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value = doc.ToString(); 
        } 
        private void btn_update_grid_Click(object sender, EventArgs e)
        {
            dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value = this.txt_result.Text;
        } 
        private void btn_user_define_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cb_user_name.Text) || string.IsNullOrEmpty(this.txt_user_value.Text)) return;
            if (string.IsNullOrEmpty(lb_row_id.Text)) { MessageBox.Show("Select no row!!"); return; }

            string id = dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[0].Value == null ? "" : dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[0].Value.ToString();
            string info = dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value == null ? "" : dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value.ToString();

            string user_define_name = this.cb_user_name.Text;
            string user_define_value = this.txt_user_value.Text;

            BsonDocument doc;
            if (!string.IsNullOrEmpty(info))
            {
                doc = MongoHelper.get_doc_from_str(info);
            }
            else
            {
                doc = new BsonDocument();
                doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
                doc.Add("names", new BsonArray());
                doc.Add("urls", new BsonArray());
            }

            bool is_has = false;
            foreach (string item in doc.Names)
            {
                if (item == user_define_name) is_has = true;
            }

            if (is_has == false)
            {
                doc.Add(user_define_name, user_define_value);
            }
            else
            {
                doc[user_define_name] = user_define_value;
            }

            this.txt_result.Text = doc.ToString();
            dgv_company.Rows[Convert.ToInt16(lb_row_id.Text)].Cells[1].Value = doc.ToString();
        }

 

 
   
    }
}
