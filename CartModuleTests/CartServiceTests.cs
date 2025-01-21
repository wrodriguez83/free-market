using System.Net;
using CartModule.Application;
using CartModule.Domain;
using DeepEqual.Syntax;
using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using Moq;
using TestHelper;

namespace CartModuleTests
{
    [TestClass]
    public sealed class CartServiceTests
    {
        private Mock<IModuleRepository<Cart>> mockCartRepository = new();

        [TestInitialize]
        public void TestInitialize()
        {
            mockCartRepository = new Mock<IModuleRepository<Cart>>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task FindAll()
        {
            List<Cart> dto = new([Factories.NewCart(true), Factories.NewCart(true)]);
            ServiceResponse<List<Cart>> responseDto = ServiceResponse<List<Cart>>.Send(dto);

            mockCartRepository.Setup(x => x.GetAll()).ReturnsAsync(dto);

            CartService service = new(mockCartRepository.Object);
            var result = await service.FindAll();
            Assert.IsNotNull(result);

            responseDto.IsDeepEqual(result);
            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public void FindAllWithHttpException()
        {
            List<Cart> dto = new([Factories.NewCart(true), Factories.NewCart(true)]);
            ServiceResponse<List<Cart>> responseDto = ServiceResponse<List<Cart>>.SendError("Test exception", HttpStatusCode.Forbidden);

            mockCartRepository.Setup(x => x.GetAll()).Throws(new HttpRequestException(responseDto.Error, null, responseDto.Status));

            CartService service = new(mockCartRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.FindAll());

            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public void FindAllWithException()
        {
            List<Cart> dto = new([Factories.NewCart(true), Factories.NewCart(true)]);
            ServiceResponse<List<Cart>> responseDto = ServiceResponse<List<Cart>>.SendError();

            mockCartRepository.Setup(x => x.GetAll()).Throws<Exception>();

            CartService service = new(mockCartRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.FindAll());

            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public async Task FindOne()
        {
            Cart dto = Factories.NewCart(true);
            ServiceResponse<Cart> responseDto = ServiceResponse<Cart>.Send(dto);

            mockCartRepository.Setup(x => x.GetOne(dto.Id)).ReturnsAsync(dto);

            CartService service = new(mockCartRepository.Object);
            var result = await service.FindOne(dto.Id);
            Assert.IsNotNull(result);

            responseDto.IsDeepEqual(result);
            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public void FindOneWithHttpException()
        {
            Cart dto = Factories.NewCart(true);
            ServiceResponse<Cart> responseDto = ServiceResponse<Cart>.SendError("Test exception", HttpStatusCode.Forbidden);

            mockCartRepository.Setup(x => x.GetOne(dto.Id)).Throws(new HttpRequestException(responseDto.Error, null, responseDto.Status));

            CartService service = new(mockCartRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.FindOne(dto.Id));

            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public void FindOneWithException()
        {
            Cart dto = Factories.NewCart(true);
            ServiceResponse<Cart> responseDto = ServiceResponse<Cart>.SendError();

            mockCartRepository.Setup(x => x.GetOne(dto.Id)).Throws<Exception>();

            CartService service = new(mockCartRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.FindOne(dto.Id));

            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public async Task Upsert()
        {
            Cart dto = Factories.NewCart(true);
            ServiceResponse<Cart> responseDto = ServiceResponse<Cart>.Send(dto);

            mockCartRepository.Setup(x => x.Upsert(dto)).ReturnsAsync(dto);

            CartService service = new(mockCartRepository.Object);
            var result = await service.Upsert(dto);
            Assert.IsNotNull(result);

            responseDto.IsDeepEqual(result);
            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public void UpsertWithHttpException()
        {
            Cart dto = Factories.NewCart(true);
            ServiceResponse<Cart> responseDto = ServiceResponse<Cart>.SendError("Test exception", HttpStatusCode.Forbidden);

            mockCartRepository.Setup(x => x.Upsert(dto)).Throws(new HttpRequestException(responseDto.Error, null, responseDto.Status));

            CartService service = new(mockCartRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.Upsert(dto));

            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public void UpsertWithException()
        {
            Cart dto = Factories.NewCart(true);
            ServiceResponse<Cart> responseDto = ServiceResponse<Cart>.SendError();

            mockCartRepository.Setup(x => x.Upsert(dto)).Throws<Exception>();

            CartService service = new(mockCartRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.Upsert(dto));

            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public async Task Delete()
        {
            Cart dto = Factories.NewCart(true);
            ServiceResponse<Cart> responseDto = ServiceResponse<Cart>.Send(dto);

            mockCartRepository.Setup(x => x.Delete(dto.Id));

            CartService service = new(mockCartRepository.Object);
            await service.Delete(dto.Id);
            
            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public void DeleteWithHttpException()
        {
            Cart dto = Factories.NewCart(true);
            ServiceResponse<Cart> responseDto = ServiceResponse<Cart>.SendError("Test exception", HttpStatusCode.Forbidden);

            mockCartRepository.Setup(x => x.Delete(dto.Id)).Throws(new HttpRequestException(responseDto.Error, null, responseDto.Status));

            CartService service = new(mockCartRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.Delete(dto.Id));

            mockCartRepository?.VerifyAll();
        }

        [TestMethod]
        public void DeleteWithException()
        {
            Cart dto = Factories.NewCart(true);
            ServiceResponse<Cart> responseDto = ServiceResponse<Cart>.SendError();

            mockCartRepository.Setup(x => x.Delete(dto.Id)).Throws<Exception>();

            CartService service = new(mockCartRepository.Object);
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await service.Delete(dto.Id));

            mockCartRepository?.VerifyAll();
        }
    }
}