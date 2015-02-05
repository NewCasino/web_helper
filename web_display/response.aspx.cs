using System;
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
//using Newtonsoft.Json;
using MongoDB.Bson;
public partial class Response : System.Web.UI.Page
{
    string[] websites = new string[] { "btcchina", "okcoin_cn" };
    protected void Page_Load(object sender, EventArgs e)
    {

        string method = Request.QueryString["method"].ToString();
        string time = "";
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
            case "get_stock_candle":
                result = get_stock_candle();
                break;
            case "get_analyse_depth_depth":
                time = Request.QueryString["time"].ToString();
                result = get_analyse_depth_depth(time);
                break;
            case "get_analyse_depth_stock":
                time = Request.QueryString["time"].ToString();
                result = get_analyse_depth_stock(time);
                break;
            case "get_depth_with_time":
                time = Request.QueryString["time"].ToString();
                result = get_depth_with_time(time);
                break; 
            default:
                break;
        }
        Response.Write(result);
    }

    public string get_stock()
    {
        string sql = "";
        string[] websites = new string[] { "btcchina", "huobi", "okcoin_cn" };
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
                BsonArray data_item = new BsonArray();
                data_item.Add(row["timespan"].ToString());

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
        sql = string.Format(sql, max_id, UnixTime.get_unix_time_from_local_long(DateTime.Now.AddSeconds(-10)));
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
          
            data_item.Add(row["timespan"].ToString());


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
                BsonArray data_item = new BsonArray();
                data_item.Add(row["timespan"].ToString());

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


        sql = "select * from ticker where website='btcchina'";
        DataTable dt_ticker = SQLServerHelper.get_table(sql);
        double min = Convert.ToDouble(dt_ticker.Rows[0]["buy"].ToString()) - 20;
        double max = Convert.ToDouble(dt_ticker.Rows[0]["sell"].ToString()) + 20;


        BsonArray list = new BsonArray();
        BsonDocument doc = new BsonDocument();
        doc.Add("name", "Depth");
        doc.Add("color", "#434348");
        BsonArray list_item = new BsonArray();
        for (int i = array_data.Count-1; i>=0; i--)
        {
            if (Convert.ToDouble(array_data[i][0]) > min && Convert.ToDouble(array_data[i][0]) < max)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("x", array_data[i][0]);
                doc_item.Add("y", array_data[i][1]);
                list_item.Add(doc_item);
            }
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

        sql = "select * from ticker where website='btcchina'";
        DataTable dt_ticker = SQLServerHelper.get_table(sql);
        double min = Convert.ToDouble(dt_ticker.Rows[0]["buy"].ToString()) - 20;
        double max = Convert.ToDouble(dt_ticker.Rows[0]["sell"].ToString()) + 20;

        BsonArray list = new BsonArray(); 
        BsonDocument doc = new BsonDocument();
        doc.Add("name", "Depth");

        BsonArray list_item=new BsonArray();
        for (int i = array_data.Count-1; i >=0;i-- )
        {
            if (Convert.ToDouble(array_data[i][0]) > min && Convert.ToDouble(array_data[i][0]) < max)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("x", array_data[i][0]);
                doc_item.Add("y", array_data[i][1]);
                list_item.Add(doc_item);
            }
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

        sql = "select * from ticker where website='btcchina'";
        DataTable dt_ticker = SQLServerHelper.get_table(sql);
        double min = Convert.ToDouble(dt_ticker.Rows[0]["buy"].ToString())-20;
        double max = Convert.ToDouble(dt_ticker.Rows[0]["sell"].ToString()) + 20;

        BsonArray array_sell = MongoHelper.get_array_from_str(result_sell);
        BsonArray array_buy = MongoHelper.get_array_from_str(result_buy);


        BsonArray list = new BsonArray();

        BsonDocument doc_buy = new BsonDocument();
        doc_buy.Add("name", "SELL"); 
        BsonArray list_buy = new BsonArray(); 
        for (int i = array_buy.Count-1; i >=0; i--)
        {
           
            if (Convert.ToDouble(array_buy[i][0].ToString()) > min && Convert.ToDouble(array_buy[i][0].ToString()) < max)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("x", array_buy[i][0]);
                doc_item.Add("y", array_buy[i][1]);
                list_buy.Add(doc_item);
            }
        }
        doc_buy.Add("data", list_buy);

        BsonDocument doc_sell = new BsonDocument();
        doc_sell.Add("name", "SELL"); 
        BsonArray list_sell = new BsonArray(); 
        for (int i =array_sell.Count-1; i>=0; i--)
        {
            if (Convert.ToDouble(array_sell[i][0].ToString()) > min && Convert.ToDouble(array_sell[i][0].ToString()) < max)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("x", array_sell[i][0]);
                doc_item.Add("y", array_sell[i][1]);
                list_sell.Add(doc_item);
            }
        }
        doc_sell.Add("data", list_sell);
        
         
        list.Add(doc_buy);
        list.Add(doc_sell);

        return list.ToString();
    }
    public string get_stock_candle()
    {
        DateTime dt_start = DateTime.Now.AddSeconds(-150000);
        DateTime dt_end = dt_start.AddDays(0.5);

        BsonArray list_result = new BsonArray();
        BsonArray list = BtcCompute.get_candle("", dt_start, dt_end, 60);
 
        for (int i = 0; i < list.Count; i++)
        {
            BsonDocument doc = list[i].AsBsonDocument;
            BsonArray item = new BsonArray();
            item.Add(Convert.ToUInt64(doc["start_time"].ToString()));
            item.Add(Convert.ToDouble(doc["open"].ToString()));
            item.Add(Convert.ToDouble(doc["hight"].ToString()));
            item.Add(Convert.ToDouble(doc["low"].ToString()));
            item.Add(Convert.ToDouble(doc["close"].ToString()));
            list_result.Add(item);
        } 

        return list_result.ToString();
    }

    public string get_analyse_depth_depth(string time)
    {
        string sql = "select max(id) from depth_log where website='btcchina' and type='sell' and time>={0} ";
        sql = string.Format(sql, time);
        string max_id = SQLServerHelper.get_table(sql).Rows[0][0].ToString();

        sql = "select * from depth_log where id={0}";
        sql = string.Format(sql, max_id);
        string result_sell = SQLServerHelper.get_table(sql).Rows[0]["text"].ToString(); 

        sql = "select max(id) from depth_log where website='btcchina' and type='buy'  and time >={0}";
        sql = string.Format(sql, time);
        max_id = SQLServerHelper.get_table(sql).Rows[0][0].ToString();
   
        sql = "select * from depth_log where id={0}";
        sql = string.Format(sql, max_id);
        string result_buy = SQLServerHelper.get_table(sql).Rows[0]["text"].ToString(); 

        BsonArray array_sell = MongoHelper.get_array_from_str(result_sell);
        BsonArray array_buy = MongoHelper.get_array_from_str(result_buy); 

        double middle = Convert.ToDouble(array_buy[0][0].ToString());
        double min = middle - 20;
        double max = middle + 20; 

        BsonArray list = new BsonArray();

        BsonDocument doc_buy = new BsonDocument();
        doc_buy.Add("name", "SELL");
        BsonArray list_buy = new BsonArray();
        for (int i = array_buy.Count - 1; i >= 0; i--)
        { 
            if (Convert.ToDouble(array_buy[i][0].ToString()) > min && Convert.ToDouble(array_buy[i][0].ToString()) < max)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("x", array_buy[i][0]);
                doc_item.Add("y", array_buy[i][1]);
                list_buy.Add(doc_item);
            }
        }
        doc_buy.Add("data", list_buy);

        BsonDocument doc_sell = new BsonDocument();
        doc_sell.Add("name", "SELL");
        BsonArray list_sell = new BsonArray();
        for (int i = array_sell.Count - 1; i >= 0; i--)
        {
            if (Convert.ToDouble(array_sell[i][0].ToString()) > min && Convert.ToDouble(array_sell[i][0].ToString()) < max)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("x", array_sell[i][0]);
                doc_item.Add("y", array_sell[i][1]);
                list_sell.Add(doc_item);
            }
        }
        doc_sell.Add("data", list_sell); 
        list.Add(doc_buy);
        list.Add(doc_sell);

        return list.ToString();
    }
    public string get_analyse_depth_stock(string time)
    {
        string sql = "";
        string[] websites = new string[] { "btcchina"};
        double rate = CurrencyHelper.get_rate("usd", "cny");

        BsonArray datas = new BsonArray();
        foreach (string website in websites)
        {
            BsonDocument doc = new BsonDocument();
            doc.Add("name", website); 
            BsonArray data = new BsonArray();
            if (website == "btcchina")
            {
                sql = "select top 1002 * from trade_btcchina  where time >{0} order by id";
                sql = string.Format(sql, time);
                DataTable dt = SQLServerHelper.get_table(sql);
                foreach (DataRow row in dt.Rows)
                {
                    BsonArray data_item = new BsonArray();
                    data_item.Add(row["time"].ToString());

                    if (row["currency"].ToString().Contains("usd"))
                    {
                        data_item.Add(Math.Round(Convert.ToDouble(row["price"].ToString()) * rate, 2));
                    }
                    else
                    {
                        data_item.Add(Convert.ToDouble(row["price"].ToString()));
                    }
                    data.Add(data_item);
                }
            }
            doc.Add("data", data);  
            datas.Add(doc); 
        }
        return datas.ToJson();
    }


    public string get_depth_with_time(string time)
    {
        BsonDocument doc_result = new BsonDocument();


        string sql = "select max(id) from depth_log where website='btcchina' and type='sell' and time>={0} ";
        sql = string.Format(sql, time);
        string max_id = SQLServerHelper.get_table(sql).Rows[0][0].ToString();

        if (string.IsNullOrEmpty(max_id))
        {
            doc_result.Add("data", "");
            doc_result.Add("time", "no");
            return doc_result.ToString();
        }

        sql = "select * from depth_log where id={0}";
        sql = string.Format(sql, max_id);
        DataTable dt_sell = SQLServerHelper.get_table(sql);
        string result_sell = dt_sell.Rows[0]["text"].ToString();
        string depth_time = dt_sell.Rows[0]["time"].ToString();

        sql = "select max(id) from depth_log where website='btcchina' and type='buy'  and time >={0}";
        sql = string.Format(sql, time);
        max_id = SQLServerHelper.get_table(sql).Rows[0][0].ToString();

      
        sql = "select * from depth_log where id={0}";
        sql = string.Format(sql, max_id);
        string result_buy = SQLServerHelper.get_table(sql).Rows[0]["text"].ToString();

        BsonArray array_sell = MongoHelper.get_array_from_str(result_sell);
        BsonArray array_buy = MongoHelper.get_array_from_str(result_buy);

        double middle = Convert.ToDouble(array_buy[0][0].ToString());
        double min = middle - 20;
        double max = middle + 20;

        BsonArray list = new BsonArray();

        BsonDocument doc_buy = new BsonDocument();
        doc_buy.Add("name", "SELL");
        BsonArray list_buy = new BsonArray();
        for (int i = array_buy.Count - 1; i >= 0; i--)
        {
            if (Convert.ToDouble(array_buy[i][0].ToString()) > min && Convert.ToDouble(array_buy[i][0].ToString()) < max)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("x", array_buy[i][0]);
                doc_item.Add("y", array_buy[i][1]);
                list_buy.Add(doc_item);
            }
        }
        doc_buy.Add("data", list_buy);

        BsonDocument doc_sell = new BsonDocument();
        doc_sell.Add("name", "SELL");
        BsonArray list_sell = new BsonArray();
        for (int i = array_sell.Count - 1; i >= 0; i--)
        {
            if (Convert.ToDouble(array_sell[i][0].ToString()) > min && Convert.ToDouble(array_sell[i][0].ToString()) < max)
            {
                BsonDocument doc_item = new BsonDocument();
                doc_item.Add("x", array_sell[i][0]);
                doc_item.Add("y", array_sell[i][1]);
                list_sell.Add(doc_item);
            }
        }
        doc_sell.Add("data", list_sell);
        list.Add(doc_buy);
        list.Add(doc_sell);

       
        doc_result.Add("data", list);
        doc_result.Add("time", depth_time);
        return doc_result.ToString();
    }
}
