using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddBeritaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beritas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PathId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beritas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beritas_Accounts_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beritas_Medias_PathId",
                        column: x => x.PathId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 10, 3, 23, 55, 791, DateTimeKind.Utc).AddTicks(1134), "$2a$11$ExrErm.mgLQU28It8HOBmOpy5GdcjZJtdGW0yQrALskXRHynLTcD2", new DateTime(2024, 7, 10, 3, 23, 55, 791, DateTimeKind.Utc).AddTicks(1128) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 7, 10, 3, 23, 55, 985, DateTimeKind.Utc).AddTicks(6854), "$2a$11$0uzzRxG6GYhke9qPgo6rh.MMDB7NlGqHNQEO2WlnQJBQelsSG68Uu", new DateTime(2024, 7, 10, 3, 23, 55, 985, DateTimeKind.Utc).AddTicks(6844) });

            migrationBuilder.CreateIndex(
                name: "IX_Beritas_AuthorId",
                table: "Beritas",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Beritas_PathId",
                table: "Beritas",
                column: "PathId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beritas");

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
    }
}
