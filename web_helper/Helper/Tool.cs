using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;


class Tool
{
    public static string get_24h_from_12h(string str)
    {
        try
        {
            int h = Convert.ToInt32(str.Substring(0, 2));
            h = h + 12;
            return h.ToString("00" + str.Substring(2, 3));
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
        return dt.AddHours(kind - 8);
    }

}
