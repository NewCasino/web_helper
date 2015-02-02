using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
 
    class BtcCompute
    { 
        public  static BsonDocument get_region(string website, DateTime start_time, int seconds)
        {
            UInt64 u_start_time = UnixTime.get_unix_time_from_local_long(start_time);
            UInt64 u_end_time = UnixTime.get_unix_time_from_local_long(start_time.AddSeconds(seconds));

            
            
            int count;

            double open = 0;
            double hight = 0;
            double low = 0;
            double close = 0;

            ulong open_tid = 0;
            ulong hight_tid = 0;
            ulong low_tid = 0;
            ulong close_tid = 0;

            string sql = "";  
            sql = "select * from trade_btcchina where time>={0} and time<={1}";
            sql = string.Format(sql, u_start_time.ToString(), u_end_time.ToString());
            DataTable dt_temp = SQLServerHelper.get_table(sql); 
            count = dt_temp.Rows.Count;

            if (count > 0)
            {
                open = Convert.ToDouble(dt_temp.Rows[0]["price"].ToString());
                open_tid = Convert.ToUInt64(dt_temp.Rows[0]["tid"].ToString());
                close = Convert.ToDouble(dt_temp.Rows[count - 1]["price"].ToString());
                close_tid = Convert.ToUInt64(dt_temp.Rows[count - 1]["tid"].ToString());

                hight = double.MinValue;
                low = double.MaxValue;
                for (int i = 0; i < count; i++)
                {
                    double price = Convert.ToDouble(dt_temp.Rows[i]["price"].ToString());
                    if (price > hight)
                    {
                        hight = price;
                        hight_tid = Convert.ToUInt64(dt_temp.Rows[i]["tid"].ToString()); 
                    }
                    if (price < low)
                    {
                        low = price;
                        low_tid = Convert.ToUInt64(dt_temp.Rows[i]["tid"].ToString());  
                    }
                }
            }
            

           
            BsonDocument doc = new BsonDocument();
            doc.Add("seconds", seconds);
            doc.Add("start_time",u_start_time );
            doc.Add("end_time", u_end_time);
            doc.Add("count", count);
            doc.Add("open", open);
            doc.Add("hight", hight);
            doc.Add("low", low);
            doc.Add("close", close);
            doc.Add("open_tid", open_tid);
            doc.Add("hight_tid", hight_tid);
            doc.Add("low_tid", low_tid);
            doc.Add("close_tid", close_tid); 
            return doc;
        }
        public static BsonArray get_candle(string website, DateTime start_time, DateTime end_time, int seconds)
        {
            BsonArray list = new BsonArray();
            UInt64 u_start_time = UnixTime.get_unix_time_from_local_long(start_time);
            UInt64 u_end_time = UnixTime.get_unix_time_from_local_long(end_time);
            while (u_start_time < u_end_time)
            {
                DateTime dt=UnixTime.get_local_time_long(u_start_time);
                BsonDocument doc = get_region("", dt, seconds);
                list.Add(doc);
                u_start_time = UnixTime.get_unix_time_from_local_long(dt.AddSeconds(seconds));
            } 
            return list; 
        }
        public static  string get_region_info_title()
        {
            StringBuilder sb=new StringBuilder();
              
            sb.Append("START-TIME".PR(20) +
                      "END-TIME".PR(20) +
                      "SECOND".PR(10)+"COUNT".PR(10)+
                      "OPEN".PR(10) + "HIGHT".PR(10) + "LOW".PR(10) + "CLOSE".PR(10) + M.N);
            //sb.Append("START TIME".PR(20) + UnixTime.get_local_time_long(Convert.ToUInt64(doc["start_time"].ToString())).ToString("yyyy-MM-dd HH:mm:ss") + M.N);
            //sb.Append("END TIME".PR(20) + UnixTime.get_local_time_long(Convert.ToUInt64(doc["end_time"].ToString())).ToString("yyyy-MM-dd HH:mm:ss") + M.N);
            //sb.Append("SECONDS".PR(20) + doc["seconds"].ToString() + M.N);
            //sb.Append("COUNT".PR(20) + doc["count"].ToString() + M.N);
            //sb.Append("OPEN".PR(20) + doc["open"].ToString() + M.N);
            //sb.Append("HIGHT".PR(20) + doc["hight"].ToString() + M.N);
            //sb.Append("LOW".PR(20) + doc["low"].ToString() + M.N);
            //sb.Append("CLOSE".PR(20) + doc["close"].ToString() + M.N);
            return sb.ToString();
        }
        public static  string get_region_info(BsonDocument doc)
        {
            StringBuilder sb=new StringBuilder();
              
            sb.Append(UnixTime.get_local_time_long(Convert.ToUInt64(doc["start_time"].ToString())).ToString("yyyy-MM-dd HH:mm:ss").PR(20) +
                      UnixTime.get_local_time_long(Convert.ToUInt64(doc["end_time"].ToString())).ToString("yyyy-MM-dd HH:mm:ss").PR(20) +
                      doc["seconds"].PR(10)+doc["count"].PR(10)+
                      doc["open"].PR(10) + doc["hight"].PR(10) + doc["low"].PR(10) + doc["close"].PR(10) + M.N);
            //sb.Append("START TIME".PR(20) + UnixTime.get_local_time_long(Convert.ToUInt64(doc["start_time"].ToString())).ToString("yyyy-MM-dd HH:mm:ss") + M.N);
            //sb.Append("END TIME".PR(20) + UnixTime.get_local_time_long(Convert.ToUInt64(doc["end_time"].ToString())).ToString("yyyy-MM-dd HH:mm:ss") + M.N);
            //sb.Append("SECONDS".PR(20) + doc["seconds"].ToString() + M.N);
            //sb.Append("COUNT".PR(20) + doc["count"].ToString() + M.N);
            //sb.Append("OPEN".PR(20) + doc["open"].ToString() + M.N);
            //sb.Append("HIGHT".PR(20) + doc["hight"].ToString() + M.N);
            //sb.Append("LOW".PR(20) + doc["low"].ToString() + M.N);
            //sb.Append("CLOSE".PR(20) + doc["close"].ToString() + M.N);
            return sb.ToString();
        }
    }
 