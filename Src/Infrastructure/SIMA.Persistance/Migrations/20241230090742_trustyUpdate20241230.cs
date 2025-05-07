using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20241230 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_Broker_SuggestedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_CurrencyType_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_DraftType_DraftTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropIndex(
                name: "IX_InquiryRequest_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "ApplicantName",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "CurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.RenameColumn(
                name: "test",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "SuggestedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "DraftTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_InquiryRequest_SuggestedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "IX_InquiryRequest_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_InquiryRequest_DraftTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "IX_InquiryRequest_CustomerId");

            migrationBuilder.AddColumn<long>(
                name: "InquiryRequestCurrencyId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryName",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DraftOrderNumber",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProformaNumber",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "InquiryRequestCurrency",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    InquiryRequestId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryRequestCurrency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryRequestCurrency_CurrencyType_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalSchema: "Bank",
                        principalTable: "CurrencyType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryRequestCurrency_InquiryRequest_InquiryRequestId",
                        column: x => x.InquiryRequestId,
                        principalSchema: "TrustyDraft",
                        principalTable: "InquiryRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InquiryResponse_InquiryRequestCurrencyId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                column: "InquiryRequestCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequest_ReferenceNumber",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "ReferenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequestCurrency_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequestCurrency",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequestCurrency_InquiryRequestId",
                schema: "TrustyDraft",
                table: "InquiryRequestCurrency",
                column: "InquiryRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_Customer_CustomerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "CustomerId",
                principalSchema: "Bank",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_PaymentType_PaymentTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "PaymentTypeId",
                principalSchema: "Bank",
                principalTable: "PaymentType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryResponse_InquiryRequestCurrency_InquiryRequestCurrencyId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                column: "InquiryRequestCurrencyId",
                principalSchema: "TrustyDraft",
                principalTable: "InquiryRequestCurrency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_Customer_CustomerId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_PaymentType_PaymentTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_InquiryResponse_InquiryRequestCurrency_InquiryRequestCurrencyId",
                schema: "TrustyDraft",
                table: "InquiryResponse");

            migrationBuilder.DropTable(
                name: "InquiryRequestCurrency",
                schema: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_InquiryResponse_InquiryRequestCurrencyId",
                schema: "TrustyDraft",
                table: "InquiryResponse");

            migrationBuilder.DropIndex(
                name: "IX_InquiryRequest_ReferenceNumber",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "InquiryRequestCurrencyId",
                schema: "TrustyDraft",
                table: "InquiryResponse");

            migrationBuilder.DropColumn(
                name: "DraftOrderNumber",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "ProformaNumber",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "SuggestedBrokerId");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "test");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "DraftTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_InquiryRequest_PaymentTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "IX_InquiryRequest_SuggestedBrokerId");

            migrationBuilder.RenameIndex(
                name: "IX_InquiryRequest_CustomerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                newName: "IX_InquiryRequest_DraftTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryName",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ApplicantName",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "CurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequest_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "CurrencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_Broker_SuggestedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "SuggestedBrokerId",
                principalSchema: "Bank",
                principalTable: "Broker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_CurrencyType_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "CurrencyTypeId",
                principalSchema: "Bank",
                principalTable: "CurrencyType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_DraftType_DraftTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "DraftTypeId",
                principalSchema: "TrustyDraft",
                principalTable: "DraftType",
                principalColumn: "Id");
        }
    }
}
