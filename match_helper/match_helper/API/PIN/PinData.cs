
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
    public static string show_sports(string result)
    {
        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        XmlNodeList sports = doc.SelectNodes("rsp/sports/sport");
        foreach (XmlNode sport in sports)
        {
            string sport_id = sport.Attributes["id"].Value.ToString();
            string sport_name = sport.InnerText;
            sb.Append(sport_id.PR(10) + sport_name.PR(50) + M.N);
        }
        return sb.ToString();
    }
    public static string show_leagues(string result)
    {
        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);


        string sport_id = doc.SelectSingleNode("/sportId").InnerText;
        XmlNodeList leagues = doc.SelectNodes("/leagues/league");
        foreach (XmlNode league in leagues)
        {
            string league_id = league.Attributes["id"].Value.ToString();
            string league_name = league.InnerText;
            sb.Append(sport_id.PR(10) + league_id.PR(10) + league_name.PR(50) + M.N);
        }

        return sb.ToString();
    }
    public static string show_feeds(string result)
    {
        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        string time = doc.SelectSingleNode("rsp/fd/fdTime").InnerText;
        XmlNodeList sports = doc.SelectNodes("rsp/fd/sprots");
        foreach (XmlNode sport in sports)
        {
            string sport_id = sport.SelectSingleNode("/id").InnerText;
            XmlNodeList leagues = sport.SelectNodes("/leagues/league");
            foreach (XmlNode league in leagues)
            {
                string league_id = league.SelectSingleNode("/id").InnerText;
                XmlNodeList events = league.SelectNodes("/events/event");
                foreach (XmlNode e in events)
                {
                    string event_id = e.SelectSingleNode("/id").InnerText;
                    string start_time = e.SelectSingleNode("/startDateTime").InnerText;
                    string home_name = e.SelectSingleNode("/homeTeam/name").InnerText;
                    string away_name = e.SelectSingleNode("/awayTeam/name").InnerText;

                    XmlNodeList periods = e.SelectNodes("/periods/period");
                    foreach (XmlNode period in periods)
                    {
                        string period_line_id = period.Attributes["lineId"].Value.ToString();
                        string period_number = period.SelectSingleNode("/number").InnerText;

                        string limit_line = period.SelectSingleNode("/maxBetAmount/moneyLine").InnerText;
                        string limit_spread = period.SelectSingleNode("/maxBetAmount/spread").InnerText;
                        string limit_total = period.SelectSingleNode("/maxBetAmount/totalPoints").InnerText;
                        string limit_team_total = period.SelectSingleNode("/maxBetAmount/teamTotals").InnerText;

                        string home_line = period.SelectSingleNode("/moneyLine/homePrice").InnerText;
                        string draw_line = period.SelectSingleNode("/moneyLine/drawPrice").InnerText;
                        string away_line = period.SelectSingleNode("/moneyLine/awayPrice").InnerText;

                        XmlNodeList spreads = period.SelectNodes("/spreads");
                        foreach (XmlNode spread in spreads)
                        {
                            string home_spread = spread.SelectSingleNode("homeSpread").InnerText;
                            string home_spread_odd = spread.SelectSingleNode("homePrice").InnerText;
                            string away_spread = spread.SelectSingleNode("awaySpread").InnerText;
                            string away_spread_odd = spread.SelectSingleNode("awayPrice").InnerText;
                        }
                        XmlNodeList totals = period.SelectNodes("/totals");
                        foreach (XmlNode total in totals)
                        {
                            string total_point2 = total.SelectSingleNode("points").InnerText;
                            string total_over = total.SelectSingleNode("overPrice").InnerText;
                            string total_under = total.SelectSingleNode("underPrice").InnerText;
                        }

                        XmlNode team_total = period.SelectSingleNode("/teamTotals");
                        string team_total_home = team_total.SelectSingleNode("/homeTeamTotal/total").InnerText;
                        string team_total_home_under_price = team_total.SelectSingleNode("/homeTeamTotal/overPrice").InnerText;
                        string team_total_home_over_price = team_total.SelectSingleNode("/homeTeamTotal/underPrice").InnerText;
                        string team_total_away_home = team_total.SelectSingleNode("/awayTeamTotal/total").InnerText;
                        string team_total_away_under_price = team_total.SelectSingleNode("/awayTeamTotal/overPrice").InnerText;
                        string team_total_away_over_price = team_total.SelectSingleNode("/awayTeamTotal/underPrice").InnerText;
                    }
                }
            }
        }
        return sb.ToString();
    }

    public static string insert_sports(string result)
    {

        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        XmlNodeList sports = doc.SelectNodes("rsp/sports/sport");
        foreach (XmlNode sport in sports)
        {
            string sport_id = sport.Attributes["id"].Value.ToString();
            string sport_name = sport.InnerText;
            PinSQL.insert_sport(sport_id, sport_name);
        }
        return sb.ToString(); 
       
    }
    public static string insert_leagues(string result)
    { 
        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);


        string sport_id = doc.SelectSingleNode("rsp/sportId").InnerText;
        XmlNodeList leagues = doc.SelectNodes("rsp/leagues/league");
        foreach (XmlNode league in leagues)
        {
            string league_id = league.Attributes["id"].Value.ToString();
            string league_name = league.InnerText;
            PinSQL.insert_league(sport_id, league_id, league_name);
        }

        return sb.ToString();
    }
    public static string insert_odds(string result)
    { 
        StringBuilder sb = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        string time = doc.SelectSingleNode("rsp/fd/fdTime").InnerText;
        XmlNodeList sports = doc.SelectNodes("rsp/fd/sports/sport");
        foreach (XmlNode sport in sports)
        {
            string sport_id = sport.SelectSingleNode("id").InnerText;
            XmlNodeList leagues = sport.SelectNodes("leagues/league");
            foreach (XmlNode league in leagues)
            {
                string league_id = league.SelectSingleNode("id").InnerText;
                XmlNodeList events = league.SelectNodes("events/event");
                foreach (XmlNode e in events)
                {
                    string event_id = e.SelectSingleNode("id").InnerText;
                    string start_time = e.SelectSingleNode("startDateTime").InnerText;
                    string home_name = e.SelectSingleNode("homeTeam/name").InnerText;
                    string away_name = e.SelectSingleNode("awayTeam/name").InnerText;

                    XmlNodeList periods = e.SelectNodes("periods/period");
                    foreach (XmlNode period in periods)
                    {
                        string period_line_id = period.Attributes["lineId"].Value.ToString();
                        string period_number = period.SelectSingleNode("number").InnerText;

                        if (period.SelectSingleNode("maxBetAmount/moneyLine") != null &
                            period.SelectSingleNode("maxBetAmount/spread") != null &
                            period.SelectSingleNode("maxBetAmount/totalPoints") != null &
                            period.SelectSingleNode("maxBetAmount/teamTotals") != null
                            )
                        {
                            string limit_line = period.SelectSingleNode("maxBetAmount/moneyLine").InnerText;
                            string limit_spread = period.SelectSingleNode("maxBetAmount/spread").InnerText;
                            string limit_total = period.SelectSingleNode("maxBetAmount/totalPoints").InnerText;
                            string limit_team_total = period.SelectSingleNode("maxBetAmount/teamTotals").InnerText;
                        }

                        if (period.SelectSingleNode("moneyLine/homePrice") != null)
                        {
                            string home_line = period.SelectSingleNode("moneyLine/homePrice").InnerText;
                            string draw_line = period.SelectSingleNode("moneyLine/drawPrice").InnerText;
                            string away_line = period.SelectSingleNode("moneyLine/awayPrice").InnerText;
                            PinSQL.insert_odd_line(event_id, period_number, home_line, draw_line, away_line);
                        }

                        XmlNodeList spreads = period.SelectNodes("spreads/spread");
                        foreach (XmlNode spread in spreads)
                        {
                            string home_spread = spread.SelectSingleNode("homeSpread").InnerText;
                            string home_spread_odd = spread.SelectSingleNode("homePrice").InnerText;
                            string away_spread = spread.SelectSingleNode("awaySpread").InnerText;
                            string away_spread_odd = spread.SelectSingleNode("awayPrice").InnerText;
                            PinSQL.insert_odd_spread(event_id, period_number, home_spread, away_spread, home_spread_odd, away_spread_odd);
                        }
                        XmlNodeList totals = period.SelectNodes("totals/total");
                        foreach (XmlNode total in totals)
                        {
                            string total_point = total.SelectSingleNode("points").InnerText;
                            string total_over = total.SelectSingleNode("overPrice").InnerText;
                            string total_under = total.SelectSingleNode("underPrice").InnerText;
                            PinSQL.insert_odd_total(event_id, period_number, total_point, total_over, total_under);
                        }

                        if (period.SelectSingleNode("teamTotals") != null)
                        {
                            XmlNode team_total = period.SelectSingleNode("teamTotals");
                            string team_total_home = team_total.SelectSingleNode("homeTeamTotal/total").InnerText;
                            string team_total_home_under_price = team_total.SelectSingleNode("homeTeamTotal/overPrice").InnerText;
                            string team_total_home_over_price = team_total.SelectSingleNode("homeTeamTotal/underPrice").InnerText;
                            string team_total_away = team_total.SelectSingleNode("awayTeamTotal/total").InnerText;
                            string team_total_away_under_price = team_total.SelectSingleNode("awayTeamTotal/overPrice").InnerText;
                            string team_total_away_over_price = team_total.SelectSingleNode("awayTeamTotal/underPrice").InnerText;
                            PinSQL.insert_odd_team_total_home(event_id, period_number, team_total_home, team_total_home_over_price, team_total_home_under_price);
                            PinSQL.insert_odd_team_total_home(event_id, period_number, team_total_away, team_total_away_over_price, team_total_away_under_price);
                        }
                    }
                }
            }
        }
        return sb.ToString();
    }
}