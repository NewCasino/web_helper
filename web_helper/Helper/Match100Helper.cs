using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.OleDb;
using System.Threading;
using mshtml;
using System.Reflection;
using System.Data;



class Match100Helper
{
    public static bool is_odd_str(string str)
    {
        if (str.Contains(".") == false) return false;

        double output = 0;
        if (double.TryParse(str, out output) == false) return false;

        return true;
    }
    public static bool is_double_str(string str)
    {
        double output = 0;
        if (double.TryParse(str, out output) == false) return false;
        return true;
    }
    public static BsonDocument get_doc_result()
    {
        BsonDocument doc_result = new BsonDocument();
        doc_result.Add("data", "");
        doc_result.Add("loop", new BsonArray());
        return doc_result;
    }
    public void insert_data(string company, string type, string start_time, string host, string client, string odd_win, string odd_draw, string odd_lose)
    {
        string sql = "";
        string timespan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        sql = " insert into europe_100_log " +
           " (start_time,host,client,company,timespan," +
           "  profit_win,profit_draw,profit_lose,persent_win,persent_draw,persent_lose,persent_return,kelly_win,kelly_draw,kelly_lose," +
           "  start_profit_win,start_profit_draw,start_profit_lose,start_persent_win,start_persent_draw,start_persent_lose," +
           "  start_persent_return,start_kelly_win,start_kelly_draw,start_kelly_lose,type)" +
           "  values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}'," +
           "          '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}')";
        sql = string.Format(sql, start_time, host, client, company, timespan,
                         odd_win, odd_draw, odd_lose, "", "", "", "", "", "", "",
                         "", "", "", "", "", "",
                         "", "", "", "", type);
        SQLServerHelper.exe_sql(sql);
    }
    public static DataTable get_match_table(DataTable dt_analyse)
    {
        DataTable dt = new DataTable();
        ArrayList cols = new ArrayList();

        if (dt_analyse.Rows.Count < 2) return dt;
        int count_row = dt_analyse.Rows.Count - 2;
        int text_row = dt_analyse.Rows.Count - 1;

        for (int i = 3; i < dt_analyse.Columns.Count; i++)
        {
            string[] text_list = dt_analyse.Rows[text_row][i].ToString().Replace(" ","").Trim().Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
            if (Convert.ToInt32(dt_analyse.Rows[count_row][i].ToString()) > 5 && text_list.Length > 5)
            {
                cols.Add(dt_analyse.Columns[i].ColumnName);
            }
        }

        foreach (string str in cols)
        {
            dt.Columns.Add(str);
        }

        for (int i = 0; i < dt_analyse.Rows.Count - 2; i++)
        {
            DataRow row_new = dt.NewRow();
            bool is_use = false;
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                
                
                string column_name = dt.Columns[j].ColumnName;
                if (string.IsNullOrEmpty(dt_analyse.Rows[i][column_name].ToString().Replace(" ", "").Trim())) continue;

                is_use = true;
                string text = "";
                string value = dt_analyse.Rows[i][column_name].ToString();
                string[] list = value.Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in list)
                {
                    text = text + item + "●";
                }
                if (text.Length > 1) text = text.Substring(0, text.Length - 1);
                row_new[column_name] = text;

            }
            if (is_use == true) dt.Rows.Add(row_new);
        }
        return dt;
    }
}

