using CartModule.Application;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CartModuleTests
{
    [TestClass]
    public sealed class CartInjectorTests
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
            CartInjector.InjectServices(mockServiceCollection.Object);
            mockServiceCollection?.VerifyAll();
        }
    }
}