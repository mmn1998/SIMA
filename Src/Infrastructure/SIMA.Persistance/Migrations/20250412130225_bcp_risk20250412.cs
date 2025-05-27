using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class bcp_risk20250412 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobitScenario_CobitCategory_CobitScenarioCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_RiskType",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskType",
                schema: "RiskManagement",
                table: "RiskType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CobitCategory",
                schema: "RiskManagement",
                table: "CobitCategory");

            migrationBuilder.DropColumn(
                name: "CobitIdentifier",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.RenameTable(
                name: "RiskType",
                schema: "RiskManagement",
                newName: "RiskCategory",
                newSchema: "RiskManagement");

            migrationBuilder.RenameTable(
                name: "CobitCategory",
                schema: "RiskManagement",
                newName: "CobitRiskCategory",
                newSchema: "RiskManagement");

            migrationBuilder.RenameColumn(
                name: "RiskTypeId",
                schema: "RiskManagement",
                table: "Risk",
                newName: "RiskCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Risk_RiskTypeId",
                schema: "RiskManagement",
                table: "Risk",
                newName: "IX_Risk_RiskCategoryId");

            migrationBuilder.RenameColumn(
                name: "CobitScenarioCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario",
                newName: "CobitRiskCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CobitScenario_CobitScenarioCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario",
                newName: "IX_CobitScenario_CobitRiskCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskType_Code",
                schema: "RiskManagement",
                table: "RiskCategory",
                newName: "IX_RiskCategory_Code");

            migrationBuilder.RenameIndex(
                name: "IX_CobitCategory_Code",
                schema: "RiskManagement",
                table: "CobitRiskCategory",
                newName: "IX_CobitRiskCategory_Code");

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

            migrationBuilder.AlterColumn<long>(
                name: "ScenarioId",
                schema: "RiskManagement",
                table: "CobitScenario",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskCategory",
                schema: "RiskManagement",
                table: "RiskCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CobitRiskCategory",
                schema: "RiskManagement",
                table: "CobitRiskCategory",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ConsequenceIntensionDescription",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConsequenceIntensionId = table.Column<long>(type: "bigint", nullable: false),
                    ConsequenceId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsequenceIntensionDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsequenceIntensionDescription_ConsequenceIntension_ConsequenceIntensionId",
                        column: x => x.ConsequenceIntensionId,
                        principalSchema: "BCP",
                        principalTable: "ConsequenceIntension",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConsequenceIntensionDescription_Consequence_ConsequenceId",
                        column: x => x.ConsequenceId,
                        principalSchema: "BCP",
                        principalTable: "Consequence",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ConsequenceIntensionDescription_ConsequenceId",
                schema: "BCP",
                table: "ConsequenceIntensionDescription",
                column: "ConsequenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsequenceIntensionDescription_ConsequenceIntensionId",
                schema: "BCP",
                table: "ConsequenceIntensionDescription",
                column: "ConsequenceIntensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CobitScenario_CobitRiskCategory_CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario",
                column: "CobitRiskCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "CobitRiskCategory",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_RiskCategory_RiskCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                column: "RiskCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "RiskCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobitScenario_CobitRiskCategory_CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_CobitRiskCategory_CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_CobitScenario_CobitScenarioId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Risk_RiskCategory_RiskCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropTable(
                name: "ConsequenceIntensionDescription",
                schema: "BCP");

            migrationBuilder.DropIndex(
                name: "IX_Risk_CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Risk_CobitScenarioId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskCategory",
                schema: "RiskManagement",
                table: "RiskCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CobitRiskCategory",
                schema: "RiskManagement",
                table: "CobitRiskCategory");

            migrationBuilder.DropColumn(
                name: "CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.DropColumn(
                name: "CobitScenarioId",
                schema: "RiskManagement",
                table: "Risk");

            migrationBuilder.RenameTable(
                name: "RiskCategory",
                schema: "RiskManagement",
                newName: "RiskType",
                newSchema: "RiskManagement");

            migrationBuilder.RenameTable(
                name: "CobitRiskCategory",
                schema: "RiskManagement",
                newName: "CobitCategory",
                newSchema: "RiskManagement");

            migrationBuilder.RenameColumn(
                name: "RiskCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                newName: "RiskTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Risk_RiskCategoryId",
                schema: "RiskManagement",
                table: "Risk",
                newName: "IX_Risk_RiskTypeId");

            migrationBuilder.RenameColumn(
                name: "CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario",
                newName: "CobitScenarioCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CobitScenario_CobitRiskCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario",
                newName: "IX_CobitScenario_CobitScenarioCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskCategory_Code",
                schema: "RiskManagement",
                table: "RiskType",
                newName: "IX_RiskType_Code");

            migrationBuilder.RenameIndex(
                name: "IX_CobitRiskCategory_Code",
                schema: "RiskManagement",
                table: "CobitCategory",
                newName: "IX_CobitCategory_Code");

            migrationBuilder.AlterColumn<long>(
                name: "ScenarioId",
                schema: "RiskManagement",
                table: "CobitScenario",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CobitIdentifier",
                schema: "RiskManagement",
                table: "CobitScenario",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskType",
                schema: "RiskManagement",
                table: "RiskType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CobitCategory",
                schema: "RiskManagement",
                table: "CobitCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CobitScenario_CobitCategory_CobitScenarioCategoryId",
                schema: "RiskManagement",
                table: "CobitScenario",
                column: "CobitScenarioCategoryId",
                principalSchema: "RiskManagement",
                principalTable: "CobitCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_RiskType",
                schema: "RiskManagement",
                table: "Risk",
                column: "RiskTypeId",
                principalSchema: "RiskManagement",
                principalTable: "RiskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
