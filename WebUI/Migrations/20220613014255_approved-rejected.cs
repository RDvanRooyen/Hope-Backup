using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class approvedrejected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedById",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectionDateTime",
                table: "ContentDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "RejectedById",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "RejectionDateTime",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "ContentDetails");
        }
    }
}
