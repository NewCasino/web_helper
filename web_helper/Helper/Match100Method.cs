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
    public BsonDocument from_baidu_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
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
        return doc_result;
    }
    public BsonDocument from_local_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        string result = "";
        HtmlElementCollection list = browser.Document.Body.All;
        foreach (HtmlElement element in list)
        {
            if (element.Id == "btn_ok") element.InvokeMember("click");
            if (element.Id == "txt") result = element.GetAttribute("value");
        }
        doc_result["data"] = result;
        return doc_result;
    }

    //-----------------------------------------------------------

    public BsonDocument from_163_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();

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
        return doc_result;
    }
    public BsonDocument from_500_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();

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
        return doc_result;
    } 

    public BsonDocument from_bwin_1(ref WebBrowser browser)
    {

        BsonDocument doc_result = Match100Helper.get_doc_result();

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
                    string start_time = node.SelectSingleNode(root + "/h6[1]/span[1]").InnerText;
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
                    string start_time = node.SelectSingleNode(root + "/div[1]/h6[1]").InnerText;
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
        return doc_result;
    } 

    public BsonDocument from_10bet_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        BrowserHelper.invoke_click_by_id(ref browser, "tp_chk_br_999_l_1_1");
        doc_result["data"] = "Invoke Click!";
        return doc_result;
    }
    public BsonDocument from_10bet_2(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();

        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();

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


        doc_result["data"] = sb.ToString();
        return doc_result;

    } 

    public BsonDocument from_macauslot_1(ref WebBrowser browser)
    {

        BsonDocument doc_result = Match100Helper.get_doc_result();
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();

        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();

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


        doc_result["data"] = sb.ToString();
        return doc_result;

    } 

    public BsonDocument from_pinnaclesports_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        string result = "";

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
                            if(string.IsNullOrEmpty(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim())) continue;
                            if (doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Contains("Offline")) continue;

                            list_lg.Add(lg_name);
                            list_times.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[1]").InnerText);
                            list_teams.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[3]").InnerText);
                            list_odds.Add(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim());
                            break;
                        case "linesAlt2":
                            if(string.IsNullOrEmpty(doc.DocumentNode.SelectSingleNode(node_tr.XPath + "/td[6]").InnerText.Replace("&nbsp;", "").Trim())) continue;
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
                result = result +f_lg.PR(50)+ f_time.PR(20) + f_host.PR(30) + f_client.PR(30) + f_win.PR(20) + f_draw.PR(20) + f_lose.PR(20) + Environment.NewLine;
            }
            i = i + 2;
        }

        doc_result["data"] = result;
        return doc_result;
    } 
  
    public BsonDocument from_188bet_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();

        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();
        
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
        List<HtmlNode> nodes = new List<HtmlNode>();

        ArrayList list_lg = new ArrayList();
        foreach (HtmlNode node in nodes_all)
        {
            if (node.Attributes.Contains("class") && node.Attributes["class"].Value.Trim() == "comp-txt-wrapper")
            {
                list_lg.Add(node.InnerText);
            }
        }


        int index = 0;
        foreach (HtmlNode node in nodes_all)
        {
            if (node.Attributes.Contains("class") && node.Attributes["class"].Value.Trim() == "comp-container")
            {
                string path1 = node.XPath;
                HtmlNodeCollection nodes_tr = doc.DocumentNode.SelectNodes(path1 + "/table[1]/tbody[1]/tr");

                foreach (HtmlNode node_tr in nodes_tr)
                {
                    string path2 = node_tr.XPath;
                    string date = "";
                    string time = "";
                    HtmlNodeCollection nodes_time = doc.DocumentNode.SelectNodes(path2 + "/td[1]/div");
                    foreach (HtmlNode node_time in nodes_time)
                    {
                        if (node_time.Attributes.Contains("class") && node_time.Attributes["class"].Value.Trim() == "date") date = node_time.InnerText;
                        if (node_time.Attributes.Contains("class") && node_time.Attributes["class"].Value.Trim() == "start-time") time = node_time.InnerText;
                    }

                    string[] str_dates = date.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    string start_time = str_dates[1] + "-" + str_dates[0] + "●" + time;

                    string league = list_lg[index].ToString();
                    string host = doc.DocumentNode.SelectSingleNode(path2 + "/td[2]/div[2]/div[1]/span[1]").InnerText;
                    string client = doc.DocumentNode.SelectSingleNode(path2 + "/td[2]/div[2]/div[2]/span[1]").InnerText;
                    string win = doc.DocumentNode.SelectSingleNode(path2 + "/td[3]/span[1]").InnerText;
                    string draw = doc.DocumentNode.SelectSingleNode(path2 + "/td[4]/span[1]").InnerText;
                    string lose = doc.DocumentNode.SelectSingleNode(path2 + "/td[5]/span[1]").InnerText;
                    if (!league.Contains("Specials"))
                    {
                        sb.AppendLine(league.PR(80) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10)); 
                        Match100Helper.insert_data("188bet", league, start_time, host, client, win, draw, lose, "8", "0");
                    }
                }
                index = index + 1;
            }
        }
        doc_result["data"] = sb.ToString();
        return doc_result;
    }
    public BsonDocument from_188bet_2(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        string txt = BrowserHelper.get_attr_by_id(ref browser, "btm_NextBtn", "class");
        if (!txt.Contains("disabled"))
        {
            BrowserHelper.invoke_click_by_id(ref browser, "btm_NextBtn");
            doc_result["loop"].AsBsonArray.Add("1");
            doc_result["loop"].AsBsonArray.Add("2"); 
        }
        doc_result["data"] = "Invoke OK!"+txt;
      
        return doc_result;

    } 
   
    public BsonDocument from_fun88_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();

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
                        if (!string.IsNullOrEmpty(win) && !string.IsNullOrEmpty(client) && !string.IsNullOrEmpty(win))
                        {
                            sb.AppendLine(league.PR(50) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                            Match100Helper.insert_data("fun88", league, start_time, host, client, win, draw, lose, "8", "0");
                        }
                    }
                    catch (Exception error) { }
                }
            }
        }

        doc_result["data"] = sb.ToString();
        return doc_result;
    } 
 
    public BsonDocument from_fubo_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();

        string html = BrowserHelper.get_html(ref browser);
        StringBuilder sb = new StringBuilder();

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


        doc_result["data"] = sb.ToString();
        return doc_result;


    }



    //back the function get content from position
    public BsonDocument from_fubo_1_back(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();

        string result = "";
        //try
        //{
        DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
        DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
        ArrayList times = new ArrayList();
        ArrayList teams = new ArrayList();
        ArrayList wins = new ArrayList();
        ArrayList draws = new ArrayList();
        ArrayList loses = new ArrayList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            if (string.IsNullOrEmpty(dt.Rows[i][3].ToString())) continue;
            times.Add(dt.Rows[i][1].ToString());
            teams.Add(dt.Rows[i][2].ToString());
            wins.Add(dt.Rows[i][3].ToString());
            draws.Add(dt.Rows[i][4].ToString());
            loses.Add(dt.Rows[i][5].ToString());
        }

        int min_count = 999999;
        if (times.Count < min_count) min_count = times.Count;
        if (teams.Count < min_count) min_count = teams.Count;
        if (wins.Count < min_count) min_count = wins.Count;
        if (draws.Count < min_count) min_count = draws.Count;
        if (loses.Count < min_count) min_count = loses.Count;

        for (int i = 0; i < min_count; i++)
        {
            //string[] single_times = times[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
            string str_time = times[i].ToString().Replace("滚球", "").Replace("●", "").Trim();
            string[] single_teams = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
            string f_time = str_time;
            string f_host = single_teams[0].ToString();
            string f_client = single_teams[1].ToString();
            string f_win = wins[i].ToString();
            string f_draw = draws[i].ToString();
            string f_lose = loses[i].ToString();
            Match100Helper.insert_data("fubo", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
            result = result + str_time.PR(20) + single_teams[0].PR(50) + single_teams[1].PR(50) + wins[i].PR(20) + draws[i].PR(20) + loses[i].PR(20) + Environment.NewLine;
        }
        //}
        //catch (Exception error)
        //{
        //    result = error.Message + Environment.NewLine + error.StackTrace;
        //}
        doc_result["data"] = result;
        return doc_result;
    }
    public BsonDocument from_pinnaclesports_1_back(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        string result = "";
        //try
        //{
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();
        DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
        DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
        ArrayList times = new ArrayList();
        ArrayList teams = new ArrayList();
        ArrayList odds = new ArrayList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(dt.Rows[i][4].ToString()) && dt.Rows[i][4].ToString().Contains("1X2") == false)
            {
                times.Add(dt.Rows[i][0].ToString());
                teams.Add(dt.Rows[i][2].ToString());
                odds.Add(dt.Rows[i][4].ToString());
            }
        }


        for (int i = 0; i < times.Count; i++)
        {
            if ((i + 2) < times.Count)
            {
                string f_time = times[i].ToString() + " " + times[i + 1].ToString();
                string f_host = teams[i].ToString();
                string f_client = teams[i + 1].ToString();
                string f_win = odds[i].ToString();
                string f_draw = odds[i + 2].ToString();
                string f_lose = odds[i + 1].ToString();
                Match100Helper.insert_data("pinnaclesports", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "-7", "0");
                result = result + f_time.PR(20) + f_host.PR(50) + f_client.PR(50) + f_win.PR(20) + f_draw.PR(20) + f_lose.PR(20) + Environment.NewLine;
            }
            i = i + 2;
        }
        //}
        //catch (Exception error)
        //{
        //    result = error.Message + Environment.NewLine + error.StackTrace;
        //}
        doc_result["data"] = result;
        return doc_result;
    }
    public BsonDocument from_188bet_1_back(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();

        string result = "";
        try
        {
            DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
            DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
            ArrayList times = new ArrayList();
            ArrayList teams = new ArrayList();
            ArrayList wins = new ArrayList();
            ArrayList draws = new ArrayList();
            ArrayList loses = new ArrayList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (string.IsNullOrEmpty(dt.Rows[i][4].ToString())) continue;
                times.Add(dt.Rows[i][0].ToString());
                teams.Add(dt.Rows[i][3].ToString());
                wins.Add(dt.Rows[i][4].ToString());
                draws.Add(dt.Rows[i][6].ToString());
                loses.Add(dt.Rows[i][8].ToString());
            }

            int min_count = 999999;
            if (times.Count < min_count) min_count = times.Count;
            if (teams.Count < min_count) min_count = teams.Count;
            if (wins.Count < min_count) min_count = wins.Count;
            if (draws.Count < min_count) min_count = draws.Count;
            if (loses.Count < min_count) min_count = loses.Count;

            for (int i = 0; i < min_count; i++)
            {
                string[] single_times = times[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                string[] single_teams = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);

                string f_time = times[i].ToString();
                string f_host = single_teams[0].ToString();
                string f_client = single_teams[1].ToString();
                string f_win = wins[i].ToString();
                string f_draw = draws[i].ToString();
                string f_lose = loses[i].ToString();
                //Match100Helper.insert_data("188be", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
                result = result + (single_times[0].ToString().Trim() + " " + single_times[1].ToString()).PR(20) + single_teams[0].PR(50) + single_teams[1].PR(50) + wins[i].PR(20) + draws[i].PR(20) + loses[i].PR(20) + Environment.NewLine;
            }
        }
        catch (Exception error)
        {
            result = error.Message + Environment.NewLine + error.StackTrace;
        }
        doc_result["data"] = result;
        return doc_result;
    }
    public BsonDocument from_macauslot_1_back(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        string result = "";
        //try
        //{
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();
        DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
        DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
        ArrayList times = new ArrayList();
        ArrayList teams = new ArrayList();
        ArrayList odds = new ArrayList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(dt.Rows[i][7].ToString()) && dt.Rows[i][7].ToString().ToLower().Contains("win") == false)
            {
                times.Add(dt.Rows[i][0].ToString());
                teams.Add(dt.Rows[i][2].ToString());
                odds.Add(dt.Rows[i][7].ToString());
            }
        }


        for (int i = 0; i < times.Count; i++)
        {

            if ((i + 2) < times.Count)
            {
                string[] single_times = times[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                string[] single_host = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                string[] single_client = teams[i + 1].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                string f_time = single_times[0].ToString() + "●" + single_times[1].ToString();
                string f_host = single_host[0].ToString();
                string f_client = single_client[0].ToString();
                string f_win = odds[i].ToString();
                string f_draw = odds[i + 2].ToString();
                string f_lose = odds[i].ToString();
                Match100Helper.insert_data("macauslot", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
                result = result + (single_times[0].ToString() + " " + single_times[1].ToString()).PR(20) + single_host[0].PR(50) + single_client[0].PR(50) + odds[i].PR(20) + odds[i + 2].PR(20) + odds[i + 1].PR(20) + Environment.NewLine;
            }
            i = i + 2;
        }
        //}
        //catch (Exception error)
        //{
        //    result = error.Message + Environment.NewLine + error.StackTrace;
        //}
        doc_result["data"] = result;
        return doc_result;
    }
    public BsonDocument from_10bet_2_back(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();
        doc_condition["element_type"].AsBsonArray.Add("div");
        string result = "";
        //try
        //{
        DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
        DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
        ArrayList times = new ArrayList();
        ArrayList teams = new ArrayList();
        ArrayList wins = new ArrayList();
        ArrayList draws = new ArrayList();
        ArrayList loses = new ArrayList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (string.IsNullOrEmpty(dt.Rows[i][3].ToString())) continue;
            times.Add(dt.Rows[i][1].ToString());
            teams.Add(dt.Rows[i][2].ToString());
            wins.Add(dt.Rows[i][3].ToString());
            draws.Add(dt.Rows[i][4].ToString());
            loses.Add(dt.Rows[i][6].ToString());
        }

        int min_count = 999999;
        if (times.Count < min_count) min_count = times.Count;
        if (teams.Count < min_count) min_count = teams.Count;
        if (wins.Count < min_count) min_count = wins.Count;
        if (draws.Count < min_count) min_count = draws.Count;
        if (loses.Count < min_count) min_count = loses.Count;

        for (int i = 0; i < min_count; i++)
        {
            string[] single_teams = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
            string f_time = times[i].ToString();
            string f_host = single_teams[0].ToString();
            string f_client = single_teams[2].ToString();
            string f_win = wins[i].ToString();
            string f_draw = draws[i].ToString();
            string f_lose = loses[i].ToString();
            Match100Helper.insert_data("10bet", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
            result = result + times[i].PR(20) + single_teams[0].PR(50) + single_teams[2].PR(50) + wins[i].PR(20) + draws[i].PR(20) + loses[i].PR(20) + Environment.NewLine;
        }
        //}
        //catch (Exception error)
        //{
        //    result = error.Message + Environment.NewLine + error.StackTrace;
        //}
        doc_result["data"] = result;
        return doc_result;
    }
    public BsonDocument from_fun88_1_back(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        string result = "";
        //try
        //{
        BsonDocument doc_condition = BrowserHelper.get_doc_condition();
        DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser, ref doc_condition);
        DataTable dt = BrowserHelper.get_filter_table(ref doc_condition, dt_analyse);
        ArrayList times = new ArrayList();
        ArrayList teams = new ArrayList();
        ArrayList odds = new ArrayList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(dt.Rows[i][5].ToString()) && dt.Rows[i][5].ToString().Contains("1X2") == false)
            {
                times.Add(dt.Rows[i][1].ToString());
                teams.Add(dt.Rows[i][2].ToString());
                odds.Add(dt.Rows[i][5].ToString());
            }
        }


        for (int i = 0; i < times.Count; i++)
        {

            string[] single_times = times[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
            string[] single_teams = teams[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
            string[] single_odds = odds[i].ToString().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);

            string f_time = times[i].ToString();
            string f_host = single_teams[0].ToString();
            string f_client = single_teams[1].ToString();
            string f_win = single_odds[0].ToString();
            string f_draw = single_odds[2].ToString();
            string f_lose = single_odds[1].ToString();
            Match100Helper.insert_data("fun88", "", f_time, f_host, f_client, f_win, f_draw, f_lose, "8", "0");
            result = result + single_times[1].PR(20) + single_teams[0].PR(50) + single_teams[1].PR(50) + single_odds[0].PR(20) + single_odds[2].PR(20) + single_odds[1].PR(20) + Environment.NewLine;
        }
        //}
        //catch (Exception error)
        //{
        //    result = error.Message + Environment.NewLine + error.StackTrace;
        //}
        doc_result["data"] = result;
        return doc_result;
    }
}
