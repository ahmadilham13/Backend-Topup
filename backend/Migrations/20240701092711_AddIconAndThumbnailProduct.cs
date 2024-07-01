using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIconAndThumbnailProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IconId",
                table: "Products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailId",
                table: "Products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 9, 27, 10, 601, DateTimeKind.Utc).AddTicks(910), "$2a$11$VmLn3BXP.l0MQygKW2ynnektVIPnVFad2oyALqY8.PhWGv./obdm6", new DateTime(2024, 7, 1, 9, 27, 10, 601, DateTimeKind.Utc).AddTicks(902) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 9, 27, 10, 870, DateTimeKind.Utc).AddTicks(5783), "$2a$11$NO3S3Nb8kLYFLtuTHOyYxuvthA71Ab9eD9I4UlmBG.jgxhDzc0.Xy", new DateTime(2024, 7, 1, 9, 27, 10, 870, DateTimeKind.Utc).AddTicks(5627) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_IconId",
                table: "Products",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ThumbnailId",
                table: "Products",
                column: "ThumbnailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Medias_IconId",
                table: "Products",
                column: "IconId",
                principalTable: "Medias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Medias_ThumbnailId",
                table: "Products",
                column: "ThumbnailId",
                principalTable: "Medias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Medias_IconId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Medias_ThumbnailId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IconId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ThumbnailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 9, 22, 42, 324, DateTimeKind.Utc).AddTicks(4704), "$2a$11$Lj5E4hITa4ZZZnFEyMY.uuJIry5WjZpbosITxeLJ2LTkba5uphiC.", new DateTime(2024, 7, 1, 9, 22, 42, 324, DateTimeKind.Utc).AddTicks(4695) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 9, 22, 42, 739, DateTimeKind.Utc).AddTicks(407), "$2a$11$8y2QGuBWCyCgYdD1aexd9.b1irGd1KhcaF7ZmSyi31.GGy5NbLMwu", new DateTime(2024, 7, 1, 9, 22, 42, 739, DateTimeKind.Utc).AddTicks(396) });
        }
    }
}
