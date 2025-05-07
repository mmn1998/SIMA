using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class changeservicecatalog3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalActivity_Department_TechnicalSupervisorDepartmentId",
                schema: "ServiceCatalog",
                table: "CriticalActivity",
                column: "TechnicalSupervisorDepartmentId",
                principalSchema: "Organization",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiRequestBodyParam_ApiVersion_ApiVersionId",
                schema: "ServiceCatalog",
                table: "ApiRequestBodyParam",
                column: "ApiVersionId",
                principalSchema: "ServiceCatalog",
                principalTable: "ApiVersion",
                principalColumn: "Id");
            migrationBuilder.DropForeignKey(
               name: "FK_CriticalActivity_Department_TechnicalSupervisorDepartmentId",
               schema: "ServiceCatalog",
               table: "CriticalActivity");
        }
    }
}
