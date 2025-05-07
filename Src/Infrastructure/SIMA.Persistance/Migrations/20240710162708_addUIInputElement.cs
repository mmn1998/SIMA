using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addUIInputElement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Ordering",
                schema: "Logistics",
                table: "SupplierRank",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApiNameForDataBounding",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BoundFormat",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoredProcedureForDataBounding",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UiInputElementId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UIInputElement",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    IsMultiSelect = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    IsSingleSelect = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    HasInputInEachRecord = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIInputElement", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressStoreProcedureParam_UiInputElementId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                column: "UiInputElementId");

            migrationBuilder.CreateIndex(
                name: "IX_UIInputElement_Code",
                schema: "Basic",
                table: "UIInputElement",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressStoreProcedureParam_UIInputElement_UiInputElementId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                column: "UiInputElementId",
                principalSchema: "Basic",
                principalTable: "UIInputElement",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressStoreProcedureParam_UIInputElement_UiInputElementId",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropTable(
                name: "UIInputElement",
                schema: "Basic");

            migrationBuilder.DropIndex(
                name: "IX_ProgressStoreProcedureParam_UiInputElementId",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropColumn(
                name: "ApiNameForDataBounding",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropColumn(
                name: "BoundFormat",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropColumn(
                name: "StoredProcedureForDataBounding",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropColumn(
                name: "UiInputElementId",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.AlterColumn<string>(
                name: "Ordering",
                schema: "Logistics",
                table: "SupplierRank",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
