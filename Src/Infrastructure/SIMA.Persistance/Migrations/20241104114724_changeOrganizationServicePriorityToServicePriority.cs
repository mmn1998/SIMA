using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class changeOrganizationServicePriorityToServicePriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_ServicePriority_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_ServicePriority_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ServicePriorityId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServicePriority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_ServicePriority_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_ServicePriority_ServicePriorityId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ServicePriorityId",
                principalSchema: "BCP",
                principalTable: "ServicePriority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
