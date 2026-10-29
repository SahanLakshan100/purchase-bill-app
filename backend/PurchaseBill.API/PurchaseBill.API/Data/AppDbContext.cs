using Microsoft.EntityFrameworkCore;
using PurchaseBill.API.Models;

namespace PurchaseBill.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<LocationDetail> Location_Details => Set<LocationDetail>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<PurchaseBillEntity> Purchase_Bills => Set<PurchaseBillEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LocationDetail>().HasIndex(x => x.Location_Code).IsUnique();
        modelBuilder.Entity<AppUser>().HasIndex(x => x.Email).IsUnique();

        modelBuilder.Entity<PurchaseBillEntity>().Property(x => x.StandardCost).HasPrecision(18, 2);
        modelBuilder.Entity<PurchaseBillEntity>().Property(x => x.StandardPrice).HasPrecision(18, 2);
        modelBuilder.Entity<PurchaseBillEntity>().Property(x => x.DiscountPct).HasPrecision(5, 2);
        modelBuilder.Entity<PurchaseBillEntity>().Property(x => x.TotalCost).HasPrecision(18, 2);
        modelBuilder.Entity<PurchaseBillEntity>().Property(x => x.TotalSelling).HasPrecision(18, 2);
    }
}