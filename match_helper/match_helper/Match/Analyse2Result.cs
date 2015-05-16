using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;


class Analyse2Result
{
    static bool is_open_mongo = false;


    public static BsonDocument get_best(int odd_id, int max_count, ArrayList list_websites)
    {
        string sql = " select b.start_time,b.team1,b.team2,d.name type_name,d.name website_name,a.r1,a.r2,a.o1,a.o2,a.timespan a.id odd_id" +
                     " from a_odd a" +
                     " left join  a_event b on  a.event_id=b.id" +
                     " left join  a_type  c on  a.type_id=c.id" +
                     " left join  a_website d on a.website_id=d.id" +
                     " where a.id={0}";
        sql = string.Format(sql, odd_id.ToString());
        DataTable dt = SQLServerHelper.get_table(sql);
        string start_time = "";
        string host = "";
        string client = "";
        if (dt.Rows.Count > 0)
        {
            start_time = dt.Rows[0]["start_time"].ToString();
            host = dt.Rows[0]["team1"].ToString();
            client = dt.Rows[0]["team2"].ToString();

            double[] max = new double[2] { -999999, -999999 };
            string[] websites = new string[2] { "", "" };

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool is_in_websites = false;
                foreach (string website in list_websites)
                {
                    if (website == dt.Rows[i]["website_name"].ToString()) is_in_websites = true;
                }
                if (is_in_websites == false) continue;
                double[] input = new double[2] { Convert.ToDouble(dt.Rows[i]["o1"].ToString()), Convert.ToDouble(dt.Rows[i]["o2"].ToString()) };
                if (input[0] > max[0]) { max[0] = input[0]; websites[0] = dt.Rows[i]["website_name"].ToString(); }
                if (input[1] > max[1]) { max[1] = input[1]; websites[1] = dt.Rows[i]["website_name"].ToString(); }
            }


            BsonDocument doc_max = AnalyseBase.get_min_by_wave(max, max_count);

            BsonDocument doc = new BsonDocument();
            doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
            doc.Add("start_time", start_time);
            doc.Add("host", host);
            doc.Add("client", client);
            doc.Add("type", "2result");
            doc.Add("bid_count", doc_max["bid_count"].ToString());
            doc.Add("min_value", doc_max["min_value"].ToString());
            doc.Add("max_value", doc_max["max_value"].ToString());

            BsonArray array_websites = new BsonArray();
            foreach (string website in websites)
            {
                array_websites.Add(website);
            }
            doc.Add("websites", array_websites);

            BsonArray array_orign_odds = new BsonArray();
            foreach (double odd in max)
            {
                array_orign_odds.Add(odd.ToString("f2"));
            }
            doc.Add("orign_odds", array_orign_odds);


            BsonArray website_odds = new BsonArray();
            foreach (string website in websites)
            {
                bool is_has = false;

                foreach (BsonDocument doc_item in website_odds)
                {
                    if (doc_item["website"].ToString() == website)
                    {
                        is_has = true;
                    }
                }
                if (is_has == false)
                {
                    BsonDocument doc_item = new BsonDocument();
                    doc_item.Add("website", website);
                    string odd_win = "";
                    string odd_draw = "";
                    string odd_lose = "";
                    string odd_timespan = "";
                    string odd_id = "";
                    string odd_league = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["website_name"].ToString() == website)
                        {
                            odd_win = row["o1"].ToString();
                            odd_lose = row["o2"].ToString();
                            odd_timespan = row["timespan"].ToString();
                            odd_id = row["odd_id"].ToString();
                            //odd_league = row["league"].ToString();
                        }
                    }
                    doc_item.Add("odd_win", odd_win);
                    doc_item.Add("odd_lose", odd_lose);
                    doc_item.Add("timespan", odd_timespan);
                    doc_item.Add("id", odd_id);
                    // doc_item.Add("league", odd_league); 
                    website_odds.Add(doc_item.AsBsonDocument);
                }


            }
            doc.Add("website_odds", website_odds);
            doc.Add("order_nos", doc_max["order_nos"].AsBsonArray);
            doc.Add("bids", doc_max["bids"].AsBsonArray);
            doc.Add("odds", doc_max["odds"].AsBsonArray);

            return doc;
        }
    }

}