using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class QualityPredictionHistory3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WineMaterialBatchGrapeSortPhaseId",
                table: "QualityPredictions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_QualityPredictions_WineMaterialBatchGrapeSortPhaseId",
                table: "QualityPredictions",
                column: "WineMaterialBatchGrapeSortPhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_QualityPredictions_WineMaterialBatchGrapeSortPhases_WineMaterialBatchGrapeSortPhaseId",
                table: "QualityPredictions",
                column: "WineMaterialBatchGrapeSortPhaseId",
                principalTable: "WineMaterialBatchGrapeSortPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QualityPredictions_WineMaterialBatchGrapeSortPhases_WineMaterialBatchGrapeSortPhaseId",
                table: "QualityPredictions");

            migrationBuilder.DropIndex(
                name: "IX_QualityPredictions_WineMaterialBatchGrapeSortPhaseId",
                table: "QualityPredictions");

            migrationBuilder.DropColumn(
                name: "WineMaterialBatchGrapeSortPhaseId",
                table: "QualityPredictions");
        }
    }
}
