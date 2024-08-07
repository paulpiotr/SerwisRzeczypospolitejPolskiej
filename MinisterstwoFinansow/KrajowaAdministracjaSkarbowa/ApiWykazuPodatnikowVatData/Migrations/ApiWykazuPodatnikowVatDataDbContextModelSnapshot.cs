#region using

using System;
using ApiWykazuPodatnikowVatData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#endregion

namespace ApiWykazuPodatnikowVatData.Migrations
{
    [DbContext(typeof(ApiWykazuPodatnikowVatDataDbContext))]
    internal class ApiWykazuPodatnikowVatDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiWykazuPodatnikowVatData.Models.Entity", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("(newsequentialid())");

                b.Property<DateTime?>("DateOfChecking")
                    .HasColumnName("DateOfChecking")
                    .HasColumnType("datetime");

                b.Property<DateTime>("DateOfCreate")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("DateOfCreate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                b.Property<DateTime?>("DateOfModification")
                    .HasColumnName("DateOfModification")
                    .HasColumnType("datetime");

                b.Property<bool>("HasVirtualAccounts")
                    .HasColumnName("HasVirtualAccounts")
                    .HasColumnType("bit");

                b.Property<string>("Krs")
                    .HasColumnName("Krs")
                    .HasColumnType("varchar(10)")
                    .HasMaxLength(10);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasColumnType("varchar(256)")
                    .HasMaxLength(256);

                b.Property<string>("Nip")
                    .HasColumnName("Nip")
                    .HasColumnType("varchar(10)")
                    .HasMaxLength(10);

                b.Property<string>("Pesel")
                    .HasColumnName("Pesel")
                    .HasColumnType("varchar(11)")
                    .HasMaxLength(11);

                b.Property<string>("RegistrationDenialBasis")
                    .HasColumnName("RegistrationDenialBasis")
                    .HasColumnType("varchar(200)")
                    .HasMaxLength(200);

                b.Property<DateTime?>("RegistrationDenialDate")
                    .HasColumnName("RegistrationDenialDate")
                    .HasColumnType("date");

                b.Property<DateTime?>("RegistrationLegalDate")
                    .HasColumnName("RegistrationLegalDate")
                    .HasColumnType("date");

                b.Property<string>("Regon")
                    .HasColumnName("Regon")
                    .HasColumnType("varchar(14)")
                    .HasMaxLength(14);

                b.Property<string>("RemovalBasis")
                    .HasColumnName("RemovalBasis")
                    .HasColumnType("varchar(200)")
                    .HasMaxLength(200);

                b.Property<DateTime?>("RemovalDate")
                    .HasColumnName("RemovalDate")
                    .HasColumnType("date");

                b.Property<Guid?>("RequestAndResponseHistoryId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("ResidenceAddress")
                    .HasColumnName("ResidenceAddress")
                    .HasColumnType("varchar(200)")
                    .HasMaxLength(200);

                b.Property<string>("RestorationBasis")
                    .HasColumnName("RestorationBasis")
                    .HasColumnType("varchar(200)")
                    .HasMaxLength(200);

                b.Property<DateTime?>("RestorationDate")
                    .HasColumnName("RestorationDate")
                    .HasColumnType("date");

                b.Property<string>("StatusVat")
                    .HasColumnName("StatusVat")
                    .HasColumnType("varchar(32)")
                    .HasMaxLength(32);

                b.Property<string>("UniqueIdentifierOfTheLoggedInUser")
                    .IsRequired()
                    .HasColumnName("UniqueIdentifierOfTheLoggedInUser")
                    .HasColumnType("varchar(512)")
                    .HasMaxLength(512);

                b.Property<string>("WorkingAddress")
                    .HasColumnName("WorkingAddress")
                    .HasColumnType("varchar(200)")
                    .HasMaxLength(200);

                b.HasKey("Id");

                b.HasIndex("DateOfChecking")
                    .HasDatabaseName("IX_EntityDateOfChecking");

                b.HasIndex("DateOfCreate")
                    .HasDatabaseName("IX_EntityDateOfCreate");

                b.HasIndex("DateOfModification")
                    .HasDatabaseName("IX_EntityDateOfModification");

                b.HasIndex("Id")
                    .IsUnique()
                    .HasDatabaseName("IX_EntityId");

                b.HasIndex("Krs")
                    .HasDatabaseName("IX_EntityKrs");

                b.HasIndex("Name")
                    .HasDatabaseName("IX_EntityName");

                b.HasIndex("Nip")
                    .HasDatabaseName("IX_EntityNip");

                b.HasIndex("Pesel")
                    .IsUnique()
                    .HasDatabaseName("IX_EntityPesel")
                    .HasFilter("[Pesel] IS NOT NULL");

                b.HasIndex("Regon")
                    .HasDatabaseName("IX_EntityRegon");

                b.HasIndex("RequestAndResponseHistoryId")
                    .HasDatabaseName("IX_EntityRequestAndResponseHistoryId");

                b.HasIndex("UniqueIdentifierOfTheLoggedInUser")
                    .HasDatabaseName("IX_EntityUniqueIdentifierOfTheLoggedInUser");

                b.ToTable("Entity", "awpv");
            });

            modelBuilder.Entity("ApiWykazuPodatnikowVatData.Models.EntityAccountNumber", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("(newsequentialid())");

                b.Property<string>("AccountNumber")
                    .IsRequired()
                    .HasColumnName("AccountNumber")
                    .HasColumnType("varchar(32)")
                    .HasMaxLength(32);

                b.Property<DateTime>("DateOfCreate")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("DateOfCreate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                b.Property<DateTime?>("DateOfModification")
                    .HasColumnName("DateOfModification")
                    .HasColumnType("datetime");

                b.Property<Guid?>("EntityId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("UniqueIdentifierOfTheLoggedInUser")
                    .IsRequired()
                    .HasColumnName("UniqueIdentifierOfTheLoggedInUser")
                    .HasColumnType("varchar(512)")
                    .HasMaxLength(512);

                b.HasKey("Id");

                b.HasIndex("AccountNumber")
                    .HasDatabaseName("IX_EntityAccountNumberAccountNumber");

                b.HasIndex("EntityId")
                    .HasDatabaseName("IX_EntityAccountEntityId");

                b.HasIndex("Id")
                    .IsUnique()
                    .HasDatabaseName("IX_EntityAccountNumberId");

                b.HasIndex("UniqueIdentifierOfTheLoggedInUser")
                    .HasDatabaseName("IX_EntityAccountNumberUniqueIdentifierOfTheLoggedInUser");

                b.HasIndex("EntityId", "AccountNumber")
                    .IsUnique()
                    .HasDatabaseName("IX_EntityAccountNumberUniqueKey")
                    .HasFilter("[EntityId] IS NOT NULL");

                b.ToTable("EntityAccountNumber", "awpv");
            });

            modelBuilder.Entity("ApiWykazuPodatnikowVatData.Models.EntityCheck", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("(newsequentialid())");

                b.Property<string>("AccountAssigned")
                    .IsRequired()
                    .HasColumnName("AccountAssigned")
                    .HasColumnType("varchar(3)")
                    .HasMaxLength(3);

                b.Property<string>("AccountNumber")
                    .HasColumnName("AccountNumber")
                    .HasColumnType("varchar(32)")
                    .HasMaxLength(32);

                b.Property<DateTime>("DateOfCreate")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("DateOfCreate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                b.Property<DateTime?>("DateOfModification")
                    .HasColumnName("DateOfModification")
                    .HasColumnType("datetime");

                b.Property<string>("Nip")
                    .HasColumnName("Nip")
                    .HasColumnType("varchar(10)")
                    .HasMaxLength(10);

                b.Property<string>("Regon")
                    .HasColumnName("Regon")
                    .HasColumnType("varchar(14)")
                    .HasMaxLength(14);

                b.Property<Guid?>("RequestAndResponseHistoryId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("RequestDateTime")
                    .IsRequired()
                    .HasColumnName("RequestDateTime")
                    .HasColumnType("varchar(19)")
                    .HasMaxLength(19);

                b.Property<DateTime?>("RequestDateTimeAsDate")
                    .HasColumnName("RequestDateTimeAsDate")
                    .HasColumnType("datetime");

                b.Property<string>("RequestId")
                    .IsRequired()
                    .HasColumnName("RequestId")
                    .HasColumnType("varchar(18)")
                    .HasMaxLength(18);

                b.Property<string>("UniqueIdentifierOfTheLoggedInUser")
                    .IsRequired()
                    .HasColumnName("UniqueIdentifierOfTheLoggedInUser")
                    .HasColumnType("varchar(512)")
                    .HasMaxLength(512);

                b.HasKey("Id");

                b.HasIndex("AccountNumber")
                    .HasDatabaseName("IX_EntityCheckAccountNumber");

                b.HasIndex("DateOfCreate")
                    .HasDatabaseName("IX_EntityCheckDateOfCreate");

                b.HasIndex("DateOfModification")
                    .HasDatabaseName("IX_EntityCheckDateOfModification");

                b.HasIndex("Id")
                    .IsUnique()
                    .HasDatabaseName("IX_EntityCheckId");

                b.HasIndex("Nip")
                    .HasDatabaseName("IX_EntityCheckNip");

                b.HasIndex("Regon")
                    .HasDatabaseName("IX_EntityCheckRegon");

                b.HasIndex("RequestAndResponseHistoryId")
                    .HasDatabaseName("IX_EntityCheckRequestAndResponseHistoryId");

                b.HasIndex("RequestId")
                    .HasDatabaseName("IX_EntityCheckRequestId");

                b.HasIndex("UniqueIdentifierOfTheLoggedInUser")
                    .HasDatabaseName("IX_EntityCheckUniqueIdentifierOfTheLoggedInUser");

                b.ToTable("EntityCheck", "awpv");
            });

            modelBuilder.Entity("ApiWykazuPodatnikowVatData.Models.EntityPerson", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("(newsequentialid())");

                b.Property<string>("CompanyName")
                    .HasColumnName("CompanyName")
                    .HasColumnType("varchar(256)")
                    .HasMaxLength(256);

                b.Property<DateTime>("DateOfCreate")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DateOfCreate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                b.Property<DateTime?>("DateOfModification")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("DateOfModification")
                    .HasColumnType("datetime");

                b.Property<Guid?>("EntityAuthorizedClerkId")
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid?>("EntityPartnerId")
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid?>("EntityRepresentativeId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("FirstName")
                    .HasColumnName("FirstName")
                    .HasColumnType("varchar(60)")
                    .HasMaxLength(60);

                b.Property<string>("LastName")
                    .HasColumnName("LastName")
                    .HasColumnType("varchar(160)")
                    .HasMaxLength(160);

                b.Property<string>("Nip")
                    .HasColumnName("Nip")
                    .HasColumnType("varchar(10)")
                    .HasMaxLength(10);

                b.Property<string>("Pesel")
                    .HasColumnName("Pesel")
                    .HasColumnType("varchar(11)")
                    .HasMaxLength(11);

                b.Property<string>("UniqueIdentifierOfTheLoggedInUser")
                    .IsRequired()
                    .HasColumnName("UniqueIdentifierOfTheLoggedInUser")
                    .HasColumnType("varchar(512)")
                    .HasMaxLength(512);

                b.HasKey("Id");

                b.HasIndex("CompanyName")
                    .HasDatabaseName("IX_EntityPersonCompanyName");

                b.HasIndex("EntityAuthorizedClerkId")
                    .HasDatabaseName("IX_EntityPersonEntityAuthorizedClerkId");

                b.HasIndex("EntityPartnerId")
                    .HasDatabaseName("IX_EntityPersonEntityPartnerId");

                b.HasIndex("EntityRepresentativeId")
                    .HasDatabaseName("IX_EntityPersonEntityRepresentativeId");

                b.HasIndex("Id")
                    .IsUnique()
                    .HasDatabaseName("IX_EntityPersonId");

                b.HasIndex("Nip")
                    .HasDatabaseName("IX_EntityNip");

                b.HasIndex("Pesel")
                    .HasDatabaseName("IX_EntityPersonPesel");

                b.HasIndex("UniqueIdentifierOfTheLoggedInUser")
                    .HasDatabaseName("IX_EntityPersonUniqueIdentifierOfTheLoggedInUser");

                b.ToTable("EntityPerson", "awpv");
            });

            modelBuilder.Entity("ApiWykazuPodatnikowVatData.Models.RequestAndResponseHistory", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("(newsequentialid())");

                b.Property<DateTime>("DateOfCreate")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("DateOfCreate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                b.Property<DateTime?>("DateOfModification")
                    .HasColumnName("DateOfModification")
                    .HasColumnType("datetime");

                b.Property<string>("ObjectMD5Hash")
                    .IsRequired()
                    .HasColumnName("ObjectMD5Hash")
                    .HasColumnType("varchar(24)")
                    .HasMaxLength(24);

                b.Property<string>("RequestBody")
                    .IsRequired()
                    .HasColumnName("RequestBody")
                    .HasColumnType("varchar(max)");

                b.Property<string>("RequestDateTime")
                    .IsRequired()
                    .HasColumnName("RequestDateTime")
                    .HasColumnType("varchar(32)")
                    .HasMaxLength(32);

                b.Property<DateTime?>("RequestDateTimeAsDateTime")
                    .HasColumnName("RequestDateTimeAsDateTime")
                    .HasColumnType("datetime");

                b.Property<string>("RequestId")
                    .IsRequired()
                    .HasColumnName("RequestId")
                    .HasColumnType("varchar(32)")
                    .HasMaxLength(32);

                b.Property<string>("RequestParameters")
                    .IsRequired()
                    .HasColumnName("RequestParameters")
                    .HasColumnType("varchar(max)");

                b.Property<string>("RequestUrl")
                    .IsRequired()
                    .HasColumnName("RequestUrl")
                    .HasColumnType("varchar(512)")
                    .HasMaxLength(512);

                b.Property<string>("ResponseContent")
                    .IsRequired()
                    .HasColumnName("ResponseContent")
                    .HasColumnType("varchar(max)");

                b.Property<string>("ResponseHeaders")
                    .IsRequired()
                    .HasColumnName("ResponseHeaders")
                    .HasColumnType("varchar(max)");

                b.Property<string>("ResponseStatusCode")
                    .IsRequired()
                    .HasColumnName("ResponseStatusCode")
                    .HasColumnType("varchar(32)")
                    .HasMaxLength(32);

                b.Property<string>("UniqueIdentifierOfTheLoggedInUser")
                    .IsRequired()
                    .HasColumnName("UniqueIdentifierOfTheLoggedInUser")
                    .HasColumnType("varchar(512)")
                    .HasMaxLength(512);

                b.HasKey("Id");

                b.HasIndex("DateOfCreate")
                    .HasDatabaseName("IX_RequestAndResponseHistoryDateOfCreate");

                b.HasIndex("DateOfModification")
                    .HasDatabaseName("IX_RequestAndResponseHistoryDateOfModification");

                b.HasIndex("Id")
                    .IsUnique()
                    .HasDatabaseName("IX_RequestAndResponseHistoryId");

                b.HasIndex("ObjectMD5Hash")
                    .IsUnique()
                    .HasDatabaseName("IX_RequestAndResponseHistoryRequestObjectMD5Hash");

                b.HasIndex("RequestDateTime")
                    .HasDatabaseName("IX_RequestAndResponseHistoryRequestDateTime");

                b.HasIndex("RequestDateTimeAsDateTime")
                    .HasDatabaseName("IX_RequestAndResponseHistoryRequestDateTimeAsDateTime");

                b.HasIndex("RequestId")
                    .HasDatabaseName("IX_RequestAndResponseHistoryRequestId");

                b.HasIndex("RequestUrl")
                    .HasDatabaseName("IX_RequestAndResponseHistoryRequestUrl");

                b.HasIndex("ResponseStatusCode")
                    .HasDatabaseName("IX_RequestAndResponseHistoryResponseStatusCode");

                b.HasIndex("UniqueIdentifierOfTheLoggedInUser")
                    .HasDatabaseName("IX_RequestAndResponseHistoryUniqueIdentifierOfTheLoggedInUser");

                b.ToTable("RequestAndResponseHistory", "awpv");
            });

            modelBuilder.Entity("ApiWykazuPodatnikowVatData.Models.Entity", b =>
            {
                b.HasOne("ApiWykazuPodatnikowVatData.Models.RequestAndResponseHistory", "RequestAndResponseHistory")
                    .WithOne("Entity")
                    .HasForeignKey("ApiWykazuPodatnikowVatData.Models.Entity", "RequestAndResponseHistoryId");
            });

            modelBuilder.Entity("ApiWykazuPodatnikowVatData.Models.EntityAccountNumber", b =>
            {
                b.HasOne("ApiWykazuPodatnikowVatData.Models.Entity", "Entity")
                    .WithMany("EntityAccountNumber")
                    .HasForeignKey("EntityId");
            });

            modelBuilder.Entity("ApiWykazuPodatnikowVatData.Models.EntityCheck", b =>
            {
                b.HasOne("ApiWykazuPodatnikowVatData.Models.RequestAndResponseHistory", "RequestAndResponseHistory")
                    .WithOne("EntityCheck")
                    .HasForeignKey("ApiWykazuPodatnikowVatData.Models.EntityCheck", "RequestAndResponseHistoryId");
            });

            modelBuilder.Entity("ApiWykazuPodatnikowVatData.Models.EntityPerson", b =>
            {
                b.HasOne("ApiWykazuPodatnikowVatData.Models.Entity", "AuthorizedClerk")
                    .WithMany("AuthorizedClerk")
                    .HasForeignKey("EntityAuthorizedClerkId");

                b.HasOne("ApiWykazuPodatnikowVatData.Models.Entity", "Partner")
                    .WithMany("Partner")
                    .HasForeignKey("EntityPartnerId");

                b.HasOne("ApiWykazuPodatnikowVatData.Models.Entity", "Representative")
                    .WithMany("Representative")
                    .HasForeignKey("EntityRepresentativeId");
            });
#pragma warning restore 612, 618
        }
    }
}
