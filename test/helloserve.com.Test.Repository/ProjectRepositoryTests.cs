using helloserve.com.Database;
using helloserve.com.Database.Entities;
using helloserve.com.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace helloserve.com.Test.Repository
{
    [TestClass]
    public class ProjectRepositoryTests : RepositoryTests<IProjectDatabaseAdaptor>
    {
        [TestMethod]
        public async Task ReadAll_Verify()
        {
            //arrange
            ArrangeDatabase("ProjectRepository_ReadAll_Verify");
            List<Project> projects = new List<Project>()
            {
                new Project() { Key = "key1", Name = "project1", ComponentPage = "project_1", IsActive = true, SortOrder = 2 },
                new Project() { Key = "key2", Name = "project2", ComponentPage = "project_2", IsActive = true, SortOrder = 3 },
                new Project() { Key = "key3", Name = "project3", ComponentPage = "project_3", IsActive = false, SortOrder = 1 }
            };
            using (var context = new helloserveContext(Options))
            {
                context.Projects.AddRange(projects);
                await context.SaveChangesAsync();
            }

            //act
            IEnumerable<Domain.Models.Project> result = await Repository.ReadAll();

            //assert
            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.Any(x => x.Key == "key3"));
            Assert.AreEqual(2, result.ToList()[0].SortOrder);
            Assert.AreEqual(3, result.ToList()[1].SortOrder);
        }

        [TestMethod]
        public async Task Read_Verify()
        {
            //arrange
            ArrangeDatabase("ProjectRepository_Read_Verify");
            string key = "key";
            Project project = new Project()
            {
                Key = key,
                Name = "project",
                ComponentPage = "project",
                IsActive = false
            };
            using (var context = new helloserveContext(Options))
            {
                context.Projects.Add(project);
                await context.SaveChangesAsync();
            }

            //act
            var result = await Repository.Read(key);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(key, result.Key);
        }
    }
}
