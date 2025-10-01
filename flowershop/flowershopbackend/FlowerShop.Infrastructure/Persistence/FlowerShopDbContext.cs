using FlowerShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Infrastructure.Persistence
{
    public class FlowerShopDbContext : DbContext
    {
        public FlowerShopDbContext(DbContextOptions<FlowerShopDbContext> options) : base(options)
        {
        }

        public DbSet<FlowerCategory> FlowerCategories { get; set; } = null!;
        public DbSet<Flower> Flowers { get; set; } = null!;
        public DbSet<FlowerPricing> FlowerPrices { get; set; } = null!;
        public DbSet<FlowerStock> FlowerStocks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Config Flower model
            modelBuilder.Entity<Flower>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreationDate).HasDefaultValueSql("GETDATE()");

                entity.HasOne<FlowerCategory>()
                      .WithMany()
                      .HasForeignKey(e => e.CategoryId);
            });

            //Config FlowerCategory model
            modelBuilder.Entity<FlowerCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreationDate).HasDefaultValueSql("GETDATE()");

                entity.HasMany<Flower>()
                      .WithOne()
                      .HasForeignKey(e => e.CategoryId);
            });

            //Config FlowerPricing model
            modelBuilder.Entity<FlowerPricing>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<Flower>()
                      .WithOne(e => e.UnitPrice);

                entity.OwnsOne(e => e.Price, money =>
                {
                    money.Property(m => m.Amount)
                        .HasColumnName("UnitPrice")
                        .HasPrecision(18, 2);
                    money.Property(m => m.Currency)
                        .HasColumnName("UnitPriceCurrency")
                        .HasMaxLength(3);
                });

                entity.OwnsOne(e => e.PriceEffective, effect =>
                {
                    effect.Property(m => m.From)
                        .HasColumnName("FromDate");
                    effect.Property(m => m.To)
                        .HasColumnName("ToDate");
                });
            });

            //Config FlowerStock model
            modelBuilder.Entity<FlowerStock>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImportedDate).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("GETDATE()");
                entity.HasOne<Flower>()
                      .WithOne(e => e.Stock);


            });

        }
    }
}
