using System.Net;
using DeepEqual.Syntax;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Contrib.HttpClient;
using TestHelper;

namespace FakeStoreApiRepositoryTests
{
    [TestClass]
    public sealed class FakeStoreAPiRepositoryTests
    {
        private readonly IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private Mock<HttpMessageHandler> mockHttpClient = new();
        private string ApiUrl = "";

        [TestInitialize]
        public void TestInitialize()
        {
            ApiUrl = $"{configuration.GetConnectionString("storeApi")}/test";
            mockHttpClient = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task GetAll()
        {
            List<TestClass> dto = new List<TestClass>([new TestClass(), new TestClass()]);
            mockHttpClient.SetupRequest(HttpMethod.Get, ApiUrl).ReturnsJsonResponse<List<TestClass>>(HttpStatusCode.OK, dto);
            HttpClient httpClient = mockHttpClient.CreateClient();
            FakeStoreApiRepositoryMock<TestClass> repository = new(httpClient, configuration);

            var result = await repository.GetAll("test");
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            mockHttpClient.VerifyRequest(HttpMethod.Get, ApiUrl, Times.Once());
        }
        [TestMethod]
        public void GetAllBy()
        {
            List<TestClass> dto = new List<TestClass>([new TestClass(), new TestClass()]);
            HttpClient httpClient = mockHttpClient.CreateClient();

            FakeStoreApiRepositoryMock<TestClass> repository = new(httpClient, configuration);
            Assert.ThrowsException<NotImplementedException>(() => repository.GetAllBy("test", dto[0].Id));
        }
        [TestMethod]
        public async Task GetOne()
        {
            TestClass dto = new TestClass();
            mockHttpClient.SetupRequest(HttpMethod.Get, $"{ApiUrl}/{dto.Id}").ReturnsJsonResponse<TestClass>(HttpStatusCode.OK, dto);
            HttpClient httpClient = mockHttpClient.CreateClient();

            FakeStoreApiRepositoryMock<TestClass> repository = new(httpClient, configuration);

            var result = await repository.GetOne("test", dto.Id);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            mockHttpClient.VerifyRequest(HttpMethod.Get, $"{ApiUrl}/{dto.Id}", Times.Once());
        }
        [TestMethod]
        public void Upsert()
        {
            TestClass dto = new TestClass();
            HttpClient httpClient = mockHttpClient.CreateClient();

            FakeStoreApiRepositoryMock<TestClass> repository = new(httpClient, configuration);
            Assert.ThrowsException<NotImplementedException>(() => repository.Upsert("test", dto));

            mockHttpClient.VerifyAll();
        }
        [TestMethod]
        public void Delete()
        {
            TestClass dto = new TestClass();
            HttpClient httpClient = mockHttpClient.CreateClient();

            FakeStoreApiRepositoryMock<TestClass> repository = new(httpClient, configuration);
            Assert.ThrowsException<NotImplementedException>(() => repository.Delete("test", dto.Id));

            mockHttpClient.VerifyAll();
        }
    }
}
