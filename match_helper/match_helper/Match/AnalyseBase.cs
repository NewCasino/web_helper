using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;


class AnalyseBase
{
    static bool is_open_mongo = false;
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

        //BsonArray array_odds = new BsonArray();
        //foreach (double odd in odds)
        //{
        //    array_odds.Add(odd.ToString("f2"));
        //}

        //BsonArray array_bids = new BsonArray();
        //foreach (int bid in global_bids)
        //{
        //    array_bids.Add(bid.ToString());
        //}

        //BsonArray array_order_nos = new BsonArray();
        //foreach (int order in order_nos)
        //{
        //    array_order_nos.Add(order.ToString());
        //}

        //排序完整后送出
        BsonArray array_odds = new BsonArray();
        BsonArray array_bids = new BsonArray();
        for (int i = 0; i < order_nos.Length; i++)
        {
            for (int j = 0; j < order_nos.Length; j++)
            {
                if (i == Convert.ToInt32(order_nos[j].ToString()))
                {
                    array_odds.Add(odds[j].ToString());
                    array_bids.Add(global_bids[j].ToString());
                }
            }
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

        #region 2 results
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
        #endregion

        #region 3 results
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
        #endregion

        #region 9 results
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
        #endregion 9 results
    }
}

