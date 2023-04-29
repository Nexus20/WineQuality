using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class Indices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WineMaterialBatchProcessPhases_WineMaterialBatchId",
                table: "WineMaterialBatchProcessPhases");

            migrationBuilder.DropIndex(
                name: "IX_ProcessPhaseParameters_ParameterId",
                table: "ProcessPhaseParameters");

            migrationBuilder.CreateIndex(
                name: "IX_WineMaterialBatchProcessPhases_WineMaterialBatchId_PhaseTypeId",
                table: "WineMaterialBatchProcessPhases",
                columns: new[] { "WineMaterialBatchId", "PhaseTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPhaseParameters_ParameterId_PhaseTypeId",
                table: "ProcessPhaseParameters",
                columns: new[] { "ParameterId", "PhaseTypeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WineMaterialBatchProcessPhases_WineMaterialBatchId_PhaseTypeId",
                table: "WineMaterialBatchProcessPhases");

            migrationBuilder.DropIndex(
                name: "IX_ProcessPhaseParameters_ParameterId_PhaseTypeId",
                table: "ProcessPhaseParameters");

            migrationBuilder.CreateIndex(
                name: "IX_WineMaterialBatchProcessPhases_WineMaterialBatchId",
                table: "WineMaterialBatchProcessPhases",
                column: "WineMaterialBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPhaseParameters_ParameterId",
                table: "ProcessPhaseParameters",
                column: "ParameterId");
        }
    }
}
