using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data.SqlClient;

namespace helloserve.com
{
    internal class ALogElementComparer : IComparer<LogElement>
    {
        public int Compare(LogElement x, LogElement y)
        {
            return DateTime.Compare(x.Timestamp, y.Timestamp);
        }
    }

    /// <summary>
    /// The base class for any log entry accepted by the Logger class. Inherit and add your own properties that match the column names in your SQL table.
    /// </summary>
    public class LogElement
    {
        /// <summary>
        /// The time stamp of your log entry.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// A specific, identifiable string used to cateogorize log entries.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// A general message for the log element, in addition to your custom fields. This field is used internally for EventViewer log entries as well.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Returns a default size of about 10Kb. Override this method to compute the size of your log element.
        /// </summary>
        public virtual long Size()
        {
            return 16 + 2000 + 8000;
        }

        /// <summary>
        /// Override this method to customize the fill of the parameters from your own properties that will be used in the dynamic SQL generation. Parameter names must correspond to table column names.
        /// The default implementation does nothing special.
        /// </summary>
        /// <param name="parameters">A dictionary containing all the parameters.</param>
        public virtual void FillParams(Dictionary<string, object> parameters)
        {
        }
    }

    /// <summary>
    /// A base performance monitoring log entry class derived from LogElement. Inherit from this class to implement a construtor to start the monitoring and the monitor method to gather the monitoring data.
    /// </summary>
    public class PerfLogElement : LogElement
    {
        /// <summary>
        /// Override this method to implement your own custom monitoring code.
        /// </summary>
        public virtual void Monitor()
        {
        }
    }

    /// <summary>
    /// A special element used for logging elapsed time based performance entries. Inherits from PerfLogElement, but implements two additional properties and a DateTime-based monitoring mechanism.
    /// </summary>
    public class ElapsedLogElement : PerfLogElement
    {
        /// <summary>
        /// The time when the performance monitoring was initiated.
        /// </summary>
        public DateTime Initiated;
        /// <summary>
        /// The duration of the monitoring, in seconds including partial.
        /// </summary>
        public double ElapsedSeconds;

        public ElapsedLogElement()
        {
            Initiated = DateTime.Now;
        }

        public override long Size()
        {
            return base.Size() + 16 + 4;
        }

        /// <summary>
        /// This override implements filling the two additional properties 'Initiated' and 'ElapsedSeconds'.
        /// </summary>
        /// <param name="parameters">A dictionary containing all the parameters.</param>
        public override void FillParams(Dictionary<string, object> parameters)
        {
            parameters["Initiated"] = Initiated;
            parameters["ElapsedSeconds"] = ElapsedSeconds;
        }

        /// <summary>
        /// Implements the calculation of the elapsed time from Initiated to current.
        /// </summary>
        public override void Monitor()
        {
            ElapsedSeconds = (DateTime.Now - Initiated).TotalSeconds;
        }
    }

    /// <summary>
    /// The main logger interface. Grab an instance through the GetLogger() static method and simply keep it alive.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Creates an instance of the Logger class for you to take care of. Each instance maintains it's own cache.
        /// </summary>
        /// <param name="name">The name of the logger - used in EventViewer logs for identification.</param>
        /// <param name="connectionString">The connection string for the permanent store (SQL Client only).</param>
        /// <param name="tableName">The table names in the permanent store to whichthe logger should write.</param>
        /// <param name="dumpInterval">The interval in seconds that the logger should attempt to dump to the permanent store. Optional, default is 30</param>
        /// <param name="byteSizeLimit">The size limit (in bytes) that the logger should attempt to stick to. Optional, default is around 5MB</param>
        /// <returns>An instance of the Logger class</returns>
        public static Logger GetLogger(string name, string connectionString, string tableName, int dumpInterval = 30, long byteSizeLimit = 5242880)
        {
            return new Logger(name, connectionString, tableName, dumpInterval, byteSizeLimit);
        }

        #region THREADING

        private bool _stop = false;
        private DateTime _lastClock;
        private bool _dumping = false;

        private void Clock(object state)
        {
            while (true)
            {
                TimeSpan elapsed = DateTime.Now - _lastClock;
                bool interval = (elapsed.TotalSeconds > _dumpInterval);
                bool size = (_logCache.TotalSizeInBytes >= _byteLimit);
                bool busy = (_logCache.LogsPerMinutes > 120);

                if (interval || (_stop) || size)
                {
                    if (_stop || size || (interval && !busy))
                        Dump();

                    if (_stop)
                        break;

                    _lastClock = DateTime.Now;
                }

                //just rest for a while
                Thread.Sleep(_dumpInterval * 1000);
            }
        }

        #endregion

        private Cache _logCache;
        private Dictionary<int, PerfLogElement> _perfCache;
        private string _logName;
        private string _connectionString;
        private string _tableName;

        private int _dumpInterval;
        private long _byteLimit;

        private Thread _clockThread;

        Logger(string name, string connectionString, string tableName, int dumpInterval, long byteLimit)
        {
            _logName = name;
            _connectionString = connectionString;
            _tableName = tableName;

            _dumpInterval = dumpInterval;
            _byteLimit = byteLimit;

            _logCache = new Cache();
            _logCache.DumpNow += new EventHandler(_logCache_DumpNow);

            _perfCache = new Dictionary<int, PerfLogElement>();

            RestartClockThread();
        }

        private void RestartClockThread()
        {
            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(Clock);
            _clockThread = new Thread(threadStart);
            _clockThread.Start();
        }

        void _logCache_DumpNow(object sender, EventArgs e)
        {
            Dump();
        }

        #region STATE

        /// <summary>
        /// Start the internal thread of the logger used for checking states and dumping to the permanent store. This does not affect it's 
        /// </summary>
        public void Start()
        {
            _stop = false;
            if (_clockThread != null && _clockThread.ThreadState != ThreadState.Running)
            {
                try
                {
                    _clockThread.Start();
                }
                catch
                {
                    RestartClockThread();
                }
            }
        }

        /// <summary>
        /// Stop the internal thread of the logger used for checking states and dumping to the permanent store. This does not affect it's ability to accept log entries.
        /// </summary>
        public void Stop()
        {
            _stop = true;
        }

        /// <summary>
        /// Indicates if the internal thread is stopped or not.
        /// </summary>
        public bool IsStopped
        {
            get
            {
                if (_clockThread == null)
                    return true;

                return _clockThread.ThreadState == ThreadState.Stopped;
            }
        }

        #endregion

        /// <summary>
        /// Use this method to log your program events.
        /// </summary>
        /// <param name="element">An instance of or derived from LogElement to log.</param>
        /// <returns>Returns whether logging was successful.</returns>
        public bool Log(LogElement element)
        {
            ////we can't log this entry since we're busy, so it will get lost
            //if (_dumping)
            //{
            //    LogWarning("Busy writing to permanent store; could not log entry", element);
            //    return false;
            //}

            try
            {
                if (_logCache == null)
                    return false;

                _logCache.PushElement(element);

            }
            catch (ALogRethrowException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LogError(ex, element);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Starts a performance monitoring log entry using the default ElapsedLogElement type.
        /// </summary>
        /// <returns>A thread-based unique key that the consumer should use to stop and log the performance monitor.</returns>
        public int StartElapsedPerfLog(string message)
        {
            return StartPerfLog(new ElapsedLogElement() { Category = "Elapsed Time", Message = message });
        }

        /// <summary>
        /// Starts a performance monitoring log entry.
        /// </summary>
        /// <param name="element">An initiated ElapsedLogElement entry.</param>
        /// <returns>A thread-based unique key that the consumer should use to stop and log the performance monitor.</returns>
        public int StartPerfLog(PerfLogElement element)
        {
            try
            {
                int key = Thread.CurrentThread.ManagedThreadId;

                _perfCache.Add(key, element);

                return key;
            }
            catch (Exception ex)
            {
                LogError(ex, element);
                return -1;
            }
        }

        /// <summary>
        /// Stops and logs the performance monitor using the given key.
        /// </summary>
        /// <param name="key">The key served by starting the performance monitor.</param>
        /// <returns>Returns wether the logging was succesfull.</returns>
        public bool LogPerfLog(int key)
        {
            try
            {
                if (!_perfCache.ContainsKey(key))
                    return false;

                PerfLogElement entry = _perfCache[key];
                entry.Timestamp = DateTime.Now;
                try
                {
                    entry.Monitor();
                }
                catch (Exception ex)
                {
                    throw new ALogRethrowException("Exception in your implementation of 'PerfLogElement.Monitor()'", ex);
                }

                _perfCache.Remove(key);

                return Log(entry);

            }
            catch (ALogRethrowException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex, new LogElement() { Message = "Performance Log for key " + key });
                return false;
            }
        }

        /// <summary>
        /// Force the logger to dump immediately. Typically use this when stopping your program or service, to make sure that all entries are written to the permanent store.
        /// </summary>
        public void Dump()
        {

            if (_dumping)
                return;

            _dumping = true;

            try
            {
                if (_perfCache != null && _perfCache.Keys.Count > 0)
                    LogWarning(string.Format("There are still {0} unmonitored performance log entries in the cache", _perfCache.Keys.Count), new LogElement() { Category = "PerfMon", Message = "Performance Monitoring cache is not empty", Timestamp = DateTime.Now });

                if (_logCache != null)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        _logCache.Dump(connection, _tableName);
                    }
                }
            }
            catch (ALogRethrowException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LogError(ex, new LogElement() { Category = "ALog", Message = string.Format("Error dumping to permanent store at '{0}', table '{1}'", _connectionString, _tableName), Timestamp = DateTime.Now });
            }

            _dumping = false;
        }

        private void LogError(Exception ex, LogElement element)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(string.Format("Alog encountered an error for configuration '{0}':", _logName));
                msg.AppendLine(string.Empty);
                msg.AppendLine(element.Message);
                msg.AppendLine(string.Empty);
                msg.AppendLine(ex.Message);
                msg.AppendLine("StackTrace:");
                msg.AppendLine(ex.StackTrace);

                System.Diagnostics.EventLog log = new System.Diagnostics.EventLog("Application");
                log.Source = "ALog - " + _logName;
                log.WriteEntry(msg.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            catch { }
        }


        private void LogWarning(string message, LogElement element)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(string.Format("Alog issued a warning for configuration '{0}':", _logName));
                msg.AppendLine(string.Empty);
                msg.AppendLine(element.Message);
                msg.AppendLine(string.Empty);
                msg.AppendLine(message);

                System.Diagnostics.EventLog log = new System.Diagnostics.EventLog("Application");
                log.Source = "ALog - " + _logName;
                //log.ModifyOverflowPolicy(System.Diagnostics.OverflowAction.OverwriteAsNeeded, 0);
                log.WriteEntry(msg.ToString(), System.Diagnostics.EventLogEntryType.Warning);
            }
            catch { }
        }
    }
}
