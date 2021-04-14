#region using

using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityConfiguration : IEntityTypeConfiguration<Entity>

    /// <summary>
    ///     Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji Entity
    /// </summary>
    internal class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        /// <summary>
        ///     Konfiguruj
        /// </summary>
        /// <param name="entity">
        ///     Kreator typów jednostek jako EntityTypeBuilder dla modelu Entity
        /// </param>
        public void Configure(EntityTypeBuilder<Entity> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasDatabaseName("IX_EntityId")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasDatabaseName("IX_EntityUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestAndResponseHistoryId)
                .HasDatabaseName("IX_EntityRequestAndResponseHistoryId")
                .IsUnique(false);

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_EntityName")
                .IsUnique(false);

            entity.HasIndex(e => e.Nip)
                .HasDatabaseName("IX_EntityNip")
                .IsUnique(false);

            entity.HasIndex(e => e.Regon)
                .HasDatabaseName("IX_EntityRegon")
                .IsUnique(false);

            entity.HasIndex(e => e.Pesel)
                .HasDatabaseName("IX_EntityPesel")
                .IsUnique();

            entity.HasIndex(e => e.Krs)
                .HasDatabaseName("IX_EntityKrs")
                .IsUnique(false);

            entity.HasIndex(e => e.DateOfCreate)
                .HasDatabaseName("IX_EntityDateOfCreate")
                .IsUnique(false);

            entity.HasIndex(e => e.DateOfModification)
                .HasDatabaseName("IX_EntityDateOfModification")
                .IsUnique(false);

            entity.HasIndex(e => e.DateOfChecking)
                .HasDatabaseName("IX_EntityDateOfChecking")
                .IsUnique(false);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }

    #endregion
}
