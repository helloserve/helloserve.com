using AutoMapper;
using System.Threading.Tasks;

namespace helloserve.com.Repository.Mappers
{
    public static partial class Config
    {
        public class BlogConfig : Profile
        {
            public BlogConfig()
            {
                CreateMap<Database.Entities.Blog, Domain.Models.Blog>();
            }
        }
    }

    public static class BlogMappers
    {
        public static Domain.Models.Blog Map(this Database.Entities.Blog entity)
        {
            return Config.Mapper.Map<Domain.Models.Blog>(entity);
        }
    }
}
