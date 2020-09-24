using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ApiWykazuPodatnikowVat");

            migrationBuilder.CreateTable(
                name: "Entity",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Nip = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    StatusVat = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    Regon = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    Pesel = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Krs = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    ResidenceAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    WorkingAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    RegistrationLegalDate = table.Column<DateTime>(type: "date", nullable: true),
                    RegistrationDenialDate = table.Column<DateTime>(type: "date", nullable: true),
                    RegistrationDenialBasis = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    RestorationDate = table.Column<DateTime>(type: "date", nullable: true),
                    RestorationBasis = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    RemovalDate = table.Column<DateTime>(type: "date", nullable: true),
                    RemovalBasis = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    HasVirtualAccounts = table.Column<bool>(type: "bit", nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityCheck",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    AccountAssigned = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    RequestDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    RequestId = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: true),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityCheck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityAccountNumber",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    EntityId = table.Column<Guid>(nullable: true),
                    AccountNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAccountNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityAccountNumber_Entity_EntityId",
                        column: x => x.EntityId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityPerson",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    EntityRepresentativeId = table.Column<Guid>(nullable: true),
                    EntityAuthorizedClerkId = table.Column<Guid>(nullable: true),
                    EntityPartnerId = table.Column<Guid>(nullable: true),
                    Pesel = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    LastName = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: true),
                    Nip = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityPerson_Entity_EntityAuthorizedClerkId",
                        column: x => x.EntityAuthorizedClerkId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityPerson_Entity_EntityPartnerId",
                        column: x => x.EntityPartnerId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityPerson_Entity_EntityRepresentativeId",
                        column: x => x.EntityRepresentativeId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityId",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityKrs",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                column: "Krs");

            migrationBuilder.CreateIndex(
                name: "IX_EntityName",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EntityNip",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                column: "Nip");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPesel",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                column: "Pesel",
                unique: true,
                filter: "[Pesel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRegon",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                column: "Regon");

            migrationBuilder.CreateIndex(
                name: "IX_EntityUniqueIdentifierOfTheLoggedInUser",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                column: "UniqueIdentifierOfTheLoggedInUser");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumberAccountNumber",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumber_EntityId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumberId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumberUniqueIdentifierOfTheLoggedInUser",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber",
                column: "UniqueIdentifierOfTheLoggedInUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityCheckId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityCheckRequestId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCheckUniqueIdentifierOfTheLoggedInUser",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityCheck",
                column: "UniqueIdentifierOfTheLoggedInUser");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPersonCompanyName",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPersonEntityAuthorizedClerkId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "EntityAuthorizedClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPersonEntityPartnerId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "EntityPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPersonEntityRepresentativeId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "EntityRepresentativeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPersonId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityNip",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "Nip");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPersonPesel",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "Pesel");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPersonUniqueIdentifierOfTheLoggedInUser",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "UniqueIdentifierOfTheLoggedInUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityAccountNumber",
                schema: "ApiWykazuPodatnikowVat");

            migrationBuilder.DropTable(
                name: "EntityCheck",
                schema: "ApiWykazuPodatnikowVat");

            migrationBuilder.DropTable(
                name: "EntityPerson",
                schema: "ApiWykazuPodatnikowVat");

            migrationBuilder.DropTable(
                name: "Entity",
                schema: "ApiWykazuPodatnikowVat");
        }
    }
}
