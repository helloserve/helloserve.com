using AutoMapper;
using helloserve.com.Models;
using System.Collections.Generic;

namespace helloserve.com.Mappers
{
    public static partial class Config
    {
        public class ProjectConfig : Profile
        {
            public ProjectConfig()
            {
                CreateMap<Domain.Models.Project, ProjectView>()
                    .ForMember(x => x.Type, opt => opt.MapFrom((p, pv) => "article"))
                    .ForMember(x=>x.MetaCollection, opt=>opt.Ignore());
                CreateMap<Domain.Models.Project, ProjectItemView>();
            }
        }
    }

    public static class ProjectMappers
    {
        public static ProjectView Map(this Domain.Models.Project model)
        {
            return Config.Mapper.Map<ProjectView>(model);
        }

        public static IEnumerable<ProjectItemView> Map(this IEnumerable<Domain.Models.Project> collection)
        {
            return Config.Mapper.Map<IEnumerable<ProjectItemView>>(collection);
        }
    }
}
