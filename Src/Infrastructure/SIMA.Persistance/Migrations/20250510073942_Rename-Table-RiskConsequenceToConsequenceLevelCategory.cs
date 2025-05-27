using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableRiskConsequenceToConsequenceLevelCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskConsequence_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "RiskConsequence");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskConsequence_ConsequenceLevel_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "RiskConsequence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskConsequence",
                schema: "RiskManagement",
                table: "RiskConsequence");

            migrationBuilder.RenameTable(
                name: "RiskConsequence",
                schema: "RiskManagement",
                newName: "ConsequenceLevelCategory",
                newSchema: "RiskManagement");

            migrationBuilder.RenameIndex(
                name: "IX_RiskConsequence_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "ConsequenceLevelCategory",
                newName: "IX_ConsequenceLevelCategory_ConsequenceLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskConsequence_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevelCategory",
                newName: "IX_ConsequenceLevelCategory_ConsequenceCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsequenceLevelCategory",
                schema: "RiskManagement",
                table: "ConsequenceLevelCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsequenceLevelCategory_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevelCategory",
                column: "ConsequenceCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsequenceLevelCategory_ConsequenceLevel_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "ConsequenceLevelCategory",
                column: "ConsequenceLevelId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceLevel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsequenceLevelCategory_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevelCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsequenceLevelCategory_ConsequenceLevel_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "ConsequenceLevelCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsequenceLevelCategory",
                schema: "RiskManagement",
                table: "ConsequenceLevelCategory");

            migrationBuilder.RenameTable(
                name: "ConsequenceLevelCategory",
                schema: "RiskManagement",
                newName: "RiskConsequence",
                newSchema: "RiskManagement");

            migrationBuilder.RenameIndex(
                name: "IX_ConsequenceLevelCategory_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "RiskConsequence",
                newName: "IX_RiskConsequence_ConsequenceLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsequenceLevelCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "RiskConsequence",
                newName: "IX_RiskConsequence_ConsequenceCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskConsequence",
                schema: "RiskManagement",
                table: "RiskConsequence",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskConsequence_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "RiskConsequence",
                column: "ConsequenceCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskConsequence_ConsequenceLevel_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "RiskConsequence",
                column: "ConsequenceLevelId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceLevel",
                principalColumn: "Id");
        }
    }
}
