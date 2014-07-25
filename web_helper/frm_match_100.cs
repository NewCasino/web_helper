using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.OleDb;
using System.Threading;
using mshtml;
using System.Reflection;

namespace web_helper
{
    public partial class frm_match_100 : Form
    {
        StringBuilder sb = new StringBuilder();
        List<IE> ies = new List<IE>();
        DataTable dt = new DataTable();

        public frm_match_100()
        {
            InitializeComponent();
        }
        private void frm_match_100_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                IE ie = new IE();
                ie.browser = new WebBrowser();
                ie.browser.Name = i.ToString();
                ie.browser.Width = 1200;
                ie.browser.Height = 600;
                ie.is_use = false;
                ie.index = i;
                ie.browser.ScriptErrorsSuppressed = true;
                ie.browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
                ies.Add(ie);
            }
            bind_data();
        }
        private void btn_load_Click(object sender, EventArgs e)
        {
            bind_data();
        }
        public void bind_data()
        {
            dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("site_name");
            dt.Columns.Add("step");
            dt.Columns.Add("method");
            dt.Columns.Add("select_type");
            dt.Columns.Add("url");
            dt.Columns.Add("seconds");
            dt.Columns.Add("state");
            dt.Columns.Add("start_time");
            dt.Columns.Add("end_time");
            dt.Columns.Add("browser");

            string sql = "select * from company_url where is_use='y' order by site_name,step";
            DataTable dt_temp = SQLServerHelper.get_table(sql);
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["id"] = i.ToString();
                row_new["site_name"] = dt_temp.Rows[i]["site_name"].ToString();
                row_new["url"] = dt_temp.Rows[i]["url"].ToString();
                row_new["step"] = dt_temp.Rows[i]["step"].ToString();
                row_new["method"] = dt_temp.Rows[i]["method"].ToString();
                row_new["select_type"] = dt_temp.Rows[i]["select_type"].ToString();
                row_new["seconds"] = dt_temp.Rows[i]["seconds"].ToString();
                row_new["state"] = "wait";
                dt.Rows.Add(row_new);
            }
            this.dgv_result.DataSource = dt;

        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }
        private void dgv_result_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dgv_result.Columns["url"].Width = 150; 
            this.dgv_result.Columns["start_time"].Width = 150;
            this.dgv_result.Columns["end_time"].Width = 150;
            this.dgv_result.Columns["id"].Width = 50;
            this.dgv_result.Columns["site_name"].Width = 80;
            this.dgv_result.Columns["step"].Width = 50;
            this.dgv_result.Columns["method"].Width = 80;
            this.dgv_result.Columns["select_type"].Width = 80;
            this.dgv_result.Columns["seconds"].Width = 50;
            this.dgv_result.Columns["state"].Width = 80;
            this.dgv_result.Columns["browser"].Width = 80;
        }

        private void btn_analyse_Click(object sender, EventArgs e)
        {
            analyse();
        }
        private void bn_start_Click(object sender, EventArgs e)
        {
            time.Start();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            time.Stop();
        }
        private void time_Tick(object sender, EventArgs e)
        {
            this.lb_time.Text = DateTime.Now.ToString("HH:mm:ss");
            analyse();
        }
        public void analyse()
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["state"].ToString() == "doing")
                {
                    DateTime start = Convert.ToDateTime(row["start_time"].ToString());
                    TimeSpan span = DateTime.Now - start;
                    if (span.TotalSeconds > 120)
                    {
                        row["state"] = "abort";
                        ies[Convert.ToInt32(row["browser"].ToString())].is_use = false;
                    }
                }
            }
            //browser top:browser index   height:0-can use 1-in use   width:row id
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["state"].ToString() == "wait" && dt.Rows[i]["select_type"].ToString().Trim() == "load")
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (ies[j].is_use == false)
                        {
                            dt.Rows[i]["state"] = "doing";
                            dt.Rows[i]["start_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            dt.Rows[i]["browser"] = j.ToString();
                            ies[j].is_use = true;
                            ies[j].row_id = i;
                            ies[j].browser.Navigate(dt.Rows[i]["url"].ToString());
                            break;
                        }
                    }
                }
                if (dt.Rows[i]["state"].ToString() == "wait" && dt.Rows[i]["select_type"].ToString().Trim() == "time")
                {
                    if (dt.Rows[i - 1]["state"].ToString() == "ok")
                    {
                        DateTime start_time = Convert.ToDateTime(dt.Rows[i - 1]["end_time"].ToString());
                        TimeSpan span = DateTime.Now - start_time;
                        if (span.TotalSeconds >= Convert.ToInt32(dt.Rows[i]["seconds"].ToString()))
                        {
                            int index = Convert.ToInt32(dt.Rows[i - 1]["browser"].ToString());
                            dt.Rows[i]["state"] = "doing";
                            dt.Rows[i]["start_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            dt.Rows[i]["browser"] = index.ToString();
                            ies[index].is_use = true;
                            ies[index].row_id = i;
                            select_method_from_site(ies[index].browser, i);
                        }
                    }
                }
            }
            Application.DoEvents();
        }
        public void insert_data(string company, string type, string start_time, string host, string client, string odd_win, string odd_draw, string odd_lose)
        {
            string sql = "";
            string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            sql = " insert into europe_100_log " +
               " (start_time,host,client,company,timespan," +
               "  profit_win,profit_draw,profit_lose,persent_win,persent_draw,persent_lose,persent_return,kelly_win,kelly_draw,kelly_lose," +
               "  start_profit_win,start_profit_draw,start_profit_lose,start_persent_win,start_persent_draw,start_persent_lose," +
               "  start_persent_return,start_kelly_win,start_kelly_draw,start_kelly_lose,type)" +
               "  values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}'," +
               "          '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}')";
            sql = string.Format(sql, start_time, host, client, company, timespan,
                             odd_win, odd_draw, odd_lose, "", "", "", "", "", "", "",
                             "", "", "", "", "", "",
                             "", "", "", "", type);
            SQLServerHelper.exe_sql(sql);
        }
        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (e.Url != browser.Document.Url) return;
            if (browser.ReadyState != WebBrowserReadyState.Complete) return;

            int index = Convert.ToInt32(browser.Name);
            int row_id = ies[index].row_id;

            string site_name = dt.Rows[row_id]["site_name"].ToString();
            string method = dt.Rows[row_id]["method"].ToString();
            select_method_from_site(browser, row_id);
        }

        public void select_method_from_site(WebBrowser browser, int row_id)
        {
            int index = Convert.ToInt32(browser.Name);
            string method = dt.Rows[row_id]["method"].ToString();

            Type reflect_type = Type.GetType("Match100Method");
            object reflect_acvtive = Activator.CreateInstance(reflect_type, null);

            MethodInfo method_info = reflect_type.GetMethod(method);
            string result = (string)method_info.Invoke(reflect_acvtive, new object[] { browser });


            dt.Rows[row_id]["state"] = "ok";
            dt.Rows[row_id]["end_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sb.AppendLine("----------------------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(browser.Url.ToString());
            sb.AppendLine("start_time:" + dt.Rows[row_id]["start_time"].PR(20));
            sb.AppendLine("end_time:" + dt.Rows[row_id]["end_time"].PR(20));
            //sb.AppendLine("doc length:" + browser.Document.Body.InnerHtml.Length.ToString());
            sb.AppendLine("result:");
            sb.AppendLine(result);
            sb.AppendLine("----------------------------------------------------------------------------------------------------------------------------------------------------------");
            this.txt_result.Text = sb.ToString();

            //检查是否还有后续动作
            if (row_id < dt.Rows.Count - 1)
            {
                if (dt.Rows[row_id]["site_name"].ToString() == dt.Rows[row_id + 1]["site_name"].ToString() && dt.Rows[row_id + 1]["state"].ToString() == "wait")
                {
                    Application.DoEvents();
                    return;
                }
            }

            ies[index].is_use = false;
            Application.DoEvents();

        }
        public void get_absolute(ref IHTMLElement element, ref int left, ref int top)
        {
            if (element.parentElement != null)
            {
                left = left + element.parentElement.offsetLeft;
                top = top + element.parentElement.offsetTop;
                IHTMLElement father_element = element.parentElement;
                get_absolute(ref father_element, ref left, ref top);
            }
        }


    }

    public class IE
    {
        public WebBrowser browser;
        public int row_id;
        public int index;
        public bool is_use = false;

    }
}
