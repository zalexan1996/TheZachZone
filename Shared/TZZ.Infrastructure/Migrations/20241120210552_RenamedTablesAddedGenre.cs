using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class RenamedTablesAddedGenre : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_GameImage_GameInfo_GameInfoId",
        schema: "TGZ",
        table: "GameImage");

      migrationBuilder.DropForeignKey(
        name: "FK_GameStatistic_GameInfo_GameInfoId",
        schema: "TGZ",
        table: "GameStatistic");

      migrationBuilder.DropTable(
        name: "GameComment",
        schema: "TGZ");

      migrationBuilder.DropTable(
        name: "GameInfo",
        schema: "TGZ");

      migrationBuilder.RenameColumn(
        name: "GameInfoId",
        schema: "TGZ",
        table: "GameStatistic",
        newName: "GameId");

      migrationBuilder.RenameIndex(
        name: "IX_GameStatistic_GameInfoId",
        schema: "TGZ",
        table: "GameStatistic",
        newName: "IX_GameStatistic_GameId");

      migrationBuilder.RenameColumn(
        name: "GameInfoId",
        schema: "TGZ",
        table: "GameImage",
        newName: "GameId");

      migrationBuilder.RenameIndex(
        name: "IX_GameImage_GameInfoId",
        schema: "TGZ",
        table: "GameImage",
        newName: "IX_GameImage_GameId");

      migrationBuilder.CreateTable(
        name: "Game",
        schema: "TGZ",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
          UploadDate = table.Column<DateOnly>(type: "date", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Game", x => x.Id);
        });

      migrationBuilder.CreateTable(
        name: "Genre",
        schema: "TGZ",
        columns: table => new
        {
          GenreId = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Genre", x => x.GenreId);
        });

      migrationBuilder.CreateTable(
        name: "Comment",
        schema: "TGZ",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          AuthorId = table.Column<int>(type: "int", nullable: false),
          GameInfoId = table.Column<int>(type: "int", nullable: false),
          Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
          PostedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
          UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Comment", x => x.Id);
          table.ForeignKey(
            name: "FK_Comment_AspNetUsers_AuthorId",
            column: x => x.AuthorId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_Comment_Game_GameInfoId",
            column: x => x.GameInfoId,
            principalSchema: "TGZ",
            principalTable: "Game",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateTable(
        name: "GameGenre",
        schema: "TGZ",
        columns: table => new
        {
          GameId = table.Column<int>(type: "int", nullable: false),
          GenresGenreId = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_GameGenre", x => new { x.GameId, x.GenresGenreId });
          table.ForeignKey(
            name: "FK_GameGenre_Game_GameId",
            column: x => x.GameId,
            principalSchema: "TGZ",
            principalTable: "Game",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_GameGenre_Genre_GenresGenreId",
            column: x => x.GenresGenreId,
            principalSchema: "TGZ",
            principalTable: "Genre",
            principalColumn: "GenreId",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateIndex(
        name: "IX_Comment_AuthorId",
        schema: "TGZ",
        table: "Comment",
        column: "AuthorId");

      migrationBuilder.CreateIndex(
        name: "IX_Comment_GameInfoId",
        schema: "TGZ",
        table: "Comment",
        column: "GameInfoId");

      migrationBuilder.CreateIndex(
        name: "IX_GameGenre_GenresGenreId",
        schema: "TGZ",
        table: "GameGenre",
        column: "GenresGenreId");

      migrationBuilder.AddForeignKey(
        name: "FK_GameImage_Game_GameId",
        schema: "TGZ",
        table: "GameImage",
        column: "GameId",
        principalSchema: "TGZ",
        principalTable: "Game",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
        name: "FK_GameStatistic_Game_GameId",
        schema: "TGZ",
        table: "GameStatistic",
        column: "GameId",
        principalSchema: "TGZ",
        principalTable: "Game",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_GameImage_Game_GameId",
        schema: "TGZ",
        table: "GameImage");

      migrationBuilder.DropForeignKey(
        name: "FK_GameStatistic_Game_GameId",
        schema: "TGZ",
        table: "GameStatistic");

      migrationBuilder.DropTable(
        name: "Comment",
        schema: "TGZ");

      migrationBuilder.DropTable(
        name: "GameGenre",
        schema: "TGZ");

      migrationBuilder.DropTable(
        name: "Game",
        schema: "TGZ");

      migrationBuilder.DropTable(
        name: "Genre",
        schema: "TGZ");

      migrationBuilder.RenameColumn(
        name: "GameId",
        schema: "TGZ",
        table: "GameStatistic",
        newName: "GameInfoId");

      migrationBuilder.RenameIndex(
        name: "IX_GameStatistic_GameId",
        schema: "TGZ",
        table: "GameStatistic",
        newName: "IX_GameStatistic_GameInfoId");

      migrationBuilder.RenameColumn(
        name: "GameId",
        schema: "TGZ",
        table: "GameImage",
        newName: "GameInfoId");

      migrationBuilder.RenameIndex(
        name: "IX_GameImage_GameId",
        schema: "TGZ",
        table: "GameImage",
        newName: "IX_GameImage_GameInfoId");

      migrationBuilder.CreateTable(
        name: "GameInfo",
        schema: "TGZ",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
          UploadDate = table.Column<DateOnly>(type: "date", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_GameInfo", x => x.Id);
        });

      migrationBuilder.CreateTable(
        name: "GameComment",
        schema: "TGZ",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          AuthorId = table.Column<int>(type: "int", nullable: false),
          GameInfoId = table.Column<int>(type: "int", nullable: false),
          Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
          PostedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
          UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_GameComment", x => x.Id);
          table.ForeignKey(
            name: "FK_GameComment_AspNetUsers_AuthorId",
            column: x => x.AuthorId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_GameComment_GameInfo_GameInfoId",
            column: x => x.GameInfoId,
            principalSchema: "TGZ",
            principalTable: "GameInfo",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateIndex(
        name: "IX_GameComment_AuthorId",
        schema: "TGZ",
        table: "GameComment",
        column: "AuthorId");

      migrationBuilder.CreateIndex(
        name: "IX_GameComment_GameInfoId",
        schema: "TGZ",
        table: "GameComment",
        column: "GameInfoId");

      migrationBuilder.AddForeignKey(
        name: "FK_GameImage_GameInfo_GameInfoId",
        schema: "TGZ",
        table: "GameImage",
        column: "GameInfoId",
        principalSchema: "TGZ",
        principalTable: "GameInfo",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
        name: "FK_GameStatistic_GameInfo_GameInfoId",
        schema: "TGZ",
        table: "GameStatistic",
        column: "GameInfoId",
        principalSchema: "TGZ",
        principalTable: "GameInfo",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade);
    }
  }
}
