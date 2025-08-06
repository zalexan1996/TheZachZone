using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCharacterProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialLinkDialogue_SocialLinkRank_SocialLinkRankId",
                schema: "PP",
                table: "SocialLinkDialogue");

            migrationBuilder.DropTable(
                name: "SocialLinkRank",
                schema: "PP");

            migrationBuilder.DropIndex(
                name: "IX_SocialLinkDialogue_Order",
                schema: "PP",
                table: "SocialLinkDialogue");

            migrationBuilder.RenameColumn(
                name: "SocialLinkRankId",
                schema: "PP",
                table: "SocialLinkDialogue",
                newName: "SocialLinkId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialLinkDialogue_SocialLinkRankId",
                schema: "PP",
                table: "SocialLinkDialogue",
                newName: "IX_SocialLinkDialogue_SocialLinkId");

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                schema: "PP",
                table: "SocialLinkDialogue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                schema: "TGZ",
                table: "GameImage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageBytes",
                schema: "PP",
                table: "Character",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateIndex(
                name: "IX_SocialLinkDialogue_Rank_Order",
                schema: "PP",
                table: "SocialLinkDialogue",
                columns: new[] { "Rank", "Order" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialLinkDialogue_SocialLink_SocialLinkId",
                schema: "PP",
                table: "SocialLinkDialogue",
                column: "SocialLinkId",
                principalSchema: "PP",
                principalTable: "SocialLink",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialLinkDialogue_SocialLink_SocialLinkId",
                schema: "PP",
                table: "SocialLinkDialogue");

            migrationBuilder.DropIndex(
                name: "IX_SocialLinkDialogue_Rank_Order",
                schema: "PP",
                table: "SocialLinkDialogue");

            migrationBuilder.DropColumn(
                name: "Rank",
                schema: "PP",
                table: "SocialLinkDialogue");

            migrationBuilder.RenameColumn(
                name: "SocialLinkId",
                schema: "PP",
                table: "SocialLinkDialogue",
                newName: "SocialLinkRankId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialLinkDialogue_SocialLinkId",
                schema: "PP",
                table: "SocialLinkDialogue",
                newName: "IX_SocialLinkDialogue_SocialLinkRankId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Data",
                schema: "TGZ",
                table: "GameImage",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageBytes",
                schema: "PP",
                table: "Character",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "SocialLinkRank",
                schema: "PP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialLinkId = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_SocialLinkDialogue_Order",
                schema: "PP",
                table: "SocialLinkDialogue",
                column: "Order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialLinkRank_SocialLinkId",
                schema: "PP",
                table: "SocialLinkRank",
                column: "SocialLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialLinkDialogue_SocialLinkRank_SocialLinkRankId",
                schema: "PP",
                table: "SocialLinkDialogue",
                column: "SocialLinkRankId",
                principalSchema: "PP",
                principalTable: "SocialLinkRank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
