using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class bcp_risk20250415 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risk_CobitRiskCategory_CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_CobitScenario_CobitScenarioId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_CobitScenarioId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "CobitScenarioId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.CreateTable(
                name: "CobitRiskCategoryScenario",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CobitScenarioId = table.Column<long>(type: "bigint", nullable: false),
                    RiskId = table.Column<long>(type: "bigint", nullable: false),
                    RiskCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobitRiskCategoryScenario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobitRiskCategoryScenario_CobitScenario_CobitScenarioId",
                        column: x => x.CobitScenarioId,
                        principalSchema: "RiskManagement",
                        principalTable: "CobitScenario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CobitRiskCategoryScenario_RiskCategory_RiskCategoryId",
                        column: x => x.RiskCategoryId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CobitRiskCategoryScenario_Risk_RiskId",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CobitRiskCategoryScenario_CobitScenarioId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario",
                column: "CobitScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CobitRiskCategoryScenario_RiskCategoryId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario",
                column: "RiskCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CobitRiskCategoryScenario_RiskId",
                schema: "RiskManagement",
                table: "CobitRiskCategoryScenario",
                column: "RiskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CobitRiskCategoryScenario",
                schema: "RiskManagement");

            migrationBuilder.AddColumn<long>(
                name: "CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CobitScenarioId",
                schema: "RiskManagement",
                table: "Risk",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Risk_CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "CobitRiskCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_CobitScenarioId",
                schema: "RiskManagement",
                table: "Risk",
                column: "CobitScenarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_CobitRiskCategory_CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "CobitRiskCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "CobitRiskCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_CobitScenario_CobitScenarioId",
                schema: "RiskManagement",
                table: "Risk",
                column: "CobitScenarioId",
                principalSchema: "RiskManagement",
                principalTable: "CobitScenario",
                principalColumn: "Id");
        }
    }
}
