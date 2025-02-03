using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FUCKINGcobit20250203 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceLevel");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceCategory",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceCategory");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "RiskManagement",
                table: "ConsequenceLevel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
