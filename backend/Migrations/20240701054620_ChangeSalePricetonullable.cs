using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSalePricetonullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SalePrice",
                table: "ProductItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 5, 46, 19, 725, DateTimeKind.Utc).AddTicks(9092), "$2a$11$2mDqmGrzqg9r.2gJj.4.VuJeMhh10RZ8/Ls1qHlylBku04WQM3pwy", new DateTime(2024, 7, 1, 5, 46, 19, 725, DateTimeKind.Utc).AddTicks(9087) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 5, 46, 19, 865, DateTimeKind.Utc).AddTicks(9545), "$2a$11$/lE/.NJ6NiahnZXlJJImJOIYHBsCz6yc36XL4vUQjTC9sU6Er7QvG", new DateTime(2024, 7, 1, 5, 46, 19, 865, DateTimeKind.Utc).AddTicks(9537) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SalePrice",
                table: "ProductItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 4, 57, 46, 984, DateTimeKind.Utc).AddTicks(8795), "$2a$11$S8wKNZydhpgq9A3.oEDvNOt05OQmBUi/oDqfbYQtNS4fDbJlKuFbK", new DateTime(2024, 7, 1, 4, 57, 46, 984, DateTimeKind.Utc).AddTicks(8639) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 1, 4, 57, 47, 234, DateTimeKind.Utc).AddTicks(3540), "$2a$11$umzIw7aK9YO1ctRvSX0TZOC2.X/vhKniEjawYP.xs5Wm.CZVbhU96", new DateTime(2024, 7, 1, 4, 57, 47, 234, DateTimeKind.Utc).AddTicks(3378) });
        }
    }
}
