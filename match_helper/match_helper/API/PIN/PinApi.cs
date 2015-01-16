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
    ODDS_FORMAT:
    AMERICAN	
    DECIMAL	
    HONGKONG	
    INDONESIAN	
    MALAY	
    
    LANGUAGE CULTURE:
    English	en-US
    British	en-GB
    ChineseSI	zh-CN
    ChineseTR	zh-TW
    Finnish	fi-FI
    German	de-DE
    Hebrew	he-IL
    Italian	it-IT
    Norwegian	nb-NO
    Portuguese	pt-BR
    Russian	ru-RU
    Spanish	es-ES
    Swedish	sv-SE
    Thai	th-TH
    Polish	pl-PL
    French	fr-FR
    Greek	el-GR
    Japanese	ja-JP
    Korean	ko-KR
    Vietnamese	vi-VN
    Indonesian	id-ID
    Czech	cs-CZ
    
     * 
    //--------------------------------------------------------*/
    public string sports()
    {
        string url = "/v1/sports";
        return PinHelper.get(url);
    }
    public string leagues()
    {
        string url = "/v1/leagues?sportid={0}";
        return PinHelper.get(url);
    }
    public string currencies()
    {
        string url = "/v1/currencies";
        return PinHelper.get(url);
    }

    public string feeds(string sport_id)
    {
        string url = "/v1/feed?sportid={0}";
        url = string.Format(url, sport_id);
        return PinHelper.get(url);
    }
    public string feeds(string sport_id, string last_time)
    {
        string url = "/v1/feed?sportid={0}&last={1}";
        url = string.Format(url, sport_id, last_time);
        return PinHelper.get(url);
    }


    public string balance()
    {
        string url = "/v1/client/balance";
        return PinHelper.get(url);
    }
    public string place(string unique_id, string is_better_line, string win_risk_state, string sport_id, string event_id, string line_id, string period_number, string bet_type, string team, string stake, string odds_format)
    {
        string url = "/v1/bets/place";
        BsonDocument doc_param = new BsonDocument();
        doc_param.Add("uniqueRequestId", "3ca3e7a7-12e1-4907-8b84-00f02e814b1d");
        doc_param.Add("acceptBetterLine", "TRUE");
        doc_param.Add("winRiskStake", "WIN");
        doc_param.Add("sportId", 29);
        doc_param.Add("eventId", 307962592);
        doc_param.Add("lineId", 103648474);
        doc_param.Add("periodNumber", 0);
        doc_param.Add("betType", "MONEYLINE");
        doc_param.Add("team", "DRAW");
        doc_param.Add("stake", 150);
        doc_param.Add("oddsFormat", "AMERICAN");
        return PinHelper.post(url, doc_param.ToString());
    }
    public string line(string sport_id, string league_id, string event_id, string period_number, string bet_type, string odds_format)
    {
        string url = "v1/line?sportId={0}&leagueId={1}&eventId={2}&periodNumber={3}&betType={4}&oddsFormat={5}";
        url = string.Format(url, sport_id, league_id, event_id, period_number, bet_type, odds_format);
        return PinHelper.get(url);
    }
    public string bets()
    {
        string url = "/v1/bets";
        return PinHelper.get(url);
    }
    public string isruning(string elapsed, string state, string id)
    {
        string url = "/v1/isrunning?elapsed={0}&state={1}&id={2}";
        url = string.Format(url, elapsed, state, id);
        return PinHelper.get(url);
    }
    public string translation(string language, string txt)
    {
        //English	en-US
        //British	en-GB
        //ChineseSI	zh-CN
        //ChineseTR	zh-TW
        //Finnish	fi-FI
        //German	de-DE
        //Hebrew	he-IL
        //Italian	it-IT
        //Norwegian	nb-NO
        //Portuguese	pt-BR
        //Russian	ru-RU
        //Spanish	es-ES
        //Swedish	sv-SE
        //Thai	th-TH
        //Polish	pl-PL
        //French	fr-FR
        //Greek	el-GR
        //Japanese	ja-JP
        //Korean	ko-KR
        //Vietnamese	vi-VN
        //Indonesian	id-ID
        //Czech	cs-CZ
        string url = "/v1/translations?cultureCodes={0}&baseTexts={1}";
        url = string.Format(url, language, txt);
        return PinHelper.get(url);
    }
}

