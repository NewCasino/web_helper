using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


class Tool
{
    public static string pad(string input, int len)
    {
        input=input.PadRight(len , ' ');
        int count = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] >= 0x4e00 && input[i] <= 0x9fbb)
            {
                count = count + 1;
            } 
        }
        return input.Substring(0, len - count);
    }

}
