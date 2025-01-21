using CartModule.Domain;
using CartModule.Infrastructure;
using DeepEqual.Syntax;
using FreeMarket.Domain.Interfaces;
using Moq;
using TestHelper;

namespace CartModuleTests
{
    [TestClass]
    public sealed class CartRepositoryTests
    {
        private Mock<IRepository<Cart>> mockRepository = new();
        private Mock<IModuleRepository<CartItem>> mockCartItemRepository = new();

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new Mock<IRepository<Cart>>(MockBehavior.Strict);
            mockCartItemRepository = new Mock<IModuleRepository<CartItem>>();
        }

        [TestMethod]
        public async Task GetAll()
        {
            List<Cart> dto = [Factories.NewCart(), Factories.NewCart()];
            List<CartItem> items = [Factories.NewCartItem(), Factories.NewCartItem()];

            mockRepository.Setup(x => x.GetAll("carts")).ReturnsAsync(dto);
            mockCartItemRepository.Setup(x => x.GetAll()).ReturnsAsync(items);

            CartRepository repository = new(mockRepository.Object, mockCartItemRepository.Object);

            var result = await repository.GetAll();
            Assert.IsNotNull(result);

            dto.ForEach(cart =>
            {
                cart.Products = items;
                cart.IsDeepEqual(result.Find(x => x.Id == cart.Id));
            });

            mockRepository?.VerifyAll();
            mockCartItemRepository?.VerifyAll();
        }

        [TestMethod]
        public async Task GetAllBy()
        {
            List<Cart> dto = [Factories.NewCart()];
            List<CartItem> items = [Factories.NewCartItem(), Factories.NewCartItem()];

            mockRepository.Setup(x => x.GetAllBy("carts", dto[0].Id)).ReturnsAsync(dto);
            mockCartItemRepository.Setup(x => x.GetAllBy(dto[0].Id)).ReturnsAsync(items);

            CartRepository repository = new(mockRepository.Object, mockCartItemRepository.Object);

            var result = await repository.GetAllBy(dto[0].Id);
            Assert.IsNotNull(result);

            dto.ForEach(cart =>
            {
                cart.Products = items;
                cart.IsDeepEqual(result.Find(x => x.Id == cart.Id));
            });

            mockRepository?.VerifyAll();
            mockCartItemRepository?.VerifyAll();
        }

        [TestMethod]
        public async Task GetOne()
        {
            Cart dto = Factories.NewCart();
            List<CartItem> items = [Factories.NewCartItem(), Factories.NewCartItem()];

            mockRepository.Setup(x => x.GetOne("carts",dto.Id)).ReturnsAsync(dto);
            mockCartItemRepository.Setup(x => x.GetAllBy(dto.Id)).ReturnsAsync(items);

            CartRepository repository = new(mockRepository.Object, mockCartItemRepository.Object);

            var result = await repository.GetOne(dto.Id);
            Assert.IsNotNull(result);

            dto.Products = items;

            dto.IsDeepEqual(result);
            mockRepository?.VerifyAll();
            mockCartItemRepository?.VerifyAll();
        }

        [TestMethod]
        public async Task Upsert()
        {
            Cart dto = Factories.NewCart();
            List<CartItem> items = [Factories.NewCartItem(), Factories.NewCartItem()];
            dto.Products = items;

            mockRepository.Setup(x => x.Upsert("carts", dto)).ReturnsAsync(dto);
            mockRepository.Setup(x => x.GetOne("carts", dto.Id)).ReturnsAsync(dto);
            mockCartItemRepository.Setup(x => x.Delete(dto.Id));
            mockCartItemRepository.Setup(x => x.Upsert(It.IsAny<CartItem>()));
            mockCartItemRepository.Setup(x => x.GetAllBy(dto.Id)).ReturnsAsync(items);

            CartRepository repository = new(mockRepository.Object, mockCartItemRepository.Object);

            var result = await repository.Upsert(dto);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            mockRepository?.VerifyAll();
            mockCartItemRepository?.VerifyAll();
        }

        [TestMethod]
        public void Delete()
        {
            Cart dto = Factories.NewCart();
            List<CartItem> items = [Factories.NewCartItem(), Factories.NewCartItem()];
            dto.Products = items;

            mockRepository.Setup(x => x.Delete("carts", dto.Id));
            mockCartItemRepository.Setup(x => x.Delete(dto.Id));

            CartRepository repository = new(mockRepository.Object, mockCartItemRepository.Object);

            repository.Delete(dto.Id);
            
            mockRepository?.VerifyAll();
            mockCartItemRepository?.VerifyAll();
        }
    }
}