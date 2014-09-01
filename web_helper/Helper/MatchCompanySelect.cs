using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace web_helper.Helper
{
    class MatchwebsiteSelect
    {
        public void get_data(string url)
        {
            switch (url.ToLower())
            {

                case "biwn": 
                    break; 
                default:
                    break; 
            }

        }
        public void from_bwin()
        { 

        }

        public void insert_data(string start_time,string host,string client,string website,string odd_win,string odd_draw,string odd_lose)
        {
            string sql="";
            string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string persent_win = "";
            string persent_draw = "";
            string persent_lose = "";
            string kelly_win = "";
            string kelly_draw = "";
            string kelly_lose = "";
            string persent_return = "";
            string start_odd_win = "";
            string start_odd_draw = "";
            string start_odd_lose = "";
            string start_persent_win = "";
            string start_persent_draw = "";
            string start_persent_lose = "";
            string start_kelly_win = "";
            string start_kelly_draw = "";
            string start_kelly_lose = "";
            string start_persent_return="";
            string lg=""; 
            //insert into table europe
            sql = " insert into europe " +
                  " (start_time,host,client,website,timespan," +
                  "  odd_win,odd_draw,odd_lose,persent_win,persent_draw,persent_lose,persent_return,kelly_win,kelly_draw,kelly_lose," +
                  "  start_odd_win,start_odd_draw,start_odd_lose,start_persent_win,start_persent_draw,start_persent_lose," +
                  "  start_persent_return,start_kelly_win,start_kelly_draw,start_kelly_lose,type)" +
                  "  values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}'," +
                  "          '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}')";
            sql = string.Format(sql, start_time, host, client, website, timespan,
                             odd_win, odd_draw, odd_lose, persent_win, persent_draw, persent_lose, persent_return, kelly_win, kelly_draw, kelly_lose,
                             start_odd_win, start_odd_draw, start_odd_lose, start_persent_win, start_persent_draw, start_persent_lose,
                             start_persent_return, start_kelly_win, start_kelly_draw, start_kelly_lose, lg);
            SQLServerHelper.exe_sql(sql);


            //insert into table europe_new
            sql = "delete  from europe_new where start_time='{0}' and host='{1}' and client='{2}' and website='{3}'";
            sql = string.Format(sql, start_time, host, client, website);
            SQLServerHelper.exe_sql(sql);
            sql = " insert into europe_new " +
                 " (start_time,host,client,website,timespan," +
                 "  odd_win,odd_draw,odd_lose,persent_win,persent_draw,persent_lose,persent_return,kelly_win,kelly_draw,kelly_lose," +
                 "  start_odd_win,start_odd_draw,start_odd_lose,start_persent_win,start_persent_draw,start_persent_lose," +
                 "  start_persent_return,start_kelly_win,start_kelly_draw,start_kelly_lose,type)" +
                 "  values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}'," +
                 "          '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}')";
            sql = string.Format(sql, start_time, host, client, website, timespan,
                             odd_win, odd_draw, odd_lose, persent_win, persent_draw, persent_lose, persent_return, kelly_win, kelly_draw, kelly_lose,
                             start_odd_win, start_odd_draw, start_odd_lose, start_persent_win, start_persent_draw, start_persent_lose,
                             start_persent_return, start_kelly_win, start_kelly_draw, start_kelly_lose, lg);
            SQLServerHelper.exe_sql(sql);
        }

    }


}
