using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using helloserve.Common;
using System.Text.RegularExpressions;

namespace helloserve.Web
{
    public static class Twitter
    {
        private static bool _isPolling = false;

        public static string ParseTwitterHashTags(this string text)
        {
            Regex regex = new Regex("(#{1}([A-Za-z0-9]*))");
            Match match = regex.Match(text);
            while (match.Success)
            {
                string newText = "<a href=\"http://search.twitter.com/search?q=%23" + match.Groups[2].Value + "\">" + match.Groups[0].Value + "</a>";
                text = text.Replace(match.Groups[0].Value, newText);
                int index = match.Index + newText.Length;
                match = regex.Match(text, index);
            }
            return text;
        }

        public static string ParseTwitterUsers(this string text)
        {
            Regex regex = new Regex("(@{1}([A-Za-z0-9]*))");
            Match match = regex.Match(text);
            while (match.Success)
            {
                string newText = "<a href=\"http://www.twitter.com/" + match.Groups[2].Value + "\">" + match.Groups[0].Value + "</a>";
                text = text.Replace(match.Groups[0].Value, newText);
                int index = match.Index + newText.Length;
                match = regex.Match(text, index);
            }
            return text;
        }

        public static string ParseLinks(this string text)
        {
            Regex regex = new Regex("((http://{1})([A-Za-z0-9/.]*))");
            Match match = regex.Match(text);
            while (match.Success)
            {
                string newText = "<a href=\"" + match.Groups[0].Value + "\">" + match.Groups[0].Value + "</a>";
                text = text.Replace(match.Groups[0].Value, newText);
                int index = match.Index + newText.Length;
                match = regex.Match(text, index);
            }
            return text;
        }

        public static void Poll()
        {
            if (_isPolling)
                return;

            //LogRepo.LogForMedia(Settings.Current.GetUserID(), 0, "Polling for latest Twitter feeds", "Twitter.Poll");
            Settings.EventLogger.Log(EventLogEntry.LogForMedia(Settings.Current.GetUserID(), 0, "Polling for latest Twitter feeds", "Twitter.Poll"));

            _isPolling = true;

            string url = "https://api.twitter.com/1/statuses/user_timeline/helloserve.json";
            string qry = "callback=?&count=10";

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "?" + qry);
            request.Method = "GET";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                int count = 0;
                byte[] part = new byte[1024];
                do
                {
                    count = stream.Read(part, 0, 1024);
                    ms.Write(part, 0, count);
                } while (stream.CanRead && count > 0);
                buffer = ms.ToArray();
            }
            response.Close();

            string json = UTF8Encoding.UTF8.GetString(buffer);

            Settings.Tweets.StoredTweets = System.Web.Helpers.Json.Decode<List<Tweet>>(json);

            _isPolling = false;
        }
    }
}