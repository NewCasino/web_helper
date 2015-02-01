using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;

namespace btc_helper
{
    public partial class frm_single_btcchina : Form
    {
        StringBuilder sb = new StringBuilder();
        public frm_single_btcchina()
        {
            InitializeComponent();
        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            StringBuilder sb=new StringBuilder();
            DateTime dt_start = UnixTime.get_local_time_long(1329104164000);
            DateTime dt_end = dt_start.AddDays(0.5);
            BsonArray list = BtcCompute.get_candle("", dt_start, dt_end, 3600);

            sb.Append(BtcCompute.get_region_info_title());
            for (int i = 0; i < list.Count; i++)
            {
                BsonDocument doc = list[i].AsBsonDocument;
                sb.Append(BtcCompute.get_region_info(doc)); 
            }
            this.txt_result.Text = sb.ToString();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            delete_repeat_trade();
        }
        private void btn_analyse_depth_Click(object sender, EventArgs e)
        {
            analyse_depth();
        }



        public void delete_repeat_trade()
        {
            string sql = "";
            int delete_count = 0;
            for (int i = 0; i < 20000000; i++)
            {
                sql = "select * from trade_btcchina where id={0}";
                sql = string.Format(sql, i);
                DataTable dt_temp=SQLServerHelper.get_table(sql);
                if (dt_temp.Rows.Count > 0)
                {
                    string id = dt_temp.Rows[0]["id"].ToString();
                    string tid = dt_temp.Rows[0]["tid"].ToString();

                    sql = "select * from trade_btcchina where tid={0} and id>{1}";
                    sql = string.Format(sql, tid, id);
                    DataTable dt_repeat = SQLServerHelper.get_table(sql);
                    if (dt_repeat.Rows.Count > 0)
                    {
                        sql = "delete from trade_btcchina where  tid={0} and id>{1}";
                        sql = string.Format(sql, tid, id);
                        SQLServerHelper.exe_sql(sql);
                        delete_count = delete_count + 1;
                    }
                    this.txt_result.Text = delete_count.PR(10) + id.PR(10) + tid.PR(10) + dt_temp.Rows.Count.PR(10);
                    Application.DoEvents();
                } 
            }
        }
        public void analyse_depth()
        { 
            StringBuilder sb = new StringBuilder();
            string sql = "select distinct time from depth_log where website='btcchina'";
            DataTable dt = SQLServerHelper.get_table(sql);
            foreach (DataRow row in dt.Rows)
            {
                string time = row["time"].ToString();
                BsonArray buys=new BsonArray();
                BsonArray sells=new BsonArray();

                sql = "select * from depth_log where website='btcchina' and time={0} and type='buy'";
                sql = string.Format(sql, time);
                DataTable dt_temp = SQLServerHelper.get_table(sql);
                if(dt_temp.Rows.Count>0)  buys=MongoHelper.get_array_from_str(dt_temp.Rows[0]["text"].ToString());

                sql = "select * from depth_log where website='btcchina' and time={0} and type='sell'";
                sql = string.Format(sql, time);
                dt_temp = SQLServerHelper.get_table(sql);
                if (dt_temp.Rows.Count > 0) sells = MongoHelper.get_array_from_str(dt_temp.Rows[0]["text"].ToString());


                buys = MongoHelper.reserve_array(buys);
                sells = MongoHelper.reserve_array(sells);

                sql = "select top 100  * from trade_btcchina where time>=time";
                DataTable dt_trade = SQLServerHelper.get_table(sql);

                sb.Append(UnixTime.get_local_time_long(Convert.ToUInt64(time)).ToString("yyyy-MM-dd HH:mm:ss").PR(20) + buys.Count.PR(10) + sells.Count.PR(10)+dt_trade.Rows.Count.PR(10)+M.N);
            }
            this.txt_result.Text = sb.ToString();
            Application.DoEvents(); 
        }

 

    }
}
