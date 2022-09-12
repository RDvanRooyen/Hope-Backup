using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class contentdetailscleanup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentExternalLinks_ContentDetails_ContentDetailsId",
                table: "ContentExternalLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentQuestions_ContentDetails_ContentDetailsId",
                table: "ContentQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentRatings_ContentDetails_ContentDetailsId",
                table: "ContentRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationComments_ContentDetails_ContentDetailsId",
                table: "ModerationComments");

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ModerationComments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ContentRatings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ContentQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ContentExternalLinks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentExternalLinks_ContentDetails_ContentDetailsId",
                table: "ContentExternalLinks",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentQuestions_ContentDetails_ContentDetailsId",
                table: "ContentQuestions",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentRatings_ContentDetails_ContentDetailsId",
                table: "ContentRatings",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModerationComments_ContentDetails_ContentDetailsId",
                table: "ModerationComments",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentExternalLinks_ContentDetails_ContentDetailsId",
                table: "ContentExternalLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentQuestions_ContentDetails_ContentDetailsId",
                table: "ContentQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentRatings_ContentDetails_ContentDetailsId",
                table: "ContentRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationComments_ContentDetails_ContentDetailsId",
                table: "ModerationComments");

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ModerationComments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ContentRatings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ContentQuestions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContentDetailsId",
                table: "ContentExternalLinks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentExternalLinks_ContentDetails_ContentDetailsId",
                table: "ContentExternalLinks",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentQuestions_ContentDetails_ContentDetailsId",
                table: "ContentQuestions",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentRatings_ContentDetails_ContentDetailsId",
                table: "ContentRatings",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModerationComments_ContentDetails_ContentDetailsId",
                table: "ModerationComments",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
