using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class _20241209Updatetrusty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_Broker_SuggectedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "ValidityPeriod",
                schema: "TrustyDraft",
                table: "InquiryResponse");

            //migrationBuilder.RenameColumn(
            //    name: "VagePrecentage",
            //    schema: "TrustyDraft",
            //    table: "WageRate",
            //    newName: "WagePercentage");

            //migrationBuilder.RenameColumn(
            //    name: "VageFixedValue",
            //    schema: "TrustyDraft",
            //    table: "WageRate",
            //    newName: "WageFixedValue");

            //migrationBuilder.RenameColumn(
            //    name: "CurrentBalance",
            //    schema: "TrustyDraft",
            //    table: "WageRate",
            //    newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "SuggectedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "SuggestedBrokerId");

            migrationBuilder.RenameIndex(
                name: "IX_InquiryRequest_SuggectedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "IX_InquiryRequest_SuggestedBrokerId");

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequest_BranchId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_Branch_BranchId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "BranchId",
                principalSchema: "Bank",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_Broker_SuggestedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "SuggestedBrokerId",
                principalSchema: "Bank",
                principalTable: "Broker",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_Branch_BranchId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_Broker_SuggestedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropIndex(
                name: "IX_InquiryRequest_BranchId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            //migrationBuilder.RenameColumn(
            //    name: "WagePercentage",
            //    schema: "TrustyDraft",
            //    table: "WageRate",
            //    newName: "VagePrecentage");

            //migrationBuilder.RenameColumn(
            //    name: "WageFixedValue",
            //    schema: "TrustyDraft",
            //    table: "WageRate",
            //    newName: "VageFixedValue");

            //migrationBuilder.RenameColumn(
            //    name: "Discount",
            //    schema: "TrustyDraft",
            //    table: "WageRate",
            //    newName: "CurrentBalance");

            migrationBuilder.RenameColumn(
                name: "SuggestedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "SuggectedBrokerId");

            migrationBuilder.RenameIndex(
                name: "IX_InquiryRequest_SuggestedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "IX_InquiryRequest_SuggectedBrokerId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidityPeriod",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_Broker_SuggectedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "SuggectedBrokerId",
                principalSchema: "Bank",
                principalTable: "Broker",
                principalColumn: "Id");
        }
    }
}
