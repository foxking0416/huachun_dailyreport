using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HuaChun_DailyReport
{
    class DayCompute
    {
        //public DateTime date = new DateTime(2015, 11, 3);
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
            this.startDate = start;
            this.duration = dur;
        }

        public void AddHoliday(DateTime day)
        {
            arrayHoliday.Add(day);
        }

        public void AddWorking(DateTime day)
        {
            arrayWorking.Add(day);
        }

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


                if (countSaturday == true && start.DayOfWeek == DayOfWeek.Saturday)
                {
                    bool flag = false;
                    for (int i = 0; i < arrayWorking.Count; i++)
                    {
                        if (start.Date.Equals(((DateTime)arrayWorking[i]).Date))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == true)
                        return CountByDuration(start.AddDays(1), duration - 1);
                    else
                        return CountByDuration(start.AddDays(1), duration);
                    //return CountByDuration(start.AddDays(1), duration);
                }
                else if (countSunday == true && start.DayOfWeek == DayOfWeek.Sunday)
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
                        return CountByDuration(start.AddDays(1), duration);
                    else
                        return CountByDuration(start.AddDays(1), duration - 1);
                }
                else
                {
                    return CountByDuration(start.AddDays(1), duration - 1);
                }
            }
        }

        public int CountByFinishDay(DateTime start, DateTime finish)
        {
            int hours = finish.Subtract(start).Hours;
            int duration = finish.Subtract(start).Days + 1;
            if (hours > 20)
                duration++;

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

        public int CountByTotalDays(DateTime start, int days)
        {
            return 0;
        }

    }
}
