using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class nonuniqeBlockingNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_BlockingNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_BlockingNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BlockingNumber",
                unique: true,
                filter: "[BlockingNumber] IS NOT NULL");
        }
    }
}
