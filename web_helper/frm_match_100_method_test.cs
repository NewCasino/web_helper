using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using MongoDB.Bson;
using System.Collections;

namespace web_helper
{
    public partial class frm_match_100_method_test : Form
    {
        StringBuilder sb = new StringBuilder();
        public static string root_path_sites = Environment.CurrentDirectory.Replace(@"bin\Debug", "").Replace(@"bin\x86\Debug", "") + @"data\sites\";
        string root_url_sites = @"file:///" + root_path_sites.Replace(@"\", @"/");
        public frm_match_100_method_test()
        {
            InitializeComponent();
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            test_fun88();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }
        public void test_188bet()
        {

            sb.Remove(0, sb.Length);
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "188bet.html"));


            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();

            ArrayList list_lg = new ArrayList();
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.Trim() == "comp-txt-wrapper")
                {
                    list_lg.Add(node.InnerText);
                }
            }


            int index = 0;
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.Trim() == "comp-container")
                {
                    string path1 = node.XPath;
                    HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(path1 + "/table[1]/tbody[1]/tr");

                    foreach (HtmlNode node_tr in nodes_tr)
                    {
                        string path2 = node_tr.XPath;
                        string date = "";
                        string time = "";
                        HtmlNodeCollection nodes_time = doc.DocumentNode.SelectNodes(path2 + "/td[1]/div");
                        foreach (HtmlNode node_time in nodes_time)
                        {
                            if (node_time.Attributes.Contains("class") && node_time.Attributes["class"].Value.Trim() == "date") date = node_time.InnerText;
                            if (node_time.Attributes.Contains("class") && node_time.Attributes["class"].Value.Trim() == "start-time") time = node_time.InnerText;
                        }

                        string[] str_dates = date.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                        string start_tiime = str_dates[1] + "-" + str_dates[0] + "●" + time;

                        string league = list_lg[index].ToString();
                        string host = doc.DocumentNode.SelectSingleNode(path2 + "/td[2]/div[2]/div[1]/span[1]").InnerText;
                        string client = doc.DocumentNode.SelectSingleNode(path2 + "/td[2]/div[2]/div[2]/span[1]").InnerText;
                        string win = doc.DocumentNode.SelectSingleNode(path2 + "/td[3]/span[1]").InnerText;
                        string draw = doc.DocumentNode.SelectSingleNode(path2 + "/td[4]/span[1]").InnerText;
                        string lose = doc.DocumentNode.SelectSingleNode(path2 + "/td[5]/span[1]").InnerText;
                        if (!league.Contains("Specials"))
                        {
                            sb.AppendLine(league.PR(80) + start_tiime.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                            //Match100Helper.insert_data("188bet", league, start_time, host, client, win, draw, lose, "8", "0");
                        }
                    }
                    index = index + 1;
                }
            }


            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

        }
        public void test_macauslot()
        {
            sb.Remove(0, sb.Length);
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "macauslot.html"));


            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();

            ArrayList list_lg = new ArrayList();
            string league = "";
            string start_time = "";
            string host = "";
            string client = "";
            string win = "";
            string draw = "";
            string lose = "";
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.Trim() == "styletour")
                {
                    league = node.InnerText;
                    list_lg.Add(league);
                }
                if (node.Attributes.Contains("class") && (node.Attributes["class"].Value.Trim() == "styleodds" || node.Attributes["class"].Value.Trim() == "styleodds2"))
                {
                    string xpath1 = node.XPath;
                    HtmlNodeCollection td_nodes = doc.DocumentNode.SelectNodes(xpath1 + "/td");
                    if (td_nodes.Count == 10)
                    {
                        string[] str_dates = td_nodes[0].InnerHtml.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                        start_time = Tool.get_12m_from_eng(str_dates[0].Substring(3, 3)) + "-" + str_dates[0].Substring(0, 2) + "●" + str_dates[1];
                        host = doc.DocumentNode.SelectSingleNode(td_nodes[2].XPath + "/font[1]").InnerText;
                        win = td_nodes[8].InnerText;
                    }
                    if (td_nodes.Count == 9)
                    {
                        client = doc.DocumentNode.SelectSingleNode(td_nodes[1].XPath + "/font[1]").InnerText;
                        lose = td_nodes[7].InnerText;
                    }
                    if (td_nodes.Count == 5)
                    {
                        draw = td_nodes[3].InnerText;
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    }
                }

            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_10bet()
        {
            sb.Remove(0, sb.Length);
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "10bet.html"));


            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();

            ArrayList list_lg = new ArrayList();
            string league = "";
            string start_time = "";
            string host = "";
            string client = "";
            string win = "";
            string draw = "";
            string lose = "";
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.Trim() == "leagueWindow")
                {
                    string xpath1 = node.XPath;
                    league = doc.DocumentNode.SelectSingleNode(xpath1 + "/h5[1]").InnerText;
                    HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(xpath1 + "/div[2]/div[1]/div");
                    foreach (HtmlNode node_tr in nodes_tr)
                    {
                        if (node_tr.Attributes.Contains("class") && node_tr.Attributes["class"].Value.Trim() == "time")
                        {
                            start_time = node_tr.InnerText;
                        }
                        if (node_tr.Attributes.Contains("class") && node_tr.Attributes["class"].Value.Trim().Contains("bets") && node_tr.Attributes["class"].Value.Trim().Contains("ml"))
                        {
                            string xpath2 = node_tr.XPath;
                            host = doc.DocumentNode.SelectSingleNode(xpath2 + "/ul[1]/li[1]/dl[1]/dt[1]/span[1]").InnerText;
                            client = doc.DocumentNode.SelectSingleNode(xpath2 + "/ul[1]/li[1]/dl[1]/dt[1]/span[2]").InnerText;
                            win = doc.DocumentNode.SelectSingleNode(xpath2 + "/ul[1]/li[1]/dl[1]/dd[1]/ul[1]/li[1]").InnerText;
                            draw = doc.DocumentNode.SelectSingleNode(xpath2 + "/ul[1]/li[1]/dl[1]/dd[1]/ul[1]/li[2]").InnerText;
                            lose = doc.DocumentNode.SelectSingleNode(xpath2 + "/ul[1]/li[1]/dl[1]/dd[1]/ul[1]/li[3]").InnerText;
                            sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        }
                    }
                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_fubo()
        {
            sb.Remove(0, sb.Length);
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "fubo.html"));


            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();

            ArrayList list_lg = new ArrayList();
            string league = "";
            string start_time = "";
            string host = "";
            string client = "";
            string win = "";
            string draw = "";
            string lose = "";
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Attributes.Contains("class") &&
                    node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("competition") &&
                    node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("eventtype"))
                {
                    string xpath1 = node.XPath;
                    league = doc.DocumentNode.SelectSingleNode(xpath1 + "/div[1]").InnerText.Replace(@" 
        
        ","").Trim();

                    HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(xpath1 + "/table");
                    foreach (HtmlNode node_tr in nodes_tr)
                    {
                        string xpath2 = node_tr.XPath;
                         start_time = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[1]/span[1]").InnerText +"●"+
                                            doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[1]/span[2]").InnerText;
                        host = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[2]/span[1]").InnerText;
                        client = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[2]/span[2]").InnerText;
                        win = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[3]").InnerText;
                        draw = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[4]").InnerText;
                        lose = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[5]").InnerText;
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10)); 
                    }
                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents(); 
        }
        public void test_fun88()
        {
            sb.Remove(0, sb.Length);
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "fun88.html"));



            html = html.Replace("<thead=\"\"", "");
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();

            ArrayList list_lg = new ArrayList();
            string league = "";
            string start_time = "";
            string host = "";
            string client = "";
            string win = "";
            string draw = "";
            string lose = "";
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath;
                if (node.Attributes.Contains("class") &&
                    node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("tabtitle") &&
                    node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("none_rline"))
                {
                   if(!string.IsNullOrEmpty(node.InnerText) && node.InnerText.Trim()!="&nbsp;")
                   {
                       league=node.InnerText;
                   }
                }
                if (node.Attributes.Contains("class") &&
                    node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("bgcpe") )
                {

                    
                    if (!string.IsNullOrEmpty(node.InnerText) && node.InnerText.Trim() != "&nbsp;")
                    {
                        start_time = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]").InnerText;
                        host = doc.DocumentNode.SelectSingleNode(xpath + "/td[2]/div[1]").InnerText;
                        client = doc.DocumentNode.SelectSingleNode(xpath + "/td[2]/div[2]").InnerText;
                        win = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]/div[1]/span[1]").InnerText;
                        lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]/div[1]/span[2]").InnerText;
                        draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]/div[1]/span[3]").InnerText;
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    }
                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
    }
}
