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

namespace match_helper
{
    public partial class frm_single_1xbet : Form
    {
        public frm_single_1xbet()
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
            this.txt_result.Text = get_data(this.txt_result.Text);
        }
        public string get_html()
        {
            string json = HtmlHelper.get_html("");
            return json;
        }
        public string get_data(string html)
        {

            StringBuilder sb = new StringBuilder();

            html = html.Replace("<thead=\"\"", "");
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();


            foreach (HtmlNode node in nodes_all)
            {

                if (node.Id == "tb2")
                {
                    string team = node.SELECT_NODE("/div[1]/table[1]/tbody[1]/tr[1]/td[2]/a[1]/span[1]").InnerText;
                    string[] teams = team.E_SPLIT("-");
                    string host = teams[0];
                    string client = teams[1];
                    string start_time = node.SELECT_NODE("/div[1]/table[1]/tbody[1]/tr[1]/td[3]").ChildNodes[0].InnerText.E_TRIM();
                    start_time = DateTime.Now.Year.ToString() +"-"+ start_time.Substring(3, 2) +"-"+ start_time.Substring(0, 2) + " " + start_time.Substring(6, 5);

                    string odd_host = node.SELECT_NODE("/div[2]/table[1]/tbody[1]/tr[2]/td[1]").InnerText;
                    string odd_draw = node.SELECT_NODE("/div[2]/table[1]/tbody[1]/tr[2]/td[2]").InnerText;
                    string odd_away = node.SELECT_NODE("/div[2]/table[1]/tbody[1]/tr[2]/td[3]").InnerText;
                    BsonDocument doc_odd=Match100Helper.get_odd_doc_from_europe(odd_host, odd_draw, odd_away);
                    sb.Append(host.PR(10)+client.PR(10)+ start_time.PR(30)+odd_host.PR(10)+odd_draw.PR(10)+odd_away.PR(10)+doc_odd["persent_return"].ToString()+M.N);
                    Match100Helper.insert_data("1-x-bet", "", start_time, host, client, odd_host, odd_draw, odd_away, "0", "0");
                }
            }


            return sb.ToString();


        }


    }
}