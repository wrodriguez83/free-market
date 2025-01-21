using CartModule.Domain;
using DeepEqual.Syntax;
using FreeMarket.Controllers;
using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using Moq;
using TestHelper;

namespace FreeMarketTests
{
    [TestClass]
    public sealed class CartControllerTests
    {
        Mock<IService<Cart>> serviceMock = new();
        [TestInitialize]
        public void Initialize()
        {
            serviceMock = new Mock<IService<Cart>>();
        }

        [TestMethod]
        public async Task Get()
        {
            List<Cart> carts = new List<Cart>([Factories.NewCart(true), Factories.NewCart(true)]);
            serviceMock.Setup(x => x.FindAll()).ReturnsAsync(ServiceResponse<List<Cart>>.Send(carts));

            CartController controller = new(serviceMock.Object);
            var result = await controller.Get();
            Assert.IsNotNull(result);

            carts.IsDeepEqual(result);

            serviceMock.VerifyAll();
        }

        [TestMethod]
        public async Task GetById()
        {
            Cart card = Factories.NewCart(true);
            serviceMock.Setup(x => x.FindOne(card.Id)).ReturnsAsync(ServiceResponse<Cart>.Send(card));

            CartController controller = new(serviceMock.Object);
            var result = await controller.Get(card.Id);
            Assert.IsNotNull(result);

            card.IsDeepEqual(result);

            serviceMock.VerifyAll();
        }
        [TestMethod]
        public async Task Upsert()
        {
            Cart card = Factories.NewCart(true);
            serviceMock.Setup(x => x.Upsert(card)).ReturnsAsync(ServiceResponse<Cart>.Send(card));

            CartController controller = new(serviceMock.Object);
            var result = await controller.Upsert(card);
            Assert.IsNotNull(result);

            card.IsDeepEqual(result);

            serviceMock.VerifyAll();
        }
        [TestMethod]
        public void Delete()
        {
            Cart card = Factories.NewCart(true);
            serviceMock.Setup(x => x.Delete(card.Id));

            CartController controller = new(serviceMock.Object);
            controller.Delete(card.Id);

            serviceMock.VerifyAll();
        }
    }
}
