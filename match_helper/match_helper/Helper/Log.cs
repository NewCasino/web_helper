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
        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string type = "info";
        string sql = "insert into log (time,type,description,detail) values('{0}','{1}','{2}','{3}')";
        description = SQLServerHelper.format_sql_str(description);
        detail = SQLServerHelper.format_sql_str(detail);
        sql = string.Format(sql, time, type, description, detail);
        SQLServerHelper.exe_sql(sql);
    }
    public static void error(string description, Exception error)
    {
        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
        FileHelper.create_file(result_path + file_name, result);
    }
    public static void create_log_file(string pathname, string result)
    {
        string result_path = @"D:\log\" + pathname + @"\" + DateTime.Now.ToString("yyyyMMdd");
        if (!Directory.Exists(result_path)) Directory.CreateDirectory(result_path);
        result_path = result_path + @"\";

        Random random = new Random();
        string file_name = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + random.Next(100).ToString("000") + ".txt";
        FileHelper.create_file(result_path + file_name, result);

    }
    public static void create_log_file(string result)
    {
        string result_path = @"D:\log\" + "log" + @"\" + DateTime.Now.ToString("yyyyMMdd");
        if (!Directory.Exists(result_path)) Directory.CreateDirectory(result_path);
        result_path = result_path + @"\";

        Random random = new Random();
        string file_name = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + random.Next(100).ToString("000") + ".txt";
        FileHelper.create_file(result_path + file_name, result);
    }

    public static string get_log_file()
    {
        DirectoryInfo[] dir_infos = FileHelper.get_dirs_sort_by_name_desc("D:/log/log");
        if (dir_infos.Length > 0)
        {
            string path = dir_infos[0].FullName;
            FileInfo[] file_infos = FileHelper.get_files_sort_by_name_desc(path);
            if (file_infos.Length > 0)
            {
                return FileHelper.get_file(file_infos[0].FullName);
            }
        }
        return "NO FILE!!!";
    }
    public static string get_log_file(int count)
    {
        DirectoryInfo[] dir_infos = FileHelper.get_dirs_sort_by_name_desc("D:/log/log");
        if (dir_infos.Length > 0)
        {
            string path = dir_infos[0].FullName;
            FileInfo[] file_infos = FileHelper.get_files_sort_by_name_desc(path);
            if (file_infos.Length > count)
            {
                return FileHelper.get_file(file_infos[count].FullName);
            }
        }
        return "NO FILE!!!";
    }
    public static string get_temp_file(string name)
    {
        string path = @"D:\log\temp\" + name;
        return FileHelper.get_file(path);
    }
    public static void   create_temp_file(string name, string result)
    {
        string path = @"D:\log\temp\" + name;
        FileHelper.create_file(path, result);
    }
}
