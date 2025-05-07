using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class risk20250414 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobitScenario_Scenario_ScenarioId",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.DropIndex(
                name: "IX_CobitScenario_ScenarioId",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.DropColumn(
                name: "ScenarioId",
                schema: "RiskManagement",
                table: "CobitScenario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ScenarioId",
                schema: "RiskManagement",
                table: "CobitScenario",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CobitScenario_ScenarioId",
                schema: "RiskManagement",
                table: "CobitScenario",
                column: "ScenarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CobitScenario_Scenario_ScenarioId",
                schema: "RiskManagement",
                table: "CobitScenario",
                column: "ScenarioId",
                principalSchema: "BCP",
                principalTable: "Scenario",
                principalColumn: "Id");
        }
    }
}
