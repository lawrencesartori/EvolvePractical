using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Extensions
{
    public static class DateTimeExtensions
    {
		public static DateTime GetStartOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		public static DateTime GetEndOfMonth(this DateTime date)
		{
			return GetStartOfMonth(date).AddMonths(1).AddDays(-1).BeforeMidnight();
		}

		public static DateTime BeforeMidnight(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
		}

		public static string ToDateFormatString(this DateTime date)
		{
			return date.ToString(Constants.DateFormat);
		}
	}
}
