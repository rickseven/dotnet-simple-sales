
using Microsoft.EntityFrameworkCore;
using SimpleSales.Api.Models;

namespace SimpleSales.Api.Databases
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<OrderModel> Order { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.UnitPrice).HasColumnType("decimal").IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
            });

            modelBuilder.Entity<OrderModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.UnitPrice).HasColumnType("decimal").IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.HasOne(p => p.Product);
            });
        }
    }
}
