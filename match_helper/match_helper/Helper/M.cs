using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// Mark
/// </summary>
class M
{
    public static string D = "●";
    public static string N = Environment.NewLine;
    public static string L(int count)
    {
        string result = "";
        for (int i = 0; i < count; i++)
        {
            result = result + "-"; 
        }
        return result + Environment.NewLine;
    }
}
