using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EntityAccountNumberAccountNumber",
                schema: "awpv",
                table: "EntityAccountNumber");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAccountNumber_EntityId",
                schema: "awpv",
                table: "EntityAccountNumber",
                newName: "IX_EntityAccountEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumberAccountNumber",
                schema: "awpv",
                table: "EntityAccountNumber",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumberUniqueKey",
                schema: "awpv",
                table: "EntityAccountNumber",
                columns: new[] { "EntityId", "AccountNumber" },
                unique: true,
                filter: "[EntityId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EntityAccountNumberAccountNumber",
                schema: "awpv",
                table: "EntityAccountNumber");

            migrationBuilder.DropIndex(
                name: "IX_EntityAccountNumberUniqueKey",
                schema: "awpv",
                table: "EntityAccountNumber");

            migrationBuilder.RenameIndex(
                name: "IX_EntityAccountEntityId",
                schema: "awpv",
                table: "EntityAccountNumber",
                newName: "IX_EntityAccountNumber_EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumberAccountNumber",
                schema: "awpv",
                table: "EntityAccountNumber",
                column: "AccountNumber",
                unique: true);
        }
    }
}
