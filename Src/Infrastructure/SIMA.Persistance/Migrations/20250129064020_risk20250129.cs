using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class risk20250129 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobitScenario_CobitScenarioCategory_CobitScenarioCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.DropTable(
                name: "CobitScenarioCategory",
                schema: "RiskManagement");

            migrationBuilder.AddColumn<float>(
                name: "NumericValue",
                schema: "RiskManagement",
                table: "RiskValue",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "CobitCategory",
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
                    table.PrimaryKey("PK_CobitCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CobitCategory_Code",
                schema: "RiskManagement",
                table: "CobitCategory",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CobitScenario_CobitCategory_CobitScenarioCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario",
                column: "CobitScenarioCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "CobitCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobitScenario_CobitCategory_CobitScenarioCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.DropTable(
                name: "CobitCategory",
                schema: "RiskManagement");

            migrationBuilder.DropColumn(
                name: "NumericValue",
                schema: "RiskManagement",
                table: "RiskValue");

            migrationBuilder.CreateTable(
                name: "CobitScenarioCategory",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobitScenarioCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CobitScenarioCategory_Code",
                schema: "RiskManagement",
                table: "CobitScenarioCategory",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CobitScenario_CobitScenarioCategory_CobitScenarioCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario",
                column: "CobitScenarioCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "CobitScenarioCategory",
                principalColumn: "Id");
        }
    }
}
