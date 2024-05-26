using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using backend.Entities;
namespace backend.Accounts.Seeders;

public class AccountSeeder(
    ModelBuilder modelBuilder
    )
{
    private readonly ModelBuilder _modelBuilder = modelBuilder;

    public void Seed()
    {
        _modelBuilder.Entity<Account>().HasData(
            new Account { Id = new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"), UserName = "superadmin", FullName = "Super Admin", Email = "superadmin@admin.com", PasswordHash = BC.HashPassword("Superadmin789@"), RoleId = new Guid("9648fc11-a4bf-40bb-84a2-2925ebaa630d"), Verified = DateTime.UtcNow, Created = DateTime.UtcNow },
            new Account { Id = new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"), UserName = "admin", FullName = "Admin", Email = "admin@admin.com", PasswordHash = BC.HashPassword("Admin789@"), RoleId = new Guid("3658f128-c3ac-4bc2-82d5-7b282183721d"), Verified = DateTime.UtcNow, Created = DateTime.UtcNow }
        );

        _modelBuilder.Entity<Account>()
            .HasIndex(x => x.RoleId)
            .IsUnique(false);

        _modelBuilder.Entity<Account>()
            .Property( x => x.Status )
            .HasDefaultValue(AccountStatus.Active);
    }
}