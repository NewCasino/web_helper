using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;



class MatchCompany
{
    static bool is_open_mongo = false;


    public static BsonDocument get_max_from_single_match(string start_time, string host, string client, int max_count, ArrayList list_companys)
    {


        string sql = "select * from europe_new where start_time='{0}' and host='{1}' and client='{2}'";
        sql = string.Format(sql, start_time, host, client);
        DataTable dt = SQLServerHelper.get_table(sql);

        //ArrayList list_fix_companys = new ArrayList();

        double[] max = new double[3] { -999999, -999999, -99999 };
        string[] companys = new string[3] { "", "", "" };

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //bool is_fix_company = false;
            //foreach (string company in ArrayList)
            //{
            //    if (company == dt.Rows[i]["company"].ToString()) is_fix_company = true;
            //}
            //if (is_fix_company == false) continue;

            bool is_in_companys = false;
            foreach (string company in list_companys)
            {
                if (company == dt.Rows[i]["company"].ToString()) is_in_companys = true;
            }
            if (is_in_companys == false) continue;
            double[] input = new double[3]{Convert.ToDouble(dt.Rows[i]["profit_win"].ToString()),
                                            Convert.ToDouble(dt.Rows[i]["profit_draw"].ToString()),
                                            Convert.ToDouble(dt.Rows[i]["profit_lose"].ToString())};
            if (input[0] > max[0]) { max[0] = input[0]; companys[0] = dt.Rows[i]["company"].ToString(); }
            if (input[1] > max[1]) { max[1] = input[1]; companys[1] = dt.Rows[i]["company"].ToString(); }
            if (input[2] > max[2]) { max[2] = input[2]; companys[2] = dt.Rows[i]["company"].ToString(); }
        }


        BsonDocument doc_max = MatchCompany.get_min_by_wave(max, max_count);



        BsonDocument doc = new BsonDocument();
        doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
        doc.Add("start_time", start_time);
        doc.Add("host", host);
        doc.Add("client", client);
        doc.Add("type", "single-match-max");
        doc.Add("bid_count", doc_max["bid_count"].ToString());
        doc.Add("min_value", doc_max["min_value"].ToString());
        doc.Add("max_value", doc_max["max_value"].ToString());

        BsonArray array_companys = new BsonArray();
        foreach (string company in companys)
        {
            array_companys.Add(company);
        }
        doc.Add("companys", array_companys);

        BsonArray array_orign_profits = new BsonArray();
        foreach (double profit in max)
        {
            array_orign_profits.Add(profit.ToString("f2"));
        }
        doc.Add("orign_profits", array_orign_profits);


        BsonArray company_odds = new BsonArray();
        foreach (string company in companys)
        {
            bool is_has = false;

            foreach (BsonDocument doc_item in company_odds)
            {
                if (doc_item["company"].ToString() == company)
                {
                    is_has = true;
                }
            }
            if (is_has == false)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("company", company);
                string profit_win = "";
                string profit_draw = "";
                string profit_lose = "";
                foreach (DataRow row in dt.Rows)
                {
                    if (row["company"].ToString() == company)
                    {
                        profit_win = row["profit_win"].ToString();
                        profit_draw = row["profit_draw"].ToString();
                        profit_lose = row["profit_lose"].ToString();
                    }
                }
                doc_item.Add("profit_win", profit_win);
                doc_item.Add("profit_draw", profit_draw);
                doc_item.Add("profit_lose", profit_lose);

                company_odds.Add(doc_item.AsBsonDocument);
            }


        }
        doc.Add("company_odds", company_odds);

        doc.Add("order_nos", doc_max["order_nos"].AsBsonArray);
        doc.Add("bids", doc_max["bids"].AsBsonArray);
        doc.Add("profits", doc_max["profits"].AsBsonArray);


        return doc;
    }
    public static BsonDocument get_max_from_two_match(string start_time1, string host1, string client1, string start_time2, string host2, string client2, int max_count, ArrayList list_companys)
    {
        string sql = "select * from europe_new where start_time='{0}' and host='{1}' and client='{2}'";
        sql = string.Format(sql, start_time1, host1, client1);
        DataTable dt1 = SQLServerHelper.get_table(sql);

        sql = "select * from europe_new where start_time='{0}' and host='{1}' and client='{2}'";
        sql = string.Format(sql, start_time2, host2, client2);
        DataTable dt2 = SQLServerHelper.get_table(sql);



        double[] max = new double[9] { -999999, -999999, -999999, -999999, -999999, -999999, -999999, -999999, -999999 };
        string[] companys = new string[9] { "", "", "", "", "", "", "", "", "" };

        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            bool is_in_companys = false;
            foreach (string company in list_companys)
            {
                if (company == dt1.Rows[i]["company"].ToString()) is_in_companys = true;
            }
            if (is_in_companys == false) continue;



            int row_no = -1;
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                if (dt1.Rows[i]["company"].ToString() == dt2.Rows[j]["company"].ToString())
                {
                    row_no = j;
                    break;
                }
            }

            if (row_no != -1)
            {
                double ww = Convert.ToDouble(dt1.Rows[i]["profit_win"].ToString()) * Convert.ToDouble(dt2.Rows[row_no]["profit_win"].ToString());
                double wd = Convert.ToDouble(dt1.Rows[i]["profit_win"].ToString()) * Convert.ToDouble(dt2.Rows[row_no]["profit_draw"].ToString());
                double wl = Convert.ToDouble(dt1.Rows[i]["profit_win"].ToString()) * Convert.ToDouble(dt2.Rows[row_no]["profit_lose"].ToString());
                double dw = Convert.ToDouble(dt1.Rows[i]["profit_draw"].ToString()) * Convert.ToDouble(dt2.Rows[row_no]["profit_win"].ToString());
                double dd = Convert.ToDouble(dt1.Rows[i]["profit_draw"].ToString()) * Convert.ToDouble(dt2.Rows[row_no]["profit_draw"].ToString());
                double dl = Convert.ToDouble(dt1.Rows[i]["profit_draw"].ToString()) * Convert.ToDouble(dt2.Rows[row_no]["profit_lose"].ToString());
                double lw = Convert.ToDouble(dt1.Rows[i]["profit_lose"].ToString()) * Convert.ToDouble(dt2.Rows[row_no]["profit_win"].ToString());
                double ld = Convert.ToDouble(dt1.Rows[i]["profit_lose"].ToString()) * Convert.ToDouble(dt2.Rows[row_no]["profit_draw"].ToString());
                double ll = Convert.ToDouble(dt1.Rows[i]["profit_lose"].ToString()) * Convert.ToDouble(dt2.Rows[row_no]["profit_lose"].ToString());

                if (ww > max[0]) { max[0] = ww; companys[0] = dt1.Rows[i]["company"].ToString(); }
                if (wd > max[1]) { max[1] = wd; companys[1] = dt1.Rows[i]["company"].ToString(); }
                if (wl > max[2]) { max[2] = wl; companys[2] = dt1.Rows[i]["company"].ToString(); }
                if (dw > max[3]) { max[3] = dw; companys[3] = dt1.Rows[i]["company"].ToString(); }
                if (dd > max[4]) { max[4] = dd; companys[4] = dt1.Rows[i]["company"].ToString(); }
                if (dl > max[5]) { max[5] = dl; companys[5] = dt1.Rows[i]["company"].ToString(); }
                if (lw > max[6]) { max[6] = lw; companys[6] = dt1.Rows[i]["company"].ToString(); }
                if (ld > max[7]) { max[7] = ld; companys[7] = dt1.Rows[i]["company"].ToString(); }
                if (ll > max[8]) { max[8] = ll; companys[8] = dt1.Rows[i]["company"].ToString(); }
            }
        }


        BsonDocument doc_max = MatchCompany.get_min_by_wave(max, max_count);



        BsonDocument doc = new BsonDocument();
        doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
        doc.Add("start_time1", start_time1);
        doc.Add("host1", host1);
        doc.Add("client1", client1);
        doc.Add("start_time2", start_time2);
        doc.Add("host2", host2);
        doc.Add("client2", client2);
        doc.Add("type", "two-match-max");
        doc.Add("bid_count", doc_max["bid_count"].ToString());
        doc.Add("min_value", doc_max["min_value"].ToString());
        doc.Add("max_value", doc_max["max_value"].ToString());

        BsonArray array_companys = new BsonArray();
        foreach (string company in companys)
        {
            array_companys.Add(company);
        }
        doc.Add("companys", array_companys);

        BsonArray array_orign_profits = new BsonArray();
        foreach (double profit in max)
        {
            array_orign_profits.Add(profit.ToString("f2"));
        }
        doc.Add("orign_profits", array_orign_profits);


        //Detail Company Odds
        BsonArray company_odds = new BsonArray();
        foreach (string company in companys)
        {
            bool is_has = false;

            foreach (BsonDocument doc_item in company_odds)
            {
                if (doc_item["company"].ToString() == company)
                {
                    is_has = true;
                }
            }
            if (is_has == false)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("company", company);
                string profit_win1 = "";
                string profit_draw1 = "";
                string profit_lose1 = "";
                string profit_win2 = "";
                string profit_draw2 = "";
                string profit_lose2 = "";
                foreach (DataRow row in dt1.Rows)
                {
                    if (row["company"].ToString() == company)
                    {
                        profit_win1 = row["profit_win"].ToString();
                        profit_draw1 = row["profit_draw"].ToString();
                        profit_lose1 = row["profit_lose"].ToString();
                    }
                }
                foreach (DataRow row in dt2.Rows)
                {
                    if (row["company"].ToString() == company)
                    {
                        profit_win2 = row["profit_win"].ToString();
                        profit_draw2 = row["profit_draw"].ToString();
                        profit_lose2 = row["profit_lose"].ToString();
                    }
                }
                doc_item.Add("profit_win1", profit_win1);
                doc_item.Add("profit_draw1", profit_draw1);
                doc_item.Add("profit_lose1", profit_lose1);
                doc_item.Add("profit_win2", profit_win2);
                doc_item.Add("profit_draw2", profit_draw2);
                doc_item.Add("profit_lose2", profit_lose2);

                company_odds.Add(doc_item.AsBsonDocument);
            }
        }
        doc.Add("company_odds", company_odds);



        doc.Add("order_nos", doc_max["order_nos"].AsBsonArray);
        doc.Add("bids", doc_max["bids"].AsBsonArray);
        doc.Add("profits", doc_max["profits"].AsBsonArray);
        return doc;
    }
    public static BsonDocument get_max_from_three_match(string start_time1, string host1, string client1, string start_time2, string host2, string client2, string start_time3, string host3, string client3, int max_count, ArrayList list_companys)
    {


        string sql = "select * from europe_new where start_time='{0}' and host='{1}' and client='{2}'";
        sql = string.Format(sql, start_time1, host1, client1);
        DataTable dt1 = SQLServerHelper.get_table(sql);

        sql = "select * from europe_new where start_time='{0}' and host='{1}' and client='{2}'";
        sql = string.Format(sql, start_time2, host2, client2);
        DataTable dt2 = SQLServerHelper.get_table(sql);


        sql = "select * from europe_new where start_time='{0}' and host='{1}' and client='{2}'";
        sql = string.Format(sql, start_time3, host3, client3);
        DataTable dt3 = SQLServerHelper.get_table(sql);



        double[] max = new double[27];

        string[] companys = new string[27];
        for (int i = 0; i < 27; i++)
        {
            max[i] = -999999;
            companys[i] = "";
        }



        for (int i = 0; i < dt1.Rows.Count; i++)
        {

            bool is_in_companys = false;
            foreach (string company in list_companys)
            {
                if (company == dt1.Rows[i]["company"].ToString()) is_in_companys = true;
            }
            if (is_in_companys == false) continue;


            int row_no2 = -1;
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                if (dt1.Rows[i]["company"].ToString() == dt2.Rows[j]["company"].ToString())
                {
                    row_no2 = j;
                    break;
                }
            }

            int row_no3 = -1;
            for (int j = 0; j < dt3.Rows.Count; j++)
            {
                if (dt1.Rows[i]["company"].ToString() == dt3.Rows[j]["company"].ToString())
                {
                    row_no3 = j;
                    break;
                }
            }


            if (row_no2 != -1 && row_no3 != -1)
            {

                double[,] single_profit = new double[3, 3]{
                     {Convert.ToDouble(dt1.Rows[i]["profit_win"].ToString()),Convert.ToDouble(dt1.Rows[i]["profit_draw"].ToString()),Convert.ToDouble(dt1.Rows[i]["profit_lose"].ToString())},
                     {Convert.ToDouble(dt2.Rows[row_no2]["profit_win"].ToString()),Convert.ToDouble(dt2.Rows[row_no2]["profit_draw"].ToString()),Convert.ToDouble(dt2.Rows[row_no2]["profit_lose"].ToString())},
                     {Convert.ToDouble(dt3.Rows[row_no3]["profit_win"].ToString()),Convert.ToDouble(dt3.Rows[row_no3]["profit_draw"].ToString()),Convert.ToDouble(dt3.Rows[row_no3]["profit_lose"].ToString())}
                };

                double[] profits = new double[27];

                int index = 0;
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            profits[index] = single_profit[0, j] * single_profit[1, k] * single_profit[2, l];
                            index = index + 1;
                        }
                    }
                }

                for (int j = 0; j < 27; j++)
                {
                    if (profits[j] > max[j])
                    {
                        max[j] = profits[j];
                        companys[j] = dt1.Rows[i]["company"].ToString();
                    }
                }
            }
        }


        BsonDocument doc_max = MatchCompany.get_min(max, max_count);



        BsonDocument doc = new BsonDocument();
        doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
        doc.Add("start_time1", start_time1);
        doc.Add("host1", host1);
        doc.Add("client1", client1);
        doc.Add("start_time2", start_time2);
        doc.Add("host2", host2);
        doc.Add("client2", client2);
        doc.Add("start_time3", start_time3);
        doc.Add("host3", host3);
        doc.Add("client3", client3);
        doc.Add("type", "three-match-max");
        doc.Add("bid_count", doc_max["bid_count"].ToString());
        doc.Add("min_value", doc_max["min_value"].ToString());
        doc.Add("max_value", doc_max["max_value"].ToString());

        BsonArray array_companys = new BsonArray();
        foreach (string company in companys)
        {
            array_companys.Add(company);
        }
        doc.Add("companys", array_companys);

        BsonArray array_orign_profits = new BsonArray();
        foreach (double profit in max)
        {
            array_orign_profits.Add(profit.ToString("f2"));
        }
        doc.Add("orign_profits", array_orign_profits);


        BsonArray company_odds = new BsonArray();
        foreach (string company in companys)
        {
            bool is_has = false;

            foreach (BsonDocument doc_item in company_odds)
            {
                if (doc_item["company"].ToString() == company)
                {
                    is_has = true;
                }
            }
            if (is_has == false)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("company", company);
                string profit_win1 = "";
                string profit_draw1 = "";
                string profit_lose1 = "";
                string profit_win2 = "";
                string profit_draw2 = "";
                string profit_lose2 = "";
                string profit_win3 = "";
                string profit_draw3 = "";
                string profit_lose3 = "";
                foreach (DataRow row in dt1.Rows)
                {
                    if (row["company"].ToString() == company)
                    {
                        profit_win1 = row["profit_win"].ToString();
                        profit_draw1 = row["profit_draw"].ToString();
                        profit_lose1 = row["profit_lose"].ToString();
                    }
                }
                foreach (DataRow row in dt2.Rows)
                {
                    if (row["company"].ToString() == company)
                    {
                        profit_win2 = row["profit_win"].ToString();
                        profit_draw2 = row["profit_draw"].ToString();
                        profit_lose2 = row["profit_lose"].ToString();
                    }
                }
                foreach (DataRow row in dt3.Rows)
                {
                    if (row["company"].ToString() == company)
                    {
                        profit_win3 = row["profit_win"].ToString();
                        profit_draw3 = row["profit_draw"].ToString();
                        profit_lose3 = row["profit_lose"].ToString();
                    }
                }
                doc_item.Add("profit_win1", profit_win1);
                doc_item.Add("profit_draw1", profit_draw1);
                doc_item.Add("profit_lose1", profit_lose1);
                doc_item.Add("profit_win2", profit_win2);
                doc_item.Add("profit_draw2", profit_draw2);
                doc_item.Add("profit_lose2", profit_lose2);
                doc_item.Add("profit_win3", profit_win3);
                doc_item.Add("profit_draw3", profit_draw3);
                doc_item.Add("profit_lose3", profit_lose3);
                company_odds.Add(doc_item.AsBsonDocument);
            }
        }
        doc.Add("company_odds", company_odds);



        doc.Add("order_nos", doc_max["order_nos"].AsBsonArray);
        doc.Add("bids", doc_max["bids"].AsBsonArray);
        doc.Add("profits", doc_max["profits"].AsBsonArray);

        return doc;
    }

    public static BsonDocument get_min(double[] input, int max_count)
    {
        int length = input.Length;


        int[] bids = new int[length];
        int[] order_nos = new int[length];
        double[] profits = new double[length];
        double[] profits_temp = new double[length];



        for (int i = 0; i < length; i++)
        {
            bids[i] = 1;
            order_nos[i] = 0;
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
            order_nos[step1] = step_index;
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
        doc.Add("doc_id", DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
        doc.Add("type", "vary");
        doc.Add("bid_count", bid_total.ToString());
        doc.Add("min_value", min.ToString("f4"));
        doc.Add("max_value", max.ToString("f4"));

        BsonArray array_profits = new BsonArray();
        foreach (double profit in profits)
        {
            array_profits.Add(profit.ToString("f2"));
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
        doc.Add("profits", array_profits);

        if (is_open_mongo) MongoHelper.insert_bson("match", doc);

        return doc;

    }
    public static BsonDocument get_min_by_wave(double[] input, int max_count)
    {
        int length = input.Length;


        int[] bids = new int[length];
        int[] order_nos = new int[length];
        double[] profits = new double[length];
        double[] profits_temp = new double[length];



        for (int i = 0; i < length; i++)
        {
            bids[i] = 1;
            order_nos[i] = 0;
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
            order_nos[step1] = step_index;
        }


        bids[0] = max_count;
        for (int i = 1; i < length; i++)
        {
            bids[i] = (int)Math.Floor(profits[0] * bids[0] / profits[i]);
        }


        //wave compute  
        double global_min = 0;
        double global_max = 0;
        int[] global_bids = new int[bids.Length];
        get_min_by_wave_child(ref global_bids, ref global_min, ref global_max, bids, profits);


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

        BsonArray array_profits = new BsonArray();
        foreach (double profit in profits)
        {
            array_profits.Add(profit.ToString("f2"));
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
        doc.Add("profits", array_profits);

        if (is_open_mongo) MongoHelper.insert_bson("match", doc);

        return doc;

    }
    public static void get_min_by_wave_child(ref int[] global_bids, ref double global_min, ref double global_max, int[] bids, double[] profits)
    {
        int[] num = new int[] { -1, 0, 1 };
        global_min = -999999;
        global_max = 999999;

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
                        if (bids_temp[i] * profits[i] - bid_total < min) min = bids_temp[i] * profits[i] - bid_total;
                        if (bids_temp[i] * profits[i] - bid_total > max) max = bids_temp[i] * profits[i] - bid_total;
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
                                                if (bids_temp[i] * profits[i] - bid_total < min) min = bids_temp[i] * profits[i] - bid_total;
                                                if (bids_temp[i] * profits[i] - bid_total > max) max = bids_temp[i] * profits[i] - bid_total;
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

                result = result + "profits".PL(10);
                foreach (string value in doc["profits"].AsBsonArray)
                {
                    result = result + value.PL(15);
                }
                result = result + Environment.NewLine;


                result = result + "bids".PL(10);
                foreach (string value in doc["bids"].AsBsonArray)
                {
                    result = result + value.PL(15);
                }
                result = result + Environment.NewLine;

                break;

            case "single-match-max":
                result = "type:" + doc["type"].ToString() + "  doc id:" + doc["doc_id"].ToString() + Environment.NewLine +
                 doc["start_time"].ToString() + "    " + doc["host"].ToString().PL(20) + doc["client"].ToString().PL(20) + Environment.NewLine +
                 "bid count:" + doc["bid_count"].ToString() + Environment.NewLine +
                 "return value: " + doc["min_value"].ToString() + "  ~  " + doc["max_value"].ToString() + Environment.NewLine +
                 "return persent: " + (Convert.ToDouble(doc["min_value"].ToString()) / Convert.ToDouble(doc["bid_count"].ToString()) * 100).ToString("f6") + "%" + Environment.NewLine;

                result = result + "companys".PL(10);
                foreach (string value in doc["companys"].AsBsonArray)
                {
                    result = result + value.PL(15);
                }
                result = result + Environment.NewLine;


                result = result + "orign".PL(10);
                foreach (string value in doc["orign_profits"].AsBsonArray)
                {
                    result = result + value.PL(15);
                }
                result = result + Environment.NewLine;

                result = result + "order no".PL(10);
                foreach (string value in doc["order_nos"].AsBsonArray)
                {
                    result = result + value.PL(15);
                }
                result = result + Environment.NewLine;

                result = result + "profits".PL(10);
                foreach (string value in doc["profits"].AsBsonArray)
                {
                    result = result + value.PL(15);
                }
                result = result + Environment.NewLine;


                result = result + "bids".PL(10);
                foreach (string value in doc["bids"].AsBsonArray)
                {
                    result = result + value.PL(15);
                }
                result = result + Environment.NewLine;

                result = result + "company detail info:" + Environment.NewLine;
                foreach (BsonDocument doc_item in doc["company_odds"].AsBsonArray)
                {
                    result = result + doc_item["company"].ToString().PL(20);
                    result = result + doc_item["profit_win"].ToString().PL(10);
                    result = result + doc_item["profit_draw"].ToString().PL(10);
                    result = result + doc_item["profit_lose"].ToString().PL(10) + Environment.NewLine;
                }


                result = result + "profit detail info:" + Environment.NewLine;
                for (int i = 0; i < doc["order_nos"].AsBsonArray.Count; i++)
                {
                    string order_no = doc["order_nos"].AsBsonArray[i].ToString();
                    string company = doc["companys"].AsBsonArray[Convert.ToInt16(order_no)].ToString();
                    string profit = doc["profits"].AsBsonArray[i].ToString();
                    string bid = doc["bids"].AsBsonArray[i].ToString();
                    result = result + company.PL(20) + profit.PL(10)+bid.PL(10);
                    foreach (BsonDocument doc_item in doc["company_odds"].AsBsonArray)
                    {
                        if (doc_item["company"].ToString() == company)
                        {
                            switch (order_no)
                            {
                                case "0":
                                    result = result + "Win".PL(10) + doc_item["profit_win"].ToString().PL(10);
                                    break;
                                case "1":
                                    result = result + "Draw".PL(10) + doc_item["profit_draw"].ToString().PL(10);
                                    break;
                                case "2":
                                    result = result + "Lose".PL(10) + doc_item["profit_lose"].ToString().PL(10);
                                    break; 
                                default:
                                    break;
                            }
                        }
                    }
                    result = result + Environment.NewLine;
                }
                break;
            case "two-match-max":
                result = "type:" + doc["type"].ToString() + "  doc id:" + doc["doc_id"].ToString() + Environment.NewLine +
                 doc["start_time1"].ToString() + "    " + doc["host1"].ToString().PL(20) + doc["client1"].ToString().PL(20) + Environment.NewLine +
                 doc["start_time2"].ToString() + "    " + doc["host2"].ToString().PL(20) + doc["client2"].ToString().PL(20) + Environment.NewLine +
                 "bid count:" + doc["bid_count"].ToString() + Environment.NewLine +
                 "return value: " + doc["min_value"].ToString() + "  ~  " + doc["max_value"].ToString() + Environment.NewLine +
                 "return persent: " + (Convert.ToDouble(doc["min_value"].ToString()) / Convert.ToDouble(doc["bid_count"].ToString()) * 100).ToString("f6") + "%" + Environment.NewLine;

                result = result + "companys".PL(10);
                foreach (string value in doc["companys"].AsBsonArray)
                {
                    result = result + value.PL(12);
                }
                result = result + Environment.NewLine;


                result = result + "orign".PL(10);
                foreach (string value in doc["orign_profits"].AsBsonArray)
                {
                    result = result + value.PL(12);
                }
                result = result + Environment.NewLine;

                result = result + "order no".PL(10);
                foreach (string value in doc["order_nos"].AsBsonArray)
                {
                    result = result + value.PL(12);
                }
                result = result + Environment.NewLine;

                result = result + "profits".PL(10);
                foreach (string value in doc["profits"].AsBsonArray)
                {
                    result = result + value.PL(12);
                }
                result = result + Environment.NewLine;


                result = result + "bids".PL(10);
                foreach (string value in doc["bids"].AsBsonArray)
                {
                    result = result + value.PL(12);
                }
                result = result + Environment.NewLine;

                result = result + "company detail info:" + Environment.NewLine;
                foreach (BsonDocument doc_item in doc["company_odds"].AsBsonArray)
                {
                    result = result + doc_item["company"].ToString().PL(20);
                    result = result + doc_item["profit_win1"].ToString().PL(10);
                    result = result + doc_item["profit_draw1"].ToString().PL(10);
                    result = result + doc_item["profit_lose1"].ToString().PL(10) + Environment.NewLine;
                    result = result + "".PL(20);
                    result = result + doc_item["profit_win2"].ToString().PL(10);
                    result = result + doc_item["profit_draw2"].ToString().PL(10);
                    result = result + doc_item["profit_lose2"].ToString().PL(10) + Environment.NewLine;
                }

                result = result + "profit detail info:" + Environment.NewLine;
                for (int i = 0; i < doc["order_nos"].AsBsonArray.Count; i++)
                {
                    string order_no = doc["order_nos"].AsBsonArray[i].ToString();
                    string company = doc["companys"].AsBsonArray[Convert.ToInt16(order_no)].ToString();
                    string profit = doc["profits"].AsBsonArray[i].ToString();
                    string bid = doc["bids"].AsBsonArray[i].ToString();
                    result = result + company.PL(20) + profit.PL(10) + bid.PL(10);
                    foreach (BsonDocument doc_item in doc["company_odds"].AsBsonArray)
                    {
                        if (doc_item["company"].ToString() == company)
                        {
                            switch (order_no)
                            {
                                case "0":
                                    result = result + "W X W".PL(10) + doc_item["profit_win1"].ToString() + " X " + doc_item["profit_win2"].ToString().PL(10);
                                    break;
                                case "1":
                                    result = result + "W X D".PL(10) + doc_item["profit_win1"].ToString() + " X " + doc_item["profit_draw2"].ToString().PL(10);
                                    break;
                                case "2":
                                    result = result + "W X L".PL(10) + doc_item["profit_win1"].ToString() + " X " + doc_item["profit_lose2"].ToString().PL(10);
                                    break;
                                case "3":
                                    result = result + "D X W".PL(10) + doc_item["profit_draw1"].ToString() + " X " + doc_item["profit_win2"].ToString().PL(10);
                                    break;
                                case "4":
                                    result = result + "D X D".PL(10) + doc_item["profit_draw1"].ToString() + " X " + doc_item["profit_draw2"].ToString().PL(10);
                                    break;
                                case "5":
                                    result = result + "D X L".PL(10) + doc_item["profit_draw1"].ToString() + " X " + doc_item["profit_lose2"].ToString().PL(10);
                                    break;
                                case "6":
                                    result = result + "L X W".PL(10) + doc_item["profit_lose1"].ToString() + " X " + doc_item["profit_win2"].ToString().PL(10);
                                    break;
                                case "7":
                                    result = result + "L X D".PL(10) + doc_item["profit_lose1"].ToString() + " X " + doc_item["profit_draw2"].ToString().PL(10);
                                    break;
                                case "8":
                                    result = result + "L X L".PL(10) + doc_item["profit_lose1"].ToString() + " X " + doc_item["profit_lose2"].ToString().PL(10);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    result = result + Environment.NewLine; 
                }
                break;
            case "three-match-max":
                result = "type:" + doc["type"].ToString() + "  doc id:" + doc["doc_id"].ToString() + Environment.NewLine +
                 doc["start_time1"].ToString() + "    " + doc["host1"].ToString().PL(20) + doc["client1"].ToString().PL(20) + Environment.NewLine +
                 doc["start_time2"].ToString() + "    " + doc["host2"].ToString().PL(20) + doc["client2"].ToString().PL(20)+ Environment.NewLine +
                 doc["start_time3"].ToString() + "    " + doc["host3"].ToString().PL(20) + doc["client3"].ToString().PL(20) + Environment.NewLine +
                 "bid count:" + doc["bid_count"].ToString() + Environment.NewLine +
                 "return value: " + doc["min_value"].ToString() + "  ~  " + doc["max_value"].ToString() + Environment.NewLine +
                 "return persent: " + (Convert.ToDouble(doc["min_value"].ToString()) / Convert.ToDouble(doc["bid_count"].ToString()) * 100).ToString("f6") + "%" + Environment.NewLine;

                result = result + "companys".PL(10);
                foreach (string value in doc["companys"].AsBsonArray)
                {
                    result = result + value.PL(12);
                }
                result = result + Environment.NewLine;


                result = result + "orign".PL(10);
                foreach (string value in doc["orign_profits"].AsBsonArray)
                {
                    result = result + value.PL(12);
                }
                result = result + Environment.NewLine;

                result = result + "order no".PL(10);
                foreach (string value in doc["order_nos"].AsBsonArray)
                {
                    result = result + value.PL(12);
                }
                result = result + Environment.NewLine;

                result = result + "profits".PL(10);
                foreach (string value in doc["profits"].AsBsonArray)
                {
                    result = result + value.PL(12);
                }
                result = result + Environment.NewLine;


                result = result + "bids".PL(10);
                foreach (string value in doc["bids"].AsBsonArray)
                {
                    result = result + value.PL(20);
                }
                result = result + Environment.NewLine;

                result = result + "company detail info:" + Environment.NewLine;
                foreach (BsonDocument doc_item in doc["company_odds"].AsBsonArray)
                {
                    result = result + doc_item["company"].ToString().PL(20);
                    result = result + doc_item["profit_win1"].ToString().PL(10);
                    result = result + doc_item["profit_draw1"].ToString().PL(10);
                    result = result + doc_item["profit_lose1"].ToString().PL(10) + Environment.NewLine;
                    result = result + "".PL(20);
                    result = result + doc_item["profit_win2"].ToString().PL(10);
                    result = result + doc_item["profit_draw2"].ToString().PL(10);
                    result = result + doc_item["profit_lose2"].ToString().PL(10) + Environment.NewLine;
                    result = result + "".PL(20);
                    result = result + doc_item["profit_win3"].ToString().PL(10);
                    result = result + doc_item["profit_draw3"].ToString().PL(10);
                    result = result + doc_item["profit_lose3"].ToString().PL(10) + Environment.NewLine;
                }

                break;
            default:
                break;
        }
        return result;
    }

}

