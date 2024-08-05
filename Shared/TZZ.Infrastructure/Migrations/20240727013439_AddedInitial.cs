using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZZ.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TGZ");

            migrationBuilder.CreateTable(
                name: "GameInfo",
                schema: "TGZ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameInfoId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameComment_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "TGZ",
                        principalTable: "GameInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameImage",
                schema: "TGZ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameInfoId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UploadedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameImage_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "TGZ",
                        principalTable: "GameInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameStatistic",
                schema: "TGZ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameInfoId = table.Column<int>(type: "int", nullable: false),
                    PlayedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStatistic_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "TGZ",
                        principalTable: "GameInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameComment_GameInfoId",
                schema: "TGZ",
                table: "GameComment",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_GameImage_GameInfoId",
                schema: "TGZ",
                table: "GameImage",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistic_GameInfoId",
                schema: "TGZ",
                table: "GameStatistic",
                column: "GameInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameComment",
                schema: "TGZ");

            migrationBuilder.DropTable(
                name: "GameImage",
                schema: "TGZ");

            migrationBuilder.DropTable(
                name: "GameStatistic",
                schema: "TGZ");

            migrationBuilder.DropTable(
                name: "GameInfo",
                schema: "TGZ");
        }
    }
}
