using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class DbUpdateContentDetailsUpdateCPLCOnnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPLConnection",
                table: "ContentDetails");

            migrationBuilder.AddColumn<int>(
                name: "CPLConnectionId",
                table: "ContentDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentDetails_CPLConnectionId",
                table: "ContentDetails",
                column: "CPLConnectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentDetails_CPLConnections_CPLConnectionId",
                table: "ContentDetails",
                column: "CPLConnectionId",
                principalTable: "CPLConnections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentDetails_CPLConnections_CPLConnectionId",
                table: "ContentDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContentDetails_CPLConnectionId",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "CPLConnectionId",
                table: "ContentDetails");

            migrationBuilder.AddColumn<string>(
                name: "CPLConnection",
                table: "ContentDetails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
