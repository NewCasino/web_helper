﻿using System;
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
    public partial class frm_match_100_team_analyse : Form
    {
        StringBuilder sb = new StringBuilder();
        public frm_match_100_team_analyse()
        {
            InitializeComponent();
        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        public static string root_path = Environment.CurrentDirectory.Replace(@"bin\Debug", "").Replace(@"bin\x86\Debug", "") + @"data\500\";
        string root_url = @"file:///" + root_path.Replace(@"\", @"/");
        public static string root_path_90vs = Environment.CurrentDirectory.Replace(@"bin\Debug", "").Replace(@"bin\x86\Debug", "") + @"data\90vs\";
        string root_url_90vs = @"file:///" + root_path_90vs.Replace(@"\", @"/");

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
        //all info，include match info
        public void read_500_team_detail()
        {
            StringBuilder sb = new StringBuilder();

            WebClient client = new WebClient();
            string html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(root_url + "step3.htm"));
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



        public void write_line(string file_name, string txt)
        {
            FileStream stream = (FileStream)File.Open(root_path + file_name, FileMode.Append);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(txt);
            writer.Close();
            stream.Close();
        }
        public void write_line_90vs(string file_name, string txt)
        {
            FileStream stream = (FileStream)File.Open(root_path_90vs + file_name, FileMode.Append);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(txt);
            writer.Close();
            stream.Close();
        }


        private void btn_90vs_read_leage_Click(object sender, EventArgs e)
        {


            string path = root_path_90vs + "leage.js";

            FileStream stream = (FileStream)File.Open(path, FileMode.Open);
            StreamReader reader = new StreamReader(stream, Encoding.Default);
            string line = "";
            BsonDocument doc_season_x = new BsonDocument();
            BsonDocument doc_season = new BsonDocument();
            BsonDocument doc_type = new BsonDocument();
            BsonDocument doc_test = new BsonDocument();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    string[] items = line.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    string name = items[0].Replace("var", "").Trim();
                    string value = items[1].Replace(";", "");


                    if (name == "test") doc_test = MongoHelper.get_doc_from_str(get_js_json(value));
                    if (name == "season_x") doc_season_x = MongoHelper.get_doc_from_str(get_js_json(value));
                    if (name == "season") doc_season = MongoHelper.get_doc_from_str(get_js_json(value));
                    if (name == "type") doc_type = MongoHelper.get_doc_from_str(get_js_json(value));
                }
            }
            reader.Close();
            stream.Close();
            int count = 0;
            foreach (BsonElement element in doc_type.Elements)
            {
                BsonArray array_i = element.Value.AsBsonArray;
                for (int i = 0; i < array_i.Count; i++)
                {
                    count = count + 1;
                    BsonArray array_j = array_i[i].AsBsonArray;
                    string lg_id = array_j[0].ToString();
                    string lg_name = array_j[1].ToString();

                    if (doc_season.Contains(lg_id))
                    {
                        BsonArray array_k = doc_season[lg_id].AsBsonArray;
                        for (int k = 0; k < array_k.Count; k++)
                        {
                            string season_id = array_k[k].ToString();
                            string season_name = doc_season_x[season_id].AsBsonArray[0].ToString();
                            count = count + 1;

                            string url = "http://bf.90vs.com/db/all_season/{1}/{0}.js";
                            url = string.Format(url, season_id, lg_id);
                            sb.AppendLine(url);
                        }
                    }
                }
            }
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
        }
        private void btn_90vs_read_all_team_Click(object sender, EventArgs e)
        {

            string path = root_path_90vs + "leages_url.txt";
            sb.Remove(0, sb.Length);
            FileStream stream = (FileStream)File.Open(path, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string url = "";
            while (url != null)
            {
                url = reader.ReadLine();
                try
                {
                    WebClient client = new WebClient();
                    string html = System.Text.Encoding.UTF8.GetString(client.DownloadData(url));
                    string[] list_line = html.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    BsonDocument doc_teams = new BsonDocument();
                    BsonDocument doc_lg = new BsonDocument();
                    foreach (string line in list_line)
                    {
                        if (line != null)
                        {
                            string[] items = line.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                            if (items.Length == 2)
                            {
                                string name = items[0].Replace("var", "").Trim();
                                string value = items[1].Replace(";", "");

                                if (name == "all_player_str") doc_teams = MongoHelper.get_doc_from_str(get_js_json(value));
                                if (name == "this_match") doc_lg = MongoHelper.get_doc_from_str(get_js_json(value));
                            }
                        }
                    }
                    string lg_id = doc_lg["1"].AsBsonArray[0].ToString();
                    string lg_name_full = doc_lg["1"].AsBsonArray[1].ToString();
                    string lg_name = doc_lg["1"].AsBsonArray[2].ToString();
                    string lg_season_name = doc_lg["1"].AsBsonArray[5].ToString();
                    foreach (BsonElement element in doc_teams.Elements)
                    {
                        if (element.Name == "1") continue;
                        string team_name_eng = "";
                        string team_name_chi = "";
                        string team_name_gd = "";
                        team_name_eng = element.Value.AsBsonArray[2].ToString();
                        team_name_chi = element.Value.AsBsonArray[1].ToString();
                        team_name_gd = element.Value.AsBsonArray[0].ToString();
                        insert_db_90vs(lg_season_name, "", lg_name, lg_name_full, team_name_eng, team_name_chi, team_name_gd);
                    }
                    sb.AppendLine(url);
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();

                }
                catch (Exception error)
                {

                    sb.AppendLine("----------------" + url);
                }


            }
            reader.Close();
            stream.Close();
        }


        public void insert_db(string eng_name, string cn_name)
        {
            string sql = "";
            sql = " select * from teams where name1='{0}' ";
            sql = string.Format(sql, eng_name);
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
        public void insert_db_90vs(string season_name, string lg_name1, string lg_name2, string lg_name3, string name1, string name2, string name3)
        {
            string sql = "";
            //sql = " select * from teams_log where web_site='90vs' and name1='{0}'";
            //sql = string.Format(sql, name1);
            //if (SQLServerHelper.get_table(sql).Rows.Count == 0)
            //{
            sql = "   insert into teams_log (web_site,season_name,lg_name1,lg_name2,lg_name3,lg_name_all,name1,name2,name3,name_all) values" +
                  "    ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')";
            sql = string.Format(sql, "90vs", season_name, lg_name1, lg_name2, lg_name3, lg_name1 + "●" + lg_name2 + "●" + lg_name3,
                                                     name1, name2, name3, name1 + "●" + name2 + "●" + name3);
            SQLServerHelper.exe_sql(sql);

            //}

        }
        private void btn_test_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string html = System.Text.Encoding.UTF8.GetString(client.DownloadData(@"http://bf.90vs.com/db/all_season/1910/76.js"));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            this.txt_result.Text = html;

            //string sql = "select * from teams ";
            //DataTable dt=SQLServerHelper.get_table(sql);
            //foreach(DataRow  row in   dt.Rows)
            //{
            //    string name1 = row["name1"].ToString();
            //    string name2 = row["name2"].ToString();
            //    string name_all = row["all_name"].ToString();

            //    sql = "insert into teams_log (web_site,name1,name2,name_all) values ('{0}','{1}','{2}','{3}')";
            //    sql = string.Format(sql, "500",name1, name2, name_all);
            //    SQLServerHelper.exe_sql(sql);
            //}
            //MessageBox.Show("OK!");

        }
        private void btn_add_simple_complex_Click(object sender, EventArgs e)
        {
            string sql = "select * from teams_log where id='3975' ";
            DataTable dt = SQLServerHelper.get_table(sql);

            foreach (DataRow row in dt.Rows)
            {

                string id = row["id"].ToString();
                string name_all = row["name_all"].ToString();
                string temp = name_all;
                string[] name_list = name_all.ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string name in name_list)
                {
                    string name_s = Tool.to_simple_chinese(name);
                    string name_c = Tool.to_complex_chinese(name);
                    if (!name_all.Contains(name_s)) temp = temp + "●" + name_s;
                    if (!name_all.Contains(name_c)) temp = temp + "●" + name_c;
                }
                if (temp != name_all)
                {
                    sql = "update teams_log set name_all='{0}' where id={1}";
                    sql = string.Format(sql,temp, id);
                    SQLServerHelper.exe_sql(sql);
                }
            }
        }
        public string get_js_json(string str)
        {
            string result = str;
            int add = 0;
            for (int i = 1; i < str.Length; i++)
            {
                int num_len = 0;
                if (i + 1 < str.Length && Match100Helper.is_double_str(str.Substring(i, 1)))
                {
                    num_len = 1;
                }
                if (i + 2 < str.Length && Match100Helper.is_double_str(str.Substring(i, 2)))
                {
                    num_len = 2;
                }
                if (i + 3 < str.Length && Match100Helper.is_double_str(str.Substring(i, 3)))
                {
                    num_len = 3;
                }
                if (i + 4 < str.Length && Match100Helper.is_double_str(str.Substring(i, 4)))
                {
                    num_len = 4;
                }
                if (i + 5 < str.Length && Match100Helper.is_double_str(str.Substring(i, 5)))
                {
                    num_len = 5;
                }
                if (i + 6 < str.Length && Match100Helper.is_double_str(str.Substring(i, 6)))
                {
                    num_len = 6;
                }
                if (i + 7 < str.Length && Match100Helper.is_double_str(str.Substring(i, 7)))
                {
                    num_len = 7;
                }
                if (i + 8 < str.Length && Match100Helper.is_double_str(str.Substring(i, 8)))
                {
                    num_len = 8;
                }
                if (i + 9 < str.Length && Match100Helper.is_double_str(str.Substring(i, 9)))
                {
                    num_len = 9;
                }
                if (i + 10 < str.Length && Match100Helper.is_double_str(str.Substring(i, 10)))
                {
                    num_len = 10;
                }
                if ((i - 1 >= 0) && str.Substring(i - 1, 1) != "{" && str.Substring(i - 1, 1) != "[" && str.Substring(i - 1, 1) != "," && str.Substring(i - 1, 1) != ":") continue;
                if (i + num_len < str.Length && str.Substring(i + num_len, 1) != "}" && str.Substring(i + num_len, 1) != "]" && str.Substring(i + num_len, 1) != "," && str.Substring(i + num_len, 1) != ":") continue;
                if (num_len != 0)
                {
                    result = result.Insert(i + add, "'");
                    result = result.Insert(i + num_len + 1 + add, "'");
                    add = add + 2;
                    i = i + num_len;
                }
            }

            return result;
        }


    }
}
