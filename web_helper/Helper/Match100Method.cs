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
                    string start_time = node.Attributes["starttime"].Value.ToString();
                    string host = node.Attributes["hostname"].Value.ToString();
                    string client = node.Attributes["guestname"].Value.ToString();
                    string win = node.SelectSingleNode(root + "/span[6]/div[1]/em[1]").InnerText;
                    string draw = node.SelectSingleNode(root + "/span[6]/div[1]/em[2]").InnerText;
                    string lose = node.SelectSingleNode(root + "/span[6]/div[1]/em[3]").InnerText;
                    sb.Append(league.PR(20) + start_time.PR(20) + host.PR(20) + client.PR(20) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);
                }
                catch (Exception error) { Log.error("from 163", error); }
            }
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
                    //sb.Append(root + Environment.NewLine);
                    //sb.Append(node.InnerHtml + Environment.NewLine);
                    sb.Append(league.PR(20) + start_time.PR(20) + host.PR(20) + client.PR(20) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);


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
                    //sb.Append(root + Environment.NewLine);
                    //sb.Append(node.InnerHtml + Environment.NewLine);
                    sb.Append(league.PR(20) + start_time.PR(20) + host.PR(20) + client.PR(20) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);


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
        string result = "";
        //try
        //{
        DataTable dt_analyse = BrowserHelper.get_analyse_table4(ref browser);
        DataTable dt = Match100Helper.get_match_table(dt_analyse);
        ArrayList times = new ArrayList();
        ArrayList teams = new ArrayList();
        ArrayList wins = new ArrayList();
        ArrayList draws = new ArrayList();
        ArrayList loses = new ArrayList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string text = dt.Rows[i][j].ToString().Trim();
                if (string.IsNullOrEmpty(text)) continue;
                if (j == 1) times.Add(text);
                if (j == 2) teams.Add(text);
                if (j == 3) wins.Add(text);
                if (j == 4) draws.Add(text);
                if (j == 6) loses.Add(text);
            }
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

    public BsonDocument from_macauslot_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();

        string result = "";

        DataTable dt = BrowserHelper.get_analyse_table(ref browser);
        ArrayList teams = new ArrayList();
        ArrayList odds = new ArrayList();
        for (int i = 0; i < dt.Rows.Count - 2; i++)
        {
            string txt = "";

            txt = dt.Rows[i]["293"].ToString().Replace("●", "").Replace("(中)", "");
            if (!string.IsNullOrEmpty(txt)) teams.Add(txt);

            for (int j = 3; j < dt.Columns.Count; j++)
            {
                int num = Convert.ToInt32(dt.Columns[j].ToString());
                if (num >= 1060 && num < 1080)
                {
                    txt = dt.Rows[i][j].ToString().Replace("●", "").Trim();
                    if (Match100Helper.is_double_str(txt) && !string.IsNullOrEmpty(txt)) odds.Add(txt);
                }
            }

        }
        int count = teams.Count / 2;
        for (int i = 0; i < count; i++)
        {
            if (i * 2 + 1 < teams.Count)
            {
                result = result + teams[i * 2].PR(20) + teams[i * 2 + 1].PR(20);
            }
            if (i * 3 + 2 < odds.Count)
            {
                result = result + odds[i * 3].PR(20) + odds[i * 3 + 1].PR(20) + odds[i * 3 + 2].PR(20);
            }
            result = result + Environment.NewLine;
        }

        doc_result["data"] = result;
        return doc_result;
    }

    public BsonDocument from_pinnaclesports_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        string result = "";

        DataTable dt = BrowserHelper.get_analyse_table(ref browser);
        ArrayList teams = new ArrayList();
        ArrayList odds = new ArrayList();
        for (int i = 0; i < dt.Rows.Count - 2; i++)
        {
            string txt1 = dt.Rows[i]["557"].ToString().Replace("●", "");
            string txt2 = dt.Rows[i]["984"].ToString().Replace("●", "").Trim();
            if (!string.IsNullOrEmpty(txt2) && Match100Helper.is_odd_str(txt2))
            {
                teams.Add(txt1);
                odds.Add(txt2);
            }
        }
        for (int i = 0; i < teams.Count; i++)
        {
            if (i + 2 < teams.Count)
            {
                result = result + teams[i].PR(20) + teams[i + 1].PR(20) + odds[i].PR(20) + odds[i + 2].PR(20) + odds[i + 1].PR(20) + Environment.NewLine;
                i = i + 2;
            }
        }


        doc_result["data"] = result;
        return doc_result;
    }

    public BsonDocument from_188bet_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();

        string result = "";

        DataTable dt = BrowserHelper.get_analyse_table(ref browser);
        ArrayList teams = new ArrayList();
        ArrayList odds = new ArrayList();
        for (int i = 0; i < dt.Rows.Count - 2; i++)
        {
            string txt = "";

            txt = dt.Rows[i]["359"].ToString().Replace("●", "");
            if (!string.IsNullOrEmpty(txt)) teams.Add(txt);
            txt = dt.Rows[i]["379"].ToString().Replace("●", "");
            if (!string.IsNullOrEmpty(txt)) teams.Add(txt);

            txt = dt.Rows[i]["600"].ToString().Replace("●", "");
            if (!string.IsNullOrEmpty(txt)) odds.Add(txt);
            txt = dt.Rows[i]["678"].ToString().Replace("●", "");
            if (!string.IsNullOrEmpty(txt)) odds.Add(txt);
            txt = dt.Rows[i]["756"].ToString().Replace("●", "");
            if (!string.IsNullOrEmpty(txt)) odds.Add(txt);
        }
        int count = teams.Count / 2;
        for (int i = 0; i < count; i++)
        {
            if (i * 2 + 1 < teams.Count)
            {
                result = result + teams[i * 2].PR(20) + teams[i * 2 + 1].PR(20);
            }
            if (i * 3 + 2 < odds.Count)
            {
                result = result + odds[i * 3].PR(20) + odds[i * 3 + 1].PR(20) + odds[i * 3 + 2].PR(20);
            }
            result = result + Environment.NewLine;
        }


        doc_result["data"] = result;
        return doc_result;
    }

    public BsonDocument from_fun88_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();

        string result = "";

        DataTable dt = BrowserHelper.get_analyse_table(ref browser);
        ArrayList teams = new ArrayList();
        ArrayList odds = new ArrayList();

        for (int i = 0; i < dt.Rows.Count - 2; i++)
        {
            string txt = "";

            txt = dt.Rows[i]["789"].ToString().Replace("●", "");//7.82 
            if (!string.IsNullOrEmpty(txt))
            {
                odds.Add(txt);
                txt = dt.Rows[i - 1]["361"].ToString().Replace("●", "");
                teams.Add(txt);
            }

            txt = dt.Rows[i]["782"].ToString().Replace("●", "");//17.22 
            if (!string.IsNullOrEmpty(txt))
            {
                odds.Add(txt);
                txt = dt.Rows[i - 1]["361"].ToString().Replace("●", "");
                teams.Add(txt);
            }
        }
        int count = teams.Count / 3;
        for (int i = 0; i < count; i++)
        {
            result = result + teams[i * 3].PR(20) + teams[i * 3 + 1].PR(20) + odds[i * 3].PR(20) + odds[i * 3 + 2].PR(20) + odds[i * 3 + 1].PR(20) + Environment.NewLine;
        }


        doc_result["data"] = result;
        return doc_result;
    }

    public BsonDocument from_fubo_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();

        string result = "";

        DataTable dt = BrowserHelper.get_analyse_table(ref browser);
        ArrayList teams = new ArrayList();
        ArrayList odds = new ArrayList();
        for (int i = 0; i < dt.Rows.Count - 2; i++)
        {
            string txt = "";

            txt = dt.Rows[i]["516"].ToString().Replace("●", "");
            if (!string.IsNullOrEmpty(txt)) teams.Add(txt);
            txt = dt.Rows[i]["692"].ToString().Replace("●", "");
            if (!string.IsNullOrEmpty(txt)) odds.Add(txt);
            txt = dt.Rows[i]["776"].ToString().Replace("●", "");
            if (!string.IsNullOrEmpty(txt)) odds.Add(txt);
            txt = dt.Rows[i]["860"].ToString().Replace("●", "");
            if (!string.IsNullOrEmpty(txt)) odds.Add(txt);
        }
        int count = teams.Count / 2;
        for (int i = 0; i < count; i++)
        {
            if (i * 2 + 1 < teams.Count)
            {
                result = result + teams[i * 2].PR(20) + teams[i * 2 + 1].PR(20);
            }
            if (i * 3 + 2 < odds.Count)
            {
                result = result + odds[i * 3].PR(20) + odds[i * 3 + 1].PR(20) + odds[i * 3 + 2].PR(20);
            }
            result = result + Environment.NewLine;
        }


        doc_result["data"] = result;
        return doc_result;
    }
}
