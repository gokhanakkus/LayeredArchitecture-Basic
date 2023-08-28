using App.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace App.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<CartEntity>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<CartItemEntity>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItemEntity>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity { Id = 1, Name = "Product 1", Price = 10.50m, ImageUrl = "https://fastly.picsum.photos/id/9/5000/3269.jpg?hmac=cZKbaLeduq7rNB8X-bigYO8bvPIWtT-mh8GRXtU3vPc" },
                new ProductEntity { Id = 2, Name = "Product 2", Price = 20.00m, ImageUrl = "https://fastly.picsum.photos/id/26/4209/2769.jpg?hmac=vcInmowFvPCyKGtV7Vfh7zWcA_Z0kStrPDW3ppP0iGI" },
                new ProductEntity { Id = 3, Name = "Product 3", Price = 30.15m, ImageUrl = "https://fastly.picsum.photos/id/668/4133/2745.jpg?hmac=n2-nIVXnrSE_pCjAmI-nlhBicoySz1xq-KUFMr9ERrM" },
                new ProductEntity { Id = 4, Name = "Product 4", Price = 40.50m, ImageUrl = "https://fastly.picsum.photos/id/454/4403/2476.jpg?hmac=pubXcBaPumNk0jElL63xrQYiSwQWA_DtS8uNNV8PmIE" },
                new ProductEntity { Id = 5, Name = "Product 5", Price = 50.00m, ImageUrl = "https://fastly.picsum.photos/id/175/2896/1944.jpg?hmac=djMSfAvFgWLJ2J3cBulHUAb4yvsQk0d4m4xBJFKzZrs" },
                new ProductEntity { Id = 6, Name = "Product 6", Price = 60.00m, ImageUrl = "https://fastly.picsum.photos/id/96/4752/3168.jpg?hmac=KNXudB1q84CHl2opIFEY4ph12da5JD5GzKzH5SeuRVM" },
                new ProductEntity { Id = 7, Name = "Product 7", Price = 70.99m, ImageUrl = "https://fastly.picsum.photos/id/357/3888/2592.jpg?hmac=322FsZ93_k9v7NNFeCTlqk_gobPP_1mYJIQwk7GxjMc" },
                new ProductEntity { Id = 8, Name = "Product 8", Price = 80.15m, ImageUrl = "https://fastly.picsum.photos/id/20/3670/2462.jpg?hmac=CmQ0ln-k5ZqkdtLvVO23LjVAEabZQx2wOaT4pyeG10I" },
                new ProductEntity { Id = 9, Name = "Product 9", Price = 90.25m, ImageUrl = "https://fastly.picsum.photos/id/160/3200/2119.jpg?hmac=cz68HnnDt3XttIwIFu5ymcvkCp-YbkEBAM-Zgq-4DHE" },
                new ProductEntity { Id = 10, Name = "Product 10", Price = 100.99m, ImageUrl = "https://fastly.picsum.photos/id/429/4128/2322.jpg?hmac=_mAS4ToWrJBx29qI2YNbOQ9IyOevQr01DEuCbArqthc" }
            );

            modelBuilder.Entity<UserEntity>().HasData(
            new UserEntity { Id = 1, Name = "Gökhan", Surname = "Akkuş", Email = "akkus@akkus.com", Password = "12345" },
            new UserEntity { Id = 2, Name = "Gökhan", Surname = "Türkmen", Email = "gokhan@gokhan.com", Password = "12345" }

        );

            modelBuilder.Entity<CartEntity>().HasData(
                new CartEntity { Id = 1, UserId = 1, CreatedAt = DateTime.Now, Name = "DefaultCart" },
                new CartEntity { Id = 2, UserId = 2, CreatedAt = DateTime.Now, Name = "DefaultCart" }
            );
        }
    }
}