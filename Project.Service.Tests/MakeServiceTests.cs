using FluentAssertions;
using Moq;
using Project.Common.Filter;
using Project.Model;
using Project.Model.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Project.Service.Tests
{
    public class MakeServiceTests
    {
        public readonly IMakeService _mockMakeService;

        public MakeServiceTests()
        {
            Mock<IMakeService> mockMakeService = new Mock<IMakeService>();

            IEnumerable<IMake> makes = new List<Make> {
                new Make { Id = 1, Name = "Audi", Abrv = "Audi" },
                new Make { Id = 2, Name = "BMW", Abrv = "BMW" },
                new Make { Id = 3, Name = "Mercedes", Abrv = "Mcds" },
                new Make { Id = 4, Name = "Mazda", Abrv = "Mzda" }};

            var updateList = makes.ToList();

            mockMakeService.Setup(ms => ms.GetAllAsync(It.IsAny<PageModel>(), It.IsAny<SearchModel>(), It.IsAny<SortModel>()))
                .ReturnsAsync(new PagedList<IMake>(updateList, updateList.Count(), It.IsAny<int>(), It.IsAny<int>()));

            mockMakeService.Setup(ms => ms.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => updateList.Where(x => x.Id == i).FirstOrDefault());

            mockMakeService.Setup(ms => ms.AddAsync(It.IsAny<IMake>())).Returns(
                (IMake target) =>
                {
                    if (target.Id.Equals(default(int)))
                    {
                        target.Id = makes.Count() + 1;
                        updateList.Add(target);
                    }

                    else
                    {
                        var original = makes.Where(q => q.Id == target.Id).Single();
                        if (original == null)
                        {
                            return Task.FromResult(false);
                        }

                        original.Id = target.Id;
                        original.Name = target.Name;
                        original.Abrv = target.Abrv;
                    }

                    return Task.FromResult(true);
                });

            mockMakeService.Setup(ms => ms.UpdateAsync(It.IsAny<IMake>()));

            mockMakeService.Setup(ms => ms.RemoveAsync(It.IsAny<IMake>())).Callback<IMake>((entity) => updateList.Remove(entity));

            _mockMakeService = mockMakeService.Object;
        }

        [Fact]
        public void GetAll()
        {
            PageModel page = new PageModel
            {
                PageNumber = 1,
                PageSize = 2
            };

            SearchModel search = new SearchModel
            {
                SearchString = ""
            };

            SortModel sort = new SortModel
            {
                SortOrder = ""
            };

            PagedList<IMake> testMakes = _mockMakeService.GetAllAsync(page, search, sort).Result;

            testMakes.Should().NotBeNull();

            testMakes.Items.Count.Should().Be(4);
        }

        [Fact]
        public void GetById()
        {
            IMake testMake = _mockMakeService.GetByIdAsync(2).Result;

            testMake.Should().NotBeNull();
            testMake.Id.Should().Be(2);
        }

        [Fact]
        public void Add()
        {
            IMake newMake = new Make
            {
                Name = "Renault",
                Abrv = "Rnlt"
            };

            _mockMakeService.AddAsync(newMake);

            IMake testMake = _mockMakeService.GetByIdAsync(5).Result;

            testMake.Name.Should().BeEquivalentTo("Renault");
        }
        [Fact]
        public void Update()
        {
            IMake testMake = _mockMakeService.GetByIdAsync(3).Result;

            testMake.Abrv = "MS";

            _mockMakeService.UpdateAsync(testMake);

            IMake getMake = _mockMakeService.GetByIdAsync(3).Result;

            getMake.Abrv.Should().BeEquivalentTo("MS");
        }

        [Fact]
        public void Remove()
        {
            IMake testMake = _mockMakeService.GetByIdAsync(3).Result;

            _mockMakeService.RemoveAsync(testMake);

            testMake = _mockMakeService.GetByIdAsync(3).Result;

            testMake.Should().BeNull();
        }
    }
}
