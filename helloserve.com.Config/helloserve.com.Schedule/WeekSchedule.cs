using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Schedule
{
    public class WeekSchedule : BaseSchedule
    {
        public WeekSchedule()
            : base()
        {
        }

        public WeekSchedule(Config.ScheduleElement config)
            : base(config)
        {
        }

        protected override DateTime CalculateStartTime(DateTime utcMoment)
        {
            int today = (int)utcMoment.DayOfWeek;
            int nextRunDay = today;

            if (Days[nextRunDay] && utcMoment >= utcMoment.Date.Add(StartTime))
                nextRunDay++;

            if (nextRunDay > 6)
                nextRunDay = 0;

            while (!Days[nextRunDay])
            {
                nextRunDay++;

                if (nextRunDay > 6)
                    nextRunDay = 0;
            }

            int dayDiff = nextRunDay - today;
            if (dayDiff < 0)
                dayDiff = 7 + dayDiff;            
            if (dayDiff == 0 && utcMoment >= utcMoment.Date.Add(StartTime))
                dayDiff = 7;

            return (LastRun ?? utcMoment).Date.Add(StartTime).AddDays(dayDiff);
        }

        protected override void FromConfig(Config.ScheduleElement config)
        {
            if (config.Start == null)
                throw new System.Configuration.ConfigurationErrorsException("Expected 'start' element in config");

            if (config.DaysOfWeek == null)
                throw new System.Configuration.ConfigurationErrorsException("Expected 'days' element in config");

            StartTime = config.Start.Time;

            Days[0] = config.DaysOfWeek.Sunday;
            Days[1] = config.DaysOfWeek.Monday;
            Days[2] = config.DaysOfWeek.Tuesday;
            Days[3] = config.DaysOfWeek.Wednesday;
            Days[4] = config.DaysOfWeek.Thursday;
            Days[5] = config.DaysOfWeek.Friday;
            Days[6] = config.DaysOfWeek.Saturday;
        }

        public TimeSpan StartTime { get; set; }
        private bool[] _days = new bool[7];
        public bool[] Days
        {
            get { return _days; }
            set { _days = value; }
        }
    }
}
