using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class bcp_3_8_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_Staff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff");

            migrationBuilder.DropIndex(
                name: "IX_ScenarioRecoveryOption_Code",
                schema: "BCP",
                table: "ScenarioRecoveryOption");

            migrationBuilder.DropIndex(
                name: "IX_ScenarioRecoveryCriteria_Code",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "BCP",
                table: "ScenarioRecoveryOption");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "BCP",
                table: "ScenarioRecoveryOption");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria");

            migrationBuilder.DropColumn(
                name: "CordinatorId",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "BCP",
                table: "ScenarioRecoveryOption",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecoveryOptionPriorityId",
                schema: "BCP",
                table: "ScenarioRecoveryOption",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TimeMeasurementId",
                schema: "BCP",
                table: "RecoveryPointObjective",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OriginId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "IsStableStrategy",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanIssue",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanIssue_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanIssue_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityStrategyIssue",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityStrategyId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityStrategyIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStrategyIssue_BusinessContinuityStrategy_BusinessContinuityStrategyId",
                        column: x => x.BusinessContinuityStrategyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStrategyIssue_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessImpactAnalysisIssue",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessImpactAnalysisId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImpactAnalysisIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisIssue_BusinessImpactAnalysis_BusinessImpactAnalysisId",
                        column: x => x.BusinessImpactAnalysisId,
                        principalSchema: "BCP",
                        principalTable: "BusinessImpactAnalysis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisIssue_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Origin",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScenarioExecutionHistory",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionNumber = table.Column<long>(type: "bigint", nullable: false),
                    ExecutionTimeFrom = table.Column<TimeOnly>(type: "time", nullable: true),
                    ExecutionTimeTo = table.Column<TimeOnly>(type: "time", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioExecutionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScenarioExecutionHistory_Scenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalSchema: "BCP",
                        principalTable: "Scenario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScenarioPossibleAction",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioPossibleAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScenarioPossibleAction_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeMeasurement",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UnitBasement = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeMeasurement", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioRecoveryOption_RecoveryOptionPriorityId",
                schema: "BCP",
                table: "ScenarioRecoveryOption",
                column: "RecoveryOptionPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryPointObjective_TimeMeasurementId",
                schema: "BCP",
                table: "RecoveryPointObjective",
                column: "TimeMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_OriginId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanIssue_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanIssue",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanIssue_IssueId",
                schema: "BCP",
                table: "BusinessContinuityPlanIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyIssue_BusinessContinuityStrategyId",
                schema: "BCP",
                table: "BusinessContinuityStrategyIssue",
                column: "BusinessContinuityStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyIssue_IssueId",
                schema: "BCP",
                table: "BusinessContinuityStrategyIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisIssue_BusinessImpactAnalysisId",
                schema: "BCP",
                table: "BusinessImpactAnalysisIssue",
                column: "BusinessImpactAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisIssue_IssueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Origin_Code",
                schema: "BCP",
                table: "Origin",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioExecutionHistory_ScenarioId",
                schema: "BCP",
                table: "ScenarioExecutionHistory",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioPossibleAction_BusinessContinuityPlanId",
                schema: "BCP",
                table: "ScenarioPossibleAction",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeMeasurement_Code",
                schema: "Basic",
                table: "TimeMeasurement",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                column: "BusinessContinuityPlanVersioningId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanVersioning",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_Staff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                column: "StaffId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_Origin_OriginId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "OriginId",
                principalSchema: "BCP",
                principalTable: "Origin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecoveryPointObjective_TimeMeasurement_TimeMeasurementId",
                schema: "BCP",
                table: "RecoveryPointObjective",
                column: "TimeMeasurementId",
                principalSchema: "Basic",
                principalTable: "TimeMeasurement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScenarioRecoveryOption_RecoveryOptionPriority_RecoveryOptionPriorityId",
                schema: "BCP",
                table: "ScenarioRecoveryOption",
                column: "RecoveryOptionPriorityId",
                principalSchema: "BCP",
                principalTable: "RecoveryOptionPriority",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_Staff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_Origin_OriginId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_RecoveryPointObjective_TimeMeasurement_TimeMeasurementId",
                schema: "BCP",
                table: "RecoveryPointObjective");

            migrationBuilder.DropForeignKey(
                name: "FK_ScenarioRecoveryOption_RecoveryOptionPriority_RecoveryOptionPriorityId",
                schema: "BCP",
                table: "ScenarioRecoveryOption");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanIssue",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityStrategyIssue",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessImpactAnalysisIssue",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "Origin",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "ScenarioExecutionHistory",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "ScenarioPossibleAction",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "TimeMeasurement",
                schema: "Basic");

            migrationBuilder.DropIndex(
                name: "IX_ScenarioRecoveryOption_RecoveryOptionPriorityId",
                schema: "BCP",
                table: "ScenarioRecoveryOption");

            migrationBuilder.DropIndex(
                name: "IX_RecoveryPointObjective_TimeMeasurementId",
                schema: "BCP",
                table: "RecoveryPointObjective");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_OriginId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "BCP",
                table: "ScenarioRecoveryOption");

            migrationBuilder.DropColumn(
                name: "RecoveryOptionPriorityId",
                schema: "BCP",
                table: "ScenarioRecoveryOption");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria");

            migrationBuilder.DropColumn(
                name: "TimeMeasurementId",
                schema: "BCP",
                table: "RecoveryPointObjective");

            migrationBuilder.DropColumn(
                name: "OriginId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "ScenarioRecoveryOption",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "ScenarioRecoveryOption",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsStableStrategy",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CordinatorId",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioRecoveryOption_Code",
                schema: "BCP",
                table: "ScenarioRecoveryOption",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioRecoveryCriteria_Code",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                column: "BusinessContinuityPlanVersioningId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanVersioning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_Staff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                column: "StaffId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
