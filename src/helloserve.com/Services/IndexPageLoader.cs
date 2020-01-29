using helloserve.com.Adaptors;
using helloserve.com.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.com.Services
{
    public interface IIndexPageLoader
    {
        Task<IEnumerable<IndexPageItem>> GetIndexPageItems();
    }

    public class IndexPageLoader : IIndexPageLoader
    {
        readonly IBlogServiceAdaptor blogServiceAdaptor;
        readonly IProjectServiceAdaptor projectServiceAdaptor;

        public IndexPageLoader(IBlogServiceAdaptor blogServiceAdaptor, IProjectServiceAdaptor projectServiceAdaptor)
        {
            this.blogServiceAdaptor = blogServiceAdaptor;
            this.projectServiceAdaptor = projectServiceAdaptor;
        }

        public async Task<IEnumerable<IndexPageItem>> GetIndexPageItems()
        {
            var blogs = await blogServiceAdaptor.ReadLatest(5);
            var projects = await projectServiceAdaptor.ReadAllActive();

            var result = blogs
                .Select(x => new IndexPageItem()
                {
                    Key = x.Key,
                    Title = x.Title,
                    Url = $"/blog/{x.Key}",
                    Content = CutContent(x.Content),
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            int i = 0;
            foreach (var project in projects)
            {
                result.Insert(i * 2 + 1, new IndexPageItem()
                {
                    Key = project.Key,
                    Title = project.Name,
                    Url = $"/projects/{project.Key}",
                    Content = CutContent(project.Description),
                    ImageUrl = project.ImageUrl
                });
                i++;
            }

            return result;
        }

        private string CutContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            if (content.StartsWith("<p>"))
            {
                return content.Substring(0, content.IndexOf("</p>") + 4);
            }

            return content;
        }
    }
}
