﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.OleDb;
using Microsoft.VisualBasic;


class Tool
{
    public static DataTable get_table_from_excel(string filename, int sheet)
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

    public static string get_24h_from_12h(string str)
    {
        try
        {
            int h = Convert.ToInt32(str.Substring(0, 2));
            if (h < 12)
            {
                h = h + 12;
                return h.ToString("00") + str.Substring(2, 3);
            }
            return str;
        }
        catch (Exception error)
        {
            return error.Message;
        } 
    }
    public static string get_12m_from_eng(string str)
    {
        //一月：January 
        //二月：February
        //三月：March
        //四月：April
        //五月：May 
        //六月：June 
        //七月：July
        //八月：August 
        //九月：September 
        //十月：October 
        //十一月：November
        //十二月：December
        str = str.ToLower();
        if (str.Contains("jan")) return "01";
        if (str.Contains("feb")) return "02";
        if (str.Contains("mar")) return "03";
        if (str.Contains("apr")) return "04";
        if (str.Contains("may")) return "05";
        if (str.Contains("jun")) return "06";
        if (str.Contains("jul")) return "07";
        if (str.Contains("aug")) return "08";
        if (str.Contains("sep")) return "09";
        if (str.Contains("oct")) return "10";
        if (str.Contains("nov")) return "11";
        if (str.Contains("dec")) return "12";
        return "00";
    }
    public static DateTime get_time(string str)
    {
        //2014-03-24 00:00:00
        DateTime dt = Convert.ToDateTime(str);
        return dt;
    }
    public static DateTime get_time(string date, string time)
    {
        //date 08-01 time 03:30
        string str = DateTime.Now.Year.ToString() + "-" + date + " " + time + ":00";
        DateTime dt = Convert.ToDateTime(str);
        return dt;
    } 
    public static DateTime get_time_by_kind(DateTime dt, int kind)
    {
        //convert to  east +8
        return dt.AddHours(8-kind);
    }
    private static bool is_english(string str)
    {
        for (int i = 0; i < str.Length - 1; i++)
        {
            string item = str.Substring(i, 1);
            byte[] byte_len = System.Text.Encoding.Default.GetBytes(item);
            if (byte_len.Length == 2) { return false; }
        }
        return true;
    }
    
    public static string to_simple_chinese(string str)
    { 
        return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
    }
    public static string to_complex_chinese(string str)
    {
        return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
    }
    public static string drop_repeat(string str)
    {
        string result = "";
        string[] items = str.Split(new string[] { "●" }, StringSplitOptions.RemoveEmptyEntries);
        if (items.Length == 0)
        { 
            return str;
        }

        foreach (string item in items)
        {
            if (!result.Contains(item))
            {
                result = result + "●" + item;
            }
        }
        if (items.Length > 0)
        {
            result = result.Substring(1, result.Length - 1);
        }
        return result; 
    }
    public static string[] str_split(string str, string mark)
    { 
        string[] list  =str.ToString().Split(new string[] { mark }, StringSplitOptions.RemoveEmptyEntries);
        return list;
    }
}
