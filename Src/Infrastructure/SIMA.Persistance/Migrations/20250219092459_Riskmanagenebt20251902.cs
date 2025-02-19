using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Riskmanagenebt20251902 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ResponsibleTypeId",
                schema: "RiskManagement",
                table: "RiskStaff",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ResponsilbeTypeId",
                schema: "RiskManagement",
                table: "RiskStaff",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConsequenceLevelId1",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskStaff_ResponsibleTypeId",
                schema: "RiskManagement",
                table: "RiskStaff",
                column: "ResponsibleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskStaff_ResponsilbeTypeId",
                schema: "RiskManagement",
                table: "RiskStaff",
                column: "ResponsilbeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Risk",
                column: "ConsequenceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_ConsequenceLevelId1",
                schema: "RiskManagement",
                table: "Risk",
                column: "ConsequenceLevelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_ConsequenceLevel_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Risk",
                column: "ConsequenceLevelId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_ConsequenceLevel_ConsequenceLevelId1",
                schema: "RiskManagement",
                table: "Risk",
                column: "ConsequenceLevelId1",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskStaff_ResponsibleType_ResponsibleTypeId",
                schema: "RiskManagement",
                table: "RiskStaff",
                column: "ResponsibleTypeId",
                principalSchema: "Basic",
                principalTable: "ResponsibleType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskStaff_ResponsibleType_ResponsilbeTypeId",
                schema: "RiskManagement",
                table: "RiskStaff",
                column: "ResponsilbeTypeId",
                principalSchema: "Basic",
                principalTable: "ResponsibleType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risk_ConsequenceLevel_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_ConsequenceLevel_ConsequenceLevelId1",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskStaff_ResponsibleType_ResponsibleTypeId",
                schema: "RiskManagement",
                table: "RiskStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskStaff_ResponsibleType_ResponsilbeTypeId",
                schema: "RiskManagement",
                table: "RiskStaff");

            migrationBuilder.DropIndex(
                name: "IX_RiskStaff_ResponsibleTypeId",
                schema: "RiskManagement",
                table: "RiskStaff");

            migrationBuilder.DropIndex(
                name: "IX_RiskStaff_ResponsilbeTypeId",
                schema: "RiskManagement",
                table: "RiskStaff");

            migrationBuilder.DropIndex(
                name: "IX_Risk_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_ConsequenceLevelId1",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "ResponsibleTypeId",
                schema: "RiskManagement",
                table: "RiskStaff");

            migrationBuilder.DropColumn(
                name: "ResponsilbeTypeId",
                schema: "RiskManagement",
                table: "RiskStaff");

            migrationBuilder.DropColumn(
                name: "ConsequenceLevelId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "ConsequenceLevelId1",
                schema: "RiskManagement",
                table: "Risk");
        }
    }
}
