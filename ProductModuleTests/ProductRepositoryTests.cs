using DeepEqual.Syntax;
using FreeMarket.Domain.Interfaces;
using Moq;
using ProductModule.Domain;
using ProductModule.Infrastructure;
using TestHelper;

namespace ProductModuleTests
{
    [TestClass]
    public sealed class ProductRepositoryTests
    {
        private Mock<IRepository<Product>> mockFakeApiRepository = new();

        [TestInitialize]
        public void TestInitialize()
        {
            mockFakeApiRepository = new Mock<IRepository<Product>>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task GetAll()
        {
            List<Product> dto = new([Factories.NewProduct(), Factories.NewProduct()]);

            mockFakeApiRepository.Setup(x => x.GetAll("products")).ReturnsAsync(dto);

            ProductRepository repository = new(mockFakeApiRepository.Object);

            var result = await repository.GetAll();
            Assert.IsNotNull(result);

            dto.ForEach(item => item.IsDeepEqual(result.Find(x => x.Id == item.Id)));

            mockFakeApiRepository?.VerifyAll();
        }
        [TestMethod]
        public void GetAllBy()
        {
            List<Product> dto = new([Factories.NewProduct(), Factories.NewProduct()]);

            ProductRepository repository = new(mockFakeApiRepository.Object);
            Assert.ThrowsException<NotImplementedException>(() => repository.GetAllBy(dto[0].Id));
        }

        [TestMethod]
        public async Task GetOne()
        {
            Product dto = Factories.NewProduct();

            mockFakeApiRepository.Setup(x => x.GetOne("products", dto.Id)).ReturnsAsync(dto);

            ProductRepository repository = new(mockFakeApiRepository.Object);
            var result = await repository.GetOne(dto.Id);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            mockFakeApiRepository?.VerifyAll();
        }

        [TestMethod]
        public void Upsert()
        {
            Product dto = Factories.NewProduct();

            ProductRepository repository = new(mockFakeApiRepository.Object);
            Assert.ThrowsException<NotImplementedException>(() => repository.Upsert(dto));
        }

        [TestMethod]
        public void Delete()
        {
            Product dto = Factories.NewProduct();

            ProductRepository repository = new(mockFakeApiRepository.Object);
            Assert.ThrowsException<NotImplementedException>(() => repository.Delete(dto.Id));
        }
    }
}