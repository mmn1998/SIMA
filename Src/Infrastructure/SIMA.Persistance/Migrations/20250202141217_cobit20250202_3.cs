using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class cobit20250202_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StrategyId",
                schema: "RiskManagement",
                table: "RiskValue",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RiskValue_StrategyId",
                schema: "RiskManagement",
                table: "RiskValue",
                column: "StrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskValue_BusinessContinuityStrategy_StrategyId",
                schema: "RiskManagement",
                table: "RiskValue",
                column: "StrategyId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityStrategy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskValue_BusinessContinuityStrategy_StrategyId",
                schema: "RiskManagement",
                table: "RiskValue");

            migrationBuilder.DropIndex(
                name: "IX_RiskValue_StrategyId",
                schema: "RiskManagement",
                table: "RiskValue");

            migrationBuilder.DropColumn(
                name: "StrategyId",
                schema: "RiskManagement",
                table: "RiskValue");
        }
    }
}
