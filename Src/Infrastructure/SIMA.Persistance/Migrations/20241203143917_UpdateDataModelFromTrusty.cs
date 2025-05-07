using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataModelFromTrusty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceiptInfo_AccountType_BeneficiaryAccountTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceiptInfo_BrokerAccountBook_BrokerAccountBookId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceiptInfo_CurrencyType_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceiptInfo_PaymentType_PaymentTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_BrokerInquiryStatus_BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceiptInfo_BeneficiaryAccountTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceiptInfo_BrokerAccountBookId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceiptInfo_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceiptInfo_PaymentTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceiptInfo_ReceiptNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropColumn(
                name: "IsBasedOnFixedValue",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropColumn(
                name: "BeneficiaryAccountNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropColumn(
                name: "BeneficiaryAccountTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropColumn(
                name: "BrokerAccountBookId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropColumn(
                name: "CurrencyTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.DropColumn(
                name: "ReceiptNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo");

            migrationBuilder.RenameColumn(
                name: "BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                newName: "BrokerSecondLevelAddressBookId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustyDraft_BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                newName: "IX_TrustyDraft_BrokerSecondLevelAddressBookId");

            migrationBuilder.AlterColumn<int>(
                name: "VagePrecentage",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "VageFixedValue",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<long>(
                name: "CurrencyPaymentChannelId",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DraftTypeId",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PaymentTypeId",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                schema: "DMS",
                table: "Documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsOrganizationalDocumentation",
                schema: "DMS",
                table: "Documents",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BrokerSecondLevelAddressBook",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerSecondLevelAddressBook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyPaymentChannel",
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
                    table.PrimaryKey("PK_CurrencyPaymentChannel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InquiryRequest",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    SuggectedBrokerId = table.Column<long>(type: "bigint", nullable: false),
                    DraftTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryRequest_Broker_SuggectedBrokerId",
                        column: x => x.SuggectedBrokerId,
                        principalSchema: "Bank",
                        principalTable: "Broker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryRequest_CurrencyType_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalSchema: "Bank",
                        principalTable: "CurrencyType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryRequest_DraftType_DraftTypeId",
                        column: x => x.DraftTypeId,
                        principalSchema: "TrustyDraft",
                        principalTable: "DraftType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WageDeductionMethod",
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
                    table.PrimaryKey("PK_WageDeductionMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InquiryResponse",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    InquiryRequestId = table.Column<long>(type: "bigint", nullable: false),
                    BrokerInquiryStatusId = table.Column<long>(type: "bigint", nullable: false),
                    BrokerId = table.Column<long>(type: "bigint", nullable: false),
                    WageRateId = table.Column<long>(type: "bigint", nullable: false),
                    CalculatedWage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExcessWage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValidityPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryResponse_BrokerInquiryStatus_BrokerInquiryStatusId",
                        column: x => x.BrokerInquiryStatusId,
                        principalSchema: "TrustyDraft",
                        principalTable: "BrokerInquiryStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryResponse_Broker_BrokerId",
                        column: x => x.BrokerId,
                        principalSchema: "Bank",
                        principalTable: "Broker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryResponse_InquiryRequest_InquiryRequestId",
                        column: x => x.InquiryRequestId,
                        principalSchema: "TrustyDraft",
                        principalTable: "InquiryRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryResponse_WageRate_WageRateId",
                        column: x => x.WageRateId,
                        principalSchema: "TrustyDraft",
                        principalTable: "WageRate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WageRate_CurrencyPaymentChannelId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "CurrencyPaymentChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_WageRate_DraftTypeId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "DraftTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WageRate_PaymentTypeId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CompanyId",
                schema: "DMS",
                table: "Documents",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyPaymentChannel_Code",
                schema: "TrustyDraft",
                table: "CurrencyPaymentChannel",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequest_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequest_DraftTypeId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "DraftTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequest_SuggectedBrokerId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "SuggectedBrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryResponse_BrokerId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryResponse_BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                column: "BrokerInquiryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryResponse_InquiryRequestId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                column: "InquiryRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryResponse_WageRateId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                column: "WageRateId");

            migrationBuilder.CreateIndex(
                name: "IX_WageDeductionMethod_Code",
                schema: "TrustyDraft",
                table: "WageDeductionMethod",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Company_CompanyId",
                schema: "DMS",
                table: "Documents",
                column: "CompanyId",
                principalSchema: "Organization",
                principalTable: "Company",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_BrokerSecondLevelAddressBook_BrokerSecondLevelAddressBookId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BrokerSecondLevelAddressBookId",
                principalSchema: "TrustyDraft",
                principalTable: "BrokerSecondLevelAddressBook",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WageRate_CurrencyPaymentChannel_CurrencyPaymentChannelId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "CurrencyPaymentChannelId",
                principalSchema: "TrustyDraft",
                principalTable: "CurrencyPaymentChannel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WageRate_DraftType_DraftTypeId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "DraftTypeId",
                principalSchema: "TrustyDraft",
                principalTable: "DraftType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WageRate_PaymentType_PaymentTypeId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "PaymentTypeId",
                principalSchema: "Bank",
                principalTable: "PaymentType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Company_CompanyId",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_BrokerSecondLevelAddressBook_BrokerSecondLevelAddressBookId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_WageRate_CurrencyPaymentChannel_CurrencyPaymentChannelId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropForeignKey(
                name: "FK_WageRate_DraftType_DraftTypeId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropForeignKey(
                name: "FK_WageRate_PaymentType_PaymentTypeId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropTable(
                name: "BrokerSecondLevelAddressBook",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "CurrencyPaymentChannel",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "InquiryResponse",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "WageDeductionMethod",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "InquiryRequest",
                schema: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_WageRate_CurrencyPaymentChannelId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropIndex(
                name: "IX_WageRate_DraftTypeId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropIndex(
                name: "IX_WageRate_PaymentTypeId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropIndex(
                name: "IX_Documents_CompanyId",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CurrencyPaymentChannelId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropColumn(
                name: "DraftTypeId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "IsOrganizationalDocumentation",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "BrokerSecondLevelAddressBookId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                newName: "BrokerInquiryStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustyDraft_BrokerSecondLevelAddressBookId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                newName: "IX_TrustyDraft_BrokerInquiryStatusId");

            migrationBuilder.AlterColumn<int>(
                name: "VagePrecentage",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VageFixedValue",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsBasedOnFixedValue",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryAccountNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BeneficiaryAccountTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BrokerAccountBookId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PaymentTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceiptInfo_BeneficiaryAccountTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "BeneficiaryAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceiptInfo_BrokerAccountBookId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "BrokerAccountBookId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceiptInfo_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceiptInfo_PaymentTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceiptInfo_ReceiptNumber",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "ReceiptNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceiptInfo_AccountType_BeneficiaryAccountTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "BeneficiaryAccountTypeId",
                principalSchema: "Bank",
                principalTable: "AccountType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceiptInfo_BrokerAccountBook_BrokerAccountBookId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "BrokerAccountBookId",
                principalSchema: "Bank",
                principalTable: "BrokerAccountBook",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceiptInfo_CurrencyType_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "CurrencyTypeId",
                principalSchema: "Bank",
                principalTable: "CurrencyType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceiptInfo_PaymentType_PaymentTypeId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "PaymentTypeId",
                principalSchema: "Bank",
                principalTable: "PaymentType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_BrokerInquiryStatus_BrokerInquiryStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BrokerInquiryStatusId",
                principalSchema: "TrustyDraft",
                principalTable: "BrokerInquiryStatus",
                principalColumn: "Id");
        }
    }
}
