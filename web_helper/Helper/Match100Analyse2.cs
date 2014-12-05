using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;



class Match100Analyse2
{
    static bool is_open_mongo = false;


    public static BsonDocument get_max_from_single_match(string start_time, string host, string client, int max_count, ArrayList list_websites)
    {


        string sql = "select * from europe_100 where start_time='{0}' and host='{1}' and client='{2}' and id in (select max(id) from europe_100 where start_time>'{3}'   group by website,start_time,host,client)";
        sql = string.Format(sql, start_time, host, client, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        DataTable dt = SQLServerHelper.get_table(sql);

        //ArrayList list_fix_websites = new ArrayList();

        double[] max = new double[3] { -999999, -999999, -99999 };
        string[] websites = new string[3] { "", "", "" };

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //bool is_fix_website = false;
            //foreach (string website in ArrayList)
            //{
            //    if (website == dt.Rows[i]["website"].ToString()) is_fix_website = true;
            //}
            //if (is_fix_website == false) continue;

            bool is_in_websites = false;
            foreach (string website in list_websites)
            {
                if (website == dt.Rows[i]["website"].ToString()) is_in_websites = true;
            }
            if (is_in_websites == false) continue;
            double[] input = new double[3]{Convert.ToDouble(dt.Rows[i]["odd_win"].ToString()),
                                            Convert.ToDouble(dt.Rows[i]["odd_draw"].ToString()),
                                            Convert.ToDouble(dt.Rows[i]["odd_lose"].ToString())};
            if (input[0] > max[0]) { max[0] = input[0]; websites[0] = dt.Rows[i]["website"].ToString(); }
            if (input[1] > max[1]) { max[1] = input[1]; websites[1] = dt.Rows[i]["website"].ToString(); }
            if (input[2] > max[2]) { max[2] = input[2]; websites[2] = dt.Rows[i]["website"].ToString(); }
        }

       
        BsonDocument doc_result = Match100Analyse2.get_single_match_result(max[0].ToString(), max[1].ToString(), max[2].ToString());




        BsonDocument doc = new BsonDocument();
        doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
        doc.Add("start_time", start_time);
        doc.Add("host", host);
        doc.Add("client", client);
        doc.Add("type", "single-match-max");
        doc.Add("bid_count", "50");
        doc.Add("min_value",doc_result["count_return"].ToString()); 
        doc.Add("result", doc_result);

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
        doc.Add("max_odds", array_orign_odds);



        BsonArray website_details = new BsonArray();
        foreach (string website in websites)
        {
            bool is_has = false;

            foreach (BsonDocument doc_item in website_details)
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
                    if (row["website"].ToString() == website)
                    {
                        odd_win = row["odd_win"].ToString();
                        odd_draw = row["odd_draw"].ToString();
                        odd_lose = row["odd_lose"].ToString();
                        odd_timespan = row["timespan"].ToString();
                        odd_id = row["id"].ToString();
                        odd_league = row["league"].ToString();
                    }
                }
                doc_item.Add("odd_win", odd_win);
                doc_item.Add("odd_draw", odd_draw);
                doc_item.Add("odd_lose", odd_lose);
                doc_item.Add("timespan", odd_timespan);
                doc_item.Add("id", odd_id);
                doc_item.Add("league", odd_league);

                website_details.Add(doc_item.AsBsonDocument);
            }


        }
        doc.Add("website_details", website_details); 


        return doc;
    } 
    public static BsonDocument get_single_match_result(string str_win, string str_draw, string str_lose)
    {
        BsonDocument doc_result = new BsonDocument();


        double win = Convert.ToDouble(str_win);
        double draw = Convert.ToDouble(str_draw);
        double lose = Convert.ToDouble(str_lose);
        double count_win = 0;
        double count_draw = 0;
        double count_lose = 0;
        double count_total = 0;
        double count_return = 0;
        double persent_win = 0;
        double persent_draw = 0;
        double persent_lose = 0;
        double persent_min = 0;
        BsonArray doc_array = new BsonArray();



        //add ideal document
        BsonDocument doc_1 = new BsonDocument();
        count_win =  1 / win  ;
        count_draw =  1 / draw  ;
        count_lose =  1 / lose  ;
        count_total = count_win + count_draw + count_lose;
        persent_win =  (count_win * win - count_total) / count_total * 100;
        persent_draw =  (count_draw * draw - count_total) / count_total  * 100;
        persent_lose =  (count_lose * lose - count_total) / count_total  * 100;
        count_return = double.MaxValue;
        if ((count_win * win - count_total) < count_return) count_return = count_win * win - count_total;
        if ((count_draw * draw - count_total) < count_return) count_return = count_draw * draw - count_total;
        if ((count_lose * lose - count_total) < count_return) count_return = count_lose * lose - count_total;
        persent_min = double.MaxValue;
        if (persent_win < persent_min) persent_min = persent_win;
        if (persent_draw < persent_min) persent_min = persent_draw;
        if (persent_lose < persent_min) persent_min = persent_lose;

        doc_1.Add("count_return", Math.Round(count_return,8).ToString("f8"));
        doc_1.Add("persent_min", Math.Round(persent_min,6).ToString("f6"));
        doc_1.Add("count_ideal", "1");
        doc_1.Add("count_actual", Math.Round(count_total, 8).ToString("f8"));
        doc_1.Add("count_win", Math.Round(count_win, 8).ToString("f8"));
        doc_1.Add("count_draw", Math.Round(count_draw, 8).ToString("f8"));
        doc_1.Add("count_lose", Math.Round(count_lose, 8).ToString("f8"));
        doc_1.Add("persent_win", Math.Round(persent_win, 6).ToString("f6"));
        doc_1.Add("persent_draw", Math.Round(persent_draw, 6).ToString("f6"));
        doc_1.Add("persent_lose", Math.Round(persent_lose, 6).ToString("f6"));
        



        //add some acture document
        double[] inputs = new double[] {50, 100, 200, 500, 1000, 2000, 5000, 10000 };
        foreach (double input in inputs)
        {
            BsonDocument doc_item = new BsonDocument();
            count_win = Math.Round(input / win);
            count_draw = Math.Round(input / draw);
            count_lose = Math.Round(input / lose);
            count_total = count_win + count_draw + count_lose;
            persent_win =  (count_win * win - count_total) / count_total  * 100;
            persent_draw =  (count_draw * draw - count_total) / count_total * 100;
            persent_lose =  (count_lose * lose - count_total) / count_total * 100;
            count_return = double.MaxValue;
            if ((count_win * win - count_total) < count_return) count_return = count_win * win - count_total;
            if ((count_draw * draw - count_total) < count_return) count_return = count_draw * draw - count_total;
            if ((count_lose * lose - count_total) < count_return) count_return = count_lose * lose - count_total;
            persent_min = double.MaxValue;
            if (persent_win < persent_min) persent_min = persent_win;
            if (persent_draw < persent_min) persent_min = persent_draw;
            if (persent_lose < persent_min) persent_min = persent_lose;

            doc_item.Add("count_return", Math.Round(count_return, 2).ToString("f2"));
            doc_item.Add("persent_min", Math.Round(persent_min, 2).ToString("f2"));
            doc_item.Add("count_ideal", Math.Round(input));
            doc_item.Add("count_actual", Math.Round(count_total));
            doc_item.Add("count_win", Math.Round(count_win));
            doc_item.Add("count_draw", Math.Round(count_draw));
            doc_item.Add("count_lose", Math.Round(count_lose));
            doc_item.Add("persent_win", Math.Round(persent_win, 2).ToString("f2"));
            doc_item.Add("persent_draw", Math.Round(persent_draw, 2).ToString("f2"));
            doc_item.Add("persent_lose", Math.Round(persent_lose, 2).ToString("f2"));

            doc_array.Add(doc_item);
        }
        doc_array.Add(doc_1);


        doc_result.Add("results", doc_array);
        doc_result.Add("count_return", doc_array[0]["count_return"].ToString());
        doc_result.Add("persent_min", doc_array[0]["persent_min"].ToString());

        return doc_result;
    }

    public static string get_info_from_doc(BsonDocument doc)
    {

        string result = "";
        StringBuilder sb = new StringBuilder();
        switch (doc["type"].ToString())
        {
            case "single-match-max":
                sb.Append("type:" + doc["type"].ToString() + "  doc id:" + doc["doc_id"].ToString()+Environment.NewLine);
                sb.Append(doc["start_time"].ToString() + "    " + doc["host"].PR(20) + doc["client"].PR(20) + Environment.NewLine);
                sb.Append("bid count:" + doc["bid_count"].ToString() + Environment.NewLine);
                sb.Append("return value: " + doc["result"]["count_return"].ToString() + Environment.NewLine);
                sb.Append("return persent: " + doc["result"]["persent_min"].ToString() + "%" + Environment.NewLine);


      

                sb.AppendLine("website detail info:");
                foreach (BsonDocument doc_item in doc["website_details"].AsBsonArray)
                {
                    sb.Append(doc_item["website"].PR(20));
                    sb.Append(doc_item["odd_win"].PR(10));
                    sb.Append(doc_item["odd_draw"].PR(10));
                    sb.Append(doc_item["odd_lose"].PR(10));
                    sb.Append(doc_item["timespan"].PR(20));
                    sb.Append(doc_item["id"].PR(10));
                    sb.Append(doc_item["league"].PR(50).E_REMOVE()+Environment.NewLine);
                }


                sb.Append("odd detail info:" + Environment.NewLine);
                sb.Append("WIN".PR(10) + doc["websites"].AsBsonArray[0].PR(20) + doc["max_odds"].AsBsonArray[0].PR(10) + Environment.NewLine);
                sb.Append("DRAW".PR(10) + doc["websites"].AsBsonArray[1].PR(20) + doc["max_odds"].AsBsonArray[1].PR(10) + Environment.NewLine);
                sb.Append("LOSE".PR(10) + doc["websites"].AsBsonArray[2].PR(20) + doc["max_odds"].AsBsonArray[2].PR(10) + Environment.NewLine);
                



                sb.Append("result detail info:" + Environment.NewLine);
                BsonArray doc_results = doc["result"]["results"].AsBsonArray;
                for (int i = 0; i < doc_results.Count; i++)
                {
                    sb.Append(doc_results[i]["count_ideal"].PR(10));
                    sb.Append(doc_results[i]["count_actual"].PR(10));
                    sb.Append(doc_results[i]["count_return"].PR(10));
                    sb.Append((doc_results[i]["persent_min"]+"%").PR(10)); 
                    sb.Append(doc_results[i]["count_win"].PR(10));
                    sb.Append(doc_results[i]["count_draw"].PR(10));
                    sb.Append(doc_results[i]["count_lose"].PR(10));
                    sb.Append((doc_results[i]["persent_win"] + "%").PR(10));
                    sb.Append((doc_results[i]["persent_draw"]+"%").PR(10));
                    sb.Append((doc_results[i]["persent_lose"]+"%").PR(10)+Environment.NewLine); 
                }
                break;
            default:
                break;
        }
        return sb.ToString();
    }


}

