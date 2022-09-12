using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class DbUpdateContentDetailsUpdatedWithDateSubmittedAndApproverAndContentBookmarksAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDateTime",
                table: "ContentDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedDateTime",
                table: "ContentDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ContentBookmarks",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentBookmarks", x => new { x.ContentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ContentBookmarks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentBookmarks_ContentDetails_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentBookmarks_UserId",
                table: "ContentBookmarks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentBookmarks");

            migrationBuilder.DropColumn(
                name: "ApprovedDateTime",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "SubmittedDateTime",
                table: "ContentDetails");
        }
    }
}
