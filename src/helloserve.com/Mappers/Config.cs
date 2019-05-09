using AutoMapper;

namespace helloserve.com.Mappers
{
    public static partial class Config
    {
        public static IMapper Mapper { get; private set; }
        static Config()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BlogConfig>();
            });

            Mapper = configuration.CreateMapper();
        }
    }
}
