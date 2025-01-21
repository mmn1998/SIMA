using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ApiMethodActionChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApiMethodAction_Code",
                schema: "Basic",
                table: "ApiMethodAction");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Basic",
                table: "ApiMethodAction",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Basic",
                table: "ApiMethodAction",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<long>(
                name: "ActiveStatusId",
                schema: "Basic",
                table: "ApiMethodAction",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Basic",
                table: "ApiMethodAction",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                schema: "Basic",
                table: "ApiMethodAction",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ModifiedAt",
                schema: "Basic",
                table: "ApiMethodAction",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                schema: "Basic",
                table: "ApiMethodAction",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiMethodAction_Code",
                schema: "Basic",
                table: "ApiMethodAction",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApiMethodAction_Code",
                schema: "Basic",
                table: "ApiMethodAction");

            migrationBuilder.DropColumn(
                name: "ActiveStatusId",
                schema: "Basic",
                table: "ApiMethodAction");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Basic",
                table: "ApiMethodAction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Basic",
                table: "ApiMethodAction");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "Basic",
                table: "ApiMethodAction");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "Basic",
                table: "ApiMethodAction");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Basic",
                table: "ApiMethodAction",
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
                schema: "Basic",
                table: "ApiMethodAction",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiMethodAction_Code",
                schema: "Basic",
                table: "ApiMethodAction",
                column: "Code",
                unique: true);
        }
    }
}
