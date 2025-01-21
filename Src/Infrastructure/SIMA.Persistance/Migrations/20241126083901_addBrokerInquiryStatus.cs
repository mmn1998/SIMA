using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addBrokerInquiryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BrokerInquiryStatus",
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
                    table.PrimaryKey("PK_BrokerInquiryStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BrokerInquiryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerInquiryStatus_Code",
                schema: "TrustyDraft",
                table: "BrokerInquiryStatus",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_BrokerInquiryStatus_BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BrokerInquiryStatusId",
                principalSchema: "TrustyDraft",
                principalTable: "BrokerInquiryStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_BrokerInquiryStatus_BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "BrokerInquiryStatus",
                schema: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");
        }
    }
}
