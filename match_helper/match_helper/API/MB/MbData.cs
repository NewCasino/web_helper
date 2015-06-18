using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using MongoDB.Bson;
using HtmlAgilityPack;

class MbData
{
    public static string get_detail(string html)
    {
        StringBuilder sb = new StringBuilder();
        //-------------------------------------------
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
        string zone = "";
        string event_id = "";
        DateTime dt_time = DateTime.Now;
        foreach (HtmlNode node in nodes_all)
        {
            zone = "8";
            if (node.Id == "timer")
            {
                //get time zone
                //02.09.14, 14:47 (GMT+1)
                string timer = node.InnerText.E_TRIM();
                DateTime dt_timer = new DateTime(Convert.ToInt16("20" + timer.Substring(6, 2)), Convert.ToInt16(timer.Substring(3, 2)), Convert.ToInt16(timer.Substring(0, 2)),
                                                 Convert.ToInt16(timer.Substring(9, 2)), Convert.ToInt16(timer.Substring(12, 2)), 0);
                TimeSpan span = DateTime.Now - dt_timer;
                zone = (8 - Math.Round(span.TotalHours)).ToString();
            }

            if (node.Id == "container_EVENTS")
            {
                HtmlNodeCollection nodes_div = node.SELECT_NODES("/div");
                foreach (HtmlNode node_div in nodes_div)
                {
                    if (node_div.Id.Contains("container"))
                    {

                        league = node_div.SELECT_NODE("div[1]/h2[1]").InnerText.E_REMOVE();

                        HtmlNode test = node_div.SELECT_NODE("div[2]/div[1]/table[1]");
                        HtmlNodeCollection nodes_table = node_div.SELECT_NODES("div[2]/div[1]/table[1]/tbody");
                        if (nodes_table == null) continue;
                        foreach (HtmlNode node_table in nodes_table)
                        {
                            if (node_table.Id.Contains("event"))
                            {
                                event_id = node_table.Id.Replace("event_", "");


                                //get start_time
                                DateTime dt_start_time = new DateTime();
                                date = node_table.SELECT_NODE("/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[2]").InnerText.E_TRIM();
                                date = date.Replace("2015", "");
                                if (date.Length == 10)
                                { 
                                    dt_start_time = Tool.get_time(DateTime.Now.Year.ToString() + "-" + date.Substring(2, 3) + "-" + date.Substring(0, 2) + " " + date.Substring(5, 5) + ":00");
                                }
                                if (date.Length == 5)
                                { 
                                    dt_start_time = Tool.get_time(dt_time.ToString("yyyy-MM-dd" + " " + date + ":00"));
                                }



                                host = node_table.SELECT_NODE("/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[1]/span[1]/div[1]").InnerText.E_REMOVE().Replace("'", "");
                                client = node_table.SELECT_NODE("/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[1]/span[1]/div[2]").InnerText.E_REMOVE().Replace("'", "");
                                win = node_table.SELECT_NODE("/tr[1]/td[2]").InnerText.E_REMOVE();
                                draw = node_table.SELECT_NODE("/tr[1]/td[3]").InnerText.E_REMOVE();
                                lose = node_table.SELECT_NODE("/tr[1]/td[4]").InnerText.E_REMOVE();


                                if (win.Contains("/"))
                                {
                                    win = Match100Helper.convert_english_odd(win);
                                    draw = Match100Helper.convert_english_odd(draw);
                                    lose = Match100Helper.convert_english_odd(lose);
                                }

                                if (!string.IsNullOrEmpty(win.E_TRIM()) && !string.IsNullOrEmpty(draw.E_TRIM()) && !string.IsNullOrEmpty(lose.E_TRIM()))
                                {
                                    sb.AppendLine(league.PR(50) + dt_start_time.ToString("yyyy-MM-dd HH:mm:ss").PR(30) + host.PR(40) + client.PR(30) + win.PR(10) + draw.PR(10) + lose.PR(10));
                                    MbSQL.insert_odds(event_id, "1", "Three Results",
                                                                              "FULL", "", "", "", "", "",
                                                                              "HOME", "DRAW", "AWAY", "", "", "",
                                                                              win, draw, lose, "", "", "");
                                }

                                MbSQL.insert_events(event_id, league, dt_start_time.ToString("yyyy-MM-dd HH:mm:ss"), host, client);

                                //get the detail information
                                HtmlNodeCollection nodes_tr = node_table.SELECT_NODES("tr");
                                foreach (HtmlNode node_tr in nodes_tr)
                                {
                                    if (node_tr.CLASS().Contains("market-details"))
                                    {
                                        sb.AppendLine("------------------------------------------------------------------------------------");
                                        HtmlNodeCollection nodes_block = node_tr.SELECT_NODES("/td[1]/div[2]/div[1]/div"); //block-market-wrapper
                                        foreach (HtmlNode node_block in nodes_block)
                                        {
                                            HtmlNodeCollection nodes_div1 = node_block.SELECT_NODES("/div[4]/div");
                                            if (nodes_div1 != null)
                                            {
                                                foreach (HtmlNode node_div1 in nodes_div1)
                                                {
                                                    string odd_type = node_div1.SELECT_NODE("/div[1]").InnerText.E_REMOVE();
                                                    sb.AppendLine(odd_type);
                                                    sb.AppendLine("--------------------");
                                                    HtmlNodeCollection nodes_tr1 = node_div1.SELECT_NODES("/table[1]/tbody[1]/tr");

                                                    foreach (HtmlNode node_tr1 in nodes_tr1)
                                                    { 
                                                        HtmlNodeCollection nodes_td1 = node_tr1.SELECT_NODES("/td");
                                                        ArrayList list = new ArrayList();
                                                        if (nodes_td1 != null)
                                                        {
                                                            for (int i = 0; i < nodes_td1.Count; i++)
                                                            {

                                                                HtmlNodeCollection nodes_div2 = nodes_td1[i].SELECT_NODES("/div[1]/div");
                                                                if (nodes_div2 != null)
                                                                {
                                                                    for (int j = 0; j < nodes_div2.Count; j++)
                                                                    {
                                                                        sb.Append(M.D + nodes_div2[j].InnerText.E_REMOVE().PR(10));
                                                                        list.Add(nodes_div2[j].InnerText.E_REMOVE().Replace("(", "").Replace(")", "").Replace("+", ""));
                                                                    }
                                                                }
                                                            }
                                                            if (list.Count == 0)
                                                            {
                                                                for (int i = 0; i < nodes_td1.Count; i++)
                                                                {
                                                                    sb.Append(nodes_td1[i].InnerText.E_REMOVE().PR(30));
                                                                } 
                                                            }
                                                            sb.Append(M.N);
                                                        }
                                                      

                                                        if (odd_type == "To Win Match With Handicap" && list.Count >= 4)
                                                        {

                                                            MbSQL.insert_odds(event_id, "2", "To Win Match With Handicap",
                                                                              "FULL", "", "", "", "", "",
                                                                              list[0].ToString(), list[2].ToString(), "", "", "", "",
                                                                              list[1].ToString(), list[3].ToString(), "", "", "", "");

                                                        }
                                                        if (odd_type == "To Win 1st Half With Handicap" && list.Count >= 4)
                                                        {
                                                            MbSQL.insert_odds(event_id, "2", "To Win 1st Half With Handicap",
                                                                               "1-HALF", "", "", "", "", "",
                                                                               list[0].ToString(), list[2].ToString(), "", "", "", "",
                                                                               list[1].ToString(), list[3].ToString(), "", "", "", "");
                                                        }
                                                        if (odd_type == "To Win 2nd Half With Handicap" && list.Count >= 4)
                                                        {
                                                            MbSQL.insert_odds(event_id, "2", "To Win 2nd Half With Handicap",
                                                                               "2-HALF", "", "", "", "", "",
                                                                               list[0].ToString(), list[2].ToString(), "", "", "", "",
                                                                               list[1].ToString(), list[3].ToString(), "", "", "", "");
                                                        }
                                                        if (odd_type == "Total Goals" && list.Count >= 4)
                                                        {
                                                            MbSQL.insert_odds(event_id, "3", "Total Goals",
                                                                               "FULL", "", "", "", "", "",
                                                                               list[0].ToString(), list[2].ToString(), "", "", "", "",
                                                                               list[1].ToString(), list[3].ToString(), "", "", "", "");
                                                        }
                                                        if (odd_type == "Total Goals - 1st Half" && list.Count >= 4)
                                                        {
                                                            MbSQL.insert_odds(event_id, "3", "Total Goals - 1st Half",
                                                                               "1-HALF", "", "", "", "", "",
                                                                               list[0].ToString(), list[2].ToString(), "", "", "", "",
                                                                               list[1].ToString(), list[3].ToString(), "", "", "", "");
                                                        }
                                                        if (odd_type == "Total Goals - 2nd Half" && list.Count >= 4)
                                                        {
                                                            MbSQL.insert_odds(event_id, "3", "Total Goals - 2nd Half",
                                                                               "2-HALF", "", "", "", "", "",
                                                                               list[0].ToString(), list[2].ToString(), "", "", "", "",
                                                                               list[1].ToString(), list[3].ToString(), "", "", "", "");
                                                        }
                                                        if (odd_type.Contains("Total Goals") && odd_type.Contains(host) && !odd_type.Contains("+") && list.Count >= 4)
                                                        {
                                                            MbSQL.insert_odds(event_id, "3", "Total Goals",
                                                                               "FULL", "HOME", "", "", "", "",
                                                                               list[0].ToString(), list[2].ToString(), "", "", "", "",
                                                                               list[1].ToString(), list[3].ToString(), "", "", "", "");
                                                        }
                                                        if (odd_type.Contains("Total Goals") && odd_type.Contains(client) && !odd_type.Contains("+") && list.Count >= 4)
                                                        {
                                                            MbSQL.insert_odds(event_id, "3", "Total Goals",
                                                                               "FULL", "AWAY", "", "", "", "",
                                                                               list[0].ToString(), list[2].ToString(), "", "", "", "",
                                                                               list[1].ToString(), list[3].ToString(), "", "", "", "");
                                                        }
                                                    }
                                                    sb.AppendLine("--------------------");

                                                }
                                            }
                                        }
                                        sb.AppendLine("------------------------------------------------------------------------------------");

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //------------------------------------------------------
        return sb.ToString();
    }
}

