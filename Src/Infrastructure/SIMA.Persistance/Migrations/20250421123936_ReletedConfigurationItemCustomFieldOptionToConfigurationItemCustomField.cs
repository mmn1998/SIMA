using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ReletedConfigurationItemCustomFieldOptionToConfigurationItemCustomField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemCustomFieldOption_ConfigurationItem_ConfigurationItemId",
                schema: "Asset",
                table: "ConfigurationItemCustomFieldOption");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemId",
                schema: "Asset",
                table: "ConfigurationItemCustomFieldOption",
                newName: "ConfigurationItemCustomFieldId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemCustomFieldOption_ConfigurationItemId",
                schema: "Asset",
                table: "ConfigurationItemCustomFieldOption",
                newName: "IX_ConfigurationItemCustomFieldOption_ConfigurationItemCustomFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemCustomFieldOption_ConfigurationItemCustomField_ConfigurationItemCustomFieldId",
                schema: "Asset",
                table: "ConfigurationItemCustomFieldOption",
                column: "ConfigurationItemCustomFieldId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItemCustomField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemCustomFieldOption_ConfigurationItemCustomField_ConfigurationItemCustomFieldId",
                schema: "Asset",
                table: "ConfigurationItemCustomFieldOption");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemCustomFieldId",
                schema: "Asset",
                table: "ConfigurationItemCustomFieldOption",
                newName: "ConfigurationItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemCustomFieldOption_ConfigurationItemCustomFieldId",
                schema: "Asset",
                table: "ConfigurationItemCustomFieldOption",
                newName: "IX_ConfigurationItemCustomFieldOption_ConfigurationItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemCustomFieldOption_ConfigurationItem_ConfigurationItemId",
                schema: "Asset",
                table: "ConfigurationItemCustomFieldOption",
                column: "ConfigurationItemId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
