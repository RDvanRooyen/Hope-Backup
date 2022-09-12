using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class ContentDrivationExternalLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalLink",
                table: "ContentDerivations");

            migrationBuilder.CreateTable(
                name: "ContentExternalLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    ContentDetailsId = table.Column<int>(type: "int", nullable: true),
                    ExternalLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentExternalLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentExternalLinks_ContentDetails_ContentDetailsId",
                        column: x => x.ContentDetailsId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentExternalLinks_ContentDetailsId",
                table: "ContentExternalLinks",
                column: "ContentDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentExternalLinks");

            migrationBuilder.AddColumn<string>(
                name: "ExternalLink",
                table: "ContentDerivations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
