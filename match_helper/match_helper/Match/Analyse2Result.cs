using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace match_helper.Match
{
    class Analyse2Result
    {
        static bool is_open_mongo = false;


        public static BsonDocument get_best(string start_time, string host, string client, int max_count, ArrayList list_websites)
        {
            string sql = "select * from europe_100 where start_time='{0}' and host='{1}' and client='{2}' and id in (select max(id) from europe_100 where start_time>'{3}'   group by website,start_time,host,client)";
            sql = string.Format(sql, start_time, host, client, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt = SQLServerHelper.get_table(sql);
 

            double[] max = new double[2] { -999999, -999999 };
            string[] websites = new string[2] { "", "" };

            for (int i = 0; i < dt.Rows.Count; i++)
            { 
                bool is_in_websites = false;
                foreach (string website in list_websites)
                {
                    if (website == dt.Rows[i]["website"].ToString()) is_in_websites = true;
                }
                if (is_in_websites == false) continue;
                double[] input = new double[2]{Convert.ToDouble(dt.Rows[i]["odd_win"].ToString()), Convert.ToDouble(dt.Rows[i]["odd_draw"].ToString())};
                if (input[0] > max[0]) { max[0] = input[0]; websites[0] = dt.Rows[i]["website"].ToString(); }
                if (input[1] > max[1]) { max[1] = input[1]; websites[1] = dt.Rows[i]["website"].ToString(); }
            }


            BsonDocument doc_max = Match100Analyse.get_min_by_wave(max, max_count);



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
                        if (row["website"].ToString() == website)
                        {
                            odd_win = row["odd_win"].ToString();
                            //odd_draw = row["odd_draw"].ToString();
                            odd_lose = row["odd_lose"].ToString();
                            odd_timespan = row["timespan"].ToString();
                            odd_id = row["id"].ToString();
                            odd_league = row["league"].ToString();
                        }
                    }
                    doc_item.Add("odd_win", odd_win);
                    //doc_item.Add("odd_draw", odd_draw);
                    doc_item.Add("odd_lose", odd_lose);
                    doc_item.Add("timespan", odd_timespan);
                    doc_item.Add("id", odd_id);
                    doc_item.Add("league", odd_league);

                    website_odds.Add(doc_item.AsBsonDocument);
                }


            }
            doc.Add("website_odds", website_odds);
            doc.Add("order_nos", doc_max["order_nos"].AsBsonArray);
            doc.Add("bids", doc_max["bids"].AsBsonArray);
            doc.Add("odds", doc_max["odds"].AsBsonArray);

            return doc;
        }



        public static BsonDocument get_min(double[] input, int max_count)
        {
            int length = input.Length;


            int[] bids = new int[length];
            int[] order_nos = new int[length];
            double[] odds = new double[length];
            double[] odds_temp = new double[length];



            for (int i = 0; i < length; i++)
            {
                bids[i] = 1;
                order_nos[i] = 0;
                odds[i] = input[i];
                odds_temp[i] = input[i];
            }


            //排序
            for (int step1 = 0; step1 < length; step1++)
            {
                int step_index = 0;
                double step_max = -999999999;
                for (int step2 = 0; step2 < length; step2++)
                {
                    if (odds_temp[step2] > step_max)
                    {
                        step_max = odds_temp[step2];
                        step_index = step2;
                    }
                }
                odds_temp[step_index] = 0;
                odds[step1] = step_max;
                order_nos[step1] = step_index;
            }


            bids[0] = max_count;
            for (int i = 1; i < length; i++)
            {
                bids[i] = (int)Math.Floor(odds[0] * bids[0] / odds[i]);
            }


            //total
            int bid_total = 0;
            for (int i = 0; i < length; i++)
            {
                bid_total = bid_total + bids[i];
            }

            //compute min and max
            double min = 999999999;
            double max = -999999999;
            for (int i = 0; i < length; i++)
            {
                if (bids[i] * odds[i] - bid_total < min) min = bids[i] * odds[i] - bid_total;
                if (bids[i] * odds[i] - bid_total > max) max = bids[i] * odds[i] - bid_total;
            }


            BsonDocument doc = new BsonDocument();
            doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
            doc.Add("type", "vary");
            doc.Add("bid_count", bid_total.ToString());
            doc.Add("min_value", min.ToString("f4"));
            doc.Add("max_value", max.ToString("f4"));

            BsonArray array_odds = new BsonArray();
            foreach (double odd in odds)
            {
                array_odds.Add(odd.ToString("f2"));
            }

            BsonArray array_bids = new BsonArray();
            foreach (int bid in bids)
            {
                array_bids.Add(bid.ToString());
            }

            BsonArray array_order_nos = new BsonArray();
            foreach (int order in order_nos)
            {
                array_order_nos.Add(order.ToString());
            }

            doc.Add("order_nos", array_order_nos);
            doc.Add("bids", array_bids);
            doc.Add("odds", array_odds);

            if (is_open_mongo) MongoHelper.insert_bson("match", doc);

            return doc;

        }
        public static BsonDocument get_min_by_wave(double[] input, int max_count)
        {
            int length = input.Length;


            int[] bids = new int[length];
            int[] order_nos = new int[length];
            double[] odds = new double[length];
            double[] odds_temp = new double[length];



            for (int i = 0; i < length; i++)
            {
                bids[i] = 1;
                order_nos[i] = 0;
                odds[i] = input[i];
                odds_temp[i] = input[i];
            }


            //排序
            for (int step1 = 0; step1 < length; step1++)
            {
                int step_index = 0;
                double step_max = -999999999;
                for (int step2 = 0; step2 < length; step2++)
                {
                    if (odds_temp[step2] > step_max)
                    {
                        step_max = odds_temp[step2];
                        step_index = step2;
                    }
                }
                odds_temp[step_index] = 0;
                odds[step1] = step_max;
                order_nos[step1] = step_index;
            }


            bids[0] = max_count;
            for (int i = 1; i < length; i++)
            {
                bids[i] = (int)Math.Floor(odds[0] * bids[0] / odds[i]);
            }


            //wave compute  
            double global_min = 0;
            double global_max = 0;
            int[] global_bids = new int[bids.Length];
            get_min_by_wave_child(ref global_bids, ref global_min, ref global_max, bids, odds);


            //total
            int bid_total = 0;
            for (int i = 0; i < length; i++)
            {
                bid_total = bid_total + global_bids[i];
            }

            BsonDocument doc = new BsonDocument();
            doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
            doc.Add("type", "vary");
            doc.Add("bid_count", bid_total.ToString());
            doc.Add("min_value", global_min.ToString("f4"));
            doc.Add("max_value", global_max.ToString("f4"));

            BsonArray array_odds = new BsonArray();
            foreach (double odd in odds)
            {
                array_odds.Add(odd.ToString("f2"));
            }

            BsonArray array_bids = new BsonArray();
            foreach (int bid in global_bids)
            {
                array_bids.Add(bid.ToString());
            }

            BsonArray array_order_nos = new BsonArray();
            foreach (int order in order_nos)
            {
                array_order_nos.Add(order.ToString());
            }

            doc.Add("order_nos", array_order_nos);
            doc.Add("bids", array_bids);
            doc.Add("odds", array_odds);

            if (is_open_mongo) MongoHelper.insert_bson("match", doc);

            return doc;

        }
        public static void get_min_by_wave_child(ref int[] global_bids, ref double global_min, ref double global_max, int[] bids, double[] odds)
        {
            int[] num = new int[] { -1, 0, 1 };
            global_min = -999999;
            global_max = 999999;


            if (bids.Length == 2)
            {
                for (int step1 = 0; step1 < 3; step1++)
                {

                    int[] bids_temp = new int[bids.Length];
                    bids_temp[0] = bids[0];
                    bids_temp[1] = bids[1] + num[step1];


                    int bid_total = 0;
                    for (int i = 0; i < bids.Length; i++)
                    {
                        bid_total = bid_total + bids_temp[i];
                    }

                    double min = 999999999;
                    double max = -999999999;
                    for (int i = 0; i < bids.Length; i++)
                    {
                        if (bids_temp[i] * odds[i] - bid_total < min) min = bids_temp[i] * odds[i] - bid_total;
                        if (bids_temp[i] * odds[i] - bid_total > max) max = bids_temp[i] * odds[i] - bid_total;
                    }

                    if (min > global_min)
                    {
                        global_min = min;
                        global_max = max;
                        for (int i = 0; i < bids.Length; i++)
                        {
                            global_bids[i] = bids_temp[i];
                        }
                    }
                }
            }


            if (bids.Length == 3)
            {
                for (int step1 = 0; step1 < 3; step1++)
                {
                    for (int step2 = 0; step2 < 3; step2++)
                    {
                        int[] bids_temp = new int[bids.Length];
                        bids_temp[0] = bids[0];
                        bids_temp[1] = bids[1] + num[step1];
                        bids_temp[2] = bids[2] + num[step2];


                        int bid_total = 0;
                        for (int i = 0; i < bids.Length; i++)
                        {
                            bid_total = bid_total + bids_temp[i];
                        }

                        double min = 999999999;
                        double max = -999999999;
                        for (int i = 0; i < bids.Length; i++)
                        {
                            if (bids_temp[i] * odds[i] - bid_total < min) min = bids_temp[i] * odds[i] - bid_total;
                            if (bids_temp[i] * odds[i] - bid_total > max) max = bids_temp[i] * odds[i] - bid_total;
                        }

                        if (min > global_min)
                        {
                            global_min = min;
                            global_max = max;
                            for (int i = 0; i < bids.Length; i++)
                            {
                                global_bids[i] = bids_temp[i];
                            }
                        }
                    }
                }
            }


            if (bids.Length == 9)
            {
                for (int step1 = 0; step1 < 3; step1++)
                {
                    for (int step2 = 0; step2 < 3; step2++)
                    {
                        for (int step3 = 0; step3 < 3; step3++)
                        {
                            for (int step4 = 0; step4 < 3; step4++)
                            {
                                for (int step5 = 0; step5 < 3; step5++)
                                {
                                    for (int step6 = 0; step6 < 3; step6++)
                                    {
                                        for (int step7 = 0; step7 < 3; step7++)
                                        {
                                            for (int step8 = 0; step8 < 3; step8++)
                                            {
                                                int[] bids_temp = new int[bids.Length];
                                                bids_temp[0] = bids[0];
                                                bids_temp[1] = bids[1] + num[step1];
                                                bids_temp[2] = bids[2] + num[step2];
                                                bids_temp[3] = bids[3] + num[step3];
                                                bids_temp[4] = bids[4] + num[step4];
                                                bids_temp[5] = bids[5] + num[step5];
                                                bids_temp[6] = bids[6] + num[step6];
                                                bids_temp[7] = bids[7] + num[step7];
                                                bids_temp[8] = bids[8] + num[step8];


                                                int bid_total = 0;
                                                for (int i = 0; i < bids.Length; i++)
                                                {
                                                    bid_total = bid_total + bids_temp[i];
                                                }

                                                double min = 999999999;
                                                double max = -999999999;
                                                for (int i = 0; i < bids.Length; i++)
                                                {
                                                    if (bids_temp[i] * odds[i] - bid_total < min) min = bids_temp[i] * odds[i] - bid_total;
                                                    if (bids_temp[i] * odds[i] - bid_total > max) max = bids_temp[i] * odds[i] - bid_total;
                                                }

                                                if (min > global_min)
                                                {
                                                    global_min = min;
                                                    global_max = max;
                                                    for (int i = 0; i < bids.Length; i++)
                                                    {
                                                        global_bids[i] = bids_temp[i];
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }

        }

        public static string get_info_from_doc(BsonDocument doc)
        {

            string result = "";
            switch (doc["type"].ToString())
            {
                case "vary":
                    result = "type:" + doc["type"].ToString() + "  doc id:" + doc["doc_id"].ToString() + Environment.NewLine +
                     "bid count:" + doc["bid_count"].ToString() + Environment.NewLine +
                     "return value: " + doc["min_value"].ToString() + "  ~  " + doc["max_value"].ToString() + Environment.NewLine +
                     "return persent: " + (Convert.ToDouble(doc["min_value"].ToString()) / Convert.ToDouble(doc["bid_count"].ToString()) * 100).ToString("f6") + "%" + Environment.NewLine;

                    result = result + "odds".PR(10);
                    foreach (string value in doc["odds"].AsBsonArray)
                    {
                        result = result + value.PR(15);
                    }
                    result = result + Environment.NewLine;


                    result = result + "bids".PR(10);
                    foreach (string value in doc["bids"].AsBsonArray)
                    {
                        result = result + value.PR(15);
                    }
                    result = result + Environment.NewLine;

                    break;

                case "2result":
                    result = "type:" + doc["type"].ToString() + "  doc id:" + doc["doc_id"].ToString() + Environment.NewLine +
                     doc["start_time"].ToString() + "    " + doc["host"].PR(20) + doc["client"].PR(20) + Environment.NewLine +
                     "bid count:" + doc["bid_count"].ToString() + Environment.NewLine +
                     "return value: " + doc["min_value"].ToString() + "  ~  " + doc["max_value"].ToString() + Environment.NewLine +
                     "return persent: " + (Convert.ToDouble(doc["min_value"].ToString()) / Convert.ToDouble(doc["bid_count"].ToString()) * 100).ToString("f6") + "%" + Environment.NewLine;

                    result = result + "websites".PR(10);
                    foreach (string value in doc["websites"].AsBsonArray)
                    {
                        result = result + value.PR(15);
                    }
                    result = result + Environment.NewLine;


                    result = result + "orign".PR(10);
                    foreach (string value in doc["orign_odds"].AsBsonArray)
                    {
                        result = result + value.PR(15);
                    }
                    result = result + Environment.NewLine;

                    result = result + "order no".PR(10);
                    foreach (string value in doc["order_nos"].AsBsonArray)
                    {
                        result = result + value.PR(15);
                    }
                    result = result + Environment.NewLine;

                    result = result + "odds".PR(10);
                    foreach (string value in doc["odds"].AsBsonArray)
                    {
                        result = result + value.PR(15);
                    }
                    result = result + Environment.NewLine;


                    result = result + "bids".PR(10);
                    foreach (string value in doc["bids"].AsBsonArray)
                    {
                        result = result + value.PR(15);
                    }
                    result = result + Environment.NewLine;

                    result = result + "website detail info:" + Environment.NewLine;
                    foreach (BsonDocument doc_item in doc["website_odds"].AsBsonArray)
                    {
                        result = result + doc_item["website"].PR(20);
                        result = result + doc_item["odd_win"].PR(10);
                        result = result + doc_item["odd_draw"].PR(10);
                        result = result + doc_item["odd_lose"].PR(10);
                        result = result + doc_item["timespan"].PR(20);
                        result = result + doc_item["id"].PR(10);
                        result = result + doc_item["league"].PR(50).E_REMOVE() + Environment.NewLine;
                    }


                    result = result + "odd detail info:" + Environment.NewLine;
                    for (int i = 0; i < doc["order_nos"].AsBsonArray.Count; i++)
                    {
                        string order_no = doc["order_nos"].AsBsonArray[i].ToString();
                        string website = doc["websites"].AsBsonArray[Convert.ToInt32(order_no)].ToString();
                        string odd = doc["odds"].AsBsonArray[i].ToString();
                        string bid = doc["bids"].AsBsonArray[i].ToString();
                        result = result + website.PR(20) + odd.PR(10) + bid.PR(10);
                        foreach (BsonDocument doc_item in doc["website_odds"].AsBsonArray)
                        {
                            if (doc_item["website"].ToString() == website)
                            {
                                switch (order_no)
                                {
                                    case "0":
                                        result = result + "Win".PR(10) + doc_item["odd_win"].PR(10);
                                        break;
                                    case "1":
                                        result = result + "Draw".PR(10) + doc_item["odd_draw"].PR(10);
                                        break;
                                    case "2":
                                        result = result + "Lose".PR(10) + doc_item["odd_lose"].PR(10);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        result = result + Environment.NewLine;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
