using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class LinkGameCommentToAuthorUser : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
        name: "Author",
        schema: "TGZ",
        table: "GameComment");

      migrationBuilder.AddColumn<int>(
        name: "AuthorId",
        schema: "TGZ",
        table: "GameComment",
        type: "int",
        nullable: false,
        defaultValue: 0);

      migrationBuilder.CreateIndex(
        name: "IX_GameComment_AuthorId",
        schema: "TGZ",
        table: "GameComment",
        column: "AuthorId");

      migrationBuilder.AddForeignKey(
        name: "FK_GameComment_AspNetUsers_AuthorId",
        schema: "TGZ",
        table: "GameComment",
        column: "AuthorId",
        principalTable: "AspNetUsers",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_GameComment_AspNetUsers_AuthorId",
        schema: "TGZ",
        table: "GameComment");

      migrationBuilder.DropIndex(
        name: "IX_GameComment_AuthorId",
        schema: "TGZ",
        table: "GameComment");

      migrationBuilder.DropColumn(
        name: "AuthorId",
        schema: "TGZ",
        table: "GameComment");

      migrationBuilder.AddColumn<string>(
        name: "Author",
        schema: "TGZ",
        table: "GameComment",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: "");
    }
  }
}
