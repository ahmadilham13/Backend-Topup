using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Accounts_AccountId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_AccountId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
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
                name: "FK_RefreshTokens_Accounts_AccountId",
                table: "RefreshTokens",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Accounts_AccountId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_AccountId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 5, 26, 6, 56, 50, 862, DateTimeKind.Utc).AddTicks(871), "$2a$11$Y0wAwx.vi0rO.4P9JqI9h.7Zw/QMizr90W.zbJZYH2Q7Bqeh7AyiC", new DateTime(2024, 5, 26, 6, 56, 50, 862, DateTimeKind.Utc).AddTicks(865) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                columns: new[] { "Created", "PasswordHash", "Verified" },
                values: new object[] { new DateTime(2024, 5, 26, 6, 56, 51, 0, DateTimeKind.Utc).AddTicks(5534), "$2a$11$v4c0vBpY2x3Ez7Szjp5xG.wFYgIKBwpSeFVqJ9.x75Si0eBasmYo.", new DateTime(2024, 5, 26, 6, 56, 51, 0, DateTimeKind.Utc).AddTicks(5528) });

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Accounts_AccountId",
                table: "RefreshToken",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
