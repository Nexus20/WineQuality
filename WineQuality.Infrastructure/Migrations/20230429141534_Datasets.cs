using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class Datasets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ForecastingModelFileReferenceId",
                table: "GrapeSortPhaseForecastModels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "GrapeSortPhaseDatasets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrapeSortPhaseForecastModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        name: "FK_GrapeSortPhaseDatasets_GrapeSortPhaseForecastModels_GrapeSortPhaseForecastModelId",
                        column: x => x.GrapeSortPhaseForecastModelId,
                        principalTable: "GrapeSortPhaseForecastModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseDatasets_DatasetFileReferenceId",
                table: "GrapeSortPhaseDatasets",
                column: "DatasetFileReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseDatasets_GrapeSortPhaseForecastModelId",
                table: "GrapeSortPhaseDatasets",
                column: "GrapeSortPhaseForecastModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrapeSortPhaseDatasets");

            migrationBuilder.AlterColumn<string>(
                name: "ForecastingModelFileReferenceId",
                table: "GrapeSortPhaseForecastModels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
