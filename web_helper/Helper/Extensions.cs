using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

static class Extensions
{  
    public static string PR(this object o, int len)
    {
        if (o == null) return "  ".PadRight(len, ' ')+"  ";  
        if (string.IsNullOrEmpty(o.ToString())) return " ".PadRight(len, ' ')+"  ";

        string input = o.ToString(); 
        input=input.Replace(Environment.NewLine, " "); //替换换行符
        if (input.Length < len) input = input.PadRight(len, ' ');
        
 
        int count = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] >= 0x4e00 && input[i] <= 0x9fbb)
            {
                count = count + 2;
            }
            else
            {
                count = count + 1; 
            }
            if (count == len) return input.Substring(0, i+1) + "  ";
        }
        return "PR WRONG".PR(len);
    }
 
}