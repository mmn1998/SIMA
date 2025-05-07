using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fixDataProcedureOutputParam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCenter_DataCenter_ParentId",
                schema: "DataProcedureInputParams",
                table: "DataCenter");

            migrationBuilder.DropForeignKey(
                name: "FK_DataCenter_DataProcedure_DataProcedureId",
                schema: "DataProcedureInputParams",
                table: "DataCenter");

            migrationBuilder.DropForeignKey(
                name: "FK_DataCenter_DataCenter_ParentId",
                schema: "DataProcedureOutputParam",
                table: "DataCenter");

            migrationBuilder.DropForeignKey(
                name: "FK_DataCenter_DataProcedure_DataProcedureId",
                schema: "DataProcedureOutputParam",
                table: "DataCenter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataCenter",
                schema: "DataProcedureOutputParam",
                table: "DataCenter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataCenter",
                schema: "DataProcedureInputParams",
                table: "DataCenter");

            migrationBuilder.RenameTable(
                name: "DataCenter",
                schema: "DataProcedureOutputParam",
                newName: "DataProcedureOutputParam",
                newSchema: "AssetAndConfiguration");

            migrationBuilder.RenameTable(
                name: "DataCenter",
                schema: "DataProcedureInputParams",
                newName: "DataProcedureInputParam",
                newSchema: "AssetAndConfiguration");

            migrationBuilder.RenameIndex(
                name: "IX_DataCenter_ParentId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureOutputParam",
                newName: "IX_DataProcedureOutputParam_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_DataCenter_DataProcedureId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureOutputParam",
                newName: "IX_DataProcedureOutputParam_DataProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_DataCenter_ParentId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureInputParam",
                newName: "IX_DataProcedureInputParam_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_DataCenter_DataProcedureId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureInputParam",
                newName: "IX_DataProcedureInputParam_DataProcedureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataProcedureOutputParam",
                schema: "AssetAndConfiguration",
                table: "DataProcedureOutputParam",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataProcedureInputParam",
                schema: "AssetAndConfiguration",
                table: "DataProcedureInputParam",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataProcedureInputParam_DataProcedureInputParam_ParentId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureInputParam",
                column: "ParentId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "DataProcedureInputParam",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataProcedureInputParam_DataProcedure_DataProcedureId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureInputParam",
                column: "DataProcedureId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "DataProcedure",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataProcedureOutputParam_DataProcedureOutputParam_ParentId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureOutputParam",
                column: "ParentId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "DataProcedureOutputParam",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataProcedureOutputParam_DataProcedure_DataProcedureId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureOutputParam",
                column: "DataProcedureId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "DataProcedure",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataProcedureInputParam_DataProcedureInputParam_ParentId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureInputParam");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProcedureInputParam_DataProcedure_DataProcedureId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureInputParam");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProcedureOutputParam_DataProcedureOutputParam_ParentId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureOutputParam");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProcedureOutputParam_DataProcedure_DataProcedureId",
                schema: "AssetAndConfiguration",
                table: "DataProcedureOutputParam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataProcedureOutputParam",
                schema: "AssetAndConfiguration",
                table: "DataProcedureOutputParam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataProcedureInputParam",
                schema: "AssetAndConfiguration",
                table: "DataProcedureInputParam");

            migrationBuilder.EnsureSchema(
                name: "DataProcedureInputParams");

            migrationBuilder.EnsureSchema(
                name: "DataProcedureOutputParam");

            migrationBuilder.RenameTable(
                name: "DataProcedureOutputParam",
                schema: "AssetAndConfiguration",
                newName: "DataCenter",
                newSchema: "DataProcedureOutputParam");

            migrationBuilder.RenameTable(
                name: "DataProcedureInputParam",
                schema: "AssetAndConfiguration",
                newName: "DataCenter",
                newSchema: "DataProcedureInputParams");

            migrationBuilder.RenameIndex(
                name: "IX_DataProcedureOutputParam_ParentId",
                schema: "DataProcedureOutputParam",
                table: "DataCenter",
                newName: "IX_DataCenter_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_DataProcedureOutputParam_DataProcedureId",
                schema: "DataProcedureOutputParam",
                table: "DataCenter",
                newName: "IX_DataCenter_DataProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_DataProcedureInputParam_ParentId",
                schema: "DataProcedureInputParams",
                table: "DataCenter",
                newName: "IX_DataCenter_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_DataProcedureInputParam_DataProcedureId",
                schema: "DataProcedureInputParams",
                table: "DataCenter",
                newName: "IX_DataCenter_DataProcedureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataCenter",
                schema: "DataProcedureOutputParam",
                table: "DataCenter",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataCenter",
                schema: "DataProcedureInputParams",
                table: "DataCenter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCenter_DataCenter_ParentId",
                schema: "DataProcedureInputParams",
                table: "DataCenter",
                column: "ParentId",
                principalSchema: "DataProcedureInputParams",
                principalTable: "DataCenter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCenter_DataProcedure_DataProcedureId",
                schema: "DataProcedureInputParams",
                table: "DataCenter",
                column: "DataProcedureId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "DataProcedure",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCenter_DataCenter_ParentId",
                schema: "DataProcedureOutputParam",
                table: "DataCenter",
                column: "ParentId",
                principalSchema: "DataProcedureOutputParam",
                principalTable: "DataCenter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCenter_DataProcedure_DataProcedureId",
                schema: "DataProcedureOutputParam",
                table: "DataCenter",
                column: "DataProcedureId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "DataProcedure",
                principalColumn: "Id");
        }
    }
}
