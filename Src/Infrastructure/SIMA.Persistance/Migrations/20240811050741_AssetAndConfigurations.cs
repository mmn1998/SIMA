using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AssetAndConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AssetAndConfiguration");

            migrationBuilder.CreateTable(
                name: "AssetPhysicalStatus",
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
                    table.PrimaryKey("PK_AssetPhysicalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetTechnicalStatusConfiguration",
                schema: "AssetAndConfiguration",
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
                    table.PrimaryKey("PK_AssetTechnicalStatusConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetType",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetType_AssetType_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessCriticality",
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
                    table.PrimaryKey("PK_BusinessCriticality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyBuildingLocation",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBuildingLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyBuildingLocation_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Organization",
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyBuildingLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Basic",
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemRelationshipType",
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
                    table.PrimaryKey("PK_ConfigurationItemRelationshipType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemStatus",
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
                    table.PrimaryKey("PK_ConfigurationItemStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemType",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_ConfigurationItemType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemType_ConfigurationItemType_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LicenseType",
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
                    table.PrimaryKey("PK_LicenseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnershipType",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnershipType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyBuildingLocationId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_CompanyBuildingLocation_CompanyBuildingLocationId",
                        column: x => x.CompanyBuildingLocationId,
                        principalSchema: "Authentication",
                        principalTable: "CompanyBuildingLocation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItem",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemStatusId = table.Column<long>(type: "bigint", nullable: false),
                    LicenseTypeId = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    LicenseSupplierId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyBuildingLocationId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItem_ConfigurationItemStatus_ConfigurationItemStatusId",
                        column: x => x.ConfigurationItemStatusId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItem_ConfigurationItemType_ConfigurationItemTypeId",
                        column: x => x.ConfigurationItemTypeId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItem_LicenseType_LicenseTypeId",
                        column: x => x.LicenseTypeId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "LicenseType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItem_Location_CompanyBuildingLocationId",
                        column: x => x.CompanyBuildingLocationId,
                        principalSchema: "Basic",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItem_Staff_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItem_Supplier_LicenseSupplierId",
                        column: x => x.LicenseSupplierId,
                        principalSchema: "Logistics",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItem_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Logistics",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: true),
                    AssetTypeId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufactureDate = table.Column<DateOnly>(type: "date", nullable: true),
                    OwnershipDate = table.Column<DateOnly>(type: "date", nullable: true),
                    InServiceDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpireDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RetiredDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetTechnicalStatusId = table.Column<long>(type: "bigint", nullable: true),
                    AssetPhysicalStatusId = table.Column<long>(type: "bigint", nullable: true),
                    OwnershipTypeId = table.Column<long>(type: "bigint", nullable: true),
                    OwnershipPrepaymentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OwnershipPaymentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserTypeId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessCriticalityId = table.Column<long>(type: "bigint", nullable: true),
                    PhysicalLocationId = table.Column<long>(type: "bigint", nullable: false),
                    HasConfidentialInformation = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_AssetPhysicalStatus_AssetPhysicalStatusId",
                        column: x => x.AssetPhysicalStatusId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetPhysicalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_AssetTechnicalStatusConfiguration_AssetTechnicalStatusId",
                        column: x => x.AssetTechnicalStatusId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetTechnicalStatusConfiguration",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_AssetType_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_BusinessCriticality_BusinessCriticalityId",
                        column: x => x.BusinessCriticalityId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "BusinessCriticality",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_Location_PhysicalLocationId",
                        column: x => x.PhysicalLocationId,
                        principalSchema: "Basic",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_OwnershipType_OwnershipTypeId",
                        column: x => x.OwnershipTypeId,
                        principalSchema: "Authentication",
                        principalTable: "OwnershipType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_Staff_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Logistics",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalSchema: "Authentication",
                        principalTable: "UserType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "Authentication",
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemChangeOwnerHistory",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    FromOwnerId = table.Column<long>(type: "bigint", nullable: false),
                    ToOwnerId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemChangeOwnerHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemChangeOwnerHistory_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemChangeOwnerHistory_Staff_FromOwnerId",
                        column: x => x.FromOwnerId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemChangeOwnerHistory_Staff_ToOwnerId",
                        column: x => x.ToOwnerId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemChangeStatusHistory",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FromConfigurationItemStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ToConfigurationItemStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemChangeStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemChangeStatusHistory_ConfigurationItemStatus_FromConfigurationItemStatusId",
                        column: x => x.FromConfigurationItemStatusId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemChangeStatusHistory_ConfigurationItemStatus_ToConfigurationItemStatusId",
                        column: x => x.ToConfigurationItemStatusId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemChangeStatusHistory_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemVersioning",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    VersionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemVersioning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemVersioning_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetChangeOwnerHistory",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    FromOwnerId = table.Column<long>(type: "bigint", nullable: true),
                    ToOwnerId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetChangeOwnerHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetChangeOwnerHistory_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetChangeOwnerHistory_Staff_FromOwnerId",
                        column: x => x.FromOwnerId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetChangeOwnerHistory_Staff_ToOwnerId",
                        column: x => x.ToOwnerId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetChangePhysicalStatusHistory",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    FromAssetPhysicalStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ToAssetPhysicalStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetChangePhysicalStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetChangePhysicalStatusHistory_AssetPhysicalStatus_FromAssetPhysicalStatusId",
                        column: x => x.FromAssetPhysicalStatusId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetPhysicalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetChangePhysicalStatusHistory_AssetPhysicalStatus_ToAssetPhysicalStatusId",
                        column: x => x.ToAssetPhysicalStatusId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetPhysicalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetChangePhysicalStatusHistory_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetChangeTechnicalStatusHistory",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    FromAssetTechnicalStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ToAssetTechnicalStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetChangeTechnicalStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatusConfiguration_FromAssetTechnicalStatusId",
                        column: x => x.FromAssetTechnicalStatusId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetTechnicalStatusConfiguration",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatusConfiguration_ToAssetTechnicalStatusId",
                        column: x => x.ToAssetTechnicalStatusId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetTechnicalStatusConfiguration",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetChangeTechnicalStatusHistory_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetDocument",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetDocument_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetIssue",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetIssue_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetIssue_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetWarehouseHistory",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    IsCheckIn = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    CheckDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetWarehouseHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetWarehouseHistory_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetWarehouseHistory_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "Authentication",
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemAccessInfo",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ConfigurationItemAccessInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemAccessInfo_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                        column: x => x.ConfigurationItemVersioningId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemVersioning",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemAsset",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemAsset_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemAsset_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                        column: x => x.ConfigurationItemVersioningId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemVersioning",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemAssetHistory",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    IsAssigned = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    AssignDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ConfigurationItemVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemAssetHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemAssetHistory_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemAssetHistory_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                        column: x => x.ConfigurationItemVersioningId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemVersioning",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemDocument",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemDocument_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                        column: x => x.ConfigurationItemVersioningId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemVersioning",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemIssue",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemIssue_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                        column: x => x.ConfigurationItemVersioningId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemVersioning",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemIssue_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemRelationship",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    RelatedConfigurationItemVersioningId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemRelationshipTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemRelationship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemRelationship_ConfigurationItemRelationshipType_ConfigurationItemRelationshipTypeId",
                        column: x => x.ConfigurationItemRelationshipTypeId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemRelationshipType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemRelationship_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                        column: x => x.ConfigurationItemVersioningId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemVersioning",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemRelationship_ConfigurationItemVersioning_RelatedConfigurationItemVersioningId",
                        column: x => x.RelatedConfigurationItemVersioningId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItemVersioning",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AssetPhysicalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "AssetPhysicalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "AssetTechnicalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AssetTypeId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_BusinessCriticalityId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "BusinessCriticalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_OwnerId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_OwnershipTypeId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "OwnershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_PhysicalLocationId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "PhysicalLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_SupplierId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_UserTypeId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_WarehouseId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetChangeOwnerHistory_AssetId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeOwnerHistory",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetChangeOwnerHistory_FromOwnerId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeOwnerHistory",
                column: "FromOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetChangeOwnerHistory_ToOwnerId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeOwnerHistory",
                column: "ToOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetChangePhysicalStatusHistory_AssetId",
                schema: "AssetAndConfiguration",
                table: "AssetChangePhysicalStatusHistory",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetChangePhysicalStatusHistory_FromAssetPhysicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangePhysicalStatusHistory",
                column: "FromAssetPhysicalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetChangePhysicalStatusHistory_ToAssetPhysicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangePhysicalStatusHistory",
                column: "ToAssetPhysicalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetChangeTechnicalStatusHistory_AssetId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetChangeTechnicalStatusHistory_FromAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory",
                column: "FromAssetTechnicalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetChangeTechnicalStatusHistory_ToAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory",
                column: "ToAssetTechnicalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDocument_AssetId",
                schema: "AssetAndConfiguration",
                table: "AssetDocument",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDocument_DocumentId",
                schema: "AssetAndConfiguration",
                table: "AssetDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetIssue_AssetId",
                schema: "AssetAndConfiguration",
                table: "AssetIssue",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetIssue_IssueId",
                schema: "AssetAndConfiguration",
                table: "AssetIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPhysicalStatus_Code",
                schema: "AssetAndConfiguration",
                table: "AssetPhysicalStatus",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTechnicalStatusConfiguration_Code",
                schema: "AssetAndConfiguration",
                table: "AssetTechnicalStatusConfiguration",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssetType_Code",
                schema: "AssetAndConfiguration",
                table: "AssetType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssetType_ParentId",
                schema: "AssetAndConfiguration",
                table: "AssetType",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetWarehouseHistory_AssetId",
                schema: "AssetAndConfiguration",
                table: "AssetWarehouseHistory",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetWarehouseHistory_WarehouseId",
                schema: "AssetAndConfiguration",
                table: "AssetWarehouseHistory",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCriticality_Code",
                schema: "AssetAndConfiguration",
                table: "BusinessCriticality",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBuildingLocation_CompanyId",
                schema: "Authentication",
                table: "CompanyBuildingLocation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBuildingLocation_LocationId",
                schema: "Authentication",
                table: "CompanyBuildingLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_Code",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_CompanyBuildingLocationId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "CompanyBuildingLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_ConfigurationItemStatusId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "ConfigurationItemStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_ConfigurationItemTypeId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "ConfigurationItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_LicenseSupplierId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "LicenseSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_LicenseTypeId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "LicenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_OwnerId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_SupplierId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemAccessInfo_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAccessInfo",
                column: "ConfigurationItemVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemAsset_AssetId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemAsset_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset",
                column: "ConfigurationItemVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemAssetHistory_AssetId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemAssetHistory_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory",
                column: "ConfigurationItemVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemChangeOwnerHistory_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemChangeOwnerHistory",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemChangeOwnerHistory_FromOwnerId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemChangeOwnerHistory",
                column: "FromOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemChangeOwnerHistory_ToOwnerId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemChangeOwnerHistory",
                column: "ToOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemChangeStatusHistory_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemChangeStatusHistory",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemChangeStatusHistory_FromConfigurationItemStatusId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemChangeStatusHistory",
                column: "FromConfigurationItemStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemChangeStatusHistory_ToConfigurationItemStatusId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemChangeStatusHistory",
                column: "ToConfigurationItemStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemDocument_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument",
                column: "ConfigurationItemVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemDocument_DocumentId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemIssue_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue",
                column: "ConfigurationItemVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemIssue_IssueId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemRelationship_ConfigurationItemRelationshipTypeId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                column: "ConfigurationItemRelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemRelationship_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                column: "ConfigurationItemVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemRelationship_RelatedConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationship",
                column: "RelatedConfigurationItemVersioningId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemRelationshipType_Code",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemRelationshipType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemStatus_Code",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemStatus",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemType_Code",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemType_ParentId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemType",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemVersioning_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemVersioning",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseType_Code",
                schema: "AssetAndConfiguration",
                table: "LicenseType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_CompanyBuildingLocationId",
                schema: "Authentication",
                table: "Warehouse",
                column: "CompanyBuildingLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetChangeOwnerHistory",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "AssetChangePhysicalStatusHistory",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "AssetChangeTechnicalStatusHistory",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "AssetDocument",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "AssetIssue",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "AssetWarehouseHistory",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemAccessInfo",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemAsset",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemAssetHistory",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemChangeOwnerHistory",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemChangeStatusHistory",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemDocument",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemIssue",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemRelationship",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "Asset",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemRelationshipType",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemVersioning",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "AssetPhysicalStatus",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "AssetTechnicalStatusConfiguration",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "AssetType",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "BusinessCriticality",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "OwnershipType",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "UserType",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "Warehouse",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "ConfigurationItem",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "CompanyBuildingLocation",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "ConfigurationItemStatus",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "ConfigurationItemType",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropTable(
                name: "LicenseType",
                schema: "AssetAndConfiguration");
        }
    }
}
