using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class HugeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Cultures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CultureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CultureCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileReferences",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrapeSorts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrapeSorts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessParameters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessPhases",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessPhases", x => x.Id);
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "WineMaterialBatches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HarvestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HarvestLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrapeSortId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineMaterialBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WineMaterialBatches_GrapeSorts_GrapeSortId",
                        column: x => x.GrapeSortId,
                        principalTable: "GrapeSorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrapeSortPhases",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GrapeSortId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    PreviousPhaseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrapeSortPhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrapeSortPhases_GrapeSortPhases_PreviousPhaseId",
                        column: x => x.PreviousPhaseId,
                        principalTable: "GrapeSortPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrapeSortPhases_GrapeSorts_GrapeSortId",
                        column: x => x.GrapeSortId,
                        principalTable: "GrapeSorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrapeSortPhases_ProcessPhases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "ProcessPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CultureId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcessParameterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProcessPhaseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localizations_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Localizations_ProcessParameters_ProcessParameterId",
                        column: x => x.ProcessParameterId,
                        principalTable: "ProcessParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Localizations_ProcessPhases_ProcessPhaseId",
                        column: x => x.ProcessPhaseId,
                        principalTable: "ProcessPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessPhaseParameters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParameterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessPhaseParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessPhaseParameters_ProcessParameters_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "ProcessParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessPhaseParameters_ProcessPhases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "ProcessPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrapeSortPhaseDatasets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrapeSortPhaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DatasetFileReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrapeSortPhaseDatasets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrapeSortPhaseDatasets_FileReferences_DatasetFileReferenceId",
                        column: x => x.DatasetFileReferenceId,
                        principalTable: "FileReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrapeSortPhaseDatasets_GrapeSortPhases_GrapeSortPhaseId",
                        column: x => x.GrapeSortPhaseId,
                        principalTable: "GrapeSortPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrapeSortPhaseForecastModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Accuracy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrapeSortPhaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ForecastingModelFileReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrapeSortPhaseForecastModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrapeSortPhaseForecastModels_FileReferences_ForecastingModelFileReferenceId",
                        column: x => x.ForecastingModelFileReferenceId,
                        principalTable: "FileReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrapeSortPhaseForecastModels_GrapeSortPhases_GrapeSortPhaseId",
                        column: x => x.GrapeSortPhaseId,
                        principalTable: "GrapeSortPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WineMaterialBatchProcessPhases",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    WineMaterialBatchId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GrapeSortPhaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineMaterialBatchProcessPhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WineMaterialBatchProcessPhases_GrapeSortPhases_GrapeSortPhaseId",
                        column: x => x.GrapeSortPhaseId,
                        principalTable: "GrapeSortPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WineMaterialBatchProcessPhases_WineMaterialBatches_WineMaterialBatchId",
                        column: x => x.WineMaterialBatchId,
                        principalTable: "WineMaterialBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrapeSortProcessPhaseParameterStandard",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LowerBound = table.Column<double>(type: "float", nullable: true),
                    UpperBound = table.Column<double>(type: "float", nullable: true),
                    GrapeSortPhaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhaseParameterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrapeSortProcessPhaseParameterStandard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrapeSortProcessPhaseParameterStandard_GrapeSortPhases_GrapeSortPhaseId",
                        column: x => x.GrapeSortPhaseId,
                        principalTable: "GrapeSortPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrapeSortProcessPhaseParameterStandard_ProcessPhaseParameters_PhaseParameterId",
                        column: x => x.PhaseParameterId,
                        principalTable: "ProcessPhaseParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WineMaterialBatchProcessPhaseParameters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhaseParameterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WineMaterialBatchGrapeSortPhaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineMaterialBatchProcessPhaseParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WineMaterialBatchProcessPhaseParameters_ProcessPhaseParameters_PhaseParameterId",
                        column: x => x.PhaseParameterId,
                        principalTable: "ProcessPhaseParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WineMaterialBatchProcessPhaseParameters_WineMaterialBatchProcessPhases_WineMaterialBatchGrapeSortPhaseId",
                        column: x => x.WineMaterialBatchGrapeSortPhaseId,
                        principalTable: "WineMaterialBatchProcessPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WineMaterialBatchProcessParameterValues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    PhaseParameterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineMaterialBatchProcessParameterValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WineMaterialBatchProcessParameterValues_WineMaterialBatchProcessPhaseParameters_PhaseParameterId",
                        column: x => x.PhaseParameterId,
                        principalTable: "WineMaterialBatchProcessPhaseParameters",
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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseDatasets_DatasetFileReferenceId",
                table: "GrapeSortPhaseDatasets",
                column: "DatasetFileReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseDatasets_GrapeSortPhaseId_DatasetFileReferenceId",
                table: "GrapeSortPhaseDatasets",
                columns: new[] { "GrapeSortPhaseId", "DatasetFileReferenceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseForecastModels_ForecastingModelFileReferenceId",
                table: "GrapeSortPhaseForecastModels",
                column: "ForecastingModelFileReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseForecastModels_GrapeSortPhaseId_ForecastingModelFileReferenceId",
                table: "GrapeSortPhaseForecastModels",
                columns: new[] { "GrapeSortPhaseId", "ForecastingModelFileReferenceId" },
                unique: true,
                filter: "[ForecastingModelFileReferenceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhases_GrapeSortId",
                table: "GrapeSortPhases",
                column: "GrapeSortId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhases_Order",
                table: "GrapeSortPhases",
                column: "Order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhases_PhaseId",
                table: "GrapeSortPhases",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhases_PreviousPhaseId",
                table: "GrapeSortPhases",
                column: "PreviousPhaseId",
                unique: true,
                filter: "[PreviousPhaseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortProcessPhaseParameterStandard_GrapeSortPhaseId",
                table: "GrapeSortProcessPhaseParameterStandard",
                column: "GrapeSortPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortProcessPhaseParameterStandard_PhaseParameterId",
                table: "GrapeSortProcessPhaseParameterStandard",
                column: "PhaseParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSorts_Name",
                table: "GrapeSorts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_CultureId",
                table: "Localizations",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_ProcessParameterId",
                table: "Localizations",
                column: "ProcessParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_ProcessPhaseId",
                table: "Localizations",
                column: "ProcessPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPhaseParameters_ParameterId_PhaseId",
                table: "ProcessPhaseParameters",
                columns: new[] { "ParameterId", "PhaseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPhaseParameters_PhaseId",
                table: "ProcessPhaseParameters",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_WineMaterialBatches_GrapeSortId",
                table: "WineMaterialBatches",
                column: "GrapeSortId");

            migrationBuilder.CreateIndex(
                name: "IX_WineMaterialBatchProcessParameterValues_PhaseParameterId",
                table: "WineMaterialBatchProcessParameterValues",
                column: "PhaseParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_WineMaterialBatchProcessPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchProcessPhaseParameters",
                column: "PhaseParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_WineMaterialBatchProcessPhaseParameters_WineMaterialBatchGrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhaseParameters",
                column: "WineMaterialBatchGrapeSortPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_WineMaterialBatchProcessPhases_GrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhases",
                column: "GrapeSortPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_WineMaterialBatchProcessPhases_WineMaterialBatchId_GrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhases",
                columns: new[] { "WineMaterialBatchId", "GrapeSortPhaseId" },
                unique: true);
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
                name: "GrapeSortPhaseDatasets");

            migrationBuilder.DropTable(
                name: "GrapeSortPhaseForecastModels");

            migrationBuilder.DropTable(
                name: "GrapeSortProcessPhaseParameterStandard");

            migrationBuilder.DropTable(
                name: "Localizations");

            migrationBuilder.DropTable(
                name: "WineMaterialBatchProcessParameterValues");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FileReferences");

            migrationBuilder.DropTable(
                name: "Cultures");

            migrationBuilder.DropTable(
                name: "WineMaterialBatchProcessPhaseParameters");

            migrationBuilder.DropTable(
                name: "ProcessPhaseParameters");

            migrationBuilder.DropTable(
                name: "WineMaterialBatchProcessPhases");

            migrationBuilder.DropTable(
                name: "ProcessParameters");

            migrationBuilder.DropTable(
                name: "GrapeSortPhases");

            migrationBuilder.DropTable(
                name: "WineMaterialBatches");

            migrationBuilder.DropTable(
                name: "ProcessPhases");

            migrationBuilder.DropTable(
                name: "GrapeSorts");
        }
    }
}
