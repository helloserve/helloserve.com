using helloserve.com.Shedding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public class ListModel
    {
        public int Id;
        public string Name;

        public static List<StageModel> GetStages()
        {
            ListRepository repo = new ListRepository();
            return repo.GetStages().ToModelList();
        }

        public static List<AuthorityModel> GetAuthorities()
        {
            ListRepository repo = new ListRepository();
            return repo.GetAuthorities().ToModelList();
        }

        public static List<AreaModel> GetAreas(int authorityId)
        {
            ListRepository repo = new ListRepository();
            return repo.GetAreas(authorityId).ToModelList();
        }
    }

    public class StageModel : ListModel
    {
        public static StageModel Get(int stageId)
        {
            ListRepository repo = new ListRepository();
            return repo.GetStage(stageId).AsModel();
        }
    }

    public class AreaModel : ListModel
    {
        public string Code;

        public static AreaModel Get(int areaId)
        {
            ListRepository repo = new ListRepository();
            return repo.GetArea(areaId).AsModel();
        }
    }
}
