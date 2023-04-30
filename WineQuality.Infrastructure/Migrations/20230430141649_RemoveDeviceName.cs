using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class RemoveDeviceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProcessPhaseParameterSensors_DeviceName",
                table: "ProcessPhaseParameterSensors");

            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "ProcessPhaseParameterSensors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "ProcessPhaseParameterSensors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPhaseParameterSensors_DeviceName",
                table: "ProcessPhaseParameterSensors",
                column: "DeviceName",
                unique: true);
        }
    }
}
