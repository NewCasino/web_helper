using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
 
    class AnalyseInfo
    {
        public static string get_info_from_doc(BsonDocument doc)
        {

            string result = "";
            StringBuilder sb = new StringBuilder();
            switch (doc["type"].ToString())
            {
                case "2result":
                    sb.Append("type:" + doc["type"].ToString() + "  doc id:" + doc["doc_id"].ToString() + Environment.NewLine);
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
                        sb.Append(doc_item["league"].PR(50).E_REMOVE() + Environment.NewLine);
                    }


                    sb.Append("odd detail info:" + Environment.NewLine);
                    sb.Append("WIN".PR(10) + doc["websites"].AsBsonArray[0].PR(20) + doc["max_odds"].AsBsonArray[0].PR(10) + Environment.NewLine);
                    sb.Append("LOSE".PR(10) + doc["websites"].AsBsonArray[1].PR(20) + doc["max_odds"].AsBsonArray[1].PR(10) + Environment.NewLine); 




                    sb.Append("result detail info:" + Environment.NewLine);
                    BsonArray doc_results = doc["result"]["results"].AsBsonArray;
                    for (int i = 0; i < doc_results.Count; i++)
                    {
                        sb.Append(doc_results[i]["count_ideal"].PR(10));
                        sb.Append(doc_results[i]["count_actual"].PR(10));
                        sb.Append(doc_results[i]["count_return"].PR(10));
                        sb.Append((doc_results[i]["persent_min"] + "%").PR(10));
                        sb.Append(doc_results[i]["count_win"].PR(10)); 
                        sb.Append(doc_results[i]["count_lose"].PR(10));
                        sb.Append((doc_results[i]["persent_win"] + "%").PR(10)); 
                        sb.Append((doc_results[i]["persent_lose"] + "%").PR(10) + Environment.NewLine);
                    }
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }
    }
 
