using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class contentcommentaddfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentComments_ContentDetails_ContentDetailsId",
                table: "ContentComments");

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ContentComments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentComments_ContentDetails_ContentDetailsId",
                table: "ContentComments",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentComments_ContentDetails_ContentDetailsId",
                table: "ContentComments");

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ContentComments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentComments_ContentDetails_ContentDetailsId",
                table: "ContentComments",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
