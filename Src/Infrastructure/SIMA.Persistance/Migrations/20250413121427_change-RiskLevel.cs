using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class changeRiskLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.AddColumn<long>(
                name: "CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RiskValueId",
                schema: "RiskManagement",
                table: "RiskLevel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SeverityValueId",
                schema: "RiskManagement",
                table: "RiskLevel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevel_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevel",
                column: "CurrentOccurrenceProbabilityValueId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevel_RiskValueId",
                schema: "RiskManagement",
                table: "RiskLevel",
                column: "RiskValueId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevel_SeverityValueId",
                schema: "RiskManagement",
                table: "RiskLevel",
                column: "SeverityValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLevel_CurrentOccurrenceProbabilityValue_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevel",
                column: "CurrentOccurrenceProbabilityValueId",
                principalSchema: "RiskManagement",
                principalTable: "CurrentOccurrenceProbabilityValue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLevel_RiskValue_RiskValueId",
                schema: "RiskManagement",
                table: "RiskLevel",
                column: "RiskValueId",
                principalSchema: "RiskManagement",
                principalTable: "RiskValue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLevel_SeverityValue_SeverityValueId",
                schema: "RiskManagement",
                table: "RiskLevel",
                column: "SeverityValueId",
                principalSchema: "RiskManagement",
                principalTable: "SeverityValue",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskLevel_CurrentOccurrenceProbabilityValue_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskLevel_RiskValue_RiskValueId",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskLevel_SeverityValue_SeverityValueId",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.DropIndex(
                name: "IX_RiskLevel_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.DropIndex(
                name: "IX_RiskLevel_RiskValueId",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.DropIndex(
                name: "IX_RiskLevel_SeverityValueId",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.DropColumn(
                name: "CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.DropColumn(
                name: "RiskValueId",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.DropColumn(
                name: "SeverityValueId",
                schema: "RiskManagement",
                table: "RiskLevel");

            migrationBuilder.AddColumn<float>(
                name: "Level",
                schema: "RiskManagement",
                table: "RiskLevel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "RiskManagement",
                table: "RiskLevel",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
