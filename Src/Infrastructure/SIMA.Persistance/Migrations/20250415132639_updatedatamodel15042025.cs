using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updatedatamodel15042025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobitRiskCategoryScenario_RiskCategory_RiskCategoryId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario");

            migrationBuilder.RenameColumn(
                name: "RiskCategoryId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario",
                newName: "CobitCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CobitRiskCategoryScenario_RiskCategoryId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario",
                newName: "IX_CobitRiskCategoryScenario_CobitCategoryId");

            migrationBuilder.AddColumn<string>(
                name: "CobitIdentifier",
                schema: "RiskManagement",
                table: "CobitScenario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CobitRiskCategoryScenario_CobitRiskCategory_CobitCategoryId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario",
                column: "CobitCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "CobitRiskCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobitRiskCategoryScenario_CobitRiskCategory_CobitCategoryId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario");

            migrationBuilder.DropColumn(
                name: "CobitIdentifier",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.RenameColumn(
                name: "CobitCategoryId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario",
                newName: "RiskCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CobitRiskCategoryScenario_CobitCategoryId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario",
                newName: "IX_CobitRiskCategoryScenario_RiskCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CobitRiskCategoryScenario_RiskCategory_RiskCategoryId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario",
                column: "RiskCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "RiskCategory",
                principalColumn: "Id");
        }
    }
}
