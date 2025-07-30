using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class SimplifiedGenreTable : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_GameGenre_Genre_GenresGenreId",
        schema: "TGZ",
        table: "GameGenre");

      migrationBuilder.DropPrimaryKey(
        name: "PK_Genre",
        schema: "TGZ",
        table: "Genre");

      migrationBuilder.DropPrimaryKey(
        name: "PK_GameGenre",
        schema: "TGZ",
        table: "GameGenre");

      migrationBuilder.DropIndex(
        name: "IX_GameGenre_GenresGenreId",
        schema: "TGZ",
        table: "GameGenre");

      migrationBuilder.DropColumn(
        name: "GenreId",
        schema: "TGZ",
        table: "Genre");

      migrationBuilder.DropColumn(
        name: "GenresGenreId",
        schema: "TGZ",
        table: "GameGenre");

      migrationBuilder.AddColumn<string>(
        name: "GenresName",
        schema: "TGZ",
        table: "GameGenre",
        type: "nvarchar(100)",
        nullable: false,
        defaultValue: "");

      migrationBuilder.AddPrimaryKey(
        name: "PK_Genre",
        schema: "TGZ",
        table: "Genre",
        column: "Name");

      migrationBuilder.AddPrimaryKey(
        name: "PK_GameGenre",
        schema: "TGZ",
        table: "GameGenre",
        columns: new[] { "GameId", "GenresName" });

      migrationBuilder.CreateIndex(
        name: "IX_Genre_Name",
        schema: "TGZ",
        table: "Genre",
        column: "Name",
        unique: true);

      migrationBuilder.CreateIndex(
        name: "IX_GameGenre_GenresName",
        schema: "TGZ",
        table: "GameGenre",
        column: "GenresName");

      migrationBuilder.AddForeignKey(
        name: "FK_GameGenre_Genre_GenresName",
        schema: "TGZ",
        table: "GameGenre",
        column: "GenresName",
        principalSchema: "TGZ",
        principalTable: "Genre",
        principalColumn: "Name",
        onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_GameGenre_Genre_GenresName",
        schema: "TGZ",
        table: "GameGenre");

      migrationBuilder.DropPrimaryKey(
        name: "PK_Genre",
        schema: "TGZ",
        table: "Genre");

      migrationBuilder.DropIndex(
        name: "IX_Genre_Name",
        schema: "TGZ",
        table: "Genre");

      migrationBuilder.DropPrimaryKey(
        name: "PK_GameGenre",
        schema: "TGZ",
        table: "GameGenre");

      migrationBuilder.DropIndex(
        name: "IX_GameGenre_GenresName",
        schema: "TGZ",
        table: "GameGenre");

      migrationBuilder.DropColumn(
        name: "GenresName",
        schema: "TGZ",
        table: "GameGenre");

      migrationBuilder.AddColumn<int>(
        name: "GenreId",
        schema: "TGZ",
        table: "Genre",
        type: "int",
        nullable: false,
        defaultValue: 0)
        .Annotation("SqlServer:Identity", "1, 1");

      migrationBuilder.AddColumn<int>(
        name: "GenresGenreId",
        schema: "TGZ",
        table: "GameGenre",
        type: "int",
        nullable: false,
        defaultValue: 0);

      migrationBuilder.AddPrimaryKey(
        name: "PK_Genre",
        schema: "TGZ",
        table: "Genre",
        column: "GenreId");

      migrationBuilder.AddPrimaryKey(
        name: "PK_GameGenre",
        schema: "TGZ",
        table: "GameGenre",
        columns: new[] { "GameId", "GenresGenreId" });

      migrationBuilder.CreateIndex(
        name: "IX_GameGenre_GenresGenreId",
        schema: "TGZ",
        table: "GameGenre",
        column: "GenresGenreId");

      migrationBuilder.AddForeignKey(
        name: "FK_GameGenre_Genre_GenresGenreId",
        schema: "TGZ",
        table: "GameGenre",
        column: "GenresGenreId",
        principalSchema: "TGZ",
        principalTable: "Genre",
        principalColumn: "GenreId",
        onDelete: ReferentialAction.Cascade);
    }
  }
}
