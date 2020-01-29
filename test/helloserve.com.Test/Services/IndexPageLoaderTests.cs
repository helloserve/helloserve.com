using helloserve.com.Adaptors;
using helloserve.com.Models;
using helloserve.com.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.com.Test.Services
{
    [TestClass]
    public class IndexPageLoaderTests
    {
        readonly Mock<IBlogServiceAdaptor> _blogAdaptorMock = new Mock<IBlogServiceAdaptor>();
        readonly Mock<IProjectServiceAdaptor> _projectAdaptorMock = new Mock<IProjectServiceAdaptor>();

        IIndexPageLoader loader => new IndexPageLoader(_blogAdaptorMock.Object, _projectAdaptorMock.Object);

        [TestMethod]
        public async Task GetIndexPageItems_Verify()
        {
            //arrange
            string fullContent = "<p>Lorem ipsum <code>dolor sit amet </code>, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nunc aliquet bibendum enim facilisis gravida neque convallis. Pretium vulputate sapien nec sagittis. Lobortis scelerisque fermentum dui faucibus in ornare quam viverra orci. Sem fringilla ut morbi tincidunt. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Nulla facilisi cras fermentum odio. Rhoncus dolor purus non enim. Et tortor consequat id porta nibh venenatis cras sed felis. Amet nisl suscipit adipiscing bibendum est ultricies. Egestas egestas fringilla phasellus faucibus scelerisque eleifend donec pretium. In fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Integer malesuada nunc vel risus commodo viverra maecenas. Platea dictumst quisque sagittis purus sit amet. Scelerisque eu ultrices vitae auctor eu.</p> <p>Cras pulvinar mattis nunc sed blandit libero volutpat sed cras. Lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis. In hac habitasse platea dictumst quisque sagittis purus. Habitant morbi tristique senectus et netus et malesuada fames. Senectus et netus et malesuada fames. Amet cursus sit amet dictum sit amet justo. Tempor id eu nisl nunc mi ipsum faucibus vitae aliquet. Elit sed vulputate mi sit amet mauris. Habitasse platea dictumst vestibulum rhoncus est pellentesque. Sit amet aliquam id diam. Luctus accumsan tortor posuere ac ut consequat semper viverra. Pellentesque habitant morbi tristique senectus et netus et. Ut ornare lectus sit amet est placerat in. Massa massa ultricies mi quis hendrerit dolor magna.</p> <pre><code>//Here is a quoted block public void QuotedBlockMethod         {  //do some stuff } </code></pre> <p>Mi quis hendrerit dolor magna eget est lorem ipsum dolor.Fringilla phasellus faucibus scelerisque eleifend donec pretium vulputate.Sapien et ligula ullamcorper malesuada proin libero nunc consequat interdum. Senectus et netus et malesuada.In est ante in nibh mauris cursus mattis molestie.Fringilla ut morbi tincidunt augue interdum. Turpis egestas pretium aenean pharetra magna ac placerat. Elit eget gravida cum sociis natoque penatibus et magnis.In dictum non consectetur a erat nam at. Varius sit amet mattis vulputate enim. Tortor condimentum lacinia quis vel eros. Gravida quis blandit turpis cursus in. Aliquam etiam erat velit scelerisque in.</p> <p>Sed arcu non odio euismod lacinia at quis risus sed. Amet purus gravida quis blandit turpis. Maecenas sed enim ut sem viverra aliquet eget. Magna sit amet purus gravida quis blandit.At volutpat diam ut venenatis tellus in metus vulputate. Tempor commodo ullamcorper a lacus vestibulum sed arcu. Habitasse platea dictumst quisque sagittis purus sit amet. Senectus et netus et malesuada fames. Nisi est sit amet facilisis magna etiam.Quis vel eros donec ac odio tempor orci dapibus.Semper risus in hendrerit gravida. Est velit egestas dui id ornare. Volutpat ac tincidunt vitae semper quis lectus nulla. Egestas congue quisque egestas diam in. Nisi porta lorem mollis aliquam.Volutpat sed cras ornare arcu dui vivamus arcu felis bibendum. Ipsum consequat nisl vel pretium.Purus gravida quis blandit turpis cursus in hac habitasse. Non pulvinar neque laoreet suspendisse interdum consectetur libero. Tortor at auctor urna nunc id.</p> <p>Facilisi etiam dignissim diam quis enim lobortis scelerisque fermentum.Pharetra convallis posuere morbi leo urna molestie at. Purus gravida quis blandit turpis cursus. Mattis rhoncus urna neque viverra justo. Amet cursus sit amet dictum sit. Et ultrices neque ornare aenean euismod elementum.Gravida rutrum quisque non tellus.Porttitor leo a diam sollicitudin tempor id eu. Mattis nunc sed blandit libero volutpat sed cras ornare.Aenean euismod elementum nisi quis.A cras semper auctor neque vitae tempus quam. In metus vulputate eu scelerisque felis. Diam phasellus vestibulum lorem sed risus ultricies tristique nulla.Sit amet nisl purus in mollis nunc sed id semper.Venenatis tellus in metus vulputate eu scelerisque felis.Sollicitudin tempor id eu nisl nunc mi ipsum faucibus vitae. Arcu bibendum at varius vel pharetra vel.Phasellus vestibulum lorem sed risus.</p>";
            var blogs = new List<BlogView>()
            {
                new BlogView() { Key = "key1", Title = "Title 1", Content = fullContent, ImageUrl = "ImageUrl 1" },
                new BlogView() { Key = "key2", Title = "Title 2", Content = fullContent, ImageUrl = "ImageUrl 2" },
                new BlogView() { Key = "key3", Title = "Title 3" },
                new BlogView() { Key = "key4", Title = "Title 4" }
            };
            _blogAdaptorMock.Setup(x => x.ReadLatest(It.IsAny<int>()))
                .ReturnsAsync(blogs);

            //act
            var result = await loader.GetIndexPageItems();

            //assert
            _blogAdaptorMock.Verify(x => x.ReadLatest(It.IsAny<int>()));
            Assert.AreEqual(4, result.Count());
            var resultList = result.ToList();
            Assert.AreEqual("key1", resultList[0].Key);
            Assert.AreEqual("Title 1", resultList[0].Title);
            Assert.AreEqual("<p>Lorem ipsum <code>dolor sit amet </code>, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nunc aliquet bibendum enim facilisis gravida neque convallis. Pretium vulputate sapien nec sagittis. Lobortis scelerisque fermentum dui faucibus in ornare quam viverra orci. Sem fringilla ut morbi tincidunt. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Nulla facilisi cras fermentum odio. Rhoncus dolor purus non enim. Et tortor consequat id porta nibh venenatis cras sed felis. Amet nisl suscipit adipiscing bibendum est ultricies. Egestas egestas fringilla phasellus faucibus scelerisque eleifend donec pretium. In fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Integer malesuada nunc vel risus commodo viverra maecenas. Platea dictumst quisque sagittis purus sit amet. Scelerisque eu ultrices vitae auctor eu.</p>", resultList[0].Content);
            Assert.AreEqual("ImageUrl 1", resultList[0].ImageUrl);
            Assert.IsTrue(resultList[0].Url.Contains(resultList[0].Key));
            Assert.AreEqual("key2", resultList[1].Key);
            Assert.AreEqual("<p>Lorem ipsum <code>dolor sit amet </code>, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nunc aliquet bibendum enim facilisis gravida neque convallis. Pretium vulputate sapien nec sagittis. Lobortis scelerisque fermentum dui faucibus in ornare quam viverra orci. Sem fringilla ut morbi tincidunt. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Nulla facilisi cras fermentum odio. Rhoncus dolor purus non enim. Et tortor consequat id porta nibh venenatis cras sed felis. Amet nisl suscipit adipiscing bibendum est ultricies. Egestas egestas fringilla phasellus faucibus scelerisque eleifend donec pretium. In fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Integer malesuada nunc vel risus commodo viverra maecenas. Platea dictumst quisque sagittis purus sit amet. Scelerisque eu ultrices vitae auctor eu.</p>", resultList[1].Content);
            Assert.AreEqual("key3", resultList[2].Key);
            Assert.AreEqual("key4", resultList[3].Key);
        }
    }
}
