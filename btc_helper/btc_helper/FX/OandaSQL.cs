using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

class OandaSQL
{
    public static void insert_pairs(string pair, string display_name, string pip, string max_unit)
    {
        string sql = "delete from oanda_pairs where pair='{0}'";
        sql = string.Format(sql, pair);
        SQLServerHelper.exe_sql(sql);

        sql = "insert into oanda_pairs (pair,display_name,pip,max_unit) values ('{0}','{1}',{2},{3})";
        sql = string.Format(sql, pair, display_name, pip, max_unit);
        SQLServerHelper.exe_sql(sql);
    }
    public static string  get_all_pairs()
    { 
      
        string sql = "select distinct pair from oanda_pairs";
        DataTable dt = SQLServerHelper.get_table(sql);
        string[] list = new string[dt.Rows.Count];
        for (int i=0;i<dt.Rows.Count;i++)
        {

            list[i] = dt.Rows[i]["pair"].ToString(); 
        }
        return String.Join("%2C", list);

    }
}

