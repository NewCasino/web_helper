using System;
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
    public void get_absolute(ref IHTMLElement element, ref int left, ref int top)
    {
        if (element.parentElement != null)
        {
            left = left + element.parentElement.offsetLeft;
            top = top + element.parentElement.offsetTop;
            IHTMLElement father_element = element.parentElement;
            get_absolute(ref father_element, ref left, ref top);
        }
    }
    public DataTable get_analyse_table(ref WebBrowser browser)
    {
        DataTable dt = new DataTable();
        if (browser.Document == null) return dt;

        //get dt_position
        DataTable dt_position = new DataTable();
        DataColumn col1 = new DataColumn();
        col1.DataType = Type.GetType("System.Int32");
        col1.ColumnName = "left";
        DataColumn col2 = new DataColumn();
        col2.DataType = Type.GetType("System.Int32");
        col2.ColumnName = "top";
        DataColumn col3 = new DataColumn();
        col3.DataType = Type.GetType("System.Int32");
        col3.ColumnName = "width";
        DataColumn col4 = new DataColumn();
        col4.DataType = Type.GetType("System.Int32");
        col4.ColumnName = "height";

        dt_position.Columns.Add(col1);
        dt_position.Columns.Add(col2);
        dt_position.Columns.Add(col3);
        dt_position.Columns.Add(col4);
        dt_position.Columns.Add("text");

        HtmlElementCollection elements = browser.Document.Body.All;

        foreach (HtmlElement element in elements)
        {

            IHTMLElement ielement = (IHTMLElement)element.DomElement;
            IHTMLDOMNode node = (IHTMLDOMNode)ielement;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)node.attributes;



            string row = "";
            row += ielement.tagName.PR(10);
            row += ielement.className.PR(10);
            row += ielement.id.PR(10);
            row += ((IHTMLElementCollection)ielement.children).length.PR(10);

            int left = ielement.offsetLeft;
            int top = ielement.offsetTop;
            get_absolute(ref ielement, ref left, ref top);


            row += left.PR(10);
            row += top.PR(10);
            row += ielement.offsetLeft.PR(10);
            row += ielement.offsetTop.PR(10);
            row += ielement.offsetWidth.PR(10);
            row += ielement.offsetHeight.PR(10);
            row += ielement.innerText.PR(100);

            DataRow row_new = dt_position.NewRow();
            row_new["left"] = left;
            row_new["top"] = top.ToString();
            row_new["width"] = ielement.offsetWidth.ToString();
            row_new["height"] = ielement.offsetHeight.ToString();
            row_new["text"] = ielement.innerText;



            if (((IHTMLElementCollection)ielement.children).length != 0) continue;
            if (string.IsNullOrEmpty(ielement.innerText)) continue;
            if (string.IsNullOrEmpty(row)) continue;
            if (left == 0 && top == 0 && ielement.offsetWidth == 0 && ielement.offsetHeight == 0) continue;

            dt_position.Rows.Add(row_new);
        }



        //get dt from dt_postion 

        dt.Columns.Add("NO");
        dt.Columns.Add("COUNT");
        dt.Columns.Add("TEXT");

        //add column to dt
        dt_position.DefaultView.Sort = "left asc";
        dt_position = dt_position.DefaultView.ToTable();
        foreach (DataRow row in dt_position.Rows)
        {
            bool is_has = false;
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.Trim() == row["left"].ToString().Trim())
                {
                    is_has = true;
                }
            }
            if (is_has == false)
            {
                DataColumn column = new DataColumn();
                column.ColumnName = row["left"].ToString();
                dt.Columns.Add(column);
            }
        }


        //add row to dt
        dt_position.DefaultView.Sort = "top asc";
        dt_position = dt_position.DefaultView.ToTable();
        for (int i = 0; i < dt_position.Rows.Count; i++)
        {
            string text = dt_position.Rows[i]["text"].ToString().TrimStart().TrimEnd();
            if (string.IsNullOrEmpty(text)) continue;


            bool is_has = false;
            int row_id = 0;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["NO"].ToString() == dt_position.Rows[i]["top"].ToString())
                {
                    is_has = true;
                    row_id = j;
                }
            }

            if (is_has == false)
            {
                DataRow row_new = dt.NewRow();
                row_new["NO"] = dt_position.Rows[i]["top"].ToString();
                row_new["COUNT"] = "1";
                row_new["TEXT"] = text;
                row_new[dt_position.Rows[i]["left"].ToString()] = text;
                dt.Rows.Add(row_new);
            }
            else
            {
                dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()] = dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()].ToString() + "●" + text;
                dt.Rows[row_id]["COUNT"] = (Convert.ToInt32(dt.Rows[row_id]["COUNT"].ToString()) + 1).ToString();
                dt.Rows[row_id]["TEXT"] = dt.Rows[row_id]["TEXT"].ToString() + "●" + dt_position.Rows[i]["text"].ToString().TrimStart().TrimEnd();
            }
        }

        DataRow row_count = dt.NewRow();
        DataRow row_text = dt.NewRow();
        row_count["NO"] = "COUNT";
        row_text["NO"] = "TEXT";
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (i == 1)
            {
                int total = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    total = total + Convert.ToInt32(dt.Rows[j][i].ToString());
                }
                row_count[1] = total.ToString();

            }
            if (i != 0 && i != 1 && i != 2)
            {
                int column_total = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j][i] != null && !string.IsNullOrEmpty(dt.Rows[j][i].ToString()))
                    {
                        column_total = column_total + 1;
                    }
                }
                row_count[i] = column_total.ToString();
            }
            if (i != 0 && i != 1 && i != 2)
            {

                string text = "";
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j][i] != null && !string.IsNullOrEmpty(dt.Rows[j][i].ToString()))
                    {
                        if (string.IsNullOrEmpty(text.Trim()))
                        {
                            text = dt.Rows[j][i].ToString();
                        }
                        else
                        {
                            text = text + "●" + dt.Rows[j][i].ToString();
                        }
                    }
                }
                row_text[i] = text.ToString();
            }
        }
        dt.Rows.Add(row_count);
        dt.Rows.Add(row_text);

        for (int i = 0; i < dt.Rows.Count - 2; i++)
        {
            if (dt.Rows[i]["COUNT"].ToString() == "1")
            {
                dt.Rows[i]["TEXT"] = "";
            }
        }

        for (int i = 3; i < dt.Columns.Count; i++)
        {
            int count = dt.Rows.Count;
            if (dt.Rows[count - 2][i].ToString() == "1")
            {
                dt.Rows[count - 1][i] = "";
            }
        }

        return dt;


    }



    public string from_163(ref WebBrowser browser)
    {
        StringBuilder sb = new StringBuilder();
        if (browser.Document == null) return "";
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
        return sb.ToString();
    }
    public string from_bwin(ref WebBrowser browser)
    {
        StringBuilder sb = new StringBuilder();

        if (browser.Document == null) return "";
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
                    sb.Append(league.PR(20) + start_time.PR(20) + host.PR(30) + client.PR(30) + win.PR(20) + draw.PR(20) + lose.PR(20) + Environment.NewLine);


                }
            }
            catch (Exception error) { Log.error("from bwin", error); }

        }
        return sb.ToString();
    }
    public string from_baidu(ref WebBrowser browser)
    {
        StringBuilder sb = new StringBuilder();

        if (browser.Document == null) return "";
        HtmlElementCollection elements = browser.Document.Body.All;
        foreach (HtmlElement element in elements)
        {
            string row = "";
            IHTMLElement el = (IHTMLElement)element.DomElement;
            IHTMLDOMNode nd = (IHTMLDOMNode)el;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)nd.attributes;

            //if (attrs != null)
            //{

            //    foreach (IHTMLDOMAttribute attr in attrs)
            //    {
            //        row += attr.nodeName + ":" + attr.nodeValue;
            //    } 
            //} 

            row += el.tagName.PR(10);
            row += el.className.PR(10);
            row += el.id.PR(10);
            row += ((IHTMLElementCollection)el.children).length.PR(10);

            int left = el.offsetLeft;
            int top = el.offsetTop;
            get_absolute(ref el, ref left, ref top);

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

        return sb.ToString();
    }

    public string from_local_0(WebBrowser browser)
    {
        return "local html has load!";
    }
    public string from_local_1(WebBrowser browser)
    {
        string result = "";
        HtmlElementCollection list = browser.Document.Body.All;
        foreach (HtmlElement element in list)
        {
            if (element.Id == "btn_ok") element.InvokeMember("click");
            if (element.Id == "txt") result = element.GetAttribute("value");


        }
        return result;
    }

    public string from_bet16(WebBrowser browser)
    {
        string result = "";
        result = browser.Document.All.Count.ToString();
        foreach (HtmlElement element in browser.Document.All)
        {
            result += element.OuterHtml + Environment.NewLine;
        }
        return result;


    }

    public string from_10bet_1(WebBrowser browser)
    {
        return "OK";
    }

    public string from_10bet_2(WebBrowser browser)
    {
        HtmlElement element = browser.Document.GetElementById("tp_chk_br_999_l_1_1");
        element.InvokeMember("click");
        return "OK";
    }
    public string from_10bet_3(WebBrowser browser)
    {
        string result = "";
        try
        {
            DataTable dt = get_analyse_table(ref browser);
            foreach (DataRow row in dt.Rows)
            {


                int count = 0;
                int.TryParse(row["COUNT"].ToString(), out count);
                if (count < 5 && count > 10) continue;

                int double_count = 0;
                double output = 0;
                string[] list = row["TEXT"].ToString().Split('●');
                foreach (string str in list)
                {
                    if (double.TryParse(str, out output) == true)
                    {
                        double_count = double_count + 1;
                    }
                }
                if (double_count < 3) continue;

                result = result + row["TEXT"].ToString() + Environment.NewLine;

            }
        }
        catch (Exception error)
        {
            result = error.Message + Environment.NewLine + error.StackTrace;
        }
        return result;
    }
}
