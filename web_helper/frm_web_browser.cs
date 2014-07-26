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

namespace web_helper
{
    public partial class frm_web_browser : Form
    {
        public frm_web_browser()
        {
            InitializeComponent();
        }

        StringBuilder sb = new StringBuilder();
        private void frm_web_browser_Load(object sender, EventArgs e)
        {
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
            this.txt_result.Text = sb.ToString();
        }
        private void btn_analyse_Click(object sender, EventArgs e)
        {
            sb.Remove(0, sb.ToString().Length);
            if (browser.Document == null) return;

            DataTable dt_postion = BrowserHelper.get_postion_table(ref browser);
            DataTable dt_analyse = BrowserHelper.get_analyse_table(ref browser);
            this.dgv_result.DataSource = dt_analyse;


            List<BsonDocument> docs = BrowserHelper.get_all_elments(ref browser);
            sb.AppendLine("TYPE".PR(15) + "ID".PR(15) + "NAME".PR(15) + "CLASS".PR(15) + "TEXT");
            sb.AppendLine("----------------------------------------------------------------------------------------------------");
            foreach (BsonDocument doc in docs)
            {
                sb.Append(doc["type"].PR(15));
                sb.Append(doc["id"].PR(15));
                sb.Append(doc["name"].PR(15));
                sb.Append(doc["class"].PR(15));
                sb.Append(doc["text"].PR(100));
                sb.Append(Environment.NewLine);
            }
            this.txt_result.Text = sb.ToString();


        }
        private void btn_script_Click(object sender, EventArgs e)
        {
            if (browser.Document == null) return;

            HtmlDocument doc_main = browser.Document;
            sb.Append(BrowserHelper.get_text_by_id(ref browser, "txt_origin"));
            BrowserHelper.invoke_member_by_id(ref browser, "btn_ok", "click");

            this.txt_result.Text = sb.ToString();
        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

    }
}
