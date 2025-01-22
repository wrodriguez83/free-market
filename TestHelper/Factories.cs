using CartModule.Domain;
using ProductModule.Domain;

namespace TestHelper
{
    public class Factories
    {
        public static Cart NewCart(bool? linkItems = false)
        {
            return new Cart { Id = new Random().Next(1, 100), Name = "Test", LastUpdate = DateTime.Now, Items = linkItems ?? false ? [NewCartItem(), NewCartItem()] : [] };
        }
        public static CartItem NewCartItem()
        {
            return new CartItem { Id = new Random().Next(1, 100), CartId = new Random().Next(1, 100), LastUpdate = DateTime.Now, Price = 1.34, Quantity = new Random().Next(1, 10), Product = NewProduct() };
        }

        public static Product NewProduct()
        {
            return new Product { Category = "Category Test", Description = "Test of Test", Id = new Random().Next(1, 100), Image = "image test", Price = 33.2, Rating = new Rating() { Count = 1, Rate = 3 }, Title = "Test" };
        }
    }
}
