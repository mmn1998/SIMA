using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class risk20250127 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Severity_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Severity");

            migrationBuilder.RenameColumn(
                name: "ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Severity",
                newName: "ConsequenceLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Severity_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Severity",
                newName: "IX_Severity_ConsequenceLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Severity_ConsequenceLevel_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Severity",
                column: "ConsequenceLevelId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceLevel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Severity_ConsequenceLevel_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Severity");

            migrationBuilder.RenameColumn(
                name: "ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Severity",
                newName: "ConsequenceCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Severity_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Severity",
                newName: "IX_Severity_ConsequenceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Severity_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Severity",
                column: "ConsequenceCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceCategory",
                principalColumn: "Id");
        }
    }
}
