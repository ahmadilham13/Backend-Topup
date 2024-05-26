using Microsoft.EntityFrameworkCore;

namespace backend.Helpers;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    // public DbSet<Account> Accounts { get; set; }

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
        
    }
}
