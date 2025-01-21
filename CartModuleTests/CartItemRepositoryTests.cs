using CartModule.Domain;
using CartModule.Infrastructure;
using DeepEqual.Syntax;
using FreeMarket.Domain.Interfaces;
using Moq;
using TestHelper;

namespace CartModuleTests
{
    [TestClass]
    public sealed class CartItemRepositoryTests
    {
        private Mock<IRepository<CartItem>> mockRepository = new();

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new Mock<IRepository<CartItem>>();
        }

        [TestMethod]
        public async Task GetAll()
        {
            List<CartItem> dto = [Factories.NewCartItem(), Factories.NewCartItem()];

            mockRepository.Setup(x => x.GetAll("cartItems")).ReturnsAsync(dto);

            CartItemRepository repository = new(mockRepository.Object);

            var result = await repository.GetAll();
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            
            mockRepository?.VerifyAll();
        }

        [TestMethod]
        public async Task GetAllBy()
        {
            List<CartItem> dto = [Factories.NewCartItem(), Factories.NewCartItem()];

            mockRepository.Setup(x => x.GetAllBy("cartItems", dto[0].CartId)).ReturnsAsync(dto.FindAll(x => x.CartId == dto[0].CartId));

            CartItemRepository repository = new(mockRepository.Object);

            var result = await repository.GetAllBy(dto[0].CartId);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);

            mockRepository?.VerifyAll();
        }

        [TestMethod]
        public void GetOne()
        {
            List<CartItem> dto = [Factories.NewCartItem(), Factories.NewCartItem()];

            CartItemRepository repository = new(mockRepository.Object);

            Assert.ThrowsException<NotImplementedException>(() => repository.GetOne(dto[0].Id));
        }

        [TestMethod]
        public async Task Upsert()
        {
            CartItem dto = Factories.NewCartItem();

            mockRepository.Setup(x => x.Upsert("cartItems", dto)).ReturnsAsync(dto);

            CartItemRepository repository = new(mockRepository.Object);

            var result = await repository.Upsert(dto);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            mockRepository?.VerifyAll();
        }

        [TestMethod]
        public void Delete()
        {
            CartItem dto = Factories.NewCartItem();

            mockRepository.Setup(x => x.Delete("cartItems", dto.Id));

            CartItemRepository repository = new(mockRepository.Object);

            repository.Delete(dto.Id);
            
            mockRepository?.VerifyAll();
        }
    }
}