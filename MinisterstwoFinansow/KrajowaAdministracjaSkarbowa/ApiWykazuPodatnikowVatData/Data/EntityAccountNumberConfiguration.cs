#region using

using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityAccountNumberConfiguration : IEntityTypeConfiguration<EntityAccountNumber>

    /// <summary>
    ///     Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji EntityAccountNumber
    /// </summary>
    internal class EntityAccountNumberConfiguration : IEntityTypeConfiguration<EntityAccountNumber>
    {
        /// <summary>
        ///     Konfiguruj
        /// </summary>
        /// <param name="entity">
        ///     Kreator typów jednostek jako EntityTypeBuilder dla modelu EntityAccountNumber
        /// </param>
        public void Configure(EntityTypeBuilder<EntityAccountNumber> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasDatabaseName("IX_EntityAccountNumberId")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasDatabaseName("IX_EntityAccountNumberUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.EntityId)
                .HasDatabaseName("IX_EntityAccountEntityId")
                .IsUnique(false);

            entity.HasIndex(e => e.AccountNumber)
                .HasDatabaseName("IX_EntityAccountNumberAccountNumber")
                .IsUnique(false);

            entity.HasIndex(e => new {e.EntityId, e.AccountNumber})
                .HasDatabaseName("IX_EntityAccountNumberUniqueKey")
                .IsUnique();

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }

    #endregion
}
