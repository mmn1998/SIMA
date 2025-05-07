using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trusydraftdatamodelupdate20241130 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reconsilation_Branch_BranchId",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reconsilation_Broker_BrokerId",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropIndex(
                name: "IX_Reconsilation_BranchId",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropIndex(
                name: "IX_Reconsilation_BrokerId",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "DraftAmount",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "DraftAmountBaseCurrency",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "LetterNumber",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "NetAmount",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "NetAmountBaseCurrency",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "PartNumber",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "SwiftMessage",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "SwiftMessageCode",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "TaxAmountBaseCurrency",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "WageAmount",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.DropColumn(
                name: "WageAmountBaseCurrency",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.RenameColumn(
                name: "LetterDate",
                schema: "TrustyDraft",
                table: "Reconsilation",
                newName: "InformedDate");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AddColumn<string>(
                name: "IsInformedByBranch",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInformedByBranch",
                schema: "TrustyDraft",
                table: "Reconsilation");

            migrationBuilder.RenameColumn(
                name: "InformedDate",
                schema: "TrustyDraft",
                table: "Reconsilation",
                newName: "LetterDate");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BrokerId",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DraftAmount",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DraftAmountBaseCurrency",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "LetterNumber",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "NetAmount",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetAmountBaseCurrency",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PartNumber",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SwiftMessage",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SwiftMessageCode",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmountBaseCurrency",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WageAmount",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WageAmountBaseCurrency",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Reconsilation_BranchId",
                schema: "TrustyDraft",
                table: "Reconsilation",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reconsilation_BrokerId",
                schema: "TrustyDraft",
                table: "Reconsilation",
                column: "BrokerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reconsilation_Branch_BranchId",
                schema: "TrustyDraft",
                table: "Reconsilation",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reconsilation_Broker_BrokerId",
                schema: "TrustyDraft",
                table: "Reconsilation",
                column: "BrokerId",
                principalSchema: "Bank",
                principalTable: "Broker",
                principalColumn: "Id");
        }
    }
}
