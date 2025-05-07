using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class asset20250309 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiDocument_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiDocument_Documents_DocumentId",
                schema: "ServiceCatalog",
                table: "ApiDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
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
                name: "FK_ApiRequestUrlParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResponseBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResponseHeaderParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiSupportTeam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiSupportTeam_Staff_StaffId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiVersion_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemAccessInfo_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemRelationship_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemRelationship_ConfigurationItemVersioning_RelatedConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceApi_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ServiceApi");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceApi_Service_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceApi");

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

            migrationBuilder.EnsureSchema(
                name: "Asset");

            migrationBuilder.EnsureSchema(
                name: "DataProcedureInputParams");

            migrationBuilder.EnsureSchema(
                name: "DataProcedureOutputParam");

            migrationBuilder.RenameColumn(
                name: "RelatedConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                newName: "RelatedConfigurationItemId");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                newName: "ConfigurationItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemRelationship_RelatedConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                newName: "IX_ConfigurationItemRelationship_RelatedConfigurationItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemRelationship_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                newName: "IX_ConfigurationItemRelationship_ConfigurationItemId");

            migrationBuilder.RenameColumn(
                name: "Port",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                newName: "PortTo");

            migrationBuilder.RenameColumn(
                name: "IPAddress",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                newName: "PortFrom");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                newName: "ConfigurationItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemAccessInfo_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                newName: "IX_ConfigurationItemAccessInfo_ConfigurationItemId");

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

            migrationBuilder.AddColumn<string>(
                name: "IPAddressFrom",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPAddressTo",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DataCenterId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VersionNumber",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AssetCategoryId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "AssetAndConfiguration",
                table: "Asset",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DataCenterId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OperationalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VersionNumber",
                schema: "AssetAndConfiguration",
                table: "Asset",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "IsInternalApi",
                schema: "ServiceCatalog",
                table: "Api",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssetAssignedStaffs",
                schema: "Asset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    ResponsibleTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAssignedStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetAssignedStaffs_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetAssignedStaffs_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Bank",
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetAssignedStaffs_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetAssignedStaffs_ResponsibleType_ResponsibleTypeId",
                        column: x => x.ResponsibleTypeId,
                        principalSchema: "Basic",
                        principalTable: "ResponsibleType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetAssignedStaffs_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetCustomField",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    AssetTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CustomeFieldTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsMandetory = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoundingViewName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ValueBoundingFeild = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TextBoundingFeild = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCustomField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetCustomField_AssetCustomField_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetCustomField",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetCustomField_AssetType_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetCustomField_CustomeFieldType_CustomeFieldTypeId",
                        column: x => x.CustomeFieldTypeId,
                        principalSchema: "Basic",
                        principalTable: "CustomeFieldType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BackupMethod",
                schema: "Asset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackupMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemApi",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemApi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemApi_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemApi_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemCustomField",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ConfigurationItemTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CustomeFieldTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsMandetory = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoundingViewName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ValueBoundingFeild = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TextBoundingFeild = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemCustomField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemCustomField_ConfigurationItemCustomField_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemCustomField",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemCustomField_ConfigurationItemType_ConfigurationItemTypeId",
                        column: x => x.ConfigurationItemTypeId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemCustomField_CustomeFieldType_CustomeFieldTypeId",
                        column: x => x.CustomeFieldTypeId,
                        principalSchema: "Basic",
                        principalTable: "CustomeFieldType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemCustomFieldValue",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemCustomFieldValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemCustomFieldValue_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemCustomFieldValue_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataCenter",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataProcedureType",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProcedureType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationalStatus",
                schema: "Asset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetCustomFieldValue",
                schema: "Asset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetCustomFieldId = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCustomFieldValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetCustomFieldValue_AssetCustomField_AssetCustomFieldId",
                        column: x => x.AssetCustomFieldId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetCustomField",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetCustomFieldValue_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemBackupSchedule",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    BackupConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    BackupMethodId = table.Column<long>(type: "bigint", nullable: false),
                    DataCenterId = table.Column<long>(type: "bigint", nullable: false),
                    TimeMeasurementId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastTestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemBackupSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemBackupSchedule_BackupMethod_BackupMethodId",
                        column: x => x.BackupMethodId,
                        principalSchema: "Asset",
                        principalTable: "BackupMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemBackupSchedule_ConfigurationItem_BackupConfigurationItemId",
                        column: x => x.BackupConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemBackupSchedule_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemBackupSchedule_DataCenter_DataCenterId",
                        column: x => x.DataCenterId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "DataCenter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemBackupSchedule_TimeMeasurement_TimeMeasurementId",
                        column: x => x.TimeMeasurementId,
                        principalSchema: "Basic",
                        principalTable: "TimeMeasurement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataProcedure",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VersionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInternalApi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatabaseId = table.Column<long>(type: "bigint", nullable: false),
                    DataProcedureTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProcedure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataProcedure_ConfigurationItem_DatabaseId",
                        column: x => x.DatabaseId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataProcedure_DataProcedureType_DataProcedureTypeId",
                        column: x => x.DataProcedureTypeId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "DataProcedureType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemDataProcedures",
                schema: "Asset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DataProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemDataProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemDataProcedures_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemDataProcedures_DataProcedure_DataProcedureId",
                        column: x => x.DataProcedureId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "DataProcedure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataCenter",
                schema: "DataProcedureInputParams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DataProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    IsMandatory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataCenter_DataCenter_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "DataProcedureInputParams",
                        principalTable: "DataCenter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataCenter_DataProcedure_DataProcedureId",
                        column: x => x.DataProcedureId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "DataProcedure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataCenter",
                schema: "DataProcedureOutputParam",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DataProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataCenter_DataCenter_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "DataProcedureOutputParam",
                        principalTable: "DataCenter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataCenter_DataProcedure_DataProcedureId",
                        column: x => x.DataProcedureId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "DataProcedure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataProcedureDocument",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DataProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProcedureDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataProcedureDocument_DataProcedure_DataProcedureId",
                        column: x => x.DataProcedureId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "DataProcedure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataProcedureDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataProcedureSupportTeam",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DataProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProcedureSupportTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataProcedureSupportTeam_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Bank",
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataProcedureSupportTeam_DataProcedure_DataProcedureId",
                        column: x => x.DataProcedureId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "DataProcedure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataProcedureSupportTeam_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataProcedureSupportTeam_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_DataCenterId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "DataCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AssetCategoryId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_DataCenterId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "DataCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_OperationalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "OperationalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiSupportTeam_BranchId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiSupportTeam_DepartmentId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignedStaffs_AssetId",
                schema: "Asset",
                table: "AssetAssignedStaffs",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignedStaffs_BranchId",
                schema: "Asset",
                table: "AssetAssignedStaffs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignedStaffs_DepartmentId",
                schema: "Asset",
                table: "AssetAssignedStaffs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignedStaffs_ResponsibleTypeId",
                schema: "Asset",
                table: "AssetAssignedStaffs",
                column: "ResponsibleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignedStaffs_StaffId",
                schema: "Asset",
                table: "AssetAssignedStaffs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCustomField_AssetTypeId",
                schema: "AssetAndConfiguration",
                table: "AssetCustomField",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCustomField_CustomeFieldTypeId",
                schema: "AssetAndConfiguration",
                table: "AssetCustomField",
                column: "CustomeFieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCustomField_ParentId",
                schema: "AssetAndConfiguration",
                table: "AssetCustomField",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCustomFieldValue_AssetCustomFieldId",
                schema: "Asset",
                table: "AssetCustomFieldValue",
                column: "AssetCustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCustomFieldValue_AssetId",
                schema: "Asset",
                table: "AssetCustomFieldValue",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Code",
                schema: "AssetAndConfiguration",
                table: "Category",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemApi_ApiId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemApi",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemApi_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemApi",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemBackupSchedule_BackupConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemBackupSchedule",
                column: "BackupConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemBackupSchedule_BackupMethodId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemBackupSchedule",
                column: "BackupMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemBackupSchedule_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemBackupSchedule",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemBackupSchedule_DataCenterId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemBackupSchedule",
                column: "DataCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemBackupSchedule_TimeMeasurementId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemBackupSchedule",
                column: "TimeMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemCustomField_ConfigurationItemTypeId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemCustomField",
                column: "ConfigurationItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemCustomField_CustomeFieldTypeId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemCustomField",
                column: "CustomeFieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemCustomField_ParentId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemCustomField",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemCustomFieldValue_AssetId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemCustomFieldValue",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemCustomFieldValue_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemCustomFieldValue",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemDataProcedures_ConfigurationItemId",
                schema: "Asset",
                table: "ConfigurationItemDataProcedures",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemDataProcedures_DataProcedureId",
                schema: "Asset",
                table: "ConfigurationItemDataProcedures",
                column: "DataProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_DataCenter_Code",
                schema: "AssetAndConfiguration",
                table: "DataCenter",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DataCenter_DataProcedureId",
                schema: "DataProcedureInputParams",
                table: "DataCenter",
                column: "DataProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_DataCenter_ParentId",
                schema: "DataProcedureInputParams",
                table: "DataCenter",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DataCenter_DataProcedureId",
                schema: "DataProcedureOutputParam",
                table: "DataCenter",
                column: "DataProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_DataCenter_ParentId",
                schema: "DataProcedureOutputParam",
                table: "DataCenter",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedure_Code",
                schema: "AssetAndConfiguration",
                table: "DataProcedure",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedure_DatabaseId",
                schema: "AssetAndConfiguration",
                table: "DataProcedure",
                column: "DatabaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedure_DataProcedureTypeId",
                schema: "AssetAndConfiguration",
                table: "DataProcedure",
                column: "DataProcedureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedureDocument_DataProcedureId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureDocument",
                column: "DataProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedureDocument_DocumentId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedureSupportTeam_BranchId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureSupportTeam",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedureSupportTeam_DataProcedureId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureSupportTeam",
                column: "DataProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedureSupportTeam_DepartmentId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureSupportTeam",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedureSupportTeam_StaffId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureSupportTeam",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProcedureType_Code",
                schema: "AssetAndConfiguration",
                table: "DataProcedureType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OperationalStatus_Code",
                schema: "Asset",
                table: "OperationalStatus",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiDocument_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiDocument_Documents_DocumentId",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                column: "DocumentId",
                principalSchema: "DMS",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestHeaderParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiSupportTeam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiSupportTeam_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiSupportTeam_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiSupportTeam_Staff_StaffId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "StaffId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiVersion_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Category_AssetCategoryId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "AssetCategoryId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_DataCenter_DataCenterId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "DataCenterId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "DataCenter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_OperationalStatus_OperationalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "OperationalStatusId",
                principalSchema: "Asset",
                principalTable: "OperationalStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItem_DataCenter_DataCenterId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "DataCenterId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "DataCenter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemAccessInfo_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                column: "ConfigurationItemId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemRelationship_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                column: "ConfigurationItemId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemRelationship_ConfigurationItem_RelatedConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                column: "RelatedConfigurationItemId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceApi_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceApi_Service_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                column: "ServiceId",
                principalSchema: "ServiceCatalog",
                principalTable: "Service",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiDocument_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiDocument_Documents_DocumentId",
                schema: "ServiceCatalog",
                table: "ApiDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestHeaderParam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiSupportTeam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiSupportTeam_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiSupportTeam_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiSupportTeam_Staff_StaffId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiVersion_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Category_AssetCategoryId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_DataCenter_DataCenterId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_OperationalStatus_OperationalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItem_DataCenter_DataCenterId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemAccessInfo_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemRelationship_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemRelationship_ConfigurationItem_RelatedConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceApi_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ServiceApi");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceApi_Service_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceApi");

            migrationBuilder.DropTable(
                name: "AssetAssignedStaffs",
                schema: "Asset");

            migrationBuilder.DropTable(
                name: "AssetCustomFieldValue",
                schema: "Asset");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemApi",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemBackupSchedule",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemCustomField",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemCustomFieldValue",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemDataProcedures",
                schema: "Asset");

            migrationBuilder.DropTable(
                name: "DataCenter",
                schema: "DataProcedureInputParams");

            migrationBuilder.DropTable(
                name: "DataCenter",
                schema: "DataProcedureOutputParam");

            migrationBuilder.DropTable(
                name: "DataProcedureDocument",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "DataProcedureSupportTeam",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "OperationalStatus",
                schema: "Asset");

            migrationBuilder.DropTable(
                name: "AssetCustomField",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "BackupMethod",
                schema: "Asset");

            migrationBuilder.DropTable(
                name: "DataCenter",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "DataProcedure",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "DataProcedureType",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_ConfigurationItem_DataCenterId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropIndex(
                name: "IX_Asset_AssetCategoryId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_DataCenterId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_OperationalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_ApiSupportTeam_BranchId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropIndex(
                name: "IX_ApiSupportTeam_DepartmentId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropColumn(
                name: "IPAddressFrom",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo");

            migrationBuilder.DropColumn(
                name: "IPAddressTo",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo");

            migrationBuilder.DropColumn(
                name: "DataCenterId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "VersionNumber",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "AssetCategoryId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "DataCenterId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "OperationalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "VersionNumber",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam");

            migrationBuilder.DropColumn(
                name: "IsInternalApi",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.RenameColumn(
                name: "RelatedConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                newName: "RelatedConfigurationItemVersioningId");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                newName: "ConfigurationItemVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemRelationship_RelatedConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                newName: "IX_ConfigurationItemRelationship_RelatedConfigurationItemVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemRelationship_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                newName: "IX_ConfigurationItemRelationship_ConfigurationItemVersioningId");

            migrationBuilder.RenameColumn(
                name: "PortTo",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                newName: "Port");

            migrationBuilder.RenameColumn(
                name: "PortFrom",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                newName: "IPAddress");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                newName: "ConfigurationItemVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemAccessInfo_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                newName: "IX_ConfigurationItemAccessInfo_ConfigurationItemVersioningId");

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

            migrationBuilder.AddColumn<long>(
                name: "ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ApiDocument_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiDocument_Documents_DocumentId",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                column: "DocumentId",
                principalSchema: "DMS",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_ApiRequestUrlParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_ApiResponseHeaderParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiSupportTeam_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiSupportTeam_Staff_StaffId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "StaffId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiVersion_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemAccessInfo_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                column: "ConfigurationItemVersioningId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItemVersioning",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemRelationship_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                column: "ConfigurationItemVersioningId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItemVersioning",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemRelationship_ConfigurationItemVersioning_RelatedConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                column: "RelatedConfigurationItemVersioningId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItemVersioning",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceApi_Api_ApiId",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                column: "ApiId",
                principalSchema: "ServiceCatalog",
                principalTable: "Api",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceApi_Service_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                column: "ServiceId",
                principalSchema: "ServiceCatalog",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
