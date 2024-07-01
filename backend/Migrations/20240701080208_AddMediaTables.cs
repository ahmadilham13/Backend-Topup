using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Media_AvatarId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Accounts_AuthorId",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "Medias");

            migrationBuilder.RenameIndex(
                name: "IX_Media_AuthorId",
                table: "Medias",
                newName: "IX_Medias_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medias",
                table: "Medias",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Medias_AvatarId",
                table: "Accounts",
                column: "AvatarId",
                principalTable: "Medias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Accounts_AuthorId",
                table: "Medias",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Medias_AvatarId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Accounts_AuthorId",
                table: "Medias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medias",
                table: "Medias");

            migrationBuilder.RenameTable(
                name: "Medias",
                newName: "Media");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_AuthorId",
                table: "Media",
                newName: "IX_Media_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Media_AvatarId",
                table: "Accounts",
                column: "AvatarId",
                principalTable: "Media",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Accounts_AuthorId",
                table: "Media",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
