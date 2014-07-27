using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 
    class Match100Helper
    {
        public static bool is_odd_str(string str)
        { 
            if (str.Contains(".") == false) return false;

            double output = 0;
            if (double.TryParse(str, out output) == false) return false;   

            return true;
        }
        public static bool is_double_str(string str)
        { 
            double output = 0;
            if (double.TryParse(str, out output) == false) return false;
            return true;
        }
    }
 
