using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class Rename2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrapeSortProcessPhaseParameterStandard_GrapeSortPhases_GrapeSortPhaseId",
                table: "GrapeSortProcessPhaseParameterStandard");

            migrationBuilder.DropForeignKey(
                name: "FK_GrapeSortProcessPhaseParameterStandard_ProcessPhaseParameters_PhaseParameterId",
                table: "GrapeSortProcessPhaseParameterStandard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrapeSortProcessPhaseParameterStandard",
                table: "GrapeSortProcessPhaseParameterStandard");

            migrationBuilder.RenameTable(
                name: "GrapeSortProcessPhaseParameterStandard",
                newName: "GrapeSortProcessPhaseParameterStandards");

            migrationBuilder.RenameIndex(
                name: "IX_GrapeSortProcessPhaseParameterStandard_PhaseParameterId",
                table: "GrapeSortProcessPhaseParameterStandards",
                newName: "IX_GrapeSortProcessPhaseParameterStandards_PhaseParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_GrapeSortProcessPhaseParameterStandard_GrapeSortPhaseId",
                table: "GrapeSortProcessPhaseParameterStandards",
                newName: "IX_GrapeSortProcessPhaseParameterStandards_GrapeSortPhaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrapeSortProcessPhaseParameterStandards",
                table: "GrapeSortProcessPhaseParameterStandards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GrapeSortProcessPhaseParameterStandards_GrapeSortPhases_GrapeSortPhaseId",
                table: "GrapeSortProcessPhaseParameterStandards",
                column: "GrapeSortPhaseId",
                principalTable: "GrapeSortPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrapeSortProcessPhaseParameterStandards_ProcessPhaseParameters_PhaseParameterId",
                table: "GrapeSortProcessPhaseParameterStandards",
                column: "PhaseParameterId",
                principalTable: "ProcessPhaseParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrapeSortProcessPhaseParameterStandards_GrapeSortPhases_GrapeSortPhaseId",
                table: "GrapeSortProcessPhaseParameterStandards");

            migrationBuilder.DropForeignKey(
                name: "FK_GrapeSortProcessPhaseParameterStandards_ProcessPhaseParameters_PhaseParameterId",
                table: "GrapeSortProcessPhaseParameterStandards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrapeSortProcessPhaseParameterStandards",
                table: "GrapeSortProcessPhaseParameterStandards");

            migrationBuilder.RenameTable(
                name: "GrapeSortProcessPhaseParameterStandards",
                newName: "GrapeSortProcessPhaseParameterStandard");

            migrationBuilder.RenameIndex(
                name: "IX_GrapeSortProcessPhaseParameterStandards_PhaseParameterId",
                table: "GrapeSortProcessPhaseParameterStandard",
                newName: "IX_GrapeSortProcessPhaseParameterStandard_PhaseParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_GrapeSortProcessPhaseParameterStandards_GrapeSortPhaseId",
                table: "GrapeSortProcessPhaseParameterStandard",
                newName: "IX_GrapeSortProcessPhaseParameterStandard_GrapeSortPhaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrapeSortProcessPhaseParameterStandard",
                table: "GrapeSortProcessPhaseParameterStandard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GrapeSortProcessPhaseParameterStandard_GrapeSortPhases_GrapeSortPhaseId",
                table: "GrapeSortProcessPhaseParameterStandard",
                column: "GrapeSortPhaseId",
                principalTable: "GrapeSortPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrapeSortProcessPhaseParameterStandard_ProcessPhaseParameters_PhaseParameterId",
                table: "GrapeSortProcessPhaseParameterStandard",
                column: "PhaseParameterId",
                principalTable: "ProcessPhaseParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
