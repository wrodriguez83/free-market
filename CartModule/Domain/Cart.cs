namespace CartModule.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        public required Dictionary<int,CartItem> Products { get; set; }
    }
}