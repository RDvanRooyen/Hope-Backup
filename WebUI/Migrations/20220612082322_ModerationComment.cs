using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class ModerationComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ModerationComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentDetailsId = table.Column<int>(type: "int", nullable: true),
                    ContentDetailsId = table.Column<int>(type: "int", nullable: true),
                    ModeratorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsInternalComment = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModerationComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModerationComments_AspNetUsers_ModeratorId",
                        column: x => x.ModeratorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModerationComments_ContentDetails_ContentDetailsId",
                        column: x => x.ContentDetailsId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModerationComments_ContentDetailsId",
                table: "ModerationComments",
                column: "ContentDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ModerationComments_ModeratorId",
                table: "ModerationComments",
                column: "ModeratorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModerationComments");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ContentDetails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
