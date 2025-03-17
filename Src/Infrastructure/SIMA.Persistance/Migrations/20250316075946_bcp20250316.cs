using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class bcp20250316 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanAssumption_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanAssumption_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanResponsible_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRisk_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanService_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanService");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanStratgy_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityStrategy_StrategyType_StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_BackupPeriod_BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_ImportanceDegree_ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_ServicePriority_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_BiaValue_BiaValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_ConsequenceValue_ConsequenceValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_HappeningPossibility_HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_RecoveryPointObjective_RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_ScenarioBusinessContinuityPlanAssumption_Scenario_ScenarioId",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption");

            migrationBuilder.DropTable(
                name: " ScenarioBusinessContinuityPlanVersioning",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanVersioning",
                schema: "BCP");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_BiaValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_ConsequenceValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysis_BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysis_ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysis_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategy_StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlanAssumption_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption");

            migrationBuilder.DropColumn(
                name: "BiaValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropColumn(
                name: "ConsequenceValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropColumn(
                name: "HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropColumn(
                name: "MTD",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropColumn(
                name: "RPO",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropColumn(
                name: "RTO",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropColumn(
                name: "WRT",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropColumn(
                name: "BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "MTD",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "RestartReason",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropColumn(
                name: "IsStableStrategy",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropColumn(
                name: "StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropColumn(
                name: "BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption");

            migrationBuilder.RenameColumn(
                name: "ScenarioId",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption",
                newName: "BusinessContinuityPlanScenarioCobitScenarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ScenarioBusinessContinuityPlanAssumption_ScenarioId",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption",
                newName: "IX_ScenarioBusinessContinuityPlanAssumption_BusinessContinuityPlanScenarioCobitScenarioId");

            migrationBuilder.RenameColumn(
                name: "RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                newName: "ConsequenceIntensionId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                newName: "IX_BusinessImpactAnalysisDisasterOrigin_ConsequenceIntensionId");

            migrationBuilder.RenameColumn(
                name: "RPO",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                newName: "MTPD");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy",
                newName: "BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanStratgy_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy",
                newName: "IX_BusinessContinuityPlanStratgy_BusinessContinuityPlanId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                newName: "BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanService_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                newName: "IX_BusinessContinuityPlanService_BusinessContinuityPlanId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                newName: "BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanRisk_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                newName: "IX_BusinessContinuityPlanRisk_BusinessContinuityPlanId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                newName: "BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanResponsible_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                newName: "IX_BusinessContinuityPlanResponsible_BusinessContinuityPlanId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                newName: "BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                newName: "IX_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                newName: "BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                newName: "IX_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanId");

            migrationBuilder.AlterColumn<float>(
                name: "WRT",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "RTO",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TimeMeasurementId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VersionNumber",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnalysisValue",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConsequenceIntensionId = table.Column<long>(type: "bigint", nullable: false),
                    ConsequenceId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisValue_ConsequenceIntension_ConsequenceIntensionId",
                        column: x => x.ConsequenceIntensionId,
                        principalSchema: "BCP",
                        principalTable: "ConsequenceIntension",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnalysisValue_Consequence_ConsequenceId",
                        column: x => x.ConsequenceId,
                        principalSchema: "BCP",
                        principalTable: "Consequence",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "RecoveryPointObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_TimeMeasurementId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "TimeMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisValue_ConsequenceId",
                schema: "BCP",
                table: "AnalysisValue",
                column: "ConsequenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisValue_ConsequenceIntensionId",
                schema: "BCP",
                table: "AnalysisValue",
                column: "ConsequenceIntensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanAssumption_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanResponsible_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanRisk_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanService_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanStratgy_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_RecoveryPointObjective_RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "RecoveryPointObjectiveId",
                principalSchema: "BCP",
                principalTable: "RecoveryPointObjective",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_TimeMeasurement_TimeMeasurementId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "TimeMeasurementId",
                principalSchema: "Basic",
                principalTable: "TimeMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_ConsequenceIntension_ConsequenceIntensionId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "ConsequenceIntensionId",
                principalSchema: "BCP",
                principalTable: "ConsequenceIntension",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScenarioBusinessContinuityPlanAssumption_BusinessContinuityPlanScenarioCobitScenario_BusinessContinuityPlanScenarioCobitScen~",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption",
                column: "BusinessContinuityPlanScenarioCobitScenarioId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanScenarioCobitScenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanAssumption_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanResponsible_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRisk_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanService_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanService");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanStratgy_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_RecoveryPointObjective_RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_TimeMeasurement_TimeMeasurementId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_ConsequenceIntension_ConsequenceIntensionId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_ScenarioBusinessContinuityPlanAssumption_BusinessContinuityPlanScenarioCobitScenario_BusinessContinuityPlanScenarioCobitScen~",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption");

            migrationBuilder.DropTable(
                name: "AnalysisValue",
                schema: "BCP");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysis_RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysis_TimeMeasurementId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "TimeMeasurementId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropColumn(
                name: "VersionNumber",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanScenarioCobitScenarioId",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption",
                newName: "ScenarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ScenarioBusinessContinuityPlanAssumption_BusinessContinuityPlanScenarioCobitScenarioId",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption",
                newName: "IX_ScenarioBusinessContinuityPlanAssumption_ScenarioId");

            migrationBuilder.RenameColumn(
                name: "ConsequenceIntensionId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                newName: "RecoveryPointObjectiveId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_ConsequenceIntensionId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                newName: "IX_BusinessImpactAnalysisDisasterOrigin_RecoveryPointObjectiveId");

            migrationBuilder.RenameColumn(
                name: "MTPD",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                newName: "RPO");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy",
                newName: "BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanStratgy_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy",
                newName: "IX_BusinessContinuityPlanStratgy_BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                newName: "BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanService_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                newName: "IX_BusinessContinuityPlanService_BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                newName: "BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanRisk_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                newName: "IX_BusinessContinuityPlanRisk_BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                newName: "BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanResponsible_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                newName: "IX_BusinessContinuityPlanResponsible_BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                newName: "BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                newName: "IX_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameColumn(
                name: "BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                newName: "BusinessContinuityPlanVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                newName: "IX_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanVersioningId");

            migrationBuilder.AddColumn<long>(
                name: "BiaValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConsequenceValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MTD",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RPO",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RTO",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WRT",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "real",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "WRT",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "RTO",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<long>(
                name: "BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MTD",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RestartReason",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsStableStrategy",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewDate",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanVersioning",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VersionNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanVersioning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanVersioning_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: " ScenarioBusinessContinuityPlanVersioning",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    ScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ ScenarioBusinessContinuityPlanVersioning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ ScenarioBusinessContinuityPlanVersioning_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                        column: x => x.BusinessContinuityPlanVersioningId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlanVersioning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ ScenarioBusinessContinuityPlanVersioning_Scenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalSchema: "BCP",
                        principalTable: "Scenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_BiaValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "BiaValueId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_ConsequenceValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "ConsequenceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "HappeningPossibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "BackupPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ImportanceDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ServicePriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategy_StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "StrategyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanAssumption_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                column: "BusinessContinuityPlanVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_ ScenarioBusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: " ScenarioBusinessContinuityPlanVersioning",
                column: "BusinessContinuityPlanVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_ ScenarioBusinessContinuityPlanVersioning_ScenarioId",
                schema: "BCP",
                table: " ScenarioBusinessContinuityPlanVersioning",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanVersioning_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanVersioning",
                column: "BusinessContinuityPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanAssumption_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                column: "BusinessContinuityPlanVersioningId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanVersioning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanAssumption_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                column: "BusinessContinuityPlanVersioningId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanVersioning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                column: "BusinessContinuityPlanVersioningId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanVersioning",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanResponsible_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                column: "BusinessContinuityPlanVersioningId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanVersioning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanRisk_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                column: "BusinessContinuityPlanVersioningId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanVersioning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanService_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                column: "BusinessContinuityPlanVersioningId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanVersioning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanStratgy_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy",
                column: "BusinessContinuityPlanVersioningId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlanVersioning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityStrategy_StrategyType_StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "StrategyTypeId",
                principalSchema: "BCP",
                principalTable: "StrategyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_BackupPeriod_BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "BackupPeriodId",
                principalSchema: "BCP",
                principalTable: "BackupPeriod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_ImportanceDegree_ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ImportanceDegreeId",
                principalSchema: "BCP",
                principalTable: "ImportanceDegree",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_ServicePriority_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ServicePriorityId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServicePriority",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_BiaValue_BiaValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "BiaValueId",
                principalSchema: "BCP",
                principalTable: "BiaValue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_ConsequenceValue_ConsequenceValueId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "ConsequenceValueId",
                principalSchema: "BCP",
                principalTable: "ConsequenceValue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_HappeningPossibility_HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "HappeningPossibilityId",
                principalSchema: "BCP",
                principalTable: "HappeningPossibility",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_RecoveryPointObjective_RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "RecoveryPointObjectiveId",
                principalSchema: "BCP",
                principalTable: "RecoveryPointObjective",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScenarioBusinessContinuityPlanAssumption_Scenario_ScenarioId",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption",
                column: "ScenarioId",
                principalSchema: "BCP",
                principalTable: "Scenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
