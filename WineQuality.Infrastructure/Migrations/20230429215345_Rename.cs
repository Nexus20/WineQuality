using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchProcessParameterValues_WineMaterialBatchProcessPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchProcessParameterValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchProcessPhaseParameters_ProcessPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchProcessPhaseParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchProcessPhaseParameters_WineMaterialBatchProcessPhases_WineMaterialBatchGrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhaseParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchProcessPhases_GrapeSortPhases_GrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhases");

            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchProcessPhases_WineMaterialBatches_WineMaterialBatchId",
                table: "WineMaterialBatchProcessPhases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WineMaterialBatchProcessPhases",
                table: "WineMaterialBatchProcessPhases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WineMaterialBatchProcessPhaseParameters",
                table: "WineMaterialBatchProcessPhaseParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WineMaterialBatchProcessParameterValues",
                table: "WineMaterialBatchProcessParameterValues");

            migrationBuilder.RenameTable(
                name: "WineMaterialBatchProcessPhases",
                newName: "WineMaterialBatchGrapeSortPhases");

            migrationBuilder.RenameTable(
                name: "WineMaterialBatchProcessPhaseParameters",
                newName: "WineMaterialBatchGrapeSortPhaseParameters");

            migrationBuilder.RenameTable(
                name: "WineMaterialBatchProcessParameterValues",
                newName: "WineMaterialBatchGrapeSortPhaseParameterValues");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchProcessPhases_WineMaterialBatchId_GrapeSortPhaseId",
                table: "WineMaterialBatchGrapeSortPhases",
                newName: "IX_WineMaterialBatchGrapeSortPhases_WineMaterialBatchId_GrapeSortPhaseId");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchProcessPhases_GrapeSortPhaseId",
                table: "WineMaterialBatchGrapeSortPhases",
                newName: "IX_WineMaterialBatchGrapeSortPhases_GrapeSortPhaseId");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchProcessPhaseParameters_WineMaterialBatchGrapeSortPhaseId",
                table: "WineMaterialBatchGrapeSortPhaseParameters",
                newName: "IX_WineMaterialBatchGrapeSortPhaseParameters_WineMaterialBatchGrapeSortPhaseId");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchProcessPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchGrapeSortPhaseParameters",
                newName: "IX_WineMaterialBatchGrapeSortPhaseParameters_PhaseParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchProcessParameterValues_PhaseParameterId",
                table: "WineMaterialBatchGrapeSortPhaseParameterValues",
                newName: "IX_WineMaterialBatchGrapeSortPhaseParameterValues_PhaseParameterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WineMaterialBatchGrapeSortPhases",
                table: "WineMaterialBatchGrapeSortPhases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WineMaterialBatchGrapeSortPhaseParameters",
                table: "WineMaterialBatchGrapeSortPhaseParameters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WineMaterialBatchGrapeSortPhaseParameterValues",
                table: "WineMaterialBatchGrapeSortPhaseParameterValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhaseParameters_ProcessPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchGrapeSortPhaseParameters",
                column: "PhaseParameterId",
                principalTable: "ProcessPhaseParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhaseParameters_WineMaterialBatchGrapeSortPhases_WineMaterialBatchGrapeSortPhaseId",
                table: "WineMaterialBatchGrapeSortPhaseParameters",
                column: "WineMaterialBatchGrapeSortPhaseId",
                principalTable: "WineMaterialBatchGrapeSortPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhaseParameterValues_WineMaterialBatchGrapeSortPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchGrapeSortPhaseParameterValues",
                column: "PhaseParameterId",
                principalTable: "WineMaterialBatchGrapeSortPhaseParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhases_GrapeSortPhases_GrapeSortPhaseId",
                table: "WineMaterialBatchGrapeSortPhases",
                column: "GrapeSortPhaseId",
                principalTable: "GrapeSortPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhases_WineMaterialBatches_WineMaterialBatchId",
                table: "WineMaterialBatchGrapeSortPhases",
                column: "WineMaterialBatchId",
                principalTable: "WineMaterialBatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhaseParameters_ProcessPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchGrapeSortPhaseParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhaseParameters_WineMaterialBatchGrapeSortPhases_WineMaterialBatchGrapeSortPhaseId",
                table: "WineMaterialBatchGrapeSortPhaseParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhaseParameterValues_WineMaterialBatchGrapeSortPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchGrapeSortPhaseParameterValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhases_GrapeSortPhases_GrapeSortPhaseId",
                table: "WineMaterialBatchGrapeSortPhases");

            migrationBuilder.DropForeignKey(
                name: "FK_WineMaterialBatchGrapeSortPhases_WineMaterialBatches_WineMaterialBatchId",
                table: "WineMaterialBatchGrapeSortPhases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WineMaterialBatchGrapeSortPhases",
                table: "WineMaterialBatchGrapeSortPhases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WineMaterialBatchGrapeSortPhaseParameterValues",
                table: "WineMaterialBatchGrapeSortPhaseParameterValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WineMaterialBatchGrapeSortPhaseParameters",
                table: "WineMaterialBatchGrapeSortPhaseParameters");

            migrationBuilder.RenameTable(
                name: "WineMaterialBatchGrapeSortPhases",
                newName: "WineMaterialBatchProcessPhases");

            migrationBuilder.RenameTable(
                name: "WineMaterialBatchGrapeSortPhaseParameterValues",
                newName: "WineMaterialBatchProcessParameterValues");

            migrationBuilder.RenameTable(
                name: "WineMaterialBatchGrapeSortPhaseParameters",
                newName: "WineMaterialBatchProcessPhaseParameters");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchGrapeSortPhases_WineMaterialBatchId_GrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhases",
                newName: "IX_WineMaterialBatchProcessPhases_WineMaterialBatchId_GrapeSortPhaseId");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchGrapeSortPhases_GrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhases",
                newName: "IX_WineMaterialBatchProcessPhases_GrapeSortPhaseId");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchGrapeSortPhaseParameterValues_PhaseParameterId",
                table: "WineMaterialBatchProcessParameterValues",
                newName: "IX_WineMaterialBatchProcessParameterValues_PhaseParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchGrapeSortPhaseParameters_WineMaterialBatchGrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhaseParameters",
                newName: "IX_WineMaterialBatchProcessPhaseParameters_WineMaterialBatchGrapeSortPhaseId");

            migrationBuilder.RenameIndex(
                name: "IX_WineMaterialBatchGrapeSortPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchProcessPhaseParameters",
                newName: "IX_WineMaterialBatchProcessPhaseParameters_PhaseParameterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WineMaterialBatchProcessPhases",
                table: "WineMaterialBatchProcessPhases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WineMaterialBatchProcessParameterValues",
                table: "WineMaterialBatchProcessParameterValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WineMaterialBatchProcessPhaseParameters",
                table: "WineMaterialBatchProcessPhaseParameters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchProcessParameterValues_WineMaterialBatchProcessPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchProcessParameterValues",
                column: "PhaseParameterId",
                principalTable: "WineMaterialBatchProcessPhaseParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchProcessPhaseParameters_ProcessPhaseParameters_PhaseParameterId",
                table: "WineMaterialBatchProcessPhaseParameters",
                column: "PhaseParameterId",
                principalTable: "ProcessPhaseParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchProcessPhaseParameters_WineMaterialBatchProcessPhases_WineMaterialBatchGrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhaseParameters",
                column: "WineMaterialBatchGrapeSortPhaseId",
                principalTable: "WineMaterialBatchProcessPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchProcessPhases_GrapeSortPhases_GrapeSortPhaseId",
                table: "WineMaterialBatchProcessPhases",
                column: "GrapeSortPhaseId",
                principalTable: "GrapeSortPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WineMaterialBatchProcessPhases_WineMaterialBatches_WineMaterialBatchId",
                table: "WineMaterialBatchProcessPhases",
                column: "WineMaterialBatchId",
                principalTable: "WineMaterialBatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
