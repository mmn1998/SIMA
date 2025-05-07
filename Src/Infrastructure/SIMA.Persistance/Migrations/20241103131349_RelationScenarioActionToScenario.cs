using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RelationScenarioActionToScenario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScenarioPossibleAction_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "ScenarioPossibleAction");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanId",
                schema: "BCP",
                table: "ScenarioPossibleAction",
                newName: "ScenarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ScenarioPossibleAction_BusinessContinuityPlanId",
                schema: "BCP",
                table: "ScenarioPossibleAction",
                newName: "IX_ScenarioPossibleAction_ScenarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScenarioPossibleAction_Scenario_ScenarioId",
                schema: "BCP",
                table: "ScenarioPossibleAction",
                column: "ScenarioId",
                principalSchema: "BCP",
                principalTable: "Scenario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScenarioPossibleAction_Scenario_ScenarioId",
                schema: "BCP",
                table: "ScenarioPossibleAction");

            migrationBuilder.RenameColumn(
                name: "ScenarioId",
                schema: "BCP",
                table: "ScenarioPossibleAction",
                newName: "BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ScenarioPossibleAction_ScenarioId",
                schema: "BCP",
                table: "ScenarioPossibleAction",
                newName: "IX_ScenarioPossibleAction_BusinessContinuityPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScenarioPossibleAction_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "ScenarioPossibleAction",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id");
        }
    }
}
