#region using

using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityPeselConfiguration : IEntityTypeConfiguration<EntityPesel>

    /// <summary>
    ///     Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji EntityPesel
    /// </summary>
    internal class EntityPeselConfiguration : IEntityTypeConfiguration<EntityPesel>
    {
        /// <summary>
        ///     Konfiguruj
        /// </summary>
        /// <param name="entity">
        ///     Kreator typów jednostek jako EntityTypeBuilder dla modelu EntityPesel
        /// </param>
        public void Configure(EntityTypeBuilder<EntityPesel> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasDatabaseName("IX_EntityPeselId")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasDatabaseName("IX_EntityPeselUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.Pesel)
                .HasDatabaseName("IX_EntityPeselPesel")
                .IsUnique();

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }

    #endregion
}
