using AutoMapper;
using ColorCode;
using helloserve.com.Domain.Models;
using helloserve.com.Models;
using Markdig;
using Markdig.SyntaxHighlighting;

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
                CreateMap<Blog, BlogCreate>()
                    .ReverseMap()
                    .ForMember(x => x.Key, opt => opt.Ignore());
            }
        }
    }

    public static class BlogMappers
    {
        internal static MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseSyntaxHighlighting(new CodeStyleSheet())
            .Build();

        public static string AsHtml(this string markdown)
        {
            return Markdown.ToHtml(markdown, _pipeline);
        }
    }

    class CodeStyleSheet : IStyleSheet
    {
        static readonly StyleDictionary styles;

        static CodeStyleSheet()
        {
            styles = new StyleDictionary
            {
                new Style("Comment")
                {
                    Foreground = ColorCode.Styling.Color.SeaGreen
                },
                new Style("Type")
                {
                    Foreground = ColorCode.Styling.Color.MediumTurquoise
                },
                new Style("TypeVariable")
                {
                    Foreground = ColorCode.Styling.Color.MediumTurquoise
                },
                new Style("String")
                {
                    Foreground = ColorCode.Styling.Color.Tomato
                },
                new Style("Number")
                {
                    Foreground = ColorCode.Styling.Color.Firebrick
                },
                new Style("Keyword")
                {
                    Foreground = ColorCode.Styling.Color.DodgerBlue
                }
            };
        }

        public StyleDictionary Styles => styles;
    }
}
