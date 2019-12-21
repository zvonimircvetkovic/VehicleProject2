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
    public class MakeRepositoryTests
    {
        public readonly IMakeRepository _mockMakeRepository;

        public MakeRepositoryTests()
        {
            Mock<IMakeRepository> mockMakeRepository = new Mock<IMakeRepository>();

            IEnumerable<IMakeEntity> makes = new List<MakeEntity> {
                new MakeEntity { Id = 1, Name = "Audi", Abrv = "Audi" },
                new MakeEntity { Id = 2, Name = "BMW", Abrv = "BMW" },
                new MakeEntity { Id = 3, Name = "Mercedes", Abrv = "Mcds" },
                new MakeEntity { Id = 4, Name = "Mazda", Abrv = "Mzda" }};

            var updateList = makes.ToList();

            // return all makes
            mockMakeRepository.Setup(mr => mr.GetAllAsync()).Returns(updateList.AsQueryable());

            // return a make by Id
            mockMakeRepository.Setup(mr => mr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => updateList.Where(x => x.Id == i).FirstOrDefault());


            // Allows us to test saving a product
            mockMakeRepository.Setup(mr => mr.Add(It.IsAny<IMakeEntity>())).Returns(
                (IMakeEntity target) =>
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

            mockMakeRepository.Setup(mr => mr.Update(It.IsAny<IMakeEntity>()));

            mockMakeRepository.Setup(mr => mr.Remove(It.IsAny<IMakeEntity>())).Callback<IMakeEntity>((entity) => updateList.Remove(entity));

            _mockMakeRepository = mockMakeRepository.Object;
        }

        [Fact]
        public void GetAll()
        {
            IEnumerable<IMakeEntity> testMakes = _mockMakeRepository.GetAllAsync();

            testMakes.Should().NotBeNull();

            testMakes.ToList().Count.Should().Be(4);
        }

        [Fact]
        public void GetById()
        {
            IMakeEntity testMake = _mockMakeRepository.GetByIdAsync(2).Result;

            testMake.Should().NotBeNull();
            testMake.Id.Should().Be(2);
        }

        [Fact]
        public void Add()
        {
            IMakeEntity newMake = new MakeEntity
            {
                Name = "Renault",
                Abrv = "Rnlt"
            };

            _mockMakeRepository.Add(newMake);

            int makeCount = _mockMakeRepository.GetAllAsync().Count();

            makeCount.Should().Be(5);

            IMakeEntity testMake = _mockMakeRepository.GetByIdAsync(5).Result;

            testMake.Name.Should().BeEquivalentTo("Renault");
        }
        [Fact]
        public void Update()
        {
            IMakeEntity testMake = _mockMakeRepository.GetByIdAsync(3).Result;

            testMake.Abrv = "MS";

            _mockMakeRepository.Update(testMake);

            IMakeEntity getMake = _mockMakeRepository.GetByIdAsync(3).Result;

            getMake.Abrv.Should().BeEquivalentTo("MS");
        }

        [Fact]
        public void Remove()
        {
            IMakeEntity testMake = _mockMakeRepository.GetByIdAsync(3).Result;

            _mockMakeRepository.Remove(testMake);

            testMake = _mockMakeRepository.GetByIdAsync(3).Result;

            testMake.Should().BeNull();
        }
    }
}
