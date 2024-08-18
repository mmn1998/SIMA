using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ServiceCatalogChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Api_ApiAuthentoicationMethod_ApiAuthentoicationMethodId",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_ApiRequestBodyParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestHeaderParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestQueryStringParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestUrlParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResponseBodyParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResponseHeaderParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam");

           
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServicePriority_ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Staff_BusinessResponsibleId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Staff_TechnicalResponsibleId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Staff_TechnicalSupervisorId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Staff_TechnicalSupportId",
                schema: "ServiceCatalog",
                table: "Service");

          

            migrationBuilder.DropIndex(
                name: "IX_Service_BusinessResponsibleId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_TechnicalResponsibleId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_TechnicalSupervisorId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_TechnicalSupportId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_ApiType_Code",
                schema: "ServiceCatalog",
                table: "ApiType");

            migrationBuilder.DropIndex(
                name: "IX_ApiAuthentoicationMethod_Code",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod");

            migrationBuilder.DropIndex(
                name: "IX_Api_Code",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.DropColumn(
                name: "BusinessResponsibleId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TechnicalResponsibleId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TechnicalSupervisorId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TechnicalSupportId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "IsMandatory",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam");

            migrationBuilder.DropColumn(
                name: "IsMandatory",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam");

            migrationBuilder.RenameColumn(
                name: "OwnerDepartmentId",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "StaffId3");

            migrationBuilder.RenameIndex(
                name: "IX_Service_OwnerDepartmentId",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "IX_Service_StaffId3");

            migrationBuilder.RenameColumn(
                name: "TechnicalSupervisorId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                newName: "TechnicalSupervisorDepartmentId");

            migrationBuilder.RenameColumn(
                name: "TechnicalResponsibleId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                newName: "StaffId1");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                newName: "StaffId");

            

            

            

            migrationBuilder.RenameColumn(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                newName: "ApiVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiRequestHeaderParam_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                newName: "IX_ApiRequestHeaderParam_ApiVersionId");

            migrationBuilder.RenameColumn(
                name: "ApiAuthentoicationMethodId",
                schema: "ServiceCatalog",
                table: "Api",
                newName: "ApiMethodActionId");

            migrationBuilder.RenameIndex(
                name: "IX_Api_ApiAuthentoicationMethodId",
                schema: "ServiceCatalog",
                table: "Api",
                newName: "IX_Api_ApiMethodActionId");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceType",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ModifiedBy",
                schema: "ServiceCatalog",
                table: "ServiceStatus",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceRelatedIssue",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceRelatedIssue",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceRelatedIssue",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ModifiedBy",
                schema: "ServiceCatalog",
                table: "ServiceProvider",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

           

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceProvider",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceDocument",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceCantract",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceCantract",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceCantract",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceBoundle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceBoundle",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceBoundle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AssetId",
                schema: "ServiceCatalog",
                table: "ServiceAsset",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceBoundleId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "Service",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "InServiceDate",
                schema: "ServiceCatalog",
                table: "Service",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsCriticalService",
                schema: "ServiceCatalog",
                table: "Service",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ServiceStatusId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StaffId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StaffId1",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StaffId2",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "PreRequisiteServices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "PreRequisiteServices",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "PreRequisiteServices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityService",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityService",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityService",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RiskId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityExecutionPlan",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityExecutionPlan",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityExecutionPlan",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AssetId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VersionNumber",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsCurrentVersion",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldUnicode: false,
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiType",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ServiceCatalog",
                table: "ApiType",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsMandatory",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldUnicode: false,
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsMandatory",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldUnicode: false,
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "Api",
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
                schema: "ServiceCatalog",
                table: "Api",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ApiAuthenticationMethodId",
                schema: "ServiceCatalog",
                table: "Api",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Channel",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceStatusId = table.Column<long>(type: "bigint", nullable: true),
                    InServiceDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channel_ServiceStatus_ServiceStatusId",
                        column: x => x.ServiceStatusId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceStatusId = table.Column<long>(type: "bigint", nullable: true),
                    ProviderCompanyId = table.Column<long>(type: "bigint", nullable: true),
                    InServiceDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Company_ProviderCompanyId",
                        column: x => x.ProviderCompanyId,
                        principalSchema: "Organization",
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ServiceStatus_ServiceStatusId",
                        column: x => x.ServiceStatusId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResponsibleType",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsibleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAssignedStaff",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAssignedStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAssignedStaff_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceAssignedStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceConfigurationItem",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceConfigurationItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceConfigurationItem_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceConfigurationItem_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceRisk",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    RiskId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRisk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRisk_Risk_RiskId",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceRisk_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChannelAccessPoint",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ChannelId = table.Column<long>(type: "bigint", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Port = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ActivationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelAccessPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelAccessPoint_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelUserType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ChannelId = table.Column<long>(type: "bigint", nullable: false),
                    UserTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelUserType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelUserType_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelUserType_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalSchema: "Authentication",
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductChannel",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ChannelId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductChannel_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductChannel_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelResponsible",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ChannelId = table.Column<long>(type: "bigint", nullable: false),
                    ResponsibleTypeId = table.Column<long>(type: "bigint", nullable: true),
                    ResponsibleId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelResponsible", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelResponsible_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelResponsible_ResponsibleType_ResponsibleTypeId",
                        column: x => x.ResponsibleTypeId,
                        principalSchema: "Basic",
                        principalTable: "ResponsibleType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChannelResponsible_Staff_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductResponsible",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ResponsibleTypeId = table.Column<long>(type: "bigint", nullable: true),
                    ResponsilbeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductResponsible", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductResponsible_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductResponsible_ResponsibleType_ResponsibleTypeId",
                        column: x => x.ResponsibleTypeId,
                        principalSchema: "Basic",
                        principalTable: "ResponsibleType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductResponsible_Staff_ResponsilbeId",
                        column: x => x.ResponsilbeId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAsset_AssetId",
                schema: "ServiceCatalog",
                table: "ServiceAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceStatusId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServiceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_StaffId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_StaffId1",
                schema: "ServiceCatalog",
                table: "Service",
                column: "StaffId1");

            migrationBuilder.CreateIndex(
                name: "IX_Service_StaffId2",
                schema: "ServiceCatalog",
                table: "Service",
                column: "StaffId2");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityRisk_RiskId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityAsset_AssetId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiType_Code",
                schema: "ServiceCatalog",
                table: "ApiType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiResponseHeaderParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                column: "ApiVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResponseBodyParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                column: "ApiVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestUrlParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                column: "ApiVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestQueryStringParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                column: "ApiVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestBodyParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ApiVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiAuthentoicationMethod_Code",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Api_ApiAuthenticationMethodId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "ApiAuthenticationMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Api_Code",
                schema: "ServiceCatalog",
                table: "Api",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Channel_ServiceStatusId",
                schema: "ServiceCatalog",
                table: "Channel",
                column: "ServiceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelAccessPoint_ChannelId",
                schema: "ServiceCatalog",
                table: "ChannelAccessPoint",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelResponsible_ChannelId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelResponsible_ResponsibleId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelResponsible_ResponsibleTypeId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible",
                column: "ResponsibleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelUserType_ChannelId",
                schema: "ServiceCatalog",
                table: "ChannelUserType",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelUserType_UserTypeId",
                schema: "ServiceCatalog",
                table: "ChannelUserType",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProviderCompanyId",
                schema: "ServiceCatalog",
                table: "Product",
                column: "ProviderCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ServiceStatusId",
                schema: "ServiceCatalog",
                table: "Product",
                column: "ServiceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductChannel_ChannelId",
                schema: "ServiceCatalog",
                table: "ProductChannel",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductChannel_ProductId",
                schema: "ServiceCatalog",
                table: "ProductChannel",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductResponsible_ProductId",
                schema: "ServiceCatalog",
                table: "ProductResponsible",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductResponsible_ResponsibleTypeId",
                schema: "ServiceCatalog",
                table: "ProductResponsible",
                column: "ResponsibleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductResponsible_ResponsilbeId",
                schema: "ServiceCatalog",
                table: "ProductResponsible",
                column: "ResponsilbeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsibleType_Code",
                schema: "Basic",
                table: "ResponsibleType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAssignedStaff_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAssignedStaff_StaffId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceConfigurationItem_ConfigurationItemId",
                schema: "ServiceCatalog",
                table: "ServiceConfigurationItem",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceConfigurationItem_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceConfigurationItem",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRisk_RiskId",
                schema: "ServiceCatalog",
                table: "ServiceRisk",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRisk_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceRisk",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Api_ApiAuthentoicationMethod_ApiAuthenticationMethodId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "ApiAuthenticationMethodId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiAuthentoicationMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Api_ApiMethodAction_ApiMethodActionId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "ApiMethodActionId",
                principalSchema: "Basic",
                principalTable: "ApiMethodAction",
                principalColumn: "Id");


            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestBodyParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestHeaderParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestQueryStringParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestQueryStringParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestUrlParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestUrlParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResponseBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResponseBodyParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResponseHeaderParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResponseHeaderParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");


            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivity_Staff_StaffId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "StaffId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivity_Staff_StaffId1",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "StaffId1",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivityAsset_Asset_AssetId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                column: "AssetId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "Asset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivityRisk_Risk_RiskId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                column: "RiskId",
                principalSchema: "RiskManagement",
                principalTable: "Risk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceStatus_ServiceStatusId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServiceStatusId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServiceStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Staff_StaffId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "StaffId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Staff_StaffId1",
                schema: "ServiceCatalog",
                table: "Service",
                column: "StaffId1",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Staff_StaffId2",
                schema: "ServiceCatalog",
                table: "Service",
                column: "StaffId2",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Staff_StaffId3",
                schema: "ServiceCatalog",
                table: "Service",
                column: "StaffId3",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAsset_Asset_AssetId",
                schema: "ServiceCatalog",
                table: "ServiceAsset",
                column: "AssetId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "Asset",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Api_ApiAuthentoicationMethod_ApiAuthenticationMethodId",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.DropForeignKey(
                name: "FK_Api_ApiMethodAction_ApiMethodActionId",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_ApiRequestBodyParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestHeaderParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestQueryStringParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestQueryStringParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestUrlParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestUrlParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResponseBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResponseBodyParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResponseHeaderParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResponseHeaderParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam");

           

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivity_Staff_StaffId",
                schema: "ServiceCatalog",
                table: "CriticalActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivity_Staff_StaffId1",
                schema: "ServiceCatalog",
                table: "CriticalActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivityAsset_Asset_AssetId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivityRisk_Risk_RiskId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceStatus_ServiceStatusId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Staff_StaffId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Staff_StaffId1",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Staff_StaffId2",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Staff_StaffId3",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAsset_Asset_AssetId",
                schema: "ServiceCatalog",
                table: "ServiceAsset");

            migrationBuilder.DropTable(
                name: "ChannelAccessPoint",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ChannelResponsible",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ChannelUserType",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ProductChannel",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ProductResponsible",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceAssignedStaff",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceConfigurationItem",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceRisk",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "Channel",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ResponsibleType",
                schema: "Basic");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAsset_AssetId",
                schema: "ServiceCatalog",
                table: "ServiceAsset");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServiceStatusId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_StaffId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_StaffId1",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_StaffId2",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_CriticalActivityRisk_RiskId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk");

            migrationBuilder.DropIndex(
                name: "IX_CriticalActivityAsset_AssetId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset");

            migrationBuilder.DropIndex(
                name: "IX_ApiType_Code",
                schema: "ServiceCatalog",
                table: "ApiType");

            migrationBuilder.DropIndex(
                name: "IX_ApiResponseHeaderParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam");

            migrationBuilder.DropIndex(
                name: "IX_ApiResponseBodyParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam");

            migrationBuilder.DropIndex(
                name: "IX_ApiRequestUrlParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam");

            migrationBuilder.DropIndex(
                name: "IX_ApiRequestQueryStringParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam");

            migrationBuilder.DropIndex(
                name: "IX_ApiRequestBodyParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.DropIndex(
                name: "IX_ApiAuthentoicationMethod_Code",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod");

            migrationBuilder.DropIndex(
                name: "IX_Api_ApiAuthenticationMethodId",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.DropIndex(
                name: "IX_Api_Code",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.DropColumn(
                name: "AssetId",
                schema: "ServiceCatalog",
                table: "ServiceAsset");

            migrationBuilder.DropColumn(
                name: "InServiceDate",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "IsCriticalService",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ServiceStatusId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "StaffId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "StaffId1",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "StaffId2",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "RiskId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk");

            migrationBuilder.DropColumn(
                name: "AssetId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset");

            migrationBuilder.DropColumn(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam");

            migrationBuilder.DropColumn(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam");

            migrationBuilder.DropColumn(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam");

            migrationBuilder.DropColumn(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam");

            migrationBuilder.DropColumn(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.DropColumn(
                name: "ApiAuthenticationMethodId",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.RenameColumn(
                name: "StaffId3",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "OwnerDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_StaffId3",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "IX_Service_OwnerDepartmentId");

            migrationBuilder.RenameColumn(
                name: "TechnicalSupervisorDepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                newName: "TechnicalSupervisorId");

            migrationBuilder.RenameColumn(
                name: "StaffId1",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                newName: "TechnicalResponsibleId");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_CriticalActivity_TechnicalSupervisorDepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                newName: "IX_CriticalActivity_TechnicalSupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_CriticalActivity_StaffId1",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                newName: "IX_CriticalActivity_TechnicalResponsibleId");

            migrationBuilder.RenameIndex(
                name: "IX_CriticalActivity_StaffId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                newName: "IX_CriticalActivity_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                newName: "ApiId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiRequestHeaderParam_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                newName: "IX_ApiRequestHeaderParam_ApiId");

            migrationBuilder.RenameColumn(
                name: "ApiMethodActionId",
                schema: "ServiceCatalog",
                table: "Api",
                newName: "ApiAuthentoicationMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Api_ApiMethodActionId",
                schema: "ServiceCatalog",
                table: "Api",
                newName: "IX_Api_ApiAuthentoicationMethodId");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceType",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceType",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceType",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ModifiedBy",
                schema: "ServiceCatalog",
                table: "ServiceStatus",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceRelatedIssue",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceRelatedIssue",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceRelatedIssue",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ModifiedBy",
                schema: "ServiceCatalog",
                table: "ServiceProvider",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ModifiedAt",
                schema: "ServiceCatalog",
                table: "ServiceProvider",
                type: "rowversion",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "rowversion",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceProvider",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceDocument",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceDocument",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceDocument",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceCantract",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceCantract",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceCantract",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceBoundle",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceBoundle",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceBoundle",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceBoundleId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "Service",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "BusinessResponsibleId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TechnicalResponsibleId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TechnicalSupervisorId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TechnicalSupportId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "PreRequisiteServices",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "PreRequisiteServices",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "PreRequisiteServices",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityService",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityService",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityService",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityExecutionPlan",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityExecutionPlan",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityExecutionPlan",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "VersionNumber",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "IsCurrentVersion",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldUnicode: false,
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiType",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiType",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ServiceCatalog",
                table: "ApiType",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsMandatory",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsMandatory",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "IsMandatory",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldUnicode: false,
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "IsMandatory",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldUnicode: false,
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "Api",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ServiceCatalog",
                table: "Api",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

           
            migrationBuilder.CreateIndex(
                name: "IX_Service_BusinessResponsibleId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "BusinessResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServicePriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TechnicalResponsibleId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "TechnicalResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TechnicalSupervisorId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "TechnicalSupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TechnicalSupportId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "TechnicalSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiType_Code",
                schema: "ServiceCatalog",
                table: "ApiType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApiAuthentoicationMethod_Code",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Api_Code",
                schema: "ServiceCatalog",
                table: "Api",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

           

            migrationBuilder.AddForeignKey(
                name: "FK_Api_ApiAuthentoicationMethod_ApiAuthentoicationMethodId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "ApiAuthentoicationMethodId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiAuthentoicationMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestBodyParam_ApiRequestBodyParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ParentId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiRequestBodyParam",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestBodyParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestHeaderParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestQueryStringParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestUrlParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResponseBodyParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResponseHeaderParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivity_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivity_Staff_TechnicalResponsibleId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "TechnicalResponsibleId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivity_Staff_TechnicalSupervisorId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "TechnicalSupervisorId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Department_OwnerDepartmentId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "OwnerDepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServicePriority_ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServicePriorityId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServicePriority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Staff_BusinessResponsibleId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "BusinessResponsibleId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Staff_TechnicalResponsibleId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "TechnicalResponsibleId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Staff_TechnicalSupervisorId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "TechnicalSupervisorId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Staff_TechnicalSupportId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "TechnicalSupportId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");
        }
    }
}
