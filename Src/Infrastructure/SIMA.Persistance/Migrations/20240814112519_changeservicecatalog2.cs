using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class changeservicecatalog2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");


            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_ApiRequestBodyParam_ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);


            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
