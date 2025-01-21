using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class changeDatamodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCustomer_ServiceCustomerType_ServiceCustomerTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceUser_ServiceUserType_ServiceUserTypeId",
                schema: "ServiceCatalog",
                table: "ServiceUser");

            migrationBuilder.DropTable(
                name: "ServiceCustomerType",
                schema: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "ServiceUserType",
                schema: "ServiceCatalog");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Authentication",
                table: "UserType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Authentication",
                table: "UserType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "Authentication",
                table: "UserType",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "Basic",
                table: "ResponsibleType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Basic",
                table: "ResponsibleType",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                schema: "Basic",
                table: "ResponsibleType",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerType",
                schema: "Basic",
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
                    table.PrimaryKey("PK_CustomerType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerType_Code",
                schema: "Basic",
                table: "CustomerType",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCustomer_CustomerType_ServiceCustomerTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                column: "ServiceCustomerTypeId",
                principalSchema: "Basic",
                principalTable: "CustomerType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceUser_UserType_ServiceUserTypeId",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                column: "ServiceUserTypeId",
                principalSchema: "Authentication",
                principalTable: "UserType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCustomer_CustomerType_ServiceCustomerTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceUser_UserType_ServiceUserTypeId",
                schema: "ServiceCatalog",
                table: "ServiceUser");

            migrationBuilder.DropTable(
                name: "CustomerType",
                schema: "Basic");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "Basic",
                table: "ResponsibleType");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Authentication",
                table: "UserType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Authentication",
                table: "UserType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<long>(
                name: "ActiveStatusId",
                schema: "Authentication",
                table: "UserType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "Basic",
                table: "ResponsibleType",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Basic",
                table: "ResponsibleType",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.CreateTable(
                name: "ServiceCustomerType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_ServiceCustomerType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCustomerType_ServiceCustomerType_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceCustomerType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceUserType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_ServiceUserType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceUserType_ServiceUserType_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ServiceCatalog",
                        principalTable: "ServiceUserType",
                        principalColumn: "Id");
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCustomer_ServiceCustomerType_ServiceCustomerTypeId",
                schema: "ServiceCatalog",
                table: "ServiceCustomer",
                column: "ServiceCustomerTypeId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServiceCustomerType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceUser_ServiceUserType_ServiceUserTypeId",
                schema: "ServiceCatalog",
                table: "ServiceUser",
                column: "ServiceUserTypeId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServiceUserType",
                principalColumn: "Id");
        }
    }
}
