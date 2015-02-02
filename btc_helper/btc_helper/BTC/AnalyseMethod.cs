using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;


class AnalyseMethod
{ 
    public static DataTable analyse_by_day()
    {
        DataTable dt = new DataTable();

        for (int i = 0; i < 24; i++)
        {
            dt.Columns.Add(i.ToString());
        }
        
        for (int i = 0; i < 7; i++)
        {
            DataRow row_new = dt.NewRow();
            for (int j = 0; j < 24; j++)
            {
               DateTime temp=DateTime.Now.AddDays(-i);
               DateTime start = new DateTime(temp.Year, temp.Month, temp.Day, j, 0, 0);  
               BsonDocument doc = BtcCompute.get_region("btcchina", start, 60 * 60); 
               row_new[j.ToString()] = Math.Round((Convert.ToDouble(doc["close"].ToString()) - Convert.ToDouble(doc["open"].ToString())),2).ToString();
             
            }
            dt.Rows.Add(row_new); 
        }
        return dt;
    }       
    public static DataTable analyse_by_minute()
    {
        DataTable dt = new DataTable();

        for (int i = 0; i < 24*60; i++)
        {
            dt.Columns.Add(i.ToString());
        }

        for (int i = 0; i < 7; i++)
        {
            DataRow row_new = dt.NewRow();
            for (int j = 0; j < 24*60; j++)
            {
                DateTime temp = DateTime.Now.AddDays(-i);
                DateTime start = new DateTime(temp.Year, temp.Month, temp.Day, 0, 0, 0);
                start = start.AddMinutes(i);
                BsonDocument doc = BtcCompute.get_region("btcchina", start, 60 * 60);
                row_new[j.ToString()] =Math.Round( (Convert.ToDouble(doc["close"].ToString()) - Convert.ToDouble(doc["open"].ToString())),2).ToString();
            }
            dt.Rows.Add(row_new);
        }
        return dt;
    }
}

