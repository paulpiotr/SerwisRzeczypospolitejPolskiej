using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ApiWykazuPodatnikowVat");

            migrationBuilder.CreateTable(
                name: "EntityCheck",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountAssigned = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    RequestDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    RequestId = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: true),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityCheck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityPesel",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Pesel = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPesel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entity",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Nip = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    StatusVat = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    Regon = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    EntityPeselId = table.Column<Guid>(nullable: true),
                    Krs = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    ResidenceAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    WorkingAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Representatives = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    AuthorizedClerks = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    Partners = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    RegistrationLegalDate = table.Column<DateTime>(type: "date", nullable: true),
                    RegistrationDenialDate = table.Column<DateTime>(type: "date", nullable: true),
                    RegistrationDenialBasis = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    RestorationDate = table.Column<DateTime>(type: "date", nullable: true),
                    RestorationBasis = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    RemovalDate = table.Column<DateTime>(type: "date", nullable: true),
                    RemovalBasis = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    HasVirtualAccounts = table.Column<bool>(type: "bit", nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entity_EntityPesel_EntityPeselId",
                        column: x => x.EntityPeselId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "EntityPesel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityAccountNumber",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityId = table.Column<Guid>(nullable: true),
                    AccountNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
                    EntityRepresentativesId = table.Column<Guid>(nullable: true),
                    EntityAuthorizedClerksId = table.Column<Guid>(nullable: true),
                    EntityPartnersId = table.Column<Guid>(nullable: true),
                    EntityPeselId = table.Column<Guid>(nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    LastName = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: true),
                    Nip = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityPerson_Entity_EntityAuthorizedClerksId",
                        column: x => x.EntityAuthorizedClerksId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityPerson_Entity_EntityPartnersId",
                        column: x => x.EntityPartnersId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityPerson_EntityPesel_EntityPeselId",
                        column: x => x.EntityPeselId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "EntityPesel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityPerson_Entity_EntityRepresentativesId",
                        column: x => x.EntityRepresentativesId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entity_EntityPeselId",
                schema: "ApiWykazuPodatnikowVat",
                table: "Entity",
                column: "EntityPeselId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAccountNumber_EntityId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityAccountNumber",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPerson_EntityAuthorizedClerksId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "EntityAuthorizedClerksId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPerson_EntityPartnersId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "EntityPartnersId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPerson_EntityPeselId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "EntityPeselId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPerson_EntityRepresentativesId",
                schema: "ApiWykazuPodatnikowVat",
                table: "EntityPerson",
                column: "EntityRepresentativesId");
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

            migrationBuilder.DropTable(
                name: "EntityPesel",
                schema: "ApiWykazuPodatnikowVat");
        }
    }
}
