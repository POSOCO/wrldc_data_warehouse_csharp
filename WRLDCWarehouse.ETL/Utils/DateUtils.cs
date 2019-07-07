using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.ETL.Utils
{
    public static class DateUtils
    {
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
