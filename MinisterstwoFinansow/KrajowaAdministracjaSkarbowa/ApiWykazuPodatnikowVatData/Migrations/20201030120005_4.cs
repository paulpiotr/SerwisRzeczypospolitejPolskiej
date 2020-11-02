using Microsoft.EntityFrameworkCore.Migrations;
using NetAppCommon.Helpers.Db.Mssql;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AuditMigration.Create(migrationBuilder, "awpv", "RequestAndResponseHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            AuditMigration.Drop(migrationBuilder, "awpv", "RequestAndResponseHistory");
        }
    }
}