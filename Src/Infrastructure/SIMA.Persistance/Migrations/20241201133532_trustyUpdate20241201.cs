using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20241201 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BrokerId",
                schema: "TrustyDraft",
                table: "Resource",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Resource_BrokerId",
                schema: "TrustyDraft",
                table: "Resource",
                column: "BrokerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Broker_BrokerId",
                schema: "TrustyDraft",
                table: "Resource",
                column: "BrokerId",
                principalSchema: "Bank",
                principalTable: "Broker",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Broker_BrokerId",
                schema: "TrustyDraft",
                table: "Resource");

            migrationBuilder.DropIndex(
                name: "IX_Resource_BrokerId",
                schema: "TrustyDraft",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                schema: "TrustyDraft",
                table: "Resource");
        }
    }
}
