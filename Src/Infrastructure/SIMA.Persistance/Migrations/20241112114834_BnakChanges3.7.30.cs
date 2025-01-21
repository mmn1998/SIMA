using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class BnakChanges3730 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Broker_BrokerType",
                schema: "Bank",
                table: "Broker");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "Bank",
                table: "Broker");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Bank",
                table: "Broker");

            migrationBuilder.AlterColumn<long>(
                name: "BrokerTypeId",
                schema: "Bank",
                table: "Broker",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Broker_BrokerType",
                schema: "Bank",
                table: "Broker",
                column: "BrokerTypeId",
                principalSchema: "Bank",
                principalTable: "BrokerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Broker_BrokerType",
                schema: "Bank",
                table: "Broker");

            migrationBuilder.AlterColumn<long>(
                name: "BrokerTypeId",
                schema: "Bank",
                table: "Broker",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "Bank",
                table: "Broker",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Bank",
                table: "Broker",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Broker_BrokerType",
                schema: "Bank",
                table: "Broker",
                column: "BrokerTypeId",
                principalSchema: "Bank",
                principalTable: "BrokerType",
                principalColumn: "Id");
        }
    }
}
