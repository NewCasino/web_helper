using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


class FileHelper
{
    public static DirectoryInfo[] get_dirs_sort_by_name_desc(string dir_name)
    {
        DirectoryInfo dir_info = new DirectoryInfo(dir_name);
        DirectoryInfo[] dir_infos = dir_info.GetDirectories();
        FileHelper.sort_dir_by_name_desc(ref dir_infos);
        return dir_infos; 
    }
    public static FileInfo[] get_files_sort_by_name_desc(string dir_name)
    {
        DirectoryInfo dir_info = new DirectoryInfo(dir_name);
        FileInfo[] file_infos = dir_info.GetFiles();
        FileHelper.sort_file_by_name_desc(ref file_infos);
        return file_infos;
    }
    public static string get_file(string path)
    {
        string result = "";
        FileStream stream = File.Open(path, FileMode.Open);
        StreamReader reader = new StreamReader(stream);
        result = reader.ReadToEnd();
        reader.Close();
        return result;
    }
    public static void create_file(string path, string result)
    {
        FileStream stream = (FileStream)File.Open(path, FileMode.Create);
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(result);
        writer.Close();
        stream.Close();

    }


    private  static void sort_file_by_name_desc(ref FileInfo[] infos)
    {
        Array.Sort(infos, delegate(FileInfo x, FileInfo y) { return y.Name.CompareTo(x.Name); });
    }
    private static void sort_file_by_modify_create_time(ref FileInfo[] arrFi)
    {
        Array.Sort(arrFi, delegate(FileInfo x, FileInfo y) { return x.CreationTime.CompareTo(y.CreationTime); });
    } 
    private static void sort_dir_by_name_desc(ref DirectoryInfo[] dirs)
    {
        Array.Sort(dirs, delegate(DirectoryInfo x, DirectoryInfo y) { return y.Name.CompareTo(x.Name); });
    }
    private static void sort_dir_by_create_time(ref DirectoryInfo[] dirs)
    {
        Array.Sort(dirs, delegate(DirectoryInfo x, DirectoryInfo y) { return x.CreationTime.CompareTo(y.CreationTime); });
    }



}

