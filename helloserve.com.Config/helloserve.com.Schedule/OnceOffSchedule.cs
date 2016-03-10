using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Schedule
{
    public class OnceOffSchedule : BaseSchedule
    {
        public OnceOffSchedule()
            : base()
        {

        }

        public OnceOffSchedule(Config.ScheduleElement config)
            : base(config)
        {

        }

        protected override DateTime CalculateStartTime(DateTime utcMoment)
        {
            if (LastRun != null)
                return DateTime.MaxValue;
            else if (StartTime.HasValue)
            {
                DateTime start = DateHelper.Today.Add(StartTime.Value);
                if (start < DateHelper.Now)
                    return start;

                return DateHelper.Today.Date.Add(StartTime.Value);
            }
            else
            {
                return DateHelper.UtcNow;
            }
        }

        protected override void FromConfig(Config.ScheduleElement config)
        {
            if (config.Start != null && config.Start.Time != TimeSpan.Zero)
                StartTime = config.Start.Time;
        }

        public TimeSpan? StartTime;
    }
}
