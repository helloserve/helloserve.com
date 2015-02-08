using helloserve.com.Shedding.Entities;
using HtmlAgilityPack;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace helloserve.com.Shedding.Model
{
    public class AuthorityModel : ListModel
    {
        ILog _log = LogManager.GetLogger(typeof(AuthorityModel));

        public string ClassType;
        public string DataUrl;
        public string StatusUrl;

        public static AuthorityModel Get(int authorityId)
        {
            ListRepository repo = new ListRepository();
            return repo.GetAuthority(authorityId).AsAuthorityModel();
        }

        public virtual SheddingStage? GetCurrentStage()
        {
            try
            {
                _log.Info("Reading load shedding stage from Eskom");

                WebRequest request = WebRequest.Create("http://loadshedding.eskom.co.za/");
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    html = sr.ReadToEnd();
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                HtmlNode statusNode = doc.GetElementbyId("lsstatus");
                if (statusNode == null)
                    throw new InvalidOperationException("Could not find 'lsstatus' node from 'http://loadshedding.eskom.co.za'");
                string nodeText = statusNode.InnerText.ToUpper();
                if (nodeText.Contains("NOT LOAD SHEDDING"))
                    return SheddingStage.None;
                if (nodeText.Contains("STAGE 1"))
                    return SheddingStage.Stage1;
                if (nodeText.Contains("STAGE 2"))
                    return SheddingStage.Stage2;
                if (nodeText.Contains("STAGE 3A"))
                    return SheddingStage.Stage3a;
                if (nodeText.Contains("STAGE 3B"))
                    return SheddingStage.Stage3b;

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
