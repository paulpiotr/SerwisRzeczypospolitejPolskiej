using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityPersonConfiguration : IEntityTypeConfiguration<EntityPerson>
    /// <summary>
    /// Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji EntityPerson
    /// </summary>
    internal class EntityPersonConfiguration : IEntityTypeConfiguration<EntityPerson>
    {
        /// <summary>
        /// Konfiguruj
        /// </summary>
        /// <param name="entity">
        /// Kreator typów jednostek jako EntityTypeBuilder dla modelu EntityPerson
        /// </param>
        public void Configure(EntityTypeBuilder<EntityPerson> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasName("IX_EntityPersonId")
                .IsUnique(true);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasName("IX_EntityPersonUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.EntityRepresentativeId)
                .HasName("IX_EntityPersonEntityRepresentativeId")
                .IsUnique(false);

            entity.HasIndex(e => e.EntityAuthorizedClerkId)
                .HasName("IX_EntityPersonEntityAuthorizedClerkId")
                .IsUnique(false);

            entity.HasIndex(e => e.EntityPartnerId)
                .HasName("IX_EntityPersonEntityPartnerId")
                .IsUnique(false);

            entity.HasIndex(e => e.Pesel)
                .HasName("IX_EntityPersonPesel")
                .IsUnique(false);

            entity.HasIndex(e => e.CompanyName)
                .HasName("IX_EntityPersonCompanyName")
                .IsUnique(false);

            entity.HasIndex(e => e.Nip)
                .HasName("IX_EntityNip")
                .IsUnique(false);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }
    #endregion
}
