using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Notification");

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachment",
                schema: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachment_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageAttachment_Message_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "Notification",
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageChangeHistory",
                schema: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageChangeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageChangeHistory_Message_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "Notification",
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageGroupDisplay",
                schema: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageGroupDisplay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageGroupDisplay_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Authentication",
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageGroupDisplay_Message_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "Notification",
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessagePositionDisplay",
                schema: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagePositionDisplay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessagePositionDisplay_Message_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "Notification",
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessagePositionDisplay_Position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "Organization",
                        principalTable: "Position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageSeenStatistics",
                schema: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSeenStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageSeenStatistics_Message_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "Notification",
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageSeenStatistics_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachment_DocumentId",
                schema: "Notification",
                table: "MessageAttachment",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachment_MessageId",
                schema: "Notification",
                table: "MessageAttachment",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageChangeHistory_MessageId",
                schema: "Notification",
                table: "MessageChangeHistory",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageGroupDisplay_GroupId",
                schema: "Notification",
                table: "MessageGroupDisplay",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageGroupDisplay_MessageId",
                schema: "Notification",
                table: "MessageGroupDisplay",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagePositionDisplay_MessageId",
                schema: "Notification",
                table: "MessagePositionDisplay",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagePositionDisplay_PositionId",
                schema: "Notification",
                table: "MessagePositionDisplay",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSeenStatistics_MessageId",
                schema: "Notification",
                table: "MessageSeenStatistics",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSeenStatistics_StaffId",
                schema: "Notification",
                table: "MessageSeenStatistics",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageAttachment",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "MessageChangeHistory",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "MessageGroupDisplay",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "MessagePositionDisplay",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "MessageSeenStatistics",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "Message",
                schema: "Notification");
        }
    }
}
