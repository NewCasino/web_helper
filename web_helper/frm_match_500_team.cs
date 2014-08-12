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
        StringBuilder sb = new StringBuilder();
        public frm_match_500_team()
        {
            InitializeComponent();
        }

        public static string root_path = Environment.CurrentDirectory.Replace(@"bin\Debug", "").Replace(@"bin\x86\Debug", "") + @"data\500\";
        string root_url = @"file:///" + root_path.Replace(@"\", @"/");
        private void btn_step_1_Click(object sender, EventArgs e)
        {
            sb.Remove(0, sb.Length);
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
            string path = root_path + "lg_url.txt";
            sb.Remove(0, sb.Length);
            FileStream stream = (FileStream)File.Open(path, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string line = "";
            while (line != null)
            {
                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    string url = "http://liansai.500.com/" + line;
                    WebClient client = new WebClient();
                    string html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(url));
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
                        if (!string.IsNullOrEmpty(href))
                        {
                            sb.AppendLine(href);
                            write_line("teams.txt", href);
                        }
                    }
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();
                }
            }
            reader.Close();
            stream.Close();




        }

        private void btn_step_3_Click(object sender, EventArgs e)
        {
            sb.Remove(0, sb.Length);
            string path = root_path + "teams.txt"; 
            FileStream stream = (FileStream)File.Open(path, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string line = "";
            bool last = false;
            while (line != null)
            {
                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line) && line.Substring(0, 2) != "--" && last == true)
                {

                    try
                    {
                        WebClient client = new WebClient();
                        string html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(line));
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(html);

                        string root = @"/html[1]/body[1]/div[3]/div[5]/div[1]/div[3]/ul[1]/li";

                        HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(root);

                        sb.AppendLine("----");
                        write_line("teams_detail.txt", "----" + line);
                        foreach (HtmlNode node in nodes_all)
                        {
                            string xpath = node.XPath + "/a[1]";
                            HtmlNode final = doc.DocumentNode.SelectNodes(xpath)[0];
                            string text = final.InnerText;
                            text = text.Replace('(', '●').Replace(')', ' ');

                            string[] items = text.Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                            if (items.Length > 1)
                            {
                                sb.AppendLine(items[0].PR(100) + items[1].PR(100));
                                write_line("teams_detail.txt", text);
                            }
                        }
                        this.txt_result.Text = sb.ToString();
                        Application.DoEvents();
                    }
                    catch (Exception error)
                    {
                        sb.AppendLine(error.Message);
                        Application.DoEvents();
                    }


                }
                if (!string.IsNullOrEmpty(line) && line.Substring(0, 2) == "--")
                {
                    last = true;
                }
                else
                {
                    last = false;
                }
            }
            //StringBuilder sb = new StringBuilder();

        }
        public void write_line(string file_name, string txt)
        {
            FileStream stream = (FileStream)File.Open(root_path + file_name, FileMode.Append);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(txt);
            writer.Close();
            stream.Close();
        }

        private void btn_read_to_db_Click(object sender, EventArgs e)
        {
            string path = root_path + "teams_detail.txt";
           
            FileStream stream = (FileStream)File.Open(path, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string line = ""; 
            while (line != null)
            {
                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line) && line.Substring(0, 2) != "--")
                {
                     

                        line = line.TrimStart().TrimEnd();
                        string[] items = line.Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                        string cn_name = items[0].TrimStart().TrimEnd();
                        string eng_name = items[1].TrimStart().TrimEnd();
                        insert_db(eng_name, cn_name);
                   
                     
                }
               
            }
        }
        public void insert_db(string eng_name, string cn_name)
        {
            string sql = "";
            sql = " select * from teams where name1='{0}' ";
            sql=string.Format(sql,eng_name);
            if (SQLServerHelper.get_table(sql).Rows.Count == 0)
            {
                sql = "  insert into teams (name1,name2,all_name) values ('{0}','{1}','{2}')";
                sql = string.Format(sql, eng_name, cn_name, eng_name + "●" + cn_name);
                SQLServerHelper.exe_sql(sql);

                sb.AppendLine(eng_name.PR(20) + cn_name.PR(20));
                this.txt_result.Text = sb.ToString();
                Application.DoEvents();
            }


        }

        private void btn_read_detail_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();

            WebClient client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(root_url+"step3.htm"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            string root = @"/html[1]/body[1]/div[3]/div[5]/div[1]/div[3]/ul[1]/li";
            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(root);
            sb.AppendLine("----"); 
            foreach (HtmlNode node in nodes_all)
            {
                string xpath = node.XPath + "/a[1]";
                HtmlNode final = doc.DocumentNode.SelectNodes(xpath)[0];
                string text = final.InnerText;
                text = text.Replace('(', '●').Replace(')', ' ');

                string[] items = text.Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                if (items.Length > 1)
                {
                    sb.AppendLine(items[0].PR(100) + items[1].PR(100));
                     
                }
            }

            string root_matchs = @"/html[1]/body[1]/div[3]/div[6]/table[1]/tr[position()>1]";
            HtmlNodeCollection nodes_match = doc.DocumentNode.SelectNodes(root_matchs);
            foreach (HtmlNode node in nodes_match)
            {
                HtmlNode node_leage = doc.DocumentNode.SelectNodes(node.XPath + "/td[1]/a[1]")[0];
                HtmlNode node_time = doc.DocumentNode.SelectNodes(node.XPath + "/td[2]")[0];
                HtmlNode node_host = doc.DocumentNode.SelectNodes(node.XPath + "/td[3]/a[1]")[0];
                HtmlNode node_client = doc.DocumentNode.SelectNodes(node.XPath + "/td[5]/a[1]")[0];
                Match100Helper.insert_future_match(node_leage.InnerText, node_time.InnerText, node_host.InnerText, node_client.InnerText);
                sb.AppendLine(node_leage.InnerText.PR(20) + node_time.InnerText.PR(30) + node_host.InnerText.PR(20) + node_client.InnerText.PR(20));
            }

            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
    }
}
