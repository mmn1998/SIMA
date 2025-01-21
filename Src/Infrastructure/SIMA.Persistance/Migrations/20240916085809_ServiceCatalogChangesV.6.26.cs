using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ServiceCatalogChangesV626 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceBoundle_ServiceBoundleId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropTable(
                name: "ServiceBoundle",
                schema: "ServiceCatalog");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCategory_Code",
                schema: "ServiceCatalog",
                table: "ServiceCategory");

            migrationBuilder.RenameColumn(
                name: "ServiceBoundleId",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "ServicePriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_ServiceBoundleId",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "IX_Service_ServicePriorityId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsInternalService",
                schema: "ServiceCatalog",
                table: "Service",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServiceCategoryId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_Code",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_ParentId",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceCategoryId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceCategory_ServiceCategoryId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServiceCategoryId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServiceCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServicePriority_ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServicePriorityId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServicePriority",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCategory_ServiceCategory_ParentId",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                column: "ParentId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServiceCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceCategory_ServiceCategoryId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServicePriority_ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCategory_ServiceCategory_ParentId",
                schema: "ServiceCatalog",
                table: "ServiceCategory");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCategory_Code",
                schema: "ServiceCatalog",
                table: "ServiceCategory");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCategory_ParentId",
                schema: "ServiceCatalog",
                table: "ServiceCategory");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServiceCategoryId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "ServiceCatalog",
                table: "ServiceCategory");

            migrationBuilder.DropColumn(
                name: "ServiceCategoryId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "ServiceBoundleId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_ServicePriorityId",
                schema: "ServiceCatalog",
                table: "Service",
                newName: "IX_Service_ServiceBoundleId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsInternalService",
                schema: "ServiceCatalog",
                table: "Service",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldFixedLength: true,
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceBoundle",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceCategoryId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ServiceBoundle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceBoundle_ServiceCategory_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_Code",
                schema: "ServiceCatalog",
                table: "ServiceCategory",
                column: "Code",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceBoundle_ServiceBoundleId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServiceBoundleId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServiceBoundle",
                principalColumn: "Id");
        }
    }
}
