using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateModelForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FormId",
                schema: "Project",
                table: "Step",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Step_FormId",
                schema: "Project",
                table: "Step",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Form_FormId",
                schema: "Project",
                table: "Step",
                column: "FormId",
                principalSchema: "Authentication",
                principalTable: "Form",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Form_FormId",
                schema: "Project",
                table: "Step");

            migrationBuilder.DropIndex(
                name: "IX_Step_FormId",
                schema: "Project",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "FormId",
                schema: "Project",
                table: "Step");
            
            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "Project",
                table: "Step");
        }
    }
}
