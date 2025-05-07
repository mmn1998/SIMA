using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeManagerIdForStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Profile_manager",
                schema: "Organization",
                table: "Staff");

            migrationBuilder.AddColumn<long>(
                name: "ProfileId1",
                schema: "Organization",
                table: "Staff",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ProfileId1",
                schema: "Organization",
                table: "Staff",
                column: "ProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Profile_ProfileId1",
                schema: "Organization",
                table: "Staff",
                column: "ProfileId1",
                principalSchema: "Authentication",
                principalTable: "Profile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Staff_ManagerId",
                schema: "Organization",
                table: "Staff",
                column: "ManagerId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Profile_ProfileId1",
                schema: "Organization",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Staff_ManagerId",
                schema: "Organization",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_ProfileId1",
                schema: "Organization",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                schema: "Organization",
                table: "Staff");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Profile_manager",
                schema: "Organization",
                table: "Staff",
                column: "ManagerId",
                principalSchema: "Authentication",
                principalTable: "Profile",
                principalColumn: "Id");
        }
    }
}
