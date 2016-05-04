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
        private DateTime startDate;
        private DateTime todayDate;
        private int duration;
        public bool countSaturday = false;
        public bool countSunday = false;
        public bool countHoliday = false;
        public bool countBadWeatherDay = false;
        public bool countBadConditionDay = false;


        ArrayList arrayBadWeatherDay = new ArrayList();
        ArrayList arrayBadConditionDay = new ArrayList();
        ArrayList arrayHoliday = new ArrayList();
        ArrayList arrayWorking = new ArrayList();

        public DayCompute(DateTime start, int dur)
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            this.startDate = start;
            this.duration = dur;

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

        ////增加放假日
        //public void AddHoliday(DateTime day)
        //{
        //    arrayHoliday.Add(day);
        //}

        ////清空放假日
        //public void ClearHoliday()
        //{
        //    arrayHoliday.Clear();
        //}

        ////增加補班日
        //public void AddWorking(DateTime day)
        //{
        //    arrayWorking.Add(day);
        //}

        ////清空補班日
        //public void ClearWorking()
        //{
        //    arrayWorking.Clear();
        //}

        //設定專案開始日
        public void SetStartDate(DateTime date)
        {
            startDate = date;
        }

        public void SetTodayDate(DateTime date)
        {
            todayDate = date;
        }

        public DateTime CountByDuration(DateTime start, int duration)
        {
            if (duration == 0)
                return start.Subtract(new TimeSpan(1,0,0,0));
            else
            {
                if (countSaturday == true && start.DayOfWeek == DayOfWeek.Saturday)//星期六不施工
                {
                    for (int i = 0; i < arrayWorking.Count; i++)
                    {
                        if (start.Date.Equals(((DateTime)arrayWorking[i]).Date))//原本星期六不施工 但剛好遇到補班日
                        {
                            return CountByDuration(start.AddDays(1), duration - 1);
                            //flag = true;
                            //break;
                        }
                    }
                        return CountByDuration(start.AddDays(1), duration);//星期六不施工
                }
                else if (countSunday == true && start.DayOfWeek == DayOfWeek.Sunday)//星期日不施工
                {
                    return CountByDuration(start.AddDays(1), duration);
                }
                else if (countHoliday == true)
                {
                    bool flag = false;
                    for (int i = 0; i < arrayHoliday.Count; i++)
                    {
                        if (start.Date.Equals(((DateTime)arrayHoliday[i]).Date))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if(flag == true)
                        return CountByDuration(start.AddDays(1), duration);//國定假日不施工
                    else
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

                if (countSaturday == true && start.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                {
                    count++;


                }
                else if (countSunday == true && start.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    count++;
                }
                else if (countHoliday == true)
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

        //public int CountByTotalDays(DateTime start, int days)
        //{
        //    return 0;
        //}

    }
}
