using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class LogRepo : BaseRepo<Log>
    {
        public static Log GetByID(int id)
        {
            return DB.Logs.Where(l=>l.LogID == id).Single();
        }

        public static void LogForFeature(int? userID, int featureID, string message, string source)
        {
            Log log = new Log()
            {
                UserID = userID,
                FeatureID = featureID,
                Message = message,
                Source = source
            };

            log.Save();
        }

        public static void LogForNews(int? userID, int newsID, string message, string source)
        {
            Log log = new Log()
            {
                UserID = userID,
                NewsID = newsID,
                Message = message,
                Source = source
            };
            log.Save();
        }

        public static void LogForMedia(int? userID, int mediaID, string message, string source)
        {
            Log log = new Log()
            {
                UserID = userID,
                MediaID = mediaID,
                Message = message,
                Source = source
            };
            log.Save();
        }

        public static void LogForDownload(int? userID, int downloadID, string message, string source)
        {
            Log log = new Log()
            {
                UserID = userID,
                DownloadID = downloadID,
                Message = message,
                Source = source
            };
            log.Save();
        }

        public static void LogForUser(int userID, string message, string source)
        {
            Log log = new Log()
            {
                UserID = userID,
                Message = message,
                Source = source
            };
            log.Save();
        }
    }
}
