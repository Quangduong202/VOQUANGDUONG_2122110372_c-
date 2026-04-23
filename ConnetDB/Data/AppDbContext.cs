using connetdb.Models;
using Microsoft.EntityFrameworkCore;

namespace connetdb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet cho tất cả bảng
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductDetail> ProductDetails => Set<ProductDetail>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
      
        public DbSet<Feedback> Feedbacks => Set<Feedback>();

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<News> News => Set<News>();
        public DbSet<NewsDetail> NewsDetails => Set<NewsDetail>();

        public DbSet<Banner> Banners => Set<Banner>();
        public DbSet<BannerDetail> BannerDetails => Set<BannerDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Quan hệ Order - OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ Product - OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ Cart - CartItem
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(c => c.CartId);

            // Quan hệ Product - CartItem
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId);

            // Quan hệ Feedback
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Product)
                .WithMany()
                .HasForeignKey(f => f.ProductId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);

            // Product - Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Product - Brand
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);



            // ===== CATEGORY =====
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Điện thoại", Image = "phone.jpg" },
                new Category { Id = 2, Name = "Laptop", Image = "laptop.jpg" },
                new Category { Id = 3, Name = "Tablet", Image = "tablet.jpg" },
                new Category { Id = 4, Name = "Phụ kiện", Image = "accessory.jpg" },
                new Category { Id = 5, Name = "Smartwatch", Image = "watch.jpg" }
            );

            // ===== BRAND =====
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Username = "Apple", Image = "apple.jpg" },
                new Brand { Id = 2, Username = "Samsung", Image = "samsung.jpg" },
                new Brand { Id = 3, Username = "Xiaomi", Image = "xiaomi.jpg" },
                new Brand { Id = 4, Username = "Oppo", Image = "oppo.jpg" },
                new Brand { Id = 5, Username = "Dell", Image = "dell.jpg" },
                new Brand { Id = 6, Username = "HP", Image = "hp.jpg" }
            );

            // ===== PRODUCT =====
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15",
                    Image = "iphone15.jpg",
                    Price = 2000,
                    OldPrice = 2200,
                    Description = "Điện thoại Apple mới",
                    Specification = "A17 chip",
                    BuyTurn = "500",
                    Quantity = 20,
                    BrandId = 1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Galaxy S23",
                    Image = "s23.jpg",
                    Price = 1500,
                    OldPrice = 1700,
                    Description = "Samsung flagship",
                    Specification = "Snapdragon 8",
                    BuyTurn = "300",
                    Quantity = 15,
                    BrandId = 2,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Xiaomi 13",
                    Image = "mi13.jpg",
                    Price = 900,
                    OldPrice = 1100,
                    Description = "Giá rẻ cấu hình mạnh",
                    Specification = "Snapdragon 8 Gen 2",
                    BuyTurn = "200",
                    Quantity = 25,
                    BrandId = 3,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Macbook M2",
                    Image = "macbook.jpg",
                    Price = 2500,
                    OldPrice = 2700,
                    Description = "Laptop Apple",
                    Specification = "M2 chip",
                    BuyTurn = "150",
                    Quantity = 10,
                    BrandId = 1,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 5,
                    Name = "Dell XPS",
                    Image = "xps.jpg",
                    Price = 1800,
                    OldPrice = 2000,
                    Description = "Laptop cao cấp",
                    Specification = "Intel i7",
                    BuyTurn = "120",
                    Quantity = 8,
                    BrandId = 5,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 6,
                    Name = "HP Pavilion",
                    Image = "hp.jpg",
                    Price = 1200,
                    OldPrice = 1400,
                    Description = "Laptop phổ thông",
                    Specification = "Intel i5",
                    BuyTurn = "100",
                    Quantity = 12,
                    BrandId = 6,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 7,
                    Name = "iPad Pro",
                    Image = "ipad.jpg",
                    Price = 1300,
                    OldPrice = 1500,
                    Description = "Tablet Apple",
                    Specification = "M2 chip",
                    BuyTurn = "80",
                    Quantity = 10,
                    BrandId = 1,
                    CategoryId = 3
                },
                new Product
                {
                    Id = 8,
                    Name = "AirPods",
                    Image = "airpods.jpg",
                    Price = 300,
                    OldPrice = 350,
                    Description = "Tai nghe Apple",
                    Specification = "Bluetooth",
                    BuyTurn = "600",
                    Quantity = 50,
                    BrandId = 1,
                    CategoryId = 4
                });
        }
    }
}