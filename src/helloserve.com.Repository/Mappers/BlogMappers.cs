﻿using AutoMapper;
using System.Collections.Generic;

namespace helloserve.com.Repository.Mappers
{
    public static partial class Config
    {
        public class BlogConfig : Profile
        {
            public BlogConfig()
            {
                CreateMap<Database.Entities.Blog, Domain.Models.Blog>()
                    .ReverseMap();
                CreateMap<Database.Queries.BlogListing, Domain.Models.BlogListing>();
            }
        }
    }

    public static class BlogMappers
    {
        public static Domain.Models.Blog Map(this Database.Entities.Blog entity)
        {
            return Config.Mapper.Map<Domain.Models.Blog>(entity);
        }

        public static IEnumerable<Domain.Models.Blog> Map(this IEnumerable<Database.Entities.Blog> collection)
        {
            return Config.Mapper.Map<IEnumerable<Domain.Models.Blog>>(collection);
        }

        public static void MapOnto(this Domain.Models.Blog model, Database.Entities.Blog entity)
        {
            Config.Mapper.Map(model, entity);
        }

        public static IEnumerable<Domain.Models.BlogListing> Map(this IEnumerable<Database.Queries.BlogListing> collection)
        {
            return Config.Mapper.Map<IEnumerable<Domain.Models.BlogListing>>(collection);
        }
    }
}
