using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSubNameOnProductTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubName",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 2, 1, 0, 58, 827, DateTimeKind.Utc).AddTicks(9757), "$2a$11$A7hJdw3LGX/dmjXs/q91dODIu3/mx6VHufEjIUpLU9zZYCKP4S3zG", new DateTime(2024, 7, 2, 1, 0, 58, 827, DateTimeKind.Utc).AddTicks(9750) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 2, 1, 0, 59, 27, DateTimeKind.Utc).AddTicks(7523), "$2a$11$diab1mGz8OLOnvdN7WR3D.dOeudOxcB0/rv/ZSaGXsbTFh6AhZiHC", new DateTime(2024, 7, 2, 1, 0, 59, 27, DateTimeKind.Utc).AddTicks(7515) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubName",
                table: "Products");

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
        }
    }
}
