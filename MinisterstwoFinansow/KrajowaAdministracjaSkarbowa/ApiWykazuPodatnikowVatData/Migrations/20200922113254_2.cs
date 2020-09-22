using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPesel",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPesel",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreate",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");
        }
    }
}
