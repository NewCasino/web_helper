using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Data;

class Match100Method
{
    public BsonDocument sample(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //================================================================



        //===============================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument sample_loop(ref WebBrowser browser, BsonDocument doc_result)
    {
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        string result = "";


        string url = "";
        try
        {
            //================================================================   
            if (doc_result["url2"].AsBsonArray.Count == 0)
            {
                url = doc_result["url1"].AsBsonArray[0].ToString();
                doc_result["url2"].AsBsonArray.Add(url);
                browser.Navigate(url);
                doc_result["loop"].AsBsonArray.Add("2");

                doc_result["data"] = "Start Read First URL ->" + url;
                doc_result["url"] = browser.Document.Url.ToString();
                return doc_result;
            }
            //---------------------------------------------------------------




            //---------------------------------------------------------------
        }
        catch (Exception error)
        {

            sb.AppendLine(error.Message + Environment.NewLine + error.StackTrace);
            Log.error("from_pinnaclesports_2", error);
        }


        if (doc_result["url1"].AsBsonArray.Count == doc_result["url2"].AsBsonArray.Count)
        {
            doc_result["url"] = browser.Document.Url.ToString();
            doc_result["data"] = result;
            doc_result["loop"].AsBsonArray.Clear();
            return doc_result;
        }
        url = doc_result["url1"].AsBsonArray[doc_result["url2"].AsBsonArray.Count].ToString();
        doc_result["url2"].AsBsonArray.Add(url);
        browser.Navigate(url);

        doc_result["loop"].AsBsonArray.Clear();
        doc_result["loop"].AsBsonArray.Add("2");
        //=============================================================== 


        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }

    #region by date
    public BsonDocument from_163_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        StringBuilder sb = new StringBuilder();
        if (browser.Document == null) return doc_result; ;
        string html = browser.Document.Body.OuterHtml;

        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
        foreach (HtmlNode node in nodes_all)
        {
            if (
                node.Attributes.Contains("leaguename") &&
                node.Attributes.Contains("starttime") &&
                node.Attributes.Contains("hostname") &&
                node.Attributes.Contains("guestname") &&
                node.Attributes.Contains("isstop") && node.Attributes["isstop"].Value.ToString() == "0"
               )
            {
                try
                {
                    string root = node.XPath;
                    string league = node.Attributes["leaguename"].Value.ToString();
                    string match_code = node.Attributes["matchcode"].Value.ToString().Insert(6, "-");
                    string start_time = match_code + "●" + node.Attributes["starttime"].Value.ToString();
                    string host = node.Attributes["hostname"].Value.ToString();
                    string client = node.Attributes["guestname"].Value.ToString();
                    string win = node.SelectSingleNode(root + "/span[6]/div[1]/em[1]").InnerText.Replace("↓", "").Replace("↑", "");
                    string draw = node.SelectSingleNode(root + "/span[6]/div[1]/em[2]").InnerText.Replace("↓", "").Replace("↑", "");
                    string lose = node.SelectSingleNode(root + "/span[6]/div[1]/em[3]").InnerText.Replace("↓", "").Replace("↑", "");
                    Match100Helper.insert_data("163", league, start_time, host, client, win, draw, lose, "8", "0");
                    sb.Append(league.PR(20) + start_time.PR(20) + host.PR(20) + client.PR(20) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);
                }
                catch (Exception error) { Log.error("from 163", error); }
            }
        }

        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_500_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();

        StringBuilder sb = new StringBuilder();
        if (browser.Document == null) return doc_result; ;
        string html = browser.Document.Body.OuterHtml;

        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);
        HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
        foreach (HtmlNode node in nodes_all)
        {
            try
            {
                if (
                    node.Attributes.Contains("lg") &&
                    node.Attributes.Contains("homesxname") &&
                    node.Attributes.Contains("awaysxname") &&
                    node.Attributes.Contains("zid") &&
                    node.Attributes.Contains("mid")
                   )
                {
                    string lg_name = node.Attributes["lg"].Value.ToString();


                    HtmlNode node_time = doc.DocumentNode.SelectNodes(node.XPath + "/td[3]/span[2]")[0];
                    string time = node_time.Attributes["title"].Value.ToString().Replace("开赛时间：", "").Remove(0, 5);
                    HtmlNode node_host = doc.DocumentNode.SelectNodes(node.XPath + "/td[4]/a[1]")[0];
                    HtmlNode node_client = doc.DocumentNode.SelectNodes(node.XPath + "/td[6]/a[1]")[0];
                    HtmlNode node_win = doc.DocumentNode.SelectNodes(node.XPath + "/td[8]/div[1]/span[1]")[0];
                    HtmlNode node_draw = doc.DocumentNode.SelectNodes(node.XPath + "/td[8]/div[1]/span[2]")[0];
                    HtmlNode node_lose = doc.DocumentNode.SelectNodes(node.XPath + "/td[8]/div[1]/span[3]")[0];

                    string host = node_host.Attributes["title"].Value.ToString();
                    string client = node_client.Attributes["title"].Value.ToString();
                    string win = node_win.InnerText;
                    string draw = node_draw.InnerText;
                    string lose = node_lose.InnerText;
                    Match100Helper.insert_data("500", lg_name, time, host, client, win, draw, lose, "8", "0");
                    sb.AppendLine(time.PR(30) + lg_name.PR(10) + host.PR(20) + client.PR(20) + win.PR(10) + draw.PR(10) + lose.PR(10));


                }
            }
            catch (Exception error) { }
        }

        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_bwin_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();

        StringBuilder sb = new StringBuilder();
        if (browser.Document == null) return doc_result;
        string html = browser.Document.Body.OuterHtml;

        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);
        HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
        foreach (HtmlNode node in nodes_all)
        {
            try
            {
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "listing event")
                {

                    string root = node.XPath;
                    string league = "NO DAT";
                    string start_time = node.SelectSingleNode(root + "/h6[1]/span[2]").InnerText + "●" + node.SelectSingleNode(root + "/h6[1]/span[1]").InnerText;
                    string host = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[1]/button[1]/span[2]").InnerText;
                    string client = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[3]/button[1]/span[2]").InnerText;
                    string win = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[1]/button[1]/span[1]").InnerText;
                    string draw = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[2]/button[1]/span[1]").InnerText;
                    string lose = node.SelectSingleNode(root + "/table[1]/tbody[1]/tr[1]/td[3]/button[1]/span[1]").InnerText;
                    Match100Helper.insert_data("bwin", "", start_time, host, client, win, draw, lose, "8", "0");
                    sb.Append(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);


                }
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value.ToString() == "listing")
                {
                    string root = node.XPath;
                    string league = "NO DAT";
                    string start_time = node.SelectSingleNode(root + "/div[1]/h6[2]").InnerText + "●" + node.SelectSingleNode(root + "/div[1]/h6[1]").InnerText;
                    string host = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[1]/button[1]/span[2]").InnerText;
                    string client = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[3]/button[1]/span[2]").InnerText;
                    string win = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[1]/button[1]/span[1]").InnerText;
                    string draw = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[2]/button[1]/span[1]").InnerText;
                    string lose = node.SelectSingleNode(root + "/div[1]/table[1]/tbody[1]/tr[1]/td[3]/button[1]/span[1]").InnerText;
                    Match100Helper.insert_data("bwin", "", start_time, host, client, win, draw, lose, "8", "0");
                    sb.Append(league.PR(50) + start_time.PR(30) + host.PR(30) + client.PR(20) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);


                }
            }
            catch (Exception error) { Log.error("from bwin", error); }

        }

        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_pinnaclesports_back(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        StringBuilder sb = new StringBuilder();

        if (browser.Document == null) return doc_result; ;
        string html = "<html>" + browser.Document.Body.OuterHtml + "</html>";

        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);
        HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
        ArrayList list_lg = new ArrayList();
        ArrayList list_times = new ArrayList();
        ArrayList list_teams = new ArrayList();
        ArrayList list_odds = new ArrayList();
        foreach (HtmlNode node in nodes_all)
        {
            if (node.Name == "table" && node.Attributes.Contains("class") && node.Attributes["class"].Value == "linesTbl")
            {
                if (doc.DocumentNode.SelectSingleNode(node.XPath + "/tbody[1]/tr[2]/td[1]").InnerText.ToLower().Contains("half")) continue;
                string lg_name = "";
                string tr_path = node.XPath + "/tbody[1]/tr";
                HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(tr_path);
                foreach (HtmlNode node_tr in nodes_tr)
                {
                    switch (node_tr.Attributes["class"].Value.ToString())
                    {
                        case "linesHeader":
                            string lg_temp = doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]/h4[1]").InnerText;
                            string[] lg_list = lg_temp.Split('-');
                            lg_name = lg_list[0] + "-" + lg_list[1];
                            break;
                        case "linesAlt1":
                            if (string.IsNullOrEmpty(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim())) continue;
                            if (doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Contains("Offline")) continue;

                            list_lg.Add(lg_name);
                            list_times.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]").InnerText);
                            list_teams.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[3]").InnerText);
                            list_odds.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim());
                            break;
                        case "linesAlt2":
                            if (string.IsNullOrEmpty(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim())) continue;
                            if (doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Contains("Offline")) continue;

                            list_lg.Add(lg_name);
                            list_times.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]").InnerText);
                            list_teams.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[3]").InnerText);
                            list_odds.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim());
                            break;
                        default:
                            break;
                    }

                }
            }
        }
        for (int i = 0; i < list_lg.Count; i++)
        {
            if ((i + 2) < list_lg.Count)
            {
                string f_lg = list_lg[i].ToString();
                string f_time = list_times[i].ToString() + " " + list_times[i + 1].ToString();
                string f_host = list_teams[i].ToString();
                string f_client = list_teams[i + 1].ToString();
                string f_win = list_odds[i].ToString();
                string f_draw = list_odds[i + 2].ToString();
                string f_lose = list_odds[i + 1].ToString();
                Match100Helper.insert_data("pinnaclesports", f_lg, f_time, f_host, f_client, f_win, f_draw, f_lose, "-7", "1");
                sb.AppendLine(f_lg.PR(50) + f_time.PR(20) + f_host.PR(30) + f_client.PR(30) + f_win.PR(20) + f_draw.PR(20) + f_lose.PR(20));
            }
            i = i + 2;
        }

        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_pinnaclesports_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();


        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //================================================================
        BsonArray url1 = new BsonArray();
        BsonArray url2 = new BsonArray();
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
                    if (str_class == "Soccer" && !name.Contains("Halfs") && !name.Contains("Totals") && !href.Contains("ContestCategory"))
                    {
                        count = count + 1;
                        sb.AppendLine(count.PR(5) + str_class.PR(20) + name.PR(50) + href);
                        url1.Add("http://www.pinnaclesports.com" + href);
                    }
                }
            }
        }
        //===============================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        doc_result.Add("url1", url1);
        doc_result.Add("url2", url2);
        return doc_result;
    }
    public BsonDocument from_pinnaclesports_2(ref WebBrowser browser, BsonDocument doc_result)
    {
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        string result = "";


        string url = "";
        try
        {
            //================================================================   
            if (doc_result["url2"].AsBsonArray.Count == 0)
            {
                url = doc_result["url1"].AsBsonArray[0].ToString();
                doc_result["url2"].AsBsonArray.Add(url);
                browser.Navigate(url);
                doc_result["loop"].AsBsonArray.Add("2");

                doc_result["data"] = "Start Read First URL ->" + url;
                doc_result["url"] = browser.Document.Url.ToString();
                return doc_result;
            }
            //---------------------------------------------------------------
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            ArrayList list_lg = new ArrayList();
            ArrayList list_times = new ArrayList();
            ArrayList list_teams = new ArrayList();
            ArrayList list_odds = new ArrayList();
            foreach (HtmlNode node in nodes_all)
            {

                if (node.Name == "table" && node.Attributes.Contains("class") && node.Attributes["class"].Value == "linesTbl")
                {
                    if (doc.DocumentNode.SelectSingleNode(node.XPath + "/tbody[1]/tr[2]/td[1]").InnerText.ToLower().Contains("half")) continue;
                    string lg_name = "";
                    string tr_path = node.XPath + "/tbody[1]/tr";
                    HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(tr_path);
                    foreach (HtmlNode node_tr in nodes_tr)
                    {
                        switch (node_tr.Attributes["class"].Value.ToString())
                        {
                            case "linesHeader":
                                string lg_temp = doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]/h4[1]").InnerText;
                                string[] lg_list = lg_temp.Split('-');
                                lg_name = lg_list[0] + "-" + lg_list[1];
                                break;
                            case "linesAlt1":
                                if (string.IsNullOrEmpty(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim())) continue;
                                if (doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Contains("Offline")) continue;

                                list_lg.Add(lg_name);
                                list_times.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]").InnerText);
                                list_teams.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[3]").InnerText);
                                list_odds.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim());
                                break;
                            case "linesAlt2":
                                if (string.IsNullOrEmpty(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim())) continue;
                                if (doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Contains("Offline")) continue;

                                list_lg.Add(lg_name);
                                list_times.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]").InnerText);
                                list_teams.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[3]").InnerText);
                                list_odds.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim());
                                break;
                            default:
                                break;
                        }

                    }
                }
            }
            for (int i = 0; i < list_lg.Count; i++)
            {
                if ((i + 2) < list_lg.Count)
                {
                    string f_lg = list_lg[i].ToString();
                    string f_time = list_times[i].ToString() + " " + list_times[i + 1].ToString();
                    string f_host = list_teams[i].ToString();
                    string f_client = list_teams[i + 1].ToString();
                    string f_win = list_odds[i].ToString();
                    string f_draw = list_odds[i + 2].ToString();
                    string f_lose = list_odds[i + 1].ToString();
                    Match100Helper.insert_data("pinnaclesports", f_lg, f_time, f_host, f_client, f_win, f_draw, f_lose, "-7", "1");
                    sb.AppendLine(f_lg.PR(50) + f_time.PR(20) + f_host.PR(30) + f_client.PR(30) + f_win.PR(20) + f_draw.PR(20) + f_lose.PR(20));
                }
                i = i + 2;
            }

        }
        catch (Exception error)
        {

            sb.AppendLine(error.Message + Environment.NewLine + error.StackTrace);
            Log.error("from_pinnaclesports_2", error);
        }

        //--------------------------------------------------------------
        if (doc_result["url1"].AsBsonArray.Count == doc_result["url2"].AsBsonArray.Count)
        {
            doc_result["url"] = browser.Document.Url.ToString();
            doc_result["data"] = result;
            doc_result["loop"].AsBsonArray.Clear();
            return doc_result;
        }
        url = doc_result["url1"].AsBsonArray[doc_result["url2"].AsBsonArray.Count].ToString();
        doc_result["url2"].AsBsonArray.Add(url);
        browser.Navigate(url);

        doc_result["loop"].AsBsonArray.Clear();
        doc_result["loop"].AsBsonArray.Add("2");
        //=============================================================== 


        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }

    //2014-08-28
    public BsonDocument from_188bet_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
            if (node.CLASS() == "comp-title" && node.Name == "thead")
            {
                league = node.SELECT_NODE("/tr[1]/th[1]").InnerText;
            }
            if (node.CLASS() == "comp-container")
            {
                HtmlNodeCollection nodes_tr = node.SELECT_NODES("/table[1]/tbody[1]/tr");
                foreach (HtmlNode node_tr in nodes_tr)
                {
                    start_time = node_tr.SELECT_NODE("/td[1]").InnerText.E_TRIM();
                    host = node_tr.SELECT_NODE("/td[2]/div[2]/div[1]").InnerText;
                    client = node_tr.SELECT_NODE("/td[2]/div[2]/div[2]").InnerText;

                    win = node_tr.SELECT_NODE("/td[3]").InnerText;
                    draw = node_tr.SELECT_NODE("/td[4]").InnerText;
                    lose = node_tr.SELECT_NODE("/td[5]").InnerText;
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("188bet", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
        }
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_188bet_2(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        string txt = BrowserHelper.get_attr_by_id(ref browser, "btm_NextBtn", "class");
        if (!txt.Contains("disabled"))
        {
            BrowserHelper.invoke_click_by_id(ref browser, "btm_NextBtn");
            doc_result["loop"].AsBsonArray.Add("1");
            doc_result["loop"].AsBsonArray.Add("2");
        }
        doc_result["data"] = "Invoke OK!" + txt;
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_macauslot_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_10bet_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        BrowserHelper.invoke_click_by_id(ref browser, "tp_chk_br_999_l_1_1");
        doc_result["data"] = "Invoke Click!";
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_10bet_2(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_fubo_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    Match100Helper.insert_data("fubo", league, start_time, host, client, win, draw, lose, "8", "0");
                }
            }
        }
        //====================================================== 
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;


    }
    public BsonDocument from_fun88_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
                            Match100Helper.insert_data("fun88", league, start_time, host, client, win, draw, lose, "8", "0");
                        }
                    }
                    catch (Exception error) { sb.Append(error.Message); }
                }
            }
        }
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }

    //2014-08-29 
    public BsonDocument from_bet16_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        BrowserHelper.invoke_click_by_id(ref browser, "1_1X2_Cnt");
        doc_result["data"] = "Invoke Click!";
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_bet16_2(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================

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
                        Match100Helper.insert_data("bet16", league, start_time, host, client, win, draw, lose, "8", "0");
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                    }

                }

            }
        }
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_betvictor_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();

        //======================================================
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
                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                Match100Helper.insert_data("betvictor", league, start_time, host, client, win, draw, lose, "2", "0");
            }
        }
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_interwetten_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
                start_time = start_time.E_TRIM();
                win = win.Replace(",", ".");
                draw = draw.Replace(",", ".");
                lose = lose.Replace(",", ".");
                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                Match100Helper.insert_data("interwetten", league, start_time, host, client, win, draw, lose, "8", "0");
            }
        }
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_sbobet_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
                        Match100Helper.insert_data("sobobet", league, start_time, host, client, win, draw, lose, "8", "0");
                    }

                }

            }
        }
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_mansion88_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        BrowserHelper.invoke_click_by_id(ref browser, "1_1X2_Cnt");
        doc_result["data"] = "Invoke Click!";
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_mansion88_2(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //====================================================== 

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
                    Match100Helper.insert_data("mansion88", league, start_time, host, client, win, draw, lose, "8", "0");
                    sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                }
            }
        }
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }

    //2014-09-02
    public BsonDocument from_sportbet_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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


        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_victorbet_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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

                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("victorbet", league, start_time, host, client, win, draw, lose, "8", "0");
                    }
                    catch (Exception error) { }

                }

            }
        }

        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_marathonbet_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
                            Match100Helper.insert_data("marathonbet", league, start_time, host, client, win, draw, lose, "0", "0");
                        }

                    }
                }
            }
        }
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_coral_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();

        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();


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
                start_time = node.SELECT_NODE("/div[1]").ChildNodes[0].InnerText.Replace(" ", M.D).E_TRIM();
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
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }

    //2014-09-03
    public BsonDocument from_gamebookers_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_oddring_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_snai_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_12bet_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        BrowserHelper.invoke_click_by_id(ref browser, "1_1X2_Cnt");
        doc_result["data"] = "Invoke Click!";
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_12bet_2(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================

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
                if (!start_time.Contains("{"))
                {
                    string str_teams = node.SELECT_NODE("/td[2]/b[1]").InnerHtml;
                    string[] teams = str_teams.E_SPLIT("<br>");
                    if (teams.Length == 2)
                    {
                        host = teams[0];
                        client = teams[1];
                    }
                    win = node.SELECT_NODE("/td[4]").InnerText;
                    draw = node.SELECT_NODE("/td[5]").InnerText;
                    lose = node.SELECT_NODE("/td[6]").InnerText;
                    //string str_odds = node.SELECT_NODE("/td[6]/div[1]").InnerHtml;
                    //string[] odds = str_odds.E_SPLIT("<br>");
                    //if (odds.Length == 3)
                    //{
                    //    win = odds[0]; draw = odds[2]; lose = odds[1];
                    //}
                    //else
                    //{
                    //    win = ""; draw = ""; lose = "";
                    //}
                    if (!league.Contains("LeagueName") && !string.IsNullOrEmpty(win.Trim()) && !start_time.Contains("-"))
                    {
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("12bet", league, start_time, host, client, win, draw, lose, "8", "0");
                    }
                }
            }

        }
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_1bet_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        BrowserHelper.invoke_click_by_id(ref browser, "T_1_Name");
        doc_result["data"] = "Invoke Click!";
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_1bet_2(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================

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
                    start_time = Tool.get_12m_from_eng(start_time.Substring(0, 3)) + "-" + start_time.Substring(3, start_time.Length - 3).E_TRIM();
                    if (!mark.Contains("half"))
                    {
                        sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                        Match100Helper.insert_data("1bet", league, start_time, host, client, win, draw, lose, "2", "0");
                    }
                }
            }

        }

        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_youwin_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //======================================================
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
        //======================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }

    //2014-10-20
    public BsonDocument from_gobetgo_1_back(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //================================================================
        html = html.Replace("<thead=\"\"", "");

        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
        BrowserHelper.invoke_click_by_outerhtml(ref browser, "<OPTION class=en value=en-US>English</OPTION>");
        //foreach (HtmlNode node in nodes_all)
        //{
        //    if (node.Name.ToLower() == "option" && node.CLASS() == "en")
        //    {
        //        BrowserHelper.invoke_click_by_outerhtml(ref browser, node.OuterHtml);
        //        sb.AppendLine("Invoke Click!!!");
        //    }
        //}


        //===============================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_gobetgo_1(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //================================================================
        html = html.Replace("<thead=\"\"", "");

        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");

        foreach (HtmlNode node in nodes_all)
        {
            if (node.Name == "a" && node.SELECT_NODE("/span[1]") != null && node.SELECT_NODE("span[1]").InnerText == "SOCCER")
            {
                BrowserHelper.invoke_click_by_outerhtml(ref browser,node.OuterHtml);
                sb.AppendLine("Invoke Click!!!");
            }
        }


        //===============================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    public BsonDocument from_gobetgo_2(ref WebBrowser browser, BsonDocument doc_result)
    {

        doc_result = Match100Helper.get_doc_result();


        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        //================================================================
        BsonArray url1 = new BsonArray();
        BsonArray url2 = new BsonArray();
        html = html.Replace("<thead=\"\"", "");

        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");


        foreach (HtmlNode node in nodes_all)
        {
            if (node.Name == "a" && node.CLASS() == "item_submenu_link")
            {
                url1.Add(node.OuterHtml);
                sb.AppendLine(node.OuterHtml);
            }
        }
        //===============================================================
        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        doc_result.Add("url1", url1);
        doc_result.Add("url2", url2);
        return doc_result;
    }
    public BsonDocument from_gobetgo_3(ref WebBrowser browser, BsonDocument doc_result)
    {
        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        string result = "";


        string url = "";

        try
        {
            //================================================================   
            if (doc_result["url2"].AsBsonArray.Count == 0)
            {
                url = doc_result["url1"].AsBsonArray[0].ToString();
                doc_result["url2"].AsBsonArray.Add(url);
                BrowserHelper.invoke_click_by_outerhtml(ref browser, url);
                doc_result["loop"].AsBsonArray.Add("4");

                doc_result["data"] = "Start Read Index URL ->" + url;
                doc_result["url"] = browser.Document.Url.ToString();
                return doc_result;
            }
            //---------------------------------------------------------------
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");

            string league = "";
            string start_time = "";
            string host = "";
            string client = "";
            string win = "";
            string draw = "";
            string lose = "";
            string date = "";
            string time = "";
            try
            {
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
                                date = node_tr.SELECT_NODE("/td[1]/header[1]/div[2]").ChildNodes[0].InnerText.E_SPLIT("/")[0] + "-" + node_tr.SELECT_NODE("/td[1]/header[1]/div[2]").ChildNodes[0].InnerText.E_SPLIT("/")[1];
                                time = node_tr.SELECT_NODE("/td[1]/header[1]/div[2]").ChildNodes[1].InnerText;
                                start_time = date + M.D + time;
                                host = node_tr.SELECT_NODE("/td[1]/header[1]/div[1]/span[1]").InnerText;
                                client = node_tr.SELECT_NODE("/td[1]/header[1]/div[1]/span[2]").InnerText;
                                win = node_tr.SELECT_NODE("/td[1]/div[1]/div[1]/span[2]").InnerText;
                                draw = node_tr.SELECT_NODE("/td[1]/div[2]/div[1]/span[2]").InnerText;
                                lose = node_tr.SELECT_NODE("/td[1]/div[3]/div[1]/span[2]").InnerText;
                                sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                if (!string.IsNullOrEmpty(win.E_TRIM()))
                                {
                                    Match100Helper.insert_data("gobetgo", league, start_time, host, client, win, draw, lose, "0", "0");
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception error)
            {
                sb.AppendLine(error.ToString());
            }
            //--------------------------------------------------------------
            if (doc_result["url1"].AsBsonArray.Count == doc_result["url2"].AsBsonArray.Count)
            {
                doc_result["url"] = browser.Document.Url.ToString();
                doc_result["data"] = result;
                doc_result["loop"].AsBsonArray.Clear();
                return doc_result;
            }
            url = doc_result["url1"].AsBsonArray[doc_result["url2"].AsBsonArray.Count].ToString();
            doc_result["url2"].AsBsonArray.Add(url);
            BrowserHelper.invoke_click_by_outerhtml(ref browser, url);

            doc_result["loop"].AsBsonArray.Clear();
            doc_result["loop"].AsBsonArray.Add("4");
            //=============================================================== 

        }
        catch(Exception error2)
        {
            sb.AppendLine(error2.ToString()); 
        }

        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;

    }
    #endregion

    #region two function for test in website

    public BsonDocument from_baidu_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        StringBuilder sb = new StringBuilder();

        if (browser.Document == null) return doc_result;
        HtmlElementCollection elements = browser.Document.Body.All;
        foreach (HtmlElement element in elements)
        {
            string row = "";
            IHTMLElement el = (IHTMLElement)element.DomElement;
            IHTMLDOMNode nd = (IHTMLDOMNode)el;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)nd.attributes;

            row += el.tagName.PR(10);
            row += el.className.PR(10);
            row += el.id.PR(10);
            row += ((IHTMLElementCollection)el.children).length.PR(10);

            int left = el.offsetLeft;
            int top = el.offsetTop;
            BrowserHelper.get_absolute(ref el, ref left, ref top);

            row += left.PR(10);
            row += top.PR(10);
            row += el.offsetLeft.PR(10);
            row += el.offsetTop.PR(10);
            row += el.offsetWidth.PR(10);
            row += el.offsetHeight.PR(10);
            row += el.innerText.PR(100);

            if (((IHTMLElementCollection)el.children).length != 0) continue;
            if (string.IsNullOrEmpty(el.innerText)) continue;
            if (!string.IsNullOrEmpty(row)) sb.AppendLine(row);
        }

        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    public BsonDocument from_local_1(ref WebBrowser browser, BsonDocument doc_result)
    {
        doc_result = Match100Helper.get_doc_result();
        StringBuilder sb = new StringBuilder();
        HtmlElementCollection list = browser.Document.Body.All;
        foreach (HtmlElement element in list)
        {
            if (element.Id == "btn_ok") element.InvokeMember("click");
            if (element.Id == "txt") sb.AppendLine(element.GetAttribute("value"));
        }

        //if (browser.Document == null) return doc_result;  
        //BrowserHelper.invoke_click_by_id(ref browser, "btn_ok"); 

        doc_result["data"] = sb.ToString();
        doc_result["url"] = browser.Document.Url.ToString();
        return doc_result;
    }
    #endregion

    #region back the function get content from position
    //public BsonDocument from_fubo_1_back(ref WebBrowser browser)
    //{
    //    BsonDocument doc_result = Match100Helper.get_doc_result();
    //    BsonDocument doc_condition = BrowserHelper.get_doc_condition();

    //    string result = "";
    //    //try
    //    //{
    //    DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
    //    DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
    //    ArrayList times = new ArrayList();
    //    ArrayList teams = new ArrayList();
    //    ArrayList wins = new ArrayList();
    //    ArrayList draws = new ArrayList();
    //    ArrayList loses = new ArrayList();

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {

    //        if (string.IsNullOrEmpty(dt.Rows[i][3].ToString())) continue;
    //        times.Add(dt.Rows[i][1].ToString());
    //        teams.Add(dt.Rows[i][2].ToString());
    //        wins.Add(dt.Rows[i][3].ToString());
    //        draws.Add(dt.Rows[i][4].ToString());
    //        loses.Add(dt.Rows[i][5].ToString());
    //    }

    //    int min_count = 999999;
    //    if (times.Count < min_count) min_count = times.Count;
    //    if (teams.Count < min_count) min_count = teams.Count;
    //    if (wins.Count < min_count) min_count = wins.Count;
    //    if (draws.Count < min_count) min_count = draws.Count;
    //    if (loses.Count < min_count) min_count = loses.Count;

    //    for (int i = 0; i < min_count; i++)
    //    {
    //        //string[] single_times = times[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
    //        string str_time = times[i].ToString().Replace("滚球", "").Replace("●", "").Trim();
    //        string[] single_teams = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
    //        string f_time = str_time;
    //        string f_host = single_teams[0].ToString();
    //        string f_client = single_teams[1].ToString();
    //        string f_win = wins[i].ToString();
    //        string f_draw = draws[i].ToString();
    //        string f_lose = loses[i].ToString();
    //        Match100Helper.insert_data("fubo", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
    //        result = result + str_time.PR(20) + single_teams[0].PR(50) + single_teams[1].PR(50) + wins[i].PR(20) + draws[i].PR(20) + loses[i].PR(20) + Environment.NewLine;
    //    }
    //    //}
    //    //catch (Exception error)
    //    //{
    //    //    result = error.Message + Environment.NewLine + error.StackTrace;
    //    //}
    //    doc_result["data"] = result;
    //    return doc_result;
    //}
    //public BsonDocument from_pinnaclesports_1_back(ref WebBrowser browser)
    //{
    //    BsonDocument doc_result = Match100Helper.get_doc_result();
    //    string result = "";
    //    //try
    //    //{
    //    BsonDocument doc_condition = BrowserHelper.get_doc_condition();
    //    DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
    //    DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
    //    ArrayList times = new ArrayList();
    //    ArrayList teams = new ArrayList();
    //    ArrayList odds = new ArrayList();

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        if (!string.IsNullOrEmpty(dt.Rows[i][4].ToString()) && dt.Rows[i][4].ToString().Contains("1X2") == false)
    //        {
    //            times.Add(dt.Rows[i][0].ToString());
    //            teams.Add(dt.Rows[i][2].ToString());
    //            odds.Add(dt.Rows[i][4].ToString());
    //        }
    //    }


    //    for (int i = 0; i < times.Count; i++)
    //    {
    //        if ((i + 2) < times.Count)
    //        {
    //            string f_time = times[i].ToString() + " " + times[i + 1].ToString();
    //            string f_host = teams[i].ToString();
    //            string f_client = teams[i + 1].ToString();
    //            string f_win = odds[i].ToString();
    //            string f_draw = odds[i + 2].ToString();
    //            string f_lose = odds[i + 1].ToString();
    //            Match100Helper.insert_data("pinnaclesports", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "-7", "0");
    //            result = result + f_time.PR(20) + f_host.PR(50) + f_client.PR(50) + f_win.PR(20) + f_draw.PR(20) + f_lose.PR(20) + Environment.NewLine;
    //        }
    //        i = i + 2;
    //    }
    //    //}
    //    //catch (Exception error)
    //    //{
    //    //    result = error.Message + Environment.NewLine + error.StackTrace;
    //    //}
    //    doc_result["data"] = result;
    //    return doc_result;
    //}
    //public BsonDocument from_188bet_1_back(ref WebBrowser browser)
    //{
    //    BsonDocument doc_result = Match100Helper.get_doc_result();
    //    BsonDocument doc_condition = BrowserHelper.get_doc_condition();

    //    string result = "";
    //    try
    //    {
    //        DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
    //        DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
    //        ArrayList times = new ArrayList();
    //        ArrayList teams = new ArrayList();
    //        ArrayList wins = new ArrayList();
    //        ArrayList draws = new ArrayList();
    //        ArrayList loses = new ArrayList();

    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {

    //            if (string.IsNullOrEmpty(dt.Rows[i][4].ToString())) continue;
    //            times.Add(dt.Rows[i][0].ToString());
    //            teams.Add(dt.Rows[i][3].ToString());
    //            wins.Add(dt.Rows[i][4].ToString());
    //            draws.Add(dt.Rows[i][6].ToString());
    //            loses.Add(dt.Rows[i][8].ToString());
    //        }

    //        int min_count = 999999;
    //        if (times.Count < min_count) min_count = times.Count;
    //        if (teams.Count < min_count) min_count = teams.Count;
    //        if (wins.Count < min_count) min_count = wins.Count;
    //        if (draws.Count < min_count) min_count = draws.Count;
    //        if (loses.Count < min_count) min_count = loses.Count;

    //        for (int i = 0; i < min_count; i++)
    //        {
    //            string[] single_times = times[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
    //            string[] single_teams = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);

    //            string f_time = times[i].ToString();
    //            string f_host = single_teams[0].ToString();
    //            string f_client = single_teams[1].ToString();
    //            string f_win = wins[i].ToString();
    //            string f_draw = draws[i].ToString();
    //            string f_lose = loses[i].ToString();
    //            //Match100Helper.insert_data("188be", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
    //            result = result + (single_times[0].ToString().Trim() + " " + single_times[1].ToString()).PR(20) + single_teams[0].PR(50) + single_teams[1].PR(50) + wins[i].PR(20) + draws[i].PR(20) + loses[i].PR(20) + Environment.NewLine;
    //        }
    //    }
    //    catch (Exception error)
    //    {
    //        result = error.Message + Environment.NewLine + error.StackTrace;
    //    }
    //    doc_result["data"] = result;
    //    return doc_result;
    //}
    //public BsonDocument from_macauslot_1_back(ref WebBrowser browser)
    //{
    //    BsonDocument doc_result = Match100Helper.get_doc_result();
    //    string result = "";
    //    //try
    //    //{
    //    BsonDocument doc_condition = BrowserHelper.get_doc_condition();
    //    DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
    //    DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
    //    ArrayList times = new ArrayList();
    //    ArrayList teams = new ArrayList();
    //    ArrayList odds = new ArrayList();

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        if (!string.IsNullOrEmpty(dt.Rows[i][7].ToString()) && dt.Rows[i][7].ToString().ToLower().Contains("win") == false)
    //        {
    //            times.Add(dt.Rows[i][0].ToString());
    //            teams.Add(dt.Rows[i][2].ToString());
    //            odds.Add(dt.Rows[i][7].ToString());
    //        }
    //    }


    //    for (int i = 0; i < times.Count; i++)
    //    {

    //        if ((i + 2) < times.Count)
    //        {
    //            string[] single_times = times[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
    //            string[] single_host = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
    //            string[] single_client = teams[i + 1].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
    //            string f_time = single_times[0].ToString() + "●" + single_times[1].ToString();
    //            string f_host = single_host[0].ToString();
    //            string f_client = single_client[0].ToString();
    //            string f_win = odds[i].ToString();
    //            string f_draw = odds[i + 2].ToString();
    //            string f_lose = odds[i].ToString();
    //            Match100Helper.insert_data("macauslot", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
    //            result = result + (single_times[0].ToString() + " " + single_times[1].ToString()).PR(20) + single_host[0].PR(50) + single_client[0].PR(50) + odds[i].PR(20) + odds[i + 2].PR(20) + odds[i + 1].PR(20) + Environment.NewLine;
    //        }
    //        i = i + 2;
    //    }
    //    //}
    //    //catch (Exception error)
    //    //{
    //    //    result = error.Message + Environment.NewLine + error.StackTrace;
    //    //}
    //    doc_result["data"] = result;
    //    return doc_result;
    //}
    //public BsonDocument from_10bet_2_back(ref WebBrowser browser)
    //{
    //    BsonDocument doc_result = Match100Helper.get_doc_result();
    //    BsonDocument doc_condition = BrowserHelper.get_doc_condition();
    //    doc_condition["element_type"].AsBsonArray.Add("div");
    //    string result = "";
    //    //try
    //    //{
    //    DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
    //    DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
    //    ArrayList times = new ArrayList();
    //    ArrayList teams = new ArrayList();
    //    ArrayList wins = new ArrayList();
    //    ArrayList draws = new ArrayList();
    //    ArrayList loses = new ArrayList();

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        if (string.IsNullOrEmpty(dt.Rows[i][3].ToString())) continue;
    //        times.Add(dt.Rows[i][1].ToString());
    //        teams.Add(dt.Rows[i][2].ToString());
    //        wins.Add(dt.Rows[i][3].ToString());
    //        draws.Add(dt.Rows[i][4].ToString());
    //        loses.Add(dt.Rows[i][6].ToString());
    //    }

    //    int min_count = 999999;
    //    if (times.Count < min_count) min_count = times.Count;
    //    if (teams.Count < min_count) min_count = teams.Count;
    //    if (wins.Count < min_count) min_count = wins.Count;
    //    if (draws.Count < min_count) min_count = draws.Count;
    //    if (loses.Count < min_count) min_count = loses.Count;

    //    for (int i = 0; i < min_count; i++)
    //    {
    //        string[] single_teams = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
    //        string f_time = times[i].ToString();
    //        string f_host = single_teams[0].ToString();
    //        string f_client = single_teams[2].ToString();
    //        string f_win = wins[i].ToString();
    //        string f_draw = draws[i].ToString();
    //        string f_lose = loses[i].ToString();
    //        Match100Helper.insert_data("10bet", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
    //        result = result + times[i].PR(20) + single_teams[0].PR(50) + single_teams[2].PR(50) + wins[i].PR(20) + draws[i].PR(20) + loses[i].PR(20) + Environment.NewLine;
    //    }
    //    //}
    //    //catch (Exception error)
    //    //{
    //    //    result = error.Message + Environment.NewLine + error.StackTrace;
    //    //}
    //    doc_result["data"] = result;
    //    return doc_result;
    //}
    //public BsonDocument from_fun88_1_back(ref WebBrowser browser)
    //{
    //    BsonDocument doc_result = Match100Helper.get_doc_result();
    //    string result = "";
    //    //try
    //    //{
    //    BsonDocument doc_condition = BrowserHelper.get_doc_condition();
    //    DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
    //    DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
    //    ArrayList times = new ArrayList();
    //    ArrayList teams = new ArrayList();
    //    ArrayList odds = new ArrayList();

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        if (!string.IsNullOrEmpty(dt.Rows[i][5].ToString()) && dt.Rows[i][5].ToString().Contains("1X2") == false)
    //        {
    //            times.Add(dt.Rows[i][1].ToString());
    //            teams.Add(dt.Rows[i][2].ToString());
    //            odds.Add(dt.Rows[i][5].ToString());
    //        }
    //    }


    //    for (int i = 0; i < times.Count; i++)
    //    {

    //        string[] single_times = times[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
    //        string[] single_teams = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
    //        string[] single_odds = odds[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);

    //        string f_time = times[i].ToString();
    //        string f_host = single_teams[0].ToString();
    //        string f_client = single_teams[1].ToString();
    //        string f_win = single_odds[0].ToString();
    //        string f_draw = single_odds[2].ToString();
    //        string f_lose = single_odds[1].ToString();
    //        Match100Helper.insert_data("fun88", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
    //        result = result + single_times[1].PR(20) + single_teams[0].PR(50) + single_teams[1].PR(50) + single_odds[0].PR(20) + single_odds[2].PR(20) + single_odds[1].PR(20) + Environment.NewLine;
    //    }
    //    //}
    //    //catch (Exception error)
    //    //{
    //    //    result = error.Message + Environment.NewLine + error.StackTrace;
    //    //}
    //    doc_result["data"] = result;
    //    return doc_result;
    //}
    #endregion
}
