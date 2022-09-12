using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class contentdetailssearchfieldsid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GradesIdText",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectsIdText",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopicsIdText",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradesIdText",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "SubjectsIdText",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "TopicsIdText",
                table: "ContentDetails");
        }
    }
}
