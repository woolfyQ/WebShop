//using Core.Entity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.InMemory;

//namespace Infrastructure.Data
//{
//    public class FakeContext : DbContext
//    {
//        // Сеттеры для DbSet
//        public DbSet<User> Users { get; set; } = null!;
//        public DbSet<Cart> Carts { get; set; } = null!;
//        public DbSet<Product> Products { get; set; } = null!;
//        public DbSet<ItemCart> ProductInCarts { get; set; } = null!;
//        public DbSet<ProductStorage> ProductStorages { get; set; } = null!;
//        public DbSet<Storage> Storages { get; set; } = null!;
//        public DbSet<Order> Orders { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {

//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseInMemoryDatabase("FakeDatabase");
//            }
//            base.OnConfiguring(optionsBuilder);
//        }
//    }
//}
