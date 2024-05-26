using Microsoft.EntityFrameworkCore;
using backend.Entities;

namespace backend.Accounts.Seeders;

public class RoleSeeder(
    ModelBuilder modelBuilder
    )
{
    private readonly ModelBuilder _modelBuilder = modelBuilder;

    public void Seed()
    {
        _modelBuilder.Entity<Role>().HasData(
            new Role { Id = new Guid("9648fc11-a4bf-40bb-84a2-2925ebaa630d"), RoleName = "Super Admin" },
            new Role { Id = new Guid("3658f128-c3ac-4bc2-82d5-7b282183721d"), RoleName = "User" }
        );
    }
}