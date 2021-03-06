﻿using AutoMapper;
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
                    .ForMember(x => x.Content, opt => opt.MapFrom(blog => blog.Content.AsHtml()))
                    .ForMember(x => x.Type, opt => opt.MapFrom((b, bv) => b.Type ?? "article"))
                    .ForMember(x => x.MetaCollection, opt => opt.Ignore());
                CreateMap<Blog, BlogCreate>()
                    .ReverseMap();
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
            string html = Markdown.ToHtml(markdown, _pipeline);
            html = html.Replace("<p><img", "<p class=\"blog-img-container\"><img");
            return html;
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
                },

                new Style("HTML Element ScopeName")
                {
                    Foreground = ColorCode.Styling.Color.DodgerBlue
                },
                new Style("HTML Attribute ScopeName")
                {
                    Foreground = ColorCode.Styling.Color.DodgerBlue
                },
                new Style("HTML Attribute Value")
                {
                    Foreground = ColorCode.Styling.Color.Tomato
                }
            };
        }

        public StyleDictionary Styles => styles;
    }
}
