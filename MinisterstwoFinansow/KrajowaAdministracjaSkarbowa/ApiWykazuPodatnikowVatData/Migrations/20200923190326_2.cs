using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EntityAccountNumberUniqueIdentifierOfTheLoggedInUser",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumberUniqueIdentifierOfTheLoggedInUser",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber",
                column: "UniqueIdentifierOfTheLoggedInUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EntityAccountNumberUniqueIdentifierOfTheLoggedInUser",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumberUniqueIdentifierOfTheLoggedInUser",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber",
                column: "UniqueIdentifierOfTheLoggedInUser",
                unique: true);
        }
    }
}
