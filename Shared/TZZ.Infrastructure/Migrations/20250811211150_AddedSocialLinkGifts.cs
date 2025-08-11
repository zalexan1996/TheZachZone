using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSocialLinkGifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialLinkGift",
                schema: "PP",
                columns: table => new
                {
                    SocialLinkGiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcquiredAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialLinkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLinkGift", x => x.SocialLinkGiftId);
                    table.ForeignKey(
                        name: "FK_SocialLinkGift_SocialLink_SocialLinkId",
                        column: x => x.SocialLinkId,
                        principalSchema: "PP",
                        principalTable: "SocialLink",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialLinkGift_SocialLinkId",
                schema: "PP",
                table: "SocialLinkGift",
                column: "SocialLinkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialLinkGift",
                schema: "PP");
        }
    }
}
