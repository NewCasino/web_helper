using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Extensions
{  
    public static string PR(this object o, int len)
    {
        if (o == null) return "  ".PR(len);

        string input = o.ToString();   
        input=input.Replace(Environment.NewLine, " "); //替换换行符
        if (input.Length > len) input = input.Substring(0, len); //长度大于索取长度时,截取

        input = input.PadRight(len, ' ');
        int count = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] >= 0x4e00 && input[i] <= 0x9fbb)
            {
                count = count + 1;
            }
        }
        return input.Substring(0, len - count)+"  ";
    }
}