using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ServiceCatalogV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivityAsset_Asset_AssetId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivityAsset_CriticalActivity_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Profile_ProfileId1",
                schema: "Organization",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_ProfileId1",
                schema: "Organization",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_CriticalActivity_Code",
                schema: "ServiceCatalog",
                table: "CriticalActivity");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                schema: "Organization",
                table: "Staff");

            migrationBuilder.RenameColumn(
                name: "WeekyDay",
                schema: "ServiceCatalog",
                table: "CriticalActivityExecutionPlan",
                newName: "WeekDay");

            migrationBuilder.AddColumn<int>(
                name: "PersonLimitation",
                schema: "Organization",
                table: "Position",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ResponsilbeTypeId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<long>(
                name: "IssueId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CriticalActivityConfigurationItem",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CriticalActivityId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalActivityConfigurationItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriticalActivityConfigurationItem_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CriticalActivityConfigurationItem_CriticalActivity_CriticalActivityId",
                        column: x => x.CriticalActivityId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "CriticalActivity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityAssignStaff_ResponsilbeTypeId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                column: "ResponsilbeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivity_Code",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivity_IssueId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityConfigurationItem_ConfigurationItemId",
                schema: "ServiceCatalog",
                table: "CriticalActivityConfigurationItem",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityConfigurationItem_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityConfigurationItem",
                column: "CriticalActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivity_Issue_IssueId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "IssueId",
                principalSchema: "IssueManagement",
                principalTable: "Issue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivityAsset_Asset_AssetId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                column: "AssetId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "Asset",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivityAsset_CriticalActivity_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                column: "CriticalActivityId",
                principalSchema: "ServiceCatalog",
                principalTable: "CriticalActivity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivityAssignStaff_ResponsibleType_ResponsilbeTypeId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                column: "ResponsilbeTypeId",
                principalSchema: "Basic",
                principalTable: "ResponsibleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivity_Issue_IssueId",
                schema: "ServiceCatalog",
                table: "CriticalActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivityAsset_Asset_AssetId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivityAsset_CriticalActivity_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivityAssignStaff_ResponsibleType_ResponsilbeTypeId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff");

            migrationBuilder.DropTable(
                name: "CriticalActivityConfigurationItem",
                schema: "ServiceCatalog");

            migrationBuilder.DropIndex(
                name: "IX_CriticalActivityAssignStaff_ResponsilbeTypeId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff");

            migrationBuilder.DropIndex(
                name: "IX_CriticalActivity_Code",
                schema: "ServiceCatalog",
                table: "CriticalActivity");

            migrationBuilder.DropIndex(
                name: "IX_CriticalActivity_IssueId",
                schema: "ServiceCatalog",
                table: "CriticalActivity");

            migrationBuilder.DropColumn(
                name: "PersonLimitation",
                schema: "Organization",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "ResponsilbeTypeId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff");

            migrationBuilder.DropColumn(
                name: "IssueId",
                schema: "ServiceCatalog",
                table: "CriticalActivity");

            migrationBuilder.RenameColumn(
                name: "WeekDay",
                schema: "ServiceCatalog",
                table: "CriticalActivityExecutionPlan",
                newName: "WeekyDay");

            migrationBuilder.AddColumn<long>(
                name: "ProfileId1",
                schema: "Organization",
                table: "Staff",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
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
                table: "CriticalActivity",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ProfileId1",
                schema: "Organization",
                table: "Staff",
                column: "ProfileId1");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivity_Code",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "Code",
                unique: true);

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
                name: "FK_CriticalActivityAsset_CriticalActivity_CriticalActivityId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAsset",
                column: "CriticalActivityId",
                principalSchema: "ServiceCatalog",
                principalTable: "CriticalActivity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Profile_ProfileId1",
                schema: "Organization",
                table: "Staff",
                column: "ProfileId1",
                principalSchema: "Authentication",
                principalTable: "Profile",
                principalColumn: "Id");
        }
    }
}
