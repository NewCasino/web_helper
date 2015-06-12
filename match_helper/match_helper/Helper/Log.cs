using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

public class Log
{
    public static void info(string description, string detail)
    {
        string time = DateTime.Now.ToString();
        string type = "info";
        string sql = "insert into log (time,type,description,detail) values('{0}','{1}','{2}','{3}')";
        description = SQLServerHelper.format_sql_str(description);
        detail = SQLServerHelper.format_sql_str(detail);
        sql = string.Format(sql, time, type, description, detail);
        SQLServerHelper.exe_sql(sql);
    }
    public static void error(string description, Exception error)
    {
        string time = DateTime.Now.ToString();
        string type = "error";
        string detail = error.ToString();

        description = SQLServerHelper.format_sql_str(description);
        detail = SQLServerHelper.format_sql_str(detail);

        string sql = "insert into log (time,type,description,detail) values('{0}','{1}','{2}','{3}')";
        sql = string.Format(sql, time, type, description, detail);
        SQLServerHelper.exe_sql(sql);
    }
    public static void error_with_msg(string description, string error_msg, Exception error)
    {
        string time = DateTime.Now.ToString();
        string type = "error";
        string detail = error_msg + Environment.NewLine + error.ToString();

        description = SQLServerHelper.format_sql_str(description);
        detail = SQLServerHelper.format_sql_str(detail);

        string sql = "insert into log (time,type,description,detail) values('{0}','{1}','{2}','{3}')";
        sql = string.Format(sql, time, type, description, detail);
        SQLServerHelper.exe_sql(sql);
    }

 
    public static void create_log_file(string pathname, string name, string result)
    {
        string result_path = @"D:\log\" + pathname + @"\" + DateTime.Now.ToString("yyyyMMdd");
        if (!Directory.Exists(result_path)) Directory.CreateDirectory(result_path);
        result_path = result_path + @"\";

        Random random = new Random();
        string file_name = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + random.Next(100).ToString("000") + "_" + name + ".txt";
        FileStream stream = (FileStream)File.Open(result_path + file_name, FileMode.Create);
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(result);
        writer.Close();
        stream.Close();

    }
    public static void create_log_file(string pathname, string result)
    {
        string result_path = @"D:\log\" + pathname + @"\" + DateTime.Now.ToString("yyyyMMdd");
        if (!Directory.Exists(result_path)) Directory.CreateDirectory(result_path);
        result_path = result_path + @"\";

        Random random = new Random();
        string file_name = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + random.Next(100).ToString("000") + ".txt";
        FileStream stream = (FileStream)File.Open(result_path + file_name, FileMode.Create);
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(result);
        writer.Close();
        stream.Close();

    }
    public static void create_log_file(string result)
    {
        string result_path = @"D:\log\" + "temp" + @"\" + DateTime.Now.ToString("yyyyMMdd");
        if (!Directory.Exists(result_path)) Directory.CreateDirectory(result_path);
        result_path = result_path + @"\";

        Random random = new Random();
        string file_name = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + random.Next(100).ToString("000") + ".txt";
        FileStream stream = (FileStream)File.Open(result_path + file_name, FileMode.Create);
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(result);
        writer.Close();
        stream.Close();
    }


}
