using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MongoDB.Bson;
using HtmlAgilityPack;
using System.Collections;

namespace web_helper
{
    public partial class frm_single_betadonis : Form
    {
        public frm_single_betadonis()
        {
            InitializeComponent();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        private void btn_beaurify_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = JsonBeautify.beautify(this.txt_result.Text);
        }
        private void btn_get_html_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = get_html();
        }

        private void btn_get_data_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = get_data2(this.txt_result.Text);
        }
        public string get_html()
        {
            string json = HtmlHelper.get_html("");
            return json;
        }
        public string get_data(string html)
        {

            StringBuilder sb = new StringBuilder();

            HtmlAgilityPack.HtmlDocument doc_html = new HtmlAgilityPack.HtmlDocument();
            doc_html.LoadHtml(html);


            HtmlNodeCollection nodes_all = doc_html.DocumentNode.SelectNodes(@"//*");
            //string str_json = doc_html.DocumentNode.SELECT_NODE("/div[1]/div[1]/script[1]").InnerText.Replace("var centralAreaLiveSectionsJsonArray =", "");
            string str_json = doc_html.DocumentNode.SELECT_NODE("/div[1]/div[2]/script[1]").InnerText.Replace("var centralAreaPreLiveSectionsJsonArray =", "");
            BsonArray list_root = MongoHelper.get_array_from_str(str_json);
            foreach (BsonDocument root in list_root)
            {
                BsonArray matchs = root["matchesJsonArray"].AsBsonArray;
                foreach (BsonDocument match in matchs)
                {
                    if (match.Contains("matchInfo"))
                    {
                        string date = match["matchInfo"]["sdfDMYTime"].ToString();
                        string time = match["matchInfo"]["sdfHourTime"].ToString();
                        string start_time = DateTime.Now.Year.ToString() + "-" + date.Substring(3, 2) + "-" + date.Substring(0, 2) + " " + time;
                        string home = match["matchInfo"]["homeName"].ToString();
                        string away = match["matchInfo"]["awayName"].ToString();
                        string odd_home = match["marketWrapper"]["oddsJsonArray"][0]["ov"].ToString();
                        string odd_draw = match["marketWrapper"]["oddsJsonArray"][1]["ov"].ToString();
                        string odd_away = match["marketWrapper"]["oddsJsonArray"][2]["ov"].ToString();
                        sb.Append(start_time.PR(20) + home.PR(20) + away.PR(20) + odd_home.PR(10) + odd_draw.PR(10) + odd_away.PR(10) + M.N);
                        Match100Helper.insert_data("betadonis", "", start_time, home, away, odd_home, odd_draw, odd_away, "2", "0");
                    }
                }
            } 
            return sb.ToString(); 
        }
        public string get_data2(string html)
        {
            StringBuilder sb = new StringBuilder();

            html = html.Replace("<thead=\"\"", "");
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();


            foreach (HtmlNode node in nodes_all)
            {
                if (node.CLASS() == "MatchContainer")
                { 
                    if (!node.InnerHtml.Contains( "LiveMatchCompetitors"))
                    {
                        try
                        {
                            string date = node.SELECT_NODE("/div[1]/span[1]/strong[1]").ChildNodes[0].InnerText.E_TRIM();
                            string time = node.SELECT_NODE("/div[1]/span[1]/em[1]").ChildNodes[0].InnerText.E_TRIM();

                            string start_time = "";
                            if (date.E_TRIM().ToLower() != "live")
                            {
                                string[] dates = date.E_SPLIT("/");
                                date = dates[1] + "/" + dates[0];
                                start_time = date + M.D + time;
                            }
                            else
                            {
                                start_time = DateTime.Now.ToString("MM/dd") + M.D + time;
                            }

                            string host = node.SELECT_NODE("/div[2]/h3[1]/a[1]/span[1]").ChildNodes[0].InnerText;
                            string client = node.SELECT_NODE("/div[2]/h3[1]/a[1]/span[3]").ChildNodes[0].InnerText;

                            string odd_host = node.SELECT_NODE("div[4]/ol[1]/li[1]/a[1]/span[1]").InnerText;
                            string odd_draw = node.SELECT_NODE("div[4]/ol[1]/li[2]/a[1]/span[1]").InnerText;
                            string odd_away = node.SELECT_NODE("div[4]/ol[1]/li[3]/a[1]/span[1]").InnerText;
                            BsonDocument doc_odd = Match100Helper.get_odd_doc_from_europe(odd_host, odd_draw, odd_away);
                            sb.Append(start_time.PR(20) + host.PR(30) + client.PR(30) + odd_host.PR(10) + odd_draw.PR(10) + odd_away.PR(10) + doc_odd["persent_return"].ToString() + M.N);
                            Match100Helper.insert_data("betadonis", "", start_time, host, client, odd_host, odd_draw, odd_away, "2", "0");
                        }
                        catch (Exception error) { }
                        }
                }
            }
            return sb.ToString();
        } 
    }
}