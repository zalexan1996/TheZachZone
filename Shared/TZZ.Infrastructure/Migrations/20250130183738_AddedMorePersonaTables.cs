using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMorePersonaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialLinkRank",
                schema: "PP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialLinkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLinkRank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialLinkRank_SocialLink_SocialLinkId",
                        column: x => x.SocialLinkId,
                        principalSchema: "PP",
                        principalTable: "SocialLink",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialLinkDialogue",
                schema: "PP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ExtraRequirement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialLinkRankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLinkDialogue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialLinkDialogue_SocialLinkRank_SocialLinkRankId",
                        column: x => x.SocialLinkRankId,
                        principalSchema: "PP",
                        principalTable: "SocialLinkRank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialLinkDialogue_Order",
                schema: "PP",
                table: "SocialLinkDialogue",
                column: "Order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialLinkDialogue_SocialLinkRankId",
                schema: "PP",
                table: "SocialLinkDialogue",
                column: "SocialLinkRankId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialLinkRank_SocialLinkId",
                schema: "PP",
                table: "SocialLinkRank",
                column: "SocialLinkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialLinkDialogue",
                schema: "PP");

            migrationBuilder.DropTable(
                name: "SocialLinkRank",
                schema: "PP");
        }
    }
}
