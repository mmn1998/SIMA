using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class bcpNew20250119 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_HappeningPossibility_HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropColumn(
                name: "HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin");

            migrationBuilder.DropColumn(
                name: "IsStableStrategy",
                schema: "BCP",
                table: "BusinessContinuityStrategy");

            migrationBuilder.CreateTable(
                name: "RiskStaff",
                schema: "RiskManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    RiskId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskStaff_Risk_RiskId",
                        column: x => x.RiskId,
                        principalSchema: "RiskManagement",
                        principalTable: "Risk",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskStaff_RiskId",
                schema: "RiskManagement",
                table: "RiskStaff",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskStaff_StaffId",
                schema: "RiskManagement",
                table: "RiskStaff",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskStaff",
                schema: "RiskManagement");

            migrationBuilder.AddColumn<long>(
                name: "HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "IsStableStrategy",
                schema: "BCP",
                table: "BusinessContinuityStrategy",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "HappeningPossibilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisDisasterOrigin_HappeningPossibility_HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "HappeningPossibilityId",
                principalSchema: "BCP",
                principalTable: "HappeningPossibility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
