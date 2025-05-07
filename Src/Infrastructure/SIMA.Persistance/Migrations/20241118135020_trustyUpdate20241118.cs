using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20241118 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DraftDestinationId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CancellationResaon",
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
                    table.PrimaryKey("PK_CancellationResaon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DraftCurrencyOrigin",
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
                    table.PrimaryKey("PK_DraftCurrencyOrigin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DraftDestination",
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
                    table.PrimaryKey("PK_DraftDestination", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DraftReviewResult",
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
                    table.PrimaryKey("PK_DraftReviewResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DraftStatus",
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
                    table.PrimaryKey("PK_DraftStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanType",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftDestinationId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_CancellationResaon_Code",
                schema: "TrustyDraft",
                table: "CancellationResaon",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DraftCurrencyOrigin_Code",
                schema: "TrustyDraft",
                table: "DraftCurrencyOrigin",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DraftDestination_Code",
                schema: "TrustyDraft",
                table: "DraftDestination",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DraftReviewResult_Code",
                schema: "TrustyDraft",
                table: "DraftReviewResult",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DraftStatus_Code",
                schema: "TrustyDraft",
                table: "DraftStatus",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanType_Code",
                schema: "Bank",
                table: "LoanType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_DraftDestination_DraftDestinationId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftDestinationId",
                principalSchema: "TrustyDraft",
                principalTable: "DraftDestination",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_DraftDestination_DraftDestinationId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "CancellationResaon",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "DraftCurrencyOrigin",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "DraftDestination",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "DraftReviewResult",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "DraftStatus",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "LoanType",
                schema: "Bank");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_DraftDestinationId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftDestinationId",
                schema: "TrustyDraft",
                table: "TrustyDraft");
        }
    }
}
