using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class createNewSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BCP");

            migrationBuilder.EnsureSchema(
                name: "BusinessContinuityPlanService");

            migrationBuilder.EnsureSchema(
                name: "BusinessContinuityPlanStaff");

            migrationBuilder.EnsureSchema(
                name: "RiskManagement");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmCode",
                schema: "Authentication",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmCodeSendDate",
                schema: "Authentication",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BackupPeriod",
                schema: "BCP",
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
                    table.PrimaryKey("PK_BackupPeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityStrategy",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CordinatorId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsStableStrategy = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityStrategy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consequence",
                schema: "BCP",
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
                    table.PrimaryKey("PK_Consequence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImpactScale",
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
                    table.PrimaryKey("PK_ImpactScale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImportanceDegree",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Ordering = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportanceDegree", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaximumAcceptableOutage",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DurationHourFrom = table.Column<int>(type: "int", nullable: false),
                    DurationHourTo = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_MaximumAcceptableOutage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskDegree",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Degree = table.Column<float>(type: "real", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskDegree", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskImpact",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Impact = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskImpact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskLevel",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Level = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskPossibility",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Possibility = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskPossibility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskType",
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
                    table.PrimaryKey("PK_RiskType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicePriority",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ordering = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "BusinessContinuityPlan",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BusinessContinuityStrategyId = table.Column<long>(type: "bigint", nullable: false),
                    PlanOwnerId = table.Column<long>(type: "bigint", nullable: true),
                    ExecutiveResponsibleId = table.Column<long>(type: "bigint", nullable: true),
                    RecoveryManagerId = table.Column<long>(type: "bigint", nullable: true),
                    RecoveryDeputyId = table.Column<long>(type: "bigint", nullable: true),
                    OfferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlan_BusinessContinuityStrategy_BusinessContinuityStrategyId",
                        column: x => x.BusinessContinuityStrategyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlan_Staff_ExecutiveResponsibleId",
                        column: x => x.ExecutiveResponsibleId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlan_Staff_PlanOwnerId",
                        column: x => x.PlanOwnerId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlan_Staff_RecoveryDeputyId",
                        column: x => x.RecoveryDeputyId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlan_Staff_RecoveryManagerId",
                        column: x => x.RecoveryManagerId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityStrategyDocument",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityStategyId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityStrategyDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStrategyDocument_BusinessContinuityStrategy_BusinessContinuityStategyId",
                        column: x => x.BusinessContinuityStategyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStrategyDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityStrategyObjective",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityStategyId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityStrategyObjective", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStrategyObjective_BusinessContinuityStrategy_BusinessContinuityStategyId",
                        column: x => x.BusinessContinuityStategyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityStrategyRisk",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityStategyId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityStrategyRisk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStrategyRisk_BusinessContinuityStrategy_BusinessContinuityStategyId",
                        column: x => x.BusinessContinuityStategyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityStrategyService",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityStategyId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityStrategyService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStrategyService_BusinessContinuityStrategy_BusinessContinuityStategyId",
                        column: x => x.BusinessContinuityStategyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityStrategyStaff",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityStategyId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityStrategyStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStrategyStaff_BusinessContinuityStrategy_BusinessContinuityStategyId",
                        column: x => x.BusinessContinuityStategyId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityStrategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityStrategyStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskCriteria",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RiskDegreeId = table.Column<long>(type: "bigint", nullable: false),
                    RiskPossibilityId = table.Column<long>(type: "bigint", nullable: false),
                    RiskImpactId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskCriteria_RiskDegree",
                        column: x => x.RiskDegreeId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskDegree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskCriteria_RiskImpact",
                        column: x => x.RiskImpactId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskImpact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskCriteria_RiskPossibility",
                        column: x => x.RiskPossibilityId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskPossibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskLevelMeasure",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RiskLevelId = table.Column<long>(type: "bigint", nullable: false),
                    RiskPossibilityId = table.Column<long>(type: "bigint", nullable: false),
                    RiskImpactId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskLevelMeasure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskLevelMeasure_RiskImpact",
                        column: x => x.RiskImpactId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskImpact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskLevelMeasure_RiskLevel",
                        column: x => x.RiskLevelId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskLevelMeasure_RiskPossibility",
                        column: x => x.RiskPossibilityId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskPossibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Risk",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskTypeId = table.Column<long>(type: "bigint", nullable: false),
                    RiskImpactId = table.Column<long>(type: "bigint", nullable: false),
                    RiskPossibilityId = table.Column<long>(type: "bigint", nullable: false),
                    ARO = table.Column<int>(type: "int", nullable: false),
                    AV = table.Column<double>(type: "float", nullable: false),
                    EF = table.Column<float>(type: "real", nullable: false),
                    SLE = table.Column<float>(type: "real", nullable: false),
                    ALE = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Risk_RiskImpact",
                        column: x => x.RiskImpactId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskImpact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Risk_RiskPossibility",
                        column: x => x.RiskPossibilityId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskPossibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Risk_RiskType",
                        column: x => x.RiskTypeId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessImpactAnalysis",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ImportanceDegreeId = table.Column<long>(type: "bigint", nullable: false),
                    ServicePriorityId = table.Column<long>(type: "bigint", nullable: false),
                    BackupPeriodId = table.Column<long>(type: "bigint", nullable: false),
                    MaximumAcceptableOutageId = table.Column<long>(type: "bigint", nullable: true),
                    ConsequenceId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RTO = table.Column<float>(type: "real", nullable: true),
                    RPO = table.Column<float>(type: "real", nullable: true),
                    WRT = table.Column<float>(type: "real", nullable: true),
                    MTD = table.Column<float>(type: "real", nullable: true),
                    RestartReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImpactAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysis_BackupPeriod_BackupPeriodId",
                        column: x => x.BackupPeriodId,
                        principalSchema: "BCP",
                        principalTable: "BackupPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysis_Consequence_ConsequenceId",
                        column: x => x.ConsequenceId,
                        principalSchema: "BCP",
                        principalTable: "Consequence",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysis_ImportanceDegree_ImportanceDegreeId",
                        column: x => x.ImportanceDegreeId,
                        principalSchema: "BCP",
                        principalTable: "ImportanceDegree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysis_MaximumAcceptableOutage_MaximumAcceptableOutageId",
                        column: x => x.MaximumAcceptableOutageId,
                        principalSchema: "BCP",
                        principalTable: "MaximumAcceptableOutage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysis_ServicePriority_ServicePriorityId",
                        column: x => x.ServicePriorityId,
                        principalSchema: "BCP",
                        principalTable: "ServicePriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BCP",
                schema: "BusinessContinuityPlanService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BCP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BCP_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BCP",
                schema: "BusinessContinuityPlanStaff",
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
                    table.PrimaryKey("PK_BCP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BCP_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BCP_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanCriticalActivity",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanCriticalActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanDetailPlanningAssumption",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
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
                name: "BusinessContinuityPlanGeneralAssumption",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanGeneralAssumption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanGeneralAssumption_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanRecoveryCriteria",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanRecoveryCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanRecoveryCriteria_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanRecoveryOption",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanRecoveryOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanRecoveryOption_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanRisk",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanRisk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanRisk_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectedAsset",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RiskId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectedAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectedAsset_Risk",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralControlAction",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RiskId = table.Column<long>(type: "bigint", nullable: false),
                    ActionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralControlAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralControlAction_Risk",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NextPossibleAction",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RiskId = table.Column<long>(type: "bigint", nullable: false),
                    ActionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextPossibleAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NextPossibleAction_Risk",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskRelatedIssue",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RiskId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskRelatedIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskRelatedIssue_Issue",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskRelatedIssue_Risk",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessImpactAnalysisAnnouncement",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessImpactAnalysisId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImpactAnalysisAnnouncement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisAnnouncement_BusinessImpactAnalysis_BusinessImpactAnalysisId",
                        column: x => x.BusinessImpactAnalysisId,
                        principalSchema: "BCP",
                        principalTable: "BusinessImpactAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisAnnouncement_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessImpactAnalysisAsset",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessImpactAnalysisId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImpactAnalysisAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisAsset_BusinessImpactAnalysis_BusinessImpactAnalysisId",
                        column: x => x.BusinessImpactAnalysisId,
                        principalSchema: "BCP",
                        principalTable: "BusinessImpactAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessImpactAnalysisCriticalActivity",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessImpactAnalysisId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImpactAnalysisCriticalActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisCriticalActivity_BusinessImpactAnalysis_BusinessImpactAnalysisId",
                        column: x => x.BusinessImpactAnalysisId,
                        principalSchema: "BCP",
                        principalTable: "BusinessImpactAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessImpactAnalysisDocument",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessImpactAnalysisId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImpactAnalysisDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisDocument_BusinessImpactAnalysis_BusinessImpactAnalysisId",
                        column: x => x.BusinessImpactAnalysisId,
                        principalSchema: "BCP",
                        principalTable: "BusinessImpactAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessImpactAnalysisStaff",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessImpactAnalysisId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImpactAnalysisStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisStaff_BusinessImpactAnalysis_BusinessImpactAnalysisId",
                        column: x => x.BusinessImpactAnalysisId,
                        principalSchema: "BCP",
                        principalTable: "BusinessImpactAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackupPeriod_Code",
                schema: "BCP",
                table: "BackupPeriod",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BCP_BusinessContinuityPlanId",
                schema: "BusinessContinuityPlanService",
                table: "BCP",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BCP_BusinessContinuityPlanId",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BCP_StaffId",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlan_BusinessContinuityStrategyId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "BusinessContinuityStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlan_Code",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlan_ExecutiveResponsibleId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "ExecutiveResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlan_PlanOwnerId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "PlanOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlan_RecoveryDeputyId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "RecoveryDeputyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlan_RecoveryManagerId",
                schema: "BCP",
                table: "BusinessContinuityPlan",
                column: "RecoveryManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanCriticalActivity_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanCriticalActivity",
                column: "BusinessContinuityPlanId");

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
                name: "IX_BusinessContinuityPlanGeneralAssumption_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanGeneralAssumption",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanGeneralAssumption_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanGeneralAssumption",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanRecoveryCriteria_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRecoveryCriteria",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanRecoveryCriteria_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanRecoveryCriteria",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanRecoveryOption_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRecoveryOption",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanRecoveryOption_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanRecoveryOption",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanRisk_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategy_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyDocument_BusinessContinuityStategyId",
                schema: "BCP",
                table: "BusinessContinuityStrategyDocument",
                column: "BusinessContinuityStategyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyDocument_DocumentId",
                schema: "BCP",
                table: "BusinessContinuityStrategyDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyObjective_BusinessContinuityStategyId",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective",
                column: "BusinessContinuityStategyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyObjective_Code",
                schema: "BCP",
                table: "BusinessContinuityStrategyObjective",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyRisk_BusinessContinuityStategyId",
                schema: "BCP",
                table: "BusinessContinuityStrategyRisk",
                column: "BusinessContinuityStategyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyService_BusinessContinuityStategyId",
                schema: "BCP",
                table: "BusinessContinuityStrategyService",
                column: "BusinessContinuityStategyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyStaff_BusinessContinuityStategyId",
                schema: "BCP",
                table: "BusinessContinuityStrategyStaff",
                column: "BusinessContinuityStategyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyStaff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityStrategyStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_BackupPeriodId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "BackupPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_Code",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_ConsequenceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ConsequenceId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_ImportanceDegreeId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ImportanceDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_MaximumAcceptableOutageId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "MaximumAcceptableOutageId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ServicePriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisAnnouncement_BusinessImpactAnalysisId",
                schema: "BCP",
                table: "BusinessImpactAnalysisAnnouncement",
                column: "BusinessImpactAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisAnnouncement_StaffId",
                schema: "BCP",
                table: "BusinessImpactAnalysisAnnouncement",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisAsset_BusinessImpactAnalysisId",
                schema: "BCP",
                table: "BusinessImpactAnalysisAsset",
                column: "BusinessImpactAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisCriticalActivity_BusinessImpactAnalysisId",
                schema: "BCP",
                table: "BusinessImpactAnalysisCriticalActivity",
                column: "BusinessImpactAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDocument_BusinessImpactAnalysisId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDocument",
                column: "BusinessImpactAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDocument_DocumentId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisStaff_BusinessImpactAnalysisId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff",
                column: "BusinessImpactAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisStaff_StaffId",
                schema: "BCP",
                table: "BusinessImpactAnalysisStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Consequence_Code",
                schema: "BCP",
                table: "Consequence",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EffectedAsset_RiskId",
                schema: "RiskManagement",
                table: "EffectedAsset",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralControlAction_RiskId",
                schema: "RiskManagement",
                table: "GeneralControlAction",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_ImpactScale_Code",
                schema: "RiskManagement",
                table: "ImpactScale",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportanceDegree_Code",
                schema: "BCP",
                table: "ImportanceDegree",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MaximumAcceptableOutage_Code",
                schema: "BCP",
                table: "MaximumAcceptableOutage",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NextPossibleAction_RiskId",
                schema: "RiskManagement",
                table: "NextPossibleAction",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_Code",
                schema: "RiskManagement",
                table: "Risk",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Risk_RiskImpactId",
                schema: "RiskManagement",
                table: "Risk",
                column: "RiskImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_RiskPossibilityId",
                schema: "RiskManagement",
                table: "Risk",
                column: "RiskPossibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_RiskTypeId",
                schema: "RiskManagement",
                table: "Risk",
                column: "RiskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteria_Code",
                schema: "RiskManagement",
                table: "RiskCriteria",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteria_RiskDegreeId",
                schema: "RiskManagement",
                table: "RiskCriteria",
                column: "RiskDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteria_RiskImpactId",
                schema: "RiskManagement",
                table: "RiskCriteria",
                column: "RiskImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteria_RiskPossibilityId",
                schema: "RiskManagement",
                table: "RiskCriteria",
                column: "RiskPossibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskDegree_Code",
                schema: "RiskManagement",
                table: "RiskDegree",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskImpact_Code",
                schema: "RiskManagement",
                table: "RiskImpact",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevel_Code",
                schema: "RiskManagement",
                table: "RiskLevel",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevelMeasure_Code",
                schema: "RiskManagement",
                table: "RiskLevelMeasure",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevelMeasure_RiskImpactId",
                schema: "RiskManagement",
                table: "RiskLevelMeasure",
                column: "RiskImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevelMeasure_RiskLevelId",
                schema: "RiskManagement",
                table: "RiskLevelMeasure",
                column: "RiskLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskLevelMeasure_RiskPossibilityId",
                schema: "RiskManagement",
                table: "RiskLevelMeasure",
                column: "RiskPossibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskPossibility_Code",
                schema: "RiskManagement",
                table: "RiskPossibility",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskRelatedIssue_IssueId",
                schema: "RiskManagement",
                table: "RiskRelatedIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskRelatedIssue_RiskId",
                schema: "RiskManagement",
                table: "RiskRelatedIssue",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskType_Code",
                schema: "RiskManagement",
                table: "RiskType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicePriority_Code",
                schema: "BCP",
                table: "ServicePriority",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BCP",
                schema: "BusinessContinuityPlanService");

            migrationBuilder.DropTable(
                name: "BCP",
                schema: "BusinessContinuityPlanStaff");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanCriticalActivity",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanDetailPlanningAssumption",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanGeneralAssumption",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanRecoveryCriteria",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanRecoveryOption",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanRisk",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityStrategyDocument",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityStrategyObjective",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityStrategyRisk",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityStrategyService",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessContinuityStrategyStaff",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessImpactAnalysisAnnouncement",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessImpactAnalysisAsset",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessImpactAnalysisCriticalActivity",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessImpactAnalysisDocument",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessImpactAnalysisStaff",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "EffectedAsset",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "GeneralControlAction",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "ImpactScale",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "NextPossibleAction",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "RiskCriteria",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "RiskLevelMeasure",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "RiskRelatedIssue",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlan",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessImpactAnalysis",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "RiskDegree",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "RiskLevel",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "Risk",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "BusinessContinuityStrategy",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BackupPeriod",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "Consequence",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "ImportanceDegree",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "MaximumAcceptableOutage",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "ServicePriority",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "RiskImpact",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "RiskPossibility",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "RiskType",
                schema: "RiskManagement");

            migrationBuilder.DropColumn(
                name: "ConfirmCode",
                schema: "Authentication",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConfirmCodeSendDate",
                schema: "Authentication",
                table: "Users");
        }
    }
}
