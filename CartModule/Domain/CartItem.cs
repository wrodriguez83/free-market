using ProductModule.Domain;

namespace CartModule.Domain
{
    public class CartItem:Product
    {
        public DateTime LastUpdate { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
