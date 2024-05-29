using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class changeSchemaBCP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BCP_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BusinessContinuityPlanService",
                table: "BCP");

            migrationBuilder.DropForeignKey(
                name: "FK_BCP_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP");

            migrationBuilder.DropForeignKey(
                name: "FK_BCP_Staff_StaffId",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BCP",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BCP",
                schema: "BusinessContinuityPlanService",
                table: "BCP");

            migrationBuilder.RenameTable(
                name: "BCP",
                schema: "BusinessContinuityPlanStaff",
                newName: "BusinessContinuityPlanStaff",
                newSchema: "BCP");

            migrationBuilder.RenameTable(
                name: "BCP",
                schema: "BusinessContinuityPlanService",
                newName: "BusinessContinuityPlanService",
                newSchema: "BCP");

            migrationBuilder.RenameIndex(
                name: "IX_BCP_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff",
                newName: "IX_BusinessContinuityPlanStaff_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_BCP_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff",
                newName: "IX_BusinessContinuityPlanStaff_BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BCP_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                newName: "IX_BusinessContinuityPlanService_BusinessContinuityPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessContinuityPlanStaff",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessContinuityPlanService",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanService_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanStaff_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanStaff_Staff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff",
                column: "StaffId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanService_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanService");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanStaff_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanStaff_Staff_StaffId",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessContinuityPlanStaff",
                schema: "BCP",
                table: "BusinessContinuityPlanStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessContinuityPlanService",
                schema: "BCP",
                table: "BusinessContinuityPlanService");

            migrationBuilder.EnsureSchema(
                name: "BusinessContinuityPlanService");

            migrationBuilder.EnsureSchema(
                name: "BusinessContinuityPlanStaff");

            migrationBuilder.RenameTable(
                name: "BusinessContinuityPlanStaff",
                schema: "BCP",
                newName: "BCP",
                newSchema: "BusinessContinuityPlanStaff");

            migrationBuilder.RenameTable(
                name: "BusinessContinuityPlanService",
                schema: "BCP",
                newName: "BCP",
                newSchema: "BusinessContinuityPlanService");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanStaff_StaffId",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP",
                newName: "IX_BCP_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanStaff_BusinessContinuityPlanId",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP",
                newName: "IX_BCP_BusinessContinuityPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessContinuityPlanService_BusinessContinuityPlanId",
                schema: "BusinessContinuityPlanService",
                table: "BCP",
                newName: "IX_BCP_BusinessContinuityPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BCP",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BCP",
                schema: "BusinessContinuityPlanService",
                table: "BCP",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BCP_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BusinessContinuityPlanService",
                table: "BCP",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BCP_BusinessContinuityPlan_BusinessContinuityPlanId",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP",
                column: "BusinessContinuityPlanId",
                principalSchema: "BCP",
                principalTable: "BusinessContinuityPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BCP_Staff_StaffId",
                schema: "BusinessContinuityPlanStaff",
                table: "BCP",
                column: "StaffId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
