using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data;
using MongoDB.Bson;

 
public class PinApi
{
    /*--------------------------------------------------------
     * ODDS_FORMAT:
     * AMERICAN	
     * DECIMAL	
     * HONGKONG	
     * INDONESIAN	
     * MALAY	
     * 
     * 
     * PERIOD ：
     * 0 - Game
     * 1 - 1st Half
     * 2 - 2nd Half
     * 
     * 
     * LANGUAGE CULTURE:
     * English	en-US 
     * British	en-GB
     * ChineseSI	zh-CN
     * ChineseTR	zh-TW   
     * Finnish	fi-FI    
     * German	de-DE    
     * Hebrew	he-IL    
     * Italian	it-IT    
     * Norwegian	nb-NO    
     * Portuguese	pt-BR    
     * Russian	ru-RU    
     * Spanish	es-ES    
     * Swedish	sv-SE    
     * Thai	th-TH    
     * Polish	pl-PL    
     * French	fr-FR
     * Greek	el-GR  
     * Japanese	ja-JP   
     * Korean	ko-KR   
     * Vietnamese	vi-VN    
     * Indonesian	id-ID
     * Czech	cs-CZ
    //--------------------------------------------------------*/
    public static string sports()
    {
        BsonDocument doc = new BsonDocument() { {"test",new BsonArray{1,2,3,4,}} };
        string url = "https://api.pinnaclesports.com/v1/sports";
        return PinHelper.get(url);
    }

    public static  string leagues()
    {
        string url = "https://api.pinnaclesports.com/v1/leagues?sportid=29";
        return PinHelper.get(url);
    }
    public static string currencies()
    {
        string url = "https://api.pinnaclesports.com/v1/currencies";
        return PinHelper.get(url);
    }

    public static string feeds()
    {
        string url = "https://api.pinnaclesports.com/v1/feed?oddsFormat=1";
        return PinHelper.get(url);
    }
    public static string feeds(string last_time)
    {
        string url = "https://api.pinnaclesports.com/v1/feed?last={0}&oddsFormat=1";
        url = string.Format(url, last_time);
        return PinHelper.get(url);
    }
    public static string feeds_by_sport_id(string sport_id)
    {
        string url = "https://api.pinnaclesports.com/v1/feed?sportid={0}&oddsFormat=1";
        url = string.Format(url, sport_id);
        return PinHelper.get(url);
    }
    public static string feeds_by_sport_id(string sport_id, string last_time)
    {
        string url = "/v1/feed?sportid={0}&last={1}&odddsFormat=DECIMAL";
        url = string.Format(url, sport_id, last_time);
        return PinHelper.get(url);
    }


    public static string balance()
    {
        string url = "/v1/client/balance";
        return PinHelper.get(url);
    }
    public static string place(string unique_id, string is_better_line, string win_risk_state, string sport_id, string event_id, string period_number, string line_id, string bet_type, string team, string stake)
    {
        string url = "/v1/bets/place";
        BsonDocument doc_param = new BsonDocument();
        doc_param.Add("uniqueRequestId", Guid.NewGuid().ToString());
        doc_param.Add("acceptBetterLine", "TRUE");
        doc_param.Add("winRiskStake", "WIN");
        doc_param.Add("sportId", sport_id);
        doc_param.Add("eventId", event_id);
        doc_param.Add("lineId", line_id);
        doc_param.Add("periodNumber", period_number);
        doc_param.Add("betType", bet_type);
        doc_param.Add("team", team);
        doc_param.Add("stake", stake);
        doc_param.Add("oddsFormat", "DECIMAL");
        return PinHelper.post(url, doc_param.ToString());
    }
    public static string line(string sport_id, string league_id, string event_id, string period_number, string bet_type)
    {
        string url = "v1/line?sportId={0}&leagueId={1}&eventId={2}&periodNumber={3}&betType={4}&oddsFormat=DECIMAL";
        url = string.Format(url, sport_id, league_id, event_id, period_number, bet_type);
        return PinHelper.get(url);
    }
    public static string bets()
    {
        string url = "/v1/bets";
        return PinHelper.get(url);
    }
    public static string isruning(string elapsed, string state, string id)
    {
        string url = "/v1/isrunning?elapsed={0}&state={1}&id={2}";
        url = string.Format(url, elapsed, state, id);
        return PinHelper.get(url);
    }
    public static string translation(string language, string txt)
    {
        string url = "/v1/translations?cultureCodes={0}&baseTexts={1}";
        url = string.Format(url, language, txt);
        return PinHelper.get(url);
    }
}

