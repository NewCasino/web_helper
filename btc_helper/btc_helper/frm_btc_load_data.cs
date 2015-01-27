using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;
using System.Threading;

namespace btc_helper
{
    public partial class frm_btc_load_data : Form
    {
        StringBuilder sb = new StringBuilder();

        public frm_btc_load_data()
        {
            InitializeComponent();
        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }
        private void btn_get_data_Click(object sender, EventArgs e)
        {
            okcoin_cn_btc_cny_trade();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
         
            this.lb_time.Text = System.DateTime.Now.ToString("HH:mm:ss");
            timer.Start();

            //every 1 seconds
            if (DateTime.Now.Second % 1 == 0)
            {
                 
            }
            //every 2 seconds
            if (DateTime.Now.Second % 2 == 0)
            {
                 
            }
            //every 5 seconds
            if (DateTime.Now.Second % 5 == 0)
            {

            }
            //every 10 seconds
            if (DateTime.Now.Second % 10 == 0)
            {

            }
            //every 15 seconds
            if (DateTime.Now.Second % 15 == 0)
            {

            }
            //every 20 seconds
            if (DateTime.Now.Second % 20 == 0)
            {
                get_data_trade();
            }
            //every 25 seconds
            if (DateTime.Now.Second % 25 == 0)
            {

            }
            //every 30 seconds
            if (DateTime.Now.Second % 30 == 0)
            {
                
            }
        }
        private void btn_start_Click(object sender, EventArgs e)
        { 
            timer.Start();
        } 
        private void btn_stop_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        public void get_data_ticker()
        {

            string sql = "select count(*) from ticker_log ";
            DataTable dt = SQLServerHelper.get_table(sql);
            string template = "TICKER COUNT:{0}";
            sb.AppendLine(string.Format(template, dt.Rows[0][0].ToString()));
            this.txt_result.Text = sb.PRINT(); 

           
            if (cb_btcchina_btc_cny_ticker.Checked) { Thread thread_btcchina_btc_cny_ticker = new Thread(new ThreadStart(btcchina_btc_cny_ticker)); thread_btcchina_btc_cny_ticker.Start(); }  
            if (cb_btcchina_ltc_cny_ticker.Checked) { Thread thread_btcchina_ltc_cny_ticker = new Thread(new ThreadStart(btcchina_ltc_cny_ticker)); thread_btcchina_ltc_cny_ticker.Start(); }

            
            if (cb_okcoin_cn_btc_cny_ticker.Checked) { Thread thread_okcoin_cn_btc_cny_ticker = new Thread(new ThreadStart(okcoin_cn_btc_cny_ticker)); thread_okcoin_cn_btc_cny_ticker.Start(); } 
            if (cb_okcoin_cn_ltc_cny_ticker.Checked) { Thread thread_okcoin_cn_ltc_cny_ticker = new Thread(new ThreadStart(okcoin_cn_ltc_cny_ticker)); thread_okcoin_cn_ltc_cny_ticker.Start(); }

            if (cb_btctrade_btc_cny_depth.Checked) { Thread thread_btctrade_btc_cny_depth = new Thread(new ThreadStart(btctrade_btc_cny_depth)); thread_btctrade_btc_cny_depth.Start(); }
            if (cb_btctrade_btc_cny_ticker.Checked) { Thread thread_btctrade_btc_cny_ticker = new Thread(new ThreadStart(btctrade_btc_cny_ticker)); thread_btctrade_btc_cny_ticker.Start(); }

            
            if (cb_huobi_btc_cny_ticker.Checked) { Thread thread_huobi_btc_cny_ticker = new Thread(new ThreadStart(huobi_btc_cny_ticker)); thread_huobi_btc_cny_ticker.Start(); }
            if (cb_huobi_ltc_cny_ticker.Checked) { Thread thread_huobi_ltc_cny_ticker = new Thread(new ThreadStart(huobi_ltc_cny_ticker)); thread_huobi_ltc_cny_ticker.Start(); }

           
            if (cb_btce_btc_usd_ticker.Checked) { Thread thread_btce_btc_usd_ticker = new Thread(new ThreadStart(btce_btc_usd_ticker)); thread_btce_btc_usd_ticker.Start(); }
            if (cb_btce_ltc_usd_ticker.Checked) { Thread thread_btce_ltc_usd_ticker = new Thread(new ThreadStart(btce_ltc_usd_ticker)); thread_btce_ltc_usd_ticker.Start(); }

            
            if (cb_okcoin_com_btc_usd_ticker.Checked) { Thread thread_okcoin_com_btc_usd_ticker = new Thread(new ThreadStart(okcoin_com_btc_usd_ticker)); thread_okcoin_com_btc_usd_ticker.Start(); }
            if (cb_okcoin_com_ltc_usd_ticker.Checked) { Thread thread_okcoin_com_ltc_usd_ticker = new Thread(new ThreadStart(okcoin_com_ltc_usd_ticker)); thread_okcoin_com_ltc_usd_ticker.Start(); }
            
        }
        public void get_data_depth()
        {

            string sql = "select count(*) from depth_log ";
            DataTable dt = SQLServerHelper.get_table(sql);
            string template = "DEPTH COUNT:{0}";
            sb.AppendLine(string.Format(template, dt.Rows[0][0].ToString()));
            this.txt_result.Text = sb.PRINT(); 


            if (cb_btcchina_btc_cny_depth.Checked) { Thread thread_btcchina_btc_cny_depth = new Thread(new ThreadStart(btcchina_btc_cny_depth)); thread_btcchina_btc_cny_depth.Start(); }
            if (cb_btcchina_ltc_cny_depth.Checked) { Thread thread_btcchina_ltc_cny_depth = new Thread(new ThreadStart(btcchina_ltc_cny_depth)); thread_btcchina_ltc_cny_depth.Start(); }

            if (cb_okcoin_cn_btc_cny_depth.Checked) { Thread thread_okcoin_cn_btc_cny_depth = new Thread(new ThreadStart(okcoin_cn_btc_cny_depth)); thread_okcoin_cn_btc_cny_depth.Start(); }

            if (cb_huobi_btc_cny_depth.Checked) { Thread thread_huobi_btc_cny_depth = new Thread(new ThreadStart(huobi_btc_cny_depth)); thread_huobi_btc_cny_depth.Start(); }
            if (cb_huobi_ltc_cny_depth.Checked) { Thread thread_huobi_ltc_cny_depth = new Thread(new ThreadStart(huobi_ltc_cny_depth)); thread_huobi_ltc_cny_depth.Start(); }

            if (cb_btce_btc_usd_depth.Checked) { Thread thread_btce_btc_usd_depth = new Thread(new ThreadStart(btce_btc_usd_depth)); thread_btce_btc_usd_depth.Start(); }
            if (cb_btce_ltc_usd_depth.Checked) { Thread thread_btce_ltc_usd_depth = new Thread(new ThreadStart(btce_ltc_usd_depth)); thread_btce_ltc_usd_depth.Start(); }

            if (cb_okcoin_com_btc_usd_depth.Checked) { Thread thread_okcoin_com_btc_usd_depth = new Thread(new ThreadStart(okcoin_com_btc_usd_depth)); thread_okcoin_com_btc_usd_depth.Start(); }
            if (cb_okcoin_com_ltc_usd_depth.Checked) { Thread thread_okcoin_com_ltc_usd_depth = new Thread(new ThreadStart(okcoin_com_ltc_usd_depth)); thread_okcoin_com_ltc_usd_depth.Start(); }

        }
        public void get_data_trade()
        {
            string max_id = BtcHelper.get_trade_max_id("btcchina");
            string sql = "select * from trade_btcchina where tid={0}";
            sql = string.Format(sql, max_id);
            DataTable dt_temp = SQLServerHelper.get_table(sql);
            sb.Append("BTCCHINA".PR(10) + dt_temp.Rows[0]["tid"].ToString().PR(15) + UnixTime.get_local_time_long(Convert.ToUInt64(dt_temp.Rows[0]["time"].ToString())).ToString("yyyy-MM-dd HH:mm:ss") + M.N);
            max_id = BtcHelper.get_trade_max_id("okcoin_cn");
            sql = "select * from trade_okcoin_cn where tid={0}";
            sql = string.Format(sql, max_id);
            dt_temp = SQLServerHelper.get_table(sql);
            sb.Append("OKCOIN_CN:".PR(10) + dt_temp.Rows[0]["tid"].ToString().PR(15) + UnixTime.get_local_time_long(Convert.ToUInt64(dt_temp.Rows[0]["time"].ToString())).ToString("yyyy-MM-dd HH:mm:ss") + M.N);
            this.txt_result.Text = sb.PRINT();
            if (cb_btcchina_btc_cny_trade.Checked) { Thread thread_btcchina_btc_cny_trade = new Thread(new ThreadStart(btcchina_btc_cny_trade)); thread_btcchina_btc_cny_trade.Start(); }
            if (cb_okcoin_cn_btc_cny_trade.Checked) { Thread thread_okcoin_cn_btc_cny_trade = new Thread(new ThreadStart(okcoin_cn_btc_cny_trade)); thread_okcoin_cn_btc_cny_trade.Start(); } 
        }
         
        public void btcchina_btc_cny_depth()
        {
            try
            {
                string result = BtcchinaApi.depth(Pair.btc_cny.ToString());
                BtcchinaData.insert_depth(result, Pair.btc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("BtcchinaApi.depth.btc_cny", error);
            }
        }
        public void btcchina_btc_cny_ticker()
        {
            try
            {
                string result = BtcchinaApi.ticker(Pair.btc_cny.ToString());
                BtcchinaData.insert_ticker(result, Pair.btc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("BtcchinaApi.ticker.btc_cny", error);
            }
        }
        public void btcchina_btc_cny_trade()
        {
            string max_id = BtcHelper.get_trade_max_id("btcchina"); 
            string result = BtcchinaApi.trade_by_id(Pair.btc_cny.ToString(), max_id);
            BsonArray list = MongoHelper.get_array_from_str(result);
            for (int i = 0; i < list.Count; i++)
            {
                BtcHelper.insert_trade("btcchina", list[i]["tid"].ToString(), list[i]["date"].ToString() + "000", list[i]["price"].ToString(), list[i]["amount"].ToString(), list[i]["type"].ToString(), "btc_cny", "btc");
            }
        }

        public void btcchina_ltc_cny_depth()
        {
            try
            {
                string result = BtcchinaApi.depth(Pair.ltc_cny.ToString());
                BtcchinaData.insert_depth(result, Pair.ltc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("BtcchinaApi.depth.ltc_cny", error);
            }
        }
        public void btcchina_ltc_cny_ticker()
        {
            try
            {
                string result = BtcchinaApi.ticker(Pair.ltc_cny.ToString());
                BtcchinaData.insert_ticker(result, Pair.ltc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("BtcchinaApi.ticker.ltc_cny", error);
            }
        }

        public void okcoin_cn_btc_cny_depth()
        {
            try
            {
                string result = OkcCNApi.depth(Pair.btc_cny.ToString());
                OkcCNData.insert_depth(result, Pair.btc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("CnokApi.depth.btc_cny", error);
            }
        }
        public void okcoin_cn_btc_cny_ticker()
        {
            try
            {
                string result = OkcCNApi.ticker(Pair.btc_cny.ToString());
                OkcCNData.insert_ticker(result, Pair.btc_cny.ToString());


            }
            catch (Exception error)
            {
                Log.error("Cnokcapi.ticker.btc_cny", error);
            }
        }
        public void okcoin_cn_ltc_cny_ticker()
        {
            try
            {
                string result = OkcCNApi.ticker(Pair.ltc_cny.ToString());
                OkcCNData.insert_ticker(result, Pair.ltc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("CnokcApi.ticker.ltc_cny", error);
            }
        }
        public void okcoin_cn_btc_cny_trade()
        {
            string max_id = BtcHelper.get_trade_max_id("okcoin_cn");
            string result = OkcCNApi.trade(max_id);
            BsonArray list = MongoHelper.get_array_from_str(result);
            for (int i = 0; i < list.Count; i++)
            {
                BtcHelper.insert_trade("okcoin_cn", list[i]["tid"].ToString(), list[i]["date_ms"].ToString(), list[i]["price"].ToString(), list[i]["amount"].ToString(), list[i]["type"].ToString(), "btc_cny", "btc");
            }
        }

        public void btctrade_btc_cny_depth()
        {
            try
            {
                string result = BtctradeApi.depth();
                BtctradeData.insert_depth(result);
            }
            catch (Exception error)
            {
                Log.error("BtctradeApi.depth", error);
            }
        }
        public void btctrade_btc_cny_ticker()
        {
            try
            {
                string result = BtctradeApi.ticker();
                BtctradeData.insert_ticker(result);

            }
            catch (Exception error)
            {
                Log.error("BtctradeApi.ticker", error);
            }
        } 
       
        public void huobi_btc_cny_depth()
        {
            try
            {
                string result = HuobiApi.depth(Pair.btc_cny.ToString());
                HuobiData.insert_depth(result, Pair.btc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("HuobiApi.depth.btc_cny", error);
            }
        }
        public void huobi_btc_cny_ticker()
        {
            try
            {
                string result = HuobiApi.ticker(Pair.btc_cny.ToString());
                HuobiData.insert_ticker(result, Pair.btc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("HuobiApi.ticker.btc_cny", error);
            }
        }
        public void huobi_ltc_cny_depth()
        {
            try
            {
                string result = HuobiApi.depth(Pair.ltc_cny.ToString());
                HuobiData.insert_depth(result, Pair.ltc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("HuobiApi.depth_ltc_cny", error);
            }
        }
        public void huobi_ltc_cny_ticker()
        {
            try
            {
                string result = HuobiApi.ticker(Pair.ltc_cny.ToString());
                HuobiData.insert_ticker(result, Pair.ltc_cny.ToString());
            }
            catch (Exception error)
            {
                Log.error("HuobiApi.ticker.ltc_cny", error);
            }
        }
       
        public void btce_btc_usd_ticker()
        {
            try
            {
                string result = BtceApi.tiker(Pair.btc_usd.ToString());
                BtceData.insert_ticker(result, Pair.btc_usd.ToString());
            }
            catch (Exception error)
            {
                Log.error("BtceApi.ticker.btc_usd", error);
            }
        }
        public void btce_btc_usd_depth()
        {
            try
            {
                string result = BtceApi.depth(Pair.btc_usd.ToString());
                BtceData.insert_depth(result, Pair.btc_usd.ToString());
            }
            catch (Exception error)
            {
                Log.error("BtceApi.depth.btc_usd", error);
            }
        }
        public void btce_ltc_usd_depth()
        {
            try
            {
                string result = BtceApi.depth(Pair.ltc_usd.ToString());
                BtceData.insert_depth(result, Pair.ltc_usd.ToString());
            }
            catch (Exception error)
            {
                Log.error("BtceApi.depth.ltc_usd", error);
            }
        }
        public void btce_ltc_usd_ticker()
        {
            try
            {
                string result = BtceApi.tiker(Pair.ltc_usd.ToString());
                BtceData.insert_ticker(result, Pair.ltc_usd.ToString());
            }
            catch (Exception error)
            {
                Log.error("BtceApi.ticker.ltc_usd", error);
            }
        }

        public void okcoin_com_btc_usd_depth()
        {
            try
            {
                string result = OkcCOMApi.depth(Pair.btc_usd.ToString());
                OkcCOMData.insert_depth(result, Pair.btc_usd.ToString());
            }
            catch (Exception error)
            {
                Log.error("OkcApi.depth.btc_usd", error);
            }
        }
        public void okcoin_com_btc_usd_ticker()
        {
            try
            {
                string result = OkcCOMApi.ticker(Pair.btc_usd.ToString());
                OkcCOMData.insert_ticker(result, Pair.btc_usd.ToString());
            }
            catch (Exception error)
            {
                Log.error("OkcApi.ticker.btc_usd", error);
            }
        }
        public void okcoin_com_ltc_usd_depth()
        {
            try
            {
                string result = OkcCOMApi.depth(Pair.ltc_usd.ToString());
                OkcCOMData.insert_depth(result, Pair.ltc_usd.ToString());
            }
            catch (Exception error)
            {
                Log.error("OkcApi.depth.ltc_usd", error);
            }
        }
        public void okcoin_com_ltc_usd_ticker()
        {
            try
            {

                string result = OkcCOMApi.ticker(Pair.ltc_usd.ToString());
                OkcCOMData.insert_ticker(result, Pair.ltc_usd.ToString());

            }
            catch (Exception error)
            {
                Log.error("Okcapi.ticker.ltc_usd", error);
            }
        } 
    }
}
