using Microsoft.EntityFrameworkCore;
using Core.Entity;
namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCart> ProductInCarts { get; set; }
        public DbSet<ProductStorage> ProductStorages { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Order> Orders { get; set; }


    }
}
