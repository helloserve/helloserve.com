﻿using helloserve.com.Domain;
using helloserve.com.Mappers;
using helloserve.com.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Adaptors
{
    public class BlogServiceAdaptor : IBlogServiceAdaptor
    {
        readonly IBlogService _service;

        public BlogServiceAdaptor(IBlogService service)
        {
            _service = service;
        }

        public async Task<BlogView> Read(string title)
        {
            Domain.Models.Blog blog = await _service.Read(title);
            BlogView viewModel = Config.Mapper.Map<BlogView>(blog);
            return viewModel;
        }

        public async Task<IEnumerable<BlogItemView>> ReadAll(int page, int count, string ownerKey, bool isAuthenticated)
        {
            var items = await _service.ReadAll(page, count, ownerKey, isAuthenticated);
            return Config.Mapper.Map<IEnumerable<BlogItemView>>(items);
        }

        public async Task<BlogCreate> Edit(string title)
        {
            return Config.Mapper.Map<BlogCreate>(await _service.Read(title));
        }

        public async Task Submit(BlogCreate blog)
        {
            await _service.CreateUpdate(Config.Mapper.Map<Domain.Models.Blog>(blog));
        }

        public async Task Publish(string title)
        {
            await _service.Publish(title, new List<Domain.Syndication.Models.SyndicationText>());
        }

        public async Task<IEnumerable<BlogView>> ReadLatest(int count)
        {
            return Config.Mapper.Map<IEnumerable<BlogView>>(await _service.ReadLatest(count));
        }
    }

    public class MockBlogServiceAdaptor : IBlogServiceAdaptor
    {
        public async Task<BlogView> Read(string title)
        {
            return await Task.FromResult(new BlogView()
            {
                Title = "Lorem Ipsum generated blog post",
                ImageUrl = "media/20180609_162848.jpg",
                Content = @"<p>Lorem ipsum <code>dolor sit amet</code>, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nunc aliquet bibendum enim facilisis gravida neque convallis. Pretium vulputate sapien nec sagittis. Lobortis scelerisque fermentum dui faucibus in ornare quam viverra orci. Sem fringilla ut morbi tincidunt. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Nulla facilisi cras fermentum odio. Rhoncus dolor purus non enim. Et tortor consequat id porta nibh venenatis cras sed felis. Amet nisl suscipit adipiscing bibendum est ultricies. Egestas egestas fringilla phasellus faucibus scelerisque eleifend donec pretium. In fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Integer malesuada nunc vel risus commodo viverra maecenas. Platea dictumst quisque sagittis purus sit amet. Scelerisque eu ultrices vitae auctor eu.</p>
<p>Cras pulvinar mattis nunc sed blandit libero volutpat sed cras. Lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis. In hac habitasse platea dictumst quisque sagittis purus. Habitant morbi tristique senectus et netus et malesuada fames. Senectus et netus et malesuada fames. Amet cursus sit amet dictum sit amet justo. Tempor id eu nisl nunc mi ipsum faucibus vitae aliquet. Elit sed vulputate mi sit amet mauris. Habitasse platea dictumst vestibulum rhoncus est pellentesque. Sit amet aliquam id diam. Luctus accumsan tortor posuere ac ut consequat semper viverra. Pellentesque habitant morbi tristique senectus et netus et. Ut ornare lectus sit amet est placerat in. Massa massa ultricies mi quis hendrerit dolor magna.</p>
<pre><code>//Here is a quoted block
public void QuotedBlockMethod {
    //do some stuff
}
</code></pre>
<p>Mi quis hendrerit dolor magna eget est lorem ipsum dolor. Fringilla phasellus faucibus scelerisque eleifend donec pretium vulputate. Sapien et ligula ullamcorper malesuada proin libero nunc consequat interdum. Senectus et netus et malesuada. In est ante in nibh mauris cursus mattis molestie. Fringilla ut morbi tincidunt augue interdum. Turpis egestas pretium aenean pharetra magna ac placerat. Elit eget gravida cum sociis natoque penatibus et magnis. In dictum non consectetur a erat nam at. Varius sit amet mattis vulputate enim. Tortor condimentum lacinia quis vel eros. Gravida quis blandit turpis cursus in. Aliquam etiam erat velit scelerisque in.</p>
<p>Sed arcu non odio euismod lacinia at quis risus sed. Amet purus gravida quis blandit turpis. Maecenas sed enim ut sem viverra aliquet eget. Magna sit amet purus gravida quis blandit. At volutpat diam ut venenatis tellus in metus vulputate. Tempor commodo ullamcorper a lacus vestibulum sed arcu. Habitasse platea dictumst quisque sagittis purus sit amet. Senectus et netus et malesuada fames. Nisi est sit amet facilisis magna etiam. Quis vel eros donec ac odio tempor orci dapibus. Semper risus in hendrerit gravida. Est velit egestas dui id ornare. Volutpat ac tincidunt vitae semper quis lectus nulla. Egestas congue quisque egestas diam in. Nisi porta lorem mollis aliquam. Volutpat sed cras ornare arcu dui vivamus arcu felis bibendum. Ipsum consequat nisl vel pretium. Purus gravida quis blandit turpis cursus in hac habitasse. Non pulvinar neque laoreet suspendisse interdum consectetur libero. Tortor at auctor urna nunc id.</p>
<p>Facilisi etiam dignissim diam quis enim lobortis scelerisque fermentum. Pharetra convallis posuere morbi leo urna molestie at. Purus gravida quis blandit turpis cursus. Mattis rhoncus urna neque viverra justo. Amet cursus sit amet dictum sit. Et ultrices neque ornare aenean euismod elementum. Gravida rutrum quisque non tellus. Porttitor leo a diam sollicitudin tempor id eu. Mattis nunc sed blandit libero volutpat sed cras ornare. Aenean euismod elementum nisi quis. A cras semper auctor neque vitae tempus quam. In metus vulputate eu scelerisque felis. Diam phasellus vestibulum lorem sed risus ultricies tristique nulla. Sit amet nisl purus in mollis nunc sed id semper. Venenatis tellus in metus vulputate eu scelerisque felis. Sollicitudin tempor id eu nisl nunc mi ipsum faucibus vitae. Arcu bibendum at varius vel pharetra vel. Phasellus vestibulum lorem sed risus.</p>
"
            });
        }

        public async Task<IEnumerable<BlogItemView>> ReadAll(int page, int count, string ownerKey, bool isAuthenticated)
        {
            return await Task.FromResult(new List<BlogItemView>()
            {
                new BlogItemView() { Title = "This is a first title", Key = "this_is_a_first_title", Description = "This is a blog description.", ImageUrl = "media/20180609_162848.jpg" },
                new BlogItemView() { Title = "This is a second title", Key = "this_is_a_second_title", Description = "This is a longer, more detailed, blog description" },
                new BlogItemView() { Title = "Here is another entry!!", Key = "here_is_another_entry" },
                new BlogItemView() { Title = "What different types of heading variations are there?", Key = "what_different_types_of_heading_variations_are_there" }
            });
        }

        public Task<BlogCreate> Edit(string title)
        {
            throw new System.NotImplementedException();
        }

        public Task Submit(BlogCreate blog)
        {
            throw new System.NotImplementedException();
        }

        public Task Publish(string title)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<BlogView>> ReadLatest(int count)
        {
            return await Task.FromResult(new List<BlogView>()
            {
                new BlogView()
                {
                    Key = "key1",
                    Title = "Lorem Ipsum generated blog post",
                    ImageUrl = "media/20180609_162848.jpg",
                    Description = "This is a blog description",
                    Content = @"<p>Lorem ipsum <code>dolor sit amet</code>, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nunc aliquet bibendum enim facilisis gravida neque convallis. Pretium vulputate sapien nec sagittis. Lobortis scelerisque fermentum dui faucibus in ornare quam viverra orci. Sem fringilla ut morbi tincidunt. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Nulla facilisi cras fermentum odio. Rhoncus dolor purus non enim. Et tortor consequat id porta nibh venenatis cras sed felis. Amet nisl suscipit adipiscing bibendum est ultricies. Egestas egestas fringilla phasellus faucibus scelerisque eleifend donec pretium. In fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Integer malesuada nunc vel risus commodo viverra maecenas. Platea dictumst quisque sagittis purus sit amet. Scelerisque eu ultrices vitae auctor eu.</p>
<p>Cras pulvinar mattis nunc sed blandit libero volutpat sed cras. Lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis. In hac habitasse platea dictumst quisque sagittis purus. Habitant morbi tristique senectus et netus et malesuada fames. Senectus et netus et malesuada fames. Amet cursus sit amet dictum sit amet justo. Tempor id eu nisl nunc mi ipsum faucibus vitae aliquet. Elit sed vulputate mi sit amet mauris. Habitasse platea dictumst vestibulum rhoncus est pellentesque. Sit amet aliquam id diam. Luctus accumsan tortor posuere ac ut consequat semper viverra. Pellentesque habitant morbi tristique senectus et netus et. Ut ornare lectus sit amet est placerat in. Massa massa ultricies mi quis hendrerit dolor magna.</p>
<pre><code>//Here is a quoted block
public void QuotedBlockMethod {
    //do some stuff
}
</code></pre>
<p>Mi quis hendrerit dolor magna eget est lorem ipsum dolor. Fringilla phasellus faucibus scelerisque eleifend donec pretium vulputate. Sapien et ligula ullamcorper malesuada proin libero nunc consequat interdum. Senectus et netus et malesuada. In est ante in nibh mauris cursus mattis molestie. Fringilla ut morbi tincidunt augue interdum. Turpis egestas pretium aenean pharetra magna ac placerat. Elit eget gravida cum sociis natoque penatibus et magnis. In dictum non consectetur a erat nam at. Varius sit amet mattis vulputate enim. Tortor condimentum lacinia quis vel eros. Gravida quis blandit turpis cursus in. Aliquam etiam erat velit scelerisque in.</p>
<p>Sed arcu non odio euismod lacinia at quis risus sed. Amet purus gravida quis blandit turpis. Maecenas sed enim ut sem viverra aliquet eget. Magna sit amet purus gravida quis blandit. At volutpat diam ut venenatis tellus in metus vulputate. Tempor commodo ullamcorper a lacus vestibulum sed arcu. Habitasse platea dictumst quisque sagittis purus sit amet. Senectus et netus et malesuada fames. Nisi est sit amet facilisis magna etiam. Quis vel eros donec ac odio tempor orci dapibus. Semper risus in hendrerit gravida. Est velit egestas dui id ornare. Volutpat ac tincidunt vitae semper quis lectus nulla. Egestas congue quisque egestas diam in. Nisi porta lorem mollis aliquam. Volutpat sed cras ornare arcu dui vivamus arcu felis bibendum. Ipsum consequat nisl vel pretium. Purus gravida quis blandit turpis cursus in hac habitasse. Non pulvinar neque laoreet suspendisse interdum consectetur libero. Tortor at auctor urna nunc id.</p>
<p>Facilisi etiam dignissim diam quis enim lobortis scelerisque fermentum. Pharetra convallis posuere morbi leo urna molestie at. Purus gravida quis blandit turpis cursus. Mattis rhoncus urna neque viverra justo. Amet cursus sit amet dictum sit. Et ultrices neque ornare aenean euismod elementum. Gravida rutrum quisque non tellus. Porttitor leo a diam sollicitudin tempor id eu. Mattis nunc sed blandit libero volutpat sed cras ornare. Aenean euismod elementum nisi quis. A cras semper auctor neque vitae tempus quam. In metus vulputate eu scelerisque felis. Diam phasellus vestibulum lorem sed risus ultricies tristique nulla. Sit amet nisl purus in mollis nunc sed id semper. Venenatis tellus in metus vulputate eu scelerisque felis. Sollicitudin tempor id eu nisl nunc mi ipsum faucibus vitae. Arcu bibendum at varius vel pharetra vel. Phasellus vestibulum lorem sed risus.</p>
                        "
                },
                new BlogView()
                {
                    Key = "key2",
                    Title = "Lorem Ipsum generated blog post",
                    ImageUrl = "media/20180609_162848.jpg",
                    Description = "This is another, longer and more detailed blog description",
                    Content = @"<p>Lorem ipsum <code>dolor sit amet</code>, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nunc aliquet bibendum enim facilisis gravida neque convallis. Pretium vulputate sapien nec sagittis. Lobortis scelerisque fermentum dui faucibus in ornare quam viverra orci. Sem fringilla ut morbi tincidunt. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Nulla facilisi cras fermentum odio. Rhoncus dolor purus non enim. Et tortor consequat id porta nibh venenatis cras sed felis. Amet nisl suscipit adipiscing bibendum est ultricies. Egestas egestas fringilla phasellus faucibus scelerisque eleifend donec pretium. In fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Integer malesuada nunc vel risus commodo viverra maecenas. Platea dictumst quisque sagittis purus sit amet. Scelerisque eu ultrices vitae auctor eu.</p>
<p>Cras pulvinar mattis nunc sed blandit libero volutpat sed cras. Lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis. In hac habitasse platea dictumst quisque sagittis purus. Habitant morbi tristique senectus et netus et malesuada fames. Senectus et netus et malesuada fames. Amet cursus sit amet dictum sit amet justo. Tempor id eu nisl nunc mi ipsum faucibus vitae aliquet. Elit sed vulputate mi sit amet mauris. Habitasse platea dictumst vestibulum rhoncus est pellentesque. Sit amet aliquam id diam. Luctus accumsan tortor posuere ac ut consequat semper viverra. Pellentesque habitant morbi tristique senectus et netus et. Ut ornare lectus sit amet est placerat in. Massa massa ultricies mi quis hendrerit dolor magna.</p>
<pre><code>//Here is a quoted block
public void QuotedBlockMethod {
    //do some stuff
}
</code></pre>
<p>Mi quis hendrerit dolor magna eget est lorem ipsum dolor. Fringilla phasellus faucibus scelerisque eleifend donec pretium vulputate. Sapien et ligula ullamcorper malesuada proin libero nunc consequat interdum. Senectus et netus et malesuada. In est ante in nibh mauris cursus mattis molestie. Fringilla ut morbi tincidunt augue interdum. Turpis egestas pretium aenean pharetra magna ac placerat. Elit eget gravida cum sociis natoque penatibus et magnis. In dictum non consectetur a erat nam at. Varius sit amet mattis vulputate enim. Tortor condimentum lacinia quis vel eros. Gravida quis blandit turpis cursus in. Aliquam etiam erat velit scelerisque in.</p>
<p>Sed arcu non odio euismod lacinia at quis risus sed. Amet purus gravida quis blandit turpis. Maecenas sed enim ut sem viverra aliquet eget. Magna sit amet purus gravida quis blandit. At volutpat diam ut venenatis tellus in metus vulputate. Tempor commodo ullamcorper a lacus vestibulum sed arcu. Habitasse platea dictumst quisque sagittis purus sit amet. Senectus et netus et malesuada fames. Nisi est sit amet facilisis magna etiam. Quis vel eros donec ac odio tempor orci dapibus. Semper risus in hendrerit gravida. Est velit egestas dui id ornare. Volutpat ac tincidunt vitae semper quis lectus nulla. Egestas congue quisque egestas diam in. Nisi porta lorem mollis aliquam. Volutpat sed cras ornare arcu dui vivamus arcu felis bibendum. Ipsum consequat nisl vel pretium. Purus gravida quis blandit turpis cursus in hac habitasse. Non pulvinar neque laoreet suspendisse interdum consectetur libero. Tortor at auctor urna nunc id.</p>
<p>Facilisi etiam dignissim diam quis enim lobortis scelerisque fermentum. Pharetra convallis posuere morbi leo urna molestie at. Purus gravida quis blandit turpis cursus. Mattis rhoncus urna neque viverra justo. Amet cursus sit amet dictum sit. Et ultrices neque ornare aenean euismod elementum. Gravida rutrum quisque non tellus. Porttitor leo a diam sollicitudin tempor id eu. Mattis nunc sed blandit libero volutpat sed cras ornare. Aenean euismod elementum nisi quis. A cras semper auctor neque vitae tempus quam. In metus vulputate eu scelerisque felis. Diam phasellus vestibulum lorem sed risus ultricies tristique nulla. Sit amet nisl purus in mollis nunc sed id semper. Venenatis tellus in metus vulputate eu scelerisque felis. Sollicitudin tempor id eu nisl nunc mi ipsum faucibus vitae. Arcu bibendum at varius vel pharetra vel. Phasellus vestibulum lorem sed risus.</p>
                        "
                },
                new BlogView()
                {
                    Key = "key3",
                    Title = "Lorem Ipsum generated blog post",
                    Content = @"<p>Lorem ipsum <code>dolor sit amet</code>, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nunc aliquet bibendum enim facilisis gravida neque convallis. Pretium vulputate sapien nec sagittis. Lobortis scelerisque fermentum dui faucibus in ornare quam viverra orci. Sem fringilla ut morbi tincidunt. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Nulla facilisi cras fermentum odio. Rhoncus dolor purus non enim. Et tortor consequat id porta nibh venenatis cras sed felis. Amet nisl suscipit adipiscing bibendum est ultricies. Egestas egestas fringilla phasellus faucibus scelerisque eleifend donec pretium. In fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Integer malesuada nunc vel risus commodo viverra maecenas. Platea dictumst quisque sagittis purus sit amet. Scelerisque eu ultrices vitae auctor eu.</p>
<p>Cras pulvinar mattis nunc sed blandit libero volutpat sed cras. Lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis. In hac habitasse platea dictumst quisque sagittis purus. Habitant morbi tristique senectus et netus et malesuada fames. Senectus et netus et malesuada fames. Amet cursus sit amet dictum sit amet justo. Tempor id eu nisl nunc mi ipsum faucibus vitae aliquet. Elit sed vulputate mi sit amet mauris. Habitasse platea dictumst vestibulum rhoncus est pellentesque. Sit amet aliquam id diam. Luctus accumsan tortor posuere ac ut consequat semper viverra. Pellentesque habitant morbi tristique senectus et netus et. Ut ornare lectus sit amet est placerat in. Massa massa ultricies mi quis hendrerit dolor magna.</p>
<pre><code>//Here is a quoted block
public void QuotedBlockMethod {
    //do some stuff
}
</code></pre>
<p>Mi quis hendrerit dolor magna eget est lorem ipsum dolor. Fringilla phasellus faucibus scelerisque eleifend donec pretium vulputate. Sapien et ligula ullamcorper malesuada proin libero nunc consequat interdum. Senectus et netus et malesuada. In est ante in nibh mauris cursus mattis molestie. Fringilla ut morbi tincidunt augue interdum. Turpis egestas pretium aenean pharetra magna ac placerat. Elit eget gravida cum sociis natoque penatibus et magnis. In dictum non consectetur a erat nam at. Varius sit amet mattis vulputate enim. Tortor condimentum lacinia quis vel eros. Gravida quis blandit turpis cursus in. Aliquam etiam erat velit scelerisque in.</p>
<p>Sed arcu non odio euismod lacinia at quis risus sed. Amet purus gravida quis blandit turpis. Maecenas sed enim ut sem viverra aliquet eget. Magna sit amet purus gravida quis blandit. At volutpat diam ut venenatis tellus in metus vulputate. Tempor commodo ullamcorper a lacus vestibulum sed arcu. Habitasse platea dictumst quisque sagittis purus sit amet. Senectus et netus et malesuada fames. Nisi est sit amet facilisis magna etiam. Quis vel eros donec ac odio tempor orci dapibus. Semper risus in hendrerit gravida. Est velit egestas dui id ornare. Volutpat ac tincidunt vitae semper quis lectus nulla. Egestas congue quisque egestas diam in. Nisi porta lorem mollis aliquam. Volutpat sed cras ornare arcu dui vivamus arcu felis bibendum. Ipsum consequat nisl vel pretium. Purus gravida quis blandit turpis cursus in hac habitasse. Non pulvinar neque laoreet suspendisse interdum consectetur libero. Tortor at auctor urna nunc id.</p>
<p>Facilisi etiam dignissim diam quis enim lobortis scelerisque fermentum. Pharetra convallis posuere morbi leo urna molestie at. Purus gravida quis blandit turpis cursus. Mattis rhoncus urna neque viverra justo. Amet cursus sit amet dictum sit. Et ultrices neque ornare aenean euismod elementum. Gravida rutrum quisque non tellus. Porttitor leo a diam sollicitudin tempor id eu. Mattis nunc sed blandit libero volutpat sed cras ornare. Aenean euismod elementum nisi quis. A cras semper auctor neque vitae tempus quam. In metus vulputate eu scelerisque felis. Diam phasellus vestibulum lorem sed risus ultricies tristique nulla. Sit amet nisl purus in mollis nunc sed id semper. Venenatis tellus in metus vulputate eu scelerisque felis. Sollicitudin tempor id eu nisl nunc mi ipsum faucibus vitae. Arcu bibendum at varius vel pharetra vel. Phasellus vestibulum lorem sed risus.</p>
                        "
                },
            });
        }
    }
}
