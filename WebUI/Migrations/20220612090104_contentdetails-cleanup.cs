using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class contentdetailscleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentDetailsId",
                table: "ModerationComments");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "ContentRatings");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "ContentQuestions");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "ContentExternalLinks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentDetailsId",
                table: "ModerationComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "ContentRatings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "ContentQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "ContentExternalLinks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
