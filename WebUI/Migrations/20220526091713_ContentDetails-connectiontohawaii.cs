using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class ContentDetailsconnectiontohawaii : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectionToHawaii",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceName",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionToHawaii",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "PlaceName",
                table: "ContentDetails");
        }
    }
}
