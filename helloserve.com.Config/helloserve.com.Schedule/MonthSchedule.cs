using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Schedule
{
    public class MonthSchedule : BaseSchedule
    {
        public MonthSchedule()
            : base()
        {
        }

        public MonthSchedule(Config.ScheduleElement config)
            : base(config)
        {
        }

        protected override DateTime CalculateStartTime(DateTime utcMoment)
        {
            DateTime thisMonth = new DateTime(utcMoment.Year, utcMoment.Month, 1);            
            DateTime nextMonth = thisMonth.AddMonths(1);
            int daySpan = (nextMonth - thisMonth).Days;
            
            int day = Math.Min(Day, daySpan) - 1;
            DateTime startTime = thisMonth.AddDays(day).Add(StartTime);

            if (utcMoment.Day > startTime.Day || utcMoment.TimeOfDay > startTime.TimeOfDay)
                startTime = startTime.AddMonths(1);

            return startTime;
        }

        protected override void FromConfig(Config.ScheduleElement config)
        {
            if (config.Start == null)
                throw new System.Configuration.ConfigurationErrorsException("Expected 'start' element in config");

            if (config.DayOfMonth == null)
                throw new System.Configuration.ConfigurationErrorsException("Expected 'day' element in config");

            StartTime = config.Start.Time;

            if (config.DayOfMonth.FirstDay && config.DayOfMonth.LastDay)
                throw new System.Configuration.ConfigurationErrorsException("Cannot schedule for both the start and end of the month");

            if (!(config.DayOfMonth.FirstDay || config.DayOfMonth.LastDay))
            {
                if (config.DayOfMonth.DayNumber == 0)
                    throw new System.Configuration.ConfigurationErrorsException("Could not determine day of month");

                Day = config.DayOfMonth.DayNumber;
            }
            else
            {
                if (config.DayOfMonth.FirstDay)
                    Day = 1;
                else if (config.DayOfMonth.LastDay)
                    Day = 31;
            }
        }

        public int Day { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
