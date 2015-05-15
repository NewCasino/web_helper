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
 
