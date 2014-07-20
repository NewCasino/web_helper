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
 
        public frm_match_100()
        {
            InitializeComponent();
        }

        private void btn_analyse_Click(object sender, EventArgs e)
        {

            sb = new StringBuilder();
            browser.Navigate(@"https://sports.bwin.com/en/sports/4/betting/football");
            //from_163();
            //from_bwin();



            this.txt_result.Text = sb.ToString();
        }

        public string get_html(string url)
        {
            browser.Navigate(url);

            string html = "";// browser.Document.Body.InnerHtml;
            return html;
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
                        //sb.Append(root + Environment.NewLine);
                        //sb.Append(node.InnerHtml + Environment.NewLine);
                        sb.Append(Tool.pad(league, 20) +
                            Tool.pad(start_time, 20) +
                            Tool.pad(host, 20) +
                            Tool.pad(client, 20) +
                            Tool.pad(win, 20) +
                            Tool.pad(draw, 20) +
                            Tool.pad(lose, 20)+Environment.NewLine);

                                 

                    }
                    catch (Exception error)
                    { }
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
                        sb.Append(league.PL(20) + start_time.PL(20) + host.PL(20) + client.PL(20) + win.PL(20) + draw.PL(20) + lose.PL(20) + Environment.NewLine);


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
                        sb.Append(league.PadRight(20, ' ') +
                            start_time.PadRight(20, ' ') +
                            host.PadRight(30, ' ').Substring(0, 30) +
                            client.PadRight(30, ' ').Substring(0, 30) +
                            win.PadRight(20, ' ') +
                            draw.PadRight(20, ' ') +
                            lose.PadRight(20, ' ') +
                            Environment.NewLine);


                    }
                }
                catch (Exception error)
                { }

            }
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            sb.Remove(0, sb.ToString().Length);
            string body ="<body>"+ browser.Document.Body.InnerHtml+"</body>";
            from_163(body);
            this.txt_result.Text = sb.ToString();
        }

        private void btn_navigate_Click(object sender, EventArgs e)
        {
            browser.Navigate(this.txt_url.Text);
        }

        private void browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // MessageBox.Show("navegaed ok");
        }

        private void frm_match_100_Load(object sender, EventArgs e)
        { 
        }

     
    }
}
