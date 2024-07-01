using backend.Accounts.Seeders;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Helpers;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<NavigationMenu> NavigationMenus { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<ProductItem> ProductItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        // Use logger when environment type is development
        if (isDevelopment)
        {
            options.EnableSensitiveDataLogging();
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Accounts Module Seeder Start
        new RoleSeeder(modelBuilder).Seed();
        new AccountSeeder(modelBuilder).Seed();
        // Accounts Module Seeder End

        modelBuilder.Entity<Media>()
            .HasIndex(x => x.AuthorId)
            .IsUnique(false);

        modelBuilder.Entity<Category>()
            .HasIndex(x => x.AuthorId)
            .IsUnique(false);

        modelBuilder.Entity<Product>()
            .HasIndex(x => x.AuthorId)
            .IsUnique(false);

        modelBuilder.Entity<Product>()
            .HasIndex(x => x.CategoryId)
            .IsUnique(false);

        modelBuilder.Entity<Voucher>()
            .HasIndex(x => x.AuthorId)
            .IsUnique(false);

        modelBuilder.Entity<ProductItem>()
            .HasIndex(x => x.ProductId)
            .IsUnique(false);

        modelBuilder.ApplyGlobalFilters<ISoftDelete>(x => x.DeletedAt == null);
        
    }
}
