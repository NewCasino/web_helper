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
            dt.Columns.Add("select_type");
            dt.Columns.Add("url"); 
            dt.Columns.Add("method");
            dt.Columns.Add("seconds");
            dt.Columns.Add("state");
            dt.Columns.Add("start_time");
            dt.Columns.Add("end_time");
            dt.Columns.Add("final_time");
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
            this.dgv_result.Columns["final_time"].Width = 150;
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
                //检查正在使用的IE是否执行完毕
                if (row["state"].ToString() == "doing")
                {
                    DateTime start = Convert.ToDateTime(row["start_time"].ToString());
                    TimeSpan span = DateTime.Now - start;
                    int seconds = Convert.ToInt32(row["seconds"].ToString());
                    string site_name = row["site_name"].ToString();

                    if (span.TotalSeconds > seconds )
                    {
                        row["state"] = "ok";
                        row["final_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        BsonDocument doc_result = ies[Convert.ToInt32(row["browser"].ToString())].doc_result;

                        sb.AppendLine("-----------------------------------------------------------------------------------------------------------------");
                        sb.AppendLine("web site:".PR(15) + row["site_name"].PR(10) + row["method"].ToString());
                        sb.AppendLine("url:".PR(15) + ies[Convert.ToInt32(row["browser"].ToString())].browser.Document.Url.ToString());
                        sb.AppendLine("time:".PR(15)+row["start_time"].ToString().Substring(11,8).PR(10)+row["end_time"].ToString().Substring(11,8).PR(10)+row["final_time"].ToString().Substring(11,8).PR(10));   
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
                                for(int j=0;j<dt.Rows.Count;j++)
                                {
                                    if (dt.Rows[j]["site_name"].ToString() == row["site_name"].ToString() && dt.Rows[j]["step"].ToString() == loop[i].ToString())
                                    {
                                        dt.Rows[j]["start_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
                        for (int i = 0; i < dt.Rows.Count;i++ )
                        {
                            if (dt.Rows[i]["site_name"].ToString() == site_name && dt.Rows[i]["state"].ToString() == "wait")
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

            //为等待的任务分配IE
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
            Application.DoEvents();
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
                ies[index].doc_result = doc_result;
            }
            Application.DoEvents();
        }

        public void select_method_from_site(WebBrowser browser, int row_id)
        {
            int index = Convert.ToInt32(browser.Name);
            string method = dt.Rows[row_id]["method"].ToString();
            string site_name=dt.Rows[row_id]["site_name"].ToString();

            //invoke method
            Type reflect_type = Type.GetType("Match100Method");
            object reflect_acvtive = Activator.CreateInstance(reflect_type, null); 
            MethodInfo method_info = reflect_type.GetMethod(method);
            BsonDocument  doc_result = (BsonDocument)method_info.Invoke(reflect_acvtive, new object[] { browser });   

            //update grid 
            dt.Rows[row_id]["end_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ies[index].doc_result = doc_result;

            Application.DoEvents();
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
