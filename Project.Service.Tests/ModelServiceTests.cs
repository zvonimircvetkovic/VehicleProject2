using FluentAssertions;
using Moq;
using Project.Common.Filter;
using Project.Model.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project.Service.Tests
{
    public class ModelServiceTests
    {
        public readonly IModelService _mockModelService;

        public ModelServiceTests()
        {
            Mock<IModelService> mockModelService = new Mock<IModelService>();

            IEnumerable<IModel> models = new List<Model.Model> {
                new Model.Model { Id = 1, MakeId = 1, Name = "A3", Abrv = "A3" },
                new Model.Model { Id = 2, MakeId = 1, Name = "A6", Abrv = "A6" },
                new Model.Model { Id = 3, MakeId = 2, Name = "3", Abrv = "3" },
                new Model.Model { Id = 4, MakeId = 2, Name = "5", Abrv = "5" }};

            var updateList = models.ToList();

            // return all makes
            mockModelService.Setup(mr => mr.GetAllByMakeIdAsync(It.IsAny<int>(), It.IsAny<PageModel>(), It.IsAny<SearchModel>(), It.IsAny<SortModel>())).
                ReturnsAsync((int i, PageModel page, SearchModel search, SortModel sort) => 
                {
                    updateList = updateList.Where(x => x.MakeId == i).ToList();
                    int count = updateList.Count();

                    var pagedList = new PagedList<IModel>(updateList, count, It.IsAny<int>(), It.IsAny<int>());

                    return pagedList;
                });

            // return a make by Id
            mockModelService.Setup(mr => mr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => updateList.Where(x => x.Id == i).FirstOrDefault());


            // Allows us to test saving a product
            mockModelService.Setup(mr => mr.AddAsync(It.IsAny<IModel>())).Returns(
                (IModel target) =>
                {
                    if (target.Id.Equals(default(int)))
                    {
                        target.Id = models.Count() + 1;
                        updateList.Add(target);
                    }

                    else
                    {
                        var original = models.Where(q => q.Id == target.Id).Single();
                        if (original == null)
                        {
                            return Task.FromResult(false);
                        }

                        original.Id = target.Id;
                        original.MakeId = target.MakeId;
                        original.Name = target.Name;
                        original.Abrv = target.Abrv;
                    }

                    return Task.FromResult(true);
                });

            mockModelService.Setup(mr => mr.UpdateAsync(It.IsAny<IModel>()));

            mockModelService.Setup(mr => mr.RemoveAsync(It.IsAny<IModel>())).Callback<IModel>((entity) => updateList.Remove(entity));

            _mockModelService = mockModelService.Object;
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

            PagedList<IModel> testModels = _mockModelService.GetAllByMakeIdAsync(1, page, search, sort).Result;

            testModels.Should().NotBeNull();

            testModels.Items.Count.Should().Be(2);
        }

        [Fact]
        public void GetById()
        {
            IModel testModel = _mockModelService.GetByIdAsync(2).Result;

            testModel.Should().NotBeNull();
            testModel.Id.Should().Be(2);
        }

        [Fact]
        public void Add()
        {
            IModel newModel = new Model.Model
            {
                MakeId = 3,
                Name = "E Class",
                Abrv = "E"
            };

            _mockModelService.AddAsync(newModel);

            IModel testModel = _mockModelService.GetByIdAsync(5).Result;

            testModel.Name.Should().BeEquivalentTo("E Class");
        }
        [Fact]
        public void Update()
        {
            IModel testModel = _mockModelService.GetByIdAsync(3).Result;

            testModel.Abrv = "7";

            _mockModelService.UpdateAsync(testModel);

            IModel getModel = _mockModelService.GetByIdAsync(3).Result;

            getModel.Abrv.Should().BeEquivalentTo("7");
        }

        [Fact]
        public void Remove()
        {
            IModel testModel = _mockModelService.GetByIdAsync(3).Result;

            _mockModelService.RemoveAsync(testModel);

            testModel = _mockModelService.GetByIdAsync(3).Result;

            testModel.Should().BeNull();
        }
    }
}
