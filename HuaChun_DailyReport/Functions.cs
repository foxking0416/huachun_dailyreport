using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaChun_DailyReport
{
    class Functions
    {


        public static string GetDateTimeValue(DateTime dt)
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
    }
}
