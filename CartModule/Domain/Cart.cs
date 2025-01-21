namespace CartModule.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public required List<CartItem> Products { get; set; }
    }
}