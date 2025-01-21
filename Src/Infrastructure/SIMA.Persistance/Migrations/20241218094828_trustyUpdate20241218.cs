using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20241218 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "WagePercentage",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "InquiryRequestDocument",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    InquiryRequestId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryRequestDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryRequestDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryRequestDocument_InquiryRequest_InquiryRequestId",
                        column: x => x.InquiryRequestId,
                        principalSchema: "TrustyDraft",
                        principalTable: "InquiryRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequestDocument_DocumentId",
                schema: "TrustyDraft",
                table: "InquiryRequestDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequestDocument_InquiryRequestId",
                schema: "TrustyDraft",
                table: "InquiryRequestDocument",
                column: "InquiryRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InquiryRequestDocument",
                schema: "TrustyDraft");

            migrationBuilder.AlterColumn<int>(
                name: "WagePercentage",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
