using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCheckAccountNumber",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCheckNip",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                column: "Nip");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCheckRegon",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                column: "Regon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EntityCheckAccountNumber",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck");

            migrationBuilder.DropIndex(
                name: "IX_EntityCheckNip",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck");

            migrationBuilder.DropIndex(
                name: "IX_EntityCheckRegon",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck");
        }
    }
}
