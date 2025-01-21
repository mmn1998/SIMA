using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateRiskTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risk_RiskImpact",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_RiskPossibility",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropTable(
                name: "GeneralControlAction",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "NextPossibleAction",
                schema: "RiskManagement");

            migrationBuilder.DropIndex(
                name: "IX_Risk_RiskImpactId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_RiskPossibilityId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "ALE",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "ARO",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "AV",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "EF",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "RiskImpactId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "RiskPossibilityId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "SLE",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.AlterColumn<string>(
                name: "User_SecretKey",
                schema: "Authentication",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecretKey",
                schema: "Authentication",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<float>(
                name: "ALE",
                schema: "RiskManagement",
                table: "EffectedAsset",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ARO",
                schema: "RiskManagement",
                table: "EffectedAsset",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AV",
                schema: "RiskManagement",
                table: "EffectedAsset",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "EF",
                schema: "RiskManagement",
                table: "EffectedAsset",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "SLE",
                schema: "RiskManagement",
                table: "EffectedAsset",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "CorrectiveAction",
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
                    table.PrimaryKey("PK_CorrectiveAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorrectiveAction_Risk",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreventiveAction",
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
                    table.PrimaryKey("PK_PreventiveAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreventiveAction_Risk",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRiskImpact",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ImpactScaleId = table.Column<long>(type: "bigint", nullable: false),
                    RiskImpactId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRiskImpact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRiskImpact_ImpactScale",
                        column: x => x.ImpactScaleId,
                        principalSchema: "RiskManagement",
                        principalTable: "ImpactScale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRiskImpact_ServiceRiskImpact",
                        column: x => x.RiskImpactId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskImpact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreatType",
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
                    table.PrimaryKey("PK_ThreatType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vulnerability",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    EffectedAssetId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vulnerability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vulnerabilities_EffectedAsset",
                        column: x => x.EffectedAssetId,
                        principalSchema: "RiskManagement",
                        principalTable: "EffectedAsset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Threat",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskId = table.Column<long>(type: "bigint", nullable: false),
                    RiskPossibilityId = table.Column<long>(type: "bigint", nullable: false),
                    ThreatTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Threat_Risk",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Threat_RiskPossibility",
                        column: x => x.RiskPossibilityId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskPossibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Threat_ThreatType",
                        column: x => x.ThreatTypeId,
                        principalSchema: "RiskManagement",
                        principalTable: "ThreatType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveAction_RiskId",
                schema: "RiskManagement",
                table: "CorrectiveAction",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_PreventiveAction_RiskId",
                schema: "RiskManagement",
                table: "PreventiveAction",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRiskImpact_ImpactScaleId",
                schema: "RiskManagement",
                table: "ServiceRiskImpact",
                column: "ImpactScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRiskImpact_RiskImpactId",
                schema: "RiskManagement",
                table: "ServiceRiskImpact",
                column: "RiskImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_Threat_RiskId",
                schema: "RiskManagement",
                table: "Threat",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_Threat_RiskPossibilityId",
                schema: "RiskManagement",
                table: "Threat",
                column: "RiskPossibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Threat_ThreatTypeId",
                schema: "RiskManagement",
                table: "Threat",
                column: "ThreatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreatType_Code",
                schema: "RiskManagement",
                table: "ThreatType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vulnerability_EffectedAssetId",
                schema: "RiskManagement",
                table: "Vulnerability",
                column: "EffectedAssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectiveAction",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "PreventiveAction",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "ServiceRiskImpact",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "Threat",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "Vulnerability",
                schema: "RiskManagement");

            migrationBuilder.DropTable(
                name: "ThreatType",
                schema: "RiskManagement");

            migrationBuilder.DropColumn(
                name: "ALE",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.DropColumn(
                name: "ARO",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.DropColumn(
                name: "AV",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.DropColumn(
                name: "EF",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.DropColumn(
                name: "SLE",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.AlterColumn<string>(
                name: "User_SecretKey",
                schema: "Authentication",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecretKey",
                schema: "Authentication",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<float>(
                name: "ALE",
                schema: "RiskManagement",
                table: "Risk",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ARO",
                schema: "RiskManagement",
                table: "Risk",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AV",
                schema: "RiskManagement",
                table: "Risk",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "EF",
                schema: "RiskManagement",
                table: "Risk",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<long>(
                name: "RiskImpactId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RiskPossibilityId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<float>(
                name: "SLE",
                schema: "RiskManagement",
                table: "Risk",
                type: "real",
                nullable: false,
                defaultValue: 0f);

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
                name: "IX_GeneralControlAction_RiskId",
                schema: "RiskManagement",
                table: "GeneralControlAction",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_NextPossibleAction_RiskId",
                schema: "RiskManagement",
                table: "NextPossibleAction",
                column: "RiskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_RiskImpact",
                schema: "RiskManagement",
                table: "Risk",
                column: "RiskImpactId",
                principalSchema: "RiskManagement",
                principalTable: "RiskImpact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_RiskPossibility",
                schema: "RiskManagement",
                table: "Risk",
                column: "RiskPossibilityId",
                principalSchema: "RiskManagement",
                principalTable: "RiskPossibility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
