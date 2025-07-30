using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class WhoKnowsWhatSinceLastTime : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.EnsureSchema(
        name: "PocketPersona");

      migrationBuilder.AddColumn<string>(
        name: "PrimaryColor",
        schema: "PP",
        table: "Game",
        type: "nvarchar(9)",
        maxLength: 9,
        nullable: false,
        defaultValue: "");

      migrationBuilder.AddColumn<string>(
        name: "SecondaryColor",
        schema: "PP",
        table: "Game",
        type: "nvarchar(9)",
        maxLength: 9,
        nullable: false,
        defaultValue: "");

      migrationBuilder.AddColumn<byte[]>(
        name: "ImageBytes",
        schema: "PP",
        table: "Character",
        type: "varbinary(max)",
        nullable: false,
        defaultValue: new byte[0]);

      migrationBuilder.CreateTable(
        name: "ActivityLog",
        schema: "PocketPersona",
        columns: table => new
        {
          ActivityLogId = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          Activity = table.Column<string>(type: "nvarchar(max)", nullable: false),
          EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
          EntityId = table.Column<int>(type: "int", nullable: false),
          Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
          UserId = table.Column<int>(type: "int", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ActivityLog", x => x.ActivityLogId);
          table.ForeignKey(
            name: "FK_ActivityLog_AspNetUsers_UserId",
            column: x => x.UserId,
            principalTable: "AspNetUsers",
            principalColumn: "Id");
        });

      migrationBuilder.CreateIndex(
        name: "IX_ActivityLog_UserId",
        schema: "PocketPersona",
        table: "ActivityLog",
        column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
        name: "ActivityLog",
        schema: "PocketPersona");

      migrationBuilder.DropColumn(
        name: "PrimaryColor",
        schema: "PP",
        table: "Game");

      migrationBuilder.DropColumn(
        name: "SecondaryColor",
        schema: "PP",
        table: "Game");

      migrationBuilder.DropColumn(
        name: "ImageBytes",
        schema: "PP",
        table: "Character");
    }
  }
}
