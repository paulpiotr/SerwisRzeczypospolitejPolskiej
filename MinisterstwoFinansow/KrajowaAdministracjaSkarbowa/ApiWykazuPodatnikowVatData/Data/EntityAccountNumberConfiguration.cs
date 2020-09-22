using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityAccountNumberConfiguration : IEntityTypeConfiguration<EntityAccountNumber>
    /// <summary>
    /// Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji EntityAccountNumber
    /// </summary>
    internal class EntityAccountNumberConfiguration : IEntityTypeConfiguration<EntityAccountNumber>
    {
        /// <summary>
        /// Konfiguruj
        /// </summary>
        /// <param name="entity">
        /// Kreator typów jednostek jako EntityTypeBuilder dla modelu EntityAccountNumber
        /// </param>
        public void Configure(EntityTypeBuilder<EntityAccountNumber> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasName("IX_EntityAccountNumberId")
                .IsUnique(true);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasName("IX_EntityAccountNumberUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(true);

            entity.HasIndex(e => e.AccountNumber)
                .HasName("IX_EntityAccountNumberAccountNumber")
                .IsUnique(true);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }
    #endregion
}
