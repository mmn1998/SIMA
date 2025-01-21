using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20241211 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AgentBankWageShareStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryExternalAccountNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailBic",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DraftNetAmountBasedOnEur",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DraftNetAmountBasedOnUsd",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DraftRequestAmountBasedOnEur",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderingExternalAccountNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OriginAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AgentBankWageShareStatus",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentBankWageShareStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_AgentBankWageShareStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "AgentBankWageShareStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentBankWageShareStatus_Code",
                schema: "TrustyDraft",
                table: "AgentBankWageShareStatus",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_AgentBankWageShareStatus_AgentBankWageShareStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "AgentBankWageShareStatusId",
                principalSchema: "TrustyDraft",
                principalTable: "AgentBankWageShareStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_AgentBankWageShareStatus_AgentBankWageShareStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "AgentBankWageShareStatus",
                schema: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_AgentBankWageShareStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "AgentBankWageShareStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "BeneficiaryExternalAccountNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DetailBic",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftNetAmountBasedOnEur",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftNetAmountBasedOnUsd",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftRequestAmountBasedOnEur",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "OrderingExternalAccountNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "OriginAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft");
        }
    }
}
