using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Schedule
{
    public class DaySchedule : BaseSchedule
    {
        public DaySchedule()
            : base()
        {

        }

        public DaySchedule(Config.ScheduleElement config)
            : base(config)
        {

        }

        protected override DateTime CalculateStartTime(DateTime utcMoment)
        {
            if (LastRun == null)
            {
                DateTime start = DateHelper.Today.Add(StartTime);
                if (start < DateHelper.Now)
                    return start;
            }

            return DateHelper.Today.Date.Add(StartTime).AddDays(Occurance);
        }

        protected override void FromConfig(Config.ScheduleElement config)
        {
            if (config.Repeats == null)
                throw new System.Configuration.ConfigurationErrorsException("Expected 'repeats' element in config");

            if (config.Start == null)
                throw new System.Configuration.ConfigurationErrorsException("Expected 'start' element in config");

            Occurance = config.Repeats.Occurance;
            StartTime = config.Start.Time;
        }

        public int Occurance { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
