using backend.Accounts.Seeders;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Helpers;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

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

        modelBuilder.ApplyGlobalFilters<ISoftDelete>(x => x.DeletedAt == null);
        
    }
}
