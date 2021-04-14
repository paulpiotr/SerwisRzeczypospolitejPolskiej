#region using

using Microsoft.EntityFrameworkCore.Migrations;
using NetAppCommon.Helpers.Db.Mssql;

#endregion

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AuditMigration.Create(migrationBuilder, "awpv", "RequestAndResponseHistory");
            AuditMigration.Create(migrationBuilder, "awpv", "Entity");
            AuditMigration.Create(migrationBuilder, "awpv", "EntityCheck");
            AuditMigration.Create(migrationBuilder, "awpv", "EntityAccountNumber");
            AuditMigration.Create(migrationBuilder, "awpv", "EntityPerson");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            AuditMigration.Drop(migrationBuilder, "awpv", "RequestAndResponseHistory");
            AuditMigration.Drop(migrationBuilder, "awpv", "Entity");
            AuditMigration.Drop(migrationBuilder, "awpv", "EntityCheck");
            AuditMigration.Drop(migrationBuilder, "awpv", "EntityAccountNumber");
            AuditMigration.Drop(migrationBuilder, "awpv", "EntityPerson");
        }
    }
}
