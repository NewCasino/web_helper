using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

/// <summary>
/// My Debug
/// </summary>
class IWindow
{
    public static string LINE = "-------------------------------------------------------------------------------------------------------------";
    public static void info (string msg)
    {
        Application.EnableVisualStyles();
        //Application.SetCompatibleTextRenderingDefault(false);
        frm_debug frm = new frm_debug(msg);
        Application.Run(frm); 
    }
    public static void write(string msg)
    {
        System.Diagnostics.Trace.Write(msg);
    }
    public static void write_line(string msg)
    {
        System.Diagnostics.Trace.WriteLine(msg);
    }
    public static void write_break()
    {
        System.Diagnostics.Trace.WriteLine(LINE);
    } 
    public static void write_content(string msg)
    {
        write_break();
        System.Diagnostics.Trace.WriteLine(msg);
        write_break();
    }
    public static void write_table(DataTable dt)
    { 
        IWindow.write(M.N);
        string str_column = "";
        foreach (DataColumn col in dt.Columns)
        {
            str_column = str_column + col.ColumnName.PR(10);
        }
        IWindow.write_line(str_column);

        IWindow.write_line(M.L(str_column.Length));
        foreach (DataRow row in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                IWindow.write(row[i].PR(10));
            }
            IWindow.write(M.N);
        }
        IWindow.write_line(M.L(str_column.Length));
    }
    public static string  help()
    {
        IWindow.write_break();
        IWindow.write_line("write()         --在即时窗体中显示一个华丽的字符串");
        IWindow.write_line("write_line()    --在即时窗体中显示一行华丽的字符串");
        IWindow.write_line("write_break()   --在即时窗体中显示一个华丽的分割线");
        IWindow.write_line("write_content() --在即时窗体中显示一块华丽的文本内容");
        IWindow.write_break();
        return "Help Information";
    }
}

