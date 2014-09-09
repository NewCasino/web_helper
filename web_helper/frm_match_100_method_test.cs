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
            test_188bet(); 
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        //2014-08-28
        public void test_188bet()
        {
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
                        string start_time = str_dates[1] + "-" + str_dates[0] + "●" + time;

                        string league = list_lg[index].ToString();
                        string host = doc.DocumentNode.SelectSingleNode(path2 + "/td[2]/div[2]/div[1]/span[1]").InnerText;
                        string client = doc.DocumentNode.SelectSingleNode(path2 + "/td[2]/div[2]/div[2]/span[1]").InnerText;
                        string win = doc.DocumentNode.SelectSingleNode(path2 + "/td[3]/span[1]").InnerText;
                        string draw = doc.DocumentNode.SelectSingleNode(path2 + "/td[4]/span[1]").InnerText;
                        string lose = doc.DocumentNode.SelectSingleNode(path2 + "/td[5]/span[1]").InnerText;
                        if (!league.Contains("Specials"))
                        {
                            sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
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
        
        ", "").Trim();

                    HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(xpath1 + "/table");
                    foreach (HtmlNode node_tr in nodes_tr)
                    {
                        string xpath2 = node_tr.XPath;
                        start_time = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[1]/span[1]").InnerText + "●" +
                                           doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[1]/span[2]").InnerText;
                        host = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[2]/span[1]").InnerText;
                        client = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[2]/span[2]").InnerText;
                        win = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[3]").InnerText;
                        draw = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[4]").InnerText;
                        lose = doc.DocumentNode.SelectSingleNode(xpath2 + "/tbody[1]/tr[1]/td[5]").InnerText;
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.E_TRIM().PR(10) + draw.E_TRIM().PR(10) + lose.E_TRIM().PR(10));
                    }
                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void test_fun88()
        {
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
                    if (!string.IsNullOrEmpty(node.InnerText) && node.InnerText.Trim() != "&nbsp;")
                    {
                        league = node.InnerText;
                    }
                }
                if (node.Attributes.Contains("class") &&
                    node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("bgcpe"))
                {

                    if (!string.IsNullOrEmpty(node.InnerText) && node.InnerText.Trim() != "&nbsp;")
                    {
                        try
                        {
                            start_time = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]").InnerText;
                            host = doc.DocumentNode.SelectSingleNode(xpath + "/td[2]/div[1]").InnerText;
                            client = doc.DocumentNode.SelectSingleNode(xpath + "/td[2]/div[2]").InnerText;
                            win = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]/div[1]/span[1]").InnerText;
                            lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]/div[1]/span[2]").InnerText;
                            draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]/div[1]/span[3]").InnerText;
                            if (!string.IsNullOrEmpty(win) && !string.IsNullOrEmpty(client) && !string.IsNullOrEmpty(win))
                            {
                                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                            }
                        }
                        catch (Exception error) { }
                    }
                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }

        //2014-08-29
        public void test_bet16()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "bet16.html"));
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
                if (node.Name == "tr")
                {

                    string xpath = node.XPath;
                    HtmlNodeCollection nodes_td = doc.DocumentNode.SelectNodes(xpath + "/td");
                    if (nodes_td != null && nodes_td.Count == 3)
                    {
                        league = nodes_td[1].InnerText;
                    }
                    if (nodes_td != null && nodes_td.Count == 10)
                    {
                        start_time = nodes_td[0].InnerText;
                        host = doc.DocumentNode.SelectSingleNode(nodes_td[1].XPath + "/div[1]").InnerText;
                        client = doc.DocumentNode.SelectSingleNode(nodes_td[1].XPath + "/div[2]").InnerText;
                        win = nodes_td[3].InnerText;
                        draw = nodes_td[4].InnerText;
                        lose = nodes_td[5].InnerText;
                        if (!league.Contains("LeagueName") && !start_time.Contains("<!--"))
                        {
                            sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        }

                    }

                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void test_betvictor()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "betvictor.html"));
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
                // && node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("list-headerskinlight")
                if (node.Name == "li" && node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "list-header skin light")
                {
                    league = node.InnerText;
                }
                if (node.Name == "li" && node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "list-header with-two-lines with-markets skin ultra-light")
                {
                    string teams = doc.DocumentNode.SelectSingleNode(xpath + "/a[1]/div[1]").InnerText;
                    string[] strs = teams.Split(new string[] { " v " }, StringSplitOptions.RemoveEmptyEntries);
                    host = strs[0];
                    client = strs[1];
                    start_time = doc.DocumentNode.SelectSingleNode(xpath + "/a[1]/div[2]").InnerText;
                }
                if (node.Name == "li" && node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "row with-columns")
                {
                    win = doc.DocumentNode.SelectSingleNode(xpath + "/ul[1]/li[1]/a[1]/span[2]").InnerText;
                    draw = doc.DocumentNode.SelectSingleNode(xpath + "/ul[1]/li[2]/a[1]/span[2]").InnerText;
                    lose = doc.DocumentNode.SelectSingleNode(xpath + "/ul[1]/li[3]/a[1]/span[2]").InnerText;
                    win = Match100Helper.convert_english_odd(win);
                    draw = Match100Helper.convert_english_odd(draw);
                    lose = Match100Helper.convert_english_odd(lose);
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void test_interwetten()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "interwetten.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath;
                // && node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("list-headerskinlight")
                if (node.Name == "td" && node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "fvd")
                {
                    league = node.InnerText;
                }
                if (node.Name == "td" && node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "playtime")
                {
                    date = node.InnerText;
                    string[] strs = date.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    date = strs[1] + "-" + strs[0] + "●";

                }
                if (node.Name == "td" && node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "date")
                {
                    time = node.InnerText;
                }
                if (node.Name == "td" && node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "bets")
                {
                    host = doc.DocumentNode.SelectSingleNode(xpath + "/table[1]/tbody[1]/tr[1]/td[1]/p[1]/span[1]").InnerText;
                    client = doc.DocumentNode.SelectSingleNode(xpath + "/table[1]/tbody[1]/tr[1]/td[3]/p[1]/span[1]").InnerText;
                    win = doc.DocumentNode.SelectSingleNode(xpath + "/table[1]/tbody[1]/tr[1]/td[1]/p[1]/strong[1]").InnerText;
                    draw = doc.DocumentNode.SelectSingleNode(xpath + "/table[1]/tbody[1]/tr[1]/td[2]/p[1]/strong[1]").InnerText;
                    lose = doc.DocumentNode.SelectSingleNode(xpath + "/table[1]/tbody[1]/tr[1]/td[3]/p[1]/strong[1]").InnerText;

                    start_time = date + time;
                    win = win.Replace(",", ".");
                    draw = draw.Replace(",", ".");
                    lose = lose.Replace(",", ".");
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));

                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void test_sbobet()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "sbobet.html"));
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
                // && node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("list-headerskinlight")
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "SubHeadT")
                {
                    league = node.InnerText;
                }
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "Onex2")
                {
                    HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(xpath + "/tbody[1]/tr");
                    foreach (HtmlNode node_tr in nodes_tr)
                    {
                        string xpath2 = node_tr.XPath;

                        HtmlNodeCollection nodes_td = doc.DocumentNode.SelectNodes(xpath2 + "/td");


                        //Aug 2902:45
                        start_time = nodes_td[0].InnerText.Trim();
                        if (!start_time.Contains("-"))
                        {
                            start_time = Tool.get_12m_from_eng(start_time.Substring(0, 3)) + "-" + start_time.Substring(4, 2) + "●" + start_time.Substring(6, 5);
                            host = doc.DocumentNode.SelectSingleNode(nodes_td[2].XPath + "/a[1]/span[2]").InnerText;
                            win = doc.DocumentNode.SelectSingleNode(nodes_td[2].XPath + "/a[1]/span[1]").InnerText;
                            draw = doc.DocumentNode.SelectSingleNode(nodes_td[3].XPath + "/a[1]/span[1]").InnerText;
                            client = doc.DocumentNode.SelectSingleNode(nodes_td[4].XPath + "/a[1]/span[2]").InnerText;
                            lose = doc.DocumentNode.SelectSingleNode(nodes_td[4].XPath + "/a[1]/span[1]").InnerText;

                            sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        }

                    }

                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        public void test_mansion88()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "mansion88.html"));
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
                // && node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("list-headerskinlight")
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "bggroup rows")
                {
                    league = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]").InnerText;
                }
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "bgAltRow")
                {
                    if (doc.DocumentNode.SelectSingleNode(xpath + "/td[1]").ChildNodes.Count > 10)
                    {
                        start_time = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]/div[1]").InnerText + "●" + doc.DocumentNode.SelectSingleNode(xpath + "/td[1]/div[2]").InnerText;
                        host = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]/td[1]").InnerText;
                        win = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]/td[2]").InnerText;
                        draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]/td[3]").InnerText;
                        lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]/td[4]").InnerText;
                    }
                    if (doc.DocumentNode.SelectSingleNode(xpath + "/td[1]").ChildNodes.Count == 1)
                    {
                        client = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]").InnerText;
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    }
                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }

        //2014-09-02
        public void test_sportbet()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "sportbet.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath;
                // && node.Attributes["class"].Value.ToString().ToLower().Trim().Contains("list-headerskinlight")
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "accordion-main")
                {
                    HtmlNodeCollection nodes_li = doc.DocumentNode.SelectNodes(xpath + "/li");
                    foreach (HtmlNode node_li in nodes_li)
                    {
                        string xpath1 = node_li.XPath;
                        if (node_li.ChildNodes.Count == 3)
                        {
                            date = node_li.InnerText.E_TRIM();
                            string[] dates = date.E_SPLIT("/");
                            if (dates.Length >= 3)
                            {
                                date = dates[1] + "-" + dates[0].Substring(dates[0].Length - 2, 2);
                            }
                        }
                        if (node_li.ChildNodes.Count == 5)
                        {
                            time = doc.DocumentNode.SelectSingleNode(xpath1 + "/div[1]/div[1]").InnerText;
                            start_time = date + "●" + time;
                            string str_teams = doc.DocumentNode.SelectSingleNode(xpath1 + "/div[1]/div[2]/a[1]").InnerText;
                            string[] teams = str_teams.E_SPLIT(" v ");
                            host = teams[0].ToString();
                            client = teams[1].ToString();

                            win = doc.DocumentNode.SelectSingleNode(xpath1 + "/div[2]/div[1]/div[1]/a[1]/span[2]").InnerText;
                            draw = doc.DocumentNode.SelectSingleNode(xpath1 + "/div[2]/div[1]/div[2]/a[1]/span[2]").InnerText;
                            lose = doc.DocumentNode.SelectSingleNode(xpath1 + "/div[2]/div[1]/div[3]/a[1]/span[2]").InnerText;
                            sb.AppendLine("".PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));

                        }

                    }

                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_victorbet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "victorbet.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath;
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "has_group_date")
                {
                    //Mon01Sep
                    date = doc.DocumentNode.SelectSingleNode(node.XPath + "/tbody[1]/tr[1]/td[1]").InnerText.E_TRIM();
                    date = Tool.get_12m_from_eng(date.Substring(5, 3)) + "-" + date.Substring(3, 2);
                }
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "tablesorter tablesorter-default")
                {
                    HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(xpath + "/tbody[1]/tr");
                    foreach (HtmlNode node_tr in nodes_tr)
                    {

                        try
                        {
                            time = node_tr.SELECT_NODE("/td[1]").InnerText;
                            start_time = date + "●" + time;
                            string str_teams = node_tr.SELECT_NODE("/td[2]/a[1]").InnerText.Replace("<!--IE fix-->", "");
                            string[] teams = str_teams.E_SPLIT(" v ");
                            if (teams.Length == 2)
                            {
                                host = teams[0]; client = teams[1];
                            }
                            league = node_tr.SELECT_NODE("/td[2]/span[1]").InnerText;
                            win = Match100Helper.convert_english_odd(node_tr.SELECT_NODE("/td[3]/span[1]/a[1]/span[1]").InnerText);
                            draw = Match100Helper.convert_english_odd(node_tr.SELECT_NODE("/td[4]/span[1]/a[1]/span[1]").InnerText);
                            lose = Match100Helper.convert_english_odd(node_tr.SELECT_NODE("/td[5]/span[1]/a[1]/span[1]").InnerText);

                            sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        }
                        catch (Exception error) { }



                    }

                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_marathonbet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "marathonbet.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Id == "container_EVENTS")
                {
                    HtmlNodeCollection nodes_div = node.SELECT_NODES("/div");
                    foreach (HtmlNode node_div in nodes_div)
                    {
                        if (node_div.Id.Contains("container"))
                        {
                            league = node_div.SELECT_NODE("div[1]/h2[1]").InnerText;

                            HtmlNode test = node_div.SELECT_NODE("div[2]/div[1]/table[1]");
                            HtmlNodeCollection nodes_table = node_div.SELECT_NODES("div[2]/div[1]/table[1]/tbody[1]/tbody");
                            foreach (HtmlNode node_table in nodes_table)
                            {
                                date = node_table.SELECT_NODE("/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[2]").InnerText.E_TRIM();
                                if (date.Length == 10)
                                {
                                    start_time = Tool.get_12m_from_eng(date.Substring(2, 3)) + "-" + date.Substring(0, 2) + "●" + date.Substring(5, 5);
                                }
                                if (date.Length == 5)
                                {
                                    start_time = date;
                                }
                                host = node_table.SELECT_NODE("/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[1]/span[1]/div[1]").InnerText;
                                client = node_table.SELECT_NODE("/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[1]/span[1]/div[2]").InnerText;
                                win = Match100Helper.convert_english_odd(node_table.SELECT_NODE("/tr[1]/td[2]").InnerText);
                                draw = Match100Helper.convert_english_odd(node_table.SELECT_NODE("/tr[1]/td[3]").InnerText);
                                lose = Match100Helper.convert_english_odd(node_table.SELECT_NODE("/tr[1]/td[4]").InnerText);
                                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));

                            }

                        }
                    }
                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_coral()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "coral.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {

                if (node.CLASS() == "match featured-match")
                {

                    string test = node.SELECT_NODE("/div[1]").TEXT(1);
                    start_time = node.SELECT_NODE("/div[1]").ChildNodes[0].InnerText.Replace(" ", "●").E_TRIM();
                    string str_teams = node.SELECT_NODE("/div[3]").InnerText;
                    string[] teams = str_teams.E_SPLIT(" v ");
                    if (teams.Length == 2)
                    {
                        host = teams[0];
                        client = teams[1];
                    }

                    win = node.SELECT_NODE("/div[5]/div[1]/span[2]").InnerText;
                    draw = node.SELECT_NODE("/div[6]/div[1]/span[2]").InnerText;
                    lose = node.SELECT_NODE("/div[7]/div[1]/span[2]").InnerText;
                    sb.AppendLine("".PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));

                }

            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }

        //2014-09-03
        public void test_gamebookers()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "gamebookers.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {

                if (node.CLASS() == "event-group-level2")
                {
                    league = node.InnerText.Replace("&nbsp;", "").E_TRIM();
                }
                if (node.CLASS() == "listing event")
                {
                    start_time = node.SELECT_NODE("/h6[1]/span[2]").InnerText + "●" + node.SELECT_NODE("/h6[1]/span[1]").InnerText;
                }
                if (node.CLASS() == "options")
                {
                    //HtmlNode button = node.SELECT_NODE("/tbody[1]/tr[1]/td[1]");
                    host = node.SELECT_NODE("/tbody[1]/tr[1]/td[1]/button[1]/span[2]").InnerText;
                    client = node.SELECT_NODE("/tbody[1]/tr[1]/td[3]/button[1]/span[2]").InnerText;
                    win = node.SELECT_NODE("/tbody[1]/tr[1]/td[1]/button[1]/span[1]").InnerText;
                    draw = node.SELECT_NODE("/tbody[1]/tr[1]/td[2]/button[1]/span[1]").InnerText;
                    lose = node.SELECT_NODE("/tbody[1]/tr[1]/td[3]/button[1]/span[1]").InnerText;

                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));

                }

            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_oddsring()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "oddsring.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {

                if (node.CLASS() == "ComingUPTable")
                {
                    HtmlNodeCollection nodes_tr = node.SELECT_NODES("/tbody[1]/tr");
                    foreach (HtmlNode node_tr in nodes_tr)
                    {
                        if (!node_tr.InnerText.Contains("Time"))
                        {

                            start_time = node_tr.SELECT_NODE("/td[2]").ChildNodes[2].InnerText;
                            string str_teams = node_tr.SELECT_NODE("/td[3]/div[1]").InnerText;
                            string[] teams = str_teams.E_SPLIT(" - ");
                            if (teams.Length > 1)
                            {
                                host = teams[0];
                                client = teams[1];
                            }
                            league = node_tr.SELECT_NODE("/td[3]/span[1]").InnerText;
                            win = node_tr.SELECT_NODE("/td[4]/div[1]/a[1]").InnerText;

                            if (node_tr.SELECT_NODE("/td[5]").InnerText.E_TRIM() == "—")
                            {
                                draw = node_tr.SELECT_NODE("/td[5]").InnerText.E_TRIM();
                            }
                            else
                            {
                                draw = node_tr.SELECT_NODE("/td[5]/div[1]/a[1]").InnerText;
                            }

                            lose = node_tr.SELECT_NODE("/td[6]/div[1]/a[1]").InnerText;
                            if (league.Contains("Soccer"))
                            {
                                start_time = start_time.Replace(" ", P.D);
                                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                            }

                        }

                    }

                }

            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_snai()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "snai.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Name == "tr")
                {
                    HtmlNodeCollection nodes_td = node.SELECT_NODES("/td");
                    if (nodes_td != null && nodes_td.Count == 1)
                    {
                        start_time = node.SELECT_NODE("/td[1]").InnerText.Replace("&nbsp;&nbsp;", "").Replace(" ", "●").E_TRIM();
                    }
                    if (nodes_td != null && nodes_td.Count == 2)
                    {
                        league = node.SELECT_NODE("/td[2]/a[1]").InnerText;
                        string str_teams = node.SELECT_NODE("/td[2]/a[2]").InnerText;
                        string[] teams = str_teams.E_SPLIT(" - ");
                        if (teams.Length == 2)
                        {
                            host = teams[0];
                            client = teams[1];
                        }
                        win = node.SELECT_NODE("/td[2]/td[1]/a[1]/div[1]").InnerText;
                        draw = node.SELECT_NODE("/td[2]/td[2]/a[1]/div[1]").InnerText;
                        lose = node.SELECT_NODE("/td[2]/td[3]/a[1]/div[1]").InnerText;

                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    }
                }


            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_12bet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "12bet.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {
                if (node.CLASS() == "tabtitle")
                {
                    league = node.InnerText.Replace("&nbsp;", "").E_REMOVE();
                }
                if (node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 10)
                {
                    start_time = node.SELECT_NODE("/td[1]").InnerText;
                    host = node.SELECT_NODE("/td[2]/span[1]").InnerText;
                    client = node.SELECT_NODE("/td[2]/span[2]").InnerText;
                    string str_odds = node.SELECT_NODE("/td[9]/div[1]").InnerHtml;
                    string[] odds = str_odds.E_SPLIT("<br>");
                    if (odds.Length == 3)
                    {
                        win = odds[0]; draw = odds[1]; lose = odds[2];
                    }
                    else
                    {
                        win = ""; draw = ""; lose = "";
                    }
                    if (!league.Contains("LeagueName") && !string.IsNullOrEmpty(win.Trim()) && !start_time.Contains("-"))
                    {
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    }
                }

            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_1bet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "1bet.html"));
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
            string date = "";
            string time = "";
            string mark = "";
            foreach (HtmlNode node in nodes_all)
            {

                if (node.Name == "h2" && node.CLASS() == "SubTitle")
                {
                    mark = node.InnerText.ToLower();
                }
                if (node.CLASS() == "Header")
                {
                    league = node.InnerText;
                }
                if (node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 9)
                {
                    start_time = node.SELECT_NODE("/td[1]/div[1]").InnerHtml.Replace("<br>", P.D);
                    if (!start_time.Contains("ShowTime"))
                    {

                        string str_teams = "";
                        if (node.SELECT_NODE("/td[2]/a[1]") != null)
                        {
                            str_teams = node.SELECT_NODE("/td[2]/a[1]").InnerHtml;
                        }
                        else
                        {
                            str_teams = node.SELECT_NODE("/td[2]").InnerHtml;
                        }
                        string[] teams = str_teams.E_SPLIT("<em>vs</em>");
                        if (teams.Length == 2)
                        {
                            host = teams[0]; client = teams[1];
                        }
                        else
                        {
                            host = ""; client = "";
                        }
                        win = node.SELECT_NODE("/td[3]").InnerText;
                        draw = node.SELECT_NODE("/td[4]").InnerText;
                        lose = node.SELECT_NODE("/td[5]").InnerText;
                        if (!mark.Contains("half"))
                        {
                            start_time = Tool.get_12m_from_eng(start_time.Substring(0, 3)) + "-" + start_time.Substring(3, start_time.Length - 3).E_TRIM();
                            sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        }
                    }
                }

            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }

        //2014-09-04
        public void test_youwin()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "youwin.html"));
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
            string date = "";
            string time = "";
            foreach (HtmlNode node in nodes_all)
            {
                if (node.CLASS() == "eventTSoccer")
                {
                    start_time = node.SELECT_NODE("tbody[1]/tr[1]/td[1]/span[1]/span[1]").InnerText + P.D + node.SELECT_NODE("tbody[1]/tr[1]/td[1]/span[1]/span[2]").InnerText;
                    host = node.SELECT_NODE("tbody[1]/tr[1]/td[2]/div[1]").InnerText;
                    client = node.SELECT_NODE("tbody[1]/tr[1]/td[2]/div[2]").InnerText;
                    win = node.SELECT_NODE("tbody[1]/tr[1]/td[3]").InnerText;
                    draw = node.SELECT_NODE("tbody[1]/tr[1]/td[4]").InnerText;
                    lose = node.SELECT_NODE("tbody[1]/tr[1]/td[5]").InnerText;

                    start_time = Tool.get_12m_from_eng(start_time.Substring(0, 3)) + "-" + start_time.Substring(3, start_time.Length - 3).E_TRIM();
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                }

            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }






    }
}
