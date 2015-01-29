using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using helloserve.com;

namespace helloserve.Common
{
    public class EventLogEntry : ElapsedLogElement
    {
        public string Source { get; set; }

        public int? UserID { get; set; }
        public int? FeatureID { get; set; }
        public int? NewsID { get; set; }
        public int? MediaID { get; set; }
        public int? DownloadID { get; set; }

        public override long Size()
        {
            return base.Size() + 2000 + 4 + 4 + 4 + 4 + 4;
        }

        public override void FillParams(Dictionary<string, object> parameters)
        {
            base.FillParams(parameters);

            if (Category != "Elapsed")
            {
                parameters["Initiated"] = null;
                parameters["ElapsedSeconds"] = null;
            }

            parameters["UserID"] = UserID;
            parameters["FeatureID"] = FeatureID;
            parameters["NewsID"] = NewsID;
            parameters["MediaID"] = MediaID;
            parameters["DownloadID"] = DownloadID;
        }

        public EventLogEntry()
        {
            Timestamp = DateTime.Now;
        }

        public static EventLogEntry LogForFeature(int? userID, int featureID, string message, string source)
        {
            return new EventLogEntry() { Category = "Feature", UserID = userID, FeatureID = featureID, Message = message, Source = source };
        }

        public static EventLogEntry LogForNews(int? userID, int newsID, string message, string source)
        {
            return new EventLogEntry() { Category = "News", UserID = userID, NewsID = newsID, Message = message, Source = source };
        }

        public static EventLogEntry LogForMedia(int? userID, int mediaID, string message, string source)
        {
            return new EventLogEntry() { Category = "Media", UserID = userID, MediaID = mediaID, Message = message, Source = source };
        }

        public static EventLogEntry LogForDownload(int? userID, int downloadID, string message, string source)
        {
            return new EventLogEntry() { Category = "Download", UserID = userID, Message = message, Source = source };
        }

        public static EventLogEntry LogForUser(int userID, string message, string source)
        {
            return new EventLogEntry() { Category = "User", UserID = userID, Message = message, Source = source };
        }

        public static EventLogEntry LogError(string message, string source)
        {
            return new EventLogEntry() { Category = "Error", Message = message, Source = source };
        }

        public static EventLogEntry LogException(Exception ex, string source)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;

            return new EventLogEntry() { Category = "Exception", Message = ex.Message + "\r\n" + ex.StackTrace, Source = source };
        }

        public static EventLogEntry LogForElapsed(string message, string source)
        {
            return new EventLogEntry() { Category = "Elapsed", Message = message, Source = source };
        }
    }
}
