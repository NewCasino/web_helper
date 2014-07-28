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


    public BsonDocument from_baidu(ref WebBrowser browser)
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

    public BsonDocument from_local_0(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        doc_result["data"] = "ok";
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
    public BsonDocument from_163(ref WebBrowser browser)
    {
        BsonDocument  doc_result= Match100Helper.get_doc_result();

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
        doc_result["data"] = "ok";
        return doc_result;
    }
    public BsonDocument from_bwin_2(ref WebBrowser browser)
    {

        BsonDocument doc_result = Match100Helper.get_doc_result();

        StringBuilder sb = new StringBuilder();
        if (browser.Document == null) return doc_result ;
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

    public BsonDocument from_bet16(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        string result = "";
        result = browser.Document.All.Count.ToString();
        foreach (HtmlElement element in browser.Document.All)
        {
            result += element.OuterHtml + Environment.NewLine;
        }
  
        doc_result["data"] = result;
        return doc_result; 
    }

    public BsonDocument from_10bet_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        doc_result["data"] = "ok";
        return doc_result;
    }
    public BsonDocument from_10bet_2(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();


        BrowserHelper.invoke_click_by_id(ref browser, "tp_chk_br_999_l_1_1");

        doc_result["data"] = "ok";
        return doc_result; 
    }
    public BsonDocument from_10bet_3(ref WebBrowser browser)
    {

        BsonDocument doc_result = Match100Helper.get_doc_result();
        string result = "";
        //try
        //{
        DataTable dt = BrowserHelper.get_analyse_table(ref browser);
        foreach (DataRow row in dt.Rows)
        {
            int count = 0;
            int.TryParse(row["COUNT"].ToString(), out count);
            if (count < 5 && count > 10) continue;


            ArrayList odds = new ArrayList();
            ArrayList teams = new ArrayList();
            string[] list = row["TEXT"].ToString().Split('●');
            for (int i = list.Length - 1; i >= 0; i--)
            {
                string str = list[i];
                if (Match100Helper.is_odd_str(str)) odds.Add(str);
                if (str.Contains("+") == false && str.Length >= 3 && Match100Helper.is_odd_str(str) == false && str != "博彩责任") teams.Add(str);
            }
            if (odds.Count >= 3 && teams.Count >= 2)
            {
                result = result + teams[1].PR(20) + teams[0].PR(20) + odds[2].PR(20) + odds[1].PR(20) + odds[0].PR(20) + Environment.NewLine;
            }

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
        doc_result["data"] = "ok";
        return doc_result;
    }
    public BsonDocument from_macauslot_2(ref WebBrowser browser)
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
                    if (Match100Helper.is_double_str(txt) &&!string.IsNullOrEmpty(txt)) odds.Add(txt);
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
        doc_result["data"] = "ok";
        return doc_result;
    }
    public BsonDocument from_pinnaclesports_2(ref WebBrowser browser)
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
                result = result + teams[i].PR(20) + teams[i + 1].PR(20) + odds[i].PR(20) + odds[i + 1].PR(20) + odds[i + 2].PR(20) + Environment.NewLine;
                i = i + 2;
            }
        }


        doc_result["data"] = result;
        return doc_result;
    }

    public BsonDocument from_188bet_1(ref WebBrowser browser)
    {
        BsonDocument doc_result = Match100Helper.get_doc_result();
        doc_result["data"] = "ok";
        return doc_result;
    }
    public BsonDocument from_188bet_2(ref WebBrowser browser)
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
}
