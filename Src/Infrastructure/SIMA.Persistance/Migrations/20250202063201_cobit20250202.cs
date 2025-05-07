using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class cobit20250202 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentOccurrenceProbability_MatrixAValue_MatrixAValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentOccurrenceProbability_ScenarioHistory_ScenarioHistoryId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceCategory");

            migrationBuilder.RenameColumn(
                name: "ScenarioHistoryId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                newName: "FrequencyId");

            migrationBuilder.RenameColumn(
                name: "MatrixAValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                newName: "CurrentOccurrenceProbabilityValueId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentOccurrenceProbability_ScenarioHistoryId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                newName: "IX_CurrentOccurrenceProbability_FrequencyId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentOccurrenceProbability_MatrixAValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                newName: "IX_CurrentOccurrenceProbability_CurrentOccurrenceProbabilityValueId");

            migrationBuilder.AddColumn<string>(
                name: "IsNeedCobit",
                schema: "RiskManagement",
                table: "Risk",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceLevel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CobitIdentifier",
                schema: "RiskManagement",
                table: "CobitScenario",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "RiskManagement",
                table: "CobitScenario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "RiskManagement",
                table: "CobitScenario",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RiskLevelCobit",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    SeverityId = table.Column<long>(type: "bigint", nullable: false),
                    InherentOccurrenceProbabilityValueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskLevelCobit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskLevelCobit_InherentOccurrenceProbabilityValue_InherentOccurrenceProbabilityValueId",
                        column: x => x.InherentOccurrenceProbabilityValueId,
                        principalSchema: "RiskManagement",
                        principalTable: "InherentOccurrenceProbabilityValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskLevelCobit_Severity_SeverityId",
                        column: x => x.SeverityId,
                        principalSchema: "RiskManagement",
                        principalTable: "Severity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevelCobit_Code",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevelCobit_InherentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                column: "InherentOccurrenceProbabilityValueId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevelCobit_SeverityId",
                schema: "RiskManagement",
                table: "RiskLevelCobit",
                column: "SeverityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentOccurrenceProbability_CurrentOccurrenceProbabilityValue_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                column: "CurrentOccurrenceProbabilityValueId",
                principalSchema: "RiskManagement",
                principalTable: "CurrentOccurrenceProbabilityValue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentOccurrenceProbability_Frequency_FrequencyId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                column: "FrequencyId",
                principalSchema: "RiskManagement",
                principalTable: "Frequency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentOccurrenceProbability_CurrentOccurrenceProbabilityValue_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentOccurrenceProbability_Frequency_FrequencyId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability");

            migrationBuilder.DropTable(
                name: "RiskLevelCobit",
                schema: "RiskManagement");

            migrationBuilder.DropColumn(
                name: "IsNeedCobit",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceLevel");

            migrationBuilder.DropColumn(
                name: "CobitIdentifier",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.RenameColumn(
                name: "FrequencyId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                newName: "ScenarioHistoryId");

            migrationBuilder.RenameColumn(
                name: "CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                newName: "MatrixAValueId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentOccurrenceProbability_FrequencyId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                newName: "IX_CurrentOccurrenceProbability_ScenarioHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentOccurrenceProbability_CurrentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                newName: "IX_CurrentOccurrenceProbability_MatrixAValueId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentOccurrenceProbability_MatrixAValue_MatrixAValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                column: "MatrixAValueId",
                principalSchema: "RiskManagement",
                principalTable: "MatrixAValue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentOccurrenceProbability_ScenarioHistory_ScenarioHistoryId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                column: "ScenarioHistoryId",
                principalSchema: "RiskManagement",
                principalTable: "ScenarioHistory",
                principalColumn: "Id");
        }
    }
}
