using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Accounts_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 6, 22, 7, 11, 12, 682, DateTimeKind.Utc).AddTicks(5589), "$2a$11$ImKzbQI.YdyiqgvLaZPYKuzxZB0dnq0Fivyg5xVlefsBbdNpel5by", new DateTime(2024, 6, 22, 7, 11, 12, 682, DateTimeKind.Utc).AddTicks(5584) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 6, 22, 7, 11, 12, 820, DateTimeKind.Utc).AddTicks(2280), "$2a$11$SjE0ihQHQlNe523sJoWFHOlrHs73LruCu1Tad456r/0hsGZisfzHO", new DateTime(2024, 6, 22, 7, 11, 12, 820, DateTimeKind.Utc).AddTicks(2273) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AuthorId",
                table: "Products",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 6, 7, 15, 42, 40, 937, DateTimeKind.Utc).AddTicks(1135), "$2a$11$sTBA2MNpvIkVCDkFNaL75.qYlGDQ.o74yrlrkbllUffFfr3irTQu6", new DateTime(2024, 6, 7, 15, 42, 40, 937, DateTimeKind.Utc).AddTicks(1127) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 6, 7, 15, 42, 41, 150, DateTimeKind.Utc).AddTicks(8413), "$2a$11$MeXR/YDhZWOFQ1OSBVjv8ew7om4j7KFpJpUBhm3jIRr2oB9tnv9kO", new DateTime(2024, 6, 7, 15, 42, 41, 150, DateTimeKind.Utc).AddTicks(8404) });
        }
    }
}
