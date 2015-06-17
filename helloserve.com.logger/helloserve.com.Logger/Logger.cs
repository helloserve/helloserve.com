using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections.Concurrent;
using System.Data.Common;
using helloserve.com.Logger.Scribe;
using helloserve.com.Logger.Config;
using helloserve.com.Logger.Scribe.Config;

namespace helloserve.com.Logger
{
    internal class ALogElementComparer : IComparer<LogElement>
    {
        public int Compare(LogElement x, LogElement y)
        {
            return DateTime.Compare(x.Timestamp, y.Timestamp);
        }
    }

    public enum LogLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        Fatal = 4,
        Monitor = 10    //this should always be logged
    }

    /// <summary>
    /// The base class for any log entry accepted by the Logger class. Inherit and add your own properties that match the column names in your SQL table.
    /// </summary>
    public class LogElement
    {
        public LogElement()
        {
            Timestamp = DateTime.UtcNow;
        }

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
        /// The exception object to log
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// The level of the logging. Logging on a level includes all levels above it.
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// Returns a default size of about 10Kb. Override this method to compute the size of your log element.
        /// </summary>
        public virtual long Size()
        {
            int length = Category.Length + Message.Length;
            if (Exception != null)
                length += Exception.AsLogString().Length;

            return 16 + (length * 4);
        }

        /// <summary>
        /// Saves this log entry
        /// </summary>
        /// <param name="connection">The scribe object that will use the output</param>
        /// <returns>An object that contains information to use saving the log</returns>
        public virtual object Scribe(BaseScribe scribe)
        {
            return this;
        }

        /// <summary>
        /// Saves this log entry specifically using the SqlCommandScribe type
        /// </summary>
        /// <param name="scribe">The SqlCommandScribe instance that will use the output</param>
        /// <returns>The SqlCommand object to use</returns>
        public virtual SqlCommand Scribe(Scribe.SqlCommandScribe scribe)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@Level", Level.ToString());
            sqlParams[1] = new SqlParameter("@Timestamp", Timestamp);
            sqlParams[2] = new SqlParameter("@Category", Category);
            sqlParams[3] = new SqlParameter("@Message", Message);

            if (Exception != null)
                sqlParams[4] = new SqlParameter("@Exception", Exception.AsLogString());
            else
                sqlParams[4] = new SqlParameter("@Exception", DBNull.Value);

            string sql = "INSERT INTO [Log] (Level, Timestamp, Category, Message, Exception) VALUES (@Level, @Timestamp, @Category, @Message, @Exception)";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddRange(sqlParams);

            return command;
        }

        /// <summary>
        /// Saves this log entry specifically using the FileScribe type
        /// </summary>
        /// <param name="scribe">The FileScribe instance that will use the output</param>
        /// <returns>The line(s) of text to write to the log file</returns>
        public virtual string Scribe(Scribe.FileScribe scribe)
        {
            StringBuilder blr = new StringBuilder();
            blr.Append(Timestamp.ToString(System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat));
            blr.Append("    ");
            blr.Append(Level.ToString().PadRight(10));
            blr.Append(" ");
            blr.Append(Category.PadRight(20));
            blr.Append(Message);
            if (Exception != null)
            {
                blr.AppendLine(string.Empty);
                blr.AppendLine(Exception.AsLogString());
            }
            blr.AppendLine(string.Empty);
            return blr.ToString();
        }
    }

    /// <summary>
    /// A base performance monitoring log entry class derived from LogElement. Inherit from this class to implement a construtor to start the monitoring and the monitor method to gather the monitoring data.
    /// </summary>
    public abstract class PerfLogElement : LogElement
    {
        /// <summary>
        /// Override this method to implement your own custom monitoring code.
        /// </summary>
        public abstract void Monitor();
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
        /// Implements the calculation of the elapsed time from Initiated to current.
        /// </summary>
        public override void Monitor()
        {
            ElapsedSeconds = (DateTime.Now - Initiated).TotalSeconds;
        }

        /// <summary>
        /// Saves this log entry specifically using the SqlCommandScribe type
        /// </summary>
        /// <param name="scribe">The SqlCommandScribe instance that will use the output</param>
        /// <returns>The SqlCommand object to use</returns>
        public override SqlCommand Scribe(SqlCommandScribe scribe)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@Level", Level.ToString());
            sqlParams[1] = new SqlParameter("@Timestamp", Timestamp);
            sqlParams[2] = new SqlParameter("@Category", Category);
            sqlParams[3] = new SqlParameter("@Message", Message);

            if (Exception != null)
            {
                StringBuilder blr = new StringBuilder();
                Exception ex = Exception;
                blr.AppendLine(ex.Message);
                blr.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
                while (ex != null)
                {
                    blr.AppendLine("-- inner exception --");
                    blr.AppendLine(ex.Message);
                    blr.AppendLine(ex.StackTrace);
                    ex = ex.InnerException;
                    if (ex == null)
                        blr.AppendLine("-- end of inner exceptions --");
                }

                sqlParams[4] = new SqlParameter("@Exception", blr.ToString());
            }
            else
                sqlParams[4] = new SqlParameter("@Exception", DBNull.Value);

            sqlParams[5] = new SqlParameter("@Initiated", Initiated);
            sqlParams[6] = new SqlParameter("@ElapsedSeconds", ElapsedSeconds);

            string sql = "INSERT INTO [PerfLog] (Level, Timestamp, Category, Message, Exception, Initiated, ElapsedSeconds) VALUES (@Level, @Timestamp, @Category, @Message, @Exception, @Initiated, @ElapsedSeconds)";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddRange(sqlParams);

            return command;
        }
    }

    /// <summary>
    /// The main logger interface. Grab an instance through the GetLogger() static method and simply keep it alive.
    /// </summary>
    public class Logger : IDisposable
    {
        private static List<Logger> _loggers = new List<Logger>();
        internal static string LoggerConfigName = "logger";

        public void SetLoggerConfigName(string name)
        {
            LoggerConfigName = "logger";
        }

        /// <summary>
        /// Creates an instance of the Logger class using the configuration from the web or app config file.
        /// </summary>
        /// <param name="name">The name of the logger - used in the EventViewer logs for identification</param>
        /// <returns>An instance of the Logger class</returns>
        public static Logger GetLogger(string name)
        {
            LoggerConfig config = LoggerConfig.GetConfig();
            if (config == null)
                throw new ArgumentNullException("Could not load config");

            List<BaseScribe> scribes = new List<BaseScribe>();
            foreach (var scribe in config.Scribes)
            {
                scribes.Add((scribe as ScribeConfig).GetScribe());
            }

            Logger logger = new Logger(name, config.DumpInterval, config.ByteSizeLimit, scribes);
            _loggers.Add(logger);
            return logger;
        }

        /// <summary>
        /// Creates an instance of the Logger class for you to take care of. Each instance maintains it's own cache.
        /// </summary>
        /// <param name="name">The name of the logger - used in EventViewer logs for identification.</param>
        /// <param name="connectionString">The connection string for the permanent store (SQL Client only).</param>
        /// <param name="tableName">The table names in the permanent store to whichthe logger should write.</param>
        /// <param name="dumpInterval">The interval in seconds that the logger should attempt to dump to the permanent store. Optional, default is 30</param>
        /// <param name="byteSizeLimit">The size limit (in bytes) that the logger should attempt to stick to. Optional, default is around 5MB</param>
        /// <returns>An instance of the Logger class</returns>
        public static Logger GetLogger(string name, int dumpInterval = 30, long byteSizeLimit = 5242880)
        {
            Logger logger = new Logger(name, dumpInterval, byteSizeLimit, null);
            _loggers.Add(logger);
            return logger;
        }

        public static void EndLoggers()
        {
            foreach (Logger logger in _loggers)
            {
                logger.Dispose();
            }
        }

        #region CLOCK

        private bool _stop = false;
        private DateTime _lastClock;
        private bool _dumping = false;

        private void Clock(object state)
        {
            if (_stop)
                return;

            bool size = (_logCache.TotalSizeInBytes >= _byteLimit);
            bool busy = (_logCache.LogsPerMinutes > 120);

            if (!busy)
                Dump();

            _lastClock = DateTime.Now;
        }

        #endregion

        private static int _logKeySequence = 1;

        private Cache _logCache;
        private ConcurrentDictionary<int, PerfLogElement> _perfCache;
        private List<BaseScribe> _scribes;
        private string _logName;

        private int _dumpInterval;
        private long _byteLimit;

        private Timer _clock;

        Logger(string name, int dumpInterval, long byteLimit, List<BaseScribe> scribes)
        {
            _logName = name;

            _dumpInterval = dumpInterval;
            _byteLimit = byteLimit;

            _logCache = new Cache();
            _logCache.DumpNow += new EventHandler(_logCache_DumpNow);

            _perfCache = new ConcurrentDictionary<int, PerfLogElement>();

            _scribes = scribes;

            RestartClockThread();
        }

        private void RestartClockThread()
        {
            _clock = new Timer(new TimerCallback(Clock), null, 0, _dumpInterval);
        }

        void _logCache_DumpNow(object sender, EventArgs e)
        {
            Dump();
        }

        public void AddScribe(BaseScribe scribe)
        {
            if (_scribes == null)
                _scribes = new List<BaseScribe>();

            _scribes.Add(scribe);
        }

        #region STATE

        /// <summary>
        /// Start the internal thread of the logger used for checking states and dumping to the permanent store. This does not affect it's 
        /// </summary>
        public void Start()
        {
            _stop = false;
            RestartClockThread();
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
            get { return _stop; }
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
                LogError(_logName, ex, element);
                return false;
            }

            return true;
        }

        public bool Log(LogLevel level, string category, string message, Exception ex = null)
        {
            return Log(new LogElement() { Level = level, Category = category, Message = message, Exception = ex });
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
                int key = ++_logKeySequence;

                _perfCache.AddOrUpdate(key, element, (k, v) =>
                {
                    v = element;
                    return v;
                });

                return key;
            }
            catch (Exception ex)
            {
                LogError(_logName, ex, element);
                return -1;
            }
        }

        /// <summary>
        /// Starts a performance monitoring log entry using the default ElapsedLogElement type.
        /// </summary>
        /// <returns>A thread-based unique key that the consumer should use to stop and log the performance monitor.</returns>
        public int StartElapsedPerfLog(LogLevel level, string category, string message, Exception ex = null)
        {
            return StartPerfLog(new ElapsedLogElement() { Level = level, Category = category, Message = message, Exception = ex });
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
                try
                {
                    entry.Monitor();
                }
                catch (Exception ex)
                {
                    throw new ALogRethrowException("Exception in your implementation of 'PerfLogElement.Monitor()'", ex);
                }

                PerfLogElement element;
                _perfCache.TryRemove(key, out element);

                return Log(entry);

            }
            catch (ALogRethrowException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogError(_logName, ex, new LogElement() { Message = "Performance Log for key " + key });
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
                    LogWarning(_logName, string.Format("There are still {0} unmonitored performance log entries in the cache", _perfCache.Keys.Count), new LogElement() { Category = "PerfMon", Message = "Performance Monitoring cache is not empty", Timestamp = DateTime.Now });

                if (_logCache != null)
                {
                    _logCache.Dump(_scribes);
                }
            }
            catch (ALogRethrowException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LogError(_logName, ex, new LogElement() { Category = "HLog", Message = "Error dumping to permanent stores", Timestamp = DateTime.Now });
            }

            _dumping = false;
        }

        internal static void LogError(string logName, Exception ex, LogElement element)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(string.Format("HLog encountered an error for configuration '{0}':", logName));
                msg.AppendLine(string.Empty);
                msg.AppendLine(element.Message);
                msg.AppendLine(string.Empty);
                msg.AppendLine(ex.Message);
                msg.AppendLine("StackTrace:");
                msg.AppendLine(ex.StackTrace);

                System.Diagnostics.EventLog log = new System.Diagnostics.EventLog("Application");
                log.Source = "HLog - " + logName;
                log.WriteEntry(msg.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            catch { }
        }


        internal static void LogWarning(string logName, string message, LogElement element)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(string.Format("HLog issued a warning for configuration '{0}':", logName));
                msg.AppendLine(string.Empty);
                msg.AppendLine(element.Message);
                msg.AppendLine(string.Empty);
                msg.AppendLine(message);

                System.Diagnostics.EventLog log = new System.Diagnostics.EventLog("Application");
                log.Source = "HLog - " + logName;
                //log.ModifyOverflowPolicy(System.Diagnostics.OverflowAction.OverwriteAsNeeded, 0);
                log.WriteEntry(msg.ToString(), System.Diagnostics.EventLogEntryType.Warning);
            }
            catch { }
        }

        public void Dispose()
        {
            Dump();
            _clock.Dispose();
        }
    }
}
