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

        public void delete_repeat_trade()
        { 
            string sql = "select * from trade_btcchina "; 
            DataTable dt = SQLServerHelper.get_table(sql);
            int delete_count = 0;
            foreach (DataRow row in dt.Rows)
            {
                string id = row["id"].ToString();
                string tid = row["tid"].ToString();
                sql = "select * from trade_btcchina where tid={0} and id>{1}";
                sql = string.Format(sql,tid, id);
                DataTable dt_temp = SQLServerHelper.get_table(sql);
                if (dt_temp.Rows.Count > 0)
                {
                    sql = "delete from trade_btcchina where  tid={0} and id>{1}";
                    sql = string.Format(sql,tid, id); 
                    SQLServerHelper.exe_sql(sql);
                    delete_count = delete_count + 1; 
                }
                this.txt_result.Text = delete_count.PR(10)+id.PR(10) + tid.PR(10) + dt_temp.Rows.Count.PR(10);
                Application.DoEvents();
              
            }
        }
  
    }
}
