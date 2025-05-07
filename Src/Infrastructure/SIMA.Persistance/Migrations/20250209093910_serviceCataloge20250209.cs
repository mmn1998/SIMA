using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class serviceCataloge20250209 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "ProductResponsible",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "ProductResponsible",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAssignedStaff_BranchId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAssignedStaff_DepartmentId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductResponsible_BranchId",
                schema: "ServiceCatalog",
                table: "ProductResponsible",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductResponsible_DepartmentId",
                schema: "ServiceCatalog",
                table: "ProductResponsible",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityAssignStaff_BranchId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalActivityAssignStaff_DepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelResponsible_BranchId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelResponsible_DepartmentId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelResponsible_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelResponsible_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivityAssignStaff_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivityAssignStaff_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductResponsible_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "ProductResponsible",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductResponsible_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "ProductResponsible",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAssignedStaff_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAssignedStaff_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelResponsible_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_ChannelResponsible_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivityAssignStaff_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalActivityAssignStaff_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductResponsible_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "ProductResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductResponsible_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "ProductResponsible");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAssignedStaff_Branch_BranchId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAssignedStaff_Department_DepartmentId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAssignedStaff_BranchId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAssignedStaff_DepartmentId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff");

            migrationBuilder.DropIndex(
                name: "IX_ProductResponsible_BranchId",
                schema: "ServiceCatalog",
                table: "ProductResponsible");

            migrationBuilder.DropIndex(
                name: "IX_ProductResponsible_DepartmentId",
                schema: "ServiceCatalog",
                table: "ProductResponsible");

            migrationBuilder.DropIndex(
                name: "IX_CriticalActivityAssignStaff_BranchId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff");

            migrationBuilder.DropIndex(
                name: "IX_CriticalActivityAssignStaff_DepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff");

            migrationBuilder.DropIndex(
                name: "IX_ChannelResponsible_BranchId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible");

            migrationBuilder.DropIndex(
                name: "IX_ChannelResponsible_DepartmentId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "ProductResponsible");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "ProductResponsible");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivityAssignStaff");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ServiceCatalog",
                table: "CriticalActivity");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "ServiceCatalog",
                table: "ChannelResponsible");
        }
    }
}
