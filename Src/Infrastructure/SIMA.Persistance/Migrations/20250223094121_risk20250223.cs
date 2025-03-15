using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class risk20250223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risk_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Risk_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "ConsequenceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "ConsequenceCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceCategory",
                principalColumn: "Id");
        }
    }
}
