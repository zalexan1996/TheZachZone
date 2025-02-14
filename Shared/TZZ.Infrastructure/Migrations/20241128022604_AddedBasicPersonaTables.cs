using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBasicPersonaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PP");

            migrationBuilder.CreateTable(
                name: "Arcana",
                schema: "PP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arcana", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                schema: "PP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                schema: "PP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Game_GameId",
                        column: x => x.GameId,
                        principalSchema: "PP",
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialLink",
                schema: "PP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    ArcanaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialLink_Arcana_ArcanaId",
                        column: x => x.ArcanaId,
                        principalSchema: "PP",
                        principalTable: "Arcana",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SocialLink_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalSchema: "PP",
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arcana_Name",
                schema: "PP",
                table: "Arcana",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_GameId",
                schema: "PP",
                table: "Character",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_Name",
                schema: "PP",
                table: "Character",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_Name",
                schema: "PP",
                table: "Game",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialLink_ArcanaId",
                schema: "PP",
                table: "SocialLink",
                column: "ArcanaId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialLink_CharacterId",
                schema: "PP",
                table: "SocialLink",
                column: "CharacterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialLink",
                schema: "PP");

            migrationBuilder.DropTable(
                name: "Arcana",
                schema: "PP");

            migrationBuilder.DropTable(
                name: "Character",
                schema: "PP");

            migrationBuilder.DropTable(
                name: "Game",
                schema: "PP");
        }
    }
}
