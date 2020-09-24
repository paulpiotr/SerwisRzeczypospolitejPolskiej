using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "varchar(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(18)",
                oldMaxLength: 18,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestDateTime",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "varchar(19)",
                maxLength: 19,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "AccountAssigned",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldMaxLength: 3,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "varchar(18)",
                maxLength: 18,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(18)",
                oldMaxLength: 18);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDateTime",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(19)",
                oldMaxLength: 19);

            migrationBuilder.AlterColumn<string>(
                name: "AccountAssigned",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                type: "varchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldMaxLength: 3);
        }
    }
}
