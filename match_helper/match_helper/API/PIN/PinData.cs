
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MongoDB.Bson;
using System.Xml;

public class PinData
{ 
    public static string  show_sports(string result)
    { 
        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        XmlNodeList list = doc.SelectNodes("rsp/sports");
        foreach (XmlNode node in list)
        {
            string id = node.Attributes["id"].ToString();
            string feed_contents = node.Attributes["feedContents"].ToString();
            string name = node.InnerText;
            sb.Append(id.PR(10) + name.PR(50) + M.N);
        } 
        return sb.ToString();
    }
    public static string show_leagues(string result)
    {
        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        XmlNodeList list = doc.SelectNodes("rsp/leagues");
        foreach (XmlNode node in list)
        {
            string id = node.Attributes["id"].ToString();
            string feed_contents = node.Attributes["feedContents"].ToString();
            string name = node.InnerText;
            sb.Append(id.PR(10) + name.PR(50) + M.N);
        }
        return sb.ToString();
    }
    public static string show_currencies(string result)
    {
        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        XmlNodeList list = doc.SelectNodes("rsp/leagues");
        foreach (XmlNode node in list)
        {
            string id = node.Attributes["id"].ToString();
            string feed_contents = node.Attributes["feedContents"].ToString();
            string name = node.InnerText;
            sb.Append(id.PR(10) + name.PR(50) + M.N);
        }
        return sb.ToString();
    }
    public static string show_feeds(string result)
    {
        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        string time = doc.SelectSingleNode("rsp/fdTime").InnerText;
        XmlNodeList sports = doc.SelectNodes("rsp/sprots");
        foreach (XmlNode sport in sports )
        {
            XmlNodeList leagues = sport.SelectNodes("/leagues");
            foreach (XmlNode league in leagues)
            {
                XmlNodeList events = league.SelectNodes("/events");
                foreach (XmlNode e in events)
                {
                    string pin_id = e.SelectSingleNode("/id").InnerText;
                    string start_time = e.SelectSingleNode("/startDateTime").InnerText;
                    string home_name = e.SelectSingleNode("/homeTeam/name").InnerText;
                    string away_name = e.SelectSingleNode("/awayTeam /name").InnerText;

                    string limit_line = e.SelectSingleNode("/maxBetAmount/moneyLine").InnerText;
                    string limit_spread = e.SelectSingleNode("/maxBetAmount/spread").InnerText;
                    string limit_total = e.SelectSingleNode("/maxBetAmount/totalPoints").InnerText;
                    string limit_team_total = e.SelectSingleNode("/maxBetAmount/teamTotals").InnerText;

                    string home_line = e.SelectSingleNode("/moneyLine/homePrice").InnerText;
                    string draw_line = e.SelectSingleNode("/moneyLine/drawPrice").InnerText;
                    string away_line = e.SelectSingleNode("/moneyLine/awayPrice").InnerText;

                    XmlNodeList spreads = e.SelectNodes("/spreads");
                    foreach (XmlNode spread in spreads)
                    {
                        string home_spread = spread.SelectSingleNode("homeSpread").InnerText;
                        string home_spread_odd = spread.SelectSingleNode("homePrice").InnerText;
                        string away_spread = spread.SelectSingleNode("awaySpread").InnerText;
                        string away_spread_odd = spread.SelectSingleNode("awayPrice").InnerText; 
                    }
                    XmlNodeList totals = e.SelectNodes("/totals");
                    foreach (XmlNode total in totals)
                    {
                        string total_point2 = total.SelectSingleNode("points").InnerText;
                        string total_over = total.SelectSingleNode("overPrice").InnerText;
                        string total_under = total.SelectSingleNode("underPrice").InnerText; 
                    }  
                }
            }
        }
        return sb.ToString();
    }
}