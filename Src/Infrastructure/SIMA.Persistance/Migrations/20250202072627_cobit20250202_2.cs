using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class cobit20250202_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskLevelCobit_InherentOccurrenceProbabilityValue_InherentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevelCobit");

            migrationBuilder.RenameColumn(
                name: "InherentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                newName: "CurrentOccurrenceProbabilityValueId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskLevelCobit_InherentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                newName: "IX_RiskLevelCobit_CurrentOccurrenceProbabilityValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLevelCobit_CurrentOccurrenceProbabilityValue_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                column: "CurrentOccurrenceProbabilityValueId",
                principalSchema: "RiskManagement",
                principalTable: "CurrentOccurrenceProbabilityValue",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskLevelCobit_CurrentOccurrenceProbabilityValue_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevelCobit");

            migrationBuilder.RenameColumn(
                name: "CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                newName: "InherentOccurrenceProbabilityValueId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskLevelCobit_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                newName: "IX_RiskLevelCobit_InherentOccurrenceProbabilityValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLevelCobit_InherentOccurrenceProbabilityValue_InherentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                column: "InherentOccurrenceProbabilityValueId",
                principalSchema: "RiskManagement",
                principalTable: "InherentOccurrenceProbabilityValue",
                principalColumn: "Id");
        }
    }
}
