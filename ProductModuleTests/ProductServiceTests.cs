using System.Net;
using DeepEqual.Syntax;
using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using Moq;
using ProductModule.Application;
using ProductModule.Domain;
using TestHelper;

namespace ProductModuleTests
{
    [TestClass]
    public sealed class ProductServiceTests
    {
        private Mock<IModuleRepository<Product>> mockProductRepository = new();

        [TestInitialize]
        public void TestInitialize()
        {
            mockProductRepository = new Mock<IModuleRepository<Product>>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task FindAll()
        {
            List<Product> dto = new([Factories.NewProduct(), Factories.NewProduct()]);
            ServiceResponse<List<Product>> responseDto = ServiceResponse<List<Product>>.Send(dto);

            mockProductRepository.Setup(x => x.GetAll()).ReturnsAsync(dto);

            ProductService service = new(mockProductRepository.Object);
            var result = await service.FindAll();
            Assert.IsNotNull(result);

            responseDto.IsDeepEqual(result);
            mockProductRepository?.VerifyAll();
        }

        [TestMethod]
        public void FindAllWithHttpException()
        {
            List<Product> dto = new([Factories.NewProduct(), Factories.NewProduct()]);
            ServiceResponse<List<Product>> responseDto = ServiceResponse<List<Product>>.SendError("Test exception", HttpStatusCode.Forbidden);

            mockProductRepository.Setup(x => x.GetAll()).Throws(new HttpRequestException(responseDto.Error, null, responseDto.Status));

            ProductService service = new(mockProductRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.FindAll());

            mockProductRepository?.VerifyAll();
        }

        [TestMethod]
        public void FindAllWithException()
        {
            List<Product> dto = new([Factories.NewProduct(), Factories.NewProduct()]);
            ServiceResponse<List<Product>> responseDto = ServiceResponse<List<Product>>.SendError();

            mockProductRepository.Setup(x => x.GetAll()).Throws<Exception>();

            ProductService service = new(mockProductRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.FindAll());

            mockProductRepository?.VerifyAll();
        }

        [TestMethod]
        public async Task FindOne()
        {
            Product dto = Factories.NewProduct();
            ServiceResponse<Product> responseDto = ServiceResponse<Product>.Send(dto);

            mockProductRepository.Setup(x => x.GetOne(dto.Id)).ReturnsAsync(dto);

            ProductService service = new(mockProductRepository.Object);
            var result = await service.FindOne(dto.Id);
            Assert.IsNotNull(result);

            responseDto.IsDeepEqual(result);
            mockProductRepository?.VerifyAll();
        }

        [TestMethod]
        public void FindOneWithHttpException()
        {
            Product dto = Factories.NewProduct();
            ServiceResponse<Product> responseDto = ServiceResponse<Product>.SendError("Test exception", HttpStatusCode.Forbidden);

            mockProductRepository.Setup(x => x.GetOne(dto.Id)).Throws(new HttpRequestException(responseDto.Error, null, responseDto.Status));

            ProductService service = new(mockProductRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.FindOne(dto.Id));

            mockProductRepository?.VerifyAll();
        }

        [TestMethod]
        public void FindOneWithException()
        {
            Product dto = Factories.NewProduct();
            ServiceResponse<Product> responseDto = ServiceResponse<Product>.SendError();

            mockProductRepository.Setup(x => x.GetOne(dto.Id)).Throws<Exception>();

            ProductService service = new(mockProductRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.FindOne(dto.Id));

            mockProductRepository?.VerifyAll();
        }

        [TestMethod]
        public void Upsert()
        {
            Product dto = Factories.NewProduct();
            ServiceResponse<Product> responseDto = ServiceResponse<Product>.Send(dto);
            mockProductRepository.Setup(x => x.Upsert(dto)).ReturnsAsync(dto);

            ProductService service = new(mockProductRepository.Object);
            Assert.ThrowsException<NotImplementedException>(() => service.Upsert(dto));
        }

        [TestMethod]
        public void Delete()
        {
            Product dto = Factories.NewProduct();
            ServiceResponse<Product> responseDto = ServiceResponse<Product>.Send(dto);
            mockProductRepository.Setup(x => x.Delete(dto.Id));

            ProductService service = new(mockProductRepository.Object);
            Assert.ThrowsException<NotImplementedException>(() => service.Delete(dto.Id));
        }
    }
}