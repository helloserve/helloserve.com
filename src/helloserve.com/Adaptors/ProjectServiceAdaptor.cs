using helloserve.com.Domain;
using helloserve.com.Mappers;
using helloserve.com.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Adaptors
{
    public class ProjectServiceAdaptor : IProjectServiceAdaptor
    {
        readonly IProjectService _service;

        public ProjectServiceAdaptor(IProjectService service)
        {
            _service = service;
        }

        public async Task<ProjectView> Read(string key)
        {
            return (await _service.Read(key)).Map();
        }

        public async Task<IEnumerable<ProjectItemView>> ReadAllActive()
        {
            return (await _service.ReadAllActive()).Map();
        }
    }

    public class MockProjectServiceAdaptor : IProjectServiceAdaptor
    {
        public async Task<ProjectView> Read(string key)
        {
            if (key == "thebluecar")
            {
                return await Task.FromResult(new ProjectView()
                {
                    Key = key,
                    Name = "The Blue Car",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin a massa vel sem ullamcorper luctus at ac dui.",
                    ComponentPage = "TheBlueCar",
                    ImageUrl = "media/20180609_162848.jpg"
                });
            }
            else
            {
                return await Task.FromResult(new ProjectView()
                {
                    Key = key,
                    Name = "This is a project name",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin a massa vel sem ullamcorper luctus at ac dui. Donec eu tellus tincidunt, consectetur ipsum et, tincidunt nisl. Morbi ac urna in turpis pellentesque ultricies. Nunc euismod augue vel diam convallis, nec fringilla orci consequat. Nam mollis condimentum lorem, sit amet luctus erat ullamcorper sit amet. Cras vitae vulputate tellus. Aliquam auctor placerat dui, sed rhoncus libero laoreet vel. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Quisque egestas neque in urna blandit condimentum. Donec quis volutpat nulla. Donec at orci sapien. Maecenas interdum laoreet malesuada.",
                    ComponentPage = null,
                    ImageUrl = "media/20180609_162848.jpg"
                });
            }
        }

        public async Task<IEnumerable<ProjectItemView>> ReadAllActive()
        {
            return await Task.FromResult<IEnumerable<ProjectItemView>>(new List<ProjectItemView>()
            {
                new ProjectItemView()
                {
                    Key = "thebluecar",
                    Name = "The Blue Car",
                    ImageUrl = "media/20180609_162848.jpg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin a massa vel sem ullamcorper luctus at ac dui."
                },
                new ProjectItemView()
                {
                    Key = "testproject2",
                    Name = "Test Project 1",
                    ImageUrl = "media/20180609_162848.jpg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin a massa vel sem ullamcorper luctus at ac dui. Donec eu tellus tincidunt, consectetur ipsum et, tincidunt nisl. Morbi ac urna in turpis pellentesque ultricies. Nunc euismod augue vel diam convallis, nec fringilla orci consequat. Nam mollis condimentum lorem, sit amet luctus erat ullamcorper sit amet. Cras vitae vulputate tellus. Aliquam auctor placerat dui, sed rhoncus libero laoreet vel. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Quisque egestas neque in urna blandit condimentum. Donec quis volutpat nulla. Donec at orci sapien. Maecenas interdum laoreet malesuada."
                },
                new ProjectItemView()
                {
                    Key = "testproject3",
                    Name = "Test Project 1",
                    ImageUrl = "media/20180609_162848.jpg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin a massa vel sem ullamcorper luctus at ac dui. Donec eu tellus tincidunt, consectetur ipsum et, tincidunt nisl. Morbi ac urna in turpis pellentesque ultricies. Nunc euismod augue vel diam convallis, nec fringilla orci consequat."
                }
            });
        }
    }
}
