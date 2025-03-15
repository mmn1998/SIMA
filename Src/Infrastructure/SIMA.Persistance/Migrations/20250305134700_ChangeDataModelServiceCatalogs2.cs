using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDataModelServiceCatalogs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceCantract",
                schema: "ServiceCatalog");

            migrationBuilder.RenameColumn(
                name: "ServiceWorkflowBpmn",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "ServiceDataFlowDiagram");

            migrationBuilder.CreateTable(
                name: "OrganizationalProject",
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
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOrganizationalProject",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationalProjectId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOrganizationalProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceOrganizationalProject_OrganizationalProject_OrganizationalProjectId",
                        column: x => x.OrganizationalProjectId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "OrganizationalProject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceOrganizationalProject_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalProject_Code",
                schema: "ServiceCatalog",
                table: "OrganizationalProject",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOrganizationalProject_OrganizationalProjectId",
                schema: "ServiceCatalog",
                table: "ServiceOrganizationalProject",
                column: "OrganizationalProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOrganizationalProject_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceOrganizationalProject",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceOrganizationalProject",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "OrganizationalProject",
                schema: "ServiceCatalog");

            migrationBuilder.RenameColumn(
                name: "ServiceDataFlowDiagram",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "ServiceWorkflowBpmn");

            migrationBuilder.CreateTable(
                name: "ServiceCantract",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCantract_ServiceId",
                schema: "ServiceCatalog",
                table: "ServiceCantract",
                column: "ServiceId");
        }
    }
}
