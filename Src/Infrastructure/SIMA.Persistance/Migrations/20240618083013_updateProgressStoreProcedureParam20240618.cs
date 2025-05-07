using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateProgressStoreProcedureParam20240618 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsSystemParam",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemParam",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystemParam",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropColumn(
                name: "SystemParam",
                schema: "Project",
                table: "ProgressStoreProcedureParam");
        }
    }
}
