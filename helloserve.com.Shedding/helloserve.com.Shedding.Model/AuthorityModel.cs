using helloserve.com.Shedding.Entities;
using HtmlAgilityPack;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace helloserve.com.Shedding.Model
{
    public class AuthorityModel : ListModel
    {
        static ILog _log = LogManager.GetLogger(typeof(AuthorityModel));

        public string ClassType;
        public string DataUrl;
        public string StatusUrl;

        public static AuthorityModel Get(int authorityId)
        {
            ListRepository repo = new ListRepository();
            AuthorityModel authority = repo.GetAuthority(authorityId).AsAuthorityModel();
            _log.Info(string.Format("Instantiating authority as '{0}'", authority.GetType().FullName)); 
            return authority;
        }

        public virtual SheddingStage? GetCurrentStage()
        {
            try
            {
                _log.Info("Reading load shedding stage from Eskom");

                string result = String.Empty;

                HttpWebRequest request = HttpWebRequest.Create("http://loadshedding.eskom.co.za/loadshedding/getstatus") as HttpWebRequest;
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Reload);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream data = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(data))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }

                if (result == "1")
                    return SheddingStage.None;
                if (result == "2")
                    return SheddingStage.Stage1;
                if (result == "3")
                    return SheddingStage.Stage2;
                if (result == "4")
                    return SheddingStage.Stage3;

                return null;
            }
            catch (Exception ex)
            {
                _log.Error("Error retrieving load shedding stage", ex);
                return null;
            }
        }

        public virtual void CalculateSchedule(int areaId, int stageId)
        {

        }

        public virtual void Initialize(Entities.Authority entity)
        {
        }
    }
}
