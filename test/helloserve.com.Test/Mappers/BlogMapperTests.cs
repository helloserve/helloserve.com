using helloserve.com.Domain.Models;
using helloserve.com.Mappers;
using helloserve.com.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace helloserve.com.Test.Mappers
{
    [TestClass]
    public class BlogMapperTests
    {
        [TestMethod]
        public void BlogConfig_Content()
        {
            //arrange
            Blog blog = new Blog() { Content = "content" };

            //act
            BlogView result = Config.Mapper.Map<BlogView>(blog);

            //assert
            Assert.AreEqual("<p>content</p>\n", result.Content);
        }

        [TestMethod]
        public void BlogConfig_CreateBlog()
        {
            //arrange
            BlogCreate blog = new BlogCreate() { Key="key", Title = "title", Content = "content" };

            //act
            Blog result = Config.Mapper.Map<Blog>(blog);

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AsHtml_Transformed()
        {
            //arrange
            string markdown = @"# Heading
Some paragraph here.

```
a quoted block
```

Another paragraph";

            //act
            string html = markdown.AsHtml();

            //assert
            Assert.AreEqual("<h1 id=\"heading\">Heading</h1>\n<p>Some paragraph here.</p>\n<pre><code>a quoted block\n</code></pre>\n<p>Another paragraph</p>\n", html);
        }
    }
}
