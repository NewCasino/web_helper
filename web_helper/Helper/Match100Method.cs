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
        public string from_163(ref WebBrowser browser)
        {
            StringBuilder sb = new StringBuilder();
            if (browser.Document == null) return "";
            string html = "<body>" + Environment.NewLine + browser.Document.Body.InnerHtml + Environment.NewLine + "</body>";

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
            string html = "<body>" + Environment.NewLine + browser.Document.Body.InnerHtml + Environment.NewLine + "</body>";

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
            HtmlElementCollection elements = browser.Document.All;
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

        public string from_local1(WebBrowser browser) 
        {
            return "locak html has load!";
        }
        public string from_local2(WebBrowser browser)
        {
            HtmlElement element = browser.Document.GetElementById("btn_ok");
            element.InvokeMember("click");

            HtmlElement element_txt = browser.Document.GetElementById("txt");
            return element_txt.GetAttribute("value").ToString(); 
        } 
    }
