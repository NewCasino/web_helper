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

namespace web_helper
{
    public partial class frm_match_500_team : Form
    {
        public frm_match_500_team()
        {
            InitializeComponent();
        }

        public static string root_path = Environment.CurrentDirectory.Replace(@"bin\Debug", "").Replace(@"bin\x86\Debug", "") + @"data\500\";
        string root_url = @"file:///" + root_path.Replace(@"\", @"/");
        private void btn_step_1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            WebClient client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(root_url + "step1.htm"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);


            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();

            foreach (HtmlNode node in nodes_all)
            {
                if (node.Attributes.Contains("href"))
                {
                    string href = node.Attributes["href"].Value.ToString();
                    if (href.ToLower().Contains("seasonid"))
                    {

                        nodes.Add(node);
                    }
                }
            }

            foreach (HtmlNode node in nodes)
            {
                sb.AppendLine(node.Attributes["href"].Value.ToString());
            }
            this.txt_result.Text = sb.ToString();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        private void btn_step_2_Click(object sender, EventArgs e)
        {
            //string path = root_path + "lg_url.txt";
            //StringBuilder sb = new StringBuilder();
            //FileStream stream = (FileStream)File.Open(path, FileMode.Open);
            //StreamReader reader = new StreamReader(stream);
            //string line = "";
            //while (line != null)
            //{
            //    line = reader.ReadLine();
            //    if (!string.IsNullOrEmpty(line))
            //    {
            //        sb.AppendLine("http://liansai.500.com/" + line);
            //    }
            //}
            //reader.Close();
            //stream.Close();
            //this.txt_result.Text = sb.ToString();
            StringBuilder sb = new StringBuilder();
            WebClient client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(root_url + "step2.htm"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();

            foreach (HtmlNode node in nodes_all)
            {
                if (node.Attributes.Contains("href"))
                {
                    string href = node.Attributes["href"].Value.ToString();
                    if (href.ToLower().Contains("teamid"))// || href.Contains("teamid"))
                    {

                        nodes.Add(node);
                    }
                }
            }

            sb.AppendLine("----");
            write_line("teams.txt", "----");
            foreach (HtmlNode node in nodes)
            {
                string href = "http://liansai.500.com/" + node.Attributes["href"].Value.ToString();
                if(!string.IsNullOrEmpty(href))
                {
                    sb.AppendLine(href);
                    write_line("teams.txt",  href);
                }
            }
            this.txt_result.Text = sb.ToString();

        }

        private void btn_step_3_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            WebClient client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(root_url + "step3.htm"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            string root = @"/html[1]/body[1]/div[3]/div[5]/div[1]/div[3]/ul[1]/li";

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(root);

            sb.AppendLine("----");
            write_line("teams_detail.txt", "----");
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath + "/a[1]";
                HtmlNode final = doc.DocumentNode.SelectNodes(xpath)[0];
                string text = final.InnerText;
                text = text.Replace('(', '●').Replace(')', ' ');

                string[] items = text.Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                sb.AppendLine(items[0].PR(20) + items[1].PR(20));
                write_line("teams_detail.txt", items[0].PR(20) + items[1].PR(20));
            }
            this.txt_result.Text = sb.ToString();
        }
        public void write_line(string file_name, string txt)
        {
            FileStream stream = (FileStream)File.Open(root_path + file_name, FileMode.Append);
            StreamWriter writer = new StreamWriter(stream);
            
            writer.WriteLine(txt);
            writer.Close(); 
            stream.Close();
        }
    }
}
