using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Schedule
{
    public class MinuteSchedule : BaseSchedule
    {
        public MinuteSchedule()
            : base()
        {

        }

        public MinuteSchedule(Config.ScheduleElement config)
            : base(config)
        {

        }

        protected override DateTime CalculateStartTime(DateTime utcMoment)
        {
            return (LastRun ?? utcMoment).AddMinutes(Occurance);
        }

        protected override void FromConfig(Config.ScheduleElement config)
        {
            if (config.Repeats == null)
                throw new System.Configuration.ConfigurationErrorsException("Expected 'repeats' element in config");

            Occurance = config.Repeats.Occurance;
        }

        public int Occurance { get; set; }
    }
}
