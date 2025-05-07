using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class logistics_379 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidatedSupplier_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "CandidatedSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsCoding_Goods_GoodsId",
                schema: "Logistics",
                table: "GoodsCoding");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsCoding_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "GoodsCoding");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCommand_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentCommand");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistory_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveOrder_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "ReceiveOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestInquiry_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierBlackListHistory_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "SupplierBlackListHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierContract_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "SupplierContract");

            migrationBuilder.DropIndex(
                name: "IX_RequestInquiry_LogisticsRequestId",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.DropIndex(
                name: "IX_GoodsType_Code",
                schema: "Logistics",
                table: "GoodsType");

            migrationBuilder.DropIndex(
                name: "IX_GoodsCoding_GoodsId",
                schema: "Logistics",
                table: "GoodsCoding");

            //migrationBuilder.DropIndex(
            //    name: "IX_GoodsCategory_Code",
            //    schema: "Logistics",
            //    table: "GoodsCategory");

            migrationBuilder.DropColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.DropColumn(
                name: "EachPrice",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropColumn(
                name: "GoodsId",
                schema: "Logistics",
                table: "GoodsCoding");

            migrationBuilder.DropColumn(
                name: "IsRequireItConfirmation",
                schema: "Logistics",
                table: "SupplierRank");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "FaxNumber",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "IbanNo",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "NationalCode",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "RegisterNumber",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "SupplierType",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.RenameTable(
                name: "SupplierRank",
                schema: "Logistics",
                newName: "SupplierRank",
                newSchema: "Basic");

            migrationBuilder.RenameTable(
                name: "SupplierDocument",
                schema: "Logistics",
                newName: "SupplierDocument",
                newSchema: "Basic");

            migrationBuilder.RenameTable(
                name: "SupplierBlackListHistory",
                schema: "Logistics",
                newName: "SupplierBlackListHistory",
                newSchema: "Basic");

            migrationBuilder.RenameTable(
                name: "Supplier",
                schema: "Logistics",
                newName: "Supplier",
                newSchema: "Basic");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "SupplierContract",
                newName: "CandidatedSupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierContract_LogisticsRequestId",
                schema: "Logistics",
                table: "SupplierContract",
                newName: "IX_SupplierContract_CandidatedSupplierId");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "ReceiveOrder",
                newName: "OrderingId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiveOrder_LogisticsRequestId",
                schema: "Logistics",
                table: "ReceiveOrder",
                newName: "IX_ReceiveOrder_OrderingId");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentHistory",
                newName: "OrderingId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentHistory_LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentHistory",
                newName: "IX_PaymentHistory_OrderingId");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentCommand",
                newName: "OrderingId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCommand_LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentCommand",
                newName: "IX_PaymentCommand_OrderingId");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "GoodsCoding",
                newName: "LogisticsSupplyGoodsId");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsCoding_LogisticsRequestId",
                schema: "Logistics",
                table: "GoodsCoding",
                newName: "IX_GoodsCoding_LogisticsSupplyGoodsId");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "CandidatedSupplier",
                newName: "LogisticsSupplyId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidatedSupplier_LogisticsRequestId",
                schema: "Logistics",
                table: "CandidatedSupplier",
                newName: "IX_CandidatedSupplier_LogisticsSupplyId");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Basic",
                table: "SupplierBlackListHistory",
                newName: "OrderingId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierBlackListHistory_LogisticsRequestId",
                schema: "Basic",
                table: "SupplierBlackListHistory",
                newName: "IX_SupplierBlackListHistory_OrderingId");

            migrationBuilder.AlterColumn<string>(
                name: "IsWrittenInquiry",
                schema: "Logistics",
                table: "RequestInquiry",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsContractRequired",
                schema: "Logistics",
                table: "RequestInquiry",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsPrePaymentRequired",
                schema: "Logistics",
                table: "RequestInquiry",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsPrePayment",
                schema: "Logistics",
                table: "PaymentCommand",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GoodsId",
                schema: "Logistics",
                table: "OrderingItem",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedPrice",
                schema: "Logistics",
                table: "LogisticsSupplyGoods",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Logistics",
                table: "LogisticsSupplyGoods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GoodsId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Logistics",
                table: "GoodsType",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsRequireItConfirmation",
                schema: "Logistics",
                table: "GoodsType",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "Logistics",
                table: "GoodsType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Logistics",
                table: "GoodsType",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Logistics",
                table: "GoodsType",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Logistics",
                table: "GoodsStatus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "IsRequiredItConfirmation",
                schema: "Logistics",
                table: "GoodsStatus",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "Logistics",
                table: "GoodsStatus",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Logistics",
                table: "GoodsStatus",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "IsFinal",
                schema: "Logistics",
                table: "GoodsStatus",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "Logistics",
                table: "GoodsCategorySupplier",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Logistics",
                table: "GoodsCategorySupplier",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsTechnological",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsRequiredSecurityCheck",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsHardware",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsGoods",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsSelected",
                schema: "Logistics",
                table: "CandidatedSupplier",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierSuccessOrderCountForm",
                schema: "Basic",
                table: "SupplierRank",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierSuccessOrderCountTo",
                schema: "Basic",
                table: "SupplierRank",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuccessOrderCountinTheYear",
                schema: "Basic",
                table: "Supplier",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SupplierAccountList",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierAccountList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierAccountList_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Basic",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierAddressBook",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    AddressTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierAddressBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierAddressBook_AddressType_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalSchema: "Basic",
                        principalTable: "AddressType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierAddressBook_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Basic",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierPhoneBook",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    PhoneTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPhoneBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierPhoneBook_PhonType_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalSchema: "Basic",
                        principalTable: "PhonType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierPhoneBook_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Basic",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequest_RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCategory_Code",
                schema: "Logistics",
                table: "GoodsCategory",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAccountList_SupplierId",
                schema: "Basic",
                table: "SupplierAccountList",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAddressBook_AddressTypeId",
                schema: "Basic",
                table: "SupplierAddressBook",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAddressBook_SupplierId",
                schema: "Basic",
                table: "SupplierAddressBook",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPhoneBook_PhoneTypeId",
                schema: "Basic",
                table: "SupplierPhoneBook",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPhoneBook_SupplierId",
                schema: "Basic",
                table: "SupplierPhoneBook",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidatedSupplier_LogisticsSupply_LogisticsSupplyId",
                schema: "Logistics",
                table: "CandidatedSupplier",
                column: "LogisticsSupplyId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupply",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsCoding_LogisticsSupplyGoods_LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "GoodsCoding",
                column: "LogisticsSupplyGoodsId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyGoods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogisticsRequest_Users_RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest",
                column: "RequesterId",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCommand_Ordering_OrderingId",
                schema: "Logistics",
                table: "PaymentCommand",
                column: "OrderingId",
                principalSchema: "Logistics",
                principalTable: "Ordering",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistory_Ordering_OrderingId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "OrderingId",
                principalSchema: "Logistics",
                principalTable: "Ordering",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveOrder_Ordering_OrderingId",
                schema: "Logistics",
                table: "ReceiveOrder",
                column: "OrderingId",
                principalSchema: "Logistics",
                principalTable: "Ordering",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierBlackListHistory_Ordering_OrderingId",
                schema: "Basic",
                table: "SupplierBlackListHistory",
                column: "OrderingId",
                principalSchema: "Logistics",
                principalTable: "Ordering",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierContract_CandidatedSupplier_CandidatedSupplierId",
                schema: "Logistics",
                table: "SupplierContract",
                column: "CandidatedSupplierId",
                principalSchema: "Logistics",
                principalTable: "CandidatedSupplier",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidatedSupplier_LogisticsSupply_LogisticsSupplyId",
                schema: "Logistics",
                table: "CandidatedSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsCoding_LogisticsSupplyGoods_LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "GoodsCoding");

            migrationBuilder.DropForeignKey(
                name: "FK_LogisticsRequest_Users_RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCommand_Ordering_OrderingId",
                schema: "Logistics",
                table: "PaymentCommand");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistory_Ordering_OrderingId",
                schema: "Logistics",
                table: "PaymentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveOrder_Ordering_OrderingId",
                schema: "Logistics",
                table: "ReceiveOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierBlackListHistory_Ordering_OrderingId",
                schema: "Basic",
                table: "SupplierBlackListHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierContract_CandidatedSupplier_CandidatedSupplierId",
                schema: "Logistics",
                table: "SupplierContract");

            migrationBuilder.DropTable(
                name: "SupplierAccountList",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "SupplierAddressBook",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "SupplierPhoneBook",
                schema: "Basic");

            migrationBuilder.DropIndex(
                name: "IX_LogisticsRequest_RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest");

            migrationBuilder.DropIndex(
                name: "IX_GoodsCategory_Code",
                schema: "Logistics",
                table: "GoodsCategory");

            migrationBuilder.DropColumn(
                name: "IsContractRequired",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.DropColumn(
                name: "IsPrePaymentRequired",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest");

            migrationBuilder.DropColumn(
                name: "IsFinal",
                schema: "Logistics",
                table: "GoodsStatus");

            migrationBuilder.DropColumn(
                name: "SupplierSuccessOrderCountForm",
                schema: "Basic",
                table: "SupplierRank");

            migrationBuilder.DropColumn(
                name: "SupplierSuccessOrderCountTo",
                schema: "Basic",
                table: "SupplierRank");

            migrationBuilder.DropColumn(
                name: "SuccessOrderCountinTheYear",
                schema: "Basic",
                table: "Supplier");

            migrationBuilder.RenameTable(
                name: "SupplierRank",
                schema: "Basic",
                newName: "SupplierRank",
                newSchema: "Logistics");

            migrationBuilder.RenameTable(
                name: "SupplierDocument",
                schema: "Basic",
                newName: "SupplierDocument",
                newSchema: "Logistics");

            migrationBuilder.RenameTable(
                name: "SupplierBlackListHistory",
                schema: "Basic",
                newName: "SupplierBlackListHistory",
                newSchema: "Logistics");

            migrationBuilder.RenameTable(
                name: "Supplier",
                schema: "Basic",
                newName: "Supplier",
                newSchema: "Logistics");

            migrationBuilder.RenameColumn(
                name: "CandidatedSupplierId",
                schema: "Logistics",
                table: "SupplierContract",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierContract_CandidatedSupplierId",
                schema: "Logistics",
                table: "SupplierContract",
                newName: "IX_SupplierContract_LogisticsRequestId");

            migrationBuilder.RenameColumn(
                name: "OrderingId",
                schema: "Logistics",
                table: "ReceiveOrder",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiveOrder_OrderingId",
                schema: "Logistics",
                table: "ReceiveOrder",
                newName: "IX_ReceiveOrder_LogisticsRequestId");

            migrationBuilder.RenameColumn(
                name: "OrderingId",
                schema: "Logistics",
                table: "PaymentHistory",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentHistory_OrderingId",
                schema: "Logistics",
                table: "PaymentHistory",
                newName: "IX_PaymentHistory_LogisticsRequestId");

            migrationBuilder.RenameColumn(
                name: "OrderingId",
                schema: "Logistics",
                table: "PaymentCommand",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCommand_OrderingId",
                schema: "Logistics",
                table: "PaymentCommand",
                newName: "IX_PaymentCommand_LogisticsRequestId");

            migrationBuilder.RenameColumn(
                name: "LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "GoodsCoding",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsCoding_LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "GoodsCoding",
                newName: "IX_GoodsCoding_LogisticsRequestId");

            migrationBuilder.RenameColumn(
                name: "LogisticsSupplyId",
                schema: "Logistics",
                table: "CandidatedSupplier",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidatedSupplier_LogisticsSupplyId",
                schema: "Logistics",
                table: "CandidatedSupplier",
                newName: "IX_CandidatedSupplier_LogisticsRequestId");

            migrationBuilder.RenameColumn(
                name: "OrderingId",
                schema: "Logistics",
                table: "SupplierBlackListHistory",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierBlackListHistory_OrderingId",
                schema: "Logistics",
                table: "SupplierBlackListHistory",
                newName: "IX_SupplierBlackListHistory_LogisticsRequestId");

            migrationBuilder.AlterColumn<string>(
                name: "IsWrittenInquiry",
                schema: "Logistics",
                table: "RequestInquiry",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldFixedLength: true,
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "RequestInquiry",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "IsPrePayment",
                schema: "Logistics",
                table: "PaymentCommand",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldFixedLength: true,
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GoodsId",
                schema: "Logistics",
                table: "OrderingItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedPrice",
                schema: "Logistics",
                table: "LogisticsSupplyGoods",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Logistics",
                table: "LogisticsSupplyGoods",
                type: "nvarchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GoodsId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "nvarchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EachPrice",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Logistics",
                table: "GoodsType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "IsRequireItConfirmation",
                schema: "Logistics",
                table: "GoodsType",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "Logistics",
                table: "GoodsType",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Logistics",
                table: "GoodsType",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Logistics",
                table: "GoodsType",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Logistics",
                table: "GoodsStatus",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IsRequiredItConfirmation",
                schema: "Logistics",
                table: "GoodsStatus",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldFixedLength: true,
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "Logistics",
                table: "GoodsStatus",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Logistics",
                table: "GoodsStatus",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<long>(
                name: "GoodsId",
                schema: "Logistics",
                table: "GoodsCoding",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "Logistics",
                table: "GoodsCategorySupplier",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Logistics",
                table: "GoodsCategorySupplier",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "IsTechnological",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IsRequiredSecurityCheck",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IsHardware",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IsGoods",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "IsSelected",
                schema: "Logistics",
                table: "CandidatedSupplier",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldFixedLength: true,
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsRequireItConfirmation",
                schema: "Logistics",
                table: "SupplierRank",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaxNumber",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IbanNo",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterNumber",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(26)",
                maxLength: 26,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierType",
                schema: "Logistics",
                table: "Supplier",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestInquiry_LogisticsRequestId",
                schema: "Logistics",
                table: "RequestInquiry",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsType_Code",
                schema: "Logistics",
                table: "GoodsType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCoding_GoodsId",
                schema: "Logistics",
                table: "GoodsCoding",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCategory_Code",
                schema: "Logistics",
                table: "GoodsCategory",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidatedSupplier_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "CandidatedSupplier",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsCoding_Goods_GoodsId",
                schema: "Logistics",
                table: "GoodsCoding",
                column: "GoodsId",
                principalSchema: "Logistics",
                principalTable: "Goods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsCoding_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "GoodsCoding",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCommand_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentCommand",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistory_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveOrder_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "ReceiveOrder",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestInquiry_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "RequestInquiry",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierBlackListHistory_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "SupplierBlackListHistory",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierContract_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "SupplierContract",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");
        }
    }
}
