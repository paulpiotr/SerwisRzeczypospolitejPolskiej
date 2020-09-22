using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityPeselConfiguration : IEntityTypeConfiguration<EntityPesel>
    /// <summary>
    /// Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji EntityPesel
    /// </summary>
    internal class EntityPeselConfiguration : IEntityTypeConfiguration<EntityPesel>
    {
        /// <summary>
        /// Konfiguruj
        /// </summary>
        /// <param name="entity">
        /// Kreator typów jednostek jako EntityTypeBuilder dla modelu EntityPesel
        /// </param>
        public void Configure(EntityTypeBuilder<EntityPesel> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasName("IX_EntityPeselId")
                .IsUnique(true);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasName("IX_EntityPeselUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(true);

            entity.HasIndex(e => e.Pesel)
                .HasName("IX_EntityPeselPesel")
                .IsUnique(true);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }
    #endregion
}
