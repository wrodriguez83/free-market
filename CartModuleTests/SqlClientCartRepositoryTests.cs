using CartModule.Domain;
using DeepEqual.Syntax;
using Microsoft.Extensions.Configuration;
using TestHelper;

namespace CartModuleTests
{
    [TestClass]
    public sealed class SqlClientCartRepositoryTests
    {
        private readonly IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        [TestMethod]
        public async Task GetAll()
        {
            List<Cart> dto = new([Factories.NewCart(), Factories.NewCart()]);
            SqlClientCartRepositoryMock.Data = dto;

            SqlClientCartRepositoryMock repository = new(configuration);

            var result = await repository.GetAll("carts");
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);

            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(0, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("get_carts", repository.SqlCommand?.CommandText);
        }
        [TestMethod]
        public async Task GetAllBy()
        {
            List<Cart> dto = new([Factories.NewCart(), Factories.NewCart()]);
            SqlClientCartRepositoryMock.Data = dto;

            SqlClientCartRepositoryMock repository = new(configuration);

            var result = await repository.GetAllBy("carts", dto[0].Id);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);

            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(1, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("@id", repository.SqlCommand?.Parameters[0].ParameterName);
            Assert.AreEqual(dto[0].Id, repository.SqlCommand?.Parameters[0].Value);
            Assert.AreEqual("get_carts", repository.SqlCommand?.CommandText);
        }

        [TestMethod]
        public async Task GetOne()
        {
            Cart dto = Factories.NewCart();
            SqlClientCartRepositoryMock.Data = [dto];

            SqlClientCartRepositoryMock repository = new(configuration);

            var result = await repository.GetOne("carts", dto.Id);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(1, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("@id", repository.SqlCommand?.Parameters[0].ParameterName);
            Assert.AreEqual(dto.Id, repository.SqlCommand?.Parameters[0].Value);
            Assert.AreEqual("get_carts", repository.SqlCommand?.CommandText);
        }

        [TestMethod]
        public async Task Upsert()
        {
            Cart dto = Factories.NewCart();
            SqlClientCartRepositoryMock.Data = [dto];

            SqlClientCartRepositoryMock repository = new(configuration);

            var result = await repository.Upsert("carts", dto);
            Assert.IsNotNull(result);

            dto.IsDeepEqual(result);
            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(2, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("@id", repository.SqlCommand?.Parameters[0].ParameterName);
            Assert.AreEqual(dto.Id, repository.SqlCommand?.Parameters[0].Value);
            Assert.AreEqual("@name", repository.SqlCommand?.Parameters[1].ParameterName);
            Assert.AreEqual(dto.Name, repository.SqlCommand?.Parameters[1].Value);
            Assert.AreEqual("upsert_carts", repository.SqlCommand?.CommandText);
        }

        [TestMethod]
        public void Delete()
        {
            Cart dto = Factories.NewCart();
            SqlClientCartRepositoryMock.Data = [dto];

            SqlClientCartRepositoryMock repository = new(configuration);

            repository.Delete("carts", dto.Id);

            Assert.IsNotNull(repository.SqlCommand);
            Assert.AreEqual(1, repository.SqlCommand?.Parameters.Count);
            Assert.AreEqual("@id", repository.SqlCommand?.Parameters[0].ParameterName);
            Assert.AreEqual(dto.Id, repository.SqlCommand?.Parameters[0].Value);
            Assert.AreEqual("delete_carts", repository.SqlCommand?.CommandText);
        }
    }
}