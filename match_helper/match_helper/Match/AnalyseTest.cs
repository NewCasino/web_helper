﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using System.Threading;


class AnalyseTest
{
    public static List<BsonDocument> get_list_by_persent_desc(List<BsonDocument> list)
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        DataColumn order = new DataColumn("order");
        order.DataType = Type.GetType("System.Int32");
        dt.Columns.Add(order);
        DataColumn persent = new DataColumn("persent");
        persent.DataType = Type.GetType("System.Double");
        dt.Columns.Add(persent);

        for (int i = 0; i < list.Count; i++)
        {
            DataRow row_new = dt.NewRow();
            row_new["order"] = i;
            row_new["persent"] = Convert.ToDouble(list[i]["min_value"].ToString()) / Convert.ToDouble(list[i]["bid_count"].ToString()) * 100;
            dt.Rows.Add(row_new);
        }

        dt.DefaultView.Sort = "persent desc";
        dt = dt.DefaultView.ToTable();

        List<BsonDocument> list_return = new List<BsonDocument>();


        foreach (DataRow row in dt.Rows)
        {
            BsonDocument doc = list[Convert.ToInt32(row["order"].ToString())];
            list_return.Add(doc);
        }

        return list_return;
    }
    public static string  test_2result_persent()
    {
        string sql = "  select * from a_odd where type_id in (0,1,2,3)";
        DataTable dt_temp = SQLServerHelper.get_table(sql);
        foreach (DataRow row in dt_temp.Rows)
        {
            BsonDocument doc = new BsonDocument();
            if (row["type_id"].ToString() == "0")
            {
                doc = Match100Helper.get_odd_doc_from_europe(row["o1"].ToString(), row["o2"].ToString(), row["o3"].ToString());
            }
            else
            {
                doc = Match100Helper.get_odd_doc_from_europe(row["o1"].ToString(), row["o2"].ToString());
            }

           IWindow.write_line(row["event_id"].PR(10) + row["type_id"].PR(10) + doc["persent_return"].PR(10));
        }
        return "Compute OK!!!";
    }
    public static string test_2result_odd()
    { 
        BsonDocument doc = Analyse2Result.get_best(1, 50);

        IWindow.write_break();
        IWindow.write(Analyse2Result.get_info(doc));
        IWindow.write_break();
        return "Compute OK!!!";
    }
    public static string test_all_2result_odd()
    {
        StringBuilder sb = new StringBuilder();
        string sql = "  select * from a_odd where type_id in (2,3,4)";
        DataTable dt_temp = SQLServerHelper.get_table(sql);
       
        List<BsonDocument> list = new List<BsonDocument>();
        foreach (DataRow row in dt_temp.Rows)
        {
            if (row["m5"].ToString() == "Y") continue;
            BsonDocument doc = Analyse2Result.get_best(Convert.ToInt32(row["id"].ToString()), 50);
            list.Add(doc);
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                if (dt_temp.Rows[i]["event_id"].ToString() == row["event_id"].ToString() &&
                    dt_temp.Rows[i]["type_id"].ToString() == row["type_id"].ToString() &&
                    dt_temp.Rows[i]["m1"].ToString() == row["m1"].ToString() &&
                    dt_temp.Rows[i]["m2"].ToString() == row["m2"].ToString() &&
                    dt_temp.Rows[i]["m3"].ToString() == row["m3"].ToString() &&
                    dt_temp.Rows[i]["m4"].ToString() == row["m4"].ToString() &&
                    dt_temp.Rows[i]["m5"].ToString() == row["m5"].ToString() &&
                    dt_temp.Rows[i]["m6"].ToString() == row["m6"].ToString() &&
                    dt_temp.Rows[i]["o1"].ToString() == row["o1"].ToString() &&
                    dt_temp.Rows[i]["o2"].ToString() == row["o2"].ToString() &&
                    dt_temp.Rows[i]["o3"].ToString() == row["o3"].ToString() &&
                    dt_temp.Rows[i]["o4"].ToString() == row["o4"].ToString() &&
                    dt_temp.Rows[i]["o5"].ToString() == row["o5"].ToString() &&
                    dt_temp.Rows[i]["o6"].ToString() == row["o6"].ToString())
                {
                    dt_temp.Rows[0]["m5"] = "Y";
                }
            }
        }
        List<BsonDocument> list_result = get_list_by_persent_desc(list);
        IWindow.write_break();
        int count = 0;
        foreach (BsonDocument doc_show in list_result)
        {
            count = count + 1;
            if (count == 11) break;
            IWindow.write(Analyse2Result.get_info(doc_show));
            IWindow.write_break();
        }
     
        return "Compute OK!!!";
    }
    public static string test()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Start Compute......");
        sb.Append("Finish Compute......");
        IWindow.write_content(sb.ToString());
        return "Compute OK!!!";
    }
}
