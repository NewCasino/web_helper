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
using System.Data.OleDb;
using System.Threading;
using mshtml;
using System.Reflection;
using System.IO;

namespace web_helper
{
    public partial class frm_web_browser : Form
    {
        public frm_web_browser()
        {
            InitializeComponent();
        }

        StringBuilder sb = new StringBuilder();
        BsonDocument doc_result = new BsonDocument();
        private void frm_web_browser_Load(object sender, EventArgs e)
        {

            this.txt_url.Text = Environment.CurrentDirectory.Replace(@"bin\Debug", "").Replace(@"bin\x86\Debug", "") + @"data\test_web\test.htm";
            this.browser.ScriptErrorsSuppressed = true; 
        }
        private void btn_navigate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_url.Text))
            {
                browser.Navigate("http://wwww.baidu.com");
            }
            else
            {
                browser.Navigate(this.txt_url.Text); 
            } 
        }
        private void btn_analyse_Click(object sender, EventArgs e)
        {
            sb.Remove(0, sb.ToString().Length);
            if (browser.Document == null) return;

            BsonDocument doc_condition = BrowserHelper.get_doc_condition();
            if (cb_ajust.Checked == true) doc_condition["ajust"] = "y";

            DataTable dt_position = BrowserHelper.get_position_table4(ref browser,ref doc_condition);
            DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser,ref doc_condition); 
            DataTable dt_match = BrowserHelper.get_filter_table(ref doc_condition,dt_analyse);
            this.dgv_1.DataSource = dt_match;
            this.dgv_2.DataSource = dt_position;
            this.dgv_3.DataSource = dt_analyse;


            //List<BsonDocument> docs = BrowserHelper.get_all_elments(ref browser);
            //sb.AppendLine("TYPE".PR(15) + "ID".PR(15) + "offsetHeight".PR(15) + "offfsetTop".PR(15) + "clientTop".PR(15) + "scrollTop".PR(15) + "TEXT".PR(30));
            //sb.AppendLine("----------------------------------------------------------------------------------------------------");
            //foreach (BsonDocument doc in docs)
            //{
            //    sb.Append(doc["type"].PR(15));
            //    sb.Append(doc["id"].PR(15));
            //    sb.Append(doc["offsetHeight"].PR(15));
            //    sb.Append(doc["offsetTop"].PR(15));
            //    sb.Append(doc["clientTop"].PR(15));
            //    sb.Append(doc["scrollTop"].PR(15));
            //    sb.Append(doc["text"].ToString().PR(30));
            //    sb.Append(doc["html"].ToString());
            //    sb.Append(Environment.NewLine);
            //}
            //this.txt_result.Text = sb.ToString();
            MessageBox.Show("analyse ok!");
        }
        private void btn_script_Click(object sender, EventArgs e)
        { 
            if (browser.Document == null) return;

            //HtmlDocument doc_main = browser.Document;
            //BrowserHelper.invoke_click_by_id(ref browser, "btn_ok"); 
          

        }
        private void btn_method1_Click(object sender, EventArgs e)
        {
            Match100Method method = new Match100Method();
            BsonDocument doc_input = new BsonDocument();
            doc_result = method.from_gobetgo_1(ref browser, doc_input);
            sb.AppendLine(doc_result["data"].ToString());
            this.txt_result_method.Text = sb.ToString();
            MessageBox.Show("method ok!"); 
        }
        private void btn_method2_Click(object sender, EventArgs e)
        {
            if (doc_result == null)
            {
                MessageBox.Show("Empty  Input Document!");
            }
            Match100Method method = new Match100Method();
            BsonDocument doc_input = doc_result;
            BsonDocument doc_result = method.from_gobetgo_2(ref browser, doc_input);
            sb.AppendLine(doc_result["data"].ToString());
            this.txt_result_method.Text = sb.ToString();
            MessageBox.Show("method ok!"); 
        }

        private void btn_method3_Click(object sender, EventArgs e)
        {
            if (doc_result == null)
            {
                MessageBox.Show("Empty  Input Document!");
            }
            Match100Method method = new Match100Method();
            BsonDocument doc_input = doc_result;
            BsonDocument doc_result = method.from_gobetgo_3(ref browser, doc_input);
            sb.AppendLine(doc_result["data"].ToString());
            this.txt_result_method.Text = sb.ToString();
            MessageBox.Show("method ok!"); 
        }
        private void btn_method4_Click(object sender, EventArgs e)
        {
            if (doc_result == null)
            {
                MessageBox.Show("Empty  Input Document!");
            }
            Match100Method method = new Match100Method();
            BsonDocument doc_input = doc_result;
            BsonDocument doc_result = method.from_gobetgo_4(ref browser, doc_input);
            sb.AppendLine(doc_result["data"].ToString());
            this.txt_result_method.Text = sb.ToString();
            MessageBox.Show("method ok!"); 
        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

   

      
    }
}
