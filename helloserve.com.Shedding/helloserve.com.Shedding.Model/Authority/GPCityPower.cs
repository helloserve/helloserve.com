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
    public class GPCityPower : AuthorityModel
    {
        private class ScheduleItem
        {
            public int Id;
            public string Title;
            public string SubBlock;
            public string EventDate;
            public string EndDate;
            public string Description;
            public string Reason;
            public string StartDate;
            public string Suburb;
        }

        ILog _log = LogManager.GetLogger(typeof(GPCityPower));

        public override void CalculateSchedule(int areaId, int stageId)
        {
            AreaModel area = AreaModel.Get(areaId);

            StageModel stage = StageModel.Get(stageId);
            string stageCode = stage.Name.Replace(" ","");

            ScheduleItem[] schedules = GetScheduleFor(area.Code, stageCode);
        }

        private ScheduleItem[] GetScheduleFor(string areacode, string stagecode)
        {
            try
            {
                string url = DataUrl;
                if (url.EndsWith("/"))
                    url = url.Substring(0, url.Length - 1);

                url = string.Format("?Suburb={0}&Stage={1}", areacode, stagecode);

                _log.Info(string.Format("Requesting schedule from CityPower with '{0}'", url));

                Uri uri = new Uri(url);

                HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                Stream responseStream = response.GetResponseStream();
                byte[] responseBytes = new byte[responseStream.Length];
                responseStream.Read(responseBytes, 0, responseBytes.Length);

                string responseString = Encoding.UTF8.GetString(responseBytes);

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
