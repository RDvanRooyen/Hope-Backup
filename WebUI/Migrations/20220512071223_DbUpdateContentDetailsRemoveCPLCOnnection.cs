using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class DbUpdateContentDetailsRemoveCPLCOnnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentCPLConnections");

            migrationBuilder.RenameColumn(
                name: "Connection",
                table: "ContentDetails",
                newName: "CPLConnection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPLConnection",
                table: "ContentDetails",
                newName: "Connection");

            migrationBuilder.CreateTable(
                name: "ContentCPLConnections",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    CPLConnectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentCPLConnections", x => new { x.ContentId, x.CPLConnectionId });
                    table.ForeignKey(
                        name: "FK_ContentCPLConnections_ContentDetails_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentCPLConnections_CPLConnections_CPLConnectionId",
                        column: x => x.CPLConnectionId,
                        principalTable: "CPLConnections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentCPLConnections_CPLConnectionId",
                table: "ContentCPLConnections",
                column: "CPLConnectionId");
        }
    }
}
