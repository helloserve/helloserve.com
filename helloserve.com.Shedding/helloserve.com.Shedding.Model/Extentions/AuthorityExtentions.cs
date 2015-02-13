using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public static class AuthorityExtentions
    {
        public static AuthorityModel AsAuthorityModel(this Entities.Authority entity)
        {
            AuthorityModel model;
            if (string.IsNullOrEmpty(entity.ClassType))
                model = new AuthorityModel();
            else
            {
                string classType = entity.ClassType;
                if (!classType.StartsWith("helloserve.com.Shedding.Model."))
                    classType = string.Format("helloserve.com.Shedding.Model.{0}", classType);

                Type modelType = Assembly.GetAssembly(typeof(AuthorityModel)).GetType(classType);
                model = Activator.CreateInstance(modelType) as AuthorityModel;
            }

            model.Id = entity.Id;
            model.Name = entity.Name;
            model.ClassType = entity.ClassType;
            model.DataUrl = entity.DataUrl;
            model.StatusUrl = entity.StatusUrl;

            model.Initialize(entity);

            return model;
        }
    }
}
