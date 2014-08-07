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

    public BsonDocument from_pinnaclesports_1(ref WebBrowser browser)
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
                result = result + (times[i].ToString() + " " + times[i + 1].ToString()).PR(20) + teams[i].PR(50) + teams[i + 1].PR(50) + odds[i].PR(20) + odds[i + 2].PR(20) + odds[i + 1].PR(20) + Environment.NewLine;
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

    public BsonDocument from_188bet_1(ref WebBrowser browser)
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
            result = result + (single_times[0].ToString().Trim()+ " " + single_times[1].ToString()).PR(20) + single_teams[0].PR(50) + single_teams[1].PR(50) + wins[i].PR(20) + draws[i].PR(20) + loses[i].PR(20) + Environment.NewLine;
        }
        //}
        //catch (Exception error)
        //{
        //    result = error.Message + Environment.NewLine + error.StackTrace;
        //}
        doc_result["data"] = result;
        return doc_result;
    }

    public BsonDocument from_fun88_1(ref WebBrowser browser)
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

    public BsonDocument from_fubo_1(ref WebBrowser browser)
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
            result = result +  str_time.PR(20)+ single_teams[0].PR(50) + single_teams[1].PR(50) + wins[i].PR(20) + draws[i].PR(20) + loses[i].PR(20) + Environment.NewLine;
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
