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

namespace web_helper
{
    public partial class frm_match_100 : Form
    {
        StringBuilder sb = new StringBuilder();
        List<WebBrowser> browsers = new List<WebBrowser>();
        DataTable dt = new DataTable();

        public frm_match_100()
        {
            InitializeComponent();
        }
        private void frm_match_100_Load(object sender, EventArgs e)
        {
            bind_data();
            for (int i = 0; i < 10; i++)
            {
                WebBrowser browser = new WebBrowser();
                browser.Width = 0;
                browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
                browsers.Add(browser);
            }
        } 
        public void bind_data()
        {
            dt.Columns.Add("id");
            dt.Columns.Add("site_name");
            dt.Columns.Add("url");
            dt.Columns.Add("state");
            dt.Columns.Add("start_time");
            dt.Columns.Add("end_time");

            string sql = "select * from company_url where is_use='y'";
            DataTable dt_temp = SQLServerHelper.get_table(sql);
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                DataRow row_new = dt.NewRow();
                row_new["id"] = i.ToString();
                row_new["site_name"] = dt_temp.Rows[i]["site_name"].ToString();
                row_new["url"] = dt_temp.Rows[i]["url"].ToString();
                row_new["state"] = "wait";
                dt.Rows.Add(row_new);
            }
            this.dgv_result.DataSource = dt;

        }
        private void dgv_result_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dgv_result.Columns[2].Width = 300;
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
            analyse();
        } 
        public void analyse()
        {
            //browser height:0-can use 1-in use  width:row id
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["state"].ToString() == "wait")
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (browsers[j].Width == 0)
                        {
                            dt.Rows[i]["state"] = "do....";
                            dt.Rows[i]["start_time"] = DateTime.Now.ToString("HH:mm:ss");
                            browsers[j].Width = 1;
                            browsers[j].Height = i;
                            browsers[j].Navigate(dt.Rows[i]["url"].ToString());
                            break;
                        }
                    }
                }
            } 
        }
      
        



        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (e.Url != browser.Document.Url) return;


            string html="<body>"+Environment.NewLine+browser.Document.Body.InnerHtml+Environment.NewLine +"</body>"; 
            switch(dt.Rows[browser.Height]["state"].ToString())
            {
                case "163":
                    from_163(html);
                    break;
                default:
                    break;
            }


            dt.Rows[browser.Height]["state"] = "ok";
            dt.Rows[browser.Height]["end_time"] = DateTime.Now.ToString("HH:mm:ss");
            sb.Append(e.Url.ToString().PR(50) +
                      dt.Rows[browser.Height]["start_time"].ToString().PR(10)+
                      dt.Rows[browser.Height]["end_time"].ToString().PR(10)+ 
                      html.Length.ToString() +
                      Environment.NewLine);
            this.txt_result.Text = sb.ToString(); 

            browser.Width = 0;
            Application.DoEvents(); 
        }   

        public void from_163(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Attributes.Contains("leaguename") &&
                    node.Attributes.Contains("starttime") &&
                    node.Attributes.Contains("hostname") &&
                    node.Attributes.Contains("guestname") &&
                    node.Attributes["isstop"].Value.ToString() == "0"
                   )
                {
                    try
                    {
                        string root = node.XPath;
                        string league = node.Attributes["leaguename"].Value.ToString();
                        string start_time = node.Attributes["starttime"].Value.ToString();
                        string host = node.Attributes["hostname"].Value.ToString();
                        string client = node.Attributes["guestname"].Value.ToString();
                        string win = node.SelectSingleNode(root + "/span[6]/div[1]/em[1]").InnerText;
                        string draw = node.SelectSingleNode(root + "/span[6]/div[1]/em[2]").InnerText;
                        string lose = node.SelectSingleNode(root + "/span[6]/div[1]/em[3]").InnerText; 
                        sb.Append(league.PR(20) + start_time.PR(20) + host.PR(20) + client.PR(20) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);



                    }
                    catch (Exception error)
                    {
                        Log.error("from 163", error);
                    }
                }
            }
        }
        public void from_bwin(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            foreach (HtmlNode node in nodes_all)
            {
                try
                {
                    if (
                        node.Attributes.Contains("class") &&
                        node.Attributes["class"].Value.ToString() == "listing event"
                       )
                    {

                        string root = node.XPath;
                        string league = "NO DAT";
                        string start_time = node.SelectSingleNode(root + "/h6[1]/span[1]").InnerText;
                        string host = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[1]/button[1]/span[2]").InnerText;
                        string client = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[3]/button[1]/span[2]").InnerText;
                        string win = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[1]/button[1]/span[1]").InnerText;
                        string draw = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[2]/button[1]/span[1]").InnerText;
                        string lose = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[3]/button[1]/span[1]").InnerText;
                        //sb.Append(root + Environment.NewLine);
                        //sb.Append(node.InnerHtml + Environment.NewLine);
                        sb.Append(league.PR(20) + start_time.PR(20) + host.PR(20) + client.PR(20) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);


                    }
                    if (node.Attributes.Contains("class") &&
                        node.Attributes["class"].Value.ToString() == "listing")
                    {

                        string root = node.XPath;
                        string league = "NO DAT";
                        string start_time = node.SelectSingleNode(root + "/div[1]/h6[1]").InnerText;
                        string host = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[1]/button[1]/span[2]").InnerText;
                        string client = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[3]/button[1]/span[2]").InnerText;
                        string win = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[1]/button[1]/span[1]").InnerText;
                        string draw = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[2]/button[1]/span[1]").InnerText;
                        string lose = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[3]/button[1]/span[1]").InnerText;
                        //sb.Append(root + Environment.NewLine);
                        //sb.Append(node.InnerHtml + Environment.NewLine);
                        sb.Append(league.PR(20) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);


                    }
                }
                catch (Exception error)
                {
                    Log.error("from bwin", error); 
                }

            }
        }


 

    }
}
