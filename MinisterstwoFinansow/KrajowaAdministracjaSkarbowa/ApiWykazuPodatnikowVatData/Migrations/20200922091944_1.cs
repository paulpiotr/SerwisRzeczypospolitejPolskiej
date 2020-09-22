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
                name: "ApiWykazuPodatnikowVatDataEntityPesel",
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
                    table.PrimaryKey("PK_ApiWykazuPodatnikowVatDataEntityPesel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiWykazuPodatnikowVatDataEntity",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Nip = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    StatusVat = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    Regon = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    ApiWykazuPodatnikowVatDataEntityPeselId = table.Column<Guid>(nullable: true),
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
                    table.PrimaryKey("PK_ApiWykazuPodatnikowVatDataEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiWykazuPodatnikowVatDataEntity_ApiWykazuPodatnikowVatDataEntityPesel_ApiWykazuPodatnikowVatDataEntityPeselId",
                        column: x => x.ApiWykazuPodatnikowVatDataEntityPeselId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "ApiWykazuPodatnikowVatDataEntityPesel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiWykazuPodatnikowVatDataEntityAccountNumber",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApiWykazuPodatnikowVatDataEntityId = table.Column<Guid>(nullable: true),
                    AccountNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiWykazuPodatnikowVatDataEntityAccountNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiWykazuPodatnikowVatDataEntityAccountNumber_ApiWykazuPodatnikowVatDataEntity_ApiWykazuPodatnikowVatDataEntityId",
                        column: x => x.ApiWykazuPodatnikowVatDataEntityId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "ApiWykazuPodatnikowVatDataEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiWykazuPodatnikowVatDataEntityPerson",
                schema: "ApiWykazuPodatnikowVat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApiWykazuPodatnikowVatDataEntityRepresentativesId = table.Column<Guid>(nullable: true),
                    ApiWykazuPodatnikowVatDataEntityAuthorizedClerksId = table.Column<Guid>(nullable: true),
                    ApiWykazuPodatnikowVatDataEntityPartnersId = table.Column<Guid>(nullable: true),
                    ApiWykazuPodatnikowVatDataEntityPeselId = table.Column<Guid>(nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    LastName = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: true),
                    Nip = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DateOfCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateOfModification = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiWykazuPodatnikowVatDataEntityPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiWykazuPodatnikowVatDataEntityPerson_ApiWykazuPodatnikowVatDataEntity_ApiWykazuPodatnikowVatDataEntityAuthorizedClerksId",
                        column: x => x.ApiWykazuPodatnikowVatDataEntityAuthorizedClerksId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "ApiWykazuPodatnikowVatDataEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApiWykazuPodatnikowVatDataEntityPerson_ApiWykazuPodatnikowVatDataEntity_ApiWykazuPodatnikowVatDataEntityPartnersId",
                        column: x => x.ApiWykazuPodatnikowVatDataEntityPartnersId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "ApiWykazuPodatnikowVatDataEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApiWykazuPodatnikowVatDataEntityPerson_ApiWykazuPodatnikowVatDataEntityPesel_ApiWykazuPodatnikowVatDataEntityPeselId",
                        column: x => x.ApiWykazuPodatnikowVatDataEntityPeselId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "ApiWykazuPodatnikowVatDataEntityPesel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApiWykazuPodatnikowVatDataEntityPerson_ApiWykazuPodatnikowVatDataEntity_ApiWykazuPodatnikowVatDataEntityRepresentativesId",
                        column: x => x.ApiWykazuPodatnikowVatDataEntityRepresentativesId,
                        principalSchema: "ApiWykazuPodatnikowVat",
                        principalTable: "ApiWykazuPodatnikowVatDataEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiWykazuPodatnikowVatDataEntity_ApiWykazuPodatnikowVatDataEntityPeselId",
                schema: "ApiWykazuPodatnikowVat",
                table: "ApiWykazuPodatnikowVatDataEntity",
                column: "ApiWykazuPodatnikowVatDataEntityPeselId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiWykazuPodatnikowVatDataEntityAccountNumber_ApiWykazuPodatnikowVatDataEntityId",
                schema: "ApiWykazuPodatnikowVat",
                table: "ApiWykazuPodatnikowVatDataEntityAccountNumber",
                column: "ApiWykazuPodatnikowVatDataEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiWykazuPodatnikowVatDataEntityPerson_ApiWykazuPodatnikowVatDataEntityAuthorizedClerksId",
                schema: "ApiWykazuPodatnikowVat",
                table: "ApiWykazuPodatnikowVatDataEntityPerson",
                column: "ApiWykazuPodatnikowVatDataEntityAuthorizedClerksId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiWykazuPodatnikowVatDataEntityPerson_ApiWykazuPodatnikowVatDataEntityPartnersId",
                schema: "ApiWykazuPodatnikowVat",
                table: "ApiWykazuPodatnikowVatDataEntityPerson",
                column: "ApiWykazuPodatnikowVatDataEntityPartnersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiWykazuPodatnikowVatDataEntityPerson_ApiWykazuPodatnikowVatDataEntityPeselId",
                schema: "ApiWykazuPodatnikowVat",
                table: "ApiWykazuPodatnikowVatDataEntityPerson",
                column: "ApiWykazuPodatnikowVatDataEntityPeselId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiWykazuPodatnikowVatDataEntityPerson_ApiWykazuPodatnikowVatDataEntityRepresentativesId",
                schema: "ApiWykazuPodatnikowVat",
                table: "ApiWykazuPodatnikowVatDataEntityPerson",
                column: "ApiWykazuPodatnikowVatDataEntityRepresentativesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiWykazuPodatnikowVatDataEntityAccountNumber",
                schema: "ApiWykazuPodatnikowVat");

            migrationBuilder.DropTable(
                name: "ApiWykazuPodatnikowVatDataEntityPerson",
                schema: "ApiWykazuPodatnikowVat");

            migrationBuilder.DropTable(
                name: "ApiWykazuPodatnikowVatDataEntity",
                schema: "ApiWykazuPodatnikowVat");

            migrationBuilder.DropTable(
                name: "ApiWykazuPodatnikowVatDataEntityPesel",
                schema: "ApiWykazuPodatnikowVat");
        }
    }
}
