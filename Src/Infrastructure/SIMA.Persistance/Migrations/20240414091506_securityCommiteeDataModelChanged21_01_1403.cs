using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class securityCommiteeDataModelChanged21_01_1403 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "SecurityCommitee",
                table: "SubjectPriority",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "SecurityCommitee",
                table: "SubjectPriority",
                newName: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "SecurityCommitee",
                table: "SubjectPriority",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "SecurityCommitee",
                table: "SubjectPriority",
                newName: "Description");
        }
    }
}
