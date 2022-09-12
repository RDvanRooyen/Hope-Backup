using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class DbUpdateCommentEntiyFieldsAltered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "ContactUsRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondDate",
                table: "ContactUsRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Responded",
                table: "ContactUsRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "ContactUsRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "ContactUsRecords");

            migrationBuilder.DropColumn(
                name: "RespondDate",
                table: "ContactUsRecords");

            migrationBuilder.DropColumn(
                name: "Responded",
                table: "ContactUsRecords");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "ContactUsRecords");
        }
    }
}
