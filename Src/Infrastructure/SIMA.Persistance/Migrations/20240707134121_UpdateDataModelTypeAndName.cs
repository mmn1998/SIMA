using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataModelTypeAndName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemParam",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Logistics",
                table: "PaymentCommand");

            migrationBuilder.RenameColumn(
                name: "AccessFaildDate",
                schema: "Authentication",
                table: "Users",
                newName: "AccessFailedDate");

            migrationBuilder.AlterColumn<string>(
                name: "CompleteName",
                schema: "Project",
                table: "Step",
                type: "varchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsSystemParam",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemParamName",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "varchar",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemParamName",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.RenameColumn(
                name: "AccessFailedDate",
                schema: "Authentication",
                table: "Users",
                newName: "AccessFaildDate");

            migrationBuilder.AlterColumn<string>(
                name: "CompleteName",
                schema: "Project",
                table: "Step",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsSystemParam",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemParam",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Logistics",
                table: "PaymentCommand",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
