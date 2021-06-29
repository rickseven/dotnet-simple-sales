
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleSales.WebAdmin.Models.Order;
using SimpleSales.WebAdmin.Models.Product;

namespace SimpleSales.WebAdmin.Databases
{
    public class AppDBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<OrderModel> Order { get; set; }

        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
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
