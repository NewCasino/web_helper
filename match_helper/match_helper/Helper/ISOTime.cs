using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
  static  class ISOTime
    { 
      public static string iso_now_short{ get { return get_iso_short_time(DateTime.Now); } }
      public static string iso_now_long { get { return get_iso_long_time(DateTime.Now); } }
      public static string get_iso_short_time(DateTime dt)
      {
          return dt.ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
      }
      public static string get_iso_long_time(DateTime dt)
      {
          return dt.ToString("o");
      }
      public  static DateTime get_time(string str)
      {
          DateTime dt;
          var sucessed = DateTime.TryParseExact(str, new string[] { @"yyyy-MM-dd\THH:mm:ss\Z", "o" }, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dt);
          return sucessed ? dt : DateTime.MinValue;
      } 
    }