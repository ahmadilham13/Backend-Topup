using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationMenuAndRolePermissionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavigationMenu_NavigationMenuGroup_GroupId",
                table: "NavigationMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_NavigationMenu_NavigationMenuId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NavigationMenu",
                table: "NavigationMenu");

            migrationBuilder.RenameTable(
                name: "RolePermission",
                newName: "RolePermissions");

            migrationBuilder.RenameTable(
                name: "NavigationMenu",
                newName: "NavigationMenus");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_NavigationMenuId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_NavigationMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_NavigationMenu_GroupId",
                table: "NavigationMenus",
                newName: "IX_NavigationMenus_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NavigationMenus",
                table: "NavigationMenus",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 5, 28, 8, 4, 21, 879, DateTimeKind.Utc).AddTicks(5506), "$2a$11$m.osRtght0FJkmxmUCgFgeies6Kl.IjU0PVcg7eaGa2wYCfePi.pi", new DateTime(2024, 5, 28, 8, 4, 21, 879, DateTimeKind.Utc).AddTicks(5499) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 5, 28, 8, 4, 22, 31, DateTimeKind.Utc).AddTicks(3130), "$2a$11$9KaT53jXL/DcS0zjI5Omt.vyiTqiNRfw2FvMyfaTJPIj2Q4lkmhci", new DateTime(2024, 5, 28, 8, 4, 22, 31, DateTimeKind.Utc).AddTicks(3118) });

            migrationBuilder.AddForeignKey(
                name: "FK_NavigationMenus_NavigationMenuGroup_GroupId",
                table: "NavigationMenus",
                column: "GroupId",
                principalTable: "NavigationMenuGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_NavigationMenus_NavigationMenuId",
                table: "RolePermissions",
                column: "NavigationMenuId",
                principalTable: "NavigationMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Role_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavigationMenus_NavigationMenuGroup_GroupId",
                table: "NavigationMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_NavigationMenus_NavigationMenuId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Role_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NavigationMenus",
                table: "NavigationMenus");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                newName: "RolePermission");

            migrationBuilder.RenameTable(
                name: "NavigationMenus",
                newName: "NavigationMenu");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermission",
                newName: "IX_RolePermission_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_NavigationMenuId",
                table: "RolePermission",
                newName: "IX_RolePermission_NavigationMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_NavigationMenus_GroupId",
                table: "NavigationMenu",
                newName: "IX_NavigationMenu_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NavigationMenu",
                table: "NavigationMenu",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 5, 27, 1, 53, 55, 209, DateTimeKind.Utc).AddTicks(4947), "$2a$11$4hME4X4TqXVU1sZzcgkDP.AViwa7Q9RtA8Pi7UdKQPAM73eWZljHS", new DateTime(2024, 5, 27, 1, 53, 55, 209, DateTimeKind.Utc).AddTicks(4941) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 5, 27, 1, 53, 55, 344, DateTimeKind.Utc).AddTicks(9747), "$2a$11$BT9SMTKxikjEzQAah6d.MOM7eTysvOqJ7.o8UsujbpfjZq10K.c4y", new DateTime(2024, 5, 27, 1, 53, 55, 344, DateTimeKind.Utc).AddTicks(9742) });

            migrationBuilder.AddForeignKey(
                name: "FK_NavigationMenu_NavigationMenuGroup_GroupId",
                table: "NavigationMenu",
                column: "GroupId",
                principalTable: "NavigationMenuGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_NavigationMenu_NavigationMenuId",
                table: "RolePermission",
                column: "NavigationMenuId",
                principalTable: "NavigationMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
