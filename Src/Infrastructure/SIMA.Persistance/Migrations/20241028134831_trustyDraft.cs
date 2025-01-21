using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyDraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TrustyDraft");

            migrationBuilder.CreateTable(
                name: "AccountType",
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
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrokerAccountBook",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BrokerId = table.Column<long>(type: "bigint", nullable: false),
                    IBANNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActivityExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerAccountBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerAccountBook_Broker_BrokerId",
                        column: x => x.BrokerId,
                        principalSchema: "Bank",
                        principalTable: "Broker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BrokerAddressBook",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BrokerId = table.Column<long>(type: "bigint", nullable: false),
                    AddressTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActivityExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerAddressBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerAddressBook_AddressType_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalSchema: "Basic",
                        principalTable: "AddressType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BrokerAddressBook_Broker_BrokerId",
                        column: x => x.BrokerId,
                        principalSchema: "Bank",
                        principalTable: "Broker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BrokerPhoneBook",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BrokerId = table.Column<long>(type: "bigint", nullable: false),
                    PhoneTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerPhoneBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerPhoneBook_Broker_BrokerId",
                        column: x => x.BrokerId,
                        principalSchema: "Bank",
                        principalTable: "Broker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BrokerPhoneBook_PhonType_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalSchema: "Basic",
                        principalTable: "PhonType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CurrencyOprationType",
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
                    table.PrimaryKey("PK_CurrencyOprationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustomerNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DraftIssueType",
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
                    table.PrimaryKey("PK_DraftIssueType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DraftOrigin",
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
                    table.PrimaryKey("PK_DraftOrigin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DraftType",
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
                    table.PrimaryKey("PK_DraftType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DraftValorStatus",
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
                    table.PrimaryKey("PK_DraftValorStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialActionType",
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
                    table.PrimaryKey("PK_FinancialActionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReconsilationType",
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
                    table.PrimaryKey("PK_ReconsilationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AccountTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CurrencyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CurrentBalance = table.Column<double>(type: "float", nullable: false),
                    AvaliableBalance = table.Column<double>(type: "float", nullable: false),
                    BlockedBalance = table.Column<double>(type: "float", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalSchema: "Bank",
                        principalTable: "AccountType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Resource_CurrencyType_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalSchema: "Bank",
                        principalTable: "CurrencyType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WageRate",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyOperationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsBasedOnPercentage = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: false),
                    IsBasedOnFixedValue = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: false),
                    CurrentBalance = table.Column<double>(type: "float", nullable: false),
                    VagePrecentage = table.Column<int>(type: "int", nullable: false),
                    VageFixedValue = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WageRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WageRate_CurrencyOprationType_CurrencyOperationTypeId",
                        column: x => x.CurrencyOperationTypeId,
                        principalSchema: "Bank",
                        principalTable: "CurrencyOprationType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WageRate_CurrencyType_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalSchema: "Bank",
                        principalTable: "CurrencyType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FinancialSupplier",
                schema: "Bank",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_FinancialSupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialSupplier_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Bank",
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrustyDraft",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ValorDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DraftNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DraftNumberBasedOnOrder = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    DraftOriginId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerAccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BlockNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DraftIssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlockingNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DraftRequestAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DraftRequestAmountBasedOnUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DraftNetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DraftRequestNetAmountBasedOnUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DraftCurrencyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    DraftNetCurrencyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PayingBankName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BrokerBankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterMediateBankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeneficiaryBankName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExchangeWage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResponsibilityWage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RegisterCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AcceptorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DraftAcceptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DraftAcceptTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IssueReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFinished = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedBy = table.Column<long>(type: "bigint", nullable: true),
                    AccountTypeId = table.Column<long>(type: "bigint", nullable: false),
                    BrokerId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    BrokerTypeId = table.Column<long>(type: "bigint", nullable: false),
                    DraftValorStatusId = table.Column<long>(type: "bigint", nullable: false),
                    DraftTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DraftFinalWage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustyDraft", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrustyDraft_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalSchema: "Bank",
                        principalTable: "AccountType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Bank",
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_BrokerType_BrokerTypeId",
                        column: x => x.BrokerTypeId,
                        principalSchema: "Bank",
                        principalTable: "BrokerType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_Broker_BrokerId",
                        column: x => x.BrokerId,
                        principalSchema: "Bank",
                        principalTable: "Broker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_CurrencyType_DraftCurrencyTypeId",
                        column: x => x.DraftCurrencyTypeId,
                        principalSchema: "Bank",
                        principalTable: "CurrencyType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_CurrencyType_DraftNetCurrencyTypeId",
                        column: x => x.DraftNetCurrencyTypeId,
                        principalSchema: "Bank",
                        principalTable: "CurrencyType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Bank",
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_DraftOrigin_DraftOriginId",
                        column: x => x.DraftOriginId,
                        principalSchema: "TrustyDraft",
                        principalTable: "DraftOrigin",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_DraftType_DraftTypeId",
                        column: x => x.DraftTypeId,
                        principalSchema: "TrustyDraft",
                        principalTable: "DraftType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_DraftValorStatus_DraftValorStatusId",
                        column: x => x.DraftValorStatusId,
                        principalSchema: "TrustyDraft",
                        principalTable: "DraftValorStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraft_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResourceTransactionHistory",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FinancialSupplierId = table.Column<long>(type: "bigint", nullable: false),
                    ResourceId = table.Column<long>(type: "bigint", nullable: false),
                    FinancialActionTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    EffectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BalanceBeforeTransaction = table.Column<double>(type: "float", nullable: true),
                    CurrentBalance = table.Column<double>(type: "float", nullable: true),
                    IsBlocked = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTransactionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceTransactionHistory_FinancialActionType_FinancialActionTypeId",
                        column: x => x.FinancialActionTypeId,
                        principalSchema: "Bank",
                        principalTable: "FinancialActionType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceTransactionHistory_FinancialSupplier_FinancialSupplierId",
                        column: x => x.FinancialSupplierId,
                        principalSchema: "Bank",
                        principalTable: "FinancialSupplier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceTransactionHistory_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "TrustyDraft",
                        principalTable: "Resource",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BrokerAddressInfo",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TrustyDraftId = table.Column<long>(type: "bigint", nullable: false),
                    BrokerAddressBookId = table.Column<long>(type: "bigint", nullable: false),
                    BrokerPhoneBookId = table.Column<long>(type: "bigint", nullable: false),
                    BrokerAccountBookId = table.Column<long>(type: "bigint", nullable: false),
                    IsLastConfirmedAddress = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerAddressInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerAddressInfo_BrokerAccountBook_BrokerAccountBookId",
                        column: x => x.BrokerAccountBookId,
                        principalSchema: "Bank",
                        principalTable: "BrokerAccountBook",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BrokerAddressInfo_BrokerAddressBook_BrokerAddressBookId",
                        column: x => x.BrokerAddressBookId,
                        principalSchema: "Bank",
                        principalTable: "BrokerAddressBook",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BrokerAddressInfo_BrokerPhoneBook_BrokerPhoneBookId",
                        column: x => x.BrokerPhoneBookId,
                        principalSchema: "Bank",
                        principalTable: "BrokerPhoneBook",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BrokerAddressInfo_TrustyDraft_TrustyDraftId",
                        column: x => x.TrustyDraftId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reconsilation",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TrustyDraftId = table.Column<long>(type: "bigint", nullable: false),
                    ReconsilationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    BrokerId = table.Column<long>(type: "bigint", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DraftAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DraftAmountBaseCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WageAmountBaseCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmountBaseCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmountBaseCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SwiftMessageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SwiftMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    LetterNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LetterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reconsilation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reconsilation_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Bank",
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reconsilation_Broker_BrokerId",
                        column: x => x.BrokerId,
                        principalSchema: "Bank",
                        principalTable: "Broker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reconsilation_ReconsilationType_ReconsilationTypeId",
                        column: x => x.ReconsilationTypeId,
                        principalSchema: "TrustyDraft",
                        principalTable: "ReconsilationType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reconsilation_TrustyDraft_TrustyDraftId",
                        column: x => x.TrustyDraftId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrustyDraftDocument",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TrustyDraftId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustyDraftDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrustyDraftDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraftDocument_TrustyDraft_TrustyDraftId",
                        column: x => x.TrustyDraftId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrustyDraftResource",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TrustyDraftId = table.Column<long>(type: "bigint", nullable: false),
                    ResourceId = table.Column<long>(type: "bigint", nullable: false),
                    AssignedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustyDraftResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrustyDraftResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "TrustyDraft",
                        principalTable: "Resource",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraftResource_TrustyDraft_TrustyDraftId",
                        column: x => x.TrustyDraftId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrustyDraftIssue",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TrustyDraftId = table.Column<long>(type: "bigint", nullable: false),
                    DraftIssueTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ReconsilationId = table.Column<long>(type: "bigint", nullable: true),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustyDraftIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrustyDraftIssue_DraftIssueType_DraftIssueTypeId",
                        column: x => x.DraftIssueTypeId,
                        principalSchema: "TrustyDraft",
                        principalTable: "DraftIssueType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraftIssue_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraftIssue_Reconsilation_ReconsilationId",
                        column: x => x.ReconsilationId,
                        principalSchema: "TrustyDraft",
                        principalTable: "Reconsilation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraftIssue_TrustyDraft_TrustyDraftId",
                        column: x => x.TrustyDraftId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentReceiptInfo",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TrustyDraftId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<double>(type: "float", nullable: false),
                    PaymentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    BrokerAccountBookId = table.Column<long>(type: "bigint", nullable: false),
                    BeneficiaryAccountTypeId = table.Column<long>(type: "bigint", nullable: false),
                    BeneficiaryAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustyDraftDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReceiptInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentReceiptInfo_AccountType_BeneficiaryAccountTypeId",
                        column: x => x.BeneficiaryAccountTypeId,
                        principalSchema: "Bank",
                        principalTable: "AccountType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentReceiptInfo_BrokerAccountBook_BrokerAccountBookId",
                        column: x => x.BrokerAccountBookId,
                        principalSchema: "Bank",
                        principalTable: "BrokerAccountBook",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentReceiptInfo_CurrencyType_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalSchema: "Bank",
                        principalTable: "CurrencyType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentReceiptInfo_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalSchema: "Bank",
                        principalTable: "PaymentType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentReceiptInfo_TrustyDraftDocument_TrustyDraftDocumentId",
                        column: x => x.TrustyDraftDocumentId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraftDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentReceiptInfo_TrustyDraft_TrustyDraftId",
                        column: x => x.TrustyDraftId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReferralLetter",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LetterNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LetterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrokerId = table.Column<long>(type: "bigint", nullable: false),
                    LeterDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralLetter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferralLetter_Broker_BrokerId",
                        column: x => x.BrokerId,
                        principalSchema: "Bank",
                        principalTable: "Broker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReferralLetter_TrustyDraftDocument_LeterDocumentId",
                        column: x => x.LeterDocumentId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraftDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReferralLetter_TrustyDraftDocument_ReceiptDocumentId",
                        column: x => x.ReceiptDocumentId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraftDocument",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Statement",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    StatementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustyDraftId = table.Column<long>(type: "bigint", nullable: false),
                    TrustyDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statement_TrustyDraftDocument_TrustyDocumentId",
                        column: x => x.TrustyDocumentId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraftDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Statement_TrustyDraft_TrustyDraftId",
                        column: x => x.TrustyDraftId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReferralLetterDraftList",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TrustyDraftId = table.Column<long>(type: "bigint", nullable: false),
                    ReferralLetterId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralLetterDraftList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferralLetterDraftList_ReferralLetter_ReferralLetterId",
                        column: x => x.ReferralLetterId,
                        principalSchema: "TrustyDraft",
                        principalTable: "ReferralLetter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReferralLetterDraftList_TrustyDraft_TrustyDraftId",
                        column: x => x.TrustyDraftId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_Code",
                schema: "Bank",
                table: "AccountType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerAccountBook_BrokerId",
                schema: "Bank",
                table: "BrokerAccountBook",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerAddressBook_AddressTypeId",
                schema: "Bank",
                table: "BrokerAddressBook",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerAddressBook_BrokerId",
                schema: "Bank",
                table: "BrokerAddressBook",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerAddressBook_PostalCode",
                schema: "Bank",
                table: "BrokerAddressBook",
                column: "PostalCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrokerAddressInfo_BrokerAccountBookId",
                schema: "TrustyDraft",
                table: "BrokerAddressInfo",
                column: "BrokerAccountBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerAddressInfo_BrokerAddressBookId",
                schema: "TrustyDraft",
                table: "BrokerAddressInfo",
                column: "BrokerAddressBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerAddressInfo_BrokerPhoneBookId",
                schema: "TrustyDraft",
                table: "BrokerAddressInfo",
                column: "BrokerPhoneBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerAddressInfo_TrustyDraftId",
                schema: "TrustyDraft",
                table: "BrokerAddressInfo",
                column: "TrustyDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerPhoneBook_BrokerId",
                schema: "Bank",
                table: "BrokerPhoneBook",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerPhoneBook_PhoneTypeId",
                schema: "Bank",
                table: "BrokerPhoneBook",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyOprationType_Code",
                schema: "Bank",
                table: "CurrencyOprationType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerNumber",
                schema: "Bank",
                table: "Customer",
                column: "CustomerNumber",
                unique: true,
                filter: "[CustomerNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DraftIssueType_Code",
                schema: "TrustyDraft",
                table: "DraftIssueType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DraftOrigin_Code",
                schema: "TrustyDraft",
                table: "DraftOrigin",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DraftType_Code",
                schema: "TrustyDraft",
                table: "DraftType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DraftValorStatus_Code",
                schema: "TrustyDraft",
                table: "DraftValorStatus",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialActionType_Code",
                schema: "Bank",
                table: "FinancialActionType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialSupplier_Code",
                schema: "Bank",
                table: "FinancialSupplier",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialSupplier_CustomerId",
                schema: "Bank",
                table: "FinancialSupplier",
                column: "CustomerId");

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
                name: "IX_PaymentReceiptInfo_TrustyDraftDocumentId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "TrustyDraftDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceiptInfo_TrustyDraftId",
                schema: "TrustyDraft",
                table: "PaymentReceiptInfo",
                column: "TrustyDraftId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Reconsilation_ReconsilationTypeId",
                schema: "TrustyDraft",
                table: "Reconsilation",
                column: "ReconsilationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reconsilation_TrustyDraftId",
                schema: "TrustyDraft",
                table: "Reconsilation",
                column: "TrustyDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_ReconsilationType_Code",
                schema: "TrustyDraft",
                table: "ReconsilationType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReferralLetter_BrokerId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralLetter_LeterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                column: "LeterDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralLetter_ReceiptDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                column: "ReceiptDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralLetterDraftList_ReferralLetterId",
                schema: "TrustyDraft",
                table: "ReferralLetterDraftList",
                column: "ReferralLetterId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralLetterDraftList_TrustyDraftId",
                schema: "TrustyDraft",
                table: "ReferralLetterDraftList",
                column: "TrustyDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_AccountTypeId",
                schema: "TrustyDraft",
                table: "Resource",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_Code",
                schema: "TrustyDraft",
                table: "Resource",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resource_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "Resource",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTransactionHistory_FinancialActionTypeId",
                schema: "TrustyDraft",
                table: "ResourceTransactionHistory",
                column: "FinancialActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTransactionHistory_FinancialSupplierId",
                schema: "TrustyDraft",
                table: "ResourceTransactionHistory",
                column: "FinancialSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTransactionHistory_ResourceId",
                schema: "TrustyDraft",
                table: "ResourceTransactionHistory",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_TrustyDocumentId",
                schema: "TrustyDraft",
                table: "Statement",
                column: "TrustyDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_TrustyDraftId",
                schema: "TrustyDraft",
                table: "Statement",
                column: "TrustyDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_AccountTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_BlockingNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BlockingNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_BranchId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_BrokerId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_BrokerTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "BrokerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_CustomerId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftCurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftNetCurrencyTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftNetCurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftNumberBasedOnOrder",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftNumberBasedOnOrder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftOriginId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_DraftValorStatusId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "DraftValorStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_StaffId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftDocument_DocumentId",
                schema: "TrustyDraft",
                table: "TrustyDraftDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftDocument_TrustyDraftId",
                schema: "TrustyDraft",
                table: "TrustyDraftDocument",
                column: "TrustyDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftIssue_DraftIssueTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraftIssue",
                column: "DraftIssueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftIssue_IssueId",
                schema: "TrustyDraft",
                table: "TrustyDraftIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftIssue_ReconsilationId",
                schema: "TrustyDraft",
                table: "TrustyDraftIssue",
                column: "ReconsilationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftIssue_TrustyDraftId",
                schema: "TrustyDraft",
                table: "TrustyDraftIssue",
                column: "TrustyDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftResource_ResourceId",
                schema: "TrustyDraft",
                table: "TrustyDraftResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftResource_TrustyDraftId",
                schema: "TrustyDraft",
                table: "TrustyDraftResource",
                column: "TrustyDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_WageRate_CurrencyOperationTypeId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "CurrencyOperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WageRate_CurrencyTypeId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "CurrencyTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrokerAddressInfo",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "PaymentReceiptInfo",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "ReferralLetterDraftList",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "ResourceTransactionHistory",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "Statement",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "TrustyDraftIssue",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "TrustyDraftResource",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "WageRate",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "BrokerAddressBook",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "BrokerPhoneBook",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "BrokerAccountBook",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "ReferralLetter",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "FinancialActionType",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "FinancialSupplier",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "DraftIssueType",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "Reconsilation",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "Resource",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "CurrencyOprationType",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "TrustyDraftDocument",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "ReconsilationType",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "TrustyDraft",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "AccountType",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Bank");

            migrationBuilder.DropTable(
                name: "DraftOrigin",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "DraftType",
                schema: "TrustyDraft");

            migrationBuilder.DropTable(
                name: "DraftValorStatus",
                schema: "TrustyDraft");
        }
    }
}
