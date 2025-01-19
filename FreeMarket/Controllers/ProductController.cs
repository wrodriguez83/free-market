using FreeMarket.Domain.Classes;
using Microsoft.AspNetCore.Mvc;
using ProductModule.Application;
using ProductModule.Domain;

namespace FreeMarket.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService service = new();

        [HttpGet]
        public Task<ServiceResponse<Product[]>> Get()
        {
            return service.FindAll();
        }

        [HttpGet("{id}")]
        public Task<ServiceResponse<Product>> Get(int id)
        {
            return service.FindOne(id);
        }

        //// POST api/<TestController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<TestController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TestController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
