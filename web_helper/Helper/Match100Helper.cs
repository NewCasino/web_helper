using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.OleDb;
using System.Threading;
using mshtml;
using System.Reflection;
using System.Data;


 
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

        public static BsonDocument get_doc_result()
        {
            BsonDocument doc_result = new BsonDocument(); 
            doc_result.Add("data", "");
            doc_result.Add("loop", new BsonArray());
            return doc_result;
        }
    }
 
