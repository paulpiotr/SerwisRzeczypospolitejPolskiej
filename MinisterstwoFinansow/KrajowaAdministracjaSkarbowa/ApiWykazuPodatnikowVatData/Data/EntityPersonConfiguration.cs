#region using

using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityPersonConfiguration : IEntityTypeConfiguration<EntityPerson>

    /// <summary>
    ///     Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji EntityPerson
    /// </summary>
    internal class EntityPersonConfiguration : IEntityTypeConfiguration<EntityPerson>
    {
        /// <summary>
        ///     Konfiguruj
        /// </summary>
        /// <param name="entity">
        ///     Kreator typów jednostek jako EntityTypeBuilder dla modelu EntityPerson
        /// </param>
        public void Configure(EntityTypeBuilder<EntityPerson> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasDatabaseName("IX_EntityPersonId")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasDatabaseName("IX_EntityPersonUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.EntityRepresentativeId)
                .HasDatabaseName("IX_EntityPersonEntityRepresentativeId")
                .IsUnique(false);

            entity.HasIndex(e => e.EntityAuthorizedClerkId)
                .HasDatabaseName("IX_EntityPersonEntityAuthorizedClerkId")
                .IsUnique(false);

            entity.HasIndex(e => e.EntityPartnerId)
                .HasDatabaseName("IX_EntityPersonEntityPartnerId")
                .IsUnique(false);

            entity.HasIndex(e => e.Pesel)
                .HasDatabaseName("IX_EntityPersonPesel")
                .IsUnique(false);

            entity.HasIndex(e => e.CompanyName)
                .HasDatabaseName("IX_EntityPersonCompanyName")
                .IsUnique(false);

            entity.HasIndex(e => e.Nip)
                .HasDatabaseName("IX_EntityNip")
                .IsUnique(false);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }

    #endregion
}
