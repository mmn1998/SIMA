using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AccessManagement3627 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AccessManagement");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsCategory_Code",
                schema: "Logistics",
                table: "GoodsCategory",
                newName: "Logistics.GoodsCategory.IX_GoodsCategory_Code1");

            migrationBuilder.AddColumn<long>(
                name: "AccessTypeId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccessTypeId",
                schema: "ServiceCatalog",
                table: "Api",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessType",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessRequest",
                schema: "AccessManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IpSourceFrom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IpSourceTo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IpDestinationFrom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IpDestinationTo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PortDestinationFrom = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    PortDestinationTo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    NetworkProtocolId = table.Column<long>(type: "bigint", nullable: false),
                    AccessTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AccessDurationStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    AccessDurationEndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasAutoRenew = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRequest_AccessType_AccessTypeId",
                        column: x => x.AccessTypeId,
                        principalSchema: "Basic",
                        principalTable: "AccessType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessRequest_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessRequest_NetworkProtocol_NetworkProtocolId",
                        column: x => x.NetworkProtocolId,
                        principalSchema: "Basic",
                        principalTable: "NetworkProtocol",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccessRequestDocument",
                schema: "AccessManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    AccessRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    AccessRequestDocumentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRequestDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRequestDocument_AccessRequestDocument_AccessRequestDocumentId",
                        column: x => x.AccessRequestDocumentId,
                        principalSchema: "AccessManagement",
                        principalTable: "AccessRequestDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessRequestDocument_AccessRequest_AccessRequestId",
                        column: x => x.AccessRequestId,
                        principalSchema: "AccessManagement",
                        principalTable: "AccessRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessRequestDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressStoreProcedureParam_AccessTypeId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                column: "AccessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Api_AccessTypeId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "AccessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_AccessTypeId",
                schema: "AccessManagement",
                table: "AccessRequest",
                column: "AccessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_IssueId",
                schema: "AccessManagement",
                table: "AccessRequest",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_NetworkProtocolId",
                schema: "AccessManagement",
                table: "AccessRequest",
                column: "NetworkProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequestDocument_AccessRequestDocumentId",
                schema: "AccessManagement",
                table: "AccessRequestDocument",
                column: "AccessRequestDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequestDocument_AccessRequestId",
                schema: "AccessManagement",
                table: "AccessRequestDocument",
                column: "AccessRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequestDocument_DocumentId",
                schema: "AccessManagement",
                table: "AccessRequestDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessType_Code",
                schema: "Basic",
                table: "AccessType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Api_AccessType_AccessTypeId",
                schema: "ServiceCatalog",
                table: "Api",
                column: "AccessTypeId",
                principalSchema: "Basic",
                principalTable: "AccessType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressStoreProcedureParam_AccessType_AccessTypeId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                column: "AccessTypeId",
                principalSchema: "Basic",
                principalTable: "AccessType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Api_AccessType_AccessTypeId",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressStoreProcedureParam_AccessType_AccessTypeId",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropTable(
                name: "AccessRequestDocument",
                schema: "AccessManagement");

            migrationBuilder.DropTable(
                name: "AccessRequest",
                schema: "AccessManagement");

            migrationBuilder.DropTable(
                name: "AccessType",
                schema: "Basic");

            migrationBuilder.DropIndex(
                name: "IX_ProgressStoreProcedureParam_AccessTypeId",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropIndex(
                name: "IX_Api_AccessTypeId",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.DropColumn(
                name: "AccessTypeId",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropColumn(
                name: "AccessTypeId",
                schema: "ServiceCatalog",
                table: "Api");

            migrationBuilder.RenameIndex(
                name: "Logistics.GoodsCategory.IX_GoodsCategory_Code1",
                schema: "Logistics",
                table: "GoodsCategory",
                newName: "IX_GoodsCategory_Code");
        }
    }
}
