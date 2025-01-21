using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addResponsibletypeForServiceAssignStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ResponsibleTypeId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAssignedStaff_ResponsibleTypeId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                column: "ResponsibleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAssignedStaff_ResponsibleType_ResponsibleTypeId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff",
                column: "ResponsibleTypeId",
                principalSchema: "Basic",
                principalTable: "ResponsibleType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAssignedStaff_ResponsibleType_ResponsibleTypeId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAssignedStaff_ResponsibleTypeId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff");

            migrationBuilder.DropColumn(
                name: "ResponsibleTypeId",
                schema: "ServiceCatalog",
                table: "ServiceAssignedStaff");
        }
    }
}
