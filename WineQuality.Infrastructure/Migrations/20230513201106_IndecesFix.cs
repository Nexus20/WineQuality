using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class IndecesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GrapeSortPhases_Order",
                table: "GrapeSortPhases");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhases_Order_PhaseId_GrapeSortId",
                table: "GrapeSortPhases",
                columns: new[] { "Order", "PhaseId", "GrapeSortId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GrapeSortPhases_Order_PhaseId_GrapeSortId",
                table: "GrapeSortPhases");

            migrationBuilder.CreateIndex(
                name: "IX_GrapeSortPhases_Order",
                table: "GrapeSortPhases",
                column: "Order",
                unique: true);
        }
    }
}
