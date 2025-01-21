using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class modifyChannelAccessPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Port",
                schema: "ServiceCatalog",
                table: "ChannelAccessPoint",
                newName: "PortTo");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                schema: "ServiceCatalog",
                table: "ChannelAccessPoint",
                newName: "IpAddressTo");

            migrationBuilder.AddColumn<string>(
                name: "IpAddressFrom",
                schema: "ServiceCatalog",
                table: "ChannelAccessPoint",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortFrom",
                schema: "ServiceCatalog",
                table: "ChannelAccessPoint",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddressFrom",
                schema: "ServiceCatalog",
                table: "ChannelAccessPoint");

            migrationBuilder.DropColumn(
                name: "PortFrom",
                schema: "ServiceCatalog",
                table: "ChannelAccessPoint");

            migrationBuilder.RenameColumn(
                name: "PortTo",
                schema: "ServiceCatalog",
                table: "ChannelAccessPoint",
                newName: "Port");

            migrationBuilder.RenameColumn(
                name: "IpAddressTo",
                schema: "ServiceCatalog",
                table: "ChannelAccessPoint",
                newName: "IpAddress");
        }
    }
}
