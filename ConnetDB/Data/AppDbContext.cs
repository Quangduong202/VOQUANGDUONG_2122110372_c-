using connetdb.Models;
using Microsoft.EntityFrameworkCore;

namespace connetdb.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor nhận cấu hình từ Program.cs
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tạo bảng Students trong DB
    
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Payment> Payments => Set<Payment>();
    }
}
