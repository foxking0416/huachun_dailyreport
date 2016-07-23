using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HuaChun_DailyReport
{
    class DayCompute
    {
        private string dbHost;
        private string dbUser;
        private string dbPass;
        private string dbName;
        private MySQL SQL;
        public bool restOnSaturday = false;
        public bool restOnSunday = false;
        public bool restOnHoliday = false;
        public bool countBadWeatherDay = false;
        public bool countBadConditionDay = false;


        private ArrayList arrayBadWeatherDay = new ArrayList();
        private ArrayList arrayBadConditionDay = new ArrayList();
        private ArrayList arrayNotWorkingMorning = new ArrayList();
        private ArrayList arrayNotWorkingAfternoon = new ArrayList();
        private ArrayList arrayHoliday = new ArrayList();
        private ArrayList arrayWorking = new ArrayList();

        public DayCompute()
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            string[] holidays = SQL.Read1DArray_SQL_Data("date", "holiday", "working = '1'");
            string[] extraWorkingday = SQL.Read1DArray_SQL_Data("date", "holiday", "working = '2'");
            for (int i = 0; i < holidays.Length; i++)
            {
                DateTime holiday = Functions.TransferSQLDateToDateTime(holidays[i]);
                arrayHoliday.Add(holiday);
            }

            for (int i = 0; i < extraWorkingday.Length; i++)
            {
                DateTime workingDay = Functions.TransferSQLDateToDateTime(extraWorkingday[i]);
                arrayWorking.Add(workingDay);
            }
        }

        public void AddNotWorking(DateTime date, int morningOrAfternoon)
        {
            if (morningOrAfternoon == 0)
                arrayNotWorkingMorning.Add(date);
            else if (morningOrAfternoon == 1)
                arrayNotWorkingAfternoon.Add(date);
        }

        public DateTime CountByDuration(DateTime start, float durationF)
        {
            int duration = (int)Math.Ceiling(durationF);

            if (duration == 0)
                return start.Subtract(new TimeSpan(1,0,0,0));
            else
            {
                if (restOnSaturday == true && start.DayOfWeek == DayOfWeek.Saturday)//每逢星期六不施工
                {
                    for (int i = 0; i < arrayWorking.Count; i++)
                    {
                        if (start.Date.Equals(((DateTime)arrayWorking[i]).Date))//原本星期六不施工 但剛好遇到補班日                    
                            return CountByDuration(start.AddDays(1), duration - 1);
                    }
                        return CountByDuration(start.AddDays(1), duration);//星期六不施工
                }
                else if (restOnSunday == true && start.DayOfWeek == DayOfWeek.Sunday)//星期日不施工
                {
                    return CountByDuration(start.AddDays(1), duration);
                }
                else if (restOnHoliday == true)//國定假日不施工
                {
                    for (int i = 0; i < arrayHoliday.Count; i++)
                    {
                        if (start.Date.Equals(((DateTime)arrayHoliday[i]).Date))
                            return CountByDuration(start.AddDays(1), duration);//遇到國定假日不施工
                    }
                    return CountByDuration(start.AddDays(1), duration - 1);//國定假日照常施工
                }
                else
                {
                    return CountByDuration(start.AddDays(1), duration - 1);
                }
            }
        }

        public int CountDurationByDate(DateTime start, DateTime today)
        {
            int duration = today.Date.Subtract(start.Date).Days + 1;

            int count = 0;
            for (int i = 0; i < duration; i++)
            {

                if (restOnSaturday == true && start.AddDays(i).DayOfWeek == DayOfWeek.Saturday)//每逢星期六不施工
                {
                    bool flag = true;
                    for (int j = 0; j < arrayWorking.Count; j++)
                    {
                        if (start.AddDays(i).Date.Equals(((DateTime)arrayWorking[j]).Date))//原本星期六不施工 但剛好遇到補班日   
                        {
                            flag = false;
                            break;
                        }
                    }
                    if(flag == true)
                        count++;


                }
                else if (restOnSunday == true && start.AddDays(i).DayOfWeek == DayOfWeek.Sunday)//每逢星期日不施工
                {
                    count++;
                }
                else if (restOnHoliday == true)
                {
                    for (int j = 0; j < arrayHoliday.Count; j++)
                    {
                        if (start.AddDays(i).Date.Equals(((DateTime)arrayHoliday[j]).Date))
                        {
                            count++;
                            break;
                        }
                    }
                }

            }

            int workingday = duration - count;

            return workingday;
        }

        public float CountTotalNotWorkingDay(DateTime start, DateTime today)
        {
            float daysOfNotWorking = 0;
            for (int i = 0; i < today.Subtract(start).Days + 1; i++)
            {
                if (restOnSaturday == true && start.AddDays(i).DayOfWeek == DayOfWeek.Saturday)//週休二日且遇到星期六
                {
                    bool flag = true;
                    for (int j = 0; j < arrayWorking.Count; j++)
                    {
                        if (start.AddDays(i).Date.Equals(((DateTime)arrayWorking[j]).Date))//原本星期六不施工 但剛好遇到補班日 
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        daysOfNotWorking += 1;
                        continue;
                    }
                    
                }

                if (restOnSunday == true && start.AddDays(i).DayOfWeek == DayOfWeek.Sunday)//週休一日或周休二日且遇到星期天
                {
                    daysOfNotWorking += 1;
                    continue;
                }

                if (restOnHoliday == true)//國定假日不施工
                {
                    bool flag = true;
                    for (int j = 0; j < arrayHoliday.Count; j++)
                    {
                        if (start.AddDays(i).Date.Equals(((DateTime)arrayHoliday[j]).Date))
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        daysOfNotWorking += 1;
                        continue;
                    }
                }

                //因故停工
                bool flagNotWorkingMorning = true;
                for (int j = 0; j < arrayNotWorkingMorning.Count; j++)
                {
                    if (start.AddDays(i).Date.Equals(((DateTime)arrayNotWorkingMorning[j]).Date))
                    {
                        flagNotWorkingMorning = false;
                        break;
                    }
                }
                if (!flagNotWorkingMorning)
                {
                    daysOfNotWorking += 0.5f;
                }

                bool flagNotWorkingAfternoon = true;
                for (int j = 0; j < arrayNotWorkingAfternoon.Count; j++)
                {
                    if (start.AddDays(i).Date.Equals(((DateTime)arrayNotWorkingAfternoon[j]).Date))
                    {
                        flagNotWorkingAfternoon = false;
                        break;
                    }
                }
                if (!flagNotWorkingAfternoon)
                {
                    daysOfNotWorking += 0.5f;
                }
            }

            return daysOfNotWorking;
        }

        public float CountNotWorkingDayWithoutEverydayCondition(DateTime start, DateTime today)
        {
            float daysOfNotWorking = 0;
            for (int i = 0; i < today.Subtract(start).Days + 1; i++)
            {
                if (restOnSaturday == true && start.AddDays(i).DayOfWeek == DayOfWeek.Saturday)//週休二日且遇到星期六
                {
                    bool flag = true;
                    for (int j = 0; j < arrayWorking.Count; j++)
                    {
                        if (start.AddDays(i).Date.Equals(((DateTime)arrayWorking[j]).Date))//原本星期六不施工 但剛好遇到補班日 
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        daysOfNotWorking += 1;
                        continue;
                    }

                }

                if (restOnSunday == true && start.AddDays(i).DayOfWeek == DayOfWeek.Sunday)//週休一日或周休二日且遇到星期天
                {
                    daysOfNotWorking += 1;
                    continue;
                }

                if (restOnHoliday == true)//國定假日不施工
                {
                    bool flag = true;
                    for (int j = 0; j < arrayHoliday.Count; j++)
                    {
                        if (start.AddDays(i).Date.Equals(((DateTime)arrayHoliday[j]).Date))
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        daysOfNotWorking += 1;
                        continue;
                    }
                }

            }

            return daysOfNotWorking;
        }

        public float CountNotWorkingDayOnlyByCondition()
        { return 0; }

        public float GetWorkingDayNonCounting(DateTime today)
        {
            if (restOnSaturday == true && today.DayOfWeek == DayOfWeek.Saturday)//週休二日且遇到星期六
            {
                for (int j = 0; j < arrayWorking.Count; j++)
                {
                    if (today.Date.Equals(((DateTime)arrayWorking[j]).Date))//原本星期六不施工 但剛好遇到補班日 
                    {
                        return 0;
                    }
                }


                return 1;
            }

            if (restOnSunday == true && today.DayOfWeek == DayOfWeek.Sunday)//週休一日或周休二日且遇到星期天
            {
                return 1;
            }

            if (restOnHoliday == true)//國定假日不施工
            {
                for (int j = 0; j < arrayHoliday.Count; j++)
                {
                    if (today.Date.Equals(((DateTime)arrayHoliday[j]).Date))
                    {
                        return 1;
                    }
                }

            }

            //因故停工
            float floatNotWorkingMorning = 0;
            for (int j = 0; j < arrayNotWorkingMorning.Count; j++)
            {
                if (today.Date.Equals(((DateTime)arrayNotWorkingMorning[j]).Date))
                {
                    floatNotWorkingMorning = 0.5f;
                    break;
                }
            }

            float floatNotWorkingAfternoon = 0;
            for (int j = 0; j < arrayNotWorkingAfternoon.Count; j++)
            {
                if (today.Date.Equals(((DateTime)arrayNotWorkingAfternoon[j]).Date))
                {
                    floatNotWorkingAfternoon = 0.5f;
                    break;
                }
            }
            return floatNotWorkingMorning + floatNotWorkingAfternoon;
        }

        public string GetCondition(DateTime today)
        {
            if (restOnSaturday == true && restOnSunday == true)//週休二日且遇到星期六或星期天
            {
                if (today.DayOfWeek == DayOfWeek.Sunday)
                    return "週休二日";

                if (today.DayOfWeek == DayOfWeek.Saturday)
                {
                    for (int j = 0; j < arrayWorking.Count; j++)
                    {
                        if (today.Date.Equals(((DateTime)arrayWorking[j]).Date))//原本星期六不施工 但剛好遇到補班日 
                        {
                            return SQL.Read_SQL_data("reason", "holiday", "date = '" + Functions.TransferDateTimeToSQL(today) + "'");
                        }
                    }

                    return "週休二日";
                }
            }
            
            if (restOnSaturday == false && restOnSunday == true)//週休一日且遇到星期天
            {
                if (today.DayOfWeek == DayOfWeek.Sunday)
                    return "週休一日";
            }

            if (restOnHoliday == true)//國定假日不施工
            {
                for (int j = 0; j < arrayHoliday.Count; j++)
                {
                    if (today.Date.Equals(((DateTime)arrayHoliday[j]).Date))
                    {
                        return SQL.Read_SQL_data("reason", "holiday", "date = '" + Functions.TransferDateTimeToSQL(today) + "'");
                    }
                }
            }


            return "";
        }

        //public int CountByTotalDays(DateTime start, int days)
        //{
        //    return 0;
        //}

    }
}
