using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    public partial class QualityPredictionHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QualityPredictions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PredictionExplanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualityPredictions_FileReferences_FileReferenceId",
                        column: x => x.FileReferenceId,
                        principalTable: "FileReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QualityPredictions_FileReferenceId",
                table: "QualityPredictions",
                column: "FileReferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QualityPredictions");
        }
    }
}
