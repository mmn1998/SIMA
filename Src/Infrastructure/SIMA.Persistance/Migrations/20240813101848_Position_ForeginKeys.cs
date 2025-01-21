using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Position_ForeginKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_BusinessContinuityStrategy_BusinessContinuityStrategyId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_ExecutiveResponsibleId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_PlanOwnerId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_RecoveryDeputyId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_RecoveryManagerId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRisk_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanService_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanService");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanDetailPlanningAssumption",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanStaff",
                schema: "BCP");

            migrationBuilder.DropIndex(
                name: "IX_ServicePriority_Code",
                schema: "BCP",
                table: "ServicePriority");

            migrationBuilder.DropIndex(
                name: "IX_RecoveryPointObjective_Code",
                schema: "BCP",
                table: "RecoveryPointObjective");

            migrationBuilder.DropIndex(
                name: "IX_ImportanceDegree_Code",
                schema: "BCP",
                table: "ImportanceDegree");

            migrationBuilder.DropIndex(
                name: "IX_HappeningPossibility_Code",
                schema: "BCP",
                table: "HappeningPossibility");

            migrationBuilder.DropIndex(
                name: "IX_Consequence_Code",
                schema: "BCP",
                table: "Consequence");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysis_Code",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategyObjective_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlanPossibleAction_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlan_Code",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropIndex(
                name: "IX_BackupPeriod_Code",
                schema: "BCP",
                table: "BackupPeriod");

            migrationBuilder.DropColumn(
                name: "DataType",
                schema: "Basic",
                table: "ConfigurationAttribute");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "MTD",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "Name",
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
                name: "OfferDate",
                schema: "BCP",
                table: "BusinessContinuityPlan");

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
                table: "BusinessContinuityPlanCriticalActivity",
                newName: "CriticalActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                newName: "IX_BusinessContinuityPlanCriticalActivity_CriticalActivityId");

            migrationBuilder.RenameColumn(
                name: "RecoveryManagerId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "StaffId3");

            migrationBuilder.RenameColumn(
                name: "RecoveryDeputyId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "StaffId2");

            migrationBuilder.RenameColumn(
                name: "PlanOwnerId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "StaffId1");

            migrationBuilder.RenameColumn(
                name: "ExecutiveResponsibleId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlan_RecoveryManagerId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "IX_BusinessContinuityPlan_StaffId3");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlan_RecoveryDeputyId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "IX_BusinessContinuityPlan_StaffId2");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlan_PlanOwnerId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "IX_BusinessContinuityPlan_StaffId1");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlan_ExecutiveResponsibleId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "IX_BusinessContinuityPlan_StaffId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "ServicePriority",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "ServicePriority",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "RecoveryPointObjective",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "RecoveryPointObjective",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "Organization",
                table: "Position",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PositionLevelId",
                schema: "Organization",
                table: "Position",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PositionTypeId",
                schema: "Organization",
                table: "Position",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Authentication",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "ImportanceDegree",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "ImportanceDegree",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "HappeningPossibility",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "HappeningPossibility",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "Consequence",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "Consequence",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeKey",
                schema: "Basic",
                table: "ConfigurationAttributeValue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConfigurationTypeId",
                schema: "Basic",
                table: "ConfigurationAttribute",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AssetId",
                schema: "BCP",
                table: "BusinessImpactAnalysisAsset",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

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
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BusinessContinuityStrategyId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "BackupPeriod",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BackupPeriod",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanVersioning",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    VersionNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
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
                name: "BusinessContinuityStratgySolution",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    BusinessContinuityStratgyId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityStratgySolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStratgySolution_BusinessContinuityStrategy_BusinessContinuityStratgyId",
                        column: x => x.BusinessContinuityStratgyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationType",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsList = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomeFieldType",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsList = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    IsMultiSelect = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomeFieldType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormPermission",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FormId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormPermission_Form_FormId",
                        column: x => x.FormId,
                        principalSchema: "Authentication",
                        principalTable: "Form",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormPermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Authentication",
                        principalTable: "Permission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanResponsibility",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Ordering = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanResponsibility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionLevel",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsList = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionType",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsList = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecoveryOptionPriority",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Ordering = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecoveryOptionPriority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scenario",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StrategyType",
                schema: "BCP",
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
                    table.PrimaryKey("PK_StrategyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanAssumption",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanAssumption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanAssumption_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                        column: x => x.BusinessContinuityPlanVersioningId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlanVersioning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanAssumption_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanRelatedStaff",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanRelatedStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                        column: x => x.BusinessContinuityPlanVersioningId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlanVersioning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanRelatedStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanStratgy",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityStratgyId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanStratgy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanStratgy_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                        column: x => x.BusinessContinuityPlanVersioningId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlanVersioning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanStratgy_BusinessContinuityStrategy_BusinessContinuityStratgyId",
                        column: x => x.BusinessContinuityStratgyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MainAggregateId = table.Column<long>(type: "bigint", nullable: false),
                    CustomeFieldTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnglishKey = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsMandatory = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configuration_CustomeFieldType_CustomeFieldTypeId",
                        column: x => x.CustomeFieldTypeId,
                        principalSchema: "Basic",
                        principalTable: "CustomeFieldType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Configuration_MainAggregate_MainAggregateId",
                        column: x => x.MainAggregateId,
                        principalSchema: "Authentication",
                        principalTable: "MainAggregate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanResponsible",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    PlanResponsibilityId = table.Column<long>(type: "bigint", nullable: false),
                    IsForBackup = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanResponsible", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanResponsible_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                        column: x => x.BusinessContinuityPlanVersioningId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlanVersioning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanResponsible_PlanResponsibility_PlanResponsibilityId",
                        column: x => x.PlanResponsibilityId,
                        principalSchema: "BCP",
                        principalTable: "PlanResponsibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanResponsible_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityStratgyResponsible",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityStrategyId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    PlanResponsibilityId = table.Column<long>(type: "bigint", nullable: false),
                    IsForBackup = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityStratgyResponsible", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStratgyResponsible_BusinessContinuityStrategy_BusinessContinuityStrategyId",
                        column: x => x.BusinessContinuityStrategyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStratgyResponsible_PlanResponsibility_PlanResponsibilityId",
                        column: x => x.PlanResponsibilityId,
                        principalSchema: "BCP",
                        principalTable: "PlanResponsibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStratgyResponsible_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
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

            migrationBuilder.CreateTable(
                name: "ScenarioRecoveryCriteria",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioRecoveryCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScenarioRecoveryCriteria_Scenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalSchema: "BCP",
                        principalTable: "Scenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenarioRecoveryOption",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioRecoveryOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScenarioRecoveryOption_Scenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalSchema: "BCP",
                        principalTable: "Scenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenarioBusinessContinuityPlanAssumption",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanAssumptionId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioBusinessContinuityPlanAssumption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScenarioBusinessContinuityPlanAssumption_BusinessContinuityPlanAssumption_BusinessContinuityPlanAssumptionId",
                        column: x => x.BusinessContinuityPlanAssumptionId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlanAssumption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScenarioBusinessContinuityPlanAssumption_Scenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalSchema: "BCP",
                        principalTable: "Scenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicePriority_Code",
                schema: "BCP",
                table: "ServicePriority",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryPointObjective_Code",
                schema: "BCP",
                table: "RecoveryPointObjective",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Position_BranchId",
                schema: "Organization",
                table: "Position",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_PositionLevelId",
                schema: "Organization",
                table: "Position",
                column: "PositionLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_PositionTypeId",
                schema: "Organization",
                table: "Position",
                column: "PositionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportanceDegree_Code",
                schema: "BCP",
                table: "ImportanceDegree",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HappeningPossibility_Code",
                schema: "BCP",
                table: "HappeningPossibility",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consequence_Code",
                schema: "BCP",
                table: "Consequence",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationAttribute_ConfigurationTypeId",
                schema: "Basic",
                table: "ConfigurationAttribute",
                column: "ConfigurationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisAsset_AssetId",
                schema: "BCP",
                table: "BusinessImpactAnalysisAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyObjective_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategy_StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "StrategyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanPossibleAction_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                column: "BusinessContinuityPlanVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlan_Code",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BackupPeriod_Code",
                schema: "BCP",
                table: "BackupPeriod",
                column: "Code",
                unique: true);

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
                name: "IX_BusinessContinuityPlanAssumption_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanAssumption_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                column: "BusinessContinuityPlanVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanAssumption_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanAssumption",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanRelatedStaff_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                column: "BusinessContinuityPlanVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanRelatedStaff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanRelatedStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanResponsible_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                column: "BusinessContinuityPlanVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanResponsible_PlanResponsibilityId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                column: "PlanResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanResponsible_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanResponsible",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanStratgy_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy",
                column: "BusinessContinuityPlanVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanStratgy_BusinessContinuityStratgyId",
                schema: "BCP",
                table: "BusinessContinuityPlanStratgy",
                column: "BusinessContinuityStratgyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanVersioning_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanVersioning",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStratgyResponsible_BusinessContinuityStrategyId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible",
                column: "BusinessContinuityStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStratgyResponsible_PlanResponsibilityId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible",
                column: "PlanResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStratgyResponsible_StaffId",
                schema: "BCP",
                table: "BusinessContinuityStratgyResponsible",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStratgySolution_BusinessContinuityStratgyId",
                schema: "BCP",
                table: "BusinessContinuityStratgySolution",
                column: "BusinessContinuityStratgyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStratgySolution_Code",
                schema: "BCP",
                table: "BusinessContinuityStratgySolution",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_CustomeFieldTypeId",
                schema: "Basic",
                table: "Configuration",
                column: "CustomeFieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_MainAggregateId",
                schema: "Basic",
                table: "Configuration",
                column: "MainAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationType_Code",
                schema: "Basic",
                table: "ConfigurationType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomeFieldType_Code",
                schema: "Basic",
                table: "CustomeFieldType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FormPermission_FormId",
                schema: "Authentication",
                table: "FormPermission",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormPermission_PermissionId",
                schema: "Authentication",
                table: "FormPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanResponsibility_Code",
                schema: "BCP",
                table: "PlanResponsibility",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PositionLevel_Code",
                schema: "Organization",
                table: "PositionLevel",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PositionType_Code",
                schema: "Organization",
                table: "PositionType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryOptionPriority_Code",
                schema: "BCP",
                table: "RecoveryOptionPriority",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scenario_Code",
                schema: "BCP",
                table: "Scenario",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioBusinessContinuityPlanAssumption_BusinessContinuityPlanAssumptionId",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption",
                column: "BusinessContinuityPlanAssumptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioBusinessContinuityPlanAssumption_ScenarioId",
                schema: "BCP",
                table: "ScenarioBusinessContinuityPlanAssumption",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioRecoveryCriteria_Code",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioRecoveryCriteria_ScenarioId",
                schema: "BCP",
                table: "ScenarioRecoveryCriteria",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioRecoveryOption_Code",
                schema: "BCP",
                table: "ScenarioRecoveryOption",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioRecoveryOption_ScenarioId",
                schema: "BCP",
                table: "ScenarioRecoveryOption",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_StrategyType_Code",
                schema: "BCP",
                table: "StrategyType",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_BusinessContinuityStrategy_BusinessContinuityStrategyId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "BusinessContinuityStrategyId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityStrategy",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "StaffId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_StaffId1",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "StaffId1",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_StaffId2",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "StaffId2",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_StaffId3",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "StaffId3",
                principalSchema: "Organization",
                principalTable: "Staff",
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
                name: "FK_BusinessContinuityPlanCriticalActivity_CriticalActivity_CriticalActivityId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                column: "CriticalActivityId",
                principalSchema: "ServiceCatalog",
                principalTable: "CriticalActivity",
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
                name: "FK_BusinessContinuityStrategy_StrategyType_StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "StrategyTypeId",
                principalSchema: "BCP",
                principalTable: "StrategyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisAsset_Asset_AssetId",
                schema: "BCP",
                table: "BusinessImpactAnalysisAsset",
                column: "AssetId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "Asset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationAttribute_ConfigurationType_ConfigurationTypeId",
                schema: "Basic",
                table: "ConfigurationAttribute",
                column: "ConfigurationTypeId",
                principalSchema: "Basic",
                principalTable: "ConfigurationType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Branch_BranchId",
                schema: "Organization",
                table: "Position",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Position_PositionLevel_PositionLevelId",
                schema: "Organization",
                table: "Position",
                column: "PositionLevelId",
                principalSchema: "Organization",
                principalTable: "PositionLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Position_PositionType_PositionTypeId",
                schema: "Organization",
                table: "Position",
                column: "PositionTypeId",
                principalSchema: "Organization",
                principalTable: "PositionType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_BusinessContinuityStrategy_BusinessContinuityStrategyId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_StaffId1",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_StaffId2",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_StaffId3",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanCriticalActivity_CriticalActivity_CriticalActivityId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRisk_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanService_BusinessContinuityPlanVersioning_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanService");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityStrategy_StrategyType_StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisAsset_Asset_AssetId",
                schema: "BCP",
                table: "BusinessImpactAnalysisAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationAttribute_ConfigurationType_ConfigurationTypeId",
                schema: "Basic",
                table: "ConfigurationAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Position_Branch_BranchId",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropForeignKey(
                name: "FK_Position_PositionLevel_PositionLevelId",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropForeignKey(
                name: "FK_Position_PositionType_PositionTypeId",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropTable(
                name: " ScenarioBusinessContinuityPlanVersioning",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanRelatedStaff",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanResponsible",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanStratgy",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityStratgyResponsible",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityStratgySolution",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "Configuration",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "ConfigurationType",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "FormPermission",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "PositionLevel",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "PositionType",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "RecoveryOptionPriority",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "ScenarioBusinessContinuityPlanAssumption",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "ScenarioRecoveryCriteria",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "ScenarioRecoveryOption",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "StrategyType",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "PlanResponsibility",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "CustomeFieldType",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanAssumption",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "Scenario",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanVersioning",
                schema: "BCP");

            migrationBuilder.DropIndex(
                name: "IX_ServicePriority_Code",
                schema: "BCP",
                table: "ServicePriority");

            migrationBuilder.DropIndex(
                name: "IX_RecoveryPointObjective_Code",
                schema: "BCP",
                table: "RecoveryPointObjective");

            migrationBuilder.DropIndex(
                name: "IX_Position_BranchId",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Position_PositionLevelId",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Position_PositionTypeId",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "IX_ImportanceDegree_Code",
                schema: "BCP",
                table: "ImportanceDegree");

            migrationBuilder.DropIndex(
                name: "IX_HappeningPossibility_Code",
                schema: "BCP",
                table: "HappeningPossibility");

            migrationBuilder.DropIndex(
                name: "IX_Consequence_Code",
                schema: "BCP",
                table: "Consequence");

            migrationBuilder.DropIndex(
                name: "IX_ConfigurationAttribute_ConfigurationTypeId",
                schema: "Basic",
                table: "ConfigurationAttribute");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysisAsset_AssetId",
                schema: "BCP",
                table: "BusinessImpactAnalysisAsset");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategyObjective_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategy_StrategyTypeId",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlanPossibleAction_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanVersioningId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlan_Code",
                schema: "BCP",
                table: "BusinessContinuityPlan");

            migrationBuilder.DropIndex(
                name: "IX_BackupPeriod_Code",
                schema: "BCP",
                table: "BackupPeriod");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "PositionLevelId",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "PositionTypeId",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Authentication",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "AttributeKey",
                schema: "Basic",
                table: "ConfigurationAttributeValue");

            migrationBuilder.DropColumn(
                name: "ConfigurationTypeId",
                schema: "Basic",
                table: "ConfigurationAttribute");

            migrationBuilder.DropColumn(
                name: "AssetId",
                schema: "BCP",
                table: "BusinessImpactAnalysisAsset");

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
                table: "BusinessContinuityPlanCriticalActivity");

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
                name: "CriticalActivityId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                newName: "BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanCriticalActivity_CriticalActivityId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                newName: "IX_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanId");

            migrationBuilder.RenameColumn(
                name: "StaffId3",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "RecoveryManagerId");

            migrationBuilder.RenameColumn(
                name: "StaffId2",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "RecoveryDeputyId");

            migrationBuilder.RenameColumn(
                name: "StaffId1",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "PlanOwnerId");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "ExecutiveResponsibleId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlan_StaffId3",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "IX_BusinessContinuityPlan_RecoveryManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlan_StaffId2",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "IX_BusinessContinuityPlan_RecoveryDeputyId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlan_StaffId1",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "IX_BusinessContinuityPlan_PlanOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlan_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                newName: "IX_BusinessContinuityPlan_ExecutiveResponsibleId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "ServicePriority",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "ServicePriority",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "RecoveryPointObjective",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "RecoveryPointObjective",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "ImportanceDegree",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "ImportanceDegree",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "HappeningPossibility",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "HappeningPossibility",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "Consequence",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "Consequence",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "DataType",
                schema: "Basic",
                table: "ConfigurationAttribute",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MTD",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "nvarchar(200)",
                maxLength: 200,
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

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);

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
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<long>(
                name: "BusinessContinuityStrategyId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OfferDate",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BCP",
                table: "BackupPeriod",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "BCP",
                table: "BackupPeriod",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanDetailPlanningAssumption",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanDetailPlanningAssumption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanDetailPlanningAssumption_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanStaff",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanStaff_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicePriority_Code",
                schema: "BCP",
                table: "ServicePriority",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryPointObjective_Code",
                schema: "BCP",
                table: "RecoveryPointObjective",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ImportanceDegree_Code",
                schema: "BCP",
                table: "ImportanceDegree",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HappeningPossibility_Code",
                schema: "BCP",
                table: "HappeningPossibility",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Consequence_Code",
                schema: "BCP",
                table: "Consequence",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_Code",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyObjective_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanPossibleAction_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlan_Code",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BackupPeriod_Code",
                schema: "BCP",
                table: "BackupPeriod",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanDetailPlanningAssumption_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanDetailPlanningAssumption",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanDetailPlanningAssumption_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanDetailPlanningAssumption",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanStaff_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanStaff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_BusinessContinuityStrategy_BusinessContinuityStrategyId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "BusinessContinuityStrategyId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityStrategy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_ExecutiveResponsibleId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "ExecutiveResponsibleId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_PlanOwnerId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "PlanOwnerId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_RecoveryDeputyId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "RecoveryDeputyId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlan_Staff_RecoveryManagerId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "RecoveryManagerId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

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
        }
    }
}
