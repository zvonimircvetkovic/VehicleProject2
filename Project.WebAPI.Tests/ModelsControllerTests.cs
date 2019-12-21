using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Project.Common.Filter;
using Project.Service.Common;
using Project.WebAPI.Controllers;
using Project.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Project.WebAPI.Tests
{
    public class ModelsControllerTests
    {
        public readonly IModelService _mockModelService;

        public ModelsControllerTests()
        {
            Mock<IModelService> mockModelService = new Mock<IModelService>();

            IEnumerable<Model.Common.IModel> models = new List<Project.Model.Model> {
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

                    var pagedList = new PagedList<Model.Common.IModel>(updateList, count, page.PageNumber, page.PageSize);

                    return pagedList;
                });

            // return a make by Id
            mockModelService.Setup(mr => mr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => updateList.Where(x => x.Id == i).FirstOrDefault());


            // Allows us to test saving a product
            mockModelService.Setup(mr => mr.AddAsync(It.IsAny<Model.Common.IModel>())).Returns(
                (Model.Common.IModel target) =>
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

            mockModelService.Setup(mr => mr.UpdateAsync(It.IsAny<Model.Common.IModel>()));

            mockModelService.Setup(mr => mr.RemoveAsync(It.IsAny<Model.Common.IModel>())).Callback<Model.Common.IModel>((entity) => updateList.Remove(entity));

            _mockModelService = mockModelService.Object;
        }

        [Fact]
        public async Task GetAll()
        {
            var controller = new ModelsController(_mockModelService, AutomapperSingleton.Mapper);

            var httpContext = new Mock<HttpContext>(MockBehavior.Strict);
            var response = new Mock<HttpResponse>(MockBehavior.Strict);
            var headers = new HeaderDictionary();

            response.Setup(x => x.Headers).Returns(headers);
            httpContext.SetupGet(x => x.Response).Returns(response.Object);
            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

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


            var checkResult = await controller.Index(1, page, search, sort);

            checkResult.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetMake()
        {
            var controller = new ModelsController(_mockModelService, AutomapperSingleton.Mapper);

            var checkResult = await controller.GetModel(1);

            checkResult.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Create()
        {
            var controller = new ModelsController(_mockModelService, AutomapperSingleton.Mapper);

            ViewModel newModel = new ViewModel
            {
                Name = "Renault",
                Abrv = "Rnlt"
            };

            var checkResult = await controller.CreateConfirmed(newModel);

            checkResult.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Update()
        {
            var controller = new ModelsController(_mockModelService, AutomapperSingleton.Mapper);

            IActionResult actionResult = await controller.GetModel(3);

            var result = actionResult as OkObjectResult;

            var model = result.Value as Model.Common.IModel;

            model.Abrv = "MS";

            var updateModel = AutomapperSingleton.Mapper.Map<ViewModel>(model);

            var checkResult = await controller.EditConfirmed(updateModel);

            checkResult.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete()
        {
            var controller = new ModelsController(_mockModelService, AutomapperSingleton.Mapper);

            IActionResult actionResult = await controller.GetModel(3);

            var result = actionResult as OkObjectResult;

            var model = result.Value as Model.Common.IModel;

            var deleteModel = AutomapperSingleton.Mapper.Map<ViewModel>(model);

            var checkResult = await controller.DeleteConfirmed(deleteModel);

            checkResult.Should().BeOfType<NoContentResult>();
        }
    }
}
