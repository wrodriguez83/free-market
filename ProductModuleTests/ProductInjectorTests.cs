using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductModule.Application;

namespace ProductModuleTests
{
    [TestClass]
    public sealed class ProductInjectorTests
    {
        private Mock<IServiceCollection> mockServiceCollection = new();

        [TestInitialize]
        public void TestInitialize()
        {
            mockServiceCollection = new Mock<IServiceCollection>();
        }

        [TestMethod]
        public void InjectServices()
        {
            ProductInjector.InjectServices(mockServiceCollection.Object);
            mockServiceCollection?.VerifyAll();
        }
    }
}