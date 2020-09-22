using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityConfiguration : IEntityTypeConfiguration<Entity>
    /// <summary>
    /// Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji Entity
    /// </summary>
    internal class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        /// <summary>
        /// Konfiguruj
        /// </summary>
        /// <param name="entity">
        /// Kreator typów jednostek jako EntityTypeBuilder dla modelu Entity
        /// </param>
        public void Configure(EntityTypeBuilder<Entity> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasName("IX_EntityId")
                .IsUnique(true);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasName("IX_EntityUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.Name)
                .HasName("IX_EntityName")
                .IsUnique(false);

            entity.HasIndex(e => e.Nip)
                .HasName("IX_EntityNip")
                .IsUnique(false);

            entity.HasIndex(e => e.Regon)
                .HasName("IX_EntityRegon")
                .IsUnique(false);

            entity.HasIndex(e => e.EntityPeselId)
                .HasName("IX_EntityEntityPeselId")
                .IsUnique(true);

            entity.HasIndex(e => e.Krs)
                .HasName("IX_EntityKrs")
                .IsUnique(false);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }
    #endregion
}
