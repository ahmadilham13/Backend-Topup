using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddWhatsappNumberColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhatsappNumber",
                table: "Accounts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified", "WhatsappNumber" },
                values: new object[] { new DateTime(2024, 7, 1, 9, 22, 42, 324, DateTimeKind.Utc).AddTicks(4704), "$2a$11$Lj5E4hITa4ZZZnFEyMY.uuJIry5WjZpbosITxeLJ2LTkba5uphiC.", new DateTime(2024, 7, 1, 9, 22, 42, 324, DateTimeKind.Utc).AddTicks(4695), null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified", "WhatsappNumber" },
                values: new object[] { new DateTime(2024, 7, 1, 9, 22, 42, 739, DateTimeKind.Utc).AddTicks(407), "$2a$11$8y2QGuBWCyCgYdD1aexd9.b1irGd1KhcaF7ZmSyi31.GGy5NbLMwu", new DateTime(2024, 7, 1, 9, 22, 42, 739, DateTimeKind.Utc).AddTicks(396), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatsappNumber",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 8, 2, 7, 161, DateTimeKind.Utc).AddTicks(7169), "$2a$11$P7hugOCwn1cn750QV7Sky.sF.OFv9zxUi1M6jmdKVzQd.FIv/Q6Wq", new DateTime(2024, 7, 1, 8, 2, 7, 161, DateTimeKind.Utc).AddTicks(7155) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 8, 2, 7, 312, DateTimeKind.Utc).AddTicks(7805), "$2a$11$Xdh09223uerWDRF29QFQJOmRXim7C0fGpy.5On04L9qaj06qQ7SCS", new DateTime(2024, 7, 1, 8, 2, 7, 312, DateTimeKind.Utc).AddTicks(7798) });
        }
    }
}
