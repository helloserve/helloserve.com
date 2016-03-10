using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace helloserve.com.Schedule
{
    public abstract class BaseSchedule : IDisposable
    {
        private DateTime? _lastRun;
        public DateTime? LastRun
        {
            get { return _lastRun; }
            private set { _lastRun = value; }
        }

        private DateTime _nextRun;
        public DateTime NextRun
        {
            get { return _nextRun; }
            private set { _nextRun = value; }
        }

        private Timer _threadTimer;

        public BaseSchedule()
        {
            _lastRun = null;
        }

        public BaseSchedule(Config.ScheduleElement config)
        {
            _lastRun = null;
            LoadFromConfig(config);
        }

        void _timerTick(object state)
        {
            _threadTimer.Dispose();

            if (Start != null)
                Start(this, EventArgs.Empty);

            LastRun = DateHelper.UtcNow;

            SetTimer();
        }

        public void LoadFromConfig(Config.ScheduleElement config)
        {
            FromConfig(config);
            SetTimer();
        }

        private void SetTimer()
        {
            DateTime utc = DateHelper.UtcNow;
            NextRun = CalculateStartTime(utc);            

            long period = (long)(NextRun - utc).TotalMilliseconds;
            if (period < 0)
                throw new InvalidOperationException("Cannot wait for a negative amount of time");
            if (period < (int)Math.Pow(2, 32) - 2)
                _threadTimer = new Timer(_timerTick, null, period, period);
        }

        public event EventHandler Start;

        protected abstract DateTime CalculateStartTime(DateTime utcMoment);        

        protected abstract void FromConfig(Config.ScheduleElement config);

        public virtual void Dispose()
        {
            _threadTimer.Dispose();
        }
    }
}
