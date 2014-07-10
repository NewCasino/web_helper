using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;



class MatchCompany
{
    static bool is_open_mongo = false;
    public static string get_min_from(string win, string draw, string lose, int max_count)
    {
        double wdl_w = Convert.ToDouble(win);
        double wdl_d = Convert.ToDouble(draw);
        double wdl_l = Convert.ToDouble(lose);


        int[] bids = new int[] { 1, 1, 1 };
        int[] bids_temp = new int[] { 1, 1, 1 };

        double[] profits = new double[] { wdl_w, wdl_d, wdl_l };
        double[] profits_temp = new double[] { wdl_w, wdl_d, wdl_l };
        for (int step1 = 0; step1 < 3; step1++)
        {
            int step_index = 0;
            double step_max = -999999999;
            for (int step2 = 0; step2 < 3; step2++)
            {
                if (profits_temp[step2] > step_max)
                {
                    step_max = profits_temp[step2];
                    step_index = step2;
                }
            }
            profits_temp[step_index] = 0;
            profits[step1] = step_max;
        }


        DateTime dt_start = DateTime.Now;
        bids[0] = max_count;
        bids[1] = (int)Math.Floor(profits[0] * bids[0] / profits[1]);
        bids[2] = (int)Math.Floor(profits[0] * bids[0] / profits[2]);


        bids_temp[0] = bids[0];
        bids_temp[1] = bids[1];
        bids_temp[2] = bids[2];

        //上下调整1
        double max_persent = -99999;
        for (int ajust1 = 0; ajust1 < 2; ajust1++)
        {
            for (int ajust2 = 0; ajust2 < 2; ajust2++)
            {

                int bid0 = bids[0];
                int bid1 = bids[1] + ajust1;
                int bid2 = bids[2] + ajust2;

                int total = bid0 + bid1 + bid2;
                double min_temp = 999999999;
                if (bid0 * profits[0] - total < min_temp) min_temp = bid0 * profits[0] - total;
                if (bid1 * profits[1] - total < min_temp) min_temp = bid1 * profits[1] - total;
                if (bid2 * profits[2] - total < min_temp) min_temp = bid2 * profits[2] - total;


                if (min_temp / total > max_persent)
                {
                    bids_temp[0] = bid0;
                    bids_temp[1] = bid1;
                    bids_temp[2] = bid2;
                }
            }
        }

        bids[0] = bids_temp[0];
        bids[1] = bids_temp[1];
        bids[2] = bids_temp[2];


        int bid_total = bids[0] + bids[1] + bids[2];
        double min = 999999999;
        double max = -999999999;
        if (bids[0] * profits[0] - bid_total < min) min = bids[0] * profits[0] - bid_total;
        if (bids[1] * profits[1] - bid_total < min) min = bids[1] * profits[1] - bid_total;
        if (bids[2] * profits[2] - bid_total < min) min = bids[2] * profits[2] - bid_total;
        if (bids[0] * profits[0] - bid_total > max) max = bids[0] * profits[0] - bid_total;
        if (bids[1] * profits[1] - bid_total > max) max = bids[1] * profits[1] - bid_total;
        if (bids[2] * profits[2] - bid_total > max) max = bids[2] * profits[2] - bid_total;


        string result = "Min Value:" + min.ToString("f4") + Environment.NewLine +
                        "Max Value:" + max.ToString("f4") + Environment.NewLine +
                        "Persent:" + (min / bid_total * 100).ToString("f4") + "%" + Environment.NewLine + Environment.NewLine;
        return result;

    }
    public static BsonDocument get_min(double[] input, int max_count)
    {
        int length = input.Length;


        int[] bids = new int[length]; 
        double[] profits = new double[31];
        double[] profits_temp = new double[31];


        for (int i = 0; i < length; i++)
        {
            bids[i] = 1; 
            profits[i] = input[i];
            profits_temp[i] = input[i];
        }


        //排序
        for (int step1 = 0; step1 < length; step1++)
        {
            int step_index = 0;
            double step_max = -999999999;
            for (int step2 = 0; step2 < length; step2++)
            {
                if (profits_temp[step2] > step_max)
                {
                    step_max = profits_temp[step2];
                    step_index = step2;
                }
            }
            profits_temp[step_index] = 0;
            profits[step1] = step_max;
        }


        bids[0] = max_count;
        for (int i = 1; i < length; i++)
        {
            bids[i] = (int)Math.Floor(profits[0] * bids[0] / profits[i]);
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
            if (bids[i] * profits[i] - bid_total < min) min = bids[i] * profits[i] - bid_total;
            if (bids[i] * profits[i] - bid_total > max) max = bids[i] * profits[i] - bid_total;
        }


        BsonDocument doc = new BsonDocument();
        return doc;

    }

}

