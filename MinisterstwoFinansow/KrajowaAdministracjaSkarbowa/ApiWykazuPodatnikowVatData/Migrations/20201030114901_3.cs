using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequestAndResponseHistoryId",
                schema: "awpv",
                table: "EntityCheck",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfChecking",
                schema: "awpv",
                table: "Entity",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RequestAndResponseHistoryId",
                schema: "awpv",
                table: "Entity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestAndResponseHistory",
                schema: "awpv",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    RequestUrl = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    RequestParameters = table.Column<string>(type: "varchar(max)", nullable: false),
                    RequestBody = table.Column<string>(type: "varchar(max)", nullable: false),
                    ResponseStatusCode = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    ResponseHeaders = table.Column<string>(type: "varchar(max)", nullable: false),
                    ResponseContent = table.Column<string>(type: "varchar(max)", nullable: false),
                    RequestDateTime = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    RequestId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    RequestDateTimeAsDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true),
                    ObjectMD5Hash = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAndResponseHistory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityCheckDateOfCreate",
                schema: "awpv",
                table: "EntityCheck",
                column: "DateOfCreate");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCheckDateOfModification",
                schema: "awpv",
                table: "EntityCheck",
                column: "DateOfModification");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCheckRequestAndResponseHistoryId",
                schema: "awpv",
                table: "EntityCheck",
                column: "RequestAndResponseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDateOfChecking",
                schema: "awpv",
                table: "Entity",
                column: "DateOfChecking");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDateOfCreate",
                schema: "awpv",
                table: "Entity",
                column: "DateOfCreate");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDateOfModification",
                schema: "awpv",
                table: "Entity",
                column: "DateOfModification");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRequestAndResponseHistoryId",
                schema: "awpv",
                table: "Entity",
                column: "RequestAndResponseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryDateOfCreate",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "DateOfCreate");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryDateOfModification",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "DateOfModification");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryId",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryRequestObjectMD5Hash",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "ObjectMD5Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryRequestDateTime",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "RequestDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryRequestDateTimeAsDateTime",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "RequestDateTimeAsDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryRequestId",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryRequestUrl",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "RequestUrl");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryResponseStatusCode",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "ResponseStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAndResponseHistoryUniqueIdentifierOfTheLoggedInUser",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "UniqueIdentifierOfTheLoggedInUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_RequestAndResponseHistory_RequestAndResponseHistoryId",
                schema: "awpv",
                table: "Entity",
                column: "RequestAndResponseHistoryId",
                principalSchema: "awpv",
                principalTable: "RequestAndResponseHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityCheck_RequestAndResponseHistory_RequestAndResponseHistoryId",
                schema: "awpv",
                table: "EntityCheck",
                column: "RequestAndResponseHistoryId",
                principalSchema: "awpv",
                principalTable: "RequestAndResponseHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entity_RequestAndResponseHistory_RequestAndResponseHistoryId",
                schema: "awpv",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityCheck_RequestAndResponseHistory_RequestAndResponseHistoryId",
                schema: "awpv",
                table: "EntityCheck");

            migrationBuilder.DropTable(
                name: "RequestAndResponseHistory",
                schema: "awpv");

            migrationBuilder.DropIndex(
                name: "IX_EntityCheckDateOfCreate",
                schema: "awpv",
                table: "EntityCheck");

            migrationBuilder.DropIndex(
                name: "IX_EntityCheckDateOfModification",
                schema: "awpv",
                table: "EntityCheck");

            migrationBuilder.DropIndex(
                name: "IX_EntityCheckRequestAndResponseHistoryId",
                schema: "awpv",
                table: "EntityCheck");

            migrationBuilder.DropIndex(
                name: "IX_EntityDateOfChecking",
                schema: "awpv",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_EntityDateOfCreate",
                schema: "awpv",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_EntityDateOfModification",
                schema: "awpv",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_EntityRequestAndResponseHistoryId",
                schema: "awpv",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "RequestAndResponseHistoryId",
                schema: "awpv",
                table: "EntityCheck");

            migrationBuilder.DropColumn(
                name: "DateOfChecking",
                schema: "awpv",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "RequestAndResponseHistoryId",
                schema: "awpv",
                table: "Entity");
        }
    }
}
