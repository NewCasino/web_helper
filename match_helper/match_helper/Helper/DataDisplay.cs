using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

 
    class DataDisplay
    {
        static bool is_black = false;
        static bool is_clear = false;
        public static void print(ref TextBox txt,StringBuilder sb)
        {
            if (is_black)
            {
                txt.ForeColor = Color.White;
                txt.BackColor = Color.Black; 
            }
            if (is_clear)
            {
                txt.Text = sb.PR200();
            }
            else
            { 
                txt.Text = sb.ToString();
            }
        }
    }
 
