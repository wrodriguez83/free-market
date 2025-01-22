using FreeMarket.Domain.Interfaces;

namespace CartModule.Domain
{
    public class Cart:IEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public required List<CartItem> Items { get; set; }
    }
}