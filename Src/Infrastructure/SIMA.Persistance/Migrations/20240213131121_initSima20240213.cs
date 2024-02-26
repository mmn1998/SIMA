using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class initSima20240213 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Project");

            migrationBuilder.EnsureSchema(
                name: "Basic");

            migrationBuilder.EnsureSchema(
                name: "Authentication");

            migrationBuilder.EnsureSchema(
                name: "Bank");

            migrationBuilder.EnsureSchema(
                name: "Organization");

            migrationBuilder.EnsureSchema(
                name: "DMS");

            migrationBuilder.EnsureSchema(
                name: "IssueManagement");

            migrationBuilder.CreateTable(
                name: "ActionType",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MainType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EventTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventInternalTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActiveStatus",
                schema: "Basic",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    IsDeleted = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    HasTimeToActiveAgain = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifyBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddressType",
                schema: "Basic",
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
                    table.PrimaryKey("PK_AddressType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchType",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrokerType",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Company_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Organization",
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationAttribute",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    EnglishKey = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsUserConfige = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_onfigurationAttribute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyType",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBaseCurrency = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentExtension",
                schema: "DMS",
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
                    table.PrimaryKey("PK_DocumentExtension", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "DMS",
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
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domain",
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
                    table.PrimaryKey("PK_Domain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                schema: "Basic",
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
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueApproval",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    IsApproval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkflowStepId = table.Column<long>(type: "bigint", nullable: false),
                    WorkflowActorId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueApproval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueCustomFeild",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    CustomeFeildId = table.Column<long>(type: "bigint", nullable: false),
                    KeyValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueCustomFeild", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueLinkReason",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLinkReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssuePriority",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ordering = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuePriority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueType",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorHex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueWeightCategory",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MinRange = table.Column<int>(type: "int", nullable: false),
                    MaxRange = table.Column<int>(type: "int", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueWeightCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhonType",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhonType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DomainID = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnglishKey = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationType",
                schema: "Basic",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_LocationType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LocationType_ActiveStatus_ActiveStatusId",
                        column: x => x.ActiveStatusId,
                        principalSchema: "Basic",
                        principalTable: "ActiveStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationType_LocationType_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Basic",
                        principalTable: "LocationType",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchTypeId = table.Column<long>(type: "bigint", nullable: true),
                    BranchChiefOfficerId = table.Column<long>(type: "bigint", nullable: true),
                    BranchDeputyId = table.Column<long>(type: "bigint", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsMultiCurrencyBranch = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_BranchType",
                        column: x => x.BranchTypeId,
                        principalSchema: "Bank",
                        principalTable: "BranchType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Broker",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BrokerTypeId = table.Column<long>(type: "bigint", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Broker_BrokerType",
                        column: x => x.BrokerTypeId,
                        principalSchema: "Bank",
                        principalTable: "BrokerType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationAttributeValue",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationAttributeId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    IsUserConfige = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationAttributeValue_A",
                        column: x => x.ConfigurationAttributeId,
                        principalSchema: "Basic",
                        principalTable: "ConfigurationAttribute",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SysConfig",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ConfigurationId = table.Column<long>(type: "bigint", nullable: true),
                    KeyValue = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysConfig_ConfigurationAttribute",
                        column: x => x.ConfigurationId,
                        principalSchema: "Basic",
                        principalTable: "ConfigurationAttribute",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowDocumentExtension",
                schema: "DMS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    WorkflowId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentExtensionId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowDocumentExtension", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowDocumentExtension_DocumentExtension_DocumentExtensionId",
                        column: x => x.DocumentExtensionId,
                        principalSchema: "DMS",
                        principalTable: "DocumentExtension",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "DMS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MainAggregateId = table.Column<long>(type: "bigint", nullable: false),
                    SourceId = table.Column<long>(type: "bigint", nullable: false),
                    AttachStepId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    FileExtensionId = table.Column<long>(type: "bigint", nullable: false),
                    FileAddress = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentExtension_FileExtensionId",
                        column: x => x.FileExtensionId,
                        principalSchema: "DMS",
                        principalTable: "DocumentExtension",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "DMS",
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowDocumentType",
                schema: "DMS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    WorkflowId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowDocumentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowDocumentType_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "DMS",
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Form",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsSystemForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Form_Domain_DomainId",
                        column: x => x.DomainId,
                        principalSchema: "Authentication",
                        principalTable: "Domain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MainAggregate",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_MainAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainAggregate_Domain_DomainId",
                        column: x => x.DomainId,
                        principalSchema: "Authentication",
                        principalTable: "Domain",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnglishKey = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Domain",
                        column: x => x.DomainId,
                        principalSchema: "Authentication",
                        principalTable: "Domain",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GenderId = table.Column<long>(type: "bigint", nullable: true),
                    NationalID = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    Brithday = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_Gender",
                        column: x => x.GenderId,
                        principalSchema: "Basic",
                        principalTable: "Gender",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CurrentWorkflowId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Summery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStateId = table.Column<long>(type: "bigint", nullable: false),
                    CurrenStepId = table.Column<long>(type: "bigint", nullable: false),
                    MainAggregateId = table.Column<long>(type: "bigint", nullable: false),
                    SourceId = table.Column<long>(type: "bigint", nullable: false),
                    IssueTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IssuePriorityId = table.Column<long>(type: "bigint", nullable: false),
                    IssueWeightCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issue_IssuePriority_IssuePriorityId",
                        column: x => x.IssuePriorityId,
                        principalSchema: "IssueManagement",
                        principalTable: "IssuePriority",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issue_IssueType_IssueTypeId",
                        column: x => x.IssueTypeId,
                        principalSchema: "IssueManagement",
                        principalTable: "IssueType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issue_IssueWeightCategory_IssueWeightCategoryId",
                        column: x => x.IssueWeightCategoryId,
                        principalSchema: "IssueManagement",
                        principalTable: "IssueWeightCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectGroup",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProjectID = table.Column<long>(type: "bigint", nullable: false),
                    GroupID = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectGroup_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "Project",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMember",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProjectID = table.Column<long>(type: "bigint", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    IsManager = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    IsAdminProject = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMember_Project",
                        column: x => x.ProjectID,
                        principalSchema: "Project",
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkFlow",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectID = table.Column<long>(type: "bigint", nullable: false),
                    FileContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAggregateId = table.Column<long>(type: "bigint", nullable: true),
                    ManagerRoleID = table.Column<long>(type: "bigint", nullable: true),
                    BpmnId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordering = table.Column<float>(type: "real", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())", comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlow_Project",
                        column: x => x.ProjectID,
                        principalSchema: "Project",
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "Basic",
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
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LocationTypeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_LocationType",
                        column: x => x.LocationTypeId,
                        principalSchema: "Basic",
                        principalTable: "LocationType",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Location_Location_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Basic",
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormGroup",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FormId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_FormGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormGroup_Form_FormId",
                        column: x => x.FormId,
                        principalSchema: "Authentication",
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Authentication",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormRole",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FormId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_FormRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormRole_Form_FormId",
                        column: x => x.FormId,
                        principalSchema: "Authentication",
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Authentication",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermission",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: true),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupPermission_Groups",
                        column: x => x.GroupId,
                        principalSchema: "Authentication",
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupPermission_Permission",
                        column: x => x.PermissionId,
                        principalSchema: "Authentication",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission",
                        column: x => x.PermissionId,
                        principalSchema: "Authentication",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role",
                        column: x => x.RoleId,
                        principalSchema: "Authentication",
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhoneBook",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProfileId = table.Column<long>(type: "bigint", nullable: true),
                    PhoneTypeId = table.Column<long>(type: "bigint", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneBook_PhonType",
                        column: x => x.PhoneTypeId,
                        principalSchema: "Basic",
                        principalTable: "PhonType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhoneBook_Profile",
                        column: x => x.ProfileId,
                        principalSchema: "Authentication",
                        principalTable: "Profile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    User_SecretKey = table.Column<string>(type: "text", nullable: true),
                    ProfileID = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    SecretKey = table.Column<string>(type: "text", nullable: false),
                    IsLoggedIn = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Company",
                        column: x => x.CompanyId,
                        principalSchema: "Organization",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Profile",
                        column: x => x.ProfileID,
                        principalSchema: "Authentication",
                        principalTable: "Profile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IssueComment",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueComment_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IssueDocument",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueDocument_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IssueHistory",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    SourceStateId = table.Column<long>(type: "bigint", nullable: false),
                    TargetStateId = table.Column<long>(type: "bigint", nullable: false),
                    SourceStepId = table.Column<long>(type: "bigint", nullable: false),
                    TargetStepId = table.Column<long>(type: "bigint", nullable: false),
                    PerformerUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueHistory_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IssueLink",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    IssueIdLinkedTo = table.Column<long>(type: "bigint", nullable: false),
                    IssueIdLinkReasonTo = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueLink_IssueLinkReason_IssueIdLinkReasonTo",
                        column: x => x.IssueIdLinkReasonTo,
                        principalSchema: "IssueManagement",
                        principalTable: "IssueLinkReason",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueLink_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueLink_Issue_IssueIdLinkedTo",
                        column: x => x.IssueIdLinkedTo,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "State",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkFlowID = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_WorkFlow",
                        column: x => x.WorkFlowID,
                        principalSchema: "Project",
                        principalTable: "WorkFlow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowActor",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowActor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowActor_WorkFlow_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalSchema: "Project",
                        principalTable: "WorkFlow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowCompany",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowCompany_WorkFlow_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalSchema: "Project",
                        principalTable: "WorkFlow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AddressBook",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProfileID = table.Column<long>(type: "bigint", nullable: true),
                    AddressTypeId = table.Column<long>(type: "bigint", nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressBook_AddressType",
                        column: x => x.AddressTypeId,
                        principalSchema: "Basic",
                        principalTable: "AddressType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AddressBook_Location",
                        column: x => x.LocationId,
                        principalSchema: "Basic",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AddressBook_Profile",
                        column: x => x.ProfileID,
                        principalSchema: "Authentication",
                        principalTable: "Profile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Company",
                        column: x => x.CompanyId,
                        principalSchema: "Organization",
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Department_Department_parent",
                        column: x => x.ParentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Department_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Basic",
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdminLocationAccess",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminLocationAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminLocationAccess_Location",
                        column: x => x.LocationId,
                        principalSchema: "Basic",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdminLocationAccess_User",
                        column: x => x.UserID,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormUser",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FormId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_FormUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormUser_Form_FormId",
                        column: x => x.FormId,
                        principalSchema: "Authentication",
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormUser_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserConfig",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    ConfigurationId = table.Column<long>(type: "bigint", nullable: true),
                    KeyValue = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConfig_ConfigurationAttribute",
                        column: x => x.ConfigurationId,
                        principalSchema: "Basic",
                        principalTable: "ConfigurationAttribute",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserConfig_User",
                        column: x => x.UserId,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserDomainAccess",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDomainAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDomainAccess_Domain",
                        column: x => x.DomainId,
                        principalSchema: "Authentication",
                        principalTable: "Domain",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDomainAccess_User",
                        column: x => x.UserId,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroup_Groups",
                        column: x => x.GroupId,
                        principalSchema: "Authentication",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_User",
                        column: x => x.UserId,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLocationAccess",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLocationAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLocationAccess_Location",
                        column: x => x.LocationId,
                        principalSchema: "Basic",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLocationAccess_User",
                        column: x => x.UserId,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPermission",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermission_Permission",
                        column: x => x.PermissionId,
                        principalSchema: "Authentication",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermission_User",
                        column: x => x.UserId,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role",
                        column: x => x.RoleId,
                        principalSchema: "Authentication",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User",
                        column: x => x.UserId,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Step",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WorkFlowID = table.Column<long>(type: "bigint", nullable: true),
                    ActionTypeId = table.Column<long>(type: "bigint", nullable: true),
                    StateID = table.Column<long>(type: "bigint", nullable: true),
                    BpmnId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Step_ActionType_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalSchema: "Project",
                        principalTable: "ActionType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Step_State",
                        column: x => x.StateID,
                        principalSchema: "Project",
                        principalTable: "State",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Step_WorkFlow",
                        column: x => x.WorkFlowID,
                        principalSchema: "Project",
                        principalTable: "WorkFlow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowActorGroup",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowActorID = table.Column<long>(type: "bigint", nullable: false),
                    GroupID = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowActorGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowActorGroup_WorkFlowActor_WorkFlowActorID",
                        column: x => x.WorkFlowActorID,
                        principalSchema: "Project",
                        principalTable: "WorkFlowActor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowActorRole",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowActorID = table.Column<long>(type: "bigint", nullable: false),
                    RoleID = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowActorRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowActorRole_WorkFlowActor",
                        column: x => x.WorkFlowActorID,
                        principalSchema: "Project",
                        principalTable: "WorkFlowActor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowActorUser",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowActorID = table.Column<long>(type: "bigint", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowActorUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowActorUser_WorkFlowActor",
                        column: x => x.WorkFlowActorID,
                        principalSchema: "Project",
                        principalTable: "WorkFlowActor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_Department",
                        column: x => x.DepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Progress",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SourceId = table.Column<long>(type: "bigint", nullable: false),
                    TargetId = table.Column<long>(type: "bigint", nullable: true),
                    WorkFlowId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BpmnId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progress_Step_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "Project",
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Progress_Step_TargetId",
                        column: x => x.TargetId,
                        principalSchema: "Project",
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Progress_WorkFlow_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalSchema: "Project",
                        principalTable: "WorkFlow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowActorStep",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowActorID = table.Column<long>(type: "bigint", nullable: false),
                    StepID = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowActorStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowActorStep_Step",
                        column: x => x.StepID,
                        principalSchema: "Project",
                        principalTable: "Step",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkFlowActorStep_WorkFlowActor",
                        column: x => x.WorkFlowActorID,
                        principalSchema: "Project",
                        principalTable: "WorkFlowActor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProfileId = table.Column<long>(type: "bigint", nullable: true),
                    ManagerId = table.Column<long>(type: "bigint", nullable: true),
                    PositionId = table.Column<long>(type: "bigint", nullable: true),
                    StaffNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Position",
                        column: x => x.PositionId,
                        principalSchema: "Organization",
                        principalTable: "Position",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Staff_Profile",
                        column: x => x.ProfileId,
                        principalSchema: "Authentication",
                        principalTable: "Profile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Staff_Profile_manager",
                        column: x => x.ManagerId,
                        principalSchema: "Authentication",
                        principalTable: "Profile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionType_Code",
                schema: "Project",
                table: "ActionType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AddressBook_AddressTypeId",
                schema: "Authentication",
                table: "AddressBook",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressBook_LocationId",
                schema: "Authentication",
                table: "AddressBook",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressBook_ProfileID",
                schema: "Authentication",
                table: "AddressBook",
                column: "ProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressType_Code",
                schema: "Basic",
                table: "AddressType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AdminLocationAccess_LocationId",
                schema: "Authentication",
                table: "AdminLocationAccess",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminLocationAccess_UserID",
                schema: "Authentication",
                table: "AdminLocationAccess",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchTypeId",
                schema: "Bank",
                table: "Branch",
                column: "BranchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Code",
                schema: "Bank",
                table: "Branch",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BranchType_Code",
                schema: "Bank",
                table: "BranchType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Broker_BrokerTypeId",
                schema: "Bank",
                table: "Broker",
                column: "BrokerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Broker_Code",
                schema: "Bank",
                table: "Broker",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerType_Code",
                schema: "Bank",
                table: "BrokerType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Code",
                schema: "Organization",
                table: "Company",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ParentId",
                schema: "Organization",
                table: "Company",
                column: "ParentId",
                unique: true,
                filter: "[ParentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "index_Key",
                schema: "Basic",
                table: "ConfigurationAttribute",
                column: "EnglishKey",
                unique: true,
                filter: "[EnglishKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationAttributeValue_ConfigurationAttributeId",
                schema: "Basic",
                table: "ConfigurationAttributeValue",
                column: "ConfigurationAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyType_Code",
                schema: "Bank",
                table: "CurrencyType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Department",
                schema: "Organization",
                table: "Department",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Code",
                schema: "Organization",
                table: "Department",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyId",
                schema: "Organization",
                table: "Department",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_LocationId",
                schema: "Organization",
                table: "Department",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentId",
                schema: "Organization",
                table: "Department",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentExtension_Code",
                schema: "DMS",
                table: "DocumentExtension",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Code",
                schema: "DMS",
                table: "Documents",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                schema: "DMS",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_FileExtensionId",
                schema: "DMS",
                table: "Documents",
                column: "FileExtensionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_Code",
                schema: "DMS",
                table: "DocumentType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Code",
                schema: "Authentication",
                table: "Domain",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Form_Code",
                schema: "Authentication",
                table: "Form",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Form_DomainId",
                schema: "Authentication",
                table: "Form",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_FormGroup_FormId",
                schema: "Authentication",
                table: "FormGroup",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormGroup_GroupId",
                schema: "Authentication",
                table: "FormGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRole_FormId",
                schema: "Authentication",
                table: "FormRole",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRole_RoleId",
                schema: "Authentication",
                table: "FormRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FormUser_FormId",
                schema: "Authentication",
                table: "FormUser",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormUser_UserId",
                schema: "Authentication",
                table: "FormUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Code",
                schema: "Basic",
                table: "Gender",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermission_GroupId",
                schema: "Authentication",
                table: "GroupPermission",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermission_PermissionId",
                schema: "Authentication",
                table: "GroupPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Code",
                schema: "Authentication",
                table: "Groups",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_Code",
                schema: "IssueManagement",
                table: "Issue",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issue_IssuePriorityId",
                schema: "IssueManagement",
                table: "Issue",
                column: "IssuePriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_IssueTypeId",
                schema: "IssueManagement",
                table: "Issue",
                column: "IssueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_IssueWeightCategoryId",
                schema: "IssueManagement",
                table: "Issue",
                column: "IssueWeightCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueComment_IssueId",
                schema: "IssueManagement",
                table: "IssueComment",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueDocument_IssueId",
                schema: "IssueManagement",
                table: "IssueDocument",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueHistory_IssueId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLink_IssueId",
                schema: "IssueManagement",
                table: "IssueLink",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLink_IssueIdLinkedTo",
                schema: "IssueManagement",
                table: "IssueLink",
                column: "IssueIdLinkedTo");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLink_IssueIdLinkReasonTo",
                schema: "IssueManagement",
                table: "IssueLink",
                column: "IssueIdLinkReasonTo");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLinkReason_Code",
                schema: "IssueManagement",
                table: "IssueLinkReason",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssuePriority_Code",
                schema: "IssueManagement",
                table: "IssuePriority",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueType_Code",
                schema: "IssueManagement",
                table: "IssueType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueWeightCategory_Code",
                schema: "IssueManagement",
                table: "IssueWeightCategory",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Code",
                schema: "Basic",
                table: "Location",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationTypeId",
                schema: "Basic",
                table: "Location",
                column: "LocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ParentId",
                schema: "Basic",
                table: "Location",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationType_ActiveStatusId",
                schema: "Basic",
                table: "LocationType",
                column: "ActiveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationType_Code",
                schema: "Basic",
                table: "LocationType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LocationType_ParentId",
                schema: "Basic",
                table: "LocationType",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "index_Code",
                schema: "Authentication",
                table: "MainAggregate",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MainAggregate_DomainId",
                schema: "Authentication",
                table: "MainAggregate",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentType_Code",
                schema: "Bank",
                table: "PaymentType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Code",
                schema: "Authentication",
                table: "Permission",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_DomainId",
                schema: "Authentication",
                table: "Permission",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneBook_PhoneTypeId",
                schema: "Authentication",
                table: "PhoneBook",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneBook_ProfileId",
                schema: "Authentication",
                table: "PhoneBook",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PhonType_Code",
                schema: "Basic",
                table: "PhonType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Position_Code",
                schema: "Organization",
                table: "Position",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Position_DepartmentId",
                schema: "Organization",
                table: "Position",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "index_national",
                schema: "Authentication",
                table: "Profile",
                column: "NationalID",
                unique: true,
                filter: "[NationalID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_GenderId",
                schema: "Authentication",
                table: "Profile",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_SourceId",
                schema: "Project",
                table: "Progress",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_TargetId",
                schema: "Project",
                table: "Progress",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_WorkFlowId",
                schema: "Project",
                table: "Progress",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Code",
                schema: "Project",
                table: "Project",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGroup_ProjectID",
                schema: "Project",
                table: "ProjectGroup",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_ProjectID",
                schema: "Project",
                table: "ProjectMember",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Code",
                schema: "Authentication",
                table: "Role",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Code_EnglishKey",
                schema: "Authentication",
                table: "Role",
                columns: new[] { "Code", "EnglishKey" },
                unique: true,
                filter: "[Code] IS NOT NULL AND [EnglishKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "Authentication",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                schema: "Authentication",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ManagerId",
                schema: "Organization",
                table: "Staff",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PositionId",
                schema: "Organization",
                table: "Staff",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ProfileId",
                schema: "Organization",
                table: "Staff",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "index_code",
                schema: "Project",
                table: "State",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_State_WorkFlowID",
                schema: "Project",
                table: "State",
                column: "WorkFlowID");

            migrationBuilder.CreateIndex(
                name: "IX_Step_ActionTypeId",
                schema: "Project",
                table: "Step",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_StateID",
                schema: "Project",
                table: "Step",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_Step_WorkFlowID",
                schema: "Project",
                table: "Step",
                column: "WorkFlowID");

            migrationBuilder.CreateIndex(
                name: "IX_SysConfig_ConfigurationId",
                schema: "Authentication",
                table: "SysConfig",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConfig_ConfigurationId",
                schema: "Authentication",
                table: "UserConfig",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConfig_UserId",
                schema: "Authentication",
                table: "UserConfig",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDomainAccess_DomainId",
                schema: "Authentication",
                table: "UserDomainAccess",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDomainAccess_UserId",
                schema: "Authentication",
                table: "UserDomainAccess",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                schema: "Authentication",
                table: "UserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId",
                schema: "Authentication",
                table: "UserGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocationAccess_LocationId",
                schema: "Authentication",
                table: "UserLocationAccess",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocationAccess_UserId",
                schema: "Authentication",
                table: "UserLocationAccess",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_PermissionId",
                schema: "Authentication",
                table: "UserPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_UserId",
                schema: "Authentication",
                table: "UserPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "Authentication",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "Authentication",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "index_Username",
                schema: "Authentication",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "index_Username_ID",
                schema: "Authentication",
                table: "Users",
                columns: new[] { "Username", "ProfileID", "CompanyId" },
                unique: true,
                filter: "[ProfileID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                schema: "Authentication",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileID",
                schema: "Authentication",
                table: "Users",
                column: "ProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlow_Code",
                schema: "Project",
                table: "WorkFlow",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlow_ProjectID",
                schema: "Project",
                table: "WorkFlow",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActor_Code",
                schema: "Project",
                table: "WorkFlowActor",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActor_WorkFlowId",
                schema: "Project",
                table: "WorkFlowActor",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActorGroup_WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorGroup",
                column: "WorkFlowActorID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActorRole_WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorRole",
                column: "WorkFlowActorID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActorStep_StepID",
                schema: "Project",
                table: "WorkFlowActorStep",
                column: "StepID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActorStep_WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorStep",
                column: "WorkFlowActorID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActorUser_WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorUser",
                column: "WorkFlowActorID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowCompany_WorkFlowId",
                schema: "Project",
                table: "WorkFlowCompany",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDocumentExtension_DocumentExtensionId",
                schema: "DMS",
                table: "WorkflowDocumentExtension",
                column: "DocumentExtensionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDocumentType_DocumentTypeId",
                schema: "DMS",
                table: "WorkflowDocumentType",
                column: "DocumentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressBook",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "AdminLocationAccess",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "Branch",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "Broker",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "ConfigurationAttributeValue",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "CurrencyType",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "DMS");

            migrationBuilder.DropTable(
                name: "FormGroup",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "FormRole",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "FormUser",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "GroupPermission",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "IssueApproval",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "IssueComment",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "IssueCustomFeild",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "IssueDocument",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "IssueHistory",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "IssueLink",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "MainAggregate",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "PaymentType",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "PhoneBook",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "Progress",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "ProjectGroup",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "ProjectMember",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "Staff",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "SysConfig",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "UserConfig",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "UserDomainAccess",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "UserGroup",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "UserLocationAccess",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "UserPermission",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "WorkFlowActorGroup",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "WorkFlowActorRole",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "WorkFlowActorStep",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "WorkFlowActorUser",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "WorkFlowCompany",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "WorkflowDocumentExtension",
                schema: "DMS");

            migrationBuilder.DropTable(
                name: "WorkflowDocumentType",
                schema: "DMS");

            migrationBuilder.DropTable(
                name: "AddressType",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "BranchType",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "BrokerType",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "Form",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "IssueLinkReason",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "Issue",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "PhonType",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "ConfigurationAttribute",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "Step",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "WorkFlowActor",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "DocumentExtension",
                schema: "DMS");

            migrationBuilder.DropTable(
                name: "DocumentType",
                schema: "DMS");

            migrationBuilder.DropTable(
                name: "IssuePriority",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "IssueType",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "IssueWeightCategory",
                schema: "IssueManagement");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Domain",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "Profile",
                schema: "Authentication");

            migrationBuilder.DropTable(
                name: "ActionType",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "State",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "Gender",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "WorkFlow",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "LocationType",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "ActiveStatus",
                schema: "Basic");
        }
    }
}
