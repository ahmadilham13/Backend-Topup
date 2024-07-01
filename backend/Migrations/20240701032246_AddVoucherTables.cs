using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddVoucherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(150)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vouchers_Accounts_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 3, 22, 45, 461, DateTimeKind.Utc).AddTicks(7657), "$2a$11$AOuFc2fM/yymV4EZV3wyZ.W3dLqwcdiidKWHCgbv4TECYzkZDxm6q", new DateTime(2024, 7, 1, 3, 22, 45, 461, DateTimeKind.Utc).AddTicks(7651) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 3, 22, 45, 591, DateTimeKind.Utc).AddTicks(1267), "$2a$11$N/10qaCcXbc6dyS4pGdlPeXZjQQZyRO7yioBos7U9OnvngtXukmvW", new DateTime(2024, 7, 1, 3, 22, 45, 591, DateTimeKind.Utc).AddTicks(1262) });

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_AuthorId",
                table: "Vouchers",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vouchers");

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
        }
    }
}
