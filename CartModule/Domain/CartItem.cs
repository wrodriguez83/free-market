using ProductModule.Domain;

namespace CartModule.Domain
{
    public class CartItem:Product
    {
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Deleted_at { get; set; }
        public int Quantity { get; set; }
    }
}
