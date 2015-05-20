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
    public static UInt64 unix_now { get { return get_unix_time(DateTime.UtcNow); } }
    public static UInt64 get_unix_time(DateTime d) { return (UInt64)(d - start_date).TotalSeconds; }
    public static UInt64 get_unix_time_from_local(DateTime d) { return (UInt64)(d.AddHours(-8) - start_date).TotalSeconds; }
    public static DateTime get_utc_time(UInt64 unixtime) { return start_date.AddSeconds(unixtime); }
    public static DateTime get_local_time(UInt64 unixtime) { return start_date.AddSeconds(unixtime).AddHours(8); }

    public static UInt64 unix_now_long { get { return get_unix_time_long(DateTime.UtcNow); } }
    public static UInt64 get_unix_time_long(DateTime d) { return (UInt64)(d - start_date).TotalMilliseconds; }
    public static UInt64 get_unix_time_from_local_long(DateTime d) { return (UInt64)(d.AddHours(-8) - start_date).TotalMilliseconds; }
    public static DateTime get_utc_time_long(UInt64 unixtime) { return start_date.AddMilliseconds(unixtime); }
    public static DateTime get_local_time_long(UInt64 unixtime) { return start_date.AddMilliseconds(unixtime).AddHours(8); }
}