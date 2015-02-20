using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public class GpCityPower : AuthorityModel
    {
        private class ScheduleItem
        {
            public int Id;
            public string Title;
            public string SubBlock;
            public string EventDate;
            
            public string EndDate;
            public DateTime EndDateValue
            {
                get { return GetDateTimeValue(EndDate); }
            }

            public string Description;
            public string Reason;
            public string StartDate;
            public DateTime StartDateValue
            {
                get { return GetDateTimeValue(StartDate); }
            }
            
            public string Suburb;

            private DateTime GetDateTimeValue(string dateStr)
            {
                long milliseconds = int.Parse(dateStr.Replace("/Date(", "").Replace(")/", ""));
                return (new DateTime(1970, 1, 1)).AddMilliseconds(milliseconds);
            }

            public Entities.ScheduleCalendar AsScheduleCalendar(int areaId, int stageId)
            {
                return new Entities.ScheduleCalendar()
                {
                    Date = StartDateValue.Date,
                    AreaId = areaId,
                    SheddingStageId = stageId,
                    ScheduleId = null,
                    StartTime = StartDateValue,
                    EndTime = EndDateValue
                };
            }
        }

        ILog _log = LogManager.GetLogger(typeof(GpCityPower));

        public override void CalculateSchedule(int areaId, int stageId)
        {
            AreaModel area = AreaModel.Get(areaId);

            StageModel stage = StageModel.Get(stageId);
            string stageCode = stage.Name.Replace(" ", "");

            ScheduleItem[] schedules = GetScheduleFor(area.Code, stageCode);

            //now we need to save the items into the schedule table
        }

        private ScheduleItem[] GetScheduleFor(string areacode, string stagecode)
        {
            try
            {
                string url = DataUrl;
                if (url.EndsWith("/"))
                    url = url.Substring(0, url.Length - 1);

                url = string.Format("{0}?Suburb={1}&Stage={2}", url, areacode, stagecode);

                _log.Info(string.Format("Requesting schedule from CityPower with '{0}'", url));

                Uri uri = new Uri(url);

                HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
                string responseString = string.Empty;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            responseString = reader.ReadToEnd();
                        }
                    }
                }

                ScheduleItem[] schedules = JsonConvert.DeserializeObject<ScheduleItem[]>(responseString);

                return schedules;
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error retrieving schedule for suburb {0}, stage {1}", areacode, stagecode), ex);
                return new ScheduleItem[0];
            }
        }
    }
}
