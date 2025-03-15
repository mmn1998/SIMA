using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addRiskLevelIdinRiskCobit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RiskLevelId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevelCobit_RiskLevelId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                column: "RiskLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLevelCobit_RiskLevel_RiskLevelId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                column: "RiskLevelId",
                principalSchema: "RiskManagement",
                principalTable: "RiskLevel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskLevelCobit_RiskLevel_RiskLevelId",
                schema: "RiskManagement",
                table: "RiskLevelCobit");

            migrationBuilder.DropIndex(
                name: "IX_RiskLevelCobit_RiskLevelId",
                schema: "RiskManagement",
                table: "RiskLevelCobit");

            migrationBuilder.DropColumn(
                name: "RiskLevelId",
                schema: "RiskManagement",
                table: "RiskLevelCobit");
        }
    }
}
