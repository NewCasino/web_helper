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
            test_gobetgo_index_language();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }


        public void sample()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "XXXXXXX.html"));
            //==========================================================================================================
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
                    start_time = node.SELECT_NODE("tbody[1]/tr[1]/td[1]/span[1]/span[1]").InnerText + M.D + node.SELECT_NODE("tbody[1]/tr[1]/td[1]/span[1]/span[2]").InnerText;
                    host = node.SELECT_NODE("tbody[1]/tr[1]/td[2]/div[1]").InnerText;
                    client = node.SELECT_NODE("tbody[1]/tr[1]/td[2]/div[2]").InnerText;
                    win = node.SELECT_NODE("tbody[1]/tr[1]/td[3]").InnerText;
                    draw = node.SELECT_NODE("tbody[1]/tr[1]/td[4]").InnerText;
                    lose = node.SELECT_NODE("tbody[1]/tr[1]/td[5]").InnerText;

                    start_time = Tool.get_12m_from_eng(start_time.Substring(0, 3)) + "-" + start_time.Substring(3, start_time.Length - 3).E_TRIM();
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    // Match100Helper.insert_data("XXXX", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }

        #region other function
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
                            Match100Helper.insert_data("188bet", league, start_time, host, client, win, draw, lose, "8", "0");
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
                        Match100Helper.insert_data("macauslot", league, start_time, host, client, win, draw, lose, "8", "0");
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
                            string[] times = start_time.E_TRIM().E_SPLIT("|");
                            start_time = times[0].Substring(3, 2) + "-" + times[0].Substring(0, 2) + M.D + times[1];
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
                            Match100Helper.insert_data("10bet", league, start_time, host, client, win, draw, lose, "8", "0");
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
                        Match100Helper.insert_data("fubo", league, start_time, host, client, win, draw, lose, "8", "0");
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

                            if (start_time.Contains("LIVE")) start_time = start_time.Replace("LIVE", DateTime.Now.ToString("MM-dd") + "●");
                            if (!string.IsNullOrEmpty(win) && !string.IsNullOrEmpty(client) && !string.IsNullOrEmpty(win))
                            {
                                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                //Match100Helper.insert_data("fun88", league, start_time, host, client, win, draw, lose, "8", "0");
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
                            Match100Helper.insert_data("bet16", league, start_time, host, client, win, draw, lose, "8", "0");
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
                    Match100Helper.insert_data("betvictor", league, start_time, host, client, win, draw, lose, "2", "0");
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
                    Match100Helper.insert_data("interwetten", league, start_time, host, client, win, draw, lose, "8", "0");

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
                            //Match100Helper.insert_data("sobobet", league, start_time, host, client, win, draw, lose, "8", "0");
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
                        //Match100Helper.insert_data("mansion88", league, start_time, host, client, win, draw, lose, "8", "0");
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
                            Match100Helper.insert_data("sportbet", league, start_time, host, client, win, draw, lose, "10", "0");
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
                            Match100Helper.insert_data("victorbet", league, start_time, host, client, win, draw, lose, "8", "0");
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
                                Match100Helper.insert_data("marathonbet", league, start_time, host, client, win, draw, lose, "1", "0");
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
                    string[] times = start_time.E_TRIM().E_SPLIT(M.D);
                    start_time = times[0].Substring(3, 2) + "-" + times[0].Substring(0, 2) + M.D + times[1];

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
                    Match100Helper.insert_data("coral", league, start_time, host, client, win, draw, lose, "8", "0");
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
                    Match100Helper.insert_data("gamebookers", league, start_time, host, client, win, draw, lose, "1", "0");

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
                                start_time = start_time.Replace(" ", M.D);
                                string[] times = start_time.E_TRIM().E_SPLIT(M.D);
                                start_time = times[0].Substring(3, 2) + "-" + times[0].Substring(0, 2) + M.D + times[1];
                                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                Match100Helper.insert_data("oddring", league, start_time, host, client, win, draw, lose, "8", "0");
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
                        string[] times = start_time.E_TRIM().E_SPLIT(M.D);
                        start_time = times[0].Substring(3, 2) + "-" + times[0].Substring(0, 2) + M.D + times[1];
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
                        win = node.SELECT_NODE("/td[2]/td[1]/a[1]/div[1]").InnerText.Replace(",", ".");
                        draw = node.SELECT_NODE("/td[2]/td[2]/a[1]/div[1]").InnerText.Replace(",", ".");
                        lose = node.SELECT_NODE("/td[2]/td[3]/a[1]/div[1]").InnerText.Replace(",", ".");

                        Match100Helper.insert_data("snai", league, start_time, host, client, win, draw, lose, "8", "0");
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
                    string str_odds = node.SELECT_NODE("/td[6]/div[1]").InnerHtml;
                    string[] odds = str_odds.E_SPLIT("<br>");
                    if (odds.Length == 3)
                    {
                        win = odds[0]; draw = odds[2]; lose = odds[1];
                    }
                    else
                    {
                        win = ""; draw = ""; lose = "";
                    }
                    if (!league.Contains("LeagueName") && !string.IsNullOrEmpty(win.Trim()) && !start_time.Contains("-"))
                    {
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("12bet", league, start_time, host, client, win, draw, lose, "8", "0");

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
                    start_time = node.SELECT_NODE("/td[1]/div[1]").InnerHtml.Replace("<br>", M.D);
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
                            Match100Helper.insert_data("1bet", league, start_time, host, client, win, draw, lose, "2", "0");
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
                    start_time = node.SELECT_NODE("tbody[1]/tr[1]/td[1]/span[1]/span[1]").InnerText + M.D + node.SELECT_NODE("tbody[1]/tr[1]/td[1]/span[1]/span[2]").InnerText;
                    host = node.SELECT_NODE("tbody[1]/tr[1]/td[2]/div[1]").InnerText;
                    client = node.SELECT_NODE("tbody[1]/tr[1]/td[2]/div[2]").InnerText;
                    win = node.SELECT_NODE("tbody[1]/tr[1]/td[3]").InnerText;
                    draw = node.SELECT_NODE("tbody[1]/tr[1]/td[4]").InnerText;
                    lose = node.SELECT_NODE("tbody[1]/tr[1]/td[5]").InnerText;

                    start_time = Tool.get_12m_from_eng(start_time.Substring(0, 3)) + "-" + start_time.Substring(3, start_time.Length - 3).E_TRIM();
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("youwin", league, start_time, host, client, win, draw, lose, "2", "0");
                }
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        #endregion


        #region  waiting function
        //2014-09-11
        public void test_youwage()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "youwage.html"));
            //==========================================================================================================
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
            int host_count = 0;
            int client_count = 0;
            foreach (HtmlNode node in nodes_all)
            {
                if (node.CLASS() == "sports-table")
                {
                    HtmlNodeCollection nodes_tr = node.SELECT_NODES("/tbody[1]/tr");
                    if (nodes_tr != null)
                    {
                        foreach (HtmlNode node_tr in nodes_tr)
                        {
                            if (node_tr.InnerText.Contains("SPREAD")) continue;
                            if (node_tr.SELECT_NODES("/td").Count == 1 && !node_tr.OuterHtml.Contains("end"))
                            {
                                start_time = node_tr.InnerText;
                                string[] times = start_time.E_SPLIT(" ");
                                start_time = times[0].E_TRIM().Substring(0, 5) + M.D + times[1].E_TRIM();
                            }
                            if (node_tr.SELECT_NODES("/td").Count == 5 && !node_tr.OuterHtml.Contains("Draw"))
                            {

                                if (host_count == client_count)
                                {
                                    host_count = host_count + 1;
                                    host = node_tr.SELECT_NODE("td[2]").InnerText;
                                    win = node_tr.SELECT_NODE("td[4]").InnerText;
                                    win = Match100Helper.convert_ameriaca_odd(win);
                                }
                                else
                                {
                                    client_count = client_count + 1;
                                    client = node_tr.SELECT_NODE("td[2]").InnerText;
                                    lose = node_tr.SELECT_NODE("td[4]").InnerText;
                                    lose = Match100Helper.convert_ameriaca_odd(lose);

                                }

                            }
                            if (node_tr.SELECT_NODES("/td").Count == 5 && node_tr.OuterHtml.Contains("Draw"))
                            {
                                draw = node_tr.SELECT_NODE("td[4]").InnerText;
                                draw = Match100Helper.convert_ameriaca_odd(draw);
                                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                Match100Helper.insert_data("youwage", league, start_time, host, client, win, draw, lose, "8", "0");

                            }


                        }
                    }
                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_paddypower()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "paddypower.html"));
            //==========================================================================================================
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
                if (node.Name == "h2")
                {
                    if (node.OuterHtml.ToLower().Contains("span"))
                    {
                        date = node.SELECT_NODE("/span[1]").InnerText;
                        string[] dates = date.E_SPLIT(" ");

                        date = Tool.get_12m_from_eng(dates[2].Substring(0, 3)) + "-" + dates[0];
                    }

                }
                if (node.CLASS().E_TRIM() == "simple_pp_fb_event")
                {
                    time = node.SELECT_NODE("/div[1]").InnerText;


                    string str_teams = node.SELECT_NODE("/div[2]").InnerText;
                    string[] teams = str_teams.E_SPLIT(" v ");
                    host = teams[0];
                    client = teams[1];

                    win = node.SELECT_NODE("/a[2]").InnerText.Replace("evens", "1/1"); ;
                    draw = node.SELECT_NODE("/a[3]").InnerText.Replace("evens", "1/1");
                    lose = node.SELECT_NODE("/a[4]").InnerText.Replace("evens", "1/1");


                    start_time = date + M.D + time;
                    win = Match100Helper.convert_english_odd(win);
                    draw = Match100Helper.convert_english_odd(draw);
                    lose = Match100Helper.convert_english_odd(lose);

                    if (!start_time.ToLower().Contains("live"))
                    {
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("paddypowser", league, start_time, host, client, win, draw, lose, "8", "0");
                    }

                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_vwin()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "vwin.html"));
            //==========================================================================================================
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
                    if (node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 3 && !node.InnerHtml.Contains("tr"))
                    {
                        league = node.SELECT_NODE("/td[2]").InnerText;
                    }
                    if (node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 10 && !node.InnerHtml.Contains("tr"))
                    {
                        HtmlNode node_time = node.SELECT_NODE("/td[1]");
                        if (node_time.ChildNodes.Count == 3)
                        {
                            start_time = node_time.ChildNodes[1].InnerText + M.D + node_time.ChildNodes[2].InnerText;
                            host = node.SELECT_NODE("/td[2]/div[1]").InnerText;
                            client = node.SELECT_NODE("/td[2]/div[2]").InnerText;
                            win = node.SELECT_NODE("/td[4]").InnerText;
                            draw = node.SELECT_NODE("/td[5]").InnerText;
                            lose = node.SELECT_NODE("/td[6]").InnerText;
                            sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                            Match100Helper.insert_data("vwin", league, start_time, host, client, win, draw, lose, "8", "0");
                        }
                    }
                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_betrally()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "betrally.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "time")
                {
                    start_time = node.InnerText;
                    string[] times = start_time.E_TRIM().E_SPLIT("|");
                    if (times.Length == 2)
                    {
                        start_time = times[0].Substring(3, 2) + "-" + times[0].Substring(0, 2) + M.D + times[1];
                    }
                    start_time = start_time.E_TRIM();
                }
                if (node.CLASS() == "bets ml")
                {
                    host = node.SELECT_NODE("/ul[1]/li[1]/dl[1]/dt[1]/span[1]").InnerText;
                    client = node.SELECT_NODE("/ul[1]/li[1]/dl[1]/dt[1]/span[2]").InnerText;

                    win = node.SELECT_NODE("/ul[1]/li[1]/dl[1]/dd[1]/ul[1]/li[1]").InnerText;
                    draw = node.SELECT_NODE("/ul[1]/li[1]/dl[1]/dd[1]/ul[1]/li[2]").InnerText;
                    lose = node.SELECT_NODE("/ul[1]/li[1]/dl[1]/dd[1]/ul[1]/li[3]").InnerText;
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("betrally", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_18bet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "18bet.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "col1")
                {
                    league = node.SELECT_NODE("/div[2]").InnerText;
                }
                if (node.CLASS() == "game_date")
                {
                    start_time = node.InnerText.E_TRIM();
                }
                if (node.CLASS() == "game_text")
                {
                    host = node.SELECT_NODE("/p[1]").InnerText;
                    client = node.SELECT_NODE("/p[2]").InnerText;
                }
                if (node.CLASS() == "fulltime")
                {
                    win = node.SELECT_NODE("/div[1]/p[1]").InnerText;
                    draw = node.SELECT_NODE("/div[1]/p[2]").InnerText;
                    lose = node.SELECT_NODE("/div[1]/p[3]").InnerText;
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("18bet", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_mcbookie()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "mcbookie.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "body")
                {
                    if (node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 6)
                    {
                        start_time = node.SELECT_NODE("/td[1]").Attributes["data-sort"].Value.ToString().Substring(0, 19);
                        DateTime dt_start = Convert.ToDateTime(start_time);
                        dt_start = dt_start.AddHours(6);
                        start_time = dt_start.ToString("MM-dd") + M.D + dt_start.ToString("hh:mm");

                        string str_teams = node.SELECT_NODE("/td[2]/a[1]").InnerText;
                        string[] teams = str_teams.E_SPLIT(" v ");
                        host = teams[0]; client = teams[1].Replace("<!--IE fix-->", "");

                        league = node.SELECT_NODE("/td[2]/span[1]").InnerText;

                        win = node.SELECT_NODE("td[3]/span[1]/a[1]/span[1]").InnerText;
                        draw = node.SELECT_NODE("td[4]/span[1]/a[1]/span[1]").InnerText;
                        lose = node.SELECT_NODE("td[5]/span[1]/a[1]/span[1]").InnerText;

                        win = Match100Helper.convert_english_odd(win);
                        draw = Match100Helper.convert_english_odd(draw);
                        lose = Match100Helper.convert_english_odd(lose);
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("mcbookie", league, start_time, host, client, win, draw, lose, "8", "0");
                    }

                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_betcenter()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "betcenter.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "black-row")
                {
                    start_time = node.InnerText.E_TRIM();
                    start_time = start_time.Substring(3, 2) + "-" + start_time.Substring(0, 2) + M.D + start_time.Substring(10, 5);
                }
                if (node.CLASS() == "odd")
                {
                    league = node.SELECT_NODE("/td[1]/span[1]").Attributes["title"].Value;

                    string[] teams = node.SELECT_NODE("/td[3]").InnerText.E_SPLIT(" - ");
                    host = teams[0]; client = teams[1];

                    win = node.SELECT_NODE("/td[4]").InnerText.Replace(",", ".");
                    draw = node.SELECT_NODE("/td[5]").InnerText.Replace(",", ".");
                    lose = node.SELECT_NODE("/td[6]").InnerText.Replace(",", ".");
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("betcenter", league, start_time, host, client, win, draw, lose, "8", "0");

                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_joinbet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "joinbet.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "coupdate")
                {
                    string[] dates = node.InnerText.E_SPLIT(" ");
                    date = Tool.get_12m_from_eng(dates[2]) + "-" + dates[1];
                }
                if (node.Name == "tr" && node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 10 && !node.InnerText.Contains("TIME"))
                {
                    start_time = date + M.D + node.SELECT_NODE("/td[2]").InnerText;
                    league = node.SELECT_NODE("/td[1]").InnerText;
                    host = node.SELECT_NODE("/td[6]").InnerText;
                    client = node.SELECT_NODE("td[8]").InnerText;
                    win = node.SELECT_NODE("td[5]").InnerText;
                    draw = node.SELECT_NODE("td[7]").InnerText;
                    lose = node.SELECT_NODE("td[9]").InnerText;
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("joinbet", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_tonybet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "tonybet.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "events singleRow")
                {
                    HtmlNodeCollection nodes_tr = node.SELECT_NODES("/tbody[1]/tr");
                    if (nodes_tr != null)
                    {
                        foreach (HtmlNode node_tr in nodes_tr)
                        {
                            if (node_tr.SELECT_NODES("th") != null && node_tr.SELECT_NODES("th").Count == 5)
                            {
                                HtmlNode test = node_tr.SELECT_NODE("/th[1]");
                                league = node_tr.SELECT_NODE("/th[1]").InnerText.Replace("\r\n", "").Replace("                                  ", "").Replace("                                     ", "");

                            }
                            if (node_tr.SELECT_NODES("td") != null && node_tr.SELECT_NODES("td").Count == 8)
                            {
                                string[] times = node_tr.SELECT_NODE("/td[1]").ChildNodes[2].InnerText.E_SPLIT(" ");
                                if (times.Length == 4)
                                {
                                    start_time = Tool.get_12m_from_eng(times[1]) + "-" + times[2] + M.D + times[3];
                                }
                                else
                                {
                                    start_time = node_tr.SELECT_NODE("/td[1]").ChildNodes[2].InnerText.E_TRIM();
                                }
                                start_time = start_time.E_TRIM();


                                string[] teams = node_tr.SELECT_NODE("/td[2]").InnerText.E_SPLIT(" - ");
                                host = teams[0].Replace("\r\n                  ", ""); client = teams[1].Replace("\r\n                  ", "");

                                win = node_tr.SELECT_NODE("/td[3]").InnerText.E_TRIM();
                                draw = node_tr.SELECT_NODE("/td[4]").InnerText.E_TRIM();
                                lose = node_tr.SELECT_NODE("/td[5]").InnerText.E_TRIM();

                                if (!start_time.Contains("min"))
                                {
                                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                    Match100Helper.insert_data("tonybet", league, start_time, host, client, win, draw, lose, "8", "0");
                                }
                            }
                        }
                    }

                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_topsport()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "topsport.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "MatchGroup")
                {
                    league = node.InnerText.Replace("Refresh", "");
                }
                if (node.CLASS() == "MatchOutcomeAt")
                {
                    string[] times = node.InnerText.E_SPLIT(" ");
                    if (times.Length == 4)
                    {
                        start_time = Tool.get_12m_from_eng(times[1].Substring(3, 3)) + "-" + times[1].Substring(0, 2) + M.D + times[3];
                    }
                }
                if (node.CLASS() == "MatchMarket")
                {
                    host = node.SELECT_NODE("/tbody[1]/tr[1]/th[1]").InnerText;
                    win = node.SELECT_NODE("/tbody[1]/tr[1]/td[1]/a[1]").InnerText;
                    draw = node.SELECT_NODE("/tbody[1]/tr[1]/td[2]/a[1]").InnerText;
                    client = node.SELECT_NODE("/tbody[1]/tr[2]/th[1]").InnerText;
                    lose = node.SELECT_NODE("/tbody[1]/tr[2]/td[1]/a[1]").InnerText;
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("topsport", league, start_time, host, client, win, draw, lose, "8", "0");
                }

            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }


        //2014-09-15
        public void test_apollobet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "apollobet.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "evenrow")
                {
                    if (node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 3)
                    {
                        start_time = "" + M.D + time;
                        time = node.SELECT_NODE("/td[1]").InnerText;
                        string[] teams = node.SELECT_NODE("/td[2]").InnerText.E_SPLIT(" v ");
                        host = teams[0]; client = teams[1];

                        win = node.SELECT_NODE("td[3]/table[1]/tbody[1]/tr[1]/td[2]/label[1]/input[1]").Attributes["value"].Value.Replace("EVS", "1/1");
                        draw = node.SELECT_NODE("td[3]/table[1]/tbody[1]/tr[1]/td[3]/label[1]/input[1]").Attributes["value"].Value.Replace("EVS", "1/1");
                        lose = node.SELECT_NODE("td[3]/table[1]/tbody[1]/tr[1]/td[4]/label[1]/input[1]").Attributes["value"].Value.Replace("EVS", "1/1");

                        win = Match100Helper.convert_english_odd(win);
                        draw = Match100Helper.convert_english_odd(draw);
                        lose = Match100Helper.convert_english_odd(lose);
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("apollobet", league, start_time, host, client, win, draw, lose, "8", "0");
                    }
                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_boylesports()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "boylesports.html"));
            //==========================================================================================================
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
                if (node.Name == "tr" && node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 6)
                {
                    string[] dates = node.SELECT_NODE("/td[1]").InnerText.Replace("\r\n", "").E_SPLIT(" ");
                    if (dates.Length == 2)
                    {
                        date = Tool.get_12m_from_eng(dates[1]) + "-" + dates[0];
                    }
                    else
                    {
                        date = "";
                    }

                }
                if (node.Name == "tr" && node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 8)
                {
                    time = node.SELECT_NODE("/td[1]").InnerText.E_TRIM();
                    start_time = date + M.D + time;
                    if (start_time.Length > 20) start_time = start_time.Substring(0, 20);

                    string[] teams = node.SELECT_NODE("/td[2]").InnerText.E_SPLIT(" v ");
                    host = teams[0].E_REMOVE(); client = teams[1].E_REMOVE();

                    win = node.SELECT_NODE("td[3]").InnerText.E_TRIM().Replace("EVS", "1/1");
                    draw = node.SELECT_NODE("td[4]").InnerText.E_TRIM().Replace("EVS", "1/1");
                    lose = node.SELECT_NODE("td[5]").InnerText.E_TRIM().Replace("EVS", "1/1");

                    win = Match100Helper.convert_english_odd(win);
                    draw = Match100Helper.convert_english_odd(draw);
                    lose = Match100Helper.convert_english_odd(lose);
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("boylesports", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_efbet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "efbet.html"));
            //==========================================================================================================
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
                if (node.Name == "tr" && node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 6)
                {
                    string[] times = node.SELECT_NODE("/td[1]").InnerHtml.E_SPLIT(" ");
                    start_time = Tool.get_12m_from_eng(times[1]) + "-" + times[0] + M.D + times[3];

                    string[] teams = node.SELECT_NODE("/td[2]").InnerText.E_SPLIT(" - ");
                    host = teams[0]; client = teams[1];

                    win = node.SELECT_NODE("/td[3]").InnerText;
                    draw = node.SELECT_NODE("/td[4]").InnerText;
                    lose = node.SELECT_NODE("/td[5]").InnerText;
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("efbet", league, start_time, host, client, win, draw, lose, "8", "0");  
                }

            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_betbright()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "betbright.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "match_table_header")
                {
                    league = node.SELECT_NODE("/th[2]").InnerText;

                }
                if (node.Id.Contains("event_") && node.Name == "tr")
                {
                    start_time = node.SELECT_NODE("/td[1]").InnerText;
                    string[] teams = node.SELECT_NODE("/td[2]").InnerText.E_SPLIT(" v ");
                    host = teams[0].E_REMOVE(); client = teams[1].E_REMOVE();

                    try
                    {
                        win = node.SELECT_NODE("/td[4]").InnerText.E_TRIM();
                        draw = node.SELECT_NODE("/td[5]").InnerText.E_TRIM();
                        lose = node.SELECT_NODE("/td[6]").InnerText.E_TRIM();
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("betbright", league, start_time, host, client, win, draw, lose, "8", "0");
                    }
                    catch (Exception error) { }

                }

            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_leonbets()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "leonbets.html"));
            //==========================================================================================================
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
                if (node.Name == "h1")
                {
                    league = node.InnerText.E_REMOVE();
                }
                if (node.Name == "tr" && node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 6)
                {
                    HtmlNode node_test = node.SELECT_NODE("/td[1]");
                    string[] times = node.SELECT_NODE("/td[1]").ChildNodes[2].InnerText.E_REMOVE().E_SPLIT(" ");
                    start_time = times[0].Substring(3, 2) + "-" + times[0].Substring(0, 2) + M.D + times[1];

                    string[] teams = node.SELECT_NODE("/td[2]").InnerText.E_SPLIT(" - ");
                    host = teams[0]; client = teams[1];

                    win = node.SELECT_NODE("/td[3]/a[1]/strong[1]").InnerText;
                    draw = node.SELECT_NODE("/td[4]/a[1]/strong[1]").InnerText;
                    lose = node.SELECT_NODE("/td[5]/a[1]/strong[1]").InnerText;
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("leonbets", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();

        }
        public void test_betadria()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "betadria.html"));
            //==========================================================================================================
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

                if (node.Name == "tr" && node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 4)
                {

                    HtmlNode node_first = node.SELECT_NODE("/td[1]");
                    if (node_first.ChildNodes.Count == 2 && !string.IsNullOrEmpty(node_first.InnerText) && !node.InnerText.Contains("1X2"))
                    {

                        string[] times = node.SELECT_NODE("/td[1]").ChildNodes[1].InnerText.E_SPLIT(" ");
                        if (times.Length == 2)
                        {
                            start_time = times[0].Substring(3, 2) + "-" + times[0].Substring(0, 2) + M.D + times[1];
                        }
                        else
                        {
                            start_time = node.SELECT_NODE("/td[1]").ChildNodes[1].InnerText;
                        }


                        string[] teams = node.SELECT_NODE("/td[1]").ChildNodes[0].InnerText.E_SPLIT(" - ");
                        host = teams[0]; client = teams[1];

                        win = node.SELECT_NODE("/td[2]").InnerText.Replace(",", ".");
                        draw = node.SELECT_NODE("/td[3]").InnerText.Replace(",", ".");
                        lose = node.SELECT_NODE("/td[4]").InnerText.Replace(",", ".");

                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("betadria", league, start_time, host, client, win, draw, lose, "8", "0");
                    }

                }


                if (node.Name == "tr" && node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 10 && !node.InnerText.Contains("Under"))
                {
                    string[] times = node.SELECT_NODE("/td[1]").ChildNodes[1].InnerText.E_SPLIT(" ");
                    if (times.Length == 2)
                    {
                        start_time = times[0].Substring(3, 2) + "-" + times[0].Substring(0, 2) + M.D + times[1];
                    }
                    else
                    {
                        start_time = node.SELECT_NODE("/td[1]").ChildNodes[1].InnerText;
                    }


                    string[] teams = node.SELECT_NODE("/td[1]").ChildNodes[0].InnerText.E_SPLIT(" - ");
                    host = teams[0]; client = teams[1];

                    win = node.SELECT_NODE("/td[2]").InnerText.Replace(",", ".");
                    draw = node.SELECT_NODE("/td[3]").InnerText.Replace(",", ".");
                    lose = node.SELECT_NODE("/td[4]").InnerText.Replace(",", ".");

                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("betadria", league, start_time, host, client, win, draw, lose, "8", "0");
                }


            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_betin()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "betin.html"));
            //==========================================================================================================
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
                if (node.Name == "h2")
                {
                    league = node.InnerText;
                }
                if (node.Name == "tr" && node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 10)
                {
                    date = node.Attributes["dt"].Value;
                    time = node.SELECT_NODE("/td[1]").InnerText;
                    start_time = date.Substring(3, 2) + "-" + date.Substring(0, 2) + M.D + time;

                    string[] teams = node.SELECT_NODE("/td[2]/div[1]/a[1]").InnerText.E_SPLIT(" v ");
                    host = teams[0]; client = teams[1];

                    win = node.SELECT_NODE("/td[3]").InnerText;
                    draw = node.SELECT_NODE("/td[4]").InnerText;
                    lose = node.SELECT_NODE("/td[5]").InnerText;

                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("betin", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //===============================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }

        //2014-09-16
        public void test_pinnaclesports_index()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "pinnaclesports_index.html"));
            //==========================================================================================================
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
                        if (str_class == "Soccer" && !name.Contains("Halfs") && !name.Contains("Totals"))
                        {
                            count = count + 1;
                            sb.AppendLine(count.PR(5) + str_class.PR(20) + name.PR(50) + href);
                        }
                    }
                }
            }
            //===================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }

        //2014-09-18
        public void test_wilsonbet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "wilsonbet.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "boxheader")
                {
                    date = node.InnerText;
                    string[] dates = date.E_SPLIT(" ");
                    if (dates.Length == 5)
                    {
                        date = Tool.get_12m_from_eng(dates[3]) + "-" + dates[1];
                    }
                }
                if (node.Name == "tr" && node.SELECT_NODES("/td") != null && node.SELECT_NODES("/td").Count == 6 && !node.InnerText.Contains("Time") && !node.InnerText.Contains("Username"))
                {
                    time = node.SELECT_NODE("/td[1]").InnerText;
                    start_time = date + M.D + time;

                    string[] teams = node.SELECT_NODE("/td[2]").InnerText.E_SPLIT(" v ");
                    if (teams.Length == 2) { host = teams[0]; client = teams[1]; }

                    win = node.SELECT_NODE("/td[3]/a[1]").InnerText.E_REMOVE().Replace("evens", "1/1");
                    draw = node.SELECT_NODE("/td[4]/a[1]").InnerText.E_REMOVE().Replace("evens", "1/1");
                    lose = node.SELECT_NODE("/td[5]/a[1]").InnerText.E_REMOVE().Replace("evens", "1/1");
                    win = Match100Helper.convert_english_odd(win);
                    draw = Match100Helper.convert_english_odd(draw);
                    lose = Match100Helper.convert_english_odd(lose);
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("wilsonbet", league, start_time, host, client, win, draw, lose, "8", "0");
                }

            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_sportsbet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "sportsbet.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "sort-by-date")
                {
                    string[] dates = node.InnerText.E_SPLIT(" ");
                    date = dates[1].Substring(3, 2) + "-" + dates[1].Substring(0, 2);
                }
                if (node.Name == "li" && node.Id.Contains("match"))
                {
                    time = node.SELECT_NODE("/div[1]/div[1]").InnerText;
                    start_time = date + M.D + time;
                    host = node.SELECT_NODE("/div[2]/div[1]/div[1]/a[1]/span[1]").InnerText;
                    client = node.SELECT_NODE("/div[2]/div[1]/div[3]/a[1]/span[1]").InnerText;

                    win = node.SELECT_NODE("/div[2]/div[1]/div[1]/a[1]/span[2]").InnerText.E_REMOVE();
                    draw = node.SELECT_NODE("/div[2]/div[1]/div[2]/a[1]/span[2]").InnerText.E_REMOVE();
                    lose = node.SELECT_NODE("/div[2]/div[1]/div[3]/a[1]/span[2]").InnerText.E_REMOVE();

                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("sportsbet", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_intertops()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "intertops.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "spoftitleslip")
                {
                    string[] teams = node.SELECT_NODE("/tbody[1]/tr[1]/td[1]").InnerText.E_REMOVE().E_SPLIT(" v ");
                    host = teams[0].Replace("&amp;", ""); client = teams[1].Replace("&amp;", "");

                    string[] times = node.SELECT_NODE("tbody[1]/tr[1]/td[2]").InnerText.E_REMOVE().E_SPLIT(" ");
                    start_time = Tool.get_12m_from_eng(times[0]) + "-" + times[1] + M.D + times[3];
                }
                if (node.CLASS() == "spofslip")
                {
                    win = node.SELECT_NODE("/tbody[1]/tr[1]/td[2]/b[1]").InnerText;
                    draw = node.SELECT_NODE("/tbody[1]/tr[1]/td[4]/b[1]").InnerText;
                    lose = node.SELECT_NODE("/tbody[1]/tr[1]/td[6]/b[1]").InnerText;
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("intertops", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_pinnaclesports()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "pinnaclesports.html"));
            //==========================================================================================================
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
            HtmlNode node_host;
            HtmlNode node_client;
            HtmlNode node_draw;
            foreach (HtmlNode node in nodes_all)
            {
                if (node.CLASS() == "linesTbl")
                {

                    HtmlNodeCollection nodes_tr = node.SELECT_NODES("/tbody[1]/tr");
                    if (nodes_tr != null)
                    {
                        league = nodes_tr[0].InnerText.E_REMOVE();


                        int count = 0;
                        for (int i = 2; i < nodes_tr.Count; i++)
                        {
                            count = count + 1;
                            if (count == 2)
                            {
                                if (i + 1 < nodes_tr.Count && nodes_tr[i + 1].SELECT_NODE("/td[3]").InnerText == "Draw")
                                {
                                    node_host = nodes_tr[i - 1];
                                    node_client = nodes_tr[i];
                                    node_draw = nodes_tr[i + 1];
                                    count = 0;
                                    i = i + 1;
                                    date = node_host.SELECT_NODE("/td[1]").InnerText;
                                    time = node_client.SELECT_NODE("/td[1]").InnerText;
                                    start_time = date + M.D + time;

                                    host = node_host.SELECT_NODE("/td[3]").InnerText;
                                    client = node_client.SELECT_NODE("/td[3]").InnerText;

                                    win = node_host.SELECT_NODE("/td[6]").InnerText.Replace("&nbsp;", "");
                                    draw = node_draw.SELECT_NODE("/td[6]").InnerText.Replace("&nbsp;", "");
                                    lose = node_client.SELECT_NODE("/td[6]").InnerText.Replace("&nbsp;", "");
                                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                    // Match100Helper.insert_data("pinnaclesports", league, start_time, host, client, win, draw, lose, "-7", "0");Match100Helper.insert_data("pinnaclesports", league, start_time, host, client, win, draw, lose, "-7", "0");
                                }
                                if (i + 1 < nodes_tr.Count && nodes_tr[i + 1].SELECT_NODE("/td[3]").InnerText != "Draw")
                                {
                                    node_host = nodes_tr[i - 1];
                                    node_client = nodes_tr[i];
                                    count = 0;
                                }
                                if (i + 1 == nodes_tr.Count)
                                {
                                    node_host = nodes_tr[i - 1];
                                    node_client = nodes_tr[i];
                                }
                            }


                        }
                    }
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }


        //2014-09-22
        public void test_666bet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "666bet.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "upcoming block")
                {
                    league = node.SELECT_NODE("/h2").InnerText;

                }
                if (node.CLASS() == "bet_row")
                {
                    start_time = node.SELECT_NODE("/div[1]").InnerText;
                    host = node.SELECT_NODE("/div[2]/a[1]/div[1]").ChildNodes[0].InnerText;
                    win = node.SELECT_NODE("/div[2]/a[1]/div[1]").ChildNodes[1].InnerText;
                    draw = node.SELECT_NODE("/div[2]/a[2]/div[1]/div[1]").InnerText;
                    client = node.SELECT_NODE("/div[2]/a[3]/div[1]").ChildNodes[0].InnerText;
                    lose = node.SELECT_NODE("/div[2]/a[3]/div[1]").ChildNodes[1].InnerText;
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("666bet", league, start_time, host, client, win, draw, lose, "8", "0");
                }

            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_betfair_es()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "betfair_es.html"));
            //==========================================================================================================
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
                if (node.CLASS() == "container-market")
                {
                    start_time = node.SELECT_NODE("/a[1]/div[1]/div[2]").InnerText;
                    host = node.SELECT_NODE("/a[1]/div[1]/div[1]/span[1]/span[1]").InnerText;
                    client = node.SELECT_NODE("/a[1]/div[1]/div[1]/span[1]/span[3]").InnerText;

                    win = node.SELECT_NODE("div[1]/ul[1]/li[1]/ul[1]/li[1]").InnerText.E_TRIM();
                    draw = node.SELECT_NODE("div[1]/ul[1]/li[1]/ul[1]/li[2]").InnerText.E_TRIM();
                    lose = node.SELECT_NODE("div[1]/ul[1]/li[1]/ul[1]/li[3]").InnerText.E_TRIM();
                    if (!start_time.Contains("Starting"))
                    {
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("betfair_es", league, start_time, host, client, win, draw, lose, "8", "0");
                    }
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_championsbet()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "championsbet.html"));
            //==========================================================================================================
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
                if (node.Name == "td" && node.CLASS() == "TdDate")
                {
                    date = Tool.get_12m_from_eng(node.InnerText.E_SPLIT(" ")[2]) + "-" + node.InnerText.E_SPLIT(" ")[1];
                }
                if (node.CLASS().Contains("TblByLeagues_Odds"))
                {
                    league = node.SELECT_NODE("/td[6]").Attributes["title"].Value;
                    start_time = date + M.D + node.SELECT_NODE("/td[5]").InnerText;

                    string[] teams = node.SELECT_NODE("/td[8]").InnerText.E_SPLIT(" - ");
                    host = teams[0]; client = teams[1];

                    win = node.SELECT_NODE("/td[9]").InnerText;
                    draw = node.SELECT_NODE("/td[10]").InnerText;
                    lose = node.SELECT_NODE("/td[11]").InnerText;

                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("champoinsbet", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }

        //2014-09-23
        public void test_betfair_es_index()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "betfair_es.html"));
            //==========================================================================================================
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
                if (node.Name == "a" && node.InnerText.Contains("Fixtures") && node.CLASS().Contains("child"))
                {
                    string url = node.Attributes["href"].Value;
                    sb.AppendLine(url);
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        #endregion

        //2013-10-20
        public void test_pinnaclesports_me_index()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "pinnaclesports_me_index.html"));
            //==========================================================================================================
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
            string str_class = "";
            string name = "";
            string href = "";
            int count = 0;
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Name == "h4")
                {
                    str_class = node.InnerText;
                }
                if (str_class == "Soccer")
                {

                    if (node.Name == "a" && node.Attributes.Contains("href") && node.Attributes["href"].Value.Contains("Soccer"))
                    {
                        name = node.InnerText.E_REMOVE();
                        href = node.Attributes["href"].Value.ToString();
                        if (!name.Contains("Half") && !name.Contains("Total") && !name.Contains("Corners") && !name.Contains(" vs ") && !name.Contains("Live") && !href.Contains("Soccer+Props"))
                        {
                            if (!sb.ToString().Contains(href))
                            {
                                count = count + 1;
                                sb.AppendLine(count.PR(5) + name.PR(50) + href);
                            }
                        }
                    }

                }

            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_pinnaclesports_me()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "pinnaclesports_me.html"));
            //==========================================================================================================
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
            HtmlNode node_host;
            HtmlNode node_client;
            HtmlNode node_draw;
            foreach (HtmlNode node in nodes_all)
            { 
                if (node.CLASS() == "linesTbl")
                {

                    HtmlNodeCollection nodes_tr = node.SELECT_NODES("/tbody[1]/tr");
                    if (nodes_tr != null)
                    {
                        league = nodes_tr[0].InnerText.E_REMOVE();


                        int count = 0;
                        for (int i = 2; i < nodes_tr.Count; i++)
                        {
                            count = count + 1;
                            if (count == 2)
                            {
                                if (i + 1 < nodes_tr.Count && nodes_tr[i + 1].SELECT_NODE("/td[3]") != null && nodes_tr[i + 1].SELECT_NODE("/td[3]").InnerText.Contains("Draw"))
                                {
                                    string test = "200";
                                    node_host = nodes_tr[i - 1];
                                    node_client = nodes_tr[i];
                                    node_draw = nodes_tr[i + 1];
                                    count = 0;
                                    i = i + 1;
                                    date = node_host.SELECT_NODE("/td[1]").InnerText;
                                    time = node_client.SELECT_NODE("/td[1]").InnerText;
                                    start_time = date + M.D + time;

                                    host = node_host.SELECT_NODE("/td[3]").InnerText;
                                    client = node_client.SELECT_NODE("/td[3]").InnerText;

                                    win = node_host.SELECT_NODE("/td[5]").InnerText.Replace("o", "").Replace("&nbsp;", "").E_TRIM();
                                    draw = node_draw.SELECT_NODE("/td[5]").InnerText.Replace("o", "").Replace("&nbsp;", "").E_TRIM();
                                    lose = node_client.SELECT_NODE("/td[5]").InnerText.Replace("o", "").Replace("&nbsp;", "").E_TRIM();
                                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                    // Match100Helper.insert_data("pinnaclesports", league, start_time, host, client, win, draw, lose, "-7", "0");Match100Helper.insert_data("pinnaclesports", league, start_time, host, client, win, draw, lose, "-7", "0");
                                }
                                if (i + 1 < nodes_tr.Count && nodes_tr[i + 1].SELECT_NODE("/td[3]") != null && nodes_tr[i + 1].SELECT_NODE("/td[3]").InnerText != "Draw")
                                {
                                    node_host = nodes_tr[i - 1];
                                    node_client = nodes_tr[i];
                                    count = 0;
                                }
                                if (i + 1 == nodes_tr.Count)
                                {
                                    node_host = nodes_tr[i - 1];
                                    node_client = nodes_tr[i];
                                }
                            }


                        }
                    }
                }

            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }


        public void test_gobetgo_index_language()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "gobetgo_index.html"));
            //==========================================================================================================
            html = html.Replace("<thead=\"\"", "");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");

            foreach (HtmlNode node in nodes_all)
            {
                if (node.Name == "dd" && node.ChildNodes.Count == 2 && node.ChildNodes[1].InnerText == "English")
                {
                    sb.AppendLine(node.OuterHtml);
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_gobetgo_index_click()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "gobetgo_index.html"));
            //==========================================================================================================
            html = html.Replace("<thead=\"\"", "");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");

            foreach (HtmlNode node in nodes_all)
            {
                if (node.Name == "a" && node.SELECT_NODE("/span[1]") != null && node.SELECT_NODE("span[1]").InnerText == "SOCCER")
                {
                    sb.AppendLine(node.OuterHtml);
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_gobetgo_index()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "gobetgo_index.html"));
            //==========================================================================================================
            html = html.Replace("<thead=\"\"", "");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*"); 

            foreach (HtmlNode node in nodes_all)
            { 
                if (node.Name == "a" && node.CLASS() == "item_submenu_link")
                { 
                    sb.AppendLine(node.InnerText); 
                } 
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }
        public void test_gobetgo()
        {

            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_sites + "gobetgo_detail.html"));
            //==========================================================================================================
            html = html.Replace("<thead=\"\"", "");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
             

            ;
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

                if (node.CLASS() == "eventsContainer BettingContent")
                {
                    league = node.SELECT_NODE("/div[1]").InnerText;

                }
                if (node.CLASS() == "eventstable")
                {
                    HtmlNodeCollection node_trs = node.SELECT_NODES("/table/tbody/tr");
                    if (node_trs.Count > 0)
                    {
                        foreach (HtmlNode node_tr in node_trs)
                        {
                            date = node_tr.SELECT_NODE("/td[1]/header[1]/div[2]").ChildNodes[0].InnerText.E_SPLIT("/")[0] +"-"+ node_tr.SELECT_NODE("/td[1]/header[1]/div[2]").ChildNodes[0].InnerText.E_SPLIT("/")[1];
                            time = node_tr.SELECT_NODE("/td[1]/header[1]/div[2]").ChildNodes[1].InnerText;
                            start_time = date + M.D + time;
                            host = node_tr.SELECT_NODE("/td[1]/header[1]/div[1]/span[1]").InnerText;
                            client = node_tr.SELECT_NODE("/td[1]/header[1]/div[1]/span[2]").InnerText;
                            win = node_tr.SELECT_NODE("/td[1]/div[1]/div[1]/span[2]").InnerText;
                            draw = node_tr.SELECT_NODE("/td[1]/div[2]/div[1]/span[2]").InnerText;
                            lose = node_tr.SELECT_NODE("/td[1]/div[3]/div[1]/span[2]").InnerText;
                            if (!string.IsNullOrEmpty(win.E_TRIM()))
                            {
                                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                //Match100Helper.insert_data("gobetgo", league, start_time, host, client, win, draw, lose, "8", "0");
                            }
                        } 
                    } 
                }
            }
            //========================================================================================================
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();


        }


    }
}
