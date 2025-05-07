using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class risk20250126 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsequenceLevel_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevel");

            migrationBuilder.DropIndex(
                name: "IX_ConsequenceLevel_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevel");

            migrationBuilder.DropColumn(
                name: "ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevel");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceLevel");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceCategory",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceCategory");

            migrationBuilder.AddColumn<long>(
                name: "ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceLevel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsequenceLevel_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevel",
                column: "ConsequenceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsequenceLevel_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevel",
                column: "ConsequenceCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceCategory",
                principalColumn: "Id");
        }
    }
}
