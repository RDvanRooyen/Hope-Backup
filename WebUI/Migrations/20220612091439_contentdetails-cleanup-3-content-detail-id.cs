using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class contentdetailscleanup3contentdetailid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentBookmarks_ContentDetails_ContentId",
                table: "ContentBookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentComments_ContentDetails_ContentDetailsId",
                table: "ContentComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentDerivations_ContentDetails_ContentId",
                table: "ContentDerivations");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentGrades_ContentDetails_ContentId",
                table: "ContentGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentQuestions_ContentDetails_ContentDetailsId",
                table: "ContentQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentRatings_ContentDetails_ContentDetailsId",
                table: "ContentRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentStandards_ContentDetails_ContentId",
                table: "ContentStandards");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentSubjects_ContentDetails_ContentId",
                table: "ContentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentTopics_ContentDetails_ContentId",
                table: "ContentTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationComments_ContentDetails_ContentDetailsId",
                table: "ModerationComments");

            migrationBuilder.DropColumn(
                name: "ArtifactsFile",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "CPLConnection",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "ConnectionPdf",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "FormativeAssessmentsFile",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "PacingGuideFile",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "ResourcesFiles",
                table: "ContentDetails");

            migrationBuilder.DropColumn(
                name: "SummativeAssessmentsFile",
                table: "ContentDetails");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "ContentTopics",
                newName: "ContentDetailsId");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "ContentSubjects",
                newName: "ContentDetailsId");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "ContentStandards",
                newName: "ContentDetailsId");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "ContentGrades",
                newName: "ContentDetailsId");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "ContentDerivations",
                newName: "ContentDetailsId");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "ContentBookmarks",
                newName: "ContentDetailsId");

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
                table: "ContentComments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBookmarks_ContentDetails_ContentDetailsId",
                table: "ContentBookmarks",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentComments_ContentDetails_ContentDetailsId",
                table: "ContentComments",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentDerivations_ContentDetails_ContentDetailsId",
                table: "ContentDerivations",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentGrades_ContentDetails_ContentDetailsId",
                table: "ContentGrades",
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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentRatings_ContentDetails_ContentDetailsId",
                table: "ContentRatings",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentStandards_ContentDetails_ContentDetailsId",
                table: "ContentStandards",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentSubjects_ContentDetails_ContentDetailsId",
                table: "ContentSubjects",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentTopics_ContentDetails_ContentDetailsId",
                table: "ContentTopics",
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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentBookmarks_ContentDetails_ContentDetailsId",
                table: "ContentBookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentComments_ContentDetails_ContentDetailsId",
                table: "ContentComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentDerivations_ContentDetails_ContentDetailsId",
                table: "ContentDerivations");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentGrades_ContentDetails_ContentDetailsId",
                table: "ContentGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentQuestions_ContentDetails_ContentDetailsId",
                table: "ContentQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentRatings_ContentDetails_ContentDetailsId",
                table: "ContentRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentStandards_ContentDetails_ContentDetailsId",
                table: "ContentStandards");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentSubjects_ContentDetails_ContentDetailsId",
                table: "ContentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentTopics_ContentDetails_ContentDetailsId",
                table: "ContentTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationComments_ContentDetails_ContentDetailsId",
                table: "ModerationComments");

            migrationBuilder.RenameColumn(
                name: "ContentDetailsId",
                table: "ContentTopics",
                newName: "ContentId");

            migrationBuilder.RenameColumn(
                name: "ContentDetailsId",
                table: "ContentSubjects",
                newName: "ContentId");

            migrationBuilder.RenameColumn(
                name: "ContentDetailsId",
                table: "ContentStandards",
                newName: "ContentId");

            migrationBuilder.RenameColumn(
                name: "ContentDetailsId",
                table: "ContentGrades",
                newName: "ContentId");

            migrationBuilder.RenameColumn(
                name: "ContentDetailsId",
                table: "ContentDerivations",
                newName: "ContentId");

            migrationBuilder.RenameColumn(
                name: "ContentDetailsId",
                table: "ContentBookmarks",
                newName: "ContentId");

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

            migrationBuilder.AddColumn<string>(
                name: "ArtifactsFile",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPLConnection",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionPdf",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormativeAssessmentsFile",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PacingGuideFile",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourcesFiles",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummativeAssessmentsFile",
                table: "ContentDetails",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "FK_ContentBookmarks_ContentDetails_ContentId",
                table: "ContentBookmarks",
                column: "ContentId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentComments_ContentDetails_ContentDetailsId",
                table: "ContentComments",
                column: "ContentDetailsId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentDerivations_ContentDetails_ContentId",
                table: "ContentDerivations",
                column: "ContentId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentGrades_ContentDetails_ContentId",
                table: "ContentGrades",
                column: "ContentId",
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
                name: "FK_ContentStandards_ContentDetails_ContentId",
                table: "ContentStandards",
                column: "ContentId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentSubjects_ContentDetails_ContentId",
                table: "ContentSubjects",
                column: "ContentId",
                principalTable: "ContentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentTopics_ContentDetails_ContentId",
                table: "ContentTopics",
                column: "ContentId",
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
    }
}
