using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaChun_DailyReport
{
    class Functions
    {
        public static string TransferDateTimeToSQL(DateTime dt)
        {
            int Year = dt.Year;
            int Month = dt.Month;
            int Day = dt.Day;
            return Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString();
        }

        public static string GetDateTimeValueSlash(DateTime dt)
        {
            int Year = dt.Year;
            int Month = dt.Month;
            int Day = dt.Day;
            return Year.ToString() + "/" + Month.ToString().PadLeft(2, '0') + "/" + Day.ToString().PadLeft(2, '0');
        }

        public static DateTime TransferSQLDateToDateTime(string SQLDate)
        {
            if (SQLDate == string.Empty || SQLDate == null)
                return DateTime.Today;
            SQLDate = SQLDate.Substring(0, SQLDate.IndexOf("上") - 1);
            int firstIndex = SQLDate.IndexOf("/");
            int secondIndex = SQLDate.IndexOf("/", firstIndex + 1);

            string Year = SQLDate.Substring(0, firstIndex);
            string Month = SQLDate.Substring(firstIndex + 1, secondIndex - firstIndex - 1);
            string Day = SQLDate.Substring(secondIndex + 1);
            string Date = Year + "/" + Month.PadLeft(2, '0') + "/" + Day.PadLeft(2, '0');

            return DateTime.ParseExact(Date, "yyyy'/'MM'/'dd", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string TransferSQLDateToDateOnly(string SQLDate)
        {
            SQLDate = SQLDate.Substring(0, SQLDate.IndexOf("上") - 1);
            int firstIndex = SQLDate.IndexOf("/");
            int secondIndex = SQLDate.IndexOf("/", firstIndex + 1);

            string Year = SQLDate.Substring(0, firstIndex);
            string Month = SQLDate.Substring(firstIndex + 1, secondIndex - firstIndex - 1);
            string Day = SQLDate.Substring(secondIndex + 1);
            string Date = Year + "/" + Month.PadLeft(2, '0') + "/" + Day.PadLeft(2, '0');

            return Date;
        }

        public static string ConvertNumberToThousandTypeDisplay(int number)
        {
            string numberStr = number.ToString();
            string newStr = numberStr;
            for (int i = 2; i < numberStr.Length; i += 3)
            {
                newStr.Insert(i, ",");
            }
                return "";
        }

        public static string ComputeDayOfWeek(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
                return "日";
            else if (date.DayOfWeek == DayOfWeek.Monday)
                return "一";
            else if (date.DayOfWeek == DayOfWeek.Tuesday)
                return "二";
            else if (date.DayOfWeek == DayOfWeek.Wednesday)
                return "三";
            else if (date.DayOfWeek == DayOfWeek.Thursday)
                return "四";
            else if (date.DayOfWeek == DayOfWeek.Friday)
                return "五";
            else if (date.DayOfWeek == DayOfWeek.Saturday)
                return "六";
            else
                return "錯誤";

        }
    }
}
