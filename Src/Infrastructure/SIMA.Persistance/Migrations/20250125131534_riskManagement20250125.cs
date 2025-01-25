using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class riskManagement20250125 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AffectedHistoryId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FrequencyId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ScenarioHistoryId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TriggerStatusId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UseVulnerabilityId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AffectedHistory",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    ValueTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffectedHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CobitScenarioCategory",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobitScenarioCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsequenceCategory",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsequenceCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentOccurrenceProbabilityValue",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ValuingIntervalTitle = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentOccurrenceProbabilityValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frequency",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    ValueTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InherentOccurrenceProbabilityValue",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InherentOccurrenceProbabilityValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatrixAValue",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    ValueTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrixAValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskValue",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskValueStrategy",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RiskId = table.Column<long>(type: "bigint", nullable: false),
                    StrategyId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskValueStrategy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskValueStrategy_BusinessContinuityPlanStratgy_StrategyId",
                        column: x => x.StrategyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlanStratgy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskValueStrategy_Risk_RiskId",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScenarioHistory",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    ValueTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeverityValue",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    ValuingIntervalTitle = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeverityValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TriggerStatus",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    ValueTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UseVulnerability",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    ValueTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseVulnerability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CobitScenario",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CobitScenarioCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobitScenario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobitScenario_CobitScenarioCategory_CobitScenarioCategoryId",
                        column: x => x.CobitScenarioCategoryId,
                        principalSchema: "RiskManagement",
                        principalTable: "CobitScenarioCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CobitScenario_Scenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalSchema: "BCP",
                        principalTable: "Scenario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConsequenceLevel",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumericValue = table.Column<float>(type: "real", nullable: false),
                    ValueTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsequenceCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsequenceLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsequenceLevel_ConsequenceCategory_ConsequenceCategoryId",
                        column: x => x.ConsequenceCategoryId,
                        principalSchema: "RiskManagement",
                        principalTable: "ConsequenceCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CurrentOccurrenceProbability",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MatrixAValueId = table.Column<long>(type: "bigint", nullable: false),
                    ScenarioHistoryId = table.Column<long>(type: "bigint", nullable: false),
                    InherentOccurrenceProbabilityValueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentOccurrenceProbability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentOccurrenceProbability_InherentOccurrenceProbabilityValue_InherentOccurrenceProbabilityValueId",
                        column: x => x.InherentOccurrenceProbabilityValueId,
                        principalSchema: "RiskManagement",
                        principalTable: "InherentOccurrenceProbabilityValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CurrentOccurrenceProbability_MatrixAValue_MatrixAValueId",
                        column: x => x.MatrixAValueId,
                        principalSchema: "RiskManagement",
                        principalTable: "MatrixAValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CurrentOccurrenceProbability_ScenarioHistory_ScenarioHistoryId",
                        column: x => x.ScenarioHistoryId,
                        principalSchema: "RiskManagement",
                        principalTable: "ScenarioHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InherentOccurrenceProbability",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MatrixAValueId = table.Column<long>(type: "bigint", nullable: false),
                    ScenarioHistoryId = table.Column<long>(type: "bigint", nullable: false),
                    InherentOccurrenceProbabilityValueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InherentOccurrenceProbability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InherentOccurrenceProbability_InherentOccurrenceProbabilityValue_InherentOccurrenceProbabilityValueId",
                        column: x => x.InherentOccurrenceProbabilityValueId,
                        principalSchema: "RiskManagement",
                        principalTable: "InherentOccurrenceProbabilityValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InherentOccurrenceProbability_MatrixAValue_MatrixAValueId",
                        column: x => x.MatrixAValueId,
                        principalSchema: "RiskManagement",
                        principalTable: "MatrixAValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InherentOccurrenceProbability_ScenarioHistory_ScenarioHistoryId",
                        column: x => x.ScenarioHistoryId,
                        principalSchema: "RiskManagement",
                        principalTable: "ScenarioHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Severity",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ConsequenceCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    AffectedHistoryId = table.Column<long>(type: "bigint", nullable: false),
                    SeverityValueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Severity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Severity_AffectedHistory_AffectedHistoryId",
                        column: x => x.AffectedHistoryId,
                        principalSchema: "RiskManagement",
                        principalTable: "AffectedHistory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Severity_ConsequenceCategory_ConsequenceCategoryId",
                        column: x => x.ConsequenceCategoryId,
                        principalSchema: "RiskManagement",
                        principalTable: "ConsequenceCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Severity_SeverityValue_SeverityValueId",
                        column: x => x.SeverityValueId,
                        principalSchema: "RiskManagement",
                        principalTable: "SeverityValue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatrixA",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TriggerStatusId = table.Column<long>(type: "bigint", nullable: false),
                    UseVulnerabilityId = table.Column<long>(type: "bigint", nullable: false),
                    MatrixAValueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrixA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatrixA_MatrixAValue_MatrixAValueId",
                        column: x => x.MatrixAValueId,
                        principalSchema: "RiskManagement",
                        principalTable: "MatrixAValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatrixA_TriggerStatus_TriggerStatusId",
                        column: x => x.TriggerStatusId,
                        principalSchema: "RiskManagement",
                        principalTable: "TriggerStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatrixA_UseVulnerability_UseVulnerabilityId",
                        column: x => x.UseVulnerabilityId,
                        principalSchema: "RiskManagement",
                        principalTable: "UseVulnerability",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskConsequence",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConsequenceCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ConsequenceLevelId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskConsequence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskConsequence_ConsequenceCategory_ConsequenceCategoryId",
                        column: x => x.ConsequenceCategoryId,
                        principalSchema: "RiskManagement",
                        principalTable: "ConsequenceCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskConsequence_ConsequenceLevel_ConsequenceLevelId",
                        column: x => x.ConsequenceLevelId,
                        principalSchema: "RiskManagement",
                        principalTable: "ConsequenceLevel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Risk_AffectedHistoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "AffectedHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "ConsequenceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_FrequencyId",
                schema: "RiskManagement",
                table: "Risk",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_ScenarioHistoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "ScenarioHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_TriggerStatusId",
                schema: "RiskManagement",
                table: "Risk",
                column: "TriggerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_UseVulnerabilityId",
                schema: "RiskManagement",
                table: "Risk",
                column: "UseVulnerabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AffectedHistory_Code",
                schema: "RiskManagement",
                table: "AffectedHistory",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CobitScenario_CobitScenarioCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario",
                column: "CobitScenarioCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CobitScenario_ScenarioId",
                schema: "RiskManagement",
                table: "CobitScenario",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CobitScenarioCategory_Code",
                schema: "RiskManagement",
                table: "CobitScenarioCategory",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsequenceCategory_Code",
                schema: "RiskManagement",
                table: "ConsequenceCategory",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsequenceLevel_Code",
                schema: "RiskManagement",
                table: "ConsequenceLevel",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsequenceLevel_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "ConsequenceLevel",
                column: "ConsequenceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentOccurrenceProbability_Code",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentOccurrenceProbability_InherentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                column: "InherentOccurrenceProbabilityValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentOccurrenceProbability_MatrixAValueId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                column: "MatrixAValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentOccurrenceProbability_ScenarioHistoryId",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbability",
                column: "ScenarioHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentOccurrenceProbabilityValue_Code",
                schema: "RiskManagement",
                table: "CurrentOccurrenceProbabilityValue",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Frequency_Code",
                schema: "RiskManagement",
                table: "Frequency",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InherentOccurrenceProbability_Code",
                schema: "RiskManagement",
                table: "InherentOccurrenceProbability",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InherentOccurrenceProbability_InherentOccurrenceProbabilityValueId",
                schema: "RiskManagement",
                table: "InherentOccurrenceProbability",
                column: "InherentOccurrenceProbabilityValueId");

            migrationBuilder.CreateIndex(
                name: "IX_InherentOccurrenceProbability_MatrixAValueId",
                schema: "RiskManagement",
                table: "InherentOccurrenceProbability",
                column: "MatrixAValueId");

            migrationBuilder.CreateIndex(
                name: "IX_InherentOccurrenceProbability_ScenarioHistoryId",
                schema: "RiskManagement",
                table: "InherentOccurrenceProbability",
                column: "ScenarioHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InherentOccurrenceProbabilityValue_Code",
                schema: "RiskManagement",
                table: "InherentOccurrenceProbabilityValue",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatrixA_Code",
                schema: "RiskManagement",
                table: "MatrixA",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatrixA_MatrixAValueId",
                schema: "RiskManagement",
                table: "MatrixA",
                column: "MatrixAValueId");

            migrationBuilder.CreateIndex(
                name: "IX_MatrixA_TriggerStatusId",
                schema: "RiskManagement",
                table: "MatrixA",
                column: "TriggerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MatrixA_UseVulnerabilityId",
                schema: "RiskManagement",
                table: "MatrixA",
                column: "UseVulnerabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MatrixAValue_Code",
                schema: "RiskManagement",
                table: "MatrixAValue",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskConsequence_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "RiskConsequence",
                column: "ConsequenceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskConsequence_ConsequenceLevelId",
                schema: "RiskManagement",
                table: "RiskConsequence",
                column: "ConsequenceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskValue_Code",
                schema: "RiskManagement",
                table: "RiskValue",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskValueStrategy_RiskId",
                schema: "RiskManagement",
                table: "RiskValueStrategy",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskValueStrategy_StrategyId",
                schema: "RiskManagement",
                table: "RiskValueStrategy",
                column: "StrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioHistory_Code",
                schema: "RiskManagement",
                table: "ScenarioHistory",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Severity_AffectedHistoryId",
                schema: "RiskManagement",
                table: "Severity",
                column: "AffectedHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Severity_Code",
                schema: "RiskManagement",
                table: "Severity",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Severity_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Severity",
                column: "ConsequenceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Severity_SeverityValueId",
                schema: "RiskManagement",
                table: "Severity",
                column: "SeverityValueId");

            migrationBuilder.CreateIndex(
                name: "IX_SeverityValue_Code",
                schema: "RiskManagement",
                table: "SeverityValue",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TriggerStatus_Code",
                schema: "RiskManagement",
                table: "TriggerStatus",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UseVulnerability_Code",
                schema: "RiskManagement",
                table: "UseVulnerability",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_AffectedHistory_AffectedHistoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "AffectedHistoryId",
                principalSchema: "RiskManagement",
                principalTable: "AffectedHistory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "ConsequenceCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "ConsequenceCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_Frequency_FrequencyId",
                schema: "RiskManagement",
                table: "Risk",
                column: "FrequencyId",
                principalSchema: "RiskManagement",
                principalTable: "Frequency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_ScenarioHistory_ScenarioHistoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "ScenarioHistoryId",
                principalSchema: "RiskManagement",
                principalTable: "ScenarioHistory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_TriggerStatus_TriggerStatusId",
                schema: "RiskManagement",
                table: "Risk",
                column: "TriggerStatusId",
                principalSchema: "RiskManagement",
                principalTable: "TriggerStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_UseVulnerability_UseVulnerabilityId",
                schema: "RiskManagement",
                table: "Risk",
                column: "UseVulnerabilityId",
                principalSchema: "RiskManagement",
                principalTable: "UseVulnerability",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risk_AffectedHistory_AffectedHistoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_ConsequenceCategory_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_Frequency_FrequencyId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_ScenarioHistory_ScenarioHistoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_TriggerStatus_TriggerStatusId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_UseVulnerability_UseVulnerabilityId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropTable(
                name: "CobitScenario",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "CurrentOccurrenceProbability",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "CurrentOccurrenceProbabilityValue",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "Frequency",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "InherentOccurrenceProbability",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "MatrixA",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "RiskConsequence",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "RiskValue",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "RiskValueStrategy",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "Severity",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "CobitScenarioCategory",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "InherentOccurrenceProbabilityValue",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "ScenarioHistory",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "MatrixAValue",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "TriggerStatus",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "UseVulnerability",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "ConsequenceLevel",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "AffectedHistory",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "SeverityValue",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "ConsequenceCategory",
                schema: "RiskManagement");

            migrationBuilder.DropIndex(
                name: "IX_Risk_AffectedHistoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_FrequencyId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_ScenarioHistoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_TriggerStatusId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_UseVulnerabilityId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "AffectedHistoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "ConsequenceCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "FrequencyId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "ScenarioHistoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "TriggerStatusId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "UseVulnerabilityId",
                schema: "RiskManagement",
                table: "Risk");
        }
    }
}
