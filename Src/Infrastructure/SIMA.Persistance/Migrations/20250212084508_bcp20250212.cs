using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class bcp20250212 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.AlterColumn<long>(
                name: "ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<float>(
                name: "MTD",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RPO",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RTO",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WRT",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SolutionPriorityId",
                schema: "BCP",
                table: "BusinessContinuityStratgySolution",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsStableStrategy",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlanTypeId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanScenarioCobitScenario",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    CobitScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanScenarioCobitScenario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanScenarioCobitScenario_CobitScenario_CobitScenarioId",
                        column: x => x.CobitScenarioId,
                        principalSchema: "RiskManagement",
                        principalTable: "CobitScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanScenarioCobitScenario_Scenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalSchema: "BCP",
                        principalTable: "Scenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsequenceIntension",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ValueNumber = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsequenceIntension", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsequenceValue",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConsequenceId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    ValueNumber = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsequenceValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsequenceValue_Consequence_ConsequenceId",
                        column: x => x.ConsequenceId,
                        principalSchema: "BCP",
                        principalTable: "Consequence",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConsequenceValue_Origin_OriginId",
                        column: x => x.OriginId,
                        principalSchema: "BCP",
                        principalTable: "Origin",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanType",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolutionPriority",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Priority = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionPriority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BiaValue",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConsequenceId = table.Column<long>(type: "bigint", nullable: false),
                    ConsequenceIntensionId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiaValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiaValue_ConsequenceIntension_ConsequenceIntensionId",
                        column: x => x.ConsequenceIntensionId,
                        principalSchema: "BCP",
                        principalTable: "ConsequenceIntension",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiaValue_Consequence_ConsequenceId",
                        column: x => x.ConsequenceId,
                        principalSchema: "BCP",
                        principalTable: "Consequence",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisStaff_BranchId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisStaff_DepartmentId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff",
                column: "DepartmentId");

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
                name: "IX_BusinessContinuityStratgySolution_SolutionPriorityId",
                schema: "BCP",
                table: "BusinessContinuityStratgySolution",
                column: "SolutionPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStratgyResponsible_BranchId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStratgyResponsible_DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanResponsible_BranchId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanResponsible_DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlan_PlanTypeId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "PlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BiaValue_ConsequenceId",
                schema: "BCP",
                table: "BiaValue",
                column: "ConsequenceId");

            migrationBuilder.CreateIndex(
                name: "IX_BiaValue_ConsequenceIntensionId",
                schema: "BCP",
                table: "BiaValue",
                column: "ConsequenceIntensionId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanScenarioCobitScenario_CobitScenarioId",
                schema: "BCP",
                table: "BusinessContinuityPlanScenarioCobitScenario",
                column: "CobitScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanScenarioCobitScenario_ScenarioId",
                schema: "BCP",
                table: "BusinessContinuityPlanScenarioCobitScenario",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsequenceIntension_Code",
                schema: "BCP",
                table: "ConsequenceIntension",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsequenceValue_ConsequenceId",
                schema: "BCP",
                table: "ConsequenceValue",
                column: "ConsequenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsequenceValue_OriginId",
                schema: "BCP",
                table: "ConsequenceValue",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanType_Code",
                schema: "BCP",
                table: "PlanType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolutionPriority_Code",
                schema: "BCP",
                table: "SolutionPriority",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_PlanType_PlanTypeId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "PlanTypeId",
                principalSchema: "BCP",
                principalTable: "PlanType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanResponsible_Branch_BranchId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanResponsible_Department_DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityStratgyResponsible_Branch_BranchId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityStratgyResponsible_Department_DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityStratgySolution_SolutionPriority_SolutionPriorityId",
                schema: "BCP",
                table: "BusinessContinuityStratgySolution",
                column: "SolutionPriorityId",
                principalSchema: "BCP",
                principalTable: "SolutionPriority",
                principalColumn: "Id");

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
                name: "FK_BusinessImpactAnalysisStaff_Branch_BranchId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisStaff_Department_DepartmentId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_PlanType_PlanTypeId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanResponsible_Branch_BranchId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanResponsible_Department_DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityStratgyResponsible_Branch_BranchId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityStratgyResponsible_Department_DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityStratgySolution_SolutionPriority_SolutionPriorityId",
                schema: "BCP",
                table: "BusinessContinuityStratgySolution");

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
                name: "FK_BusinessImpactAnalysisStaff_Branch_BranchId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisStaff_Department_DepartmentId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff");

            migrationBuilder.DropTable(
                name: "BiaValue",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanScenarioCobitScenario",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "ConsequenceValue",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "PlanType",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "SolutionPriority",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "ConsequenceIntension",
                schema: "BCP");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysisStaff_BranchId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysisStaff_DepartmentId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff");

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
                name: "IX_BusinessContinuityStratgySolution_SolutionPriorityId",
                schema: "BCP",
                table: "BusinessContinuityStratgySolution");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStratgyResponsible_BranchId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStratgyResponsible_DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlanResponsible_BranchId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlanResponsible_DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlan_PlanTypeId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff");

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
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "RPO",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "RTO",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "WRT",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "SolutionPriorityId",
                schema: "BCP",
                table: "BusinessContinuityStratgySolution");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible");

            migrationBuilder.DropColumn(
                name: "IsStableStrategy",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible");

            migrationBuilder.DropColumn(
                name: "PlanTypeId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_BackupPeriod_BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "BackupPeriodId",
                principalSchema: "BCP",
                principalTable: "BackupPeriod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_ImportanceDegree_ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ImportanceDegreeId",
                principalSchema: "BCP",
                principalTable: "ImportanceDegree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_ServicePriority_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ServicePriorityId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServicePriority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
