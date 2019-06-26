using AutoMapper;
using helloserve.com.Domain.Models;
using helloserve.com.Models;
using Markdig;

namespace helloserve.com.Mappers
{
    public static partial class Config
    {
        public class BlogConfig : Profile
        {
            public BlogConfig()
            {
                CreateMap<BlogListing, BlogItemView>();
                CreateMap<Blog, BlogView>()
                    .ForMember(x => x.Content, opt => opt.MapFrom(blog => blog.Content.AsHtml()));
                CreateMap<BlogCreate, Blog>()
                    .ForMember(x => x.Key, opt => opt.Ignore());
            }
        }
    }

    public static class BlogMappers
    {
        internal static MarkdownPipeline _pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

        public static string AsHtml(this string markdown)
        {
            return Markdown.ToHtml(markdown, _pipeline);
        }
    }
}
