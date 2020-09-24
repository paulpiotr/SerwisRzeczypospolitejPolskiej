using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDateTimeAsDate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestDateTimeAsDate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck");
        }
    }
}
