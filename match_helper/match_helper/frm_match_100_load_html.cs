using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.OleDb;
using System.Threading;
using mshtml;
using System.Reflection; 
using System.Collections;

namespace match_helper
{
    public partial class frm_match_100_load_html : Form
    {
        public frm_match_100_load_html()
        {
            InitializeComponent();
        }

        private void btn_from_MB_html_Click(object sender, EventArgs e)
        {

            this.txt_result.Text= from_marathonbet(this.txt_html.Text);
        }

        public string  from_marathonbet(string html)
        {
            StringBuilder sb = new StringBuilder();
            //-------------------------------------------
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
            string zone = "";
            foreach (HtmlNode node in nodes_all)
            {
                zone = "8"; 
                if (node.Id == "timer")
                {
                    //02.09.14, 14:47 (GMT+1)
                    string timer = node.InnerText.E_TRIM();
                    DateTime dt_timer = new DateTime(Convert.ToInt16("20" + timer.Substring(6, 2)), Convert.ToInt16(timer.Substring(3, 2)), Convert.ToInt16(timer.Substring(0, 2)),
                                                     Convert.ToInt16(timer.Substring(9, 2)), Convert.ToInt16(timer.Substring(12, 2)), 0);
                    TimeSpan span = DateTime.Now - dt_timer; 
                    zone = (8 - Math.Round(span.TotalHours)).ToString();
                }

                if (node.Id == "container_EVENTS")
                {
                    HtmlNodeCollection nodes_div = node.SELECT_NODES("/div");
                    foreach (HtmlNode node_div in nodes_div)
                    {
                        if (node_div.Id.Contains("container"))
                        {
                            league = node_div.SELECT_NODE("div[1]/h2[1]").InnerText;

                            HtmlNode test = node_div.SELECT_NODE("div[2]/div[1]/table[1]");
                            HtmlNodeCollection nodes_table = node_div.SELECT_NODES("div[2]/div[1]/table[1]/tbody");
                            foreach (HtmlNode node_table in nodes_table)
                            {
                                if (node_table.Id.Contains("event"))
                                {
                                    date = node_table.SELECT_NODE("/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[2]").InnerText.E_TRIM();
                                    date = date.Replace("2015", "");
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
                                   
                                    win =  node_table.SELECT_NODE("/tr[1]/td[2]").InnerText.E_REMOVE();
                                    draw =  node_table.SELECT_NODE("/tr[1]/td[3]").InnerText.E_REMOVE();
                                    lose = node_table.SELECT_NODE("/tr[1]/td[4]").InnerText.E_REMOVE();
                                    if (win.Contains("/"))
                                    {
                                        win = AnalyseTool.convert_english_odd(win);
                                        draw = AnalyseTool.convert_english_odd(draw);
                                        lose = AnalyseTool.convert_english_odd(lose); 
                                    }

                                    if (!string.IsNullOrEmpty(win.E_TRIM()) && !string.IsNullOrEmpty(draw.E_TRIM()) && !string.IsNullOrEmpty(lose.E_TRIM()))
                                    {
                                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                        Match100Helper.insert_data("marathonbet", league, start_time, host, client, win, draw, lose, "0", zone);
                                    }
                                }

                            }

                        }
                    }
                }
            }
            //------------------------------------------------------
            return sb.ToString(); 
        }
    }
}
