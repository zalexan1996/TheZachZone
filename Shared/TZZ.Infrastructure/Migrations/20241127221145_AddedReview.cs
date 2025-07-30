using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class AddedReview : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_Game_AspNetUsers_AuthorId",
        schema: "TGZ",
        table: "Game");

      migrationBuilder.CreateTable(
        name: "Review",
        schema: "TGZ",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          AuthorId = table.Column<int>(type: "int", nullable: false),
          GameId = table.Column<int>(type: "int", nullable: false),
          Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
          CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
          LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Review", x => x.Id);
          table.ForeignKey(
            name: "FK_Review_AspNetUsers_AuthorId",
            column: x => x.AuthorId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_Review_Game_GameId",
            column: x => x.GameId,
            principalSchema: "TGZ",
            principalTable: "Game",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateIndex(
        name: "IX_Review_AuthorId_GameId",
        schema: "TGZ",
        table: "Review",
        columns: new[] { "AuthorId", "GameId" },
        unique: true);

      migrationBuilder.CreateIndex(
        name: "IX_Review_GameId",
        schema: "TGZ",
        table: "Review",
        column: "GameId");

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

      migrationBuilder.DropTable(
        name: "Review",
        schema: "TGZ");

      migrationBuilder.AddForeignKey(
        name: "FK_Game_AspNetUsers_AuthorId",
        schema: "TGZ",
        table: "Game",
        column: "AuthorId",
        principalTable: "AspNetUsers",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);
    }
  }
}
