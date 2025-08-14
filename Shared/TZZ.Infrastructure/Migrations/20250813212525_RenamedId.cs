using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SocialLinkGiftId",
                schema: "PP",
                table: "SocialLinkGift",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ActivityLogId",
                schema: "PocketPersona",
                table: "ActivityLog",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "PP",
                table: "SocialLinkGift",
                newName: "SocialLinkGiftId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "PocketPersona",
                table: "ActivityLog",
                newName: "ActivityLogId");
        }
    }
}
