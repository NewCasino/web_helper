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
        public static string root_path_teams = Environment.CurrentDirectory.Replace(@"bin\Debug", "").Replace(@"bin\x86\Debug", "") + @"data\teams\";
        string root_url_teams = @"file:///" + root_path_teams.Replace(@"\", @"/");

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
                    insert_db_500(eng_name, cn_name);


                }

            }
        } 
        public void read_500_team_detail()
        {
            //all info，include match info
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
        public void insert_db_500(string eng_name, string cn_name)
        {
            string sql = "";
            sql = " select * from teams_log where name1='{0}' ";
            sql = string.Format(sql, eng_name);
            if (SQLServerHelper.get_table(sql).Rows.Count == 0)
            {
                sql = "  insert into teams_log (website,name1,name2,name_all) values ('500','{0}','{1}','{2}')";
                sql = string.Format(sql, eng_name, cn_name, eng_name + "●" + cn_name);
                SQLServerHelper.exe_sql(sql);

                sb.AppendLine(eng_name.PR(20) + cn_name.PR(20));
                this.txt_result.Text = sb.ToString();
                Application.DoEvents();
            }
        }
        public void write_line(string file_name, string txt)
        {
            FileStream stream = (FileStream)File.Open(root_path + file_name, FileMode.Append);
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
                        Match100Helper.insert_teams_log("90vs", lg_season_name, "", lg_name, lg_name_full, team_name_eng, team_name_chi, team_name_gd);
                    }
                    sb.AppendLine(url);
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();

                }
                catch (Exception error)
                {

                    sb.AppendLine("----------------" + url);
                    this.txt_result.Text = sb.ToString();
                    Application.DoEvents();
                }


            }
            reader.Close();
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
  
        private void btn_test_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            //intsert 500 teams data to [teams_log] from [teams]
            //--------------------------------------------------------------------------------------------------------------
            //string sql = "select * from teams ";
            //DataTable dt = SQLServerHelper.get_table(sql);
            //foreach (DataRow row in dt.Rows)
            //{
            //    string name1 = row["name1"].ToString();
            //    string name2 = row["name2"].ToString(); 
            //    Match100Helper.insert_teams_log("500", "", "", "", "", name1, name2, "");
            //}

            //--------------------------------------------------------------------------------------------------------------


            //get leage english name from pinnaclesports 
            //--------------------------------------------------------------------------------------------------------------
            XmlDocument doc = new XmlDocument();
            FileStream stream = File.Open(root_path_teams + "leagues.xml", FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string xml = reader.ReadToEnd();
            reader.Close();
            stream.Close();

            doc.LoadXml(xml);
            XmlNodeList node_list = doc.SelectNodes("rsp/leagues/league");

            int count = 0;
            foreach (XmlNode node in node_list)
            {
                count = count + 1;
                string[] items = node.InnerText.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                if (items.Length > 1)
                {
                    //sb.AppendLine(items[0].ToString());
                    sb.AppendLine(count.PR(5) + items[0].PR(50) + items[1].ToString().TrimStart().TrimEnd());
                }
                else
                {
                    sb.AppendLine(count.PR(5) + "".PR(50) + node.InnerText.TrimStart().TrimEnd());
                }
                //sb.AppendLine(node.InnerText.TrimStart().TrimEnd());
            }
            this.txt_result.Text = sb.ToString();
            Application.DoEvents();
            //--------------------------------------------------------------------------------------------------------------

            //read excel
            //--------------------------------------------------------------------------------------------------------------
            //DataTable table = Tool.get_table_from_excel(root_path_teams + "leagues.xls", 3);
            //this.dgv_grid.DataSource = table;
            //--------------------------------------------------------------------------------------------------------------

            //read pin IndexPage
            //--------------------------------------------------------------------------------------------------------------
            //sb.Remove(0, sb.Length);
            //WebClient client = new WebClient();
            //string html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(root_url_teams + "pin_index_eng.html"));
            //HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
            //doc1.LoadHtml(html);
            //ArrayList list_eng = new ArrayList();
            //ArrayList list_chn = new ArrayList();
            //ArrayList list_hk = new ArrayList();
            //HtmlNodeCollection nodes_all1 = doc1.DocumentNode.SelectNodes(@"//a");
            //foreach (HtmlNode node in nodes_all1)
            //{
            //    if (!node.Attributes.Contains("href")) continue;
            //    if (!node.Attributes["href"].Value.ToLower().Contains("soccer")) continue;
            //    if (string.IsNullOrEmpty(node.InnerText.Replace(Environment.NewLine, "").Trim())) continue;
            //    string txt = node.InnerText.Replace(Environment.NewLine, "").Trim();
            //    list_eng.Add(txt);
            //}

            //html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(root_url_teams + "pin_index_chn.html"));
            //HtmlAgilityPack.HtmlDocument doc2 = new HtmlAgilityPack.HtmlDocument();
            //doc2.LoadHtml(html);
            //HtmlNodeCollection nodes_all2 = doc2.DocumentNode.SelectNodes(@"//a");
            //foreach (HtmlNode node in nodes_all2)
            //{
            //    if (!node.Attributes.Contains("href")) continue;
            //    if (!node.Attributes["href"].Value.ToLower().Contains("soccer")) continue;
            //    if (string.IsNullOrEmpty(node.InnerText.Replace(Environment.NewLine, "").Trim())) continue;
            //    string txt = node.InnerText.Replace(Environment.NewLine, "").Trim();
            //    list_chn.Add(txt);
            //}

            //html = System.Text.Encoding.GetEncoding("GBK").GetString(client.DownloadData(root_url_teams + "pin_index_hk.html"));
            //HtmlAgilityPack.HtmlDocument doc3 = new HtmlAgilityPack.HtmlDocument();
            //doc3.LoadHtml(html);
            //HtmlNodeCollection nodes_all3 = doc3.DocumentNode.SelectNodes(@"//a");
            //foreach (HtmlNode node in nodes_all3)
            //{
            //    if (!node.Attributes.Contains("href")) continue;
            //    if (!node.Attributes["href"].Value.ToLower().Contains("soccer")) continue;
            //    if (string.IsNullOrEmpty(node.InnerText.Replace(Environment.NewLine, "").Trim())) continue;
            //    string txt = node.InnerText.Replace(Environment.NewLine, "").Trim(); 
            //    list_hk.Add(txt);
            //}

            //for (int i = 0; i < list_eng.Count; i++)
            //{
            //    string txt = list_eng[i].ToString();
            //    if (txt.Contains("Half") || txt.Contains("Total") || txt.Contains("Corner") ||txt.Contains("Proposition")) continue;
            //    sb.AppendLine(list_eng[i].PR(50) + list_chn[i].PR(50) + list_hk[i].PR(50));
            //    //Match100Helper.insert_teams_log("pinnaclesports", "", list_eng[i].ToString(), "", list_chn[i].ToString(), "", "", "");
            //}
            //this.txt_result.Text = sb.ToString();
            //--------------------------------------------------------------------------------------------------------------




            MessageBox.Show("OK!");
        }
        private void btn_read_to_names_Click(object sender, EventArgs e)
        {
            string sql = "";
            sql = "select * from teams_log where website='90vs'";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                sql = "select * from names where name='{0}'";
                sql = string.Format(sql, row["name1"].ToString());
                if (SQLServerHelper.get_table(sql).Rows.Count == 0)
                {
                    sql = "insert into names   (name,name_all) values ( '{0}','{1}')";
                    sql = string.Format(sql, row["name1"].ToString(), Tool.drop_repeat(row["name_all"].ToString()));
                    SQLServerHelper.exe_sql(sql);
                }
            }

            sql = " select * from teams_log where website='500'";
            DataTable dt_500 = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_500.Rows)
            {
                string name1 = row["name1"].ToString();
                string name2 = row["name2"].ToString();
                Match100Helper.insert_name(name1, name2);
            }
            MessageBox.Show("OK");

        }
        private void btn_add_simple_complex_Click(object sender, EventArgs e)
        {
            string sql = "select * from names";
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
                    sql = "update names set name_all='{0}' where id={1}";
                    sql = string.Format(sql, Tool.drop_repeat(temp), id);
                    SQLServerHelper.exe_sql(sql);
                }
            }
            MessageBox.Show("OK");
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
