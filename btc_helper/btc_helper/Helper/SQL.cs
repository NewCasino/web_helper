using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class SQL
{
    public static string get_value(string key)
    {
        string sql = "select * from [key] where k='{0}'";
        sql = string.Format(sql, key);
        return SQLServerHelper.get_table(sql).Rows[0]["v"].ToString();
    }
}



