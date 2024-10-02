using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddParentIdToDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                schema: "Authentication",
                table: "Domain",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Domain_ParentId",
                schema: "Authentication",
                table: "Domain",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Domain_ParentId",
                schema: "Authentication",
                table: "Domain",
                column: "ParentId",
                principalSchema: "Authentication",
                principalTable: "Domain",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domain_Domain_ParentId",
                schema: "Authentication",
                table: "Domain");

            migrationBuilder.DropIndex(
                name: "IX_Domain_ParentId",
                schema: "Authentication",
                table: "Domain");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "Authentication",
                table: "Domain");
        }
    }
}
