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
class Iw
{
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
}

