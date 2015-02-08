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
            Type modelType = Assembly.GetAssembly(typeof(AuthorityModel)).GetType(entity.ClassType);
            AuthorityModel model = Activator.CreateInstance(modelType) as AuthorityModel;

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
