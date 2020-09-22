﻿using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWykazuPodatnikowVatData.Data
{
    #region class EntityCheckConfiguration : IEntityTypeConfiguration<EntityCheck>
    /// <summary>
    /// Klasa konfiguracji dodatkowych ustawień bazy danych dla modelu encji EntityCheck
    /// </summary>
    internal class EntityCheckConfiguration : IEntityTypeConfiguration<EntityCheck>
    {
        /// <summary>
        /// Konfiguruj
        /// </summary>
        /// <param name="entity">
        /// Kreator typów jednostek jako EntityTypeBuilder dla modelu EntityCheck
        /// </param>
        public void Configure(EntityTypeBuilder<EntityCheck> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasName("IX_EntityCheckId")
                .IsUnique(true);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasName("IX_EntityCheckUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestId)
                .HasName("IX_EntityCheckRequestId")
                .IsUnique(false);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }
    #endregion
}
