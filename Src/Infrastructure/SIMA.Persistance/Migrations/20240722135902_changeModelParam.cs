using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class changeModelParam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextBoundName",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValueBoundName",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextBoundName",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropColumn(
                name: "ValueBoundName",
                schema: "Project",
                table: "ProgressStoreProcedureParam");
        }
    }
}
