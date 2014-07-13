using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;



class MatchCompany
{
    static bool is_open_mongo = false;


    public static BsonDocument get_max_from_single_match(string start_time, string host, string client, int max_count)
    {
        string sql = "select * from europe_new where start_time='{0}' and host='{1}' and client='{2}'";
        sql = string.Format(sql, start_time, host, client);
        DataTable dt = SQLServerHelper.get_table(sql);


        double[] max = new double[3] { -999999, -999999, -99999 };
        string[] companys = new string[3] { "", "", "" };

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            double[] input = new double[3]{Convert.ToDouble(dt.Rows[i]["profit_win"].ToString()),
                                            Convert.ToDouble(dt.Rows[i]["profit_draw"].ToString()),
                                            Convert.ToDouble(dt.Rows[i]["profit_lose"].ToString())};
            if (input[0] > max[0]) { max[0] = input[0]; companys[0] = dt.Rows[i]["company"].ToString(); }
            if (input[1] > max[1]) { max[1] = input[1]; companys[1] = dt.Rows[i]["company"].ToString(); }
            if (input[2] > max[2]) { max[2] = input[2]; companys[2] = dt.Rows[i]["company"].ToString(); }
        }


        BsonDocument doc_max = MatchCompany.get_min(max, 50);



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

        doc.Add("order_nos", doc_max["order_nos"].AsBsonArray);
        doc.Add("bids", doc_max["bids"].AsBsonArray);
        doc.Add("profits", doc_max["profits"].AsBsonArray);

        return doc;
    } 
    public static BsonDocument get_max_from_two_match(string start_time1, string host1, string client1, string start_time2, string host2, string client2, int max_count)
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


        BsonDocument doc_max = MatchCompany.get_min(max, 50);



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

                result = result + "profits".PadRight(10, ' ');
                foreach (string value in doc["profits"].AsBsonArray)
                {
                    result = result + value.PadRight(15, ' ');
                }
                result = result + Environment.NewLine;


                result = result + "bids".PadRight(10, ' ');
                foreach (string value in doc["bids"].AsBsonArray)
                {
                    result = result + value.PadRight(15, ' ');
                }
                result = result + Environment.NewLine;

                break;

            case "single-match-max":
                result = "type:" + doc["type"].ToString() + "  doc id:" + doc["doc_id"].ToString() + Environment.NewLine + 
                 doc["start_time"].ToString()+"    "+doc["host"].ToString().PadRight(20,' ')+doc["client"].ToString().PadRight(20,' ')+Environment.NewLine+
                 "bid count:" + doc["bid_count"].ToString() + Environment.NewLine +
                 "return value: " + doc["min_value"].ToString() + "  ~  " + doc["max_value"].ToString() + Environment.NewLine +
                 "return persent: " + (Convert.ToDouble(doc["min_value"].ToString()) / Convert.ToDouble(doc["bid_count"].ToString()) * 100).ToString("f6") + "%" + Environment.NewLine;

                result = result + "companys".PadRight(10, ' ');
                foreach (string value in doc["companys"].AsBsonArray)
                {
                    result = result + value.PadRight(15, ' ');
                }
                result = result + Environment.NewLine;


                result = result + "orign".PadRight(10, ' ');
                foreach (string value in doc["orign_profits"].AsBsonArray)
                {
                    result = result + value.PadRight(15, ' ');
                }
                result = result + Environment.NewLine;

                result = result + "order no".PadRight(10, ' ');
                foreach (string value in doc["order_nos"].AsBsonArray)
                {
                    result = result + value.PadRight(15, ' ');
                }
                result = result + Environment.NewLine;

                result = result + "profits".PadRight(10, ' ');
                foreach (string value in doc["profits"].AsBsonArray)
                {
                    result = result + value.PadRight(15, ' ');
                }
                result = result + Environment.NewLine;


                result = result + "bids".PadRight(10, ' ');
                foreach (string value in doc["bids"].AsBsonArray)
                {
                    result = result + value.PadRight(15, ' ');
                }
                result = result + Environment.NewLine;
                break;
            case "two-match-max":
                result = "type:" + doc["type"].ToString() + "  doc id:" + doc["doc_id"].ToString() + Environment.NewLine +
                 doc["start_time1"].ToString() + "    " + doc["host1"].ToString().PadRight(20, ' ') + doc["client1"].ToString().PadRight(20, ' ') + Environment.NewLine +
                 doc["start_time2"].ToString() + "    " + doc["host2"].ToString().PadRight(20, ' ') + doc["client2"].ToString().PadRight(20, ' ') + Environment.NewLine +
                 "bid count:" + doc["bid_count"].ToString() + Environment.NewLine +
                 "return value: " + doc["min_value"].ToString() + "  ~  " + doc["max_value"].ToString() + Environment.NewLine +
                 "return persent: " + (Convert.ToDouble(doc["min_value"].ToString()) / Convert.ToDouble(doc["bid_count"].ToString()) * 100).ToString("f6") + "%" + Environment.NewLine;

                result = result + "companys".PadRight(10, ' ');
                foreach (string value in doc["companys"].AsBsonArray)
                {
                    result = result + value.PadRight(12, ' ');
                }
                result = result + Environment.NewLine;


                result = result + "orign".PadRight(10, ' ');
                foreach (string value in doc["orign_profits"].AsBsonArray)
                {
                    result = result + value.PadRight(12, ' ');
                }
                result = result + Environment.NewLine;

                result = result + "order no".PadRight(10, ' ');
                foreach (string value in doc["order_nos"].AsBsonArray)
                {
                    result = result + value.PadRight(12, ' ');
                }
                result = result + Environment.NewLine;

                result = result + "profits".PadRight(10, ' ');
                foreach (string value in doc["profits"].AsBsonArray)
                {
                    result = result + value.PadRight(12, ' ');
                }
                result = result + Environment.NewLine;


                result = result + "bids".PadRight(10, ' ');
                foreach (string value in doc["bids"].AsBsonArray)
                {
                    result = result + value.PadRight(12, ' ');
                }
                result = result + Environment.NewLine;

                break;
            default:
                break;
        }
        return result;
    }

}

