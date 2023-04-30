using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class SensorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessPhaseParameterSensors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhaseParameterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WineMaterialBatchGrapeSortPhaseParameterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeviceKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessPhaseParameterSensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessPhaseParameterSensors_ProcessPhaseParameters_PhaseParameterId",
                        column: x => x.PhaseParameterId,
                        principalTable: "ProcessPhaseParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessPhaseParameterSensors_WineMaterialBatchGrapeSortPhaseParameters_WineMaterialBatchGrapeSortPhaseParameterId",
                        column: x => x.WineMaterialBatchGrapeSortPhaseParameterId,
                        principalTable: "WineMaterialBatchGrapeSortPhaseParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPhaseParameterSensors_DeviceKey",
                table: "ProcessPhaseParameterSensors",
                column: "DeviceKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPhaseParameterSensors_DeviceName",
                table: "ProcessPhaseParameterSensors",
                column: "DeviceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPhaseParameterSensors_PhaseParameterId",
                table: "ProcessPhaseParameterSensors",
                column: "PhaseParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPhaseParameterSensors_WineMaterialBatchGrapeSortPhaseParameterId",
                table: "ProcessPhaseParameterSensors",
                column: "WineMaterialBatchGrapeSortPhaseParameterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessPhaseParameterSensors");
        }
    }
}
