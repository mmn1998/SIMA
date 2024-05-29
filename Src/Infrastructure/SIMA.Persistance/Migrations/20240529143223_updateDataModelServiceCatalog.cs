using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateDataModelServiceCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ServiceCatalog");

            migrationBuilder.CreateTable(
                name: "ApiAuthentoicationMethod",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiAuthentoicationMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiMethodCall",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiMethodCall", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CriticalActivity",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    TechnicalResponsibleId = table.Column<long>(type: "bigint", nullable: true),
                    TechnicalSupervisorId = table.Column<long>(type: "bigint", nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriticalActivity_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CriticalActivity_Staff_TechnicalResponsibleId",
                        column: x => x.TechnicalResponsibleId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CriticalActivity_Staff_TechnicalSupervisorId",
                        column: x => x.TechnicalSupervisorId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NetworkProtocol",
                schema: "Basic",
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
                    table.PrimaryKey("PK_NetworkProtocol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCustomerType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCustomerType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCustomerType_ServiceCustomerType_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceCustomerType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServicePriority",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ordering = table.Column<int>(type: "int", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePriority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceStatus",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceUserType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUserType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceUserType_ServiceUserType_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceUserType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CriticalActivityAsset",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CriticalActivityId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalActivityAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriticalActivityAsset_CriticalActivity_CriticalActivityId",
                        column: x => x.CriticalActivityId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "CriticalActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CriticalActivityAssignStaff",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CriticalActivityId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalActivityAssignStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriticalActivityAssignStaff_CriticalActivity_CriticalActivityId",
                        column: x => x.CriticalActivityId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "CriticalActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriticalActivityAssignStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CriticalActivityExecutionPlan",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CriticalActivityId = table.Column<long>(type: "bigint", nullable: false),
                    WeekyDay = table.Column<int>(type: "int", nullable: false),
                    ServiceAvalibilityStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ServiceAvalibilityEndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalActivityExecutionPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriticalActivityExecutionPlan_CriticalActivity_CriticalActivityId",
                        column: x => x.CriticalActivityId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "CriticalActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Api",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prerequisites = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PortNumber = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    RateLimitingMin = table.Column<int>(type: "int", nullable: true),
                    RateLimitingMax = table.Column<int>(type: "int", nullable: true),
                    ApiTypeId = table.Column<long>(type: "bigint", nullable: true),
                    ApiMethodCallId = table.Column<long>(type: "bigint", nullable: true),
                    ApiAuthentoicationMethodId = table.Column<long>(type: "bigint", nullable: true),
                    NetworkProtocolId = table.Column<long>(type: "bigint", nullable: true),
                    AuthenticationWorkflow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerResponsibleId = table.Column<long>(type: "bigint", nullable: true),
                    RulesAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Api", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Api_ApiAuthentoicationMethod_ApiAuthentoicationMethodId",
                        column: x => x.ApiAuthentoicationMethodId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ApiAuthentoicationMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Api_ApiMethodCall_ApiMethodCallId",
                        column: x => x.ApiMethodCallId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ApiMethodCall",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Api_ApiType_ApiTypeId",
                        column: x => x.ApiTypeId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ApiType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Api_Department_OwnerDepartmentId",
                        column: x => x.OwnerDepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Api_NetworkProtocol_NetworkProtocolId",
                        column: x => x.NetworkProtocolId,
                        principalSchema: "Basic",
                        principalTable: "NetworkProtocol",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Api_Staff_OwnerResponsibleId",
                        column: x => x.OwnerResponsibleId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategory",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCategory_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApiDocument",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiDocument_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiRequestBodyParam",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsMandatory = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequestBodyParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiRequestBodyParam_ApiRequestBodyParam_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ApiRequestBodyParam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApiRequestBodyParam_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiRequestHeaderParam",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsMandatory = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequestHeaderParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiRequestHeaderParam_ApiRequestHeaderParam_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ApiRequestHeaderParam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApiRequestHeaderParam_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiRequestQueryStringParam",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsMandatory = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequestQueryStringParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiRequestQueryStringParam_ApiRequestQueryStringParam_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ApiRequestQueryStringParam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApiRequestQueryStringParam_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiRequestUrlParam",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsMandatory = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequestUrlParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiRequestUrlParam_ApiRequestUrlParam_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ApiRequestUrlParam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApiRequestUrlParam_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResponseBodyParam",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsMandatory = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResponseBodyParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResponseBodyParam_ApiResponseBodyParam_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ApiResponseBodyParam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApiResponseBodyParam_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResponseHeaderParam",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsMandatory = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResponseHeaderParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResponseHeaderParam_ApiResponseHeaderParam_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ApiResponseHeaderParam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApiResponseHeaderParam_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiSupportTeam",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiSupportTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiSupportTeam_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiSupportTeam_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiVersion",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    VersionNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCurrentVersion = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiVersion_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceBoundle",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceBoundle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceBoundle_ServiceCategory_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ServiceCost = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceWorkflowDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContinuousImprovement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Suggestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ServiceBoundleId = table.Column<long>(type: "bigint", nullable: true),
                    TechnicalResponsibleId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessResponsibleId = table.Column<long>(type: "bigint", nullable: false),
                    TechnicalSupportId = table.Column<long>(type: "bigint", nullable: false),
                    TechnicalSupervisorId = table.Column<long>(type: "bigint", nullable: false),
                    ServicePriorityId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    TechnicalSupervisorDepartmentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Department_OwnerDepartmentId",
                        column: x => x.OwnerDepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_Department_TechnicalSupervisorDepartmentId",
                        column: x => x.TechnicalSupervisorDepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_ServiceBoundle_ServiceBoundleId",
                        column: x => x.ServiceBoundleId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceBoundle",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_ServicePriority_ServicePriorityId",
                        column: x => x.ServicePriorityId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServicePriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Service_Staff_BusinessResponsibleId",
                        column: x => x.BusinessResponsibleId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_Staff_TechnicalResponsibleId",
                        column: x => x.TechnicalResponsibleId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_Staff_TechnicalSupervisorId",
                        column: x => x.TechnicalSupervisorId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_Staff_TechnicalSupportId",
                        column: x => x.TechnicalSupportId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CriticalActivityRisk",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CriticalActivityId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ServiceId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalActivityRisk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriticalActivityRisk_CriticalActivity_CriticalActivityId",
                        column: x => x.CriticalActivityId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "CriticalActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriticalActivityRisk_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CriticalActivityService",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CriticalActivityId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalActivityService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriticalActivityService_CriticalActivity_CriticalActivityId",
                        column: x => x.CriticalActivityId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "CriticalActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriticalActivityService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreRequisiteServices",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    PreRequiredServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreRequisiteServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreRequisiteServices_Service_PreRequiredServiceId",
                        column: x => x.PreRequiredServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreRequisiteServices_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceApi",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceApi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceApi_Api_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Api",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceApi_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAsset",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAsset_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceAssignStaff",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAssignStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAssignStaff_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceAssignStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceAvalibility",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    WeekDay = table.Column<int>(type: "int", nullable: false),
                    ServiceAvalibilityStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ServiceAvalibilityEndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAvalibility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAvalibility_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceCantract",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCantract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCantract_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceChanel",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ChannelTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceChanel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceChanel_ChannelType_ChannelTypeId",
                        column: x => x.ChannelTypeId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ChannelType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceChanel_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceCustomer",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceCustomerTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCustomer_ServiceCustomerType_ServiceCustomerTypeId",
                        column: x => x.ServiceCustomerTypeId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceCustomerType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceCustomer_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceDocument",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceDocument_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceProvider",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProvider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProvider_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Organization",
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceProvider_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceRelatedIssue",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRelatedIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRelatedIssue_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceRelatedIssue_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceUser",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceUserTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceUser_ServiceUserType_ServiceUserTypeId",
                        column: x => x.ServiceUserTypeId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceUserType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceUser_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Api_ApiAuthentoicationMethodId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "ApiAuthentoicationMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Api_ApiMethodCallId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "ApiMethodCallId");

            migrationBuilder.CreateIndex(
                name: "IX_Api_ApiTypeId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "ApiTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Api_Code",
                schema: "ServiceCatalog",
                table: "Api",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Api_NetworkProtocolId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "NetworkProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_Api_OwnerDepartmentId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "OwnerDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Api_OwnerResponsibleId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "OwnerResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiAuthentoicationMethod_Code",
                schema: "ServiceCatalog",
                table: "ApiAuthentoicationMethod",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApiDocument_ApiId",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiDocument_DocumentId",
                schema: "ServiceCatalog",
                table: "ApiDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiMethodCall_Code",
                schema: "ServiceCatalog",
                table: "ApiMethodCall",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestBodyParam_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestBodyParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestHeaderParam_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestHeaderParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestHeaderParam",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestQueryStringParam_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestQueryStringParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestQueryStringParam",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestUrlParam_ApiId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestUrlParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestUrlParam",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResponseBodyParam_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResponseBodyParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiResponseBodyParam",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResponseHeaderParam_ApiId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResponseHeaderParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiResponseHeaderParam",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiSupportTeam_ApiId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiSupportTeam_StaffId",
                schema: "ServiceCatalog",
                table: "ApiSupportTeam",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiType_Code",
                schema: "ServiceCatalog",
                table: "ApiType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApiVersion_ApiId",
                schema: "ServiceCatalog",
                table: "ApiVersion",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelType_Code",
                schema: "ServiceCatalog",
                table: "ChannelType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivity_Code",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivity_DepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivity_TechnicalResponsibleId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "TechnicalResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivity_TechnicalSupervisorId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "TechnicalSupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityAsset_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                column: "CriticalActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityAssignStaff_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                column: "CriticalActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityAssignStaff_StaffId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityExecutionPlan_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityExecutionPlan",
                column: "CriticalActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityRisk_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                column: "CriticalActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityRisk_ServiceId",
                schema: "ServiceCatalog",
                table: "CriticalActivityRisk",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityService_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityService",
                column: "CriticalActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityService_ServiceId",
                schema: "ServiceCatalog",
                table: "CriticalActivityService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkProtocol_Code",
                schema: "Basic",
                table: "NetworkProtocol",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PreRequisiteServices_PreRequiredServiceId",
                schema: "ServiceCatalog",
                table: "PreRequisiteServices",
                column: "PreRequiredServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PreRequisiteServices_ServiceId",
                schema: "ServiceCatalog",
                table: "PreRequisiteServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_BusinessResponsibleId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "BusinessResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Code",
                schema: "ServiceCatalog",
                table: "Service",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_OwnerDepartmentId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "OwnerDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceBoundleId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServiceBoundleId");

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
                name: "IX_Service_TechnicalSupervisorDepartmentId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "TechnicalSupervisorDepartmentId");

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
                name: "IX_ServiceApi_ApiId",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceApi_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceApi",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAsset_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceAsset",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAssignStaff_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceAssignStaff",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAssignStaff_StaffId",
                schema: "ServiceCatalog",
                table: "ServiceAssignStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAvalibility_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBoundle_Code",
                schema: "ServiceCatalog",
                table: "ServiceBoundle",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBoundle_ServiceCategoryId",
                schema: "ServiceCatalog",
                table: "ServiceBoundle",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCantract_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceCantract",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_Code",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_ServiceTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceChanel_ChannelTypeId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                column: "ChannelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceChanel_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCustomer_ServiceCustomerTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                column: "ServiceCustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCustomer_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCustomerType_Code",
                schema: "ServiceCatalog",
                table: "ServiceCustomerType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCustomerType_ParentId",
                schema: "ServiceCatalog",
                table: "ServiceCustomerType",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDocument_DocumentId",
                schema: "ServiceCatalog",
                table: "ServiceDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDocument_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceDocument",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePriority_Code",
                schema: "ServiceCatalog",
                table: "ServicePriority",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProvider_CompanyId",
                schema: "ServiceCatalog",
                table: "ServiceProvider",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProvider_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceProvider",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRelatedIssue_IssueId",
                schema: "ServiceCatalog",
                table: "ServiceRelatedIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRelatedIssue_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceRelatedIssue",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceStatus_Code",
                schema: "ServiceCatalog",
                table: "ServiceStatus",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_Code",
                schema: "ServiceCatalog",
                table: "ServiceType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUser_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUser_ServiceUserTypeId",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                column: "ServiceUserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserType_Code",
                schema: "ServiceCatalog",
                table: "ServiceUserType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserType_ParentId",
                schema: "ServiceCatalog",
                table: "ServiceUserType",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiDocument",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiRequestBodyParam",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiRequestHeaderParam",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiRequestQueryStringParam",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiRequestUrlParam",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiResponseBodyParam",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiResponseHeaderParam",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiSupportTeam",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiVersion",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "CriticalActivityAsset",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "CriticalActivityAssignStaff",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "CriticalActivityExecutionPlan",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "CriticalActivityRisk",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "CriticalActivityService",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "PreRequisiteServices",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceApi",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceAsset",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceAssignStaff",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceAvalibility",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceCantract",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceChanel",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceCustomer",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceDocument",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceProvider",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceRelatedIssue",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceStatus",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceUser",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "CriticalActivity",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "Api",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ChannelType",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceCustomerType",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceUserType",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "Service",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiAuthentoicationMethod",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiMethodCall",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ApiType",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "NetworkProtocol",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "ServiceBoundle",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServicePriority",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceCategory",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceType",
                schema: "ServiceCatalog");
        }
    }
}
