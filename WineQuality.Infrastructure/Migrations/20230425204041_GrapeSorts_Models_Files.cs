using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class GrapeSorts_Models_Files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GrapeSortId",
                table: "WineMaterialBatches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FileReferences",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContainerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "GrapeSortPhaseForecastModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhaseTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GrapeSortId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ForecastingModelFileReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        name: "FK_GrapeSortPhaseForecastModels_GrapeSorts_GrapeSortId",
                        column: x => x.GrapeSortId,
                        principalTable: "GrapeSorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrapeSortPhaseForecastModels_ProcessPhaseTypes_PhaseTypeId",
                        column: x => x.PhaseTypeId,
                        principalTable: "ProcessPhaseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WineMaterialBatches_GrapeSortId",
                table: "WineMaterialBatches",
                column: "GrapeSortId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseForecastModels_ForecastingModelFileReferenceId",
                table: "GrapeSortPhaseForecastModels",
                column: "ForecastingModelFileReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseForecastModels_GrapeSortId",
                table: "GrapeSortPhaseForecastModels",
                column: "GrapeSortId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseForecastModels_PhaseTypeId",
                table: "GrapeSortPhaseForecastModels",
                column: "PhaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSorts_Name",
                table: "GrapeSorts",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatches_GrapeSorts_GrapeSortId",
                table: "WineMaterialBatches",
                column: "GrapeSortId",
                principalTable: "GrapeSorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatches_GrapeSorts_GrapeSortId",
                table: "WineMaterialBatches");

            migrationBuilder.DropTable(
                name: "GrapeSortPhaseForecastModels");

            migrationBuilder.DropTable(
                name: "FileReferences");

            migrationBuilder.DropTable(
                name: "GrapeSorts");

            migrationBuilder.DropIndex(
                name: "IX_WineMaterialBatches_GrapeSortId",
                table: "WineMaterialBatches");

            migrationBuilder.DropColumn(
                name: "GrapeSortId",
                table: "WineMaterialBatches");
        }
    }
}
