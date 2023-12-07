#region using

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace ApiWykazuPodatnikowVatData.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                "awpv");

            migrationBuilder.CreateTable(
                "RequestAndResponseHistory",
                schema: "awpv",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser =
                        table.Column<string>("varchar(512)", maxLength: 512, nullable: false),
                    RequestUrl = table.Column<string>("varchar(512)", maxLength: 512, nullable: false),
                    RequestParameters = table.Column<string>("varchar(max)", nullable: false),
                    RequestBody = table.Column<string>("varchar(max)", nullable: false),
                    ResponseStatusCode = table.Column<string>("varchar(32)", maxLength: 32, nullable: false),
                    ResponseHeaders = table.Column<string>("varchar(max)", nullable: false),
                    ResponseContent = table.Column<string>("varchar(max)", nullable: false),
                    RequestDateTime = table.Column<string>("varchar(32)", maxLength: 32, nullable: false),
                    RequestId = table.Column<string>("varchar(32)", maxLength: 32, nullable: false),
                    RequestDateTimeAsDateTime = table.Column<DateTime>("datetime", nullable: true),
                    DateOfCreate =
                        table.Column<DateTime>("datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>("datetime", nullable: true),
                    ObjectMD5Hash = table.Column<string>("varchar(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAndResponseHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Entity",
                schema: "awpv",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser =
                        table.Column<string>("varchar(512)", maxLength: 512, nullable: false),
                    RequestAndResponseHistoryId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>("varchar(256)", maxLength: 256, nullable: false),
                    Nip = table.Column<string>("varchar(10)", maxLength: 10, nullable: true),
                    StatusVat = table.Column<string>("varchar(32)", maxLength: 32, nullable: true),
                    Regon = table.Column<string>("varchar(14)", maxLength: 14, nullable: true),
                    Pesel = table.Column<string>("varchar(11)", maxLength: 11, nullable: true),
                    Krs = table.Column<string>("varchar(10)", maxLength: 10, nullable: true),
                    ResidenceAddress = table.Column<string>("varchar(200)", maxLength: 200, nullable: true),
                    WorkingAddress = table.Column<string>("varchar(200)", maxLength: 200, nullable: true),
                    RegistrationLegalDate = table.Column<DateTime>("date", nullable: true),
                    RegistrationDenialDate = table.Column<DateTime>("date", nullable: true),
                    RegistrationDenialBasis = table.Column<string>("varchar(200)", maxLength: 200, nullable: true),
                    RestorationDate = table.Column<DateTime>("date", nullable: true),
                    RestorationBasis = table.Column<string>("varchar(200)", maxLength: 200, nullable: true),
                    RemovalDate = table.Column<DateTime>("date", nullable: true),
                    RemovalBasis = table.Column<string>("varchar(200)", maxLength: 200, nullable: true),
                    HasVirtualAccounts = table.Column<bool>("bit", nullable: false),
                    DateOfCreate =
                        table.Column<DateTime>("datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>("datetime", nullable: true),
                    DateOfChecking = table.Column<DateTime>("datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                    table.ForeignKey(
                        "FK_Entity_RequestAndResponseHistory_RequestAndResponseHistoryId",
                        x => x.RequestAndResponseHistoryId,
                        principalSchema: "awpv",
                        principalTable: "RequestAndResponseHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "EntityCheck",
                schema: "awpv",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser =
                        table.Column<string>("varchar(512)", maxLength: 512, nullable: false),
                    RequestAndResponseHistoryId = table.Column<Guid>(nullable: true),
                    Nip = table.Column<string>("varchar(10)", maxLength: 10, nullable: true),
                    Regon = table.Column<string>("varchar(14)", maxLength: 14, nullable: true),
                    AccountNumber = table.Column<string>("varchar(32)", maxLength: 32, nullable: true),
                    AccountAssigned = table.Column<string>("varchar(3)", maxLength: 3, nullable: false),
                    RequestDateTime = table.Column<string>("varchar(19)", maxLength: 19, nullable: false),
                    RequestDateTimeAsDate = table.Column<DateTime>("datetime", nullable: true),
                    RequestId = table.Column<string>("varchar(18)", maxLength: 18, nullable: false),
                    DateOfCreate =
                        table.Column<DateTime>("datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>("datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityCheck", x => x.Id);
                    table.ForeignKey(
                        "FK_EntityCheck_RequestAndResponseHistory_RequestAndResponseHistoryId",
                        x => x.RequestAndResponseHistoryId,
                        principalSchema: "awpv",
                        principalTable: "RequestAndResponseHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "EntityAccountNumber",
                schema: "awpv",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser =
                        table.Column<string>("varchar(512)", maxLength: 512, nullable: false),
                    EntityId = table.Column<Guid>(nullable: true),
                    AccountNumber = table.Column<string>("varchar(32)", maxLength: 32, nullable: false),
                    DateOfCreate =
                        table.Column<DateTime>("datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>("datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAccountNumber", x => x.Id);
                    table.ForeignKey(
                        "FK_EntityAccountNumber_Entity_EntityId",
                        x => x.EntityId,
                        principalSchema: "awpv",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "EntityPerson",
                schema: "awpv",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    UniqueIdentifierOfTheLoggedInUser =
                        table.Column<string>("varchar(512)", maxLength: 512, nullable: false),
                    EntityRepresentativeId = table.Column<Guid>(nullable: true),
                    EntityAuthorizedClerkId = table.Column<Guid>(nullable: true),
                    EntityPartnerId = table.Column<Guid>(nullable: true),
                    Pesel = table.Column<string>("varchar(11)", maxLength: 11, nullable: true),
                    CompanyName = table.Column<string>("varchar(256)", maxLength: 256, nullable: true),
                    FirstName = table.Column<string>("varchar(60)", maxLength: 60, nullable: true),
                    LastName = table.Column<string>("varchar(160)", maxLength: 160, nullable: true),
                    Nip = table.Column<string>("varchar(10)", maxLength: 10, nullable: true),
                    DateOfCreate =
                        table.Column<DateTime>("datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfModification = table.Column<DateTime>("datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPerson", x => x.Id);
                    table.ForeignKey(
                        "FK_EntityPerson_Entity_EntityAuthorizedClerkId",
                        x => x.EntityAuthorizedClerkId,
                        principalSchema: "awpv",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_EntityPerson_Entity_EntityPartnerId",
                        x => x.EntityPartnerId,
                        principalSchema: "awpv",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_EntityPerson_Entity_EntityRepresentativeId",
                        x => x.EntityRepresentativeId,
                        principalSchema: "awpv",
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_EntityDateOfChecking",
                schema: "awpv",
                table: "Entity",
                column: "DateOfChecking");

            migrationBuilder.CreateIndex(
                "IX_EntityDateOfCreate",
                schema: "awpv",
                table: "Entity",
                column: "DateOfCreate");

            migrationBuilder.CreateIndex(
                "IX_EntityDateOfModification",
                schema: "awpv",
                table: "Entity",
                column: "DateOfModification");

            migrationBuilder.CreateIndex(
                "IX_EntityId",
                schema: "awpv",
                table: "Entity",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_EntityKrs",
                schema: "awpv",
                table: "Entity",
                column: "Krs");

            migrationBuilder.CreateIndex(
                "IX_EntityName",
                schema: "awpv",
                table: "Entity",
                column: "Name");

            migrationBuilder.CreateIndex(
                "IX_EntityNip",
                schema: "awpv",
                table: "Entity",
                column: "Nip");

            migrationBuilder.CreateIndex(
                "IX_EntityPesel",
                schema: "awpv",
                table: "Entity",
                column: "Pesel",
                unique: true,
                filter: "[Pesel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_EntityRegon",
                schema: "awpv",
                table: "Entity",
                column: "Regon");

            migrationBuilder.CreateIndex(
                "IX_EntityRequestAndResponseHistoryId",
                schema: "awpv",
                table: "Entity",
                column: "RequestAndResponseHistoryId");

            migrationBuilder.CreateIndex(
                "IX_EntityUniqueIdentifierOfTheLoggedInUser",
                schema: "awpv",
                table: "Entity",
                column: "UniqueIdentifierOfTheLoggedInUser");

            migrationBuilder.CreateIndex(
                "IX_EntityAccountNumberAccountNumber",
                schema: "awpv",
                table: "EntityAccountNumber",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                "IX_EntityAccountEntityId",
                schema: "awpv",
                table: "EntityAccountNumber",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                "IX_EntityAccountNumberId",
                schema: "awpv",
                table: "EntityAccountNumber",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_EntityAccountNumberUniqueIdentifierOfTheLoggedInUser",
                schema: "awpv",
                table: "EntityAccountNumber",
                column: "UniqueIdentifierOfTheLoggedInUser");

            migrationBuilder.CreateIndex(
                "IX_EntityAccountNumberUniqueKey",
                schema: "awpv",
                table: "EntityAccountNumber",
                columns: new[] {"EntityId", "AccountNumber"},
                unique: true,
                filter: "[EntityId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_EntityCheckAccountNumber",
                schema: "awpv",
                table: "EntityCheck",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                "IX_EntityCheckDateOfCreate",
                schema: "awpv",
                table: "EntityCheck",
                column: "DateOfCreate");

            migrationBuilder.CreateIndex(
                "IX_EntityCheckDateOfModification",
                schema: "awpv",
                table: "EntityCheck",
                column: "DateOfModification");

            migrationBuilder.CreateIndex(
                "IX_EntityCheckId",
                schema: "awpv",
                table: "EntityCheck",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_EntityCheckNip",
                schema: "awpv",
                table: "EntityCheck",
                column: "Nip");

            migrationBuilder.CreateIndex(
                "IX_EntityCheckRegon",
                schema: "awpv",
                table: "EntityCheck",
                column: "Regon");

            migrationBuilder.CreateIndex(
                "IX_EntityCheckRequestAndResponseHistoryId",
                schema: "awpv",
                table: "EntityCheck",
                column: "RequestAndResponseHistoryId");

            migrationBuilder.CreateIndex(
                "IX_EntityCheckRequestId",
                schema: "awpv",
                table: "EntityCheck",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                "IX_EntityCheckUniqueIdentifierOfTheLoggedInUser",
                schema: "awpv",
                table: "EntityCheck",
                column: "UniqueIdentifierOfTheLoggedInUser");

            migrationBuilder.CreateIndex(
                "IX_EntityPersonCompanyName",
                schema: "awpv",
                table: "EntityPerson",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                "IX_EntityPersonEntityAuthorizedClerkId",
                schema: "awpv",
                table: "EntityPerson",
                column: "EntityAuthorizedClerkId");

            migrationBuilder.CreateIndex(
                "IX_EntityPersonEntityPartnerId",
                schema: "awpv",
                table: "EntityPerson",
                column: "EntityPartnerId");

            migrationBuilder.CreateIndex(
                "IX_EntityPersonEntityRepresentativeId",
                schema: "awpv",
                table: "EntityPerson",
                column: "EntityRepresentativeId");

            migrationBuilder.CreateIndex(
                "IX_EntityPersonId",
                schema: "awpv",
                table: "EntityPerson",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_EntityNip",
                schema: "awpv",
                table: "EntityPerson",
                column: "Nip");

            migrationBuilder.CreateIndex(
                "IX_EntityPersonPesel",
                schema: "awpv",
                table: "EntityPerson",
                column: "Pesel");

            migrationBuilder.CreateIndex(
                "IX_EntityPersonUniqueIdentifierOfTheLoggedInUser",
                schema: "awpv",
                table: "EntityPerson",
                column: "UniqueIdentifierOfTheLoggedInUser");

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryDateOfCreate",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "DateOfCreate");

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryDateOfModification",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "DateOfModification");

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryId",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryRequestObjectMD5Hash",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "ObjectMD5Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryRequestDateTime",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "RequestDateTime");

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryRequestDateTimeAsDateTime",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "RequestDateTimeAsDateTime");

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryRequestId",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryRequestUrl",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "RequestUrl");

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryResponseStatusCode",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "ResponseStatusCode");

            migrationBuilder.CreateIndex(
                "IX_RequestAndResponseHistoryUniqueIdentifierOfTheLoggedInUser",
                schema: "awpv",
                table: "RequestAndResponseHistory",
                column: "UniqueIdentifierOfTheLoggedInUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "EntityAccountNumber",
                "awpv");

            migrationBuilder.DropTable(
                "EntityCheck",
                "awpv");

            migrationBuilder.DropTable(
                "EntityPerson",
                "awpv");

            migrationBuilder.DropTable(
                "Entity",
                "awpv");

            migrationBuilder.DropTable(
                "RequestAndResponseHistory",
                "awpv");
        }
    }
}
