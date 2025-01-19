namespace CartModule.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Deleted_at { get; set; }
        public required CartItem[] Products { get; set; }
    }
}