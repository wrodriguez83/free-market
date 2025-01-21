using ProductModule.Domain;

namespace CartModule.Domain
{
    public class CartItem
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public DateTime LastUpdate { get; set; }
        public Product? Product { get; set; }
    }
}