﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Newtonsoft.Json;
using MongoDB.Bson;
public partial class Response : System.Web.UI.Page
{
    string[] websites = new string[] { "btce", "btcchina" };
    protected void Page_Load(object sender, EventArgs e)
    {

        string method = Request.QueryString["method"].ToString();
        string result = "";

        switch (method)
        {
            case "get_stock":
                result = get_stock();
                break;
            case "get_new_data":
                result = get_new_data();
                break;
            case "get_series":
                result = get_series();
                break;
            case "get_depth_sell":
                result = get_depth_sell();
                break;
            case "get_depth_buy":
                result = get_depth_buy();
                break;
            case "get_depth_all":
                result = get_depth_all();
                break;
            case "get_stock_test":
                result = get_stock_test();
                break;
            case "get_new_data_test":
                result = get_new_data_test();
                break;
            case "get_series_test":
                result = get_series_test();
                break;
            default:
                break;
        }
        Response.Write(result);
    }

    public string get_stock()
    {
        string sql = "";
        string[] websites = new string[] { "btce", "okcoin", "btcchina" };
        double rate = CurrencyHelper.get_rate("usd", "cny");

        BsonArray datas = new BsonArray();
        foreach (string website in websites)
        {
            BsonDocument doc = new BsonDocument();
            doc.Add("name", website);

            BsonArray data = new BsonArray();
            sql = "select * from ticker_log where  currency like  '%btc%' and  website='{0}'";
            sql = string.Format(sql, website);
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                DateTime date = Convert.ToDateTime(row["timespan"].ToString().Substring(0, 19));
                TimeSpan span = date - new System.DateTime(1970, 1, 1);
                BsonArray data_item = new BsonArray();
                data_item.Add(Math.Round(span.TotalSeconds).ToString() + "000");

                if (row["currency"].ToString().Contains("usd"))
                {
                    data_item.Add(Math.Round(Convert.ToDouble(row["last"].ToString()) * rate, 2));
                }
                else
                {
                    data_item.Add(Convert.ToDouble(row["last"].ToString()));
                }
                data.Add(data_item);
            }

            doc.Add("data", data); 
            datas.Add(doc);
        }
        return datas.ToJson();
    }
    public string get_new_data()
    {
        string sql = "";
        double rate = CurrencyHelper.get_rate("usd", "cny");
        BsonArray data = new BsonArray();

        string max_id = get_max_id();
        sql = "select * from ticker_log where id >{0} and timespan>'{1}' and currency like '%btc%'";
        sql = string.Format(sql, max_id, DateTime.Now.AddSeconds(-10).ToString("yyyy-MM-dd HH:mm:ss"));
        DataTable dt = SQLServerHelper.get_table(sql);
        if (dt.Rows.Count > 0)
        {
            Application["stock_dynamic_max_id"] = dt.Rows[dt.Rows.Count - 1]["id"].ToString();
        }
        //0 btctrade,1,btcchina
        foreach (DataRow row in dt.Rows)
        {
            BsonArray data_item = new BsonArray();

            bool is_has = false;
            for (int i = 0; i < websites.Length; i++)
            {
                if (websites[i].ToString() == row["website"].ToString())
                {
                    data_item.Add(i);
                    is_has = true;
                }
            }
            if (is_has == false) continue;

            DateTime date = Convert.ToDateTime(row["timespan"].ToString().Substring(0, 19));
            TimeSpan span = date - new System.DateTime(1970, 1, 1);
            data_item.Add(Math.Round(span.TotalSeconds).ToString() + "000");


            if (row["currency"].ToString().Contains("usd"))
            {
                data_item.Add(Math.Round(Convert.ToDouble(row["last"].ToString()) * rate, 2));
            }
            else
            {
                data_item.Add(Convert.ToDouble(row["last"].ToString()));
            }
            data.Add(data_item);
        }



        return data.ToString();
    }
    public string get_series()
    {
        string sql = "";
        double rate = CurrencyHelper.get_rate("usd", "cny");
        BsonArray datas = new BsonArray();
        foreach (string website in websites)
        {

            BsonDocument doc = new BsonDocument();
            doc.Add("name", website);


            BsonArray data = new BsonArray();
            sql = " select * " +
                 " from (select top 1002 * from ticker_log where  currency like  '%btc%' and  website='{0}'  order by id desc) a" +
                 " order by id ";
            sql = string.Format(sql, website);
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                DateTime date = Convert.ToDateTime(row["timespan"].ToString().Substring(0, 19));
                TimeSpan span = date - new System.DateTime(1970, 1, 1);
                BsonArray data_item = new BsonArray();
                data_item.Add(Math.Round(span.TotalSeconds).ToString() + "000");

                if (row["currency"].ToString().Contains("usd"))
                {
                    data_item.Add(Math.Round(Convert.ToDouble(row["last"].ToString()) * rate, 2));
                }
                else
                {
                    data_item.Add(Convert.ToDouble(row["last"].ToString()));
                }
                data.Add(data_item);
            }
            doc.Add("data", data); 
            datas.Add(doc);
        }
        return datas.ToString();
    }
    public string get_max_id()
    {
        string sql = "";
        string max_id = "";
        if (Application["stock_dynamic_max_id"] == null)
        {
            max_id = SQLServerHelper.get_table("select max(id) from ticker_log").Rows[0][0].ToString();
            Application["stock_dynamic_max_id"] = max_id;
        }
        else
        {
            max_id = Application["stock_dynamic_max_id"].ToString();
        }
        return max_id;
    }
    public string get_depth_sell()
    {
        string sql = "select max(id) from depth_log where website='btcchina' and type='sell' ";
        string max_id = SQLServerHelper.get_table(sql).Rows[0][0].ToString();

        sql = "select * from depth_log where id={0}";
        sql = string.Format(sql, max_id);
        string result = SQLServerHelper.get_table(sql).Rows[0]["text"].ToString();
        BsonArray array_data = MongoHelper.get_array_from_str(result);

        BsonArray list = new BsonArray();
        BsonDocument doc = new BsonDocument();
        doc.Add("name", "Depth");
        doc.Add("color", "#434348");
        BsonArray list_item = new BsonArray();
        for (int i = array_data.Count-1; i>=0; i--)
        {
            BsonDocument doc_item = new BsonDocument();
            doc_item.Add("x", array_data[i][0]);
            doc_item.Add("y", array_data[i][1]);
            list_item.Add(doc_item);
        }
        doc.Add("data", list_item);
        list.Add(doc);

        return list.ToString();
    }
    public string get_depth_buy()
    {
        string sql = "select max(id) from depth_log where website='btcchina' and type='buy' ";
        string max_id = SQLServerHelper.get_table(sql).Rows[0][0].ToString();

        sql = "select * from depth_log where id={0}";
        sql = string.Format(sql, max_id);
        string result = SQLServerHelper.get_table(sql).Rows[0]["text"].ToString();
        BsonArray array_data = MongoHelper.get_array_from_str(result);

        BsonArray list = new BsonArray(); 
        BsonDocument doc = new BsonDocument();
        doc.Add("name", "Depth");

        BsonArray list_item=new BsonArray();
        for (int i = array_data.Count-1; i >=0;i-- )
        {
            BsonDocument doc_item = new BsonDocument();
            doc_item.Add("x", array_data[i][0]);
            doc_item.Add("y", array_data[i][1]);
            list_item.Add(doc_item);
        }
        doc.Add("data", list_item);
        list.Add(doc);

        return list.ToString();
    }
    public string get_depth_all()
    {
        string sql = "select max(id) from depth_log where website='btcchina' and type='sell' ";
        string max_id = SQLServerHelper.get_table(sql).Rows[0][0].ToString();

        sql = "select * from depth_log where id={0}";
        sql = string.Format(sql, max_id);
        string result_sell = SQLServerHelper.get_table(sql).Rows[0]["text"].ToString();


        sql = "select max(id) from depth_log where website='btcchina' and type='buy' ";
        max_id = SQLServerHelper.get_table(sql).Rows[0][0].ToString();

        sql = "select * from depth_log where id={0}";
        sql = string.Format(sql, max_id);
        string result_buy = SQLServerHelper.get_table(sql).Rows[0]["text"].ToString();

        BsonArray array_sell = MongoHelper.get_array_from_str(result_sell);
        BsonArray array_buy = MongoHelper.get_array_from_str(result_buy);


        BsonArray list = new BsonArray();

        BsonDocument doc_buy = new BsonDocument();
        doc_buy.Add("name", "SELL"); 
        BsonArray list_buy = new BsonArray(); 
        for (int i = array_buy.Count-1; i >=0; i--)
        {
            BsonDocument doc_item = new BsonDocument();
            doc_item.Add("x", array_buy[i][0]);
            doc_item.Add("y", array_buy[i][1]); 
            list_buy.Add(doc_item);
        }
        doc_buy.Add("data", list_buy);

        BsonDocument doc_sell = new BsonDocument();
        doc_sell.Add("name", "SELL"); 
        BsonArray list_sell = new BsonArray(); 
        for (int i =array_sell.Count-1; i>=0; i--)
        {
            BsonDocument doc_item = new BsonDocument();
            doc_item.Add("x", array_sell[i][0]);
            doc_item.Add("y", array_sell[i][1]);
            list_sell.Add(doc_item);
        }
        doc_sell.Add("data", list_sell);
        
         
        list.Add(doc_buy);
        list.Add(doc_sell);

        return list.ToString();
    }

    public string get_stock_test()
    {
        TimeSpan span = DateTime.Now - new System.DateTime(1970, 1, 1);

        Random randm = new Random();
        BsonDocument doc = new BsonDocument();
        BsonArray datas = new BsonArray();

        BsonArray data1 = new BsonArray();
        for (int i = 1; i < 200; i++)
        {
            BsonArray data_item = new BsonArray();
            data_item.Add((Math.Round(span.TotalSeconds) + i).ToString() + "000");
            data_item.Add(1000+randm.Next(100));
            data1.Add(data_item);
        }

        BsonArray data2 = new BsonArray();
        for (int i = 1; i < 200; i++)
        {
            BsonArray data_item = new BsonArray();
            data_item.Add((Math.Round(span.TotalSeconds) + i).ToString() + "000");
            data_item.Add(900+randm.Next(10));
            data2.Add(data_item);
        }

        BsonDocument doc_data1 = new BsonDocument();
        doc_data1.Add("name", "Trade");
        doc_data1.Add("data", data1);

        BsonDocument doc_data2 = new BsonDocument();
        doc_data2.Add("name", "China");
        doc_data2.Add("data", data2);

        datas.Add(doc_data1);
        datas.Add(doc_data2);
        return datas.ToJson();
    }
    public string get_series_test()
    {
        TimeSpan span = DateTime.Now.AddHours(12) - new System.DateTime(1970, 1, 1);

        Random randm = new Random();
        BsonDocument doc = new BsonDocument();
        BsonArray datas = new BsonArray();

        BsonArray data1 = new BsonArray();
        for (int i = 1; i < 1002; i++)
        {
            BsonArray data_item = new BsonArray();
            data_item.Add((Math.Round(span.TotalSeconds) + i).ToString() + "000");
            data_item.Add(1000 + randm.Next(100));
            data1.Add(data_item);
        }

        BsonArray data2 = new BsonArray();
        for (int i = 1; i < 1002; i++)
        {
            BsonArray data_item = new BsonArray();
            data_item.Add((Math.Round(span.TotalSeconds) + i).ToString() + "000");
            data_item.Add(900 + randm.Next(10));
            data2.Add(data_item);
        }

        BsonDocument doc_data1 = new BsonDocument();
        doc_data1.Add("name", "Trade");
        doc_data1.Add("data", data1);

        BsonDocument doc_data2 = new BsonDocument();
        doc_data2.Add("name", "China");
        doc_data2.Add("data", data2);

        datas.Add(doc_data1);
        datas.Add(doc_data2);
        return datas.ToJson();
    }
    public string get_new_data_test()
    {
        Random randm = new Random();

        BsonArray data = new BsonArray();

        TimeSpan span = DateTime.Now - new System.DateTime(1970, 1, 1);


        BsonArray data_item = new BsonArray();
        data_item.Add(0);
        data_item.Add((Math.Round(span.TotalSeconds)).ToString() + "000");
        data_item.Add(1000+randm.Next(400));


        BsonArray data_item1 = new BsonArray();
        data_item1.Add(1);
        data_item1.Add((Math.Round(span.TotalSeconds)).ToString() + "000");
        data_item1.Add(900+randm.Next(100));

        data.Add(data_item);
        data.Add(data_item1);
        return data.ToString();


    }
}
