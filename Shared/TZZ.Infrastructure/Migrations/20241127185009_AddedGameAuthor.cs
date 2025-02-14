using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedGameAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                schema: "TGZ",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Game_AuthorId",
                schema: "TGZ",
                table: "Game",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_AspNetUsers_AuthorId",
                schema: "TGZ",
                table: "Game",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_AspNetUsers_AuthorId",
                schema: "TGZ",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_AuthorId",
                schema: "TGZ",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                schema: "TGZ",
                table: "Game");
        }
    }
}
