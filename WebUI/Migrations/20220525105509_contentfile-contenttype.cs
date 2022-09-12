using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class contentfilecontenttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ContentFiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ContentFiles");
        }
    }
}
