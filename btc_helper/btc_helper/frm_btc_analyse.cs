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
    public partial class frm_btc_analyse : Form
    {
       
        StringBuilder sb = new StringBuilder();
        public frm_btc_analyse()
        {
            InitializeComponent(); 
        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }
        private void btn_get_Click(object sender, EventArgs e)
        {
            this.grid.DataSource = get_depth_table();
        }
        private void btn_get_ticker_Click(object sender, EventArgs e)
        {
            this.grid.DataSource = get_ticker_table();
        }
        private void btn_analyse_Click(object sender, EventArgs e)
        {
            this.grid.DataSource = get_analyse_table();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            this.lb_time.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void btn_start_Click(object sender, EventArgs e)
        {
            this.timer.Start();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            this.timer.Stop();
        }
        private void btn_test_Click(object sender, EventArgs e)
        {

          

        }
        public DataTable get_depth_table()
        {

            string coin = get_coin();

            DataTable dt_result = new DataTable();

            string sql = "";
            double rate = CurrencyHelper.get_rate("usd", "cny");

            sql = " select max(a.qty) from" +
                  " (select count(*) qty  from depth group  by website,type) a";
            int row_count = Convert.ToInt32(SQLServerHelper.get_table(sql).Rows[0][0].ToString());


            sql = "select distinct website from depth where currency like '%{0}%'";
            sql = string.Format(sql, coin);
            DataTable dt_website = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt_website.Rows)
            {
                dt_result.Columns.Add(row[0].ToString() + "-0-price");
                dt_result.Columns.Add(row[0].ToString() + "-0-qty");
                dt_result.Columns.Add(row[0].ToString() + "-1-price");
                dt_result.Columns.Add(row[0].ToString() + "-1-qty");
            }

            for (int i = 0; i < row_count; i++)
            {
                DataRow row_new = dt_result.NewRow();
                dt_result.Rows.Add(row_new);
            }

            foreach (DataRow row_website in dt_website.Rows)
            {
                string website = row_website[0].ToString();
                sql = "select * from depth where website='{0}' and type='sell' and currency like '%{1}%'  order by price";
                sql = string.Format(sql, website, coin);
                DataTable dt_temp1 = SQLServerHelper.get_table(sql);
                for (int i = 0; i < dt_temp1.Rows.Count; i++)
                {
                    double price1 = Convert.ToDouble(dt_temp1.Rows[i]["price"].ToString());

                    if (dt_temp1.Rows[i]["currency"].ToString().Contains("usd"))
                    {
                        price1 = price1 * rate;
                    }
                    dt_result.Rows[i][website + "-0-price"] = Math.Round(price1, 3).ToString(); ;
                    dt_result.Rows[i][website + "-0-qty"] = dt_temp1.Rows[i]["qty"].ToString();
                }


                sql = "select * from depth where website='{0}' and type='buy'  and currency like '%{1}%' order by price desc";
                sql = string.Format(sql, row_website[0].ToString(), coin);
                DataTable dt_temp2 = SQLServerHelper.get_table(sql);
                for (int i = 0; i < dt_temp2.Rows.Count; i++)
                {
                    double price2 = Convert.ToDouble(dt_temp2.Rows[i]["price"].ToString());
                    if (dt_temp2.Rows[i]["currency"].ToString().Contains("usd"))
                    {
                        price2 = price2 * rate;
                    }
                    dt_result.Rows[i][website + "-1-price"] = Math.Round(price2, 3).ToString();
                    dt_result.Rows[i][website + "-1-qty"] = dt_temp2.Rows[i]["qty"].ToString();
                }
            }
            return dt_result;
        }
        public DataTable get_ticker_table()
        {

            string coin = get_coin();

            string sql = "select * from ticker where currency like '%{0}%'";
            sql = string.Format(sql, coin);
            DataTable dt_ticker = SQLServerHelper.get_table(sql);

            double rate = CurrencyHelper.get_rate("usd", "cny");
            foreach (DataRow row in dt_ticker.Rows)
            {
                if (row["currency"].ToString().Contains("usd"))
                {
                    row["last"] = Math.Round(Convert.ToDouble(row["last"].ToString()) * rate, 4).ToString();
                    row["sell"] = Math.Round(Convert.ToDouble(row["sell"].ToString()) * rate, 4).ToString();
                    row["buy"] = Math.Round(Convert.ToDouble(row["buy"].ToString()) * rate, 4).ToString();
                    row["high"] = Math.Round(Convert.ToDouble(row["high"].ToString()) * rate, 4).ToString();
                    row["low"] = Math.Round(Convert.ToDouble(row["low"].ToString()) * rate, 4).ToString();
                }


            }

            return dt_ticker;
        }
        public DataTable get_analyse_table()
        {
            string coin = get_coin();
            string sql = "";
            double rate = CurrencyHelper.get_rate("usd", "cny");
            DataTable dt_result = new DataTable();
            dt_result.Columns.Add("sell_price");
            dt_result.Columns.Add("buy_price");
            dt_result.Columns.Add("sell_website");
            dt_result.Columns.Add("buy_website");
            dt_result.Columns.Add("sell_qty");
            dt_result.Columns.Add("buy_qty");
            dt_result.Columns.Add("sell_currency");
            dt_result.Columns.Add("buy_currency");

            sql = "select *  from depth where type='sell' and  currency like '%{0} %' order by  price ";
            sql = string.Format(sql, coin);
            DataTable dt_sell = SQLServerHelper.get_table(sql);

            sql = "select *  from depth where type='buy' and currency like '%{0}%' order by  price desc";
            sql = string.Format(sql, coin);
            DataTable dt_buy = SQLServerHelper.get_table(sql);

            int count = int.MaxValue;
            if (dt_sell.Rows.Count < count) count = dt_sell.Rows.Count;
            if (dt_buy.Rows.Count < count) count = dt_buy.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow row_new = dt_result.NewRow();
                dt_result.Rows.Add(row_new);
            }
            for (int i = 0; i < count; i++)
            {
                dt_result.Rows[i]["sell_price"] = dt_sell.Rows[i]["price"].ToString();
                dt_result.Rows[i]["sell_website"] = dt_sell.Rows[i]["website"].ToString();
                dt_result.Rows[i]["sell_qty"] = dt_sell.Rows[i]["qty"].ToString();
                dt_result.Rows[i]["sell_currency"] = dt_sell.Rows[i]["currency"].ToString();

                dt_result.Rows[i]["buy_price"] = dt_buy.Rows[i]["price"].ToString();
                dt_result.Rows[i]["buy_website"] = dt_buy.Rows[i]["website"].ToString();
                dt_result.Rows[i]["buy_qty"] = dt_buy.Rows[i]["qty"].ToString();
                dt_result.Rows[i]["buy_currency"] = dt_buy.Rows[i]["currency"].ToString();
            }
            foreach (DataRow row in dt_result.Rows)
            {
                if (row["sell_currency"].ToString() == "btc_usd") row["sell_price"] = Math.Round(Convert.ToDouble(row["sell_price"].ToString()) * rate, 4);
                if (row["buy_currency"].ToString() == "btc_usd") row["buy_price"] = Math.Round(Convert.ToDouble(row["buy_price"].ToString()) * rate, 4);
            }

            return dt_result;
        }
        public string get_coin()
        {
            string result = "btc";
            if (cb_ltc.Checked) result = "ltc";
            return result;
        }
    }
}
