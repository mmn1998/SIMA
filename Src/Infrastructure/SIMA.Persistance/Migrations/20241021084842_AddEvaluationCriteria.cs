using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddEvaluationCriteria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationCriteria",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RiskDegreeId = table.Column<long>(type: "bigint", nullable: false),
                    RiskPossibilityId = table.Column<long>(type: "bigint", nullable: false),
                    RiskImpactId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationCriteria_RiskDegree_RiskDegreeId",
                        column: x => x.RiskDegreeId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskDegree",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluationCriteria_RiskImpact_RiskImpactId",
                        column: x => x.RiskImpactId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskImpact",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluationCriteria_RiskPossibility_RiskPossibilityId",
                        column: x => x.RiskPossibilityId,
                        principalSchema: "RiskManagement",
                        principalTable: "RiskPossibility",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_RiskDegreeId",
                schema: "RiskManagement",
                table: "EvaluationCriteria",
                column: "RiskDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_RiskImpactId",
                schema: "RiskManagement",
                table: "EvaluationCriteria",
                column: "RiskImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_RiskPossibilityId",
                schema: "RiskManagement",
                table: "EvaluationCriteria",
                column: "RiskPossibilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationCriteria",
                schema: "RiskManagement");
        }
    }
}
