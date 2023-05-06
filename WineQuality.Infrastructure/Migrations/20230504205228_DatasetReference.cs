using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class DatasetReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DatasetId",
                table: "GrapeSortPhaseForecastModels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhaseForecastModels_DatasetId",
                table: "GrapeSortPhaseForecastModels",
                column: "DatasetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GrapeSortPhaseForecastModels_GrapeSortPhaseDatasets_DatasetId",
                table: "GrapeSortPhaseForecastModels",
                column: "DatasetId",
                principalTable: "GrapeSortPhaseDatasets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrapeSortPhaseForecastModels_GrapeSortPhaseDatasets_DatasetId",
                table: "GrapeSortPhaseForecastModels");

            migrationBuilder.DropIndex(
                name: "IX_GrapeSortPhaseForecastModels_DatasetId",
                table: "GrapeSortPhaseForecastModels");

            migrationBuilder.DropColumn(
                name: "DatasetId",
                table: "GrapeSortPhaseForecastModels");
        }
    }
}
