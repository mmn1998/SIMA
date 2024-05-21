using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addDepartmentIdToBranch20240214 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                schema: "Bank",
                table: "Branch",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_DepartmentId",
                schema: "Bank",
                table: "Branch",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Department_DepartmentId",
                schema: "Bank",
                table: "Branch",
                column: "DepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Department_DepartmentId",
                schema: "Bank",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_DepartmentId",
                schema: "Bank",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "Bank",
                table: "Branch");
        }
    }
}
