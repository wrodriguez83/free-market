using CartModule.Domain;
using DeepEqual.Syntax;
using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using ProductModule.Domain;
using TestHelper;

namespace CartModuleTests
{
    [TestClass]
    public sealed class SqlClientCartItemRepositoryTests
    {
        private readonly IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private Mock<IService<Product>> mockProductService = new();

        [TestInitialize]
        public void TestInitialize()
        {
            mockProductService = new Mock<IService<Product>>();
        }

        [TestMethod]
        public async Task GetAll()
        {
            List<CartItem> dto = new([Factories.NewCartItem(), Factories.NewCartItem()]);
            SqlClientCartItemRepositoryMock.Data = dto;

            dto.ForEach(item => mockProductService.Setup(x => x.FindOne(item.Product.Id)).ReturnsAsync(ServiceResponse<Product>.Send(item.Product)));
            SqlClientCartItemRepositoryMock repository = new(mockProductService.Object, configuration);

            var result = await repository.GetAll("cartItems");
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);

            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(0, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("get_cartItems", repository.SqlCommand?.CommandText);
            mockProductService.VerifyAll();
        }
        [TestMethod]
        public async Task GetAllBy()
        {
            List<CartItem> dto = new([Factories.NewCartItem(), Factories.NewCartItem()]);
            dto.ForEach(item => mockProductService.Setup(x => x.FindOne(item.Product.Id)).ReturnsAsync(ServiceResponse<Product>.Send(item.Product)));

            SqlClientCartItemRepositoryMock.Data = dto;

            SqlClientCartItemRepositoryMock repository = new(mockProductService.Object, configuration);

            var result = await repository.GetAllBy("cartItems", dto[0].CartId);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);

            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(1, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("@cartId", repository.SqlCommand?.Parameters[0].ParameterName);
            Assert.AreEqual(dto[0].CartId, repository.SqlCommand?.Parameters[0].Value);
            Assert.AreEqual("get_cartItems", repository.SqlCommand?.CommandText);
            mockProductService.VerifyAll();
        }

        [TestMethod]
        public async Task GetOne()
        {
            CartItem dto = Factories.NewCartItem();
            mockProductService.Setup(x => x.FindOne(dto.Product.Id)).ReturnsAsync(ServiceResponse<Product>.Send(dto.Product));

            SqlClientCartItemRepositoryMock.Data = [dto];

            SqlClientCartItemRepositoryMock repository = new(mockProductService.Object, configuration);

            var result = await repository.GetOne("cartItems", dto.CartId);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(1, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("@cartId", repository.SqlCommand?.Parameters[0].ParameterName);
            Assert.AreEqual(dto.CartId, repository.SqlCommand?.Parameters[0].Value);
            Assert.AreEqual("get_cartItems", repository.SqlCommand?.CommandText);
            mockProductService.VerifyAll();
        }

        [TestMethod]
        public async Task Upsert()
        {
            CartItem dto = Factories.NewCartItem();
            SqlClientCartItemRepositoryMock.Data = [dto];

            SqlClientCartItemRepositoryMock repository = new(mockProductService.Object, configuration);

            var result = await repository.Upsert("cartItems", dto);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(5, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("@id", repository.SqlCommand?.Parameters[0].ParameterName);
            Assert.AreEqual(dto.Id, repository.SqlCommand?.Parameters[0].Value);
            Assert.AreEqual("@productId", repository.SqlCommand?.Parameters[1].ParameterName);
            Assert.AreEqual(dto.Product?.Id, repository.SqlCommand?.Parameters[1].Value);
            Assert.AreEqual("@quantity", repository.SqlCommand?.Parameters[2].ParameterName);
            Assert.AreEqual(dto.Quantity, repository.SqlCommand?.Parameters[2].Value);
            Assert.AreEqual("@price", repository.SqlCommand?.Parameters[3].ParameterName);
            Assert.AreEqual(dto.Price, repository.SqlCommand?.Parameters[3].Value);
            Assert.AreEqual("@cartId", repository.SqlCommand?.Parameters[4].ParameterName);
            Assert.AreEqual(dto.CartId, repository.SqlCommand?.Parameters[4].Value);
            Assert.AreEqual("upsert_cartItems", repository.SqlCommand?.CommandText);
        }

        [TestMethod]
        public void Delete()
        {
            CartItem dto = Factories.NewCartItem();
            SqlClientCartItemRepositoryMock.Data = [dto];

            SqlClientCartItemRepositoryMock repository = new(mockProductService.Object, configuration);

            repository.Delete("cartItems", dto.CartId);

            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(1, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("@cartId", repository.SqlCommand?.Parameters[0].ParameterName);
            Assert.AreEqual(dto.CartId, repository.SqlCommand?.Parameters[0].Value);
            Assert.AreEqual("delete_cartItems", repository.SqlCommand?.CommandText);
        }
    }
}