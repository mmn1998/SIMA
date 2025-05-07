using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyChanges20241119 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_BlockingNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_DraftNumberBasedOnOrder",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "ResponsibilityWage",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.RenameColumn(
                name: "DraftFinalWage",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                newName: "DarftFinalWage");

            migrationBuilder.AlterColumn<decimal>(
                name: "VageFixedValue",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBalance",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "StaffId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterCode",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "PayingBankName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "IssueReason",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IsFinished",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldFixedLength: true,
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "InterMediateBankName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExchangeWage",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<long>(
                name: "DraftValorStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "DraftRequestNetAmountBasedOnUsd",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DraftRequestAmountBasedOnUsd",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DraftRequestAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<long>(
                name: "DraftOriginId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "DraftNumberBasedOnOrder",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<long>(
                name: "DraftNetCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "DraftNetAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DraftIssueDate",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "DraftCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "DraftAcceptTime",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DraftAcceptDate",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerAccountNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<long>(
                name: "BrokerTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "BrokerId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "BrokerBankName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BlockingNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "BlockNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryBankName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<long>(
                name: "AccountTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "AcceptorName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "AgentBank",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryAccountNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryAddress",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryIban",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryPassportNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryPhoneNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BlockingAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CancellationAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CancellationCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDate",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancellationReferenceNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CancellationResaonId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancellationValorNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerIban",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerNationalCode",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhoneNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DraftCurrencyOriginId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DraftOrderNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DraftReviewResultId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DraftStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IntermediaryBank",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsFromAgentBank",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LoanTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RequestValorId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ResponsibilityWageTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBalance",
                schema: "TrustyDraft",
                table: "ResourceTransactionHistory",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BalanceBeforeTransaction",
                schema: "TrustyDraft",
                table: "ResourceTransactionHistory",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "TrustyDraft",
                table: "ResourceTransactionHistory",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBalance",
                schema: "TrustyDraft",
                table: "Resource",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "BlockedBalance",
                schema: "TrustyDraft",
                table: "Resource",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "AvaliableBalance",
                schema: "TrustyDraft",
                table: "Resource",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "SwiftMessage",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryAccountNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "RequestValor",
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
                    table.PrimaryKey("PK_RequestValor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponsibilityWageType",
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
                    table.PrimaryKey("PK_ResponsibilityWageType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_BlockingNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BlockingNumber",
                unique: true,
                filter: "[BlockingNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_CancellationCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "CancellationCurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_CancellationResaonId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "CancellationResaonId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftCurrencyOriginId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftCurrencyOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftNumberBasedOnOrder",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftNumberBasedOnOrder",
                unique: true,
                filter: "[DraftNumberBasedOnOrder] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftReviewResultId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftReviewResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_LoanTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "LoanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_RequestValorId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "RequestValorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_ResponsibilityWageTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "ResponsibilityWageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceiptInfo_ReceiptNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "ReceiptNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestValor_Code",
                schema: "TrustyDraft",
                table: "RequestValor",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResponsibilityWageType_Code",
                schema: "TrustyDraft",
                table: "ResponsibilityWageType",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_CancellationResaon_CancellationResaonId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "CancellationResaonId",
                principalSchema: "TrustyDraft",
                principalTable: "CancellationResaon",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_CurrencyType_CancellationCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "CancellationCurrencyTypeId",
                principalSchema: "Bank",
                principalTable: "CurrencyType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_DraftCurrencyOrigin_DraftCurrencyOriginId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftCurrencyOriginId",
                principalSchema: "TrustyDraft",
                principalTable: "DraftCurrencyOrigin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_DraftReviewResult_DraftReviewResultId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftReviewResultId",
                principalSchema: "TrustyDraft",
                principalTable: "DraftReviewResult",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_DraftStatus_DraftStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftStatusId",
                principalSchema: "TrustyDraft",
                principalTable: "DraftStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_LoanType_LoanTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "LoanTypeId",
                principalSchema: "Bank",
                principalTable: "LoanType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_RequestValor_RequestValorId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "RequestValorId",
                principalSchema: "TrustyDraft",
                principalTable: "RequestValor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_ResponsibilityWageType_ResponsibilityWageTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "ResponsibilityWageTypeId",
                principalSchema: "TrustyDraft",
                principalTable: "ResponsibilityWageType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_CancellationResaon_CancellationResaonId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_CurrencyType_CancellationCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_DraftCurrencyOrigin_DraftCurrencyOriginId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_DraftReviewResult_DraftReviewResultId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_DraftStatus_DraftStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_LoanType_LoanTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_RequestValor_RequestValorId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_ResponsibilityWageType_ResponsibilityWageTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "RequestValor",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "ResponsibilityWageType",
                schema: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_BlockingNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_CancellationCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_CancellationResaonId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_DraftCurrencyOriginId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_DraftNumberBasedOnOrder",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_DraftReviewResultId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_DraftStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_LoanTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_RequestValorId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_ResponsibilityWageTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceiptInfo_ReceiptNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropColumn(
                name: "AgentBank",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "BeneficiaryAccountNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "BeneficiaryAddress",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "BeneficiaryIban",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "BeneficiaryPassportNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "BeneficiaryPhoneNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "BlockingAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CancellationAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CancellationCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CancellationDate",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CancellationReferenceNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CancellationResaonId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CancellationValorNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CustomerIban",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CustomerNationalCode",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "CustomerPhoneNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftCurrencyOriginId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftOrderNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftReviewResultId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "IntermediaryBank",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "IsFromAgentBank",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "LoanTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "RejectReason",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "RequestValorId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "ResponsibilityWageTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.RenameColumn(
                name: "DarftFinalWage",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                newName: "DraftFinalWage");

            migrationBuilder.AlterColumn<double>(
                name: "VageFixedValue",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "CurrentBalance",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<long>(
                name: "StaffId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RegisterCode",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PayingBankName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IssueReason",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsFinished",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldFixedLength: true,
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InterMediateBankName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExchangeWage",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DraftValorStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DraftRequestNetAmountBasedOnUsd",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DraftRequestAmountBasedOnUsd",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DraftRequestAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DraftOriginId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DraftNumberBasedOnOrder",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DraftNetCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DraftNetAmount",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DraftIssueDate",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DraftCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "DraftAcceptTime",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DraftAcceptDate",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerAccountNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BrokerTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BrokerId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BrokerBankName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BlockingNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BlockNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryBankName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AccountTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcceptorName",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ResponsibilityWage",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "CurrentBalance",
                schema: "TrustyDraft",
                table: "ResourceTransactionHistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "BalanceBeforeTransaction",
                schema: "TrustyDraft",
                table: "ResourceTransactionHistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                schema: "TrustyDraft",
                table: "ResourceTransactionHistory",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "CurrentBalance",
                schema: "TrustyDraft",
                table: "Resource",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "BlockedBalance",
                schema: "TrustyDraft",
                table: "Resource",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "AvaliableBalance",
                schema: "TrustyDraft",
                table: "Resource",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "SwiftMessage",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                schema: "TrustyDraft",
                table: "Reconsilation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryAccountNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_BlockingNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BlockingNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftNumberBasedOnOrder",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftNumberBasedOnOrder",
                unique: true);
        }
    }
}
