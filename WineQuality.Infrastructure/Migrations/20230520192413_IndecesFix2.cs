using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class IndecesFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrapeSortPhaseDatasets_FileReferences_DatasetFileReferenceId",
                table: "GrapeSortPhaseDatasets");

            migrationBuilder.DropForeignKey(
                name: "FK_GrapeSortPhaseForecastModels_FileReferences_ForecastingModelFileReferenceId",
                table: "GrapeSortPhaseForecastModels");

            migrationBuilder.AddForeignKey(
                name: "FK_GrapeSortPhaseDatasets_FileReferences_DatasetFileReferenceId",
                table: "GrapeSortPhaseDatasets",
                column: "DatasetFileReferenceId",
                principalTable: "FileReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GrapeSortPhaseForecastModels_FileReferences_ForecastingModelFileReferenceId",
                table: "GrapeSortPhaseForecastModels",
                column: "ForecastingModelFileReferenceId",
                principalTable: "FileReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrapeSortPhaseDatasets_FileReferences_DatasetFileReferenceId",
                table: "GrapeSortPhaseDatasets");

            migrationBuilder.DropForeignKey(
                name: "FK_GrapeSortPhaseForecastModels_FileReferences_ForecastingModelFileReferenceId",
                table: "GrapeSortPhaseForecastModels");

            migrationBuilder.AddForeignKey(
                name: "FK_GrapeSortPhaseDatasets_FileReferences_DatasetFileReferenceId",
                table: "GrapeSortPhaseDatasets",
                column: "DatasetFileReferenceId",
                principalTable: "FileReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrapeSortPhaseForecastModels_FileReferences_ForecastingModelFileReferenceId",
                table: "GrapeSortPhaseForecastModels",
                column: "ForecastingModelFileReferenceId",
                principalTable: "FileReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
