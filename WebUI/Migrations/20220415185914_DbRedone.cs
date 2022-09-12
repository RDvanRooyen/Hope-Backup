using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class DbRedone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfilePhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "ApplicationUser$tblAspNetUsersChangedBy",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ApplicationUser$tblAspNetUsersCreatedBy",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CompanyAddress_CompanyAddressLine1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyAddress_CompanyCity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyAddress_CompanyState = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyAddress_CompanyZipCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyPhoneNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CompanyReportEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CompanySenderEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CompanyLogoImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyFavicon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingRate = table.Column<decimal>(type: "money", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "Client$tblAspNetUsersChangedBy",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Client$tblAspNetUsersCreatedBy",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenEduLicAdd = table.Column<bool>(type: "bit", nullable: false),
                    EssentialQuestion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Objective = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Connection = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    MaterialsText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaterialFiles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourcesText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ResourcesFiles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ConnectionPdf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormativeAssessments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormativeAssessmentsFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SummativeAssessments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SummativeAssessmentsFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacingGuideFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtifactsFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowUserEmail = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "ContentDetails$tblAspNetUsersChangedBy",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ContentDetails$tblAspNetUsersCreatedBy",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentDetails_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CPLConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPLConnections", x => x.Id);
                    table.ForeignKey(
                        name: "CPLConnection$tblAspNetUsersChangedBy",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "CPLConnection$tblAspNetUsersCreatedBy",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CPLConnections_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Grade$tblAspNetUsersChangedBy",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Grade$tblAspNetUsersCreatedBy",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Standards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Standards_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Standard$tblAspNetUsersChangedBy",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Standard$tblAspNetUsersCreatedBy",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Subject$tblAspNetUsersChangedBy",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Subject$tblAspNetUsersCreatedBy",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Topic$tblAspNetUsersChangedBy",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Topic$tblAspNetUsersCreatedBy",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceFrequency = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingType = table.Column<int>(type: "int", nullable: false),
                    BillingRate = table.Column<decimal>(type: "money", nullable: false),
                    Deposit = table.Column<decimal>(type: "money", nullable: false),
                    ReferralPercent = table.Column<decimal>(type: "money", nullable: false),
                    EstimatedRevenue = table.Column<decimal>(type: "money", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Project$tblAspNetUsersChangedBy",
                        column: x => x.ChangedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Project$tblAspNetUsersCreatedBy",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentAuthors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContentDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentAuthors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentAuthors_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentAuthors_ContentDetails_ContentDetailsId",
                        column: x => x.ContentDetailsId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentDerivations",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    DerivationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentDerivations", x => new { x.ContentId, x.DerivationId });
                    table.ForeignKey(
                        name: "FK_ContentDerivations_ContentDetails_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentDerivations_ContentDetails_DerivationId",
                        column: x => x.DerivationId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelitivePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentFiles_ContentDetails_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "ContentGrades",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentGrades", x => new { x.ContentId, x.GradeId });
                    table.ForeignKey(
                        name: "FK_ContentGrades_ContentDetails_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentGrades_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGrades",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGrades", x => new { x.UserId, x.GradeId });
                    table.ForeignKey(
                        name: "FK_UserGrades_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGrades_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentStandards",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    StandardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentStandards", x => new { x.ContentId, x.StandardId });
                    table.ForeignKey(
                        name: "FK_ContentStandards_ContentDetails_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentStandards_Standards_StandardId",
                        column: x => x.StandardId,
                        principalTable: "Standards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentSubjects",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSubjects", x => new { x.ContentId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ContentSubjects_ContentDetails_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSubjects",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubjects", x => new { x.UserId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_UserSubjects_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentTopics",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTopics", x => new { x.ContentId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_ContentTopics_ContentDetails_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentTopics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChangedById",
                table: "AspNetUsers",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreatedById",
                table: "AspNetUsers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DeletedById",
                table: "AspNetUsers",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ChangedById",
                table: "Clients",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CreatedById",
                table: "Clients",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DeletedById",
                table: "Clients",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAuthors_AuthorId",
                table: "ContentAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAuthors_ContentDetailsId",
                table: "ContentAuthors",
                column: "ContentDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentCPLConnections_CPLConnectionId",
                table: "ContentCPLConnections",
                column: "CPLConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentDerivations_DerivationId",
                table: "ContentDerivations",
                column: "DerivationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentDetails_ChangedById",
                table: "ContentDetails",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContentDetails_CreatedById",
                table: "ContentDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContentDetails_DeletedById",
                table: "ContentDetails",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContentFiles_ContentId",
                table: "ContentFiles",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentGrades_GradeId",
                table: "ContentGrades",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentStandards_StandardId",
                table: "ContentStandards",
                column: "StandardId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentSubjects_SubjectId",
                table: "ContentSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTopics_TopicId",
                table: "ContentTopics",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_CPLConnections_ChangedById",
                table: "CPLConnections",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_CPLConnections_CreatedById",
                table: "CPLConnections",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CPLConnections_DeletedById",
                table: "CPLConnections",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_ChangedById",
                table: "Grades",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CreatedById",
                table: "Grades",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_DeletedById",
                table: "Grades",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ChangedById",
                table: "Projects",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeletedById",
                table: "Projects",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Standards_ChangedById",
                table: "Standards",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_Standards_CreatedById",
                table: "Standards",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Standards_DeletedById",
                table: "Standards",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ChangedById",
                table: "Subjects",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CreatedById",
                table: "Subjects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_DeletedById",
                table: "Subjects",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ChangedById",
                table: "Topics",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CreatedById",
                table: "Topics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_DeletedById",
                table: "Topics",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGrades_GradeId",
                table: "UserGrades",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubjects_SubjectId",
                table: "UserSubjects",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "ContentAuthors");

            migrationBuilder.DropTable(
                name: "ContentCPLConnections");

            migrationBuilder.DropTable(
                name: "ContentDerivations");

            migrationBuilder.DropTable(
                name: "ContentFiles");

            migrationBuilder.DropTable(
                name: "ContentGrades");

            migrationBuilder.DropTable(
                name: "ContentStandards");

            migrationBuilder.DropTable(
                name: "ContentSubjects");

            migrationBuilder.DropTable(
                name: "ContentTopics");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "UserGrades");

            migrationBuilder.DropTable(
                name: "UserSubjects");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CPLConnections");

            migrationBuilder.DropTable(
                name: "Standards");

            migrationBuilder.DropTable(
                name: "ContentDetails");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
