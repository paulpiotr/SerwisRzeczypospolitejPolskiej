#region using

using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityCheckConfiguration : IEntityTypeConfiguration<EntityCheck>

    /// <summary>
    ///     Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji EntityCheck
    /// </summary>
    internal class EntityCheckConfiguration : IEntityTypeConfiguration<EntityCheck>
    {
        /// <summary>
        ///     Konfiguruj
        /// </summary>
        /// <param name="entity">
        ///     Kreator typów jednostek jako EntityTypeBuilder dla modelu EntityCheck
        /// </param>
        public void Configure(EntityTypeBuilder<EntityCheck> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasDatabaseName("IX_EntityCheckId")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasDatabaseName("IX_EntityCheckUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestAndResponseHistoryId)
                .HasDatabaseName("IX_EntityCheckRequestAndResponseHistoryId")
                .IsUnique(false);

            entity.HasIndex(e => e.Nip)
                .HasDatabaseName("IX_EntityCheckNip")
                .IsUnique(false);

            entity.HasIndex(e => e.Regon)
                .HasDatabaseName("IX_EntityCheckRegon")
                .IsUnique(false);

            entity.HasIndex(e => e.AccountNumber)
                .HasDatabaseName("IX_EntityCheckAccountNumber")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestId)
                .HasDatabaseName("IX_EntityCheckRequestId")
                .IsUnique(false);

            entity.HasIndex(e => e.DateOfCreate)
                .HasDatabaseName("IX_EntityCheckDateOfCreate")
                .IsUnique(false);

            entity.HasIndex(e => e.DateOfModification)
                .HasDatabaseName("IX_EntityCheckDateOfModification")
                .IsUnique(false);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }

    #endregion
}
