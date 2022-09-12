using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class contentdetailssearchfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorText",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoAuthorsText",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradesText",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhotoPath",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectsText",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopicsText",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorText",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "CoAuthorsText",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "GradesText",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "PrimaryPhotoPath",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "SubjectsText",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "TopicsText",
                table: "ContentDetails");
        }
    }
}
