using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Project.Common.Filter;
using Project.Model;
using Project.Model.Common;
using Project.Service.Common;
using Project.WebAPI.Controllers;
using Project.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Project.WebAPI.Tests
{
    public class MakesControllerTests
    {
        public readonly IMakeService _mockMakeService;

        public MakesControllerTests()
        {
            Mock<IMakeService> mockMakeService = new Mock<IMakeService>();

            IEnumerable<IMake> makes = new List<Make> {
                new Make { Id = 1, Name = "Audi", Abrv = "Audi" },
                new Make { Id = 2, Name = "BMW", Abrv = "BMW" },
                new Make { Id = 3, Name = "Mercedes", Abrv = "Mcds" },
                new Make { Id = 4, Name = "Mazda", Abrv = "Mzda" }};

            var updateList = makes.ToList();

            mockMakeService.Setup(ms => ms.GetAllAsync(It.IsAny<PageModel>(), It.IsAny<SearchModel>(), It.IsAny<SortModel>()))
                .ReturnsAsync((PageModel page, SearchModel search, SortModel sort) =>
                {
                    int count = updateList.Count();

                    var pagedList = new PagedList<IMake>(updateList, count, page.PageNumber, page.PageSize);

                    return pagedList;
                });

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
        public async Task GetAll()
        {
            var controller = new MakesController(_mockMakeService, AutomapperSingleton.Mapper);

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

            var checkResult = await controller.Index(page, search, sort);

            checkResult.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetMake()
        {
            var controller = new MakesController(_mockMakeService, AutomapperSingleton.Mapper);

            var checkResult = await controller.GetMake(1);

            checkResult.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Create()
        {
            var controller = new MakesController(_mockMakeService, AutomapperSingleton.Mapper);

            ViewMake newMake = new ViewMake
            {
                Name = "Renault",
                Abrv = "Rnlt"
            };

            var checkResult = await controller.CreateConfirmed(newMake);

            checkResult.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Update()
        {
            var controller = new MakesController(_mockMakeService, AutomapperSingleton.Mapper);

            IActionResult actionResult = await controller.GetMake(3);

            var result = actionResult as OkObjectResult;

            var make = result.Value as IMake;

            make.Abrv = "MS";

            var updateMake = AutomapperSingleton.Mapper.Map<ViewMake>(make);

            var checkResult = await controller.EditConfirmed(updateMake);

            checkResult.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete()
        {
            var controller = new MakesController(_mockMakeService, AutomapperSingleton.Mapper);

            IActionResult actionResult = await controller.GetMake(3);

            var result = actionResult as OkObjectResult;

            var make = result.Value as IMake;

            var deleteMake = AutomapperSingleton.Mapper.Map<ViewMake>(make);

            var checkResult = await controller.DeleteConfirmed(deleteMake);

            checkResult.Should().BeOfType<NoContentResult>();
        }
    }
}
