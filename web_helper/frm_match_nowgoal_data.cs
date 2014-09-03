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
using System.Xml;
using System.Xml.XPath;

namespace web_helper
{
    public partial class frm_match_nowgoal_data : Form
    {
        public static string root_path_nowgoal = Environment.CurrentDirectory.Replace(@"bin\Debug", "").Replace(@"bin\x86\Debug", "") + @"data\nowgoal\";
        string root_url_nowgoal = @"file:///" + root_path_nowgoal.Replace(@"\", @"/");
        StringBuilder sb = new StringBuilder();
        public frm_match_nowgoal_data()
        {
            InitializeComponent();
        }


        private void frm_match_nowgoal_data_Load(object sender, EventArgs e)
        {

        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }
        private void btn_live_score_Click(object sender, EventArgs e)
        {
            get_live_score();
        }
        private void btn_odd_1X2_single_company_Click(object sender, EventArgs e)
        {
            get_odd_1X2_single_company();
        } 
        private void btn_odd_1X2_single_match_Click(object sender, EventArgs e)
        {
            get_odd_1X2_single_match();
        }
        private void btn_league_all_Click(object sender, EventArgs e)
        {
            get_league_all();
        }
        private void btn_league_fixtures_standings_Click(object sender, EventArgs e)
        {
            get_league_fixtures_standings();
        }

        public void get_live_score()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_nowgoal + "livescore.html"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);


            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");

            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath;
                if (node.Name == "tr")
                {
                    HtmlNodeCollection nodes_td = doc.DocumentNode.SelectNodes(xpath + "/td");
                    if (nodes_td.Count == 9)
                    {
                        string league = "";
                        string start_time = "";
                        string host = "";
                        string client = "";
                        string host_point = "0";
                        string client_point = "0";
                        string points = "";

                        league = doc.DocumentNode.SelectSingleNode(xpath + "/td[2]").InnerText;
                        if (league == "Contest") continue;
                        start_time = doc.DocumentNode.SelectSingleNode(xpath + "/td[3]").InnerText;
                        host = doc.DocumentNode.SelectSingleNode(xpath + "/td[5]/a[3]").InnerText;
                        client = doc.DocumentNode.SelectSingleNode(xpath + "/td[7]/a[1]").InnerText;
                        points = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]").InnerText;

                        string[] list_point = points.ToString().Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (list_point.Length > 1)
                        {
                            host_point = list_point[0].ToString().Trim();
                            client_point = list_point[1].ToString().Trim();
                        }

                        if (league.Contains("off")) league = league.Replace("off", " [off]");
                        sb.AppendLine(league.PR(20) + start_time.PR(20) + host.PR(30) + client.PR(30) + (host_point + "  -  " + client_point).PR(10));
                    }

                }

            }
            this.txt_result.Text = sb.ToString();

        }
        public void get_odd_1X2_single_company()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_nowgoal + "odd_1X2_single_company.html"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);


            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");

            string league = "";
            string start_time = "";
            string host = "";
            string client = "";
            string odd_win = "";
            string odd_draw = "";
            string odd_lose = "";
            string persent_win = "";
            string persent_draw = "";
            string persent_lose = "";
            string persent_return = "";
            string start_odd_win = "";
            string start_odd_draw = "";
            string start_odd_lose = "";
            string start_persent_win = "";
            string start_persent_draw = "";
            string start_persent_lose = "";
            string start_persent_return = "";
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath;
                if (node.Name == "tr")
                {
                    HtmlNodeCollection nodes_td = doc.DocumentNode.SelectNodes(xpath + "/td");
                    if (nodes_td.Count == 12)
                    {
                       
                        league = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]").InnerText;   

                        start_time = doc.DocumentNode.SelectSingleNode(xpath + "/td[2]").InnerText;
                        HtmlNode node_start_time= doc.DocumentNode.SelectSingleNode(xpath + "/td[2]");
                        foreach (HtmlNode node_script in node_start_time.ChildNodes)
                        {
                            if (node_script.Name == "script")
                            {
                                start_time = start_time.Replace(node_script.InnerText, "");
                            }
                        }
                        host = doc.DocumentNode.SelectSingleNode(xpath + "/td[3]").InnerText;
                        client = doc.DocumentNode.SelectSingleNode(xpath + "/td[11]").InnerText;
                        start_odd_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[4]").InnerText;
                        start_odd_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[5]").InnerText;
                        start_odd_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]").InnerText;
                        start_persent_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[7]").InnerText;
                        start_persent_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[8]").InnerText;
                        start_persent_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[9]").InnerText;
                        start_persent_return = doc.DocumentNode.SelectSingleNode(xpath + "/td[10]").InnerText;


                    }
                    if (nodes_td.Count == 7)
                    {
                        odd_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[1]").InnerText;
                        odd_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[2]").InnerText;
                        odd_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[3]").InnerText;
                        persent_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[4]").InnerText;
                        persent_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[5]").InnerText;
                        persent_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]").InnerText;
                        persent_return = doc.DocumentNode.SelectSingleNode(xpath + "/td[7]").InnerText;

                        sb.AppendLine(league.PR(10) + start_time.PR(20) + host.PR(30) + client.PR(30)+start_odd_win.PR(10)+start_odd_draw.PR(10)+start_odd_lose.PR(10)+start_persent_win.PR(10)+start_persent_draw.PR(10)+start_persent_lose.PR(10)+start_persent_return.PR(10));
                        sb.AppendLine("".PR(10) + "".PR(20) + "".PR(30) + "".PR(30) + odd_win.PR(10) + odd_draw.PR(10) + odd_lose.PR(10) + persent_win.PR(10) + persent_draw.PR(10) + persent_lose.PR(10) + persent_return.PR(10));
                    }

                }

            }
            this.txt_result.Text = sb.ToString();

        }
        public void get_odd_1X2_single_match()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_nowgoal + "odd_1X2_single_match.html"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);


            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");

            string league = "";
            string start_time = "";
            string host = "";
            string client = "";

            string company = "";
            string odd_win = "";
            string odd_draw = "";
            string odd_lose = "";
            string persent_win = "";
            string persent_draw = "";
            string persent_lose = "";
            string persent_return = "";
            string kelly_win = "";
            string kelly_draw = "";
            string kelly_lose = "";
            string modify_time = "";
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath;
                if (node.Name == "tr")
                {
                    HtmlNodeCollection nodes_td = doc.DocumentNode.SelectNodes(xpath + "/td");
                    if (nodes_td.Count == 14)
                    {
                        if (node.InnerText.Contains("Kelly")) continue;
                        company = doc.DocumentNode.SelectSingleNode(xpath + "/td[2]").InnerText; 
                        odd_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[3]").InnerText;
                        odd_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[4]").InnerText;
                        odd_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[5]").InnerText;
                        persent_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]").InnerText;
                        persent_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[7]").InnerText;
                        persent_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[8]").InnerText;
                        persent_return = doc.DocumentNode.SelectSingleNode(xpath + "/td[9]").InnerText;
                        kelly_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[10]").InnerText;
                        kelly_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[11]").InnerText;
                        kelly_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[12]").InnerText;
                        modify_time = doc.DocumentNode.SelectSingleNode(xpath + "/td[13]").InnerText;
                        sb.AppendLine(company.PR(20) + odd_win.PR(10) + odd_draw.PR(10) + odd_lose.PR(10)+persent_win.PR(10)+persent_draw.PR(10)+persent_lose.PR(10)+persent_return.PR(10)+kelly_win.PR(10)+kelly_draw.PR(10)+kelly_lose.PR(10)+modify_time.PR(20));


                    } 
                }

            }
            this.txt_result.Text = sb.ToString();

        }
        public void get_league_all()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_nowgoal + "league_all.html"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);


            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");

            string league="";
            string year="";
            string url="";
            foreach (HtmlNode node in nodes_all)
            { 
                if (node.Name == "div"&&
                    node.Attributes.Contains("class") &&
                    node.Attributes["class"].Value == "floatDiv"  )
                {
                    string xpath = node.XPath;
                    HtmlNodeCollection nodes_ul = doc.DocumentNode.SelectNodes(xpath + "/div[1]/ul[1]/li");
                    foreach (HtmlNode node_url in nodes_ul)
                    {
                        string xpath2 = node_url.XPath;
                        league = doc.DocumentNode.SelectSingleNode(xpath2 + "/a[1]").InnerText;

                        HtmlNodeCollection node_years = doc.DocumentNode.SelectNodes(xpath2 + "/ul[1]/li");
                        foreach (HtmlNode node_year in node_years)
                        {
                            string xpath3=node_year.XPath;
                            year = doc.DocumentNode.SelectSingleNode(xpath3 + "/a[1]").InnerText;
                            url = doc.DocumentNode.SelectSingleNode(xpath3 + "/a[1]").Attributes["href"].Value;
                            sb.AppendLine(league.PR(20) + year.PR(20) + url);

                        }
                    }
                }

            }
            this.txt_result.Text = sb.ToString();

        }
        public void get_league_single()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_nowgoal + "league_single.html"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);


            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");

            string league = "";
            string start_time = "";
            string host = "";
            string client = "";

            string company = "";
            string odd_win = "";
            string odd_draw = "";
            string odd_lose = "";
            string persent_win = "";
            string persent_draw = "";
            string persent_lose = "";
            string persent_return = "";
            string kelly_win = "";
            string kelly_draw = "";
            string kelly_lose = "";
            string modify_time = "";
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath;
                if (node.Name == "tr")
                {
                    HtmlNodeCollection nodes_td = doc.DocumentNode.SelectNodes(xpath + "/td");
                    if (nodes_td.Count == 14)
                    {
                        if (node.InnerText.Contains("Kelly")) continue;
                        company = doc.DocumentNode.SelectSingleNode(xpath + "/td[2]").InnerText;
                        odd_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[3]").InnerText;
                        odd_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[4]").InnerText;
                        odd_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[5]").InnerText;
                        persent_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[6]").InnerText;
                        persent_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[7]").InnerText;
                        persent_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[8]").InnerText;
                        persent_return = doc.DocumentNode.SelectSingleNode(xpath + "/td[9]").InnerText;
                        kelly_win = doc.DocumentNode.SelectSingleNode(xpath + "/td[10]").InnerText;
                        kelly_draw = doc.DocumentNode.SelectSingleNode(xpath + "/td[11]").InnerText;
                        kelly_lose = doc.DocumentNode.SelectSingleNode(xpath + "/td[12]").InnerText;
                        modify_time = doc.DocumentNode.SelectSingleNode(xpath + "/td[13]").InnerText;
                        sb.AppendLine(company.PR(20) + odd_win.PR(10) + odd_draw.PR(10) + odd_lose.PR(10) + persent_win.PR(10) + persent_draw.PR(10) + persent_lose.PR(10) + persent_return.PR(10) + kelly_win.PR(10) + kelly_draw.PR(10) + kelly_lose.PR(10) + modify_time.PR(20));


                    }
                }

            }
            this.txt_result.Text = sb.ToString();

        }
        public void get_league_fixtures_standings()
        {
            WebClient web_client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(web_client.DownloadData(root_url_nowgoal + "league_fixtures_standings.html"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);


            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");


            string index = "";
            string name_eng = "";
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath;
                if (node.Id == "div_Table1")
                {
                    HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(xpath + "/tbody[1]/tr");
                    for (int i = 1; i < nodes_tr.Count-1;i++ )
                    {
                        HtmlNode node_tr = nodes_tr[i];
                        string xpath2 = node_tr.XPath;
                        index = doc.DocumentNode.SelectSingleNode(xpath2 + "/td[1]").InnerText;
                        name_eng = doc.DocumentNode.SelectSingleNode(xpath2 + "/td[2]").InnerText;
                        //&nbsp
                        string[] names =  name_eng.E_SPLIT( "&nbsp");
                        if (names.Length > 0) name_eng = names[0];
                        sb.AppendLine(index.PR(5) + name_eng);
                    }
            
                }

            }
            this.txt_result.Text = sb.ToString();

        }



        







    }
}
