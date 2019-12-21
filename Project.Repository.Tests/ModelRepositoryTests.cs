using FluentAssertions;
using Moq;
using Project.DAL.Entities;
using Project.Repository.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Project.Repository.Tests
{
    public class ModelRepositoryTests
    {
        public readonly IModelRepository _mockModelRepository;

        public ModelRepositoryTests()
        {
            Mock<IModelRepository> mockModelRepository = new Mock<IModelRepository>();

            IEnumerable<IModelEntity> models = new List<ModelEntity> {
                new ModelEntity { Id = 1, MakeId = 1, Name = "A3", Abrv = "A3" },
                new ModelEntity { Id = 2, MakeId = 1, Name = "A6", Abrv = "A6" },
                new ModelEntity { Id = 3, MakeId = 2, Name = "3", Abrv = "3" },
                new ModelEntity { Id = 4, MakeId = 2, Name = "5", Abrv = "5" }};

            var updateList = models.ToList();

            // return all makes
            mockModelRepository.Setup(mr => mr.GetAllByMakeIdAsync(It.IsAny<int>())).Returns((int i) => updateList.Where(x => x.MakeId == i).ToList().AsQueryable());

            // return a make by Id
            mockModelRepository.Setup(mr => mr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => updateList.Where(x => x.Id == i).FirstOrDefault());


            // Allows us to test saving a product
            mockModelRepository.Setup(mr => mr.Add(It.IsAny<IModelEntity>())).Returns(
                (IModelEntity target) =>
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

            mockModelRepository.Setup(mr => mr.Update(It.IsAny<IModelEntity>()));

            mockModelRepository.Setup(mr => mr.Remove(It.IsAny<IModelEntity>())).Callback<IModelEntity>((entity) => updateList.Remove(entity));

            _mockModelRepository = mockModelRepository.Object;
        }

        [Fact]
        public void GetAll()
        {
            IEnumerable<IModelEntity> testModels = _mockModelRepository.GetAllByMakeIdAsync(1);

            testModels.Should().NotBeNull();

            testModels.ToList().Count.Should().Be(2);
        }

        [Fact]
        public void GetById()
        {
            IModelEntity testModel = _mockModelRepository.GetByIdAsync(2).Result;

            testModel.Should().NotBeNull();
            testModel.Id.Should().Be(2);
        }

        [Fact]
        public void Add()
        {
            IModelEntity newModel = new ModelEntity
            {
                MakeId = 3,
                Name = "E Class",
                Abrv = "E"
            };

            _mockModelRepository.Add(newModel);

            int modelCount = _mockModelRepository.GetAllByMakeIdAsync(3).Count();

            modelCount.Should().Be(1);

            IModelEntity testModel = _mockModelRepository.GetByIdAsync(5).Result;

            testModel.Name.Should().BeEquivalentTo("E Class");
        }
        [Fact]
        public void Update()
        {
            IModelEntity testModel = _mockModelRepository.GetByIdAsync(3).Result;

            testModel.Abrv = "7";

            _mockModelRepository.Update(testModel);

            IModelEntity getModel = _mockModelRepository.GetByIdAsync(3).Result;

            getModel.Abrv.Should().BeEquivalentTo("7");
        }

        [Fact]
        public void Remove()
        {
            IModelEntity testModel = _mockModelRepository.GetByIdAsync(3).Result;

            _mockModelRepository.Remove(testModel);

            testModel = _mockModelRepository.GetByIdAsync(3).Result;

            testModel.Should().BeNull();
        }
    }
}
