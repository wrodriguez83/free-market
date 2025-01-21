using CartModule.Application;
using CartModule.Domain;
using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreeMarket.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController(IService<Cart> service) : ControllerBase
    {
        [HttpGet]
        public Task<ServiceResponse<List<Cart>>> Get()
        {
            return service.FindAll();
        }
        [HttpGet("{id}")]
        public Task<ServiceResponse<Cart>> Get(int id)
        {
            return service.FindOne(id);
        }
        [HttpPost]
        public Task<ServiceResponse<Cart>> Upsert([FromBody] Cart value)
        {
            return service.Upsert(value);
        }

        [HttpDelete("{id}")]
        public Task<ServiceResponse<object>> Delete(int id)
        {
            return service.Delete(id);
        }
    }
}
