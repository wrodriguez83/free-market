using FreeMarket.Domain.Interfaces;

namespace ProductModule.Domain
{
    public class Product:IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public double Price { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public string? Image { get; set; }
        public required Rating Rating { get; set; }
    }
}