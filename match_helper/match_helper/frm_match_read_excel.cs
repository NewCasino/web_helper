using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace match_helper
{


    public partial class frm_match_read_excel : Form
    {

        public DataTable table = new DataTable();
        public frm_match_read_excel()
        {
            InitializeComponent();
        } 
        private void btn_read_Click(object sender, EventArgs e)
        {
            table = get_table_from_excel(@"C:/data/de.xls", 2);
            this.dgv_result.DataSource = table;
        } 
        private void btn_read_to_db_Click(object sender, EventArgs e)
        {
            string sql = "";
            DataTable dt_temp = new DataTable();
            dt_temp = table;
            for(int i=5;i<dt_temp.Rows.Count;i++)
            {
                string start_time = "2014-09-04 00:00:00";
                string host = "德国";
                string client = "阿根廷";
                string website = dt_temp.Rows[i][0].ToString();
                string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string odd_win = (string.IsNullOrEmpty(dt_temp.Rows[i][1].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][1].ToString()).ToString("f2");
                string odd_draw = (string.IsNullOrEmpty(dt_temp.Rows[i][2].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][2].ToString()).ToString("f2");
                string odd_lose = (string.IsNullOrEmpty(dt_temp.Rows[i][3].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][3].ToString()).ToString("f2");
                string persent_win = (string.IsNullOrEmpty(dt_temp.Rows[i][4].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][4].ToString()).ToString("f2");
                string persent_draw = (string.IsNullOrEmpty(dt_temp.Rows[i][5].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][5].ToString()).ToString("f2");
                string persent_lose = (string.IsNullOrEmpty(dt_temp.Rows[i][6].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][6].ToString()).ToString("f2");
                string persent_return = (string.IsNullOrEmpty(dt_temp.Rows[i][7].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][7].ToString()).ToString("f2");
                string kelly_win = (string.IsNullOrEmpty(dt_temp.Rows[i][8].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][8].ToString()).ToString("f2");
                string kelly_draw = (string.IsNullOrEmpty(dt_temp.Rows[i][9].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][9].ToString()).ToString("f2");
                string kelly_lose = (string.IsNullOrEmpty(dt_temp.Rows[i][10].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][10].ToString()).ToString("f2");

                string start_odd_win = (string.IsNullOrEmpty(dt_temp.Rows[i][11].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][11].ToString()).ToString("f2");
                string start_odd_draw = (string.IsNullOrEmpty(dt_temp.Rows[i][12].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][12].ToString()).ToString("f2");
                string start_odd_lose = (string.IsNullOrEmpty(dt_temp.Rows[i][13].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][13].ToString()).ToString("f2");
                string start_persent_win = (string.IsNullOrEmpty(dt_temp.Rows[i][14].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][14].ToString()).ToString("f2");
                string start_persent_draw = (string.IsNullOrEmpty(dt_temp.Rows[i][15].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][15].ToString()).ToString("f2");
                string start_persent_lose = (string.IsNullOrEmpty(dt_temp.Rows[i][16].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][16].ToString()).ToString("f2");
                string start_persent_return = (string.IsNullOrEmpty(dt_temp.Rows[i][17].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][17].ToString()).ToString("f2");
                string start_kelly_win = (string.IsNullOrEmpty(dt_temp.Rows[i][18].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][18].ToString()).ToString("f2");
                string start_kelly_draw = (string.IsNullOrEmpty(dt_temp.Rows[i][19].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][19].ToString()).ToString("f2");
                string start_kelly_lose = (string.IsNullOrEmpty(dt_temp.Rows[i][20].ToString())) ? "" : Convert.ToDouble(dt_temp.Rows[i][20].ToString()).ToString("f2");


                //insert into table europe
                sql = " insert into europe " +
                      " (start_time,host,client,website,timespan," +
                      "  odd_win,odd_draw,odd_lose,persent_win,persent_draw,persent_lose,persent_return,kelly_win,kelly_draw,kelly_lose," +
                      "  start_odd_win,start_odd_draw,start_odd_lose,start_persent_win,start_persent_draw,start_persent_lose," +
                      "  start_persent_return,start_kelly_win,start_kelly_draw,start_kelly_lose)" +
                      "  values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}'," +
                      "          '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}')";
                sql = string.Format(sql, start_time, host, client, website, timespan,
                                 odd_win, odd_lose, odd_lose, persent_win, persent_draw, persent_lose, persent_return, kelly_win, kelly_draw, kelly_win,
                                 start_odd_win, start_odd_draw, start_odd_lose, start_persent_win, start_persent_draw, start_persent_lose,
                                 start_persent_return, start_kelly_win, start_kelly_draw, start_kelly_lose);
                SQLServerHelper.exe_sql(sql);


                //insert into table europe_new
                sql = "delete  from europe_new where start_time='{0}' and host='{1}' and client='{2}' and website='{3}'";
                sql = string.Format(sql, start_time, host, client,website);
                SQLServerHelper.exe_sql(sql);
                sql = " insert into europe_new " +
                     " (start_time,host,client,website,timespan," +
                     "  odd_win,odd_draw,odd_lose,persent_win,persent_draw,persent_lose,persent_return,kelly_win,kelly_draw,kelly_lose," +
                     "  start_odd_win,start_odd_draw,start_odd_lose,start_persent_win,start_persent_draw,start_persent_lose," +
                     "  start_persent_return,start_kelly_win,start_kelly_draw,start_kelly_lose)" +
                     "  values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}'," +
                     "          '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}')";
                sql = string.Format(sql, start_time, host, client, website, timespan,
                                 odd_win, odd_lose, odd_lose, persent_win, persent_draw, persent_lose, persent_return, kelly_win, kelly_draw, kelly_win,
                                 start_odd_win, start_odd_draw, start_odd_lose, start_persent_win, start_persent_draw, start_persent_lose,
                                 start_persent_return, start_kelly_win, start_kelly_draw, start_kelly_lose);
                SQLServerHelper.exe_sql(sql);
            }
        } 
        public DataTable get_table_from_excel(string filename, int sheet)
        {
            DataSet ds;


            string str_con = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                            "Extended Properties=Excel 8.0;" +
                            "data source=" + filename;
            OleDbConnection con = new OleDbConnection(str_con);


            con.Open();
            DataTable dt_schema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sql = " SELECT * FROM [" + dt_schema.Rows[sheet - 1][2].ToString() + "]";
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, con);
            ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            return ds.Tables[0];
        }

    }
}
