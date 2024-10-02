using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class serviceCatalogChangesV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCategory_ServiceType_ServiceTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceChanel_ChannelType_ChannelTypeId",
                schema: "ServiceCatalog",
                table: "ServiceChanel");

            migrationBuilder.DropTable(
                name: "ChannelType",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceType",
                schema: "ServiceCatalog");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCategory_ServiceTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCategory");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCategory");

            migrationBuilder.RenameColumn(
                name: "ChannelTypeId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                newName: "ChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceChanel_ChannelTypeId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                newName: "IX_ServiceChanel_ChannelId");

            migrationBuilder.RenameColumn(
                name: "Suggestion",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "ServiceWorkflowBpmn");

            migrationBuilder.RenameColumn(
                name: "ServiceWorkflowDescription",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "IsInternalService");

            migrationBuilder.AlterColumn<decimal>(
                name: "ServiceCost",
                schema: "ServiceCatalog",
                table: "Service",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_ParentId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Service_ParentId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ParentId",
                principalSchema: "ServiceCatalog",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceChanel_Channel_ChannelId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                column: "ChannelId",
                principalSchema: "ServiceCatalog",
                principalTable: "Channel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Service_ParentId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceChanel_Channel_ChannelId",
                schema: "ServiceCatalog",
                table: "ServiceChanel");

            migrationBuilder.DropIndex(
                name: "IX_Service_ParentId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                newName: "ChannelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceChanel_ChannelId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                newName: "IX_ServiceChanel_ChannelTypeId");

            migrationBuilder.RenameColumn(
                name: "ServiceWorkflowBpmn",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "Suggestion");

            migrationBuilder.RenameColumn(
                name: "IsInternalService",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "ServiceWorkflowDescription");

            migrationBuilder.AddColumn<long>(
                name: "ServiceTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<double>(
                name: "ServiceCost",
                schema: "ServiceCatalog",
                table: "Service",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ChannelType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_ServiceTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelType_Code",
                schema: "ServiceCatalog",
                table: "ChannelType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_Code",
                schema: "ServiceCatalog",
                table: "ServiceType",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCategory_ServiceType_ServiceTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                column: "ServiceTypeId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServiceType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceChanel_ChannelType_ChannelTypeId",
                schema: "ServiceCatalog",
                table: "ServiceChanel",
                column: "ChannelTypeId",
                principalSchema: "ServiceCatalog",
                principalTable: "ChannelType",
                principalColumn: "Id");
        }
    }
}
