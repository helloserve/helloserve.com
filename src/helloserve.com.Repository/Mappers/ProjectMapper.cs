using AutoMapper;
using System.Collections.Generic;

namespace helloserve.com.Repository.Mappers
{
    public static partial class Config
    {
        public class ProjectConfig : Profile
        {
            public ProjectConfig()
            {
                CreateMap<Database.Entities.Project, Domain.Models.Project>();
            }
        }
    }
    public static class ProjectMapper
    {
        public static IEnumerable<Domain.Models.Project> Map(this IEnumerable<Database.Entities.Project> collection)
        {
            return Config.Mapper.Map<IEnumerable<Domain.Models.Project>>(collection);
        }
    }
}
