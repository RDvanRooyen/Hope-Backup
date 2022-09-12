using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class LinkCheckingReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkCheckingReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExternalLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentDetailsId = table.Column<int>(type: "int", nullable: true),
                    NotificationSentDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkCheckingReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkCheckingReport_ContentDetails_ContentDetailsId",
                        column: x => x.ContentDetailsId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkCheckingReport_ContentDetailsId",
                table: "LinkCheckingReport",
                column: "ContentDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkCheckingReport");
        }
    }
}
