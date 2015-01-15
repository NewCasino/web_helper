using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class UnixTime
{
    static DateTime start_date;
    static UnixTime()
    {
        start_date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    } 
    public static UInt32 unix_now { get { return get_unix_time(DateTime.UtcNow); } }
    public static UInt32 get_unix_time(DateTime d) { return (UInt32)(d - start_date).TotalSeconds; }
    public static DateTime get_utc_time(UInt32 unixtime) { return start_date.AddSeconds(unixtime); }
    public static DateTime get_local_time(UInt32 unixtime) { return start_date.AddSeconds(unixtime).AddHours(8); }
}