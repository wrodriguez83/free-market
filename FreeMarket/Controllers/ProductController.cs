using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ProductModule.Domain;

namespace FreeMarket.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController(IService<Product> service) : ControllerBase
    {
        [HttpGet]
        public Task<ServiceResponse<List<Product>>> Get()
        {
            return service.FindAll();
        }

        [HttpGet("{id}")]
        public Task<ServiceResponse<Product>> Get(int id)
        {
            return service.FindOne(id);
        }
    }
}
