using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nip",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Regon",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "varchar(14)",
                maxLength: 14,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nip",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck");

            migrationBuilder.DropColumn(
                name: "Regon",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck");
        }
    }
}
