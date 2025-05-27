using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class removeStrategyIdinRiskValueEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskValue_BusinessContinuityStrategy_StrategyId",
                schema: "RiskManagement",
                table: "RiskValue");

            migrationBuilder.RenameColumn(
                name: "StrategyId",
                schema: "RiskManagement",
                table: "RiskValue",
                newName: "BusinessContinuityStrategyId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskValue_StrategyId",
                schema: "RiskManagement",
                table: "RiskValue",
                newName: "IX_RiskValue_BusinessContinuityStrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskValue_BusinessContinuityStrategy_BusinessContinuityStrategyId",
                schema: "RiskManagement",
                table: "RiskValue",
                column: "BusinessContinuityStrategyId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityStrategy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskValue_BusinessContinuityStrategy_BusinessContinuityStrategyId",
                schema: "RiskManagement",
                table: "RiskValue");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityStrategyId",
                schema: "RiskManagement",
                table: "RiskValue",
                newName: "StrategyId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskValue_BusinessContinuityStrategyId",
                schema: "RiskManagement",
                table: "RiskValue",
                newName: "IX_RiskValue_StrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskValue_BusinessContinuityStrategy_StrategyId",
                schema: "RiskManagement",
                table: "RiskValue",
                column: "StrategyId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityStrategy",
                principalColumn: "Id");
        }
    }
}
