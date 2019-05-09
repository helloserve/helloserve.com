using AutoMapper;

namespace helloserve.com.Repository.Mappers
{
    public static partial class Config
    {
        public static IMapper Mapper { get; private set; }

        static Config()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BlogConfig>();
            });

            Mapper = config.CreateMapper();
        }
    }
}
