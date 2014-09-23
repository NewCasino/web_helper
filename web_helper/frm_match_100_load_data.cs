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
using System.IO;
using System.Collections;

namespace web_helper
{
    public partial class frm_match_100_load_data : Form
    {
        StringBuilder sb = new StringBuilder();
        List<IE> ies = new List<IE>();
        DataTable dt = new DataTable();

        public frm_match_100_load_data()
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
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }
        private void dgv_result_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dgv_result.Columns["url"].Width = 150;
            this.dgv_result.Columns["start_time"].Width = 120;
            this.dgv_result.Columns["end_time"].Width = 120;
            this.dgv_result.Columns["final_time"].Width = 120;
            this.dgv_result.Columns["id"].Width = 50;
            this.dgv_result.Columns["site_name"].Width = 80;
            this.dgv_result.Columns["step"].Width = 50;
            this.dgv_result.Columns["method"].Width = 80;
            this.dgv_result.Columns["select_type"].Width = 80;
            this.dgv_result.Columns["seconds"].Width = 50;
            this.dgv_result.Columns["state"].Width = 80;
            this.dgv_result.Columns["browser"].Width = 50;
            this.dgv_result.Columns["selected"].Width = 50;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            time.Start();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            time.Stop();
        }
        private void time_Tick(object sender, EventArgs e)
        {
            //每秒判断
            this.lb_time.Text = DateTime.Now.ToString("HH:mm:ss");
            analyse();

            //每分钟判断
            if (DateTime.Now.ToString("ss") == "01")
            {

            }
        }
        private void btn_all_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_result.Rows.Count - 1; i++)
            {
                dgv_result.Rows[i].Cells["selected"].Value = true;
            }
        }
        private void btn_reverse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_result.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(dgv_result.Rows[i].Cells["selected"].Value) == true)
                {
                    dgv_result.Rows[i].Cells["selected"].Value = false;
                }
                else
                {
                    dgv_result.Rows[i].Cells["selected"].Value = true;
                }
            }
        }


        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (e.Url != browser.Document.Url) return;
            if (browser.ReadyState != WebBrowserReadyState.Complete) return;

            int index = Convert.ToInt32(browser.Name);
            int row_id = ies[index].row_id;

            if (dt.Rows[row_id]["select_type"].ToString() == "load")
            {
                dt.Rows[row_id]["end_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BsonDocument doc_result = Match100Helper.get_doc_result();
                doc_result["data"] = "Load Complete!";
                doc_result["url"] = browser.Document.Url.ToString();
                ies[index].doc_result = doc_result;
            }
        }
        public void analyse()
        {
            //检查正在使用的IE是否执行完毕
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToBoolean(row["selected"].ToString()) == false) continue;
                if (row["state"].ToString() == "doing")
                {
                    DateTime start_time = Convert.ToDateTime(row["start_time"].ToString());
                    TimeSpan span = DateTime.Now - start_time;
                    int seconds = Convert.ToInt32(row["seconds"].ToString());
                    string site_name = row["site_name"].ToString();

                    if (span.TotalSeconds > seconds)
                    {
                        if (string.IsNullOrEmpty(row["end_time"].ToString()))
                        {
                            //等待10分钟
                            if (span.TotalSeconds > 600)
                            {
                                row["state"] = "abort";
                            }
                        }
                        else
                        {
                            row["state"] = "ok";
                            row["final_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            BsonDocument doc_result = ies[Convert.ToInt32(row["browser"].ToString())].doc_result;

                            sb.AppendLine("-----------------------------------------------------------------------------------------------------------------");
                            sb.AppendLine("web site:".PR(15) + row["site_name"].PR(10) + row["method"].ToString()); 
                            sb.AppendLine("time:".PR(15) + row["start_time"].ToString().Substring(11, 8).PR(10) + row["end_time"].ToString().Substring(11, 8).PR(10) + row["final_time"].ToString().Substring(11, 8).PR(10));
                            sb.AppendLine("url:".PR(15) + doc_result["url"].ToString());
                            sb.AppendLine("result data:".PR(15));
                            sb.AppendLine(doc_result["data"].ToString());
                            sb.AppendLine("-----------------------------------------------------------------------------------------------------------------");
                            this.txt_result.Text = sb.ToString();


                            //判断是否要循环使用此IE 
                            if (doc_result["loop"].AsBsonArray.Count > 0)
                            {
                                BsonArray loop = doc_result["loop"].AsBsonArray;
                                for (int i = 0; i < loop.Count; i++)
                                {
                                    for (int j = 0; j < dt.Rows.Count; j++)
                                    {
                                        if (dt.Rows[j]["site_name"].ToString() == row["site_name"].ToString() && dt.Rows[j]["step"].ToString() == loop[i].ToString())
                                        {
                                            dt.Rows[j]["start_time"] = "";
                                            dt.Rows[j]["state"] = "wait";
                                            dt.Rows[j]["end_time"] = "";
                                            dt.Rows[j]["final_time"] = "";
                                            dt.Rows[j]["browser"] = row["browser"].ToString();
                                        }
                                    }
                                }
                            }

                            //判断是否还有使用此IE
                            bool is_use = false;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["site_name"].ToString() == site_name &&
                                    Convert.ToInt16(dt.Rows[i]["step"].ToString()) == Convert.ToInt16(row["step"].ToString()) &&
                                    dt.Rows[i]["state"].ToString() == "wait" &&
                                    dt.Rows[i]["select_type"].ToString().Trim() == "method")
                                {
                                    is_use = true;
                                }
                            }
                            if (is_use == false)
                            {
                                ies[Convert.ToInt32(row["browser"].ToString())].is_use = false;
                            }
                        }
                    }
                }
            }

            //为等待的任务分配IE
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i]["selected"].ToString()) == false) continue;
                bool is_can_start = true;

                //同一个site_name只可以使用一个IE
                if (i != 0 && dt.Rows[i - 1]["site_name"].ToString() == dt.Rows[i]["site_name"].ToString() && dt.Rows[i - 1]["state"].ToString() != "ok")
                {
                    is_can_start = false;
                }
                if (dt.Rows[i]["state"].ToString() == "wait" && dt.Rows[i]["select_type"].ToString().Trim() == "load" && is_can_start == true)
                {
                    for (int j = 0; j < ies.Count; j++)
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
                if (dt.Rows[i]["state"].ToString() == "wait" && dt.Rows[i]["select_type"].ToString().Trim() == "method")
                {
                    if (dt.Rows[i - 1]["state"].ToString() == "ok")
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
            //Application.DoEvents();
        }
        public void analyse_reset()
        {
            ArrayList list_selected = new ArrayList();
            ArrayList list_abort = new ArrayList();
            ArrayList list_wait = new ArrayList();
            ArrayList list_ok = new ArrayList();
            ArrayList list_doing = new ArrayList();

            foreach (DataRow row in dt.Rows)
            {
              bool is_has=false;
              foreach(string item in list_selected)
              {
                  if (item == row["site_name"].ToString()) is_has = true;
              }
              if (is_has == false) list_selected.Add(row["site_name"].ToString()); 
            }

            //5 minutes later,restar browser
            foreach (string site_name in list_selected)
            {

                string state = "";
                foreach (DataRow row in dt.Rows)
                {
                    if (row["site_name"].ToString() == site_name)
                    {
                        state = row["state"].ToString();
                        if (state == "abort") break;
                    }
                }
                switch (state)
                {
                    case "abort":
                        list_abort.Add(site_name);
                        break;
                    case "wait":
                        list_wait.Add(site_name);
                        break;
                    case "doing":
                        list_doing.Add(site_name);
                        break;
                    case "ok":
                        list_ok.Add(site_name);
                        break;
                    default:
                        break;
                }
            }

            //ok
            foreach (string site_name in list_ok)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["site_name"].ToString()== site_name)
                    {
                        DateTime final_time = Convert.ToDateTime(row["final"].ToString());
                        TimeSpan span = DateTime.Now - final_time;
                        if (span.TotalMinutes > 5)
                        {
                            row["state"] = "waiting";
                            row["start_time"] = "";
                            row["end_time"] = "";
                            row["final_time"] = "";
                            row["browser"] = "";
                        }
                    }
                }

            }
        }
        public void select_method_from_site(WebBrowser browser, int row_id)
        {
            //try
            //{
                int index = Convert.ToInt32(browser.Name);
                string method = dt.Rows[row_id]["method"].ToString();
                string site_name = dt.Rows[row_id]["site_name"].ToString();

                //invoke method
                Type reflect_type = Type.GetType("Match100Method");
                object reflect_acvtive = Activator.CreateInstance(reflect_type, null);
                MethodInfo method_info = reflect_type.GetMethod(method);
                Match100Helper.create_log(method_info.Name, browser);

                BsonDocument doc_input = ies[index].doc_result;
                BsonDocument doc_result = (BsonDocument)method_info.Invoke(reflect_acvtive, new object[] { browser,doc_input});
                ies[index].doc_result = doc_result;

                //update grid 
                dt.Rows[row_id]["end_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                

                Application.DoEvents();
            //}
            //catch (Exception error) { }

        }
        public void bind_data()
        {
            dt = new DataTable();

            DataColumn col = new DataColumn();
            col.DataType = Type.GetType("System.Boolean");
            col.ColumnName = "selected";
            col.DefaultValue = true;
            dt.Columns.Add(col);

            dt.Columns.Add("id");
            dt.Columns.Add("site_name");
            dt.Columns.Add("step");
            dt.Columns.Add("select_type");
            dt.Columns.Add("url");
            dt.Columns.Add("method");
            dt.Columns.Add("seconds");
            dt.Columns.Add("state");
            dt.Columns.Add("start_time");
            dt.Columns.Add("end_time");
            dt.Columns.Add("final_time");
            dt.Columns.Add("browser");

            string sql = "select * from website_url where is_use='y' order by site_name,step";
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
    }

    public class IE
    {
        public WebBrowser browser;
        public int row_id;
        public int index;
        public bool is_use = false;
        public BsonDocument doc_result = new BsonDocument();
    }
}
