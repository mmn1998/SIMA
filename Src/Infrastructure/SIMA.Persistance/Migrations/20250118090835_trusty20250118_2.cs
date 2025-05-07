using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trusty20250118_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProformaCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProformaDate",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequest_ProformaCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "ProformaCurrencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_CurrencyType_ProformaCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "ProformaCurrencyTypeId",
                principalSchema: "Bank",
                principalTable: "CurrencyType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_CurrencyType_ProformaCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropIndex(
                name: "IX_InquiryRequest_ProformaCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "ProformaCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "ProformaDate",
                schema: "TrustyDraft",
                table: "InquiryRequest");
        }
    }
}
