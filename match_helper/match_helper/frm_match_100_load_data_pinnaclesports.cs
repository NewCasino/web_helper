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

namespace match_helper
{
    public partial class frm_match_100_load_data_pinnaclesports : Form
    {
        StringBuilder sb = new StringBuilder();
        List<IE> ies = new List<IE>();
        DataTable dt = new DataTable();
        BsonDocument doc_result = Match100Helper.get_doc_result();
        public frm_match_100_load_data_pinnaclesports()
        {
            InitializeComponent();
        }

        private void btn_navigate_Click(object sender, EventArgs e)
        {
            browser.Navigate(this.txt_url.Text);
        }
        private void frm_match_100_Load(object sender, EventArgs e)
        {
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
            this.dgv_result.Columns["url"].Width = 80;
            this.dgv_result.Columns["start_time"].Width = 120;
            this.dgv_result.Columns["end_time"].Width = 120;
            this.dgv_result.Columns["final_time"].Width = 120;
            this.dgv_result.Columns["id"].Width = 50;
            this.dgv_result.Columns["site_name"].Width = 80;
            this.dgv_result.Columns["step"].Width = 50;
            this.dgv_result.Columns["method"].Width = 150;
            this.dgv_result.Columns["select_type"].Width = 80;
            this.dgv_result.Columns["seconds"].Width = 50;
            this.dgv_result.Columns["state"].Width = 80;
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

            ////int index = Convert.ToInt32(browser.Name);
            ////int row_id = ies[index].row_id;

            //if (dt.Rows[row_id]["select_type"].ToString() == "load")
            //{
            //    dt.Rows[row_id]["end_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    BsonDocument doc_result = Match100Helper.get_doc_result();
            //    doc_result["data"] = "Load Complete!";
            //    doc_result["url"] = browser.Document.Url.ToString(); 
            //}
            foreach (DataRow row in dt.Rows)
            {
                if (row["method"].ToString() == "load" && row["state"].ToString() == "doing" && !string.IsNullOrEmpty(row["start_time"].ToString()) && string.IsNullOrEmpty(row["end_time"].ToString()))
                {
                    row["end_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    doc_result["data"] = "Load Complete!";
                    doc_result["url"] = browser.Document.Url.ToString();
                }
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

                            sb.AppendLine("-----------------------------------------------------------------------------------------------------------------");
                            sb.AppendLine("web site:".PR(15) + row["site_name"].PR(10) + row["method"].ToString());
                            sb.AppendLine("time:".PR(15) + row["start_time"].ToString().Substring(11, 8).PR(10) + row["end_time"].ToString().Substring(11, 8).PR(10) + row["final_time"].ToString().Substring(11, 8).PR(10));
                            sb.AppendLine("url:".PR(15) + doc_result["url"].ToString());
                            sb.AppendLine("result data:".PR(15));
                            sb.AppendLine(doc_result["data"].ToString());
                            sb.AppendLine("-----------------------------------------------------------------------------------------------------------------");
                            this.txt_result.Text = sb.ToString();

                            //判断是否要循环使用
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
                                        }
                                    }
                                }
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


                if (i != 0 && dt.Rows[i - 1]["state"].ToString() != "ok")
                {
                    is_can_start = false;
                }
                if (dt.Rows[i]["state"].ToString() == "wait" && is_can_start == true)
                {
                    if (dt.Rows[i - 1]["state"].ToString() == "ok")
                    {
                        dt.Rows[i]["state"] = "doing";
                        dt.Rows[i]["start_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        select_method_from_site(browser, i);
                    }
                }
            }
            Application.DoEvents();
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
                bool is_has = false;
                foreach (string item in list_selected)
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
                    if (row["site_name"].ToString() == site_name)
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

            int index = Convert.ToInt32(browser.Name);
            string method = dt.Rows[row_id]["method"].ToString();
            string site_name = dt.Rows[row_id]["site_name"].ToString();

            switch (method)
            {
                case "from_pinnaclesports_me_index":
                    doc_result = from_pinnaclesports_me_index(ref browser, doc_result);
                    break;
                case "from_pinnaclesports_me":
                    doc_result = from_pinnaclesports_me(ref browser, doc_result);
                    break;
                default:
                    break;
            }
            //invoke method
            //Type reflect_type = Type.GetType("Match100Method");
            //object reflect_acvtive = Activator.CreateInstance(reflect_type, null);
            //MethodInfo method_info = reflect_type.GetMethod(method);
            //Match100Helper.create_log(method_info.Name, browser); 
            //doc_result = (BsonDocument)method_info.Invoke(reflect_acvtive, new object[] { browser,doc_result});


            //update grid 
            dt.Rows[row_id]["end_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Application.DoEvents();


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

            DataRow row0 = dt.NewRow();
            row0["id"] = "0";
            row0["site_name"] = "pinnaclesports";
            row0["step"] = "0";
            row0["select_type"] = "method";
            row0["url"] = "";
            row0["method"] = "from_pinnaclesports_me_index";
            row0["seconds"] = "30";
            row0["state"] = "wait";
            row0["start_time"] = "";
            row0["end_time"] = "";
            row0["final_time"] = "";
            dt.Rows.Add(row0);

            DataRow row1 = dt.NewRow();
            row1["id"] = "1";
            row1["site_name"] = "pinnaclesports";
            row1["step"] = "1";
            row1["select_type"] = "method";
            row1["url"] = "";
            row1["method"] = "from_pinnaclesports_me";
            row1["seconds"] = "30";
            row1["state"] = "wait";
            row1["start_time"] = "";
            row1["end_time"] = "";
            row1["final_time"] = "";
            dt.Rows.Add(row1);


            this.dgv_result.DataSource = dt;

        }


        //methods
        public BsonDocument from_pinnaclesports_me_index(ref WebBrowser browser, BsonDocument doc_result)
        {

            doc_result = Match100Helper.get_doc_result();


            string html = BrowserHelper.get_html(ref browser);
            StringBuilder sb = new StringBuilder();
            //================================================================
            BsonArray url1 = new BsonArray();
            BsonArray url2 = new BsonArray();
            html = html.Replace("<thead=\"\"", "");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();

            ArrayList list_lg = new ArrayList();

            string str_class = "";
            string name = "";
            string href = "";
            int count = 0;

            foreach (HtmlNode node in nodes_all)
            {
                if (node.Name == "div" && node.CLASS() == "clr")
                {
                    str_class = node.InnerText;
                }
                if (node.Name == "li" && node.SELECT_NODES("/div") != null && node.SELECT_NODES("/div").Count == 2)
                {
                    if (node.SELECT_NODE("/div[1]").CLASS() == "mea i")
                    {
                        name = node.SELECT_NODE("/div[2]").InnerText.E_REMOVE();
                        href = node.SELECT_NODE("/div[2]/a[1]").Attributes["href"].Value;
                        if (str_class == "Soccer" && !name.Contains("Halfs") && !name.Contains("Totals") && !href.Contains("ContestCategory"))
                        {
                            count = count + 1;
                            sb.AppendLine(count.PR(5) + str_class.PR(20) + name.PR(50) + href);
                            url1.Add("http://www.pinnaclesports.com" + href);
                        }
                    }
                }
            }
            //===============================================================
            doc_result["data"] = sb.ToString();
            doc_result["url"] = browser.Document.Url.ToString();
            doc_result.Add("url1", url1);
            doc_result.Add("url2", url2);
            return doc_result;
        }
        public BsonDocument from_pinnaclesports_me(ref WebBrowser browser, BsonDocument doc_result)
        {
            string html = BrowserHelper.get_html(ref browser);
            StringBuilder sb = new StringBuilder();
            string result = "";


            string url = "";
            try
            {
                //================================================================   
                if (doc_result["url2"].AsBsonArray.Count == 0)
                {
                    url = doc_result["url1"].AsBsonArray[0].ToString();
                    doc_result["url2"].AsBsonArray.Add(url);
                    browser.Navigate(url);
                    doc_result["loop"].AsBsonArray.Add("2");

                    doc_result["data"] = "Start Read First URL ->" + url;
                    doc_result["url"] = browser.Document.Url.ToString();
                    return doc_result;
                }
                //---------------------------------------------------------------
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
                ArrayList list_lg = new ArrayList();
                ArrayList list_times = new ArrayList();
                ArrayList list_teams = new ArrayList();
                ArrayList list_odds = new ArrayList();
                foreach (HtmlNode node in nodes_all)
                {

                    if (node.Name == "table" && node.Attributes.Contains("class") && node.Attributes["class"].Value == "linesTbl")
                    {
                        if (doc.DocumentNode.SelectSingleNode(node.XPath + "/tbody[1]/tr[2]/td[1]").InnerText.ToLower().Contains("half")) continue;
                        string lg_name = "";
                        string tr_path = node.XPath + "/tbody[1]/tr";
                        HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(tr_path);
                        foreach (HtmlNode node_tr in nodes_tr)
                        {
                            switch (node_tr.Attributes["class"].Value.ToString())
                            {
                                case "linesHeader":
                                    string lg_temp = doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]/h4[1]").InnerText;
                                    string[] lg_list = lg_temp.Split('-');
                                    lg_name = lg_list[0] + "-" + lg_list[1];
                                    break;
                                case "linesAlt1":
                                    if (string.IsNullOrEmpty(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim())) continue;
                                    if (doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Contains("Offline")) continue;

                                    list_lg.Add(lg_name);
                                    list_times.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]").InnerText);
                                    list_teams.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[3]").InnerText);
                                    list_odds.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim());
                                    break;
                                case "linesAlt2":
                                    if (string.IsNullOrEmpty(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim())) continue;
                                    if (doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Contains("Offline")) continue;

                                    list_lg.Add(lg_name);
                                    list_times.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]").InnerText);
                                    list_teams.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[3]").InnerText);
                                    list_odds.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim());
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                }
                for (int i = 0; i < list_lg.Count; i++)
                {
                    if ((i + 2) < list_lg.Count)
                    {
                        string f_lg = list_lg[i].ToString();
                        string f_time = list_times[i].ToString() + " " + list_times[i + 1].ToString();
                        string f_host = list_teams[i].ToString();
                        string f_client = list_teams[i + 1].ToString();
                        string f_win = list_odds[i].ToString();
                        string f_draw = list_odds[i + 2].ToString();
                        string f_lose = list_odds[i + 1].ToString();
                        Match100Helper.insert_data("pinnaclesports", f_lg, f_time, f_host, f_client, f_win, f_draw, f_lose, "-7", "1");
                        sb.AppendLine(f_lg.PR(50) + f_time.PR(20) + f_host.PR(30) + f_client.PR(30) + f_win.PR(20) + f_draw.PR(20) + f_lose.PR(20));
                    }
                    i = i + 2;
                }

            }
            catch (Exception error)
            {

                sb.AppendLine(error.Message + Environment.NewLine + error.StackTrace);
                Log.error("from_pinnaclesports_2", error);
            }

            //--------------------------------------------------------------
            if (doc_result["url1"].AsBsonArray.Count == doc_result["url2"].AsBsonArray.Count)
            {
                doc_result["url"] = browser.Document.Url.ToString();
                doc_result["data"] = result;
                doc_result["loop"].AsBsonArray.Clear();
                return doc_result;
            }
            url = doc_result["url1"].AsBsonArray[doc_result["url2"].AsBsonArray.Count].ToString();
            doc_result["url2"].AsBsonArray.Add(url);
            browser.Navigate(url);

            doc_result["loop"].AsBsonArray.Clear();
            doc_result["loop"].AsBsonArray.Add("2");
            //=============================================================== 


            doc_result["data"] = sb.ToString();
            doc_result["url"] = browser.Document.Url.ToString();
            return doc_result;
        }

        private void btn_full_Click(object sender, EventArgs e)
        { 

        }

    }
}
