using DeepEqual.Syntax;
using FreeMarket.Controllers;
using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using Moq;
using ProductModule.Domain;
using TestHelper;

namespace FreeMarketTests
{
    [TestClass]
    public sealed class ProductControllerTests
    {
        Mock<IService<Product>> serviceMock = new();
        [TestInitialize]
        public void Initialize()
        {
            serviceMock=new Mock<IService<Product>>(MockBehavior.Strict);
        }

        [TestMethod]
        public void Get()
        {
            List<Product> products = new List<Product>([Factories.NewProduct(), Factories.NewProduct()]);
            serviceMock.Setup(x=>x.FindAll()).ReturnsAsync(ServiceResponse<List<Product>>.Send(products));

            ProductController controller = new(serviceMock.Object);
            var result = controller.Get();
            Assert.IsNotNull(result);

            products.IsDeepEqual(result);

            serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetById()
        {
            Product product = Factories.NewProduct();
            serviceMock.Setup(x => x.FindOne(product.Id)).ReturnsAsync(ServiceResponse<Product>.Send(product));

            ProductController controller = new(serviceMock.Object);
            var result = controller.Get(product.Id);
            Assert.IsNotNull(result);

            product.IsDeepEqual(result);

            serviceMock.VerifyAll();
        }
    }
}
