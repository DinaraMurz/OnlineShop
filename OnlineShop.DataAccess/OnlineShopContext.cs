using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain;

namespace OnlineShop.DataAccess
{
    public class OnlineShopContext : DbContext
    {
        public OnlineShopContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<ProductInOrder> ProductInOrders { get; set; }
        public DbSet<ProductInCart> ProductInCarts { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
