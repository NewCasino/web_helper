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
                string name = dgv_company.Rows[i].Cells[1].Value == null ? "" : dgv_company.Rows[i].Cells[1].Value.ToString();
                string url = dgv_company.Rows[i].Cells[1].Value == null ? "" : dgv_company.Rows[i].Cells[2].Value.ToString();
                string other_names = dgv_company.Rows[i].Cells[1].Value == null ? "" : dgv_company.Rows[i].Cells[3].Value.ToString();
                string other_urls = dgv_company.Rows[i].Cells[1].Value == null ? "" : dgv_company.Rows[i].Cells[4].Value.ToString();
                string pay_ways = dgv_company.Rows[i].Cells[1].Value == null ? "" : dgv_company.Rows[i].Cells[5].Value.ToString();
                string info = dgv_company.Rows[i].Cells[1].Value == null ? "" : dgv_company.Rows[i].Cells[6].Value.ToString();


                if (string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(info))
                {
                    sql = " insert into company (name,url,other_names,other_urls,pay_ways,info) values ('{0}','{1}','{2}','{3}','{4}','{5}')";
                    sql = string.Format(sql, name, url, other_names, other_urls, pay_ways, info);
                    SQLServerHelper.exe_sql(sql);
                    continue;
                }
                if (!string.IsNullOrEmpty(id))
                {
                    sql = " update company set name='{0}',url='{1}',other_names='{2}',other_urls='{3}',pay_ways='{4}',info='{5}'  where id={6}";
                    sql = string.Format(sql, name, url, other_names, other_urls, pay_ways, info, id);
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

            string sql = "select *  from company where  info like '{0}'";
            sql = string.Format(sql, info);

            DataTable dt = new DataTable();
            dt = SQLServerHelper.get_table(sql);
            this.dgv_company.DataSource = dt;
        }

        private void dgv_company_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //this.dgv_company.Columns[1].Width = 200;
            this.dgv_company.Columns[0].ReadOnly = true;
        }
        private void dgv_company_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.lb_company_id.Text = dgv_company.Rows[e.RowIndex].Cells["id"].Value == null ? "" : dgv_company.Rows[e.RowIndex].Cells["id"].Value.ToString();
            this.txt_result.Text = dgv_company.Rows[e.RowIndex].Cells["info"].Value == null ? "" : dgv_company.Rows[e.RowIndex].Cells["info"].Value.ToString();
            this.lb_row_id.Text = e.RowIndex.ToString();

            this.txt_name.Text = dgv_company.Rows[e.RowIndex].Cells["name"].Value == null ? "" : dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["name"].Value.ToString();
            this.txt_url.Text = dgv_company.Rows[e.RowIndex].Cells["url"].Value == null ? "" : dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["url"].Value.ToString();
            this.txt_other_names.Text = dgv_company.Rows[e.RowIndex].Cells["other_names"].Value == null ? "" : dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["other_names"].Value.ToString();
            this.txt_other_urls.Text = dgv_company.Rows[e.RowIndex].Cells["other_urls"].Value == null ? "" : dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["other_urls"].Value.ToString();
            this.txt_pay_ways.Text = dgv_company.Rows[e.RowIndex].Cells["pay_ways"].Value == null ? "" : dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["pay_ways"].Value.ToString();

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


        private void btn_update_grid_Click(object sender, EventArgs e)
        {
            dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["info"].Value = this.txt_result.Text;
        }


        private void btn_update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_name.Text) || string.IsNullOrEmpty(this.txt_url.Text)) return;

            string id = dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["id"].Value == null ? "" : dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["id"].Value.ToString();
            string info = dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["info"].Value == null ? "" : dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["info"].Value.ToString();

            BsonDocument doc;
            if (!string.IsNullOrEmpty(info))
            {
                doc = MongoHelper.get_doc_from_str(info);
                doc["name"] = this.txt_name.Text;
                doc["url"] = this.txt_url.Text;
                doc["other_names"] = get_array_from_str(this.txt_other_names.Text);
                doc["other_urls"] = get_array_from_str(this.txt_other_urls.Text);
                doc["pay_ways"] = get_array_from_str(this.txt_pay_ways.Text); ;

            }
            else
            {
                doc = new BsonDocument();
                doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
                doc.Add("name", this.txt_name.Text);
                doc.Add("url", this.txt_url.Text);
                doc.Add("other_names", get_array_from_str(this.txt_other_names.Text));
                doc.Add("other_urls", get_array_from_str(this.txt_other_urls.Text));
                doc.Add("pay_ways", get_array_from_str(this.txt_pay_ways.Text));
            }



            dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["name"].Value = this.txt_name.Text;
            dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["url"].Value = this.txt_url.Text;
            dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["other_names"].Value = this.txt_other_names.Text;
            dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["other_urls"].Value = this.txt_other_urls.Text;
            dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["pay_ways"].Value = this.txt_pay_ways.Text;
            dgv_company.Rows[Convert.ToInt32(lb_row_id.Text)].Cells["info"].Value = doc.ToString();



        }
        public BsonArray get_array_from_str(string str)
        {
            str = str.Replace("\r\n", "");
            BsonArray array = new BsonArray();
            if (!string.IsNullOrEmpty(str))
            {
                string[] list = str.Split('$');
                foreach (string item in list)
                {
                    array.Add(item);
                }
            }
            return array;
        }
    }
}
