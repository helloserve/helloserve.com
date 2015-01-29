using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class RequirementRepo : BaseRepo<Requirement>
    {
        public static Requirement GetByID(int id)
        {
            var data = DB.Requirements.Where(r => r.RequirementID == id).SingleOrDefault();

            return data;
        }

        public static void Remove(Requirement requirement)
        {
            var links = (from fr in DB.FeatureRequirements
                         where fr.RequirementID == requirement.RequirementID
                         select fr);

            foreach (var link in links)
            {
                BaseRepo<FeatureRequirement>.Delete(link);
            }

            Delete(requirement);
        }
    }
}
